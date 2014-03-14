using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
    public sealed class IdleTimerWatcher : IMessageFilter, IDisposable
    {
        // constants from WinUser.h (google is great!)
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_MOUSEWHEEL = 0x20A;
        private const int WM_KEYDOWN = 0x100;

        private int Timeout { get; set; }
        private int LastActivityTicks { get; set; }

        private Timer timer;

        public IdleTimerWatcher (int timeOutMinutes)
        {
            Timeout = timeOutMinutes;
            LastActivityTicks = Environment.TickCount;

            if (timeOutMinutes < 10) {
                EditorCore.LogDebug ("Timeout set to less than 10 minutes, disabling. ({0})", timeOutMinutes);
                return;
            }

            EditorCore.LogDebug ("Setting up IdleTimeWatcher for {0} minutes.", timeOutMinutes);

            timer = new Timer ();
            timer.Interval = 30 * 1000;     // Check if we're idle every 30 seconds
            timer.Tick += timer_Tick;
            timer.Start ();
        }

        private void timer_Tick (object sender, EventArgs e)
        {
            timer.Stop ();

            var idle_ticks = Environment.TickCount - LastActivityTicks;
            var idle_time = new TimeSpan (0, 0, 0, 0, idle_ticks);

            // We want to give the user a 5 minute warning before we release their jobs
            if (idle_time.TotalMinutes > (Timeout - 5)) {
                EditorCore.Background.RaiseIdleWarning ();
                LastActivityTicks = Environment.TickCount;
            }

            timer.Start ();
        }

        public bool PreFilterMessage (ref Message m)
        {
            switch (m.Msg) {
                //case WM_MOUSEMOVE:
                case WM_LBUTTONDOWN:
                case WM_RBUTTONDOWN:
                case WM_MBUTTONDOWN:
                case WM_MOUSEWHEEL:
                case WM_KEYDOWN:
                    LastActivityTicks = Environment.TickCount;
                    break;
            }

            return false;
        }

        #region IDisposable Members
        public void Dispose ()
        {
            if (timer != null)
                timer.Dispose ();
        }
        #endregion
    }
}
