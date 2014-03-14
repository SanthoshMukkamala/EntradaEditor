using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entrada.Editor.Core;
using Entrada.Entities;

namespace Entrada.Editor
{
    public static class EntityExtensions
    {
        private static Dictionary<string, DownloadedJobStatus> downloaded_job_status;

        static EntityExtensions ()
        {
            downloaded_job_status = new Dictionary<string,DownloadedJobStatus> ();
        }

        public static DownloadedJobStatus GetStatus (this MedicalJobEntity job)
        {
            if (downloaded_job_status.ContainsKey (job.JobNumber))
                return downloaded_job_status[job.JobNumber];

            return DownloadedJobStatus.Claimed;
        }

        public static void SetStatus (this MedicalJobEntity job, DownloadedJobStatus status)
        {
            downloaded_job_status[job.JobNumber] = status;
        }
    }
}
