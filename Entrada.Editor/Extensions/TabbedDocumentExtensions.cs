using DevExpress.XtraBars.Docking2010.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;

namespace Entrada.Editor
{
	public static class TabbedDocumentExtensions
	{
		public static EditorTab GetPanel (this BaseDocument doc)
		{
			return doc.Control as EditorTab;
		}

        public static bool IsAvailableJobsTab (this BaseDocument doc)
        {
            return doc.Control is AvailableJobsTab;
        }

        public static bool IsEditorTab (this BaseDocument doc)
        {
            return doc.Control is EditorTab;
        }

        public static AvailableJobsTab GetAvailableJobsTab (this BaseDocument doc)
        {
            return doc.Control as AvailableJobsTab;
        }

        public static EditorTab GetEditorTab (this BaseDocument doc)
        {
            return doc.Control as EditorTab;
        }

        public static void SetVisible (this BarItem item, bool visible)
        {
            item.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
	}
}
