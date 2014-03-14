using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using Entrada.Editor.Core;
using Entrada.Entities;

namespace Entrada.Editor
{
    public class DownloadedJobsListBox : ListBox
    {
        public Color HighlightColor { get; set; }

        public DownloadedJobsListBox ()
        {
            EditorCore.Jobs.JobAdded += Jobs_JobsChanged;
            EditorCore.Jobs.JobRemoved += Jobs_JobsChanged;

            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 30;
            IntegralHeight = false;
        }

        public MedicalJobEntity SelectedJob {
            get { return (MedicalJobEntity)SelectedItem; }
        }

        private void Jobs_JobsChanged (object sender, JobEventArgs e)
        {
            Items.Clear ();
            Items.AddRange (EditorCore.Jobs.ClaimedJobs.ToArray ());
        }

        protected override void OnDrawItem (DrawItemEventArgs e)
        {
            if (e.Index < 0 || Items.Count == 0)
                return;

            var job = (MedicalJobEntity)Items[e.Index];
            var text_color = Color.Black;

            if (e.State.HasFlag (DrawItemState.Selected))
                using (var b = new SolidBrush (HighlightColor))
                    e.Graphics.FillRectangle (b, e.Bounds);
            else
                e.Graphics.FillRectangle (Brushes.White, e.Bounds);

            if (job.Stat)
                using (var b = new SolidBrush (Color.FromArgb (247, 85, 66)))
                    e.Graphics.FillRectangle (b, e.Bounds.Left, e.Bounds.Top, 3, e.Bounds.Height);

            var line_height = 15;

            var line1 = string.Format ("{0} - {1} {2}", job.JobNumber, job.Encounter.Patient.FirstName, job.Encounter.Patient.LastName);
            var line2 = string.Format ("{0} - {1}", job.Dictations[0].Dictator.DictatorID, job.JobType);
            var line3 = "  " + GetStatusText (job.GetStatus ());

            var line1_bounds = new Rectangle (e.Bounds.Left + 2, e.Bounds.Top + 1, e.Bounds.Width, line_height);
            TextRenderer.DrawText (e.Graphics, line1, Font, line1_bounds, text_color, TextFormatFlags.Top);

            var line2_bounds = new Rectangle (e.Bounds.Left + 2, e.Bounds.Top + line_height + 1, e.Bounds.Width, line_height);
            TextRenderer.DrawText (e.Graphics, line2, Font, line2_bounds, text_color, TextFormatFlags.Top);

            var back_color = e.State.HasFlag (DrawItemState.Selected) ? HighlightColor : Color.White;
            TextRenderer.DrawText (e.Graphics, line3, Font, line2_bounds, text_color, back_color, TextFormatFlags.Top | TextFormatFlags.Right);

            if (e.Index < Items.Count)
                e.Graphics.DrawLine (Pens.LightGray, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
        }

        private string GetStatusText (DownloadedJobStatus status)
        {
            switch (status) {
                case DownloadedJobStatus.InProgress:
                    return "In Progress";
                case DownloadedJobStatus.OnHold:
                    return "On Hold";
                case DownloadedJobStatus.Downloaded:
                    return "Ready";
                case DownloadedJobStatus.Claimed:
                    return "Queued";
                case DownloadedJobStatus.ToQA:
                    return "Needs QA";
                case DownloadedJobStatus.ToCR:
                    return "Needs Customer Review";
                default:
                    return status.ToString ();
            }
        }
    }
}
