using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entrada.Editor.Core;
using Entrada.Entities;
using Entrada.WebServices.Client;
using Ionic.Zip;
using Entrada.Editor;

namespace Entrada.Editor.Data
{
    public class JobRepository
    {
        public static Task<MedicalJobEntity> ClaimJob(string jobNumber)
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                var job = ws.ClaimJob(jobNumber);

                return job;
            });
        }

        public static Task<MedicalJobEntity> GetJob(string jobNumber)
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                var job = ws.GetJob(jobNumber);

                return job;
            });
        }

        public static Task DownloadJob(string jobNumber, string path, string Type)
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                // Download and verify zip file
                var job = ws.DownloadJobFile(jobNumber);
                job.VerifyIntegrity();

                // If the directory already exists, delete it
                if (Directory.Exists(Path.Combine(path, jobNumber)))
                    Directory.Delete(Path.Combine(path, jobNumber), true);

                // Write zip file contents to disk
                var filename = string.Format("{0}.zip", jobNumber);
                Directory.CreateDirectory(Path.Combine(path, jobNumber));

                // The zip is unencrypted, so we're going to keep it in memory
                // rather than write it to disk.
                using (var ms = new MemoryStream(job.Content))
                {
                    var zip = ZipFile.Read(ms);

#if DEBUG_DOCUMENTS
                    File.WriteAllBytes (@"C:\temp\job.zip", ms.ToArray ());
#endif


                    foreach (var file in zip)
                        if (".xml|.txt|.ogg".Contains(Path.GetExtension(file.FileName).ToLowerInvariant()) || (Path.GetExtension(file.FileName).ToLowerInvariant() == ".doc" && jobNumber == Path.GetFileNameWithoutExtension(file.FileName)))
                        {
                            // Files we want encrypted
                            using (var stream = EncryptedFileSystem.GetOutputStream(Path.Combine(path, jobNumber, file.FileName)))
                                file.Extract(stream);
                        }
                        else
                        {
                            // Non-encrypted files (templates)
                            file.Extract(Path.Combine(path, jobNumber));
                        }

                }
            });
        }

        public static Task FinishJob(ReturnJobTask job)
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                ws.ReturnCompleteJob(job);
            });
        }

        public static Task<MedicalJobEntity[]> GetAssignedJobs()
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                var listJobs = ws.GetEditorAssignedJobs();
                return listJobs;
            });
        }

        public static Task<MedicalJobEntity[]> GetAvailableJobs()
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                var listJobs = ws.GetAvailableJobs(DateTime.MinValue, DateTime.MinValue, "", "");
                return listJobs;
            });
        }

        public static Task<string> GetAvailableJobsJson()
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                var listJobs = ws.GetAvailableJobsJSON(string.Empty, string.Empty);
                return listJobs;
            });
        }

        public static Task<MedicalJobEntity> GetNextAvailableJob()
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                var job = ws.ClaimNextJob();
                return job;
            });
        }

        public static Task ReleaseJob(string jobNumber)
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();
                ws.ReleaseJob(jobNumber);
            });
        }

        //public static Task SendJobToCR (JobEntity job)
        //{
        //    return Task.Factory.StartNew (() => {
        //        using (var ws = Settings.CreateEditService ()) {
        //            ws.SignIn (Settings.LoginToken);

        //            ws.ReturnIncompleteJobToCR (null);
        //        }
        //    });
        //}

        public static Task SendJobToQA(ReturnJobTask job)
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                ws.ReturnIncompleteJob(job, NextQAStatus.ReturnJobToNextQA);
            });
        }

        public static Task<MedicalJobEntity> SplitJobByJobType(string jobNumber, string jobType, Entrada.Entities.PatientEntity patient, string text)
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                var split = new Entrada.Entities.SplitJobTaskEntity();
                split.JobType = jobType;
                split.JobNumber = jobNumber;
                split.Patient = patient;
                split.SplitMode = 'T';
                split.RecognizedText = text;

                var job = ws.SplitJob(split);
                return job;
            });
        }

        public static Task<MedicalJobEntity> SplitJobByPatient(string jobNumber, Entrada.Entities.PatientEntity patient, string text)
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                var split = new Entrada.Entities.SplitJobTaskEntity();
                split.JobNumber = jobNumber;
                split.Patient = patient;
                split.SplitMode = 'P';
                split.RecognizedText = text;

                var job = ws.SplitJob(split);
                return job;
            });
        }

        public static Task<QACategory[]> GetAllQACategories()
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();
                var listCategory = ws.GetChildQACategories(QACategory.Empty);
                return listCategory;
            });
        }

        public static Task<QACategory[]> GetSubQACategories(int iParentID)
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();
                QACategory objCategory = new QACategory
                {
                    Id = iParentID,
                };
                var listSubCategory = ws.GetChildQACategories(objCategory);
                return listSubCategory;
            });
        }


        public static Task SendJobToNextQA(ReturnJobTask job, NextQAStatus NextQAJobStatus)
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();

                ws.ReturnIncompleteJob(job, NextQAJobStatus);

            });
        }
    }
}
