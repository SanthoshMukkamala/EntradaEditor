using DevExpress.XtraBars.Docking2010.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraSpellChecker;
using System.IO;
using System.Globalization;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
	public static class EditorFormExtensions
	{
        public static DevExpress.XtraBars.Docking2010.Views.BaseDocument FindDocument (this EditorForm form, string jobNumber)
        {
            return form.documentManager1.View.Documents.Where (p => (string)p.Tag == jobNumber).FirstOrDefault ();
        }

        public static bool ActivateTab<T> (this DevExpress.XtraBars.Docking2010.DocumentManager manager) where T : Control
        {
            var tab = FindTabDocument<T> (manager);

            if (tab != null) {
                manager.View.Controller.Activate (tab);
                return true;
            }

            return false;
        }

        public static DevExpress.XtraBars.Docking2010.Views.BaseDocument CreateTab (this EditorForm form, Control control, string caption, Image image = null, object tag = null, bool allowClose = true)
        {
            var view_doc = form.documentManager1.View.Controller.AddDocument (control);
            view_doc.Caption = caption;

            view_doc.Image = image;
            view_doc.Tag = tag;
            view_doc.Properties.AllowClose = allowClose.ToDefaultBool ();
            form.documentManager1.View.Controller.Activate (view_doc);

            return view_doc;
        }

        public static T FindTab<T> (this DevExpress.XtraBars.Docking2010.DocumentManager manager) where T : Control
        {
            if (manager == null || manager.View == null || manager.View.Documents == null)
                return null;

            return manager.View.Documents.Where (p => p.Control is T).Select (p => (T)p.Control).FirstOrDefault ();
        }

        public static DevExpress.XtraBars.Docking2010.Views.BaseDocument FindTabDocument<T> (this DevExpress.XtraBars.Docking2010.DocumentManager manager) where T : Control
        {
            return manager.View.Documents.Where (p => p.Control is T).FirstOrDefault ();
        }
        
        public static EditorTab GetActiveRichEditPanel(this EditorForm form)
        {
            var doc = form.documentManager1.View.ActiveDocument;

            if (doc == null)
                return null;

            var control = doc.Control;

            if (control == null || !(control is EditorTab))
                return null;

            return control as EditorTab;
        }

        public static List<ISpellCheckerDictionary> CreateDictionaries (this EditorForm form)
        {
            var dicts = new List<ISpellCheckerDictionary> ();

            // Set up our spell checker dictionaries
            var dictionary = new SpellCheckerISpellDictionary ();
            dictionary.AlphabetPath = Path.Combine (EditorCore.Settings.DictionaryDirectory, "EnglishAlphabet.txt");
            dictionary.Culture = new CultureInfo ("en-US");
            dictionary.DictionaryPath = Path.Combine (EditorCore.Settings.DictionaryDirectory, "american.xlg");
            dictionary.GrammarPath = Path.Combine (EditorCore.Settings.DictionaryDirectory, "english.aff");

            dicts.Add (dictionary);

            var c1_dictionary = new SpellCheckerDictionary ();
            c1_dictionary.AlphabetPath = Path.Combine (EditorCore.Settings.DictionaryDirectory, "EnglishAlphabet.txt");
            c1_dictionary.Culture = new CultureInfo ("en-US");
            c1_dictionary.DictionaryPath = Path.Combine (EditorCore.Settings.DictionaryDirectory, "c1-english.txt");

            dicts.Add (c1_dictionary);

            var med_dictionary = new SpellCheckerDictionary ();
            med_dictionary.AlphabetPath = Path.Combine (EditorCore.Settings.DictionaryDirectory, "EnglishAlphabet.txt");
            med_dictionary.Culture = new CultureInfo ("en-US");
            med_dictionary.DictionaryPath = Path.Combine (EditorCore.Settings.DictionaryDirectory, "medical.txt");

            dicts.Add (med_dictionary);

            var custom_dictionary = new SpellCheckerCustomDictionary ();
            custom_dictionary.Culture = new CultureInfo ("en-US");
            custom_dictionary.AlphabetPath = Path.Combine (EditorCore.Settings.DictionaryDirectory, "EnglishAlphabet.txt");
            custom_dictionary.DictionaryPath = Path.Combine (EditorCore.Settings.UserDataDirectory, "CustomEnglish.dic");

            dicts.Add (custom_dictionary);

            return dicts;
        }
    }
}
