using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entrada.Editor.Core
{
    public static class DocumentMerger
    {
        public static Task<Stream> Merge (this DocumentEntity document, Stream templateStream = null)
        {
            return Task.Factory.StartNew (() => {
                Aspose.Words.License lic = new Aspose.Words.License ();
                lic.SetLicense ("Aspose.Words.lic");

                using (EditorCore.CreateStopwatch ("Merging document")) {

                    Aspose.Words.Document doc = null;

                    if (templateStream != null)
                        doc = new Aspose.Words.Document (templateStream);
                    else {
                        var template = document.SelectedDocumentTemplate;

                        if (template == null)
                            throw new Exception ("null template");

                        var template_file = Path.Combine (EditorCore.Settings.JobsDirectory, document.JobNumber, template);

                        doc = new Aspose.Words.Document (template_file);
                    }

                    // Store custom properties
                    document.CustomDocumentProperties.Clear ();

                    foreach (Aspose.Words.Properties.DocumentProperty de in doc.CustomDocumentProperties)
                        document.CustomDocumentProperties[de.Name] = (int)de.Type;
                    
                    SetCustomProperties (document, doc);

                    var text = document.EditedText;

                    // This should have been done when the macro was inserted
                    //foreach (var entry in document.Macros)
                    //    text = text.Replace ("[" + (string)entry.Key + "]", (string)entry.Value);

                    // Execute mail merge.
                    doc.MailMerge.Execute (new string[] { }, new string[] { });
                    
                    // Remove merge fields
                    UnlinkFields (doc);

                    var ms = new MemoryStream ();
                    doc.Save (ms, Aspose.Words.SaveFormat.Doc);

#if DEBUG_DOCUMENTS
                    doc.Save (@"C:\temp\merged.doc", Aspose.Words.SaveFormat.Doc);
#endif

                    ms.Position = 0;

                    return (Stream)ms;
                }
            });
        }

        public static MemoryStream AddCustomProperties (this DocumentEntity document, Stream input)
        {
            var lic = new Aspose.Words.License ();
            lic.SetLicense ("Aspose.Words.lic");
            input.Position = 0;

            using (EditorCore.CreateStopwatch ("Setting Document Custom Properties")) {

                var doc = new Aspose.Words.Document (input);

                // Merge custom properties
                foreach (var prop in document.CustomDocumentProperties)
                    doc.CustomDocumentProperties.Add (document, prop);

                var ms = new MemoryStream ();
                doc.Save (ms, Aspose.Words.SaveFormat.Doc);

#if DEBUG_DOCUMENTS
                    doc.Save (@"C:\temp\addproperties.doc", Aspose.Words.SaveFormat.Doc);
#endif

                ms.Position = 0;

                return ms;
            }
        }

        private static void SetCustomProperties (DocumentEntity document, Aspose.Words.Document file)
        {
            foreach (Aspose.Words.Properties.DocumentProperty de in file.CustomDocumentProperties) {
                if (document.Demographics.Raw.ContainsKey (de.Name))
                    de.Value = document.Demographics.Raw[de.Name] ?? string.Empty;
                else if (de.Name.ToLower () == "jobnumber")
                    de.Value = document.JobNumber;
            }
        }

        private static void Add (this Aspose.Words.Properties.CustomDocumentProperties properties, DocumentEntity document, KeyValuePair<string, int> prop)
        {
            switch ((Aspose.Words.Properties.PropertyType)prop.Value) {
                case Aspose.Words.Properties.PropertyType.Boolean:
                    properties.Add (prop.Key, GetBool (document, prop.Key));
                    return;
                case Aspose.Words.Properties.PropertyType.DateTime:
                    properties.Add (prop.Key, GetDateTime (document, prop.Key));
                    return;
                case Aspose.Words.Properties.PropertyType.Double:
                    properties.Add (prop.Key, GetDouble (document, prop.Key));
                    return;
                case Aspose.Words.Properties.PropertyType.Number:
                    properties.Add (prop.Key, GetInt (document, prop.Key));
                    return;
                case Aspose.Words.Properties.PropertyType.String:
                default:
                    properties.Add (prop.Key, GetString (document, prop.Key));
                    return;
            }
        }

        private static string GetString (DocumentEntity document, string fieldName)
        {
            if (fieldName.ToLowerInvariant () == "jobnumber")
                return document.JobNumber;

            if (!document.Demographics.Raw.ContainsKey (fieldName))
                return string.Empty;

            return document.Demographics.Raw[fieldName] ?? string.Empty;
        }

        private static bool GetBool (DocumentEntity document, string fieldName)
        {
            if (!document.Demographics.Raw.ContainsKey (fieldName) || string.IsNullOrWhiteSpace (document.Demographics.Raw[fieldName]))
                return false;

            bool result = false;

            if (bool.TryParse (document.Demographics.Raw[fieldName], out result))
                return result;

            return false;
        }

        private static DateTime GetDateTime (DocumentEntity document, string fieldName)
        {
            if (!document.Demographics.Raw.ContainsKey (fieldName) || string.IsNullOrWhiteSpace (document.Demographics.Raw[fieldName]))
                return new DateTime (2001, 1, 1);

            DateTime result;

            if (DateTime.TryParse (document.Demographics.Raw[fieldName], out result))
                return result;

            return new DateTime (2001, 1, 1);
        }

        private static int GetInt (DocumentEntity document, string fieldName)
        {
            if (!document.Demographics.Raw.ContainsKey (fieldName) || string.IsNullOrWhiteSpace (document.Demographics.Raw[fieldName]))
                return 0;

            int result;

            if (int.TryParse (document.Demographics.Raw[fieldName], out result))
                return result;

            return 0;
        }

        private static double GetDouble (DocumentEntity document, string fieldName)
        {
            if (!document.Demographics.Raw.ContainsKey (fieldName) || string.IsNullOrWhiteSpace (document.Demographics.Raw[fieldName]))
                return 0;

            double result;

            if (double.TryParse (document.Demographics.Raw[fieldName], out result))
                return result;

            return 0;
        }

        // Removes the property merge fields from the document after we've merged them
        private static void UnlinkFields (Aspose.Words.Document doc)
        {
            // Get all the fields in the document
            var propertyStarts = new List<Aspose.Words.Fields.FieldStart> ();
            var starts = doc.GetChildNodes (Aspose.Words.NodeType.FieldStart, true);
            
            foreach (Aspose.Words.Fields.FieldStart start in starts)
                if (start.FieldType == Aspose.Words.Fields.FieldType.FieldDocProperty || start.FieldType == Aspose.Words.Fields.FieldType.FieldDate)
                    propertyStarts.Add (start);
            
            // Remove each DOCUMENTPROPERTY field start
            foreach (var start in propertyStarts) {
                Aspose.Words.Node currentNode = start;
                Aspose.Words.Node fieldSeparator = null;

                // Remove field code
                while (currentNode.NodeType != Aspose.Words.NodeType.FieldSeparator) {
                    currentNode = currentNode.NextSibling;
                    currentNode.PreviousSibling.Remove ();
                }

                fieldSeparator = currentNode;

                // Move to field end
                while (currentNode.NodeType != Aspose.Words.NodeType.FieldEnd)
                    currentNode = currentNode.NextSibling;

                // Remove field separator
                fieldSeparator.Remove ();

                // Remove field end
                currentNode.Remove ();
            }
        }
    }
}
