using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.XtraRichEdit.Export;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
	public static class RichEditDocumentExtensions
	{
        public static string GetSelectedText (this Document doc)
        {
            try {
                return doc.GetText (doc.Selection);
            } catch (Exception) {
                // Occasionally we get "specified position belongs to another document"
                return string.Empty;
            }
        }

		public static string GetWordAtCaret (this Document doc)
		{
			return GetWordAtPosition (doc, doc.CaretPosition);
		}

        public static string GetSelectedTextOrWord (this Document doc)
        {
            var text = GetSelectedText (doc);

            if (!string.IsNullOrWhiteSpace (text))
                return text;

            return GetWordAtCaret (doc);
        }

		public static string GetWordAtPosition (this Document doc, DocumentPosition pos)
		{
            try {
                var orig = doc.GetText (doc.Range, new PlainTextDocumentExporterOptions { ExportHiddenText = true }).Replace ("\r\n", "\n");
                var word = new System.Text.StringBuilder ();

                // Look for letters to the left of the position
                for (var i = Math.Min (pos.ToInt (), orig.Length) - 1; i >= 0; i--) {
                    var c = orig[i];

                    if (!char.IsControl (c) && !char.IsPunctuation (c) && !char.IsWhiteSpace (c))
                        word.Insert (0, c);
                    else
                        break;
                }

                // Look for letters to the right of the position
                for (var i = pos.ToInt (); i < orig.Length; i++) {
                    var c = orig[i];

                    if (!char.IsControl (c) && !char.IsPunctuation (c) && !char.IsWhiteSpace (c))
                        word.Append (c);
                    else
                        break;
                }

                return word.ToString ();
            } catch (Exception) {
                // Occasionally we get "specified position belongs to another document"
                return string.Empty;
            }
		}

		public static bool IsNumber (this string text)
		{
			int number;

			if (int.TryParse (text, out number))
				return true;

			return false;
		}

        public static void AutoSentence (this Document doc)
        {
            try {
                var pos = doc.CaretPosition.ToInt ();
                var orig = doc.GetText (doc.Range, new PlainTextDocumentExporterOptions { ExportHiddenText = true }).Replace ("\r\n", "\n");

                // If this is the beginning of the document, bail
                if (pos == 0)
                    return;

                // If this is the end of the document, ensure there's a period and bail
                if (pos == orig.Length) {
                    if (!char.IsPunctuation (orig[pos - 1]))
                        doc.InsertText (doc.CreatePosition (pos), ".");

                    return;
                }

                // If we're in the middle of a word, move to the end
                if (IsWordCharacter (orig[pos - 1]) && IsWordCharacter (orig[pos]))
                    pos = MoveToEndOfWord (orig, pos);

                // If there's a period to our right, move past it
                if (char.IsPunctuation (orig[pos]))
                    pos++;

                // If there's space to the left, back up
                while (char.IsWhiteSpace (orig[pos - 1]))
                    pos--;

                // At this point, we should be positioned directly after
                // the last word of the sentence.
                var doc_ptr = pos;
                doc.BeginUpdate ();

                // If there's not a period, add one
                if (!char.IsPunctuation (orig[pos - 1])) {
                    doc.InsertText (doc.CreatePosition (doc_ptr), ".");
                    doc_ptr++;
                }

                // If there's not a space, add one
                if (!char.IsWhiteSpace (orig[pos])) {
                    doc.InsertText (doc.CreatePosition (doc_ptr), " ");
                    doc_ptr++;
                } else {
                    pos++;
                    doc_ptr++;
                }

                // Uncomment if you want 2 spaces after the period.
                //// If there's not a space, add one
                //if (!char.IsWhiteSpace (orig[pos])) {
                //    doc.InsertText (doc.CreatePosition (doc_ptr), " ");
                //    doc_ptr++;
                //} else {
                //    pos++;
                //    doc_ptr++;
                //}

                // Check if there's too many spaces
                while (orig[pos] == ' ') {
                    doc.Delete (doc.CreateRange (doc_ptr, 1));
                    pos++;
                }

                // Make sure the next letter is uppercase (start of new sentence)
                if (char.IsLower (orig[pos]))
                    doc.Replace (doc.CreateRange (doc_ptr, 1), char.ToUpper (orig[pos]).ToString ());

                doc.EndUpdate ();

                // Move the cursor to the start of the new sentence
                doc.CaretPosition = doc.CreatePosition (doc_ptr);
            } catch (Exception ex) {
                // Log but ignore error
                EditorCore.LogException ("AutoSentence Error", ex);
            }
        }

        private static int MoveToEndOfWord (string text, int pos)
        {
            while (IsWordCharacter (text[pos]))
                pos++;

            return pos;
        }

        private static bool IsWordCharacter (char c)
        {
            return char.IsLetterOrDigit (c) || c == '\'' || c == '-';
        }

        private static bool IsNewLineCharcter (char c)
        {
            return c == '\r' || c == '\n';
        }

        private static Regex underscores = new Regex ("_{4,}");

        public static void ApplyTag (this Document doc, string jobSeparator, TddTag tag)
        {
            DocumentRange range;
            var select_start = -1;
            var select_length = -1;

            // If there's text selected, apply the tag to the selected text
            var selection = doc.Selection;

            if (selection.Length > 0) {
                range = selection;
                select_start = selection.Start.ToInt ();
                select_length = selection.Length;
            } else {
                var pos = doc.CaretPosition;
                var para = doc.GetParagraph (pos);
                range = para.Range;
            }
            
            doc.BeginUpdate ();
            // Don't insert the closing tag, per Kelley
            // doc.InsertFullLine (range.End, jobSeparator);
            doc.InsertFullLine (range.Start, jobSeparator + tag.Name);
            doc.EndUpdate ();

            if (select_start >= 0)
                doc.Selection = doc.CreateRange (range.Start, select_length);
        }

        private static void InsertFullLine (this Document doc, DocumentPosition pos, string text)
        {
            var insert_start_line = false;

            if (pos.ToInt () > 0) {
                var prev_char = doc.GetText (doc.CreateRange (pos.ToInt () - 1, 2));

                if (prev_char.Length == 0 || !IsNewLineCharcter (prev_char[0]))
                    insert_start_line = true;
            }

            if (insert_start_line)
                doc.InsertText (pos, Environment.NewLine + text + Environment.NewLine);
            else
                doc.InsertText (pos, text + Environment.NewLine);
        }

        public static IEnumerable<DocumentRange> FindAnomalies (this Document doc, DocumentRange range = null)
        {
            if (range == null)
                range = doc.CreateRange (0, doc.Length);

            //var question_ranges = doc.FindAll ("??", SearchOptions.WholeWord, range);
            var underscore_ranges = doc.FindAll (underscores, range);
            
            //return question_ranges.Concat (underscore_ranges);
            return underscore_ranges;
        }

        public static DocumentRange FindNextAnomaly (this Document doc)
        {
            var range = doc.CreateRange (doc.CaretPosition, doc.Length - doc.CaretPosition.ToInt ());

            return FindAnomalies (doc, range).OrderBy (p => p.Start).FirstOrDefault ();
        }

        public static DocumentRange FindPreviousAnomaly (this Document doc)
        {
            var range = doc.CreateRange (0, doc.Selection.Start.ToInt ());

            return FindAnomalies (doc, range).OrderByDescending (p => p.Start).FirstOrDefault ();
        }

        public static DocumentRange InsertHiddenText (this Document doc, string text)
        {
            var range = doc.InsertText (doc.CaretPosition, "<macro>");
            var styles = doc.BeginUpdateCharacters (range);

            styles.Hidden = true;
            doc.EndUpdateCharacters (styles);

            return range;
        }

        public static IEnumerable<DocumentRange> FindFemininePronouns (this Document doc)
        {
            return doc.FindAllWords ("she", "hers", "herself", "her", "female", "woman");
        }

        public static IEnumerable<DocumentRange> FindMasculinePronouns (this Document doc)
        {
            return doc.FindAllWords ("he", "him", "himself", "his", "male", "man");
        }

        public static IEnumerable<DocumentRange> FindLeftDirectionWords (this Document doc)
        {
            return doc.FindAllWords ("left");
        }

        public static IEnumerable<DocumentRange> FindRightDirectionWords (this Document doc)
        {
            return doc.FindAllWords ("right");
        }

        public static IEnumerable<DocumentRange> FindAllWords (this Document doc, params string[] words)
        {
            foreach (var word in words)
                foreach (var range in doc.FindAll (word, SearchOptions.None | SearchOptions.WholeWord))
                    yield return range;
        }

        public static void ColorWords (this Document doc, IEnumerable<DocumentRange> ranges, System.Drawing.Color color)
        {
            doc.BeginUpdate ();

            foreach (var r in ranges) {
                var ch = doc.BeginUpdateCharacters (r);
                ch.BackColor = color;
                doc.EndUpdateCharacters (ch);
            }

            doc.EndUpdate ();
        }

        public static void ClearAllColorWords (this Document doc)
        {
            doc.BeginUpdate ();

            var ch = doc.BeginUpdateCharacters (doc.Range);
            ch.BackColor = System.Drawing.Color.Transparent;
            doc.EndUpdateCharacters (ch);

            doc.EndUpdate ();
        }
    }
}
