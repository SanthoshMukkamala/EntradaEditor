using System;
using DevExpress.XtraGrid.Views.Grid;

namespace Entrada.Editor
{
    public static class XtraGridExtensions
	{
        public static void HideColumns (this GridView grid, params string[] columns)
        {
            if (grid == null || grid.Columns == null)
                return;

            foreach (var col in columns) {
                try {
                    var c = grid.Columns[col];

                    if (c != null)
                        c.Visible = false;
                } catch (Exception) { }
            }                
        }
    }
}
