using HospitalManagementSystem.Config;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Views.Auth;
using HospitalManagementSystem.Views.Main;

namespace HospitalManagementSystem
{
    internal static class Program
    {
        /// <summary>
        /// Application entry point: Login -> MainForm (sidebar + tabs).
        /// "Log out" in MainForm returns to the Login screen; closing MainForm exits the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Standard WinForms start-up: DPI awareness PerMonitorV2 (set by ApplicationHighDpiMode in the .csproj),
            // default font and visual styles.
            ApplicationConfiguration.Initialize();

            // Any error that is not caught elsewhere is shown in plain language instead of crashing.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) =>
                MessageHelper.Error(e.Exception, "An unexpected error occurred.");

            try
            {
                AppConfig.Load();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex.Message, "Configuration error");
                return;
            }

            // An older database (made with the first schema.sql) is upgraded in place - no data is removed.
            string? upgradeProblem = SchemaUpgrader.EnsureUpToDate();
            if (upgradeProblem != null) MessageHelper.Warning(upgradeProblem, "Database upgrade");

            bool showLogin = true;
            while (showLogin)
            {
                // 1) Sign in. Closing the login window (or pressing Exit) ends the application.
                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }
                }

                // 2) Main window. Returns when the user logs out or closes it.
                using (var main = new MainForm())
                {
                    Application.Run(main);
                    showLogin = main.LogoutRequested;
                }

                SessionManager.End();
            }
        }
    }
}