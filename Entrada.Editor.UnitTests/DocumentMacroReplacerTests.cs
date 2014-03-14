using System;
using DevExpress.XtraRichEdit;
using Entrada.Editor.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Entrada.Editor.UnitTests
{
    [TestClass]
    public class DocumentMacroReplacerTests : BaseTest
    {
        private DocumentEntity entity;

        public DocumentMacroReplacerTests ()
        {
            entity = new DocumentEntity ();

            entity.AutoText.Add ("Text1", "Test Data 1:");
            entity.AutoText.Add ("TEXT2", "Test Data 2:");
            entity.AutoText.Add ("TExt3", "<b>Test Data 3</b>:");
            entity.AutoText.Add ("Auto", "AutoText1:");
            entity.AutoText.Add ("Auto2", "AutoText2:");
        }

        [TestMethod]
        public void AutoTextDefaultTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("autotext1.txt");

            DocumentMacroReplacer.ReplaceAutoText (rtc.Document, entity);

            Assert.AreEqual ("Test Data 1: stuff!\r\n\r\nTest Data 2: stuff!", rtc.Document.Text);
        }

        [TestMethod]
        public void AutoTextCaseInsensitiveTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("autotext2.txt");

            DocumentMacroReplacer.ReplaceAutoText (rtc.Document, entity);

            Assert.AreEqual ("Test Data 1: stuff!\r\n\r\nTest Data 2: stuff!", rtc.Document.Text);
        }

        [TestMethod]
        public void AutoTextStartOfLineOnlyTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("autotext3.txt");

            DocumentMacroReplacer.ReplaceAutoText (rtc.Document, entity);

            Assert.AreEqual ("There is a lot of Text1 stuff!", rtc.Document.Text);
        }

        [TestMethod]
        public void AutoTextSimilarNameTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("autotext4.txt");

            DocumentMacroReplacer.ReplaceAutoText (rtc.Document, entity);

            Assert.AreEqual ("AutoText1: stuff!\r\n\r\nAutoText2: stuff!", rtc.Document.Text);
        }

        [TestMethod]
        public void AutoTextHtmlTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("autotext5.txt");

            DocumentMacroReplacer.ReplaceAutoText (rtc.Document, entity);

            Assert.AreEqual ("Test Data 3: stuff!", rtc.Document.Text);
        }
    }
}
