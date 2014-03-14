using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit.API.Native;

namespace Entrada.Editor.Core
{
    public static class DocumentMacroReplacer
    {
        public static void ReplaceAutoText (Document doc, DocumentEntity entity)
        {
            foreach (var para in doc.Paragraphs) {
                var text = doc.GetText (para.Range);

                // Find matching autotexts
                var matches = entity.AutoText.Where (p => text.StartsWith (p.Key, StringComparison.InvariantCultureIgnoreCase));

                if (matches.Count () == 0)
                    continue;

                // We want the longest match, in case there are multiples
                var match = matches.OrderByDescending (p => p.Key.Length).First ();

                var match_range = doc.CreateRange (para.Range.Start.ToInt (), match.Key.Length);
                var pos = para.Range.Start;

                // Delete the old text and insert the new text
                doc.Delete (match_range);
                doc.InsertHtmlText (pos, match.Value);
            }
        }

        public static string ReplaceContractions (string text, DocumentEntity entity)
        {
            foreach (var contraction in entity.Contractions)
                text = text.Replace (contraction.Key, contraction.Value);

            return text;
        }

        public static System.Collections.Generic.IEnumerable<int> GetSentenceEnumerator (string text)
        {
            // The beginning of the text is obviously a new sentence
            yield return 0;

            var index = 0;

            while (true) {
                var i = text.IndexOfAny (new char[] { '\r', '\n' }, index);

                // No more found
                if (i < 0)
                    yield break;

                var chr = text[i];

                // Look for LF
                if (chr == '\n') {
                    index = i + 1;
                    yield return index;
                    continue;
                }

                // Look for CRLF
                if (i + 1 < text.Length && text[i + 1] == '\n') {
                    index = i + 2;
                    yield return index;
                    continue;
                }

                // Must just be LF
                index = i + 1;
                yield return index;
                continue;
            }
        }
    }
}
