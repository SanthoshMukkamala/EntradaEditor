using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrada.Editor.Core
{
	public class DocumentEventArgs : EventArgs
	{
		public DocumentEntity Document { get; private set; }

		public DocumentEventArgs (DocumentEntity doc)
		{
			Document = doc;
		}
	}
}
