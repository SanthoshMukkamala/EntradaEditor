using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
    public partial class MacroDialog : DevExpress.XtraEditors.XtraForm
    {
        private DocumentEntity document;        
        
        public MacroDialog (DocumentEntity document) : this ()
        {
            this.document = document;

            Shown += MacroDialog_Shown;

            richEditControlMacro.ReadOnly = true;
        }

        public MacroDialog ()
        {
            InitializeComponent ();
        }

        public string SelectedMacroText {
            get {
                if (listBoxControl1.SelectedIndex < 0)
                    return string.Empty;

                return document.Macros[(string)listBoxControl1.SelectedItem];
            }
        }

        public string SelectedMacroName {
            get {
                if (listBoxControl1.SelectedIndex < 0)
                    return string.Empty;

                return (string)listBoxControl1.SelectedItem;
            }
        }

        private void MacroDialog_Shown (object sender, EventArgs e)
        {
            foreach (var macro in document.Macros)
                listBoxControl1.Items.Add (macro.Key);

            listBoxControl1.Focus ();
        }

        private void listBoxControl1_SelectedIndexChanged (object sender, EventArgs e)
        {
            if (listBoxControl1.SelectedIndex < 0)
            {
                //labelControl2.Text = string.Empty;
                richEditControlMacro.Text = string.Empty;
                return;
            }
            else
            {
                richEditControlMacro.Text = string.Empty;
            }

            var text = document.Macros[(string)listBoxControl1.SelectedItem];
            var html = string.Format ("<html><head></head><body>{0}</body></html>", text);

            //labelControl2.Xml = html;
            richEditControlMacro.Document.InsertHtmlText(richEditControlMacro.Document.CreatePosition(0),html);
            listBoxControl1.Focus ();
        }
    }
}