using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Entrada.Editor
{
    public partial class IdleWarningDialog : DevExpress.XtraEditors.XtraForm
    {
        private DateTime start_time;

        public IdleWarningDialog ()
        {
            InitializeComponent ();

            start_time = DateTime.Now;
            timer1.Tick += timer1_Tick;
        }

        private void timer1_Tick (object sender, EventArgs e)
        {
            timer1.Stop ();
            var expire_time = start_time.AddMinutes (5);

            if (DateTime.Now >= expire_time) {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                return;
            }

            var time_left = expire_time.Subtract (DateTime.Now);

            labelControl2.Text = time_left.ToString (@"mm\:ss");
            timer1.Start ();
        }

        private void IdleWarningDialog_Load (object sender, EventArgs e)
        {
            timer1.Start ();
        }
    }
}
