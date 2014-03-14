using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Entrada.Editor
{
	public partial class StatusBanner : UserControl
	{
		public StatusBanner ()
		{
			InitializeComponent ();

            // Give some feedback when user hovers over the close button
            btnClose.MouseEnter += delegate { btnClose.BackColor = Color.FromArgb (50, 0, 0, 0); };
            btnClose.MouseLeave += delegate { btnClose.BackColor = Color.Transparent; };
        }

		public override string Text {
			get { return label1.Text; }
			set { label1.Text = value; }
		}

        public bool Closable {
            get { return btnClose.Visible; }
            set { btnClose.Visible = value; }
        }

		protected override void OnPaintBackground (PaintEventArgs e)
		{
			base.OnPaintBackground (e);

			e.Graphics.DrawLine (Pens.PaleGoldenrod, 0, Bottom - 1, Right, Bottom - 1);
		}

        private void btnClose_Click (object sender, EventArgs e)
        {
            Hide ();
        }
	}
}
