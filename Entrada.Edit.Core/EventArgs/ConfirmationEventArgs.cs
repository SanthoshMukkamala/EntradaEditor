using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entrada.Entities;

namespace Entrada.Editor.Core
{
	public class ConfirmationEventArgs : CancelEventArgs
	{
        public string Text { get; private set; }
        public string Title { get; private set; }

        public ConfirmationEventArgs (string text)
		{
			Text = text;
		}

        public ConfirmationEventArgs (string title, string text) : this (text)
        {
            Title = title;
        }
	}
}
