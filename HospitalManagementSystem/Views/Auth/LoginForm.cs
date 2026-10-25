using HospitalManagementSystem.Config;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Repositories.Common;

namespace HospitalManagementSystem.Views.Auth
{
    /// <summary>
    /// Entry screen. Returns DialogResult.OK after a successful sign-in
    /// (SessionManager then holds the user); Cancel/Exit closes the application.
    ///
    /// All colours, fonts and button styles live in LoginForm.Designer.cs so the design is
    /// visible in the [Design] tab. This file only holds behaviour.
    /// </summary>
    public partial class LoginForm : Form
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly FormValidator _validator;

        public LoginForm()
        {
            InitializeComponent();
            ThemeHelper.MakeCard(pnlCard, 16);   // the sign-in card has 16 px corners in the mockup
            FormHelper.Prepare(this);            // light theme, rounded inputs, drop-downs, window icon
            ThemeHelper.StyleButton(btnLogin, ButtonKind.Primary);
            ThemeHelper.StyleButton(btnExit, ButtonKind.Secondary);
            pnlBrand.Paint += OnBrandPaint;
            pnlBrand.Resize += (s, e) => pnlBrand.Invalidate();

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtUsername, txtPassword);

            // User names hold letters, digits, dot, underscore and hyphen only (typing and pasting).
            // The password is deliberately not restricted: any character may be part of it.
            InputHelper.Attach(txtUsername, InputKind.Username, 50);

            ApplyConfigurableText();
            WireEvents();
        }

        // ------------------------------------------------------------------
        // Setup (only values that come from configuration - everything visual is in the Designer)
        // ------------------------------------------------------------------

        private void ApplyConfigurableText()
        {
            Text = AppConfig.AppName + " - Login";
            lblTitle.Text = AppConfig.AppName.ToUpperInvariant();
            LogoHelper.ShowInLabel(lblLogo);        // hospital logo (Assets\logo.png) instead of the cross symbol
            LogoHelper.ShowInLabel(lblLogoSmall);

            string database = "the hospital database";
            try
            {
                var builder = new MySqlConnector.MySqlConnectionStringBuilder(AppConfig.ConnectionString);
                if (!string.IsNullOrWhiteSpace(builder.Database)) database = builder.Database;
            }
            catch (Exception)
            {
                // A malformed connection string is reported when signing in; the caption just stays generic.
            }
            lblVersion.Text = $"Version {AppConfig.AppVersion}  \u00B7  Connected to {database}";

            try
            {
                int attempts = SettingsHelper.GetInt("max_login_attempts", 5);
                int minutes = SettingsHelper.GetInt("lockout_minutes", 15);
                lblLockNote.Text = $"{attempts} failed attempts lock the account for {minutes} minutes.";
            }
            catch (Exception)
            {
                // Database not reachable yet: keep the default text from the Designer.
            }
        }

        /// <summary>The faint teal ring in the lower-right corner of the brand panel (mockup decoration).</summary>
        private void OnBrandPaint(object? sender, PaintEventArgs e)
        {
            float scale = pnlBrand.DeviceDpi / 96f;
            float size = 520 * scale;
            float stroke = 48 * scale;
            var ring = new RectangleF(pnlBrand.Width - size + 220 * scale + stroke / 2,
                                      pnlBrand.Height - size + 180 * scale + stroke / 2,
                                      size - stroke, size - stroke);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.FromArgb(31, ThemeHelper.AccentBright), stroke);
            e.Graphics.DrawEllipse(pen, ring);
        }

        private void ShowMessage(string text)
        {
            lblMessage.Text = text;
            lblMessage.Visible = !string.IsNullOrWhiteSpace(text);
        }

        private void WireEvents()
        {
            btnLogin.Click += (s, e) => AttemptLogin();

            chkShowPassword.CheckedChanged += (s, e) =>
                txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;

            txtPassword.KeyUp += (s, e) => UpdateCapsLockHint();
            txtPassword.Enter += (s, e) => UpdateCapsLockHint();
            txtPassword.Leave += (s, e) => lblCapsLock.Visible = false;

            Shown += (s, e) => txtUsername.Focus();
        }

        private void UpdateCapsLockHint()
        {
            lblCapsLock.Visible = Control.IsKeyLocked(Keys.CapsLock);
        }

        // ------------------------------------------------------------------
        // Sign-in
        // ------------------------------------------------------------------

        private void AttemptLogin()
        {
            ShowMessage(string.Empty);

            _validator.Begin();
            _validator.Required(txtUsername, "Username");
            _validator.Required(txtPassword, "Password");

            if (!_validator.IsValid)
            {
                ShowMessage(string.Join(" ", _validator.Errors));
                _validator.FocusFirstInvalid();
                return;
            }

            Cursor = Cursors.WaitCursor;
            btnLogin.Enabled = false;

            try
            {
                AuthResult result = _userRepository.Authenticate(txtUsername.Text.Trim(), txtPassword.Text);

                if (result.IsSuccess && result.User != null)
                {
                    SessionManager.Start(result.User);
                    DialogResult = DialogResult.OK;   // closes the dialog
                    return;
                }

                ShowMessage(result.Message);
                txtPassword.Clear();
                txtPassword.Focus();
            }
            catch (Exception ex)
            {
                ShowMessage("Cannot sign in right now.\n" + DatabaseHelper.GetFriendlyMessage(ex));
            }
            finally
            {
                Cursor = Cursors.Default;
                btnLogin.Enabled = true;
            }
        }
    }
}