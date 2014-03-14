using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
    public class BackgroundStatusEventArgs : EventArgs
    {
        public string ImageHint { get; set; }
        public string Text { get; set; }
        public int Progress { get; set; }
    }
}
