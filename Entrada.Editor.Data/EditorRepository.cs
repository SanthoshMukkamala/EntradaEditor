using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entrada.Entities;

namespace Entrada.Editor.Data
{
    public static class EditorRepository
    {
        public static Task<int> StartWork ()
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateEditService ();
                return ws.StartWork ();
            });
        }

        public static Task<EditorEntity> LogIn (string username, string password)
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateEditService (true);

                EntradaUser result = null;

                try {
                    result = ws.Authenticate (EntradaUserType.Editor, username, Settings.CalculateMD5Hash (password));
                } catch (System.ServiceModel.FaultException ex) {
                    if (ex.Message == "UserID and password not found.")
                        throw new LoginFailureException ();

                    throw;
                }

                if (result == null)
                    throw new LoginFailureException ();

                if (string.IsNullOrWhiteSpace (result.Token))
                    Settings.MustChangePassword = true;

                Settings.LoginToken = result.Token;
                Settings.UserName = username;
                Settings.Password = password;

                return GetEditor ().Result;
            });
        }

        public static Task ChangePassword (string oldPassword, string newPassword)
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateEditService ();
                ws.ChangePassword (oldPassword, newPassword);
            });
        }

        public static Task<QACategory[]> GetAllQACategories()
        {
            return Task.Factory.StartNew(() =>
            {
                var ws = Settings.CreateEditService();
                var listCategory = ws.GetAllQACategories();
                return listCategory;
            });
        }

        public static Task<EditorEntity> GetEditor ()
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateEditService ();
                return ws.GetEditor ();
            });
        }

        public static Task<CalculatedWorkSummaryEntity[]> GetEditorWorkSummary ()
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateEditService ();
                return ws.MyWorkSummary ().Select (p => CalculatedWorkSummaryEntity.FromWorkSummary (p)).ToArray ();
            });
        }

        public static void EndWork (int timesheetID)
        {
            var ws = Settings.CreateEditService ();
            ws.EndWork (timesheetID);
        }
    }
}
