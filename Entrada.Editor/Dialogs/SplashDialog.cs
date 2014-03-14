using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using Entrada.Editor.Properties;

namespace Entrada.Editor
{
    public partial class SplashDialog : SplashScreen
    {
        public SplashDialog ()
        {
            InitializeComponent ();
        }

        #region Overrides
        protected override void OnPaint (PaintEventArgs e)
        {
            base.OnPaint (e);

            e.Graphics.DrawImage (Resources.login_logo, 0, 0);

            var rect = new Rectangle (0, 0, Width - 1, Height - 1);
            e.Graphics.DrawRectangle (Pens.LightGray, rect);
        }

        public override void ProcessCommand (Enum cmd, object arg)
        {
            base.ProcessCommand (cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }
    }
}