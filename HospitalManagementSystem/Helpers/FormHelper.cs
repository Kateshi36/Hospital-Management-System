using System.Windows.Forms;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// The shared set-up every form runs once, right after InitializeComponent():
    ///   - the light redesign theme: colours, rounded cards, rounded inputs, buttons, tables (ThemeHelper),
    ///   - drop-down lists line up with their boxes (ComboBoxHelper),
    ///   - the hospital logo is the window icon (LogoHelper).
    /// </summary>
    public static class FormHelper
    {
        /// <param name="theme">false only for a form that paints every pixel itself (the main window shell).</param>
        public static void Prepare(Form form, bool theme = true)
        {
            if (theme) ThemeHelper.ApplyTheme(form);
            ComboBoxHelper.AttachAll(form);
            LogoHelper.ApplyIcon(form);
        }
    }
}
