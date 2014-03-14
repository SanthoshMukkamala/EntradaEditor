using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entrada.Editor.Data;
using System.Threading.Tasks;

namespace Entrada.Editor.Core
{
    public class EditorManager
    {
        public async Task StartWork ()
        {
            using (EditorCore.CreateStopwatch ("StartWork")) {
                try {
                    var timesheet_id = await EditorRepository.StartWork ();
                    EditorCore.Settings.TimeSheetID = timesheet_id;
                } catch (Exception ex) {
                    // Log but ignore this error
                    EditorCore.LogException ("Error in StartWork", ex);
                }
            }
        }

        // Async doesn't really work here because we are sitting in the
        // FormClosing handler, so our message pump is terminating
        public void EndWork ()
        {
            using (EditorCore.CreateStopwatch ("EndWork")) {
                try {
                    var timesheet_id = EditorCore.Settings.TimeSheetID;

                    if (timesheet_id > -1)
                        EditorRepository.EndWork (timesheet_id);
                } catch (Exception ex) {
                    // Log but ignore this error
                    EditorCore.LogException ("Error in EndWork", ex);
                }
            }
        }

        public async Task ChangePassword (string oldPassword, string newPassword)
        {
            using (EditorCore.CreateStopwatch ("ChangePassword")) {
                await EditorRepository.ChangePassword (oldPassword, newPassword);

                // Update our version we use for the web services
                Settings.ResetPassword (newPassword);
            }
        }

        public bool IsPasswordStrongEnough (string password)
        {
            if (string.IsNullOrWhiteSpace (password))
                return false;

            if (password.Length < 8)
                return false;

            var categories = 0;

            if (password.Any (p => char.IsLower (p)))
                categories++;

            if (password.Any (p => char.IsUpper (p)))
                categories++;

            if (password.Any (p => char.IsNumber (p)))
                categories++;

            if (password.Any (p => char.IsPunctuation (p) || char.IsSymbol (p)))
                categories++;

            // Must contain at least 4 categories of upper/lower/number/symbol
            return categories >= 4;
        }
    }
}
