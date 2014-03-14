using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Entrada.Editor.Core;
using DevExpress.XtraGrid.Views.Grid;

namespace Entrada.Editor
{
    public partial class AutoCorrectOptionsTab : UserControl
    {
        private BindingList<AutoCorrectItem> list;
        private bool modified;

        public event EventHandler ModifiedChanged;

        public AutoCorrectOptionsTab ()
        {
            InitializeComponent ();
        }

        private void PreferencesTab_Load (object sender, EventArgs e)
        {
            chkTwoCaps.Checked = EditorCore.Settings.AutoCorrectTwoCaps;
            chkSpellCheck.Checked = EditorCore.Settings.AutoCorrectSpellCheck;

            list = new BindingList<AutoCorrectItem> (EditorCore.Settings.AutoCorrectTextList.Select (p => new AutoCorrectItem () { Replace = p.Key, With = p.Value }).ToList ());
            list.ListChanged += (o, _) => { Modified = true; };

            gridControl1.DataSource = list;
            gridControl1.DefaultView.ValidatingEditor += (o, _) => { Modified = true; };
        }

        public bool Modified {
            get { return modified; }
            set {
                modified = value;

                if (ModifiedChanged != null)
                    ModifiedChanged (null, EventArgs.Empty);
            }
        }

        private void btnSave_Click (object sender, EventArgs e)
        {
            EditorCore.Settings.AutoCorrectTwoCaps = chkTwoCaps.Checked;
            EditorCore.Settings.AutoCorrectSpellCheck = chkSpellCheck.Checked;

            EditorCore.Settings.AutoCorrectTextList.Clear ();
            
            foreach (var ac in list)
                if (!string.IsNullOrWhiteSpace (ac.Replace))
                    EditorCore.Settings.AutoCorrectTextList[ac.Replace] = ac.With;

            EditorCore.Settings.SaveAutoCorrectList ();
            Modified = false;
        }

        private void btnDelete_Click (object sender, EventArgs e)
        {
            var grid_view = (gridControl1.DefaultView as GridView);

            foreach (var row_index in grid_view.GetSelectedRows ()) {
                var ac = (AutoCorrectItem)grid_view.GetRow (row_index);

                list.Remove (ac);
            }
        }

        private void chkTwoCaps_CheckedChanged (object sender, EventArgs e)
        {
            Modified = true;
        }

        private void chkSpellCheck_CheckedChanged (object sender, EventArgs e)
        {
            Modified = true;
        }

        private class AutoCorrectItem
        {
            public string Replace { get; set; }
            public string With { get; set; }
        }
    }
}
