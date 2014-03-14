using System;
using System.Linq;
using DevExpress.XtraRichEdit;
using Entrada.Editor.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Entrada.Editor.UnitTests
{
    [TestClass]
    public class DocumentTaggerTests : BaseTest
    {
        [TestMethod]
        public void TDDDefaultTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd1.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".P:");

            Assert.AreEqual (1, tags.Count ());
            Assert.IsInstanceOfType (tags.First (), typeof (TaggedSection));

            var tag = tags.First () as TaggedSection;

            Assert.AreEqual ("H AND P", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (9, tag.Text.Length);
        }

        [TestMethod]
        public void TDDImplicitCloseTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd2.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".P:");

            Assert.AreEqual (2, tags.Count ());
            Assert.IsInstanceOfType (tags.First (), typeof (TaggedSection));

            var tag = tags.First () as TaggedSection;

            Assert.AreEqual ("H AND P", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (11, tag.Text.Length);

            Assert.IsInstanceOfType (tags.ElementAt (1), typeof (TaggedSection));
            tag = tags.ElementAt (1) as TaggedSection;

            Assert.AreEqual ("LAST", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (14, tag.Text.Length);
        }

        [TestMethod]
        public void TDDEmptyTagTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd3.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".P:");

            Assert.AreEqual (2, tags.Count ());
            Assert.IsInstanceOfType (tags.First (), typeof (TaggedSection));

            var tag = tags.First () as TaggedSection;

            Assert.AreEqual ("EMPTY TAG", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (0, tag.Text.Length);

            Assert.IsInstanceOfType (tags.ElementAt (1), typeof (TaggedSection));
            tag = tags.ElementAt (1) as TaggedSection;

            Assert.AreEqual ("EMPTY END", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (0, tag.Text.Length);
        }

        [TestMethod]
        public void TDDNoTagsTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd4.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".P:");

            Assert.AreEqual (0, tags.Count ());
        }

        [TestMethod]
        public void TDDImplicitMixingOfJobAndPatientTagsTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd5.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".T:");

            Assert.AreEqual (2, tags.Count ());
            Assert.IsInstanceOfType (tags.First (), typeof (TaggedSection));

            var tag = tags.First () as TaggedSection;

            Assert.AreEqual ("JOB TAG", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (11, tag.Text.Length);

            Assert.IsInstanceOfType (tags.ElementAt (1), typeof (TaggedSection));
            tag = tags.ElementAt (1) as TaggedSection;

            Assert.AreEqual ("PATIENT END", tag.Name);
            Assert.IsFalse (tag.IsJobType);
            Assert.AreEqual (12, tag.Text.Length);
        }

        [TestMethod]
        public void TDDEmptyTagNameTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd6.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".P:");

            Assert.AreEqual (1, tags.Count ());
            Assert.IsInstanceOfType (tags.First (), typeof (TaggedSection));

            var tag = tags.First () as TaggedSection;

            Assert.AreEqual (string.Empty, tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (9, tag.Text.Length);
        }

        [TestMethod]
        public void TDDDefaultTestWithUntagged ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd1.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".P:", true);

            Assert.AreEqual (1, tags.Count ());
            Assert.IsInstanceOfType (tags.First (), typeof (TaggedSection));

            var tag = tags.First () as TaggedSection;

            Assert.AreEqual ("H AND P", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (9, tag.Text.Length);
        }

        [TestMethod]
        public void TDDImplicitCloseTestWithUntagged ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd2.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".P:", true);

            Assert.AreEqual (2, tags.Count ());
            Assert.IsInstanceOfType (tags.First (), typeof (TaggedSection));

            var tag = tags.First () as TaggedSection;

            Assert.AreEqual ("H AND P", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (11, tag.Text.Length);

            Assert.IsInstanceOfType (tags.ElementAt (1), typeof (TaggedSection));
            tag = tags.ElementAt (1) as TaggedSection;

            Assert.AreEqual ("LAST", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (14, tag.Text.Length);
        }

        [TestMethod]
        public void TDDEmptyTagTestWithUntagged ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd3.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".P:", true);

            Assert.AreEqual (4, tags.Count ());

            // 1st and 3rd sections are untagged
            Assert.IsInstanceOfType (tags.First (), typeof (TextSection));
            var text = tags.First () as TextSection;
            Assert.AreEqual ("Untagged data\r\n", text.Text);

            Assert.IsInstanceOfType (tags.ElementAt (2), typeof (TextSection));
            text = tags.ElementAt (2) as TextSection;
            Assert.AreEqual ("", text.Text);

            // 2nd and 4th sections are tagged
            Assert.IsInstanceOfType (tags.ElementAt (1), typeof (TaggedSection));
            var tag = tags.ElementAt (1) as TaggedSection;

            Assert.AreEqual ("EMPTY TAG", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (0, tag.Text.Length);

            Assert.IsInstanceOfType (tags.ElementAt (1), typeof (TaggedSection));
            tag = tags.ElementAt (3) as TaggedSection;

            Assert.AreEqual ("EMPTY END", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (0, tag.Text.Length);
        }

        [TestMethod]
        public void TDDNoTagsTestWithUntagged ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd4.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".P:", true);

            Assert.AreEqual (1, tags.Count ());
            Assert.IsInstanceOfType (tags.First (), typeof (TextSection));
            Assert.AreEqual (54, tags.First ().Text.Length);
        }

        [TestMethod]
        public void TDDImplicitMixingOfJobAndPatientTagsWithUntaggedTest ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd5.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".T:", true);

            Assert.AreEqual (3, tags.Count ());

            // 1st section is untagged
            Assert.IsInstanceOfType (tags.First (), typeof (TextSection));
            var text = tags.First () as TextSection;
            Assert.AreEqual ("Untagged data\r\n", text.Text);

            // 2nd and 3rd sections are untagged
            Assert.IsInstanceOfType (tags.ElementAt (1), typeof (TaggedSection));

            var tag = tags.ElementAt (1) as TaggedSection;

            Assert.AreEqual ("JOB TAG", tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (11, tag.Text.Length);

            Assert.IsInstanceOfType (tags.ElementAt (2), typeof (TaggedSection));
            tag = tags.ElementAt (2) as TaggedSection;

            Assert.AreEqual ("PATIENT END", tag.Name);
            Assert.IsFalse (tag.IsJobType);
            Assert.AreEqual (12, tag.Text.Length);
        }

        [TestMethod]
        public void TDDEmptyTagNameTestWithUntagged ()
        {
            var rtc = new RichEditDocumentServer ();
            rtc.Text = GetText ("tdd6.txt");

            var tags = DocumentTagger.GetTags (rtc.Document, ".P:", ".P:", true);

            Assert.AreEqual (1, tags.Count ());
            Assert.IsInstanceOfType (tags.First (), typeof (TaggedSection));

            var tag = tags.First () as TaggedSection;

            Assert.AreEqual (string.Empty, tag.Name);
            Assert.IsTrue (tag.IsJobType);
            Assert.AreEqual (9, tag.Text.Length);
        }
    }
}
