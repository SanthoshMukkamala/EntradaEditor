using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using Entrada.Editor.Core;
using System.IO;
using DevExpress.XtraRichEdit.Services;
using DevExpress.Utils.Menu;
using System.Diagnostics;
using System.Web;
using Entrada.Editor.Properties;
using DevExpress.XtraRichEdit.API.Native;

namespace Entrada.Editor
{
    public partial class EditorTab : Panel
    {
        private string google_url = "http://www.google.com/search?q={0}";
        private string wikipedia_url = "http://en.wikipedia.org/w/index.php?search={0}";
        private string drugs_url = "http://www.drugs.com/search.php?searchterm={0}";
        private string medicine_url = "http://search.medicinenet.com/search/search_results/default.aspx?sourceType=all&query={0}";

        public DocumentEntity Document { get; private set; }
        public RichEditControl RichEdit { get; private set; }
        public MemoryStream EditedDocument { get; set; }

        public EditorTab()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }

        public EditorTab(DocumentEntity document)
        {
            var rich = new RichEditControl();
            rich.LayoutUnit = DocumentLayoutUnit.Twip;

            rich.Options.Behavior.Save = DocumentCapability.Hidden;
            rich.Options.Behavior.SaveAs = DocumentCapability.Hidden;
            rich.Options.Behavior.CreateNew = DocumentCapability.Hidden;

            if (document.HasWorkInProgress)
            {
                using (var stream = EncryptedFileSystem.GetDecryptedStream(document.WipFile))
                    rich.LoadDocument(stream, DocumentFormat.OpenXml);
            }
            else
            {
                if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "editor")
                {
                    var text = DocumentMacroReplacer.ReplaceContractions(document.RecognizedText, document);
                    rich.Text = text;
                    DocumentMacroReplacer.ReplaceAutoText(rich.Document, document);
                }
                else
                {
                    MemoryStream ms = new MemoryStream();
                    using (ms = EncryptedFileSystem.GetDecryptedStream(document.recognized_wordfile))
                    {
                        rich.LoadDocument(ms, DocumentFormat.Doc);
                    }
                }
            }

            rich.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "editor")
            { rich.ActiveViewType = RichEditViewType.Simple; }
            else
            { rich.ActiveViewType = RichEditViewType.PrintLayout; }
            rich.Dock = DockStyle.Fill;
            rich.ActiveView.ZoomFactor = EditorCore.Settings.DefaultDocumentZoom / 100.0f;

            rich.ModifiedChanged += (o, e) => { document.IsDirty = rich.Modified; };

            Controls.Add(rich);

            Document = document;
            RichEdit = rich;

            AddMultilevelListStyle(rich.Document);

            EditorCore.Settings.SettingsChanged += (o, e) => { if (rich.ActiveView != null) rich.ActiveView.ZoomFactor = EditorCore.Settings.DefaultDocumentZoom / 100.0f; };

            EditorCore.Settings.AutoCorrectListChanged += Settings_AutoCorrectListChanged;
            Settings_AutoCorrectListChanged(null, EventArgs.Empty);

            Document.SaveRequested += Document_SaveRequested;
            Document.CCChanged += Document_CCChanged;

            Document_CCChanged(null, EventArgs.Empty);

            RichEdit.PopupMenuShowing += RichEdit_PopupMenuShowing;
            RichEdit.AutoCorrect += RichEdit_AutoCorrect;
        }

        private void RichEdit_AutoCorrect(object sender, AutoCorrectEventArgs e)
        {
            var info = e.AutoCorrectInfo;

            // We need to set this to null if we don't do a replacement
            e.AutoCorrectInfo = null;

            // No text
            if (info.Text.Length <= 0)
                return;

            // Don't trigger AutoCorrect mid-word
            if (char.IsLetterOrDigit(info.Text[0]))
                return;

            while (true)
            {
                // Special case start of file
                if (!info.DecrementStartPosition())
                {
                    var lookup2 = info.Text.Substring(0, info.Text.Length - 1);

                    if (EditorCore.Settings.AutoCorrectTextList.ContainsKey(lookup2))
                    {
                        if (char.IsUpper(lookup2[0]))
                            info.ReplaceWith = Capitalize(EditorCore.Settings.AutoCorrectTextList[lookup2]) + info.Text.Substring(info.Text.Length - 1);
                        else
                            info.ReplaceWith = EditorCore.Settings.AutoCorrectTextList[lookup2] + info.Text.Substring(info.Text.Length - 1);

                        e.AutoCorrectInfo = info;
                    }

                    return;
                }

                // Only look back up to 3 words for performance
                if (info.Text.Count(c => c == ' ') > 3)
                    return;

                // Chop off the end, which should be a space or punctionation
                var text = info.Text.Substring(0, info.Text.Length - 1);

                // Don't replace half of a word, make sure it's a full word
                if (char.IsLetterOrDigit(text[0]))
                    continue;

                // Remove the extra non-letter/digit that signaled the full word
                var lookup = text.Substring(1);

                if (EditorCore.Settings.AutoCorrectTextList.ContainsKey(lookup))
                {
                    if (char.IsUpper(lookup[0]))
                        info.ReplaceWith = info.Text.Substring(0, 1) + Capitalize(EditorCore.Settings.AutoCorrectTextList[lookup]) + info.Text.Substring(info.Text.Length - 1);
                    else
                        info.ReplaceWith = info.Text.Substring(0, 1) + EditorCore.Settings.AutoCorrectTextList[lookup] + info.Text.Substring(info.Text.Length - 1);

                    e.AutoCorrectInfo = info;
                    return;
                }
            }
        }

        private void Document_CCChanged(object sender, EventArgs e)
        {
            var add = Document.CC.Where(cc => FindBanner("cc" + cc.Unique) == null);
            var remove = GetBanners().Where(ban => !Document.CC.Any(cc => cc.Unique == (ban.Tag as string).Substring(2)));

            foreach (var banner in remove)
                RemoveStatusBanner((string)banner.Tag);

            foreach (var cc in add)
            {
                var msg = string.Format("CC: {0}", cc.Format());
                var banner = AddStatusBanner("cc" + cc.Unique, msg, DockStyle.Bottom, true);

                banner.VisibleChanged += (o, _) =>
                {
                    RemoveStatusBanner((string)banner.Tag);
                    Document.CC.Remove(cc);
                    Document.Demographics.JOBS_CC = Document.CC.Count > 0;
                    Document.IsDirty = true;
                    EditorCore.Background.RaiseRefreshDemographics();
                };
            }
        }

        public StatusBanner AddStatusBanner(string tag, string text, DockStyle dock = DockStyle.Top, bool closeable = false)
        {
            // Don't allow duplicate tagged banners
            RemoveStatusBanner(tag);

            var banner = new StatusBanner();

            banner.Text = text;
            banner.Dock = dock;
            banner.Tag = tag;
            banner.Closable = closeable;

            Controls.Add(banner);
            return banner;
        }

        public void RemoveStatusBanner(string tag)
        {
            var c = FindBanner(tag);

            if (c != null)
                Controls.Remove(c);
        }

        public bool HasBanner(string tag)
        {
            return FindBanner(tag) != null;
        }

        private Control FindBanner(string tag)
        {
            foreach (Control control in Controls)
                if (control is StatusBanner && (string)control.Tag == tag)
                    return control;

            return null;
        }

        private IEnumerable<StatusBanner> GetBanners()
        {
            var banners = new List<StatusBanner>();

            foreach (Control control in Controls)
                if (control is StatusBanner)
                    banners.Add((StatusBanner)control);

            return banners;
        }

        private void Document_SaveRequested(object sender, EventArgs e)
        {
            // Newest version of DevExpress requires
            // RichEdit.SaveDocument be called on the GUI thread.
            if (InvokeRequired)
            {
                Invoke(new EventHandler(Document_SaveRequested), sender, e);
                return;
            }

            // If we try to save while preview is up, we'll save a merged copy
            if (Document.IsPreview)
                return;

            using (var ms = new MemoryStream())
            {
                RichEdit.SaveDocument(ms, DocumentFormat.OpenXml);

                using (var stream = EncryptedFileSystem.GetOutputStream(Document.WipFile))
                {
                    ms.Position = 0;
                    ms.CopyTo(stream);
                }
            }
        }

        private void Settings_AutoCorrectListChanged(object sender, EventArgs e)
        {
            if (RichEdit.Options == null)
                return;

            RichEdit.Options.AutoCorrect.CorrectTwoInitialCapitals = EditorCore.Settings.AutoCorrectTwoCaps;
            RichEdit.Options.AutoCorrect.UseSpellCheckerSuggestions = EditorCore.Settings.AutoCorrectSpellCheck;
        }

        private void RichEdit_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            var word = RichEdit.Document.GetSelectedText();

            if (string.IsNullOrWhiteSpace(word))
                word = RichEdit.Document.GetWordAtCaret();

            if (string.IsNullOrWhiteSpace(word) || word.IsNumber())
                return;

            var http_word = HttpUtility.HtmlEncode(word);

            var google = new DXMenuItem("Google", null, Resources.google);
            google.BeginGroup = true;
            google.Click += (o, _) => { Process.Start(string.Format(google_url, http_word)); };
            e.Menu.Items.Add(google);

            var wiki = new DXMenuItem("Wikipedia", null, Resources.wikipedia);
            wiki.Click += (o, _) => { Process.Start(string.Format(wikipedia_url, http_word)); };
            e.Menu.Items.Add(wiki);

            var drugs = new DXMenuItem("Drugs.com", null, Resources.drugs);
            drugs.Click += (o, _) => { Process.Start(string.Format(drugs_url, http_word)); };
            e.Menu.Items.Add(drugs);

            var medicine = new DXMenuItem("MedicineNet", null, Resources.medicine);
            medicine.Click += (o, _) => { Process.Start(string.Format(medicine_url, http_word)); };
            e.Menu.Items.Add(medicine);
        }

        private string Capitalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            if (text.Length == 1)
                return char.ToUpperInvariant(text[0]).ToString();

            return char.ToUpperInvariant(text[0]) + text.Substring(1);
        }

        private void AddMultilevelListStyle(Document doc)
        {
            // Basically we're adding an empty list with our style, so our style
            // will appear for any new lists that are created.  We only need to add
            // 1 empty list.
            if (doc.AbstractNumberingLists.Count > 0)
                return;

            doc.BeginUpdate();

            // Describe the pattern used for the numbered list.
            // Specify parameters used to represent each list level.
            var list = doc.AbstractNumberingLists.Add();
            list.NumberingType = NumberingType.MultiLevel;

            var level = list.Levels[0];
            level.DisplayFormatString = "{0}.";
            level.ParagraphProperties.FirstLineIndentType = ParagraphFirstLineIndent.Hanging;
            level.ParagraphProperties.FirstLineIndent = 75f;
            level.ParagraphProperties.LeftIndent = 75f;

            level = list.Levels[1];
            level.DisplayFormatString = "{1}.";
            level.ParagraphProperties.FirstLineIndentType = ParagraphFirstLineIndent.Hanging;
            level.ParagraphProperties.FirstLineIndent = 90f;
            level.ParagraphProperties.LeftIndent = 165f;
            level.NumberingFormat = NumberingFormat.LowerLetter;

            level = list.Levels[2];
            level.DisplayFormatString = "{2}.";
            level.ParagraphProperties.FirstLineIndentType = ParagraphFirstLineIndent.Hanging;
            level.ParagraphProperties.FirstLineIndent = 105f;
            level.ParagraphProperties.LeftIndent = 255f;
            level.NumberingFormat = NumberingFormat.LowerRoman;

            doc.EndUpdate();
        }
    }
}
