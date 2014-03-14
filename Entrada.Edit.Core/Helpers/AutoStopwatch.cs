using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
    public sealed class AutoStopwatch : Stopwatch, IDisposable
    {
        private string text;

        public AutoStopwatch (string text)
        {
            this.text = text;
            EditorCore.LogDebug ("->  Starting: {0}", text);
            Start ();
        }

        public void Dispose ()
        {
            Stop ();
            EditorCore.LogDebug ("-> Completed: {0} [Duration: {1}ms]", text.PadRight (20, ' '), ElapsedMilliseconds);
        }
    }
}
