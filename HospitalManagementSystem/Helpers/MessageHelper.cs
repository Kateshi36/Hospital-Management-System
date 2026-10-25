using System.Windows.Forms;
using HospitalManagementSystem.Config;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// One place for every pop-up message so wording, icons and captions stay consistent.
    /// (Inline validation uses ErrorProvider via FormValidator; these dialogs are for results and confirmations.)
    /// </summary>
    public static class MessageHelper
    {
        private static string Caption => AppConfig.AppName;

        public static void Info(string message, string? title = null, IWin32Window? owner = null)
        {
            MessageBox.Show(owner, message, title ?? Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Success(string message, string? title = null, IWin32Window? owner = null)
        {
            MessageBox.Show(owner, message, title ?? "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Warning(string message, string? title = null, IWin32Window? owner = null)
        {
            MessageBox.Show(owner, message, title ?? "Please check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string message, string? title = null, IWin32Window? owner = null)
        {
            MessageBox.Show(owner, message, title ?? "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>Shows a database / unexpected exception in plain language.</summary>
        public static void Error(Exception ex, string? contextMessage = null, IWin32Window? owner = null)
        {
            string friendly = DatabaseHelper.GetFriendlyMessage(ex);
            string text = string.IsNullOrWhiteSpace(contextMessage) ? friendly : contextMessage + "\n\n" + friendly;
            MessageBox.Show(owner, text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>Yes/No question. The default button is No, so Enter never confirms by accident.</summary>
        public static bool Confirm(string message, string? title = null, IWin32Window? owner = null)
        {
            return MessageBox.Show(owner, message, title ?? "Please confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        /// <summary>Confirmation for destructive actions (delete, cancel) with a warning icon.</summary>
        public static bool ConfirmDestructive(string message, string? title = null, IWin32Window? owner = null)
        {
            return MessageBox.Show(owner, message, title ?? "Confirm action",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        /// <summary>Lists every validation problem at once (specific messages, not a generic "Error").</summary>
        public static void ValidationErrors(IEnumerable<string> errors, string? title = null, IWin32Window? owner = null)
        {
            string body = "Please fix the following before saving:\n\n" +
                          string.Join("\n", errors.Select(e => "  \u2022 " + e));
            MessageBox.Show(owner, body, title ?? "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}