namespace HospitalManagementSystem.Views.Auth
{
    partial class LoginForm
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer? components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // Layout = redesign mockup, artboard "1 · Login" (sizes are the mockup's px at 96 DPI):
        //   left  : navy brand panel (logo, name, headline, tagline, version line), 64 px padding
        //   right : light page with a 420 px white sign-in card (44 px padding, 22 px between rows)
        // Palette: Navy #1B2B3F | Page #F3F6F9 | Card #FFFFFF | Teal #127A8C | Text #142233 | Muted #5B6B7C

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlBrand = new Panel();
            tlpBrand = new TableLayoutPanel();
            pnlBrandTop = new Panel();
            lblLogoSmall = new Label();
            lblTitle = new Label();
            pnlBrandMiddle = new Panel();
            lblLogo = new Label();
            lblHeadline = new Label();
            lblTagline = new Label();
            lblVersion = new Label();
            tlpRight = new TableLayoutPanel();
            pnlCard = new Panel();
            tlpCard = new TableLayoutPanel();
            lblSignIn = new Label();
            lblSubtitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            pnlPasswordOptions = new Panel();
            chkShowPassword = new CheckBox();
            lblCapsLock = new Label();
            lblMessage = new Label();
            btnLogin = new Button();
            btnExit = new Button();
            lblLockNote = new Label();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlBrand.SuspendLayout();
            tlpBrand.SuspendLayout();
            pnlBrandTop.SuspendLayout();
            pnlBrandMiddle.SuspendLayout();
            tlpRight.SuspendLayout();
            pnlCard.SuspendLayout();
            tlpCard.SuspendLayout();
            pnlPasswordOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            //
            // tlpRoot
            //
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 2;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.67F));
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58.33F));
            tlpRoot.Controls.Add(pnlBrand, 0, 0);
            tlpRoot.Controls.Add(tlpRight, 1, 0);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Location = new Point(0, 0);
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 1;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.Size = new Size(1440, 900);
            tlpRoot.TabIndex = 0;
            //
            // pnlBrand
            //
            pnlBrand.BackColor = Color.FromArgb(27, 43, 63);
            pnlBrand.Controls.Add(tlpBrand);
            pnlBrand.Dock = DockStyle.Fill;
            pnlBrand.Margin = new Padding(0);
            pnlBrand.Name = "pnlBrand";
            pnlBrand.Padding = new Padding(64);
            pnlBrand.Size = new Size(600, 900);
            pnlBrand.TabIndex = 0;
            //
            // tlpBrand
            //
            tlpBrand.BackColor = Color.Transparent;
            tlpBrand.ColumnCount = 1;
            tlpBrand.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBrand.Controls.Add(pnlBrandTop, 0, 0);
            tlpBrand.Controls.Add(pnlBrandMiddle, 0, 2);
            tlpBrand.Controls.Add(lblVersion, 0, 4);
            tlpBrand.Dock = DockStyle.Fill;
            tlpBrand.Margin = new Padding(0);
            tlpBrand.Name = "tlpBrand";
            tlpBrand.RowCount = 5;
            tlpBrand.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpBrand.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpBrand.RowStyles.Add(new RowStyle(SizeType.Absolute, 460F));
            tlpBrand.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpBrand.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpBrand.Size = new Size(472, 772);
            tlpBrand.TabIndex = 0;
            //
            // pnlBrandTop
            //
            pnlBrandTop.BackColor = Color.Transparent;
            pnlBrandTop.Controls.Add(lblTitle);
            pnlBrandTop.Controls.Add(lblLogoSmall);
            pnlBrandTop.Dock = DockStyle.Fill;
            pnlBrandTop.Margin = new Padding(0);
            pnlBrandTop.Name = "pnlBrandTop";
            pnlBrandTop.Size = new Size(472, 56);
            pnlBrandTop.TabIndex = 0;
            //
            // lblLogoSmall
            //
            lblLogoSmall.BackColor = Color.Transparent;
            lblLogoSmall.Location = new Point(0, 0);
            lblLogoSmall.Margin = new Padding(0);
            lblLogoSmall.Name = "lblLogoSmall";
            lblLogoSmall.Size = new Size(56, 56);
            lblLogoSmall.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(207, 227, 234);
            lblTitle.Location = new Point(72, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 56);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "HOSPITAL MANAGEMENT SYSTEM";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.UseMnemonic = false;
            //
            // pnlBrandMiddle
            //
            pnlBrandMiddle.BackColor = Color.Transparent;
            pnlBrandMiddle.Controls.Add(lblTagline);
            pnlBrandMiddle.Controls.Add(lblHeadline);
            pnlBrandMiddle.Controls.Add(lblLogo);
            pnlBrandMiddle.Dock = DockStyle.Fill;
            pnlBrandMiddle.Margin = new Padding(0);
            pnlBrandMiddle.Name = "pnlBrandMiddle";
            pnlBrandMiddle.Size = new Size(472, 460);
            pnlBrandMiddle.TabIndex = 1;
            //
            // lblLogo
            //
            lblLogo.BackColor = Color.Transparent;
            lblLogo.Font = new Font("Segoe UI Symbol", 72F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLogo.ForeColor = Color.FromArgb(34, 163, 184);
            lblLogo.Location = new Point(0, 0);
            lblLogo.Margin = new Padding(0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(168, 168);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "✚";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblHeadline
            //
            lblHeadline.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblHeadline.BackColor = Color.Transparent;
            lblHeadline.Font = new Font("Segoe UI", 33F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHeadline.ForeColor = Color.White;
            lblHeadline.Location = new Point(0, 188);
            lblHeadline.Margin = new Padding(0);
            lblHeadline.Name = "lblHeadline";
            lblHeadline.Size = new Size(472, 180);
            lblHeadline.TabIndex = 1;
            lblHeadline.Text = "Care, organised\r\nfrom front desk\r\nto pharmacy.";
            //
            // lblTagline
            //
            lblTagline.BackColor = Color.Transparent;
            lblTagline.Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTagline.ForeColor = Color.FromArgb(185, 203, 214);
            lblTagline.Location = new Point(0, 388);
            lblTagline.Margin = new Padding(0);
            lblTagline.Name = "lblTagline";
            lblTagline.Size = new Size(420, 72);
            lblTagline.TabIndex = 2;
            lblTagline.Text = "Registration, appointments, queue, consultation, prescriptions, laboratory and billing — one system for the whole visit.";
            //
            // lblVersion
            //
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Dock = DockStyle.Fill;
            lblVersion.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVersion.ForeColor = Color.FromArgb(143, 165, 180);
            lblVersion.Margin = new Padding(0);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(472, 22);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "Version 1.0  ·  Connected to hospital_management_db";
            lblVersion.TextAlign = ContentAlignment.BottomLeft;
            //
            // tlpRight
            //
            tlpRight.BackColor = Color.FromArgb(243, 246, 249);
            tlpRight.ColumnCount = 3;
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 420F));
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRight.Controls.Add(pnlCard, 1, 1);
            tlpRight.Dock = DockStyle.Fill;
            tlpRight.Margin = new Padding(0);
            tlpRight.Name = "tlpRight";
            tlpRight.RowCount = 3;
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpRight.Size = new Size(840, 900);
            tlpRight.TabIndex = 1;
            //
            // pnlCard
            //
            pnlCard.AutoSize = true;
            pnlCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlCard.BackColor = Color.White;
            pnlCard.Controls.Add(tlpCard);
            pnlCard.Margin = new Padding(0);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(44);
            pnlCard.Size = new Size(420, 542);
            pnlCard.TabIndex = 0;
            //
            // tlpCard
            //
            tlpCard.AutoSize = true;
            tlpCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpCard.BackColor = Color.White;
            tlpCard.ColumnCount = 1;
            tlpCard.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 332F));
            tlpCard.Controls.Add(lblSignIn, 0, 0);
            tlpCard.Controls.Add(lblSubtitle, 0, 1);
            tlpCard.Controls.Add(lblUsername, 0, 2);
            tlpCard.Controls.Add(txtUsername, 0, 3);
            tlpCard.Controls.Add(lblPassword, 0, 4);
            tlpCard.Controls.Add(txtPassword, 0, 5);
            tlpCard.Controls.Add(pnlPasswordOptions, 0, 6);
            tlpCard.Controls.Add(lblMessage, 0, 7);
            tlpCard.Controls.Add(btnLogin, 0, 8);
            tlpCard.Controls.Add(btnExit, 0, 9);
            tlpCard.Controls.Add(lblLockNote, 0, 10);
            tlpCard.Location = new Point(44, 44);
            tlpCard.Margin = new Padding(0);
            tlpCard.Name = "tlpCard";
            tlpCard.RowCount = 11;
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCard.Size = new Size(332, 454);
            tlpCard.TabIndex = 0;
            //
            // lblSignIn
            //
            lblSignIn.AutoSize = true;
            lblSignIn.BackColor = Color.Transparent;
            lblSignIn.Font = new Font("Segoe UI", 19.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSignIn.ForeColor = Color.FromArgb(20, 34, 51);
            lblSignIn.Margin = new Padding(0, 0, 0, 6);
            lblSignIn.Name = "lblSignIn";
            lblSignIn.TabIndex = 10;
            lblSignIn.Text = "Sign in";
            //
            // lblSubtitle
            //
            lblSubtitle.AutoSize = true;
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtitle.ForeColor = Color.FromArgb(91, 107, 124);
            lblSubtitle.Margin = new Padding(0, 0, 0, 22);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.TabIndex = 11;
            lblSubtitle.Text = "Use your staff account to continue.";
            //
            // lblUsername
            //
            lblUsername.AutoSize = true;
            lblUsername.BackColor = Color.Transparent;
            lblUsername.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsername.ForeColor = Color.FromArgb(20, 34, 51);
            lblUsername.Margin = new Padding(0, 0, 0, 8);
            lblUsername.Name = "lblUsername";
            lblUsername.TabIndex = 12;
            lblUsername.Text = "Username";
            //
            // txtUsername
            //
            txtUsername.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.AutoSize = false;
            txtUsername.BackColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.ForeColor = Color.FromArgb(20, 34, 51);
            txtUsername.Margin = new Padding(0, 0, 0, 22);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(332, 46);
            txtUsername.TabIndex = 0;
            //
            // lblPassword
            //
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.Transparent;
            lblPassword.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassword.ForeColor = Color.FromArgb(20, 34, 51);
            lblPassword.Margin = new Padding(0, 0, 0, 8);
            lblPassword.Name = "lblPassword";
            lblPassword.TabIndex = 13;
            lblPassword.Text = "Password";
            //
            // txtPassword
            //
            txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.AutoSize = false;
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.ForeColor = Color.FromArgb(20, 34, 51);
            txtPassword.Margin = new Padding(0, 0, 0, 8);
            txtPassword.MaxLength = 128;
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(332, 46);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            //
            // pnlPasswordOptions
            //
            pnlPasswordOptions.BackColor = Color.Transparent;
            pnlPasswordOptions.Controls.Add(lblCapsLock);
            pnlPasswordOptions.Controls.Add(chkShowPassword);
            pnlPasswordOptions.Margin = new Padding(0, 0, 0, 22);
            pnlPasswordOptions.Name = "pnlPasswordOptions";
            pnlPasswordOptions.Size = new Size(332, 24);
            pnlPasswordOptions.TabIndex = 2;
            //
            // chkShowPassword
            //
            chkShowPassword.AutoSize = true;
            chkShowPassword.BackColor = Color.Transparent;
            chkShowPassword.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkShowPassword.ForeColor = Color.FromArgb(91, 107, 124);
            chkShowPassword.Location = new Point(0, 1);
            chkShowPassword.Margin = new Padding(0);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.TabIndex = 2;
            chkShowPassword.Text = "Show password";
            chkShowPassword.UseVisualStyleBackColor = false;
            //
            // lblCapsLock
            //
            lblCapsLock.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCapsLock.BackColor = Color.Transparent;
            lblCapsLock.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapsLock.ForeColor = Color.FromArgb(154, 91, 0);
            lblCapsLock.Location = new Point(182, 0);
            lblCapsLock.Name = "lblCapsLock";
            lblCapsLock.Size = new Size(150, 24);
            lblCapsLock.TabIndex = 3;
            lblCapsLock.Text = "Caps Lock is on";
            lblCapsLock.TextAlign = ContentAlignment.MiddleRight;
            lblCapsLock.Visible = false;
            //
            // lblMessage
            //
            lblMessage.AutoSize = true;
            lblMessage.BackColor = Color.Transparent;
            lblMessage.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMessage.ForeColor = Color.FromArgb(180, 35, 24);
            lblMessage.Margin = new Padding(0, 0, 0, 16);
            lblMessage.MaximumSize = new Size(332, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.TabIndex = 14;
            lblMessage.Visible = false;
            //
            // btnLogin
            //
            btnLogin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnLogin.BackColor = Color.FromArgb(18, 122, 140);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Margin = new Padding(0, 0, 0, 22);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(332, 48);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Sign in";
            btnLogin.UseVisualStyleBackColor = false;
            //
            // btnExit
            //
            btnExit.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnExit.BackColor = Color.White;
            btnExit.Cursor = Cursors.Hand;
            btnExit.DialogResult = DialogResult.Cancel;
            btnExit.FlatAppearance.BorderColor = Color.FromArgb(201, 211, 221);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.FromArgb(58, 74, 91);
            btnExit.Margin = new Padding(0, 0, 0, 22);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(332, 44);
            btnExit.TabIndex = 4;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = false;
            //
            // lblLockNote
            //
            lblLockNote.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblLockNote.BackColor = Color.Transparent;
            lblLockNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLockNote.ForeColor = Color.FromArgb(91, 107, 124);
            lblLockNote.Margin = new Padding(0);
            lblLockNote.Name = "lblLockNote";
            lblLockNote.Size = new Size(332, 20);
            lblLockNote.TabIndex = 15;
            lblLockNote.Text = "5 failed attempts lock the account for 15 minutes.";
            lblLockNote.TextAlign = ContentAlignment.MiddleCenter;
            //
            // errorProvider
            //
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            //
            // LoginForm
            //
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            CancelButton = btnExit;
            ClientSize = new Size(1440, 900);
            Controls.Add(tlpRoot);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            MinimumSize = new Size(1000, 680);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            WindowState = FormWindowState.Maximized;
            tlpRoot.ResumeLayout(false);
            pnlBrand.ResumeLayout(false);
            tlpBrand.ResumeLayout(false);
            pnlBrandTop.ResumeLayout(false);
            pnlBrandMiddle.ResumeLayout(false);
            tlpRight.ResumeLayout(false);
            tlpRight.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            tlpCard.ResumeLayout(false);
            tlpCard.PerformLayout();
            pnlPasswordOptions.ResumeLayout(false);
            pnlPasswordOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.TableLayoutPanel tlpBrand;
        private System.Windows.Forms.Panel pnlBrandTop;
        private System.Windows.Forms.Label lblLogoSmall;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlBrandMiddle;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblHeadline;
        private System.Windows.Forms.Label lblTagline;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.TableLayoutPanel tlpCard;
        private System.Windows.Forms.Label lblSignIn;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel pnlPasswordOptions;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Label lblCapsLock;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblLockNote;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
