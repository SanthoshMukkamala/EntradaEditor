using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Entrada.Editor.Data;
using Entrada.Entities;
using Ionic.Zip;
using Newtonsoft.Json;

namespace Entrada.Editor.Core
{
    public class JobManager
    {
        public List<AvailableJobEntity> AvailableJobs { get; set; }
        public List<MedicalJobEntity> ClaimedJobs { get; set; }
        public List<CalculatedWorkSummaryEntity> WorkSummary { get; set; }

        public DateTime AvailableJobsLastUpdate { get; private set; }
        public DateTime WorkSummaryLastUpdate { get; private set; }
        public bool WorkQueueRunning { get; private set; }

        public event EventHandler<JobEventArgs> JobAdded;
        public event EventHandler<JobEventArgs> JobRemoved;
        public event EventHandler ClaimedJobsUpdated;
        public event EventHandler AvailableJobsUpdated;
        public event EventHandler WorkSummaryUpdated;
        public event EventHandler WorkQueueRunningChanged;
        public event EventHandler<CloseSoundEventArgs> CloseSoundRequested;

        public JobManager ()
        {
            WorkQueueRunning = false;
            ClaimedJobs = new List<MedicalJobEntity> ();
        }

        public int QueuedJobs { get { return ClaimedJobs.Where (j => j.GetStatus () == DownloadedJobStatus.Claimed).Count (); } }
        public int DownloadingJobs { get { return ClaimedJobs.Where (j => j.GetStatus () == DownloadedJobStatus.Downloading).Count (); } }
        public int DownloadedJobs { get { return ClaimedJobs.Where (j => j.GetStatus () == DownloadedJobStatus.Downloaded).Count (); } }
        public bool CanDownloadMoreJobs { get { return ClaimedJobs.Count < EditorCore.Settings.Editor.MaxJobs; } }
        public int ReturnableJobs { get { return ClaimedJobs.Where (j => j.GetStatus () == DownloadedJobStatus.Completed || j.GetStatus () == DownloadedJobStatus.ToQA).Count (); } }

        public void OpenNextJob ()
        {
            if (EditorCore.Documents.HasOpenDocuments)
                return;

            foreach (var job in ClaimedJobs.Where (j => j.GetStatus () == DownloadedJobStatus.Downloaded).OrderByDescending (p => p.Stat)) {
                if (!EditorCore.Documents.IsDocumentOpen (job.JobNumber)) {
                    EditorCore.Documents.OpenDocument (job);
                    break;
                }
            }
        }

        public void AddJob (MedicalJobEntity job)
        {
            ClaimedJobs.Add (job);

            if (JobAdded != null)
                JobAdded (null, new JobEventArgs (job));

            if (ClaimedJobsUpdated != null)
                ClaimedJobsUpdated (null, EventArgs.Empty);
        }

        public void RemoveJob (string jobnumber)
        {
            var job = ClaimedJobs.Where (p => p.JobNumber == jobnumber).FirstOrDefault ();

            if (job != null)
                RemoveJob (job);
        }

        public void RemoveJob (MedicalJobEntity job)
        {
            ClaimedJobs.Remove (job);

            if (JobRemoved != null)
                JobRemoved (null, new JobEventArgs (job));

            if (ClaimedJobsUpdated != null)
                ClaimedJobsUpdated (null, EventArgs.Empty);
        }

        public async void RefreshAvailableJobs ()
        {
            using (EditorCore.CreateStatusUpdate ("Refreshing available jobs..", "refresh")) {
                using (EditorCore.CreateStopwatch ("RefreshAvailableJobs"))
                    while (true) {
                        try {
                            var json = await JobRepository.GetAvailableJobsJson ();

                            AvailableJobs = await JsonConvert.DeserializeObjectAsync<List<AvailableJobEntity>> (json);
                            AvailableJobs.ForEach (p => p.ReceivedOn = p.ReceivedOn.ToLocalTime ());

                            AvailableJobsLastUpdate = DateTime.Now;

                            break;
                        } catch (Exception ex) {
                            if (!EditorCore.Background.RetryGettingDataException ("An error has occured getting available jobs from the server.", ex))
                                return;
                        }
                    }
            }

            if (AvailableJobsUpdated != null)
                AvailableJobsUpdated (null, EventArgs.Empty);
        }

        public async void RefreshWorkSummary ()
        {
            using (EditorCore.CreateStatusUpdate ("Refreshing work summary..", "refresh")) {
                using (EditorCore.CreateStopwatch ("RefreshWorkSummary"))
                    while (true) {
                        try {
                            WorkSummary = new List<CalculatedWorkSummaryEntity> (await EditorRepository.GetEditorWorkSummary ());
                            WorkSummaryLastUpdate = DateTime.Now;
                            break;
                        } catch (Exception ex) {
                            if (!EditorCore.Background.RetryGettingDataException ("An error has occured getting work summary data from the server.", ex))
                                return;
                        }
                    }
            }

            if (WorkSummaryUpdated != null)
                WorkSummaryUpdated (null, EventArgs.Empty);
        }

        public async Task<List<MedicalJobEntity>> GetAssignedJobs ()
        {
            using (EditorCore.CreateStatusUpdate ("Getting assigned jobs..", "download"))
            using (EditorCore.CreateStopwatch ("GetAssignedJobs")) {
                while (true) {
                    try {
                        var jobs = (await JobRepository.GetAssignedJobs ()).ToList ();
                        return jobs;
                    } catch (Exception ex) {
                        if (!EditorCore.Background.RetryGettingDataException ("An error has occured getting assigned jobs from the server. You will not be able to continue until this data is available.", ex))
                            return null;
                    }
                }
            }
        }

        public async Task<string[]> GetAvailableJobTypes (int clinicID)
        {
            using (EditorCore.CreateStopwatch ("GetAvailableJobTypes")) {
                while (true) {
                    try {
                        var jobs = (await AdtRepository.GetAvailableJobTypes (clinicID));
                        return jobs;
                     } catch (Exception ex) {
                        if (!EditorCore.Background.RetryGettingDataException ("An error has occured getting available job types from the server. Without this data, you will not be able to change the job type or split by job type.", ex))
                            return null;
                    }
                }
            }
        }

        public async Task<MedicalJobEntity> ClaimJob (string jobnumber)
        {
            using (EditorCore.CreateStatusUpdate (string.Format ("Claiming job {0}..", jobnumber), "claim"))
                using (EditorCore.CreateStopwatch ("Claiming Job {0}", jobnumber)) {
                    while (true) {
                        try {
                            var job = await JobRepository.ClaimJob (jobnumber);
                            job.SetStatus (DownloadedJobStatus.Claimed);
                            AddJob (job);
                            return job;
                        } catch (Exception ex) {
                            // We already have claimed this job, so just add it to our local data
                            if (ex.Message.Contains ("already assigned to this editor")) {
                                // Make sure we don't already have it in our list
                                var job = ClaimedJobs.Where (p => p.JobNumber == jobnumber).FirstOrDefault ();

                                if (job == null) {
                                    job = JobRepository.GetJob (jobnumber).Result;
                                    job.SetStatus (DownloadedJobStatus.Claimed);
                                    AddJob (job);
                                }

                                return job;
                            }

                            // See if someone else already claimed this job
                            if (ex.Message.Contains ("is assigned to a different editor")) {
                                EditorCore.Background.RaiseShowMessage ("Cannot Claim Job", string.Format ("Job {0} has already been claimed by another editor.", jobnumber));
                                return null;
                            }

                            // See if the job has already been edited
                            if (ex.Message.Contains ("has a not available for edition status")) {
                                EditorCore.Background.RaiseShowMessage ("Cannot Claim Job", string.Format ("Job {0} has already been edited.", jobnumber));
                                return null;
                            }

                            // See if the editor doesn't have permission for the job
                            if (ex.Message.Contains ("does not have permissions to work with job")) {
                                EditorCore.Background.RaiseShowMessage ("Cannot Claim Job", string.Format ("You do not have access to job {0}.", jobnumber));
                                return null;
                            }

                            if (!EditorCore.Background.RetryGettingDataException ("An error has occured claiming job " + jobnumber + " from the server.", ex))
                                return null;
                        }
                    }
                }
        }

        public async Task<MedicalJobEntity> ClaimNextJob ()
        {
            using (EditorCore.CreateStatusUpdate (string.Format ("Claiming next job in queue.."), "claim")) {
                using (EditorCore.CreateStopwatch ("GetNextAvailableJob")) {
                    while (true) {
                        try {
                            var job = await JobRepository.GetNextAvailableJob ();

                            // No more jobs in the queue
                            if (job == null)
                                return null;

                            job.SetStatus (DownloadedJobStatus.Claimed);
                            AddJob (job);
                            return job;
                        } catch (Exception ex) {
                            if (!EditorCore.Background.RetryGettingDataException ("An error has occured claiming the next job in the queue.", ex))
                                return null;
                        }
                    }
                }
            }
        }

        public void StartQueue ()
        {
            if (WorkQueueRunning)
                return;

            EditorCore.LogDebug ("Starting automatic edit queue");

            WorkQueueRunning = true;

            if (WorkQueueRunningChanged != null)
                WorkQueueRunningChanged (null, EventArgs.Empty);
        }

        public void StopQueue ()
        {
            if (!WorkQueueRunning)
                return;

            EditorCore.LogDebug ("Stopping automatic edit queue");

            WorkQueueRunning = false;

            if (WorkQueueRunningChanged != null)
                WorkQueueRunningChanged (null, EventArgs.Empty);
        }

        // Returns true for success, false for failure
        public async Task<bool> ReleaseAllJobs ()
        {
            using (EditorCore.CreateStatusUpdate ("Releasing all jobs", "release")) {
                while (ClaimedJobs.Count > 0) {
                    var job = ClaimedJobs[0];

                    var success = await ReleaseJob (job.JobNumber);

                    if (!success)
                        return false;
                }
            }

            return true;
        }

        // Returns true for success, false for failure
        public async Task<bool> ReleaseJob (string jobnumber)
        {
            using (EditorCore.CreateStatusUpdate (string.Format ("Releasing Job {0}", jobnumber), "release"))
                using (EditorCore.CreateStopwatch ("Releasing Job {0}", jobnumber))
                    while (true) {
                        try {
                            await JobRepository.ReleaseJob (jobnumber);
                            break;
                        } catch (Exception ex) {
                            // If the job had already been released, we don't need to throw an exception
                            if (ex.Message.Contains ("is not assigned for edition"))
                                break;

                            // If the job had already been released, we don't need to throw an exception
                            if (ex.Message.Contains ("is not assigned to editor"))
                                break;

                            // If the job had been reassigned to someone else, we don't need to throw an exception
                            if (ex.Message.Contains ("does not have permissions"))
                                break;

                            // If the job had already been finished by another editor, we don't need to throw an exception
                            if (ex.Message.Contains ("not found on table or view getJobDownloadedForEditionFlag"))
                                break;

                            if (!EditorCore.Background.RetryGettingDataException ("An error has occured releasing job " + jobnumber + ". It is still assigned to you.", ex))
                                return false;
                        }
                    }

            RemoveJob (jobnumber);

            // Delete our local copy
            DeleteJob (jobnumber);

            return true;
        }

        // Fired every second by a background timer, checks to see 
        // if we need to claim or download new jobs
        public async Task<bool> BackgroundPulse ()
        {
            var out_of_jobs = false;

            // See if we need to claim more jobs
            if (WorkQueueRunning) {
                while (ClaimedJobs.Count < EditorCore.Settings.Editor.NumJobs) {
                    var job = await ClaimNextJob ();

                    // If the queue ran out of jobs, break out of the loop
                    if (job == null) {
                        EditorCore.LogDebug ("ClaimNextJob returned nothing.");
                        out_of_jobs = true;
                        break;
                    }

                    // If this is the first job, break out and start downloading
                    // it since ClaimNextJob can be pretty slow.  We'll get the
                    // rest on the next pulse.
                    if (ClaimedJobs.Count == 1)
                        break;
                }
            }

            // See if we need to download any more jobs:
            // - Not currently a job downloading
            // - Jobs that need to be downloaded
            while (QueuedJobs > 0) {
                // Find the next job
                var job = ClaimedJobs.Where (j => j.GetStatus () == DownloadedJobStatus.Claimed).First ();

                // Let everyone know we are downloading it
                job.SetStatus (DownloadedJobStatus.Downloading);

                if (ClaimedJobsUpdated != null)
                    ClaimedJobsUpdated (null, EventArgs.Empty);

                // Download it
                var success = await DownloadJob (job.JobNumber);
                
                // Bail if we didn't successfully download
                if (!success) {
                    await ReleaseJob (job.JobNumber);
                    break;
                }

                // Let everyone know we finished the download
                job.SetStatus (DownloadedJobStatus.Downloaded);

                if (ClaimedJobsUpdated != null)
                    ClaimedJobsUpdated (null, EventArgs.Empty);
            }

            // See if we need to return any jobs
            while (ReturnableJobs > 0) {
                // Find the next job
                var job = ClaimedJobs.Where (j => j.GetStatus () == DownloadedJobStatus.Completed || j.GetStatus () == DownloadedJobStatus.ToQA).First ();
                var old_status = job.GetStatus ();

                // Let everyone know we are uploading it
                job.SetStatus (DownloadedJobStatus.Sending);

                if (ClaimedJobsUpdated != null)
                    ClaimedJobsUpdated (null, EventArgs.Empty);

                // Upload it
                if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "editor")
                {
                    if (old_status == DownloadedJobStatus.Completed)
                    {
                        if (!(await FinishJob(job)))
                            break;
                    }
                    else if (old_status == DownloadedJobStatus.ToQA)
                    {
                        if (!(await SendJobToQA(job)))
                            break;
                    }
                }
                else
                {

                    if (!(await SendJobToQA(job)))
                        break;


                }

                RemoveJob (job);
                DeleteJob (job.JobNumber);
            }

            return out_of_jobs;
        }

        // Returns true for success, false for failure
        public async Task<bool> SplitJobByJobType (string jobNumber, string jobType, PatientEntity patient, string text)
        {
            using (EditorCore.CreateStatusUpdate (string.Format ("Spliting job by job type {0}..", jobNumber), "split")) {
                using (EditorCore.CreateStopwatch ("Spliting job by job type {0}..", jobNumber))
                    while (true) {
                        try {
                            var new_job = (await JobRepository.SplitJobByJobType (jobNumber, jobType, patient, text));

                            AddJob (new_job);
                            return true;
                        } catch (Exception ex) {
                            if (!EditorCore.Background.RetryGettingDataException ("An error has occured splitting the job. The job has not been split.", ex))
                                return false;
                        }
                    }
            }
        }

        // Returns true for success, false for failure
        public async Task<bool> SplitJobByPatient (string jobNumber, PatientEntity patient, string text)
        {
            using (EditorCore.CreateStatusUpdate (string.Format ("Spliting job by patient {0}..", jobNumber), "split")) {
                using (EditorCore.CreateStopwatch ("Spliting job by patient {0}..", jobNumber))
                    while (true) {
                        try {
                            var new_job = (await JobRepository.SplitJobByPatient (jobNumber, patient, text));

                            AddJob (new_job);
                            return true;
                        } catch (Exception ex) {
                            if (!EditorCore.Background.RetryGettingDataException ("An error has occured splitting the job. The job has not been split.", ex))
                                return false;
                        }
                    }
            }
        }

        // Returns true for success, false for failure
        public async Task<bool> FinishJob (MedicalJobEntity job)
        {
            using (EditorCore.CreateStatusUpdate (string.Format ("Uploading finished job {0}..", job.JobNumber), "upload"))
            using (EditorCore.CreateStopwatch ("Uploading finished job {0}..", job.JobNumber)) {

                var task = new ReturnJobTask ();

                task.JobNumber = job.JobNumber;

                task.File.Content = CreateSubmitZip (job);
                task.File.Checksum = EntityUtilities.CalculateFileChecksum (task.File.Content);

                while (true) {
                    try {
                        await JobRepository.FinishJob (task);
                        return true;
                    } catch (Exception ex) {
                        // If the job wasn't assigned to this editor, we don't need to throw an exception
                        if (ex.Message.Contains ("is not assigned for edition"))
                            return true;

                        if (!EditorCore.Background.RetryGettingDataException ("An error has occured returning finished job " + job.JobNumber + ". Will keep retrying in the background.", ex))
                            return false;
                    }
                }
            }
        }

        // Returns true for success, false for failure
        public async Task<bool> SendJobToQA (MedicalJobEntity job)
        {
            using (EditorCore.CreateStatusUpdate (string.Format ("Uploading finished job {0} to QA..", job.JobNumber), "upload"))
            using (EditorCore.CreateStopwatch ("Uploading finished job {0} to QA..", job.JobNumber)) {

                var task = new ReturnJobTask ();

                task.JobNumber = job.JobNumber;

                task.File.Content = CreateSubmitZip (job);
                task.File.Checksum = EntityUtilities.CalculateFileChecksum (task.File.Content);

                task.Note = job.QAData.LastQANote;
                if (EditorCore.Settings.Editor.Type.ToLowerInvariant() != "editor")
                {
                    if (job.QAData.CanReturnQACategory)
                    {
                        Entrada.Entities.QACategory cat = new Entrada.Entities.QACategory();
                        cat.ParentId = job.QAData.Category.ParentId;

                        cat.Id = job.QAData.Category.Id;
                        task.QACategory = cat;
                    }
                }

                while (true)
                {
                    try
                    {

                        if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "editor")
                        {
                            await JobRepository.SendJobToQA(task);
                        }
                        else
                        {
                            if ((int)job.QAData.NextQAStatusFlags == 1)
                            {
                                await JobRepository.SendJobToNextQA(task, NextQAStatus.ReturnJobToSameQA);
                            }
                            else if ((int)job.QAData.NextQAStatusFlags == 2)
                            {
                                await JobRepository.SendJobToNextQA(task, NextQAStatus.ReturnJobToNextQA);
                            }
                            else if ((int)job.QAData.NextQAStatusFlags == 4)
                            {
                                await JobRepository.SendJobToNextQA(task, NextQAStatus.ReturnJobToEntradaQA);
                            }
                            else if ((int)job.QAData.NextQAStatusFlags == 8)
                            {
                                await JobRepository.SendJobToNextQA(task, NextQAStatus.ReturnJobToCR);
                            }
                        }
                        return true;
                    } catch (Exception ex) {
                        // If the job wasn't assigned to this editor, we don't need to throw an exception
                        if (ex.Message.Contains ("is not assigned for edition"))
                            return true;

                        if (!EditorCore.Background.RetryGettingDataException ("An error has occured returning job " + job.JobNumber + " to QA. Will keep retrying in the background.", ex))
                            return false;
                    }
                }
            }
        }

        private byte[] CreateSubmitZip (MedicalJobEntity job)
        {
            // We need to build a zip to return with 4 items:
            // - Finished document (.doc)
            // - Updated demographics (.dat)
            // - VR "truth text" (.txt)
            // - Inserted macros stats (.cnt)
            var doc = Path.Combine (EditorCore.Settings.JobsDirectory, job.JobNumber, "Output", job.JobNumber + ".doc");
            var dat = Path.Combine (EditorCore.Settings.JobsDirectory, job.JobNumber, job.JobNumber + ".dat");
            var txt = Path.Combine (EditorCore.Settings.JobsDirectory, job.JobNumber, "Output", job.JobNumber + ".txt");
            var cnt = Path.Combine (EditorCore.Settings.JobsDirectory, job.JobNumber, "Output", job.JobNumber + ".cnt");

            using (var ms = new MemoryStream ()) {
                using (var zip = new ZipFile ()) {
                    using (var doc_file = EncryptedFileSystem.GetInputStream (doc)) {
                    using (var dat_file = EncryptedFileSystem.GetInputStream (dat)) {
                    using (var txt_file = EncryptedFileSystem.GetInputStream (txt)) {
                    using (var cnt_file = EncryptedFileSystem.GetInputStream (cnt)) {
                        zip.AddEntry (Path.GetFileName (doc), doc_file);
                        zip.AddEntry (Path.GetFileName (dat), dat_file);
                        zip.AddEntry (Path.GetFileName (txt), txt_file);
                        zip.AddEntry (Path.GetFileName (cnt), cnt_file);
                        zip.Save (ms);
#if DEBUG_DOCUMENTS
                        File.WriteAllBytes (@"C:\temp\output.zip", ms.ToArray ());
#endif
                    }
                    }
                    }
                    }
                }

                return ms.ToArray ();
            }
        }

        // Returns true for success, false for failure
        public async Task<bool> DownloadJob (string jobnumber)
        {
            using (EditorCore.CreateStatusUpdate (string.Format ("Downloading job {0}..", jobnumber), "download")) {
                using (EditorCore.CreateStopwatch ("Downloading Job {0}", jobnumber))
                    while (true) {
                        try {
                            // Let's try to delete it first, just in case we still
                            // have a copy sitting out here for some reason
                            DeleteJob (jobnumber);
                            
                            await JobRepository.DownloadJob (jobnumber, EditorCore.Settings.JobsDirectory, EditorCore.Settings.Editor.Type.ToLowerInvariant());
                            return true;
                        } catch (Exception ex) {
                            if (!EditorCore.Background.RetryGettingDataException ("An error has occured downloading job " + jobnumber + ". Hitting cancel will release the job.", ex))
                                return false;
                        }
                    }
            }
        }

        public void FireClaimedJobsChanged ()
        {
            if (ClaimedJobsUpdated != null)
                ClaimedJobsUpdated (null, EventArgs.Empty);
        }

        public IEnumerable<string> GetLocalStoredJobs ()
        {
            return Directory.EnumerateDirectories (EditorCore.Settings.JobsDirectory).Select (p => Path.GetFileName (p));
        }

        // Make sure that a downloaded job has everything needed to edit
        public bool IsDownloadedJobValid (string jobnumber)
        {
            var dir = Path.Combine (EditorCore.Settings.JobsDirectory, jobnumber);

            if (!Directory.Exists (dir))
                return false;

            var audio = Path.Combine (dir, jobnumber + ".ogg");
            var txt = Path.Combine (dir, jobnumber + ".txt");
            var xml = Path.Combine (dir, jobnumber + ".xml");

            if (!File.Exists (audio))
                return false;

            if (!File.Exists (txt))
                return false;

            if (!File.Exists (xml))
                return false;

            return true;
        }

        public void DeleteJob (string jobnumber)
        {
            var dir = Path.Combine (EditorCore.Settings.JobsDirectory, jobnumber);

            if (CloseSoundRequested != null)
                CloseSoundRequested (null, new CloseSoundEventArgs (dir));

            try {
                if (Directory.Exists (dir))
                    Directory.Delete (dir, true);
            } catch {
                // This might fail, but we'll clean it up during Reconcile jobs on next app start
            }
        }

        private void DeleteInvalidLocalJobs ()
        {
            var jobs = GetLocalStoredJobs ();

            foreach (var job in jobs.Where (p => !IsDownloadedJobValid (p)))
                DeleteJob (job);
        }

        /// <summary>
        /// Compares what we can find in our downloaded jobs directory to what
        /// the server thinks we're working on and reconciles the differences
        /// </summary>
        /// <returns>Jobs that the server says we've claimed, but we don't have local (missing jobs)</returns>
        public async Task<IEnumerable<MedicalJobEntity>> ReconcileJobs ()
        {
            // Get all server claimed jobs
            var remote = await GetAssignedJobs ();

            using (EditorCore.CreateStatusUpdate ("Reconciling claimed jobs..", "reconcile")) {
                // Clean up invalid local jobs
                DeleteInvalidLocalJobs ();

                // Get all local downloaded jobs
                var local = GetLocalStoredJobs ();

                // If we couldn't get assigned jobs, we can't let the
                // editor continue in an unknown state
                if (remote == null)
                    throw new ApplicationTerminatingException ();
                
                // Delete local jobs we don't have claimed
                var extra_local = local.Where (jn => !remote.Any (job => job.JobNumber == jn));

                foreach (var job in extra_local)
                    DeleteJob (job);
                
                // Figure up claimed jobs we don't have downloaded (missing jobs)
                var extra_remote = remote.Where (job => !local.Any (jn => job.JobNumber == jn));

                // Figure up claimed jobs that we have downloaded
                var good_jobs = remote.Where (job => local.Any (jn => job.JobNumber == jn));

                foreach (var job in good_jobs) {
                    job.SetStatus (DownloadedJobStatus.Downloaded);

                    if (!ClaimedJobs.Any (p => p.JobNumber == job.JobNumber))
                        AddJob (job);
                }

                return extra_remote;
            }
        }
    }
}
