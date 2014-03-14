using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entrada.Entities;
using Ionic.Zip;

namespace Entrada.Editor.Core
{
	public class DocumentManager
	{
		private int active_doc_index = -1;
        private object documents_lock = new object ();
		private List<DocumentEntity> open_documents;

        public bool HasActiveDocument { get { return active_doc_index >= 0; } }
        public bool HasOpenDocuments { get { lock (documents_lock) return open_documents.Count > 0; } }

		public event EventHandler<DocumentEventArgs> DocumentActivated;
		public event EventHandler<DocumentEventArgs> DocumentClosed;
		public event EventHandler<DocumentEventArgs> DocumentDeactivated;
		public event EventHandler<DocumentEventArgs> DocumentOpened;

		internal DocumentManager ()
		{
			open_documents = new List<DocumentEntity> ();
		}

		public DocumentEntity ActiveDocument {
			get {
                if (!HasActiveDocument)
					return null;

                if (active_doc_index >= open_documents.Count)
                    DebugDocumentIndex ("ActiveDocument ERROR");

                lock (documents_lock)
                    return open_documents[active_doc_index];
			}
		}

		public DocumentEntity GetDocument (string jobNumber)
		{
            lock (documents_lock)
			    return open_documents.Where (p => p.JobNumber == jobNumber).FirstOrDefault ();
		}

		public bool IsDocumentOpen (string jobNumber)
		{
            lock (documents_lock)
                return open_documents.Where (p => p.JobNumber == jobNumber).Count () > 0;
		}

		public void OpenDocument (MedicalJobEntity job)
		{
			var doc = new DocumentEntity (job);

            lock (documents_lock)
                open_documents.Add (doc);

            DebugDocumentIndex ("OpenDocument");

			if (DocumentOpened != null)
				DocumentOpened (null, new DocumentEventArgs (doc));
		}

        public void CloseAllDocuments ()
        {
            for (var i = open_documents.Count - 1; i >= 0; i--)
                CloseDocument (open_documents[i]);
        }

        public void CloseDocument (DocumentEntity document)
        {
            RemoveDocument (document);

            DebugDocumentIndex ("CloseDocument");

            if (DocumentClosed != null)
                DocumentClosed (null, new DocumentEventArgs (document));
        }

		public void SendDocument (DocumentEntity document)
		{
            // We need to build a zip to return with 4 items:
            // - Finished document (.doc)
            // - Updated demographics (.dat)
            // - VR "truth text" (.txt)
            // - Macro data (.cnt)
            var doc = Path.Combine (EditorCore.Settings.JobsDirectory, document.JobNumber, "Output", document.JobNumber + ".doc");
            var dat = Path.Combine (EditorCore.Settings.JobsDirectory, document.JobNumber, document.JobNumber + ".dat");
            var txt = Path.Combine (EditorCore.Settings.JobsDirectory, document.JobNumber, "Output", document.JobNumber + ".txt");
            var cnt = Path.Combine (EditorCore.Settings.JobsDirectory, document.JobNumber, "Output", document.JobNumber + ".cnt");

            if (!File.Exists (doc))
                throw new ApplicationException (string.Format (".doc file not found: {0}", doc));
            if (!File.Exists (dat))
                throw new ApplicationException (string.Format (".dat file not found: {0}", dat));
            if (!File.Exists (txt))
                throw new ApplicationException (string.Format (".txt file not found: {0}", txt));
            if (!File.Exists (cnt))
                throw new ApplicationException (string.Format (".cnt file not found: {0}", cnt));

            RemoveDocument (document);

            DebugDocumentIndex ("SendDocument");

            if (document.PreviewFor == SendTo.Finished)
                document.Job.SetStatus (DownloadedJobStatus.Completed);
            else if (document.PreviewFor == SendTo.QA)
                document.Job.SetStatus (DownloadedJobStatus.ToQA);
            else if (document.PreviewFor == SendTo.CR)
                document.Job.SetStatus (DownloadedJobStatus.ToCR);

            EditorCore.Jobs.FireClaimedJobsChanged ();

			if (DocumentClosed != null)
				DocumentClosed (null, new DocumentEventArgs (document));
		}

		public void ActivateDocument (DocumentEntity doc)
		{
			var old_index = active_doc_index;

            lock (documents_lock)
                active_doc_index = open_documents.IndexOf (doc);

            DebugDocumentIndex ("ActivateDocument");

			if (old_index == active_doc_index)
				return;

			if (DocumentActivated != null)
				DocumentActivated (null, new DocumentEventArgs (doc));
		}

		public void DeactivateDocument (DocumentEntity doc)
		{
			active_doc_index = -1;

            DebugDocumentIndex ("DeactivateDocument");

			if (DocumentDeactivated != null)
				DocumentDeactivated (null, new DocumentEventArgs (doc));
		}

        private void DebugDocumentIndex (string method)
        {
            EditorCore.LogDebug ("----> [{2}] - OpenDocuments: {0}, ActiveIndex: {1}", open_documents.Count, active_doc_index, method);
        }

        private void RemoveDocument (DocumentEntity document)
        {
            lock (documents_lock) {
                var active_doc = ActiveDocument;

                open_documents.Remove (document);

                if (active_doc == null) {
                    // Do nothing
                } else if (active_doc.JobNumber == document.JobNumber) {
                    // If this was the active document, reset the active index                   
                    active_doc_index = -1;
                } else {
                    // This might have changed the index of the active document
                    active_doc_index = open_documents.IndexOf (active_doc);
                }
            }
        }
	}
}
