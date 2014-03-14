using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
    public enum DownloadedJobStatus
    {
        Claimed,
        Downloading,
        Downloaded,
        InProgress,
        OnHold,
        ToQA,
        ToCR,
        Completed,
        Sending
    }
}
