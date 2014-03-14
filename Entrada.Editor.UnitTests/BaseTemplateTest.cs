using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using Entrada.Editor.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Entrada.Editor.UnitTests
{
    public abstract class BaseTemplateTest
    {
        private string results_folder = @"C:\TemplatesTestResults";
        private string edited = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas nisl mauris, imperdiet at justo tempor, porttitor rutrum felis. Aliquam ligula velit, volutpat vitae ipsum dapibus, luctus fermentum neque. Aenean egestas tellus ipsum, id scelerisque turpis aliquet sit amet. Donec a venenatis ligula. Nam tristique hendrerit consequat. Donec sed ipsum eget leo tempor lobortis sed malesuada ipsum. Maecenas sed vehicula sem. Phasellus nec rutrum massa, non interdum arcu. Donec scelerisque aliquam nisi. Sed faucibus libero ac lorem molestie, in posuere leo blandit. Etiam in justo pellentesque, dignissim libero in, viverra dolor. Sed quis velit ultrices, dignissim quam ac, commodo diam. In lobortis diam velit, id congue massa cursus sit amet. Nullam sagittis dolor ligula, et tristique ligula tincidunt sed. Etiam eu scelerisque risus. Fusce sed euismod nisl, sed sagittis elit.{0}{0}Ut nec neque at orci fermentum semper id molestie augue. Sed vitae tortor eleifend metus iaculis interdum a eget nisl. Aliquam tincidunt nisl sapien, a pellentesque urna aliquet id. Sed sollicitudin, ipsum sit amet sodales malesuada, purus nisl condimentum odio, et adipiscing eros tortor sit amet odio. Aenean vitae arcu erat. Phasellus dignissim tempus purus, non accumsan eros consectetur et. Duis quis nisl eu tellus rhoncus semper at semper dui.{0}";

        internal async Task TemplateTest (string template)
        {
            var lic = new Aspose.Words.License ();
            lic.SetLicense ("Aspose.Words.lic");

            using (var template_stream = GetTemplate (template))
            using (var test_stream = await SimulateMerge (template_stream)) {

                var aspose_doc = new Aspose.Words.Document (test_stream);

                using (var ms_png = new MemoryStream ()) {
                    aspose_doc.Save (ms_png, Aspose.Words.SaveFormat.Png);

                    var id = template.Substring (0, 4);

                    Directory.CreateDirectory (results_folder);
                    aspose_doc.Save (Path.Combine (results_folder, id + ".png"), Aspose.Words.SaveFormat.Png);

                    ms_png.Position = 0;

                    var expected = GetExpectedImage (id);

                    if (expected == null)
                        Assert.Inconclusive ("No expected results for test id {0}", id);

                    Assert.AreEqual (MD5 (expected), MD5 (ms_png));
                }
            }
        }

        private Stream GetTemplate (string template)
        {
            var asm = Assembly.GetExecutingAssembly ();

            return asm.GetManifestResourceStream ("Entrada.Editor.UnitTests.TestTemplates." + template);
        }

        private Stream GetExpectedImage (string id)
        {
            var asm = Assembly.GetExecutingAssembly ();

            return asm.GetManifestResourceStream ("Entrada.Editor.UnitTests.TemplatesExpectedResults." + id + ".png");
        }

        private async Task<Stream> SimulateMerge (Stream template)
        {
            var server = new RichEditDocumentServer ();

            var demographics = CreateDummyDemographics ();

            var doc = new DocumentEntity ("1234567890", demographics);
            doc.EditedText = edited;

            using (var merged = await doc.Merge (template)) {
                server.LoadDocument (merged, DocumentFormat.Doc);

                var vr = server.Document.FindAll ("«TEXT_BODY_VR»", SearchOptions.None);

                // Some templates may not have a place to replace text
                if (vr != null && vr.Length > 0) {
                    var text = server.Document.GetText (vr[0]);
                    server.Document.Replace (vr[0], string.Format (edited, Environment.NewLine));
                }

                using (var final_stream = new MemoryStream ()) {
                    server.Document.SaveDocument (final_stream, DocumentFormat.Doc);

                    // DevExpress doesn't support document properties, so we have to
                    // use Aspose to put them back in case QA needs them
                    return doc.AddCustomProperties (final_stream);
                }
            }
        }

        private static string MD5 (Stream stream)
        {
            using (var md5 = new MD5CryptoServiceProvider ()) {
                var hash_bytes = md5.ComputeHash (stream);
                return FormatHashBytes (hash_bytes);
            }
        }

        private static string FormatHashBytes (byte[] bytes)
        {
            var sb = new StringBuilder ();

            for (int i = 0; i < bytes.Length; i++)
                sb.Append (bytes[i].ToString ("x2"));

            return sb.ToString ().ToLower ();
        }

        private Dictionary<string, string> CreateDummyDemographics ()
        {
            var demographics = new Dictionary<string, string> ();

            demographics.Add ("JOBS_DICTATORID", "entapple");
            demographics.Add ("JOBS_CLINICID", "13");
            demographics.Add ("JOBS_CLINICNAME", "Entrada Surgical Center");
            demographics.Add ("JOBS_LOCATION", "1");
            demographics.Add ("JOBS_LOCATIONNAME", "Brentwood Office");
            demographics.Add ("JOBS_APPOINTMENTDATE", "1/20/2013");
            demographics.Add ("JOBS_APPOINTMENTTIME", "2:00 PM");
            demographics.Add ("JOBS_JOBTYPE", "Letter");
            demographics.Add ("JOBS_STAT", "False");
            demographics.Add ("JOBS_CC", "False");
            demographics.Add ("JOBS_DURATION", "56");
            demographics.Add ("JOBS_DICTATIONDATE", "1/20/2013");
            demographics.Add ("JOBS_DICTATIONTIME", "6:34:12 PM");
            demographics.Add ("DICTATOR_FIRSTNAME", "Greg");
            demographics.Add ("DICTATOR_MI", "R");
            demographics.Add ("DICTATOR_LASTNAME", "Apple");
            demographics.Add ("DICTATOR_SUFFIX", "M.D.");
            demographics.Add ("DICTATOR_INITIALS", "GRA");
            demographics.Add ("DICTATOR_SIGNATURE", "Greg R Apple, M.D.");
            demographics.Add ("DICTATOR_USER_CODE", "apple");
            demographics.Add ("EDITOR_SIGNOFF1", "10000");
            demographics.Add ("EDITOR_SIGNOFF2", "11000");
            demographics.Add ("EDITOR_SIGNOFF3", "12000");
            demographics.Add ("PATIENT_ALTERNATEID", "624667788");
            demographics.Add ("PATIENT_MRN", "133421");
            demographics.Add ("PATIENT_FIRSTNAME", "Smith");
            demographics.Add ("PATIENT_MI", "K");
            demographics.Add ("PATIENT_LASTNAME", "Mary");
            demographics.Add ("PATIENT_SUFFIX", "Jr.");
            demographics.Add ("PATIENT_DOB", "6/12/1967");
            demographics.Add ("PATIENT_SSN", "784-11-2499");
            demographics.Add ("PATIENT_ADDRESS1", "123 Fake Street");
            demographics.Add ("PATIENT_ADDRESS2", "Apt 122");
            demographics.Add ("PATIENT_CITY", "Brentwood");
            demographics.Add ("PATIENT_STATE", "TN");
            demographics.Add ("PATIENT_ZIP", "37027");
            demographics.Add ("PATIENT_PHONE", "615-555-8778");
            demographics.Add ("PATIENT_SEX", "F");
            demographics.Add ("PATIENT_PATIENTID", "754433");
            demographics.Add ("PATIENT_APPOINTMENTID", "996322");
            demographics.Add ("REFERRING_PHYSICIANID", "1222");
            demographics.Add ("REFERRING_FIRSTNAME", "John");
            demographics.Add ("REFERRING_MI", "D");
            demographics.Add ("REFERRING_LASTNAME", "Doe");
            demographics.Add ("REFERRING_SUFFIX", "M.D.");
            demographics.Add ("REFERRING_DOB", "9/6/1954");
            demographics.Add ("REFERRING_SSN", "944-12-7774");
            demographics.Add ("REFERRING_ADDRESS1", "1200 Maryland Way");
            demographics.Add ("REFERRING_ADDRESS2", "Suite 320");
            demographics.Add ("REFERRING_CITY", "Brentwood");
            demographics.Add ("REFERRING_STATE", "TN");
            demographics.Add ("REFERRING_ZIP", "37027");
            demographics.Add ("REFERRING_PHONE", "615-555-1227");
            demographics.Add ("REFERRING_SEX", "M");
            demographics.Add ("REFERRING_FAX", "615-555-1228");
            demographics.Add ("REFERRING_CLINICNAME", "Brentwood General Hospital");
            demographics.Add ("CUSTOM_CUSTOM1", "CUST1");
            demographics.Add ("CUSTOM_CUSTOM2", "CUST2");
            demographics.Add ("CUSTOM_CUSTOM3", "CUST3");
            demographics.Add ("CUSTOM_CUSTOM4", "CUST4");
            demographics.Add ("CUSTOM_CUSTOM5", "CUST5");
            demographics.Add ("CUSTOM_CUSTOM6", "CUST6");
            demographics.Add ("CUSTOM_CUSTOM7", "CUST7");
            demographics.Add ("CUSTOM_CUSTOM8", "CUST8");
            demographics.Add ("CUSTOM_CUSTOM9", "CUST9");
            demographics.Add ("CUSTOM_CUSTOM10", "CUST10");
            demographics.Add ("CUSTOM_CUSTOM11", "CUST11");
            demographics.Add ("CUSTOM_CUSTOM12", "CUST12");
            demographics.Add ("CUSTOM_CUSTOM13", "CUST13");
            demographics.Add ("CUSTOM_CUSTOM14", "CUST14");
            demographics.Add ("CUSTOM_CUSTOM15", "CUST15");
            demographics.Add ("CUSTOM_CUSTOM16", "CUST16");
            demographics.Add ("CUSTOM_CUSTOM17", "CUST17");
            demographics.Add ("CUSTOM_CUSTOM18", "CUST18");
            demographics.Add ("CUSTOM_CUSTOM19", "CUST19");
            demographics.Add ("CUSTOM_CUSTOM20", "CUST20");
            demographics.Add ("CUSTOM_CUSTOM21", "CUST21");
            demographics.Add ("CUSTOM_CUSTOM22", "CUST22");
            demographics.Add ("CUSTOM_CUSTOM23", "CUST23");
            demographics.Add ("CUSTOM_CUSTOM24", "CUST24");
            demographics.Add ("CUSTOM_CUSTOM25", "CUST25");
            demographics.Add ("CUSTOM_CUSTOM26", "CUST26");
            demographics.Add ("CUSTOM_CUSTOM27", "CUST27");
            demographics.Add ("CUSTOM_CUSTOM28", "CUST28");
            demographics.Add ("CUSTOM_CUSTOM29", "CUST29");
            demographics.Add ("CUSTOM_CUSTOM30", "CUST30");
            demographics.Add ("CUSTOM_CUSTOM31", "CUST31");
            demographics.Add ("CUSTOM_CUSTOM32", "CUST32");
            demographics.Add ("CUSTOM_CUSTOM33", "CUST33");
            demographics.Add ("CUSTOM_CUSTOM34", "CUST34");
            demographics.Add ("CUSTOM_CUSTOM35", "CUST35");
            demographics.Add ("CUSTOM_CUSTOM36", "CUST36");
            demographics.Add ("CUSTOM_CUSTOM37", "CUST37");
            demographics.Add ("CUSTOM_CUSTOM38", "CUST38");
            demographics.Add ("CUSTOM_CUSTOM39", "CUST39");
            demographics.Add ("CUSTOM_CUSTOM40", "CUST40");
            demographics.Add ("CUSTOM_CUSTOM41", "CUST41");
            demographics.Add ("CUSTOM_CUSTOM42", "CUST42");
            demographics.Add ("CUSTOM_CUSTOM43", "CUST43");
            demographics.Add ("CUSTOM_CUSTOM44", "CUST44");
            demographics.Add ("CUSTOM_CUSTOM45", "CUST45");
            demographics.Add ("CUSTOM_CUSTOM46", "CUST46");
            demographics.Add ("CUSTOM_CUSTOM47", "CUST47");
            demographics.Add ("CUSTOM_CUSTOM48", "CUST48");
            demographics.Add ("CUSTOM_CUSTOM49", "CUST49");
            demographics.Add ("CUSTOM_CUSTOM50", "CUST50");

            return demographics;
        }
    }
}
