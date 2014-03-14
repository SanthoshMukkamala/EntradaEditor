using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entrada.Entities;

namespace Entrada.Editor.Core
{
	public class UnhandledExceptionEventArgs : CancelEventArgs
	{
        public string Text { get; private set; }
        public Exception Exception { get; private set; }
        public bool CanRetry { get; private set; }

        public UnhandledExceptionEventArgs (string text, Exception ex, bool canRetry)
		{
			Text = text;
            Exception = ex;
            CanRetry = canRetry;
		}
	}
}
