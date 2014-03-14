using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
    public class BackgroundManager
    {
        public event EventHandler<BackgroundStatusEventArgs> StatusUpdated;
        public event EventHandler StatusFinished;
        public event EventHandler<ConfirmationEventArgs> ConfirmAction;
        public event EventHandler<ConfirmationEventArgs> ShowMessage;
        public event EventHandler<UnhandledExceptionEventArgs> UnhandledException;
        public event EventHandler RefreshDemographics;
        public event EventHandler ShowIdleWarning;

        internal void ProvideStatusUpdate (string text)
        {
            if (StatusUpdated != null)
                StatusUpdated (null, new BackgroundStatusEventArgs () { Text = text });
        }

        internal void ProvideStatusUpdate (string text, string imageHint)
        {
            if (StatusUpdated != null)
                StatusUpdated (null, new BackgroundStatusEventArgs () { Text = text, ImageHint = imageHint });
        }

        internal void ProvideStatusFinished ()
        {
            if (StatusFinished != null)
                StatusFinished (null, EventArgs.Empty);
        }

        public bool RaiseConfirmAction (string text)
        {
            var e = new ConfirmationEventArgs (text);

            if (ConfirmAction != null)
                ConfirmAction (null, e);

            return !e.Cancel;
        }

        public bool RaiseShowMessage (string title, string text)
        {
            var e = new ConfirmationEventArgs (title, text);

            if (ShowMessage != null)
                ShowMessage (null, e);

            return !e.Cancel;
        }

        public void RaiseIdleWarning ()
        {
            // We only need to worry about idle-ness if the user has any jobs claimed
            if (EditorCore.Jobs.ClaimedJobs.Count > 0 && ShowIdleWarning != null)
                ShowIdleWarning (null, EventArgs.Empty);
        }
        
        public void RaiseRefreshDemographics ()
        {
            if (RefreshDemographics != null)
                RefreshDemographics (null, EventArgs.Empty);
        }

        public bool RaiseUnhandledException (string text, Exception ex, bool canRetry)
        {
            var e = new UnhandledExceptionEventArgs (text, ex, canRetry);

            EditorCore.LogDebug ("RETRY? {0}", ex.Message);

            if (UnhandledException != null)
                UnhandledException (null, e);

            // If they aren't choosing to retry it, let's log it
            if (e.Cancel)
                EditorCore.LogException ("Unhandled web service exception:\n\n{0}", ex);
            else
                EditorCore.LogDebug ("Retrying..");

            return !e.Cancel;
        }

        public bool RetryGettingDataException (string text, Exception ex)
        {
            text += " The error may be temporary (like losing connection to the internet), and retrying may fix it.";
            return RaiseUnhandledException (text, ex, true);
        }
    }
}
