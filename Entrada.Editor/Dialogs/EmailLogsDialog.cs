using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
    public partial class EmailLogsDialog : DevExpress.XtraEditors.XtraForm
    {
        public List<LogFile> LogList { get; private set; }
        public Func<string, Task> Option1Action { get; set; }
        public Func<string, Task> Option2Action { get; set; }

        public EmailLogsDialog ()
        {
            InitializeComponent ();

            LogList = new List<LogFile> ();
            labelControl1.Text = "Choose the logs you would like to send to support.";
        }

        private void EmailLogsDialog_Load (object sender, EventArgs e)
        {
            var logs = Directory.EnumerateFiles (EditorCore.Settings.LogsDirectory, "*.txt", SearchOption.TopDirectoryOnly);
            var log_objects = logs.Select (p => new LogFile (p)).OrderByDescending (p => p.RawLogDate);

            LogList.AddRange (log_objects);

            gridControl1.DataSource = LogList;
        }

        public string LabelText {
            get { return labelControl1.Text; }
            set { labelControl1.Text = value; }
        }

        public string Option1Text {
            get { return gridColumn2.Caption; }
            set { gridColumn2.Caption = value; }
        }

        public bool AllowCancel {
            get { return btnCancel.Enabled; }
            set { btnCancel.Enabled = btnCancel.Visible = value; }
        }

        public string ContinueButtonText {
            get { return btnAccept.Text; }
            set { btnAccept.Text = value; }
        }

        public class LogFile
        {
            public DateTime RawLogDate { get; set; }
            public string LogFileName { get; set; }
            public bool Send { get; set; }

            public LogFile (string file)
            {
                LogFileName = file;

                DateTime date;

                if (DateTime.TryParse (Path.GetFileNameWithoutExtension (file), out date))
                    RawLogDate = date;
            }

            public string LogDate {
                get {
                    if (RawLogDate == DateTime.MinValue)
                        return LogFileName;

                    if (RawLogDate.Date == DateTime.Now.Date)
                        return "Today";

                    if (RawLogDate.Date == DateTime.Now.Subtract (new TimeSpan (1, 0, 0, 0)).Date)
                        return "Yesterday";

                    return RawLogDate.ToShortDateString ();
                }
            }
        }

        private void btnAccept_Click (object sender, EventArgs e)
        {
            var logs = LogList.Where (p => p.Send);

            if (logs.Count () == 0) {
                MessageBox.Show ("No logs selected.");
                return;
            }

            var msg = new MailMessage ();

            try {
                EditorCore.LogDebug ("Sending logs to support..");
                var loginInfo = new NetworkCredential ("sendlogs@mail.entradaedit.com", "3ntradA!");

                msg.From = new MailAddress ("sendlogs@mail.entradaedit.com");
                msg.To.Add (new MailAddress ("editorlogs@entradahealth.com"));
                msg.Subject = String.Format ("EditOne logs from <{0}>", EditorCore.Settings.Editor.EditorID);

                foreach (var log in logs)
                    msg.Attachments.Add (new Attachment (log.LogFileName));

                msg.Body = "Logs attached";
                msg.IsBodyHtml = true;

                var client = new SmtpClient ("mail.entradaedit.com", 587);

                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = loginInfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send (msg);

                MessageBox.Show ("Logs succesfully sent!!", "Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                EditorCore.LogException ("Error sending logs to support:\n{0}", ex);
                MessageBox.Show ("Error sending logs", "Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                if (msg != null)
                    msg.Attachments.Dispose ();
            }

            EditorCore.LogDebug ("Logs successfully sent to support..");
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}