using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
    public partial class EnvironmentSelectionDialog : DevExpress.XtraEditors.XtraForm
    {
        public EnvironmentSelectionDialog ()
        {
            InitializeComponent ();

            radioGroup1.EditValue = (int)EditorCore.Settings.EditEnvironment;
        }

        private void btnAccept_Click (object sender, EventArgs e)
        {
            EditorCore.Settings.EditEnvironment = (EditEnvironment)radioGroup1.EditValue;
            EditorCore.Settings.SaveSettings ();

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
