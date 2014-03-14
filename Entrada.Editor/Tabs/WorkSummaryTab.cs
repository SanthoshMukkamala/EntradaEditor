using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Entrada.Editor.Core;
using Entrada.Editor.Properties;
using Entrada.Entities;

namespace Entrada.Editor
{
    public class WorkSummaryTab : PanelControl
    {
        private ToolStrip strip;
        private GridControl grid;
        private ToolStripItem last_update;

        public WorkSummaryTab ()
        {
            strip = new ToolStrip ();
            grid = new GridControl ();

            grid.Dock = DockStyle.Fill;
            var refresh = strip.Items.Add ("Refresh", Resources.update);

            last_update = new ToolStripLabel ();
            strip.Items.Add (last_update);
            last_update.Alignment = ToolStripItemAlignment.Right;

            Controls.Add (grid);
            Controls.Add (strip);

            EditorCore.Jobs.WorkSummaryUpdated += Jobs_WorkSummaryUpdated;
            EditorCore.Jobs.RefreshWorkSummary ();

            refresh.Click += (o, e) => { EditorCore.Jobs.RefreshWorkSummary (); };
        }

        private void Jobs_WorkSummaryUpdated (object sender, EventArgs e)
        {
            grid.DataSource = EditorCore.Jobs.WorkSummary.OrderByDescending (p => p.Date);
            grid.ForceInitialize ();
            grid.RefreshDataSource ();

            var gridview = (GridView)grid.DefaultView;

            if (gridview != null) {
                gridview.OptionsView.ShowAutoFilterRow = true;
                gridview.OptionsView.ShowGroupPanel = false;

                gridview.OptionsBehavior.Editable = false;
                gridview.OptionsSelection.MultiSelect = true;

                var linecount_col = gridview.Columns["LineCount"];

                if (linecount_col != null) {
                    linecount_col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    linecount_col.DisplayFormat.FormatString = "{0:0.00}";
                }

                gridview.HideColumns ("ExtensionData");
                gridview.BestFitColumns ();
            }

            last_update.Text = "Last refresh: " + EditorCore.Jobs.WorkSummaryLastUpdate.ToLongTimeString ();
        }

        public void SaveSettings ()
        {
            //var layout_file = Path.Combine (EditorCore.Settings.UserDataDirectory, "manual_jobs.xml");
            //var gridview = (GridView)grid.DefaultView;

            //if (gridview != null)
            //    gridview.SaveLayoutToXml (layout_file);
        }

        public void OnClosing ()
        {
            EditorCore.Jobs.WorkSummaryUpdated -= Jobs_WorkSummaryUpdated;

            SaveSettings ();
        }
    }
}
