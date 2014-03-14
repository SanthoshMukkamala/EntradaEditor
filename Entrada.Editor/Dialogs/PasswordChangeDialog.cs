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
    public partial class PasswordChangeDialog : DevExpress.XtraEditors.XtraForm
    {
        public PasswordChangeDialog (bool cancelable)
        {
            InitializeComponent ();

            btnCancel.Enabled = cancelable;

            txtCurrent.GotFocus += (o, e) => { txtCurrent.SelectAll (); };
            txtNew.GotFocus += (o, e) => { txtNew.SelectAll (); };
            txtConfirm.GotFocus += (o, e) => { txtConfirm.SelectAll (); };
        }

        private void PasswordChangeDialog_Load (object sender, EventArgs e)
        {
            txtCurrent.Select ();
        }

        private async void btnChange_Click (object sender, EventArgs e)
        {
            SetError (null);

            if (string.IsNullOrWhiteSpace (txtCurrent.Text)) {
                SetError ("All fields must be filled in");
                return;
            }

            if (string.IsNullOrWhiteSpace (txtNew.Text)) {
                SetError ("All fields must be filled in");
                return;
            }

            if (string.IsNullOrWhiteSpace (txtConfirm.Text)) {
                SetError ("All fields must be filled in");
                return;
            }

            if (!EditorCore.Editor.IsPasswordStrongEnough (txtNew.Text)) {
                SetError ("Password not strong enough");
                return;
            }

            if (txtNew.Text != txtConfirm.Text) {
                SetError ("Password confirmation doesn't match");
                return;
            }

            if (txtNew.Text == txtCurrent.Text) {
                SetError ("Password must be different from current password");
                return;
            }

            // Attempt to change password
            try {
                await EditorCore.Editor.ChangePassword (txtCurrent.Text, txtNew.Text);
                DialogResult = System.Windows.Forms.DialogResult.OK;
                return;
            } catch (Exception ex) {
                EditorCore.LogException ("Error changing password.", ex);

                if (ex.Message.Contains ("The strength of the supplied password")) {
                    SetError ("Password not strong enough");
                    return;
                }

                if (ex.Message.Contains ("The supplied password does not match")) {
                    SetError ("Incorrect current password");
                    return;
                }

                throw;
            }
        }

        private void SetError (string error)
        {
            lblError.Text = error;
            lblError.Visible = !string.IsNullOrWhiteSpace (error);
        }
    }
}
