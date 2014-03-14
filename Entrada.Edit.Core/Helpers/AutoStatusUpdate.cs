using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
    public sealed class AutoStatusUpdate : IDisposable
    {
        public AutoStatusUpdate (string text)
        {
            EditorCore.Background.ProvideStatusUpdate (text);
        }

        public AutoStatusUpdate (string text, string imageHint)
        {
            EditorCore.Background.ProvideStatusUpdate (text, imageHint);
        }

        public void Dispose ()
        {
            EditorCore.Background.ProvideStatusFinished ();
        }
    }
}
