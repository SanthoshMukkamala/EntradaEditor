using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraRichEdit.API.Native;

namespace Entrada.Editor.Core
{
    public class TextSection
    {
        public string Text { get; set; }

        internal TextSection ()
        {
        }

        internal TextSection (string text)
        {
            Text = text;
        }

        public bool IsEmpty { get { return string.IsNullOrWhiteSpace (Text); } }
    }

    public class TaggedSection : TextSection
    {
        public string Name { get; set; }
        public bool IsJobType { get; set; }

        internal TaggedSection ()
        {
        }

        internal TaggedSection (string name, string text, bool isJobType) : base (text)
        {
            Name = name;
            IsJobType = isJobType;
        }
    }

    public static class DocumentTagger
    {
        public static IEnumerable<TextSection> GetTags (Document doc, string jobSeparator, string patientSeparator, bool includeUntaggedText = false)
        {
            TextSectionBuilder builder = null;

            foreach (var para in doc.Paragraphs) {
                var text = doc.GetText (para.Range);

                // See if this is a close tag
                if (builder != null && (text.Trim () == jobSeparator || text.Trim () == patientSeparator)) {
                    if (includeUntaggedText || builder.IsTagged)
                        yield return builder.ToSection ();
                    builder = null;

                    continue;
                }

                var start_job_tag = text.Trim ().StartsWith (jobSeparator, StringComparison.InvariantCultureIgnoreCase);
                var start_pat_tag = text.Trim ().StartsWith (patientSeparator, StringComparison.InvariantCultureIgnoreCase);

                // If we found a new start tag and we're already in a tag, implicitly close the tag
                if (builder != null && (start_job_tag || start_pat_tag)) {
                    if (includeUntaggedText || builder.IsTagged)
                        yield return builder.ToSection ();
                    builder = null;
                }

                // This is the start of a new job tag
                if (start_job_tag) {
                    builder = new TextSectionBuilder (doc, text.Trim ().Substring (jobSeparator.Length), true);
                    continue;
                }

                // This is the start of a new patient tag
                if (start_pat_tag) {
                    builder = new TextSectionBuilder (doc, text.Trim ().Substring (jobSeparator.Length), false);
                    continue;
                }

                // This is untagged text, start a builder for it
                if (builder == null)
                    builder = new TextSectionBuilder (doc);

                // This is just a block of text, add it to our text range
                builder.AppendParagraph (para);
            }

            // Implicitly close tag at the end of document
            if (builder != null)
                if (includeUntaggedText || builder.IsTagged)
                    yield return builder.ToSection ();
        }

        class TextSectionBuilder
        {
            private Document document;
            private bool is_tagged;
            private string name;
            private bool is_job;
            private DocumentPosition tag_start;
            private DocumentPosition tag_end;

            public TextSectionBuilder (Document document)
            {
                this.document = document;
            }

            public TextSectionBuilder (Document document, string name, bool isJob) : this (document)
            {
                this.name = name;
                is_job = isJob;
                is_tagged = true;
            }

            public bool IsTagged { get { return is_tagged; } }

            public void AppendParagraph (Paragraph paragraph)
            {
                if (tag_start == null)
                    tag_start = paragraph.Range.Start;

                tag_end = paragraph.Range.End;
            }

            public TextSection ToSection ()
            {
                // Untagged text
                if (!is_tagged) {
                    var text_range = document.CreateRange (tag_start, tag_end.ToInt () - tag_start.ToInt ());
                    return new TextSection (document.GetText (text_range));
                }

                // Tag with no text
                if (tag_start == null)
                    return new TaggedSection (name, string.Empty, is_job);

                var tag_range = document.CreateRange (tag_start, tag_end.ToInt () - tag_start.ToInt ());
                return new TaggedSection (name, document.GetText (tag_range), is_job);
            }
        }
    }
}
