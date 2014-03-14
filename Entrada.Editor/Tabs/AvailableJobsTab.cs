using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Entrada.Editor.Core;
using Entrada.Editor.Properties;
using Entrada.Entities;

namespace Entrada.Editor
{
    public class AvailableJobsTab : PanelControl
    {
        private ToolStrip strip;
        private GridControl grid;
        private ToolStripItem last_update;
        private ToolStripItem download_button;
        private ToolStripItem refresh_button;

        private bool events_attached;

        public AvailableJobsTab()
        {
            strip = new ToolStrip();
            grid = new GridControl();

            grid.Dock = DockStyle.Fill;
            download_button = strip.Items.Add("Download Selected", Resources.download);
            refresh_button = strip.Items.Add("Refresh", Resources.update);

            refresh_button.Enabled = false;
            download_button.Enabled = false;

            last_update = new ToolStripLabel();
            strip.Items.Add(last_update);
            last_update.Alignment = ToolStripItemAlignment.Right;

            Controls.Add(grid);
            Controls.Add(strip);

            EditorCore.Jobs.AvailableJobsUpdated += Jobs_AvailableJobsUpdated;
            EditorCore.Jobs.RefreshAvailableJobs();

            refresh_button.Click += (o, e) =>
            {
                refresh_button.Enabled = false;
                download_button.Enabled = false;
                EditorCore.Jobs.RefreshAvailableJobs();
            };

            download_button.Click += DownloadButton_Click;
        }

        private void Jobs_AvailableJobsUpdated(object sender, EventArgs e)
        {
            grid.DataSource = EditorCore.Jobs.AvailableJobs.OrderByDescending(p => p.Stat).ThenBy(p => p.TurnaroundTime);
            grid.ForceInitialize();
            grid.RefreshDataSource();

            var gridview = (GridView)grid.DefaultView;

            if (!events_attached)
            {

                // Create a default panel layout so we can reset to default if needed
                gridview.SaveLayoutToXml(EditorCore.Settings.DefaultAvailableJobsGridFile);

                /*if (File.Exists(EditorCore.Settings.AvailableJobsGridFile))
                    gridview.RestoreLayoutFromXml(EditorCore.Settings.AvailableJobsGridFile);*/

                // Only configure the table on first load, not between refreshes                    
                if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "qa")
                {
                    gridview.HideColumns("AppointmentDate", "AppointmentTime", "DictationDate", "JobQAStage");
                }
                else
                {
                    //gridview.Columns[10].Visible = false;
                    gridview.HideColumns("LastQANote", "AppointmentDate", "AppointmentTime", "DictationDate", "JobQAStage");
                }
                gridview.BestFitColumns();

                gridview.OptionsView.ShowAutoFilterRow = true;
                gridview.OptionsView.ShowGroupPanel = false;

                gridview.OptionsBehavior.Editable = false;
                gridview.OptionsSelection.MultiSelect = true;
            }

            download_button.Enabled = EditorCore.Jobs.AvailableJobs.Count > 0;

            if (EditorCore.Jobs.AvailableJobs.Count > 0 && gridview != null)
            {
                if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "qa")
                {
                    var QANote_col = gridview.Columns["LastQANote"];
                    if (QANote_col != null)
                    {
                        QANote_col.Caption = "QA Note";
                        QANote_col.OptionsFilter.AllowFilter = false;
                        QANote_col.OptionsFilter.AllowAutoFilter = false;
                    }
                }

                var tat_col = gridview.Columns["TurnaroundTime"];

                if (tat_col != null)
                {
                    tat_col.Caption = "Due";
                    tat_col.DisplayFormat.FormatString = "g";
                    tat_col.OptionsFilter.AllowFilter = false;
                    tat_col.OptionsFilter.AllowAutoFilter = false;
                }

                var dur_col = gridview.Columns["Duration"];

                if (dur_col != null)
                {
                    dur_col.OptionsFilter.AllowFilter = false;
                    dur_col.OptionsFilter.AllowAutoFilter = false;
                }

                var appt_time_col = gridview.Columns["AppointmentTime"];

                if (appt_time_col != null)
                {
                    appt_time_col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    appt_time_col.DisplayFormat.FormatString = "t";
                }

                // Use a custom icon for the stat column rather than a checkbox,
                // because the checkbox looks like "check these boxes to download"
                var checkEdit = grid.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
                checkEdit.PictureChecked = Resources.flag_orange;
                checkEdit.PictureUnchecked = null;
                checkEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;

                var col = gridview.Columns["Stat"];

                if (col != null)
                    col.ColumnEdit = checkEdit;
            }

            // Can't attach events till we have a grid view, 
            // but don't want more events on every refresh
            if (!events_attached)
            {
                gridview.CustomDrawCell += gridview_CustomDrawCell;
                events_attached = true;
            }

            last_update.Text = "Last refresh: " + EditorCore.Jobs.AvailableJobsLastUpdate.ToLongTimeString();
            refresh_button.Enabled = true;
        }

        private void gridview_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "Duration" && e.CellValue != null)
                e.DisplayText = MiscellaneousExtensions.FormatDuration((int)e.CellValue);
            else if (e.Column.FieldName == "TurnaroundTime" && e.CellValue != null)
                e.DisplayText = MiscellaneousExtensions.FormatTAT((DateTime)e.CellValue);
        }

        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            refresh_button.Enabled = false;
            download_button.Enabled = false;

            var grid_view = (grid.DefaultView as GridView);

            foreach (var row_index in grid_view.GetSelectedRows())
            {
                if (!EditorCore.Jobs.CanDownloadMoreJobs)
                {
                    MessageBox.Show(string.Format("You can only claim {0} jobs at once.", EditorCore.Settings.Editor.MaxJobs));
                    break;
                }

                var job = (AvailableJobEntity)grid_view.GetRow(row_index);

                if (job != null)
                    await EditorCore.Jobs.ClaimJob(job.JobNumber);
            }

            EditorCore.Jobs.RefreshAvailableJobs();
        }

        public void SaveSettings()
        {
            var gridview = (GridView)grid.DefaultView;

            if (gridview != null)
            {
                gridview.ActiveFilter.Clear();
                gridview.SaveLayoutToXml(EditorCore.Settings.AvailableJobsGridFile);
            }
        }

        public void ResetToDefaultSettings()
        {
            var gridview = (GridView)grid.DefaultView;

            if (gridview != null)
                if (File.Exists(EditorCore.Settings.DefaultAvailableJobsGridFile))
                    gridview.RestoreLayoutFromXml(EditorCore.Settings.DefaultAvailableJobsGridFile);
        }

        public void OnClosing()
        {
            EditorCore.Jobs.AvailableJobsUpdated -= Jobs_AvailableJobsUpdated;

            SaveSettings();
        }
    }
}
