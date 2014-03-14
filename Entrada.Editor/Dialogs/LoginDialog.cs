using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using Entrada.Editor.Core;
using Entrada.Editor.Data;

namespace Entrada.Editor
{
    public partial class LoginDialog : Form
    {
        private bool username_error;
        private bool password_error;

        public bool MustChangePassword { get; private set; }

        public LoginDialog ()
        {
            InitializeComponent ();

            // Give some feedback when user hovers over the close button
            btnClose.MouseEnter += delegate { btnClose.BackColor = Color.FromArgb (50, 0, 0, 0); };
            btnClose.MouseLeave += delegate { btnClose.BackColor = Color.Transparent; };

            txtUser.GotFocus += (o, e) => { txtUser.SelectAll (); };
            txtPassword.GotFocus += (o, e) => { txtPassword.SelectAll (); };

            Shown += (o, e) => { txtUser.Focus (); };

            var version = Assembly.GetEntryAssembly ().GetName ().Version;
            lblVersion.Text = string.Format ("Version: {0}", version);

            UpdateEnvironmentLabel ();
        }

        protected override void OnPaintBackground (PaintEventArgs e)
        {
            base.OnPaintBackground (e);

            var rect = new Rectangle (0, 0, Width - 1, Height - 1);
            e.Graphics.DrawRectangle (Pens.LightGray, rect);

            if (txtUser.Visible) {
                var user_border = new Rectangle (txtUser.Left - 1, txtUser.Top - 1, txtUser.Width + 1, txtUser.Height + 1);
                e.Graphics.DrawRectangle (username_error ? Pens.IndianRed : Pens.DarkGray, user_border);
            }

            if (txtPassword.Visible) {
                var pass_border = new Rectangle (txtPassword.Left - 1, txtPassword.Top - 1, txtPassword.Width + 1, txtPassword.Height + 1);
                e.Graphics.DrawRectangle (password_error ? Pens.IndianRed : Pens.DarkGray, pass_border);
            }
        }

        protected override void OnKeyDown (KeyEventArgs e)
        {
            // Allow Escape to dismiss the dialog
            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.Cancel;
            else if (e.KeyCode == Keys.F12 && e.Modifiers == Keys.Control)
                using (var dialog = new EnvironmentSelectionDialog ())
                    if (dialog.ShowDialog () == System.Windows.Forms.DialogResult.OK)
                        UpdateEnvironmentLabel ();
            else
                base.OnKeyDown (e);
        }

        private async void btnLogin_Click (object sender, EventArgs e)
        {
            UpdateLoginErrorUI (false);

            if (string.IsNullOrWhiteSpace (txtUser.Text) || string.IsNullOrWhiteSpace (txtPassword.Text)) {
                if (string.IsNullOrWhiteSpace (txtUser.Text))
                    username_error = true;

                if (string.IsNullOrWhiteSpace (txtPassword.Text))
                    password_error = true;

                Invalidate ();
                return;
            }

            UpdateUI (false);

            Entrada.Editor.Data.Settings.SetEnvironment ((int)EditorCore.Settings.EditEnvironment);

            using (EditorCore.CreateStopwatch ("Log In - {0}", EditorCore.Settings.EditEnvironment)) {
                while (true) {
                    try {
                        Invalidate ();
                        var editor = await EditorRepository.LogIn (txtUser.Text.Trim (), txtPassword.Text.Trim ());

                        EditorCore.Settings.Editor = editor;
                        MustChangePassword = Settings.MustChangePassword;

                        /*if (editor.Type.ToLowerInvariant () != "editor") {
                            MessageBox.Show ("At this time, this application can only be used by editors.  Please log on with an editor account.");
                            UpdateUI (true);
                            break;
                        }*/

                        await EditorCore.Editor.StartWork ();

                        DialogResult = System.Windows.Forms.DialogResult.OK;
                        return;
                    } catch (LoginFailureException) {
                        UpdateUI (true);
                        UpdateLoginErrorUI (true);

                        break;
                    } catch (Exception ex) {
                        if (!EditorCore.Background.RetryGettingDataException ("An error has occured trying to log in.", ex)) {
                            UpdateUI (true);
                            break;
                        }
                    }
                }
            }

            Invalidate ();
        }

        private void UpdateLoginErrorUI (bool error)
        {
            username_error = error;
            password_error = error;
            lblError.Visible = error;
        }

        private void UpdateUI (bool activate)
        {
            lblUser.Visible = activate;
            lblPassword.Visible = activate;
            txtUser.Visible = activate;
            txtPassword.Visible = activate;
            btnLogin.Enabled = activate;

            lblStatus.Visible = !activate;
            marqueeProgressBarControl1.Visible = !activate;
        }

        private void UpdateEnvironmentLabel ()
        {
            var env = EditorCore.Settings.EditEnvironment;

            lblEnvironment.Text = string.Format ("Connected to the {0} environment", env.ToString ().ToUpperInvariant ());
            lblEnvironment.Visible = env != EditEnvironment.Production;
        }

        private void btnClose_Click (object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}