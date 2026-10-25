namespace HospitalManagementSystem.Views.MP02_PatientSearchEditor
{
    partial class PatientEditForm
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null!;

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

        // Redesign: page title + patient line, four cards (identification, contact, medical, emergency) in two
        // scrolling columns, and a footer card with the change summary and the actions.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblPatientInfo = new Label();
            lblTitle = new Label();
            pnlBody = new Panel();
            tlpBody = new TableLayoutPanel();
            pnlIdentityCard = new Panel();
            tlpIdentityCard = new TableLayoutPanel();
            lblIdentityTitle = new Label();
            lblIdentityHint = new Label();
            tlpIdentity = new TableLayoutPanel();
            lblCode = new Label();
            txtCode = new TextBox();
            lblRegistered = new Label();
            txtRegistered = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            tlpIdentity2 = new TableLayoutPanel();
            lblGender = new Label();
            txtGender = new TextBox();
            lblDob = new Label();
            dtpDob = new DateTimePicker();
            lblAge = new Label();
            txtAge = new TextBox();
            tlpOverride = new TableLayoutPanel();
            chkAdminOverride = new CheckBox();
            pnlAdmin = new Panel();
            tlpAdmin = new TableLayoutPanel();
            lblAdminUser = new Label();
            txtAdminUser = new TextBox();
            lblAdminPassword = new Label();
            txtAdminPassword = new TextBox();
            flowVerify = new FlowLayoutPanel();
            btnVerifyAdmin = new Button();
            lblOverrideStatus = new Label();
            pnlContactCard = new Panel();
            tlpContactCard = new TableLayoutPanel();
            lblContactTitle = new Label();
            tlpContact = new TableLayoutPanel();
            lblPhone = new Label();
            tlpPhone = new TableLayoutPanel();
            txtPhonePrefix = new TextBox();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblStreet = new Label();
            txtStreet = new TextBox();
            lblCity = new Label();
            cboCity = new ComboBox();
            lblProvince = new Label();
            txtProvince = new TextBox();
            lblPostal = new Label();
            txtPostalCode = new TextBox();
            pnlMedicalCard = new Panel();
            tlpMedicalCard = new TableLayoutPanel();
            lblMedicalTitle = new Label();
            tlpMedical = new TableLayoutPanel();
            lblBloodType = new Label();
            cboBloodType = new ComboBox();
            lblBloodSpacer = new Label();
            lblAllergies = new Label();
            txtAllergies = new TextBox();
            lblHistory = new Label();
            txtMedicalHistory = new TextBox();
            pnlEmergencyCard = new Panel();
            tlpEmergencyCard = new TableLayoutPanel();
            lblEmergencyTitle = new Label();
            tlpEmergency = new TableLayoutPanel();
            lblEmergencyName = new Label();
            txtEmergencyName = new TextBox();
            lblRelationship = new Label();
            cboRelationship = new ComboBox();
            lblEmergencyPhone = new Label();
            tlpEmergencyPhone = new TableLayoutPanel();
            txtEmergencyPhonePrefix = new TextBox();
            txtEmergencyPhone = new TextBox();
            pnlFooter = new Panel();
            lblChangeSummary = new Label();
            flowButtons = new FlowLayoutPanel();
            btnClose = new Button();
            btnReset = new Button();
            btnHistory = new Button();
            btnSave = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            pnlBody.SuspendLayout();
            tlpBody.SuspendLayout();
            pnlIdentityCard.SuspendLayout();
            tlpIdentityCard.SuspendLayout();
            tlpIdentity.SuspendLayout();
            tlpIdentity2.SuspendLayout();
            tlpOverride.SuspendLayout();
            pnlAdmin.SuspendLayout();
            tlpAdmin.SuspendLayout();
            flowVerify.SuspendLayout();
            pnlContactCard.SuspendLayout();
            tlpContactCard.SuspendLayout();
            tlpContact.SuspendLayout();
            tlpPhone.SuspendLayout();
            pnlMedicalCard.SuspendLayout();
            tlpMedicalCard.SuspendLayout();
            tlpMedical.SuspendLayout();
            pnlEmergencyCard.SuspendLayout();
            tlpEmergencyCard.SuspendLayout();
            tlpEmergency.SuspendLayout();
            tlpEmergencyPhone.SuspendLayout();
            pnlFooter.SuspendLayout();
            flowButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlTitle, 0, 0);
            tlpRoot.Controls.Add(pnlBody, 0, 1);
            tlpRoot.Controls.Add(pnlFooter, 0, 2);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 3;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
            tlpRoot.TabIndex = 0;
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Transparent;
            pnlTitle.Controls.Add(lblPatientInfo);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Dock = DockStyle.Fill;
            pnlTitle.Margin = new Padding(0, 0, 0, 18);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Padding = new Padding(0);
            pnlTitle.TabIndex = 0;
            // 
            // lblPatientInfo
            // 
            lblPatientInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPatientInfo.AutoEllipsis = true;
            lblPatientInfo.BackColor = Color.Transparent;
            lblPatientInfo.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPatientInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblPatientInfo.Location = new Point(-2, 34);
            lblPatientInfo.Name = "lblPatientInfo";
            lblPatientInfo.Size = new Size(900, 22);
            lblPatientInfo.TabIndex = 0;
            lblPatientInfo.Text = "";
            lblPatientInfo.UseMnemonic = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblTitle.Location = new Point(-4, -2);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Edit Patient Profile";
            lblTitle.UseMnemonic = false;
            // 
            // pnlBody
            // 
            pnlBody.AutoScroll = true;
            pnlBody.BackColor = Color.Transparent;
            pnlBody.Controls.Add(tlpBody);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Margin = new Padding(0);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(0, 0, 8, 0);
            pnlBody.TabIndex = 1;
            // 
            // tlpBody
            // 
            tlpBody.AutoSize = true;
            tlpBody.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpBody.BackColor = Color.Transparent;
            tlpBody.ColumnCount = 2;
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBody.Controls.Add(pnlIdentityCard, 0, 0);
            tlpBody.Controls.Add(pnlContactCard, 0, 1);
            tlpBody.Controls.Add(pnlMedicalCard, 1, 0);
            tlpBody.Controls.Add(pnlEmergencyCard, 1, 1);
            tlpBody.Dock = DockStyle.Top;
            tlpBody.Margin = new Padding(0);
            tlpBody.Name = "tlpBody";
            tlpBody.RowCount = 2;
            tlpBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBody.TabIndex = 0;
            // 
            // pnlIdentityCard
            // 
            pnlIdentityCard.AutoSize = true;
            pnlIdentityCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlIdentityCard.BackColor = Color.White;
            pnlIdentityCard.Controls.Add(tlpIdentityCard);
            pnlIdentityCard.Dock = DockStyle.Top;
            pnlIdentityCard.Margin = new Padding(0, 0, 10, 20);
            pnlIdentityCard.Name = "pnlIdentityCard";
            pnlIdentityCard.Padding = new Padding(20, 18, 20, 4);
            pnlIdentityCard.TabIndex = 0;
            // 
            // tlpIdentityCard
            // 
            tlpIdentityCard.AutoSize = true;
            tlpIdentityCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpIdentityCard.BackColor = Color.Transparent;
            tlpIdentityCard.ColumnCount = 1;
            tlpIdentityCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpIdentityCard.Controls.Add(lblIdentityTitle, 0, 0);
            tlpIdentityCard.Controls.Add(lblIdentityHint, 0, 1);
            tlpIdentityCard.Controls.Add(tlpIdentity, 0, 2);
            tlpIdentityCard.Controls.Add(tlpIdentity2, 0, 3);
            tlpIdentityCard.Controls.Add(tlpOverride, 0, 4);
            tlpIdentityCard.Dock = DockStyle.Top;
            tlpIdentityCard.Margin = new Padding(0);
            tlpIdentityCard.Name = "tlpIdentityCard";
            tlpIdentityCard.RowCount = 5;
            tlpIdentityCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpIdentityCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpIdentityCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpIdentityCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpIdentityCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpIdentityCard.TabIndex = 0;
            // 
            // lblIdentityTitle
            // 
            lblIdentityTitle.AutoSize = true;
            lblIdentityTitle.BackColor = Color.Transparent;
            lblIdentityTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIdentityTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblIdentityTitle.Margin = new Padding(0, 0, 0, 4);
            lblIdentityTitle.Name = "lblIdentityTitle";
            lblIdentityTitle.TabIndex = 0;
            lblIdentityTitle.Text = "Identification";
            // 
            // lblIdentityHint
            // 
            lblIdentityHint.AutoSize = true;
            lblIdentityHint.BackColor = Color.Transparent;
            lblIdentityHint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblIdentityHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblIdentityHint.Margin = new Padding(0, 0, 0, 14);
            lblIdentityHint.Name = "lblIdentityHint";
            lblIdentityHint.TabIndex = 1;
            lblIdentityHint.Text = "Locked fields are gray. Only an administrator can change the date of birth.";
            // 
            // tlpIdentity
            // 
            tlpIdentity.AutoSize = true;
            tlpIdentity.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpIdentity.BackColor = Color.Transparent;
            tlpIdentity.ColumnCount = 2;
            tlpIdentity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpIdentity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpIdentity.Controls.Add(lblCode, 0, 0);
            tlpIdentity.Controls.Add(txtCode, 0, 1);
            tlpIdentity.Controls.Add(lblRegistered, 1, 0);
            tlpIdentity.Controls.Add(txtRegistered, 1, 1);
            tlpIdentity.Controls.Add(lblFullName, 0, 2);
            tlpIdentity.SetColumnSpan(lblFullName, 2);
            tlpIdentity.Controls.Add(txtFullName, 0, 3);
            tlpIdentity.SetColumnSpan(txtFullName, 2);
            tlpIdentity.Dock = DockStyle.Top;
            tlpIdentity.Margin = new Padding(0);
            tlpIdentity.Name = "tlpIdentity";
            tlpIdentity.RowCount = 4;
            tlpIdentity.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpIdentity.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpIdentity.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpIdentity.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpIdentity.TabIndex = 2;
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.BackColor = Color.Transparent;
            lblCode.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCode.ForeColor = Color.FromArgb(20, 34, 51);
            lblCode.Margin = new Padding(0, 0, 6, 6);
            lblCode.Name = "lblCode";
            lblCode.TabIndex = 0;
            lblCode.Text = "Patient ID";
            // 
            // txtCode
            // 
            txtCode.AutoSize = false;
            txtCode.BackColor = Color.White;
            txtCode.BorderStyle = BorderStyle.FixedSingle;
            txtCode.Dock = DockStyle.Fill;
            txtCode.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCode.ForeColor = Color.FromArgb(20, 34, 51);
            txtCode.Margin = new Padding(0, 0, 6, 14);
            txtCode.MaxLength = 20;
            txtCode.Name = "txtCode";
            txtCode.ReadOnly = true;
            txtCode.Size = new Size(200, 40);
            txtCode.TabIndex = 1;
            txtCode.TabStop = false;
            // 
            // lblRegistered
            // 
            lblRegistered.AutoSize = true;
            lblRegistered.BackColor = Color.Transparent;
            lblRegistered.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRegistered.ForeColor = Color.FromArgb(20, 34, 51);
            lblRegistered.Margin = new Padding(6, 0, 0, 6);
            lblRegistered.Name = "lblRegistered";
            lblRegistered.TabIndex = 2;
            lblRegistered.Text = "Registration date";
            // 
            // txtRegistered
            // 
            txtRegistered.AutoSize = false;
            txtRegistered.BackColor = Color.White;
            txtRegistered.BorderStyle = BorderStyle.FixedSingle;
            txtRegistered.Dock = DockStyle.Fill;
            txtRegistered.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRegistered.ForeColor = Color.FromArgb(20, 34, 51);
            txtRegistered.Margin = new Padding(6, 0, 0, 14);
            txtRegistered.MaxLength = 40;
            txtRegistered.Name = "txtRegistered";
            txtRegistered.ReadOnly = true;
            txtRegistered.Size = new Size(200, 40);
            txtRegistered.TabIndex = 3;
            txtRegistered.TabStop = false;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.BackColor = Color.Transparent;
            lblFullName.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFullName.ForeColor = Color.FromArgb(20, 34, 51);
            lblFullName.Margin = new Padding(0, 0, 0, 6);
            lblFullName.Name = "lblFullName";
            lblFullName.TabIndex = 4;
            lblFullName.Text = "Full name";
            // 
            // txtFullName
            // 
            txtFullName.AutoSize = false;
            txtFullName.BackColor = Color.White;
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            txtFullName.Dock = DockStyle.Fill;
            txtFullName.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFullName.ForeColor = Color.FromArgb(20, 34, 51);
            txtFullName.Margin = new Padding(0, 0, 0, 14);
            txtFullName.MaxLength = 200;
            txtFullName.Name = "txtFullName";
            txtFullName.ReadOnly = true;
            txtFullName.Size = new Size(200, 40);
            txtFullName.TabIndex = 5;
            txtFullName.TabStop = false;
            // 
            // tlpIdentity2
            // 
            tlpIdentity2.AutoSize = true;
            tlpIdentity2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpIdentity2.BackColor = Color.Transparent;
            tlpIdentity2.ColumnCount = 3;
            tlpIdentity2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tlpIdentity2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpIdentity2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26F));
            tlpIdentity2.Controls.Add(lblGender, 0, 0);
            tlpIdentity2.Controls.Add(txtGender, 0, 1);
            tlpIdentity2.Controls.Add(lblDob, 1, 0);
            tlpIdentity2.Controls.Add(dtpDob, 1, 1);
            tlpIdentity2.Controls.Add(lblAge, 2, 0);
            tlpIdentity2.Controls.Add(txtAge, 2, 1);
            tlpIdentity2.Dock = DockStyle.Top;
            tlpIdentity2.Margin = new Padding(0);
            tlpIdentity2.Name = "tlpIdentity2";
            tlpIdentity2.RowCount = 2;
            tlpIdentity2.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpIdentity2.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpIdentity2.TabIndex = 3;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.BackColor = Color.Transparent;
            lblGender.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGender.ForeColor = Color.FromArgb(20, 34, 51);
            lblGender.Margin = new Padding(0, 0, 6, 6);
            lblGender.Name = "lblGender";
            lblGender.TabIndex = 0;
            lblGender.Text = "Gender";
            // 
            // txtGender
            // 
            txtGender.AutoSize = false;
            txtGender.BackColor = Color.White;
            txtGender.BorderStyle = BorderStyle.FixedSingle;
            txtGender.Dock = DockStyle.Fill;
            txtGender.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtGender.ForeColor = Color.FromArgb(20, 34, 51);
            txtGender.Margin = new Padding(0, 0, 6, 14);
            txtGender.MaxLength = 20;
            txtGender.Name = "txtGender";
            txtGender.ReadOnly = true;
            txtGender.Size = new Size(200, 40);
            txtGender.TabIndex = 1;
            txtGender.TabStop = false;
            // 
            // lblDob
            // 
            lblDob.AutoSize = true;
            lblDob.BackColor = Color.Transparent;
            lblDob.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDob.ForeColor = Color.FromArgb(20, 34, 51);
            lblDob.Margin = new Padding(6, 0, 6, 6);
            lblDob.Name = "lblDob";
            lblDob.TabIndex = 2;
            lblDob.Text = "Date of birth";
            // 
            // dtpDob
            // 
            dtpDob.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpDob.Enabled = false;
            dtpDob.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDob.Format = DateTimePickerFormat.Short;
            dtpDob.Margin = new Padding(6, 0, 6, 14);
            dtpDob.Name = "dtpDob";
            dtpDob.Size = new Size(200, 31);
            dtpDob.TabIndex = 3;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.BackColor = Color.Transparent;
            lblAge.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAge.ForeColor = Color.FromArgb(20, 34, 51);
            lblAge.Margin = new Padding(6, 0, 0, 6);
            lblAge.Name = "lblAge";
            lblAge.TabIndex = 4;
            lblAge.Text = "Age";
            // 
            // txtAge
            // 
            txtAge.AutoSize = false;
            txtAge.BackColor = Color.White;
            txtAge.BorderStyle = BorderStyle.FixedSingle;
            txtAge.Dock = DockStyle.Fill;
            txtAge.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAge.ForeColor = Color.FromArgb(20, 34, 51);
            txtAge.Margin = new Padding(6, 0, 0, 14);
            txtAge.MaxLength = 10;
            txtAge.Name = "txtAge";
            txtAge.ReadOnly = true;
            txtAge.Size = new Size(200, 40);
            txtAge.TabIndex = 5;
            txtAge.TabStop = false;
            // 
            // tlpOverride
            // 
            tlpOverride.AutoSize = true;
            tlpOverride.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpOverride.BackColor = Color.Transparent;
            tlpOverride.ColumnCount = 1;
            tlpOverride.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpOverride.Controls.Add(chkAdminOverride, 0, 0);
            tlpOverride.Controls.Add(pnlAdmin, 0, 1);
            tlpOverride.Dock = DockStyle.Top;
            tlpOverride.Margin = new Padding(0);
            tlpOverride.Name = "tlpOverride";
            tlpOverride.RowCount = 2;
            tlpOverride.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpOverride.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpOverride.TabIndex = 4;
            // 
            // chkAdminOverride
            // 
            chkAdminOverride.AutoSize = true;
            chkAdminOverride.BackColor = Color.Transparent;
            chkAdminOverride.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkAdminOverride.ForeColor = Color.FromArgb(20, 34, 51);
            chkAdminOverride.Margin = new Padding(0, 0, 0, 10);
            chkAdminOverride.Name = "chkAdminOverride";
            chkAdminOverride.TabIndex = 0;
            chkAdminOverride.Text = "Administrator override: unlock date of birth";
            chkAdminOverride.UseVisualStyleBackColor = false;
            // 
            // pnlAdmin
            // 
            pnlAdmin.AutoSize = true;
            pnlAdmin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlAdmin.BackColor = Color.FromArgb(247, 249, 251);
            pnlAdmin.Controls.Add(tlpAdmin);
            pnlAdmin.Dock = DockStyle.Top;
            pnlAdmin.Margin = new Padding(0);
            pnlAdmin.Name = "pnlAdmin";
            pnlAdmin.Padding = new Padding(14, 12, 14, 0);
            pnlAdmin.TabIndex = 1;
            pnlAdmin.Visible = false;
            // 
            // tlpAdmin
            // 
            tlpAdmin.AutoSize = true;
            tlpAdmin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpAdmin.BackColor = Color.Transparent;
            tlpAdmin.ColumnCount = 2;
            tlpAdmin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpAdmin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpAdmin.Controls.Add(lblAdminUser, 0, 0);
            tlpAdmin.Controls.Add(txtAdminUser, 0, 1);
            tlpAdmin.Controls.Add(lblAdminPassword, 1, 0);
            tlpAdmin.Controls.Add(txtAdminPassword, 1, 1);
            tlpAdmin.Controls.Add(flowVerify, 0, 2);
            tlpAdmin.SetColumnSpan(flowVerify, 2);
            tlpAdmin.Dock = DockStyle.Top;
            tlpAdmin.Margin = new Padding(0);
            tlpAdmin.Name = "tlpAdmin";
            tlpAdmin.RowCount = 3;
            tlpAdmin.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpAdmin.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpAdmin.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpAdmin.TabIndex = 0;
            // 
            // lblAdminUser
            // 
            lblAdminUser.AutoSize = true;
            lblAdminUser.BackColor = Color.Transparent;
            lblAdminUser.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminUser.ForeColor = Color.FromArgb(20, 34, 51);
            lblAdminUser.Margin = new Padding(0, 0, 6, 6);
            lblAdminUser.Name = "lblAdminUser";
            lblAdminUser.TabIndex = 0;
            lblAdminUser.Text = "Administrator username";
            // 
            // txtAdminUser
            // 
            txtAdminUser.AutoSize = false;
            txtAdminUser.BackColor = Color.White;
            txtAdminUser.BorderStyle = BorderStyle.FixedSingle;
            txtAdminUser.Dock = DockStyle.Fill;
            txtAdminUser.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAdminUser.ForeColor = Color.FromArgb(20, 34, 51);
            txtAdminUser.Margin = new Padding(0, 0, 6, 14);
            txtAdminUser.MaxLength = 50;
            txtAdminUser.Name = "txtAdminUser";
            txtAdminUser.Size = new Size(200, 40);
            txtAdminUser.TabIndex = 1;
            // 
            // lblAdminPassword
            // 
            lblAdminPassword.AutoSize = true;
            lblAdminPassword.BackColor = Color.Transparent;
            lblAdminPassword.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminPassword.ForeColor = Color.FromArgb(20, 34, 51);
            lblAdminPassword.Margin = new Padding(6, 0, 0, 6);
            lblAdminPassword.Name = "lblAdminPassword";
            lblAdminPassword.TabIndex = 2;
            lblAdminPassword.Text = "Administrator password";
            // 
            // txtAdminPassword
            // 
            txtAdminPassword.AutoSize = false;
            txtAdminPassword.BackColor = Color.White;
            txtAdminPassword.BorderStyle = BorderStyle.FixedSingle;
            txtAdminPassword.Dock = DockStyle.Fill;
            txtAdminPassword.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAdminPassword.ForeColor = Color.FromArgb(20, 34, 51);
            txtAdminPassword.Margin = new Padding(6, 0, 0, 14);
            txtAdminPassword.MaxLength = 100;
            txtAdminPassword.Name = "txtAdminPassword";
            txtAdminPassword.Size = new Size(200, 40);
            txtAdminPassword.TabIndex = 3;
            txtAdminPassword.UseSystemPasswordChar = true;
            // 
            // flowVerify
            // 
            flowVerify.AutoSize = true;
            flowVerify.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowVerify.BackColor = Color.Transparent;
            flowVerify.Controls.Add(btnVerifyAdmin);
            flowVerify.Controls.Add(lblOverrideStatus);
            flowVerify.Margin = new Padding(0, 0, 0, 0);
            flowVerify.Name = "flowVerify";
            flowVerify.Padding = new Padding(0);
            flowVerify.TabIndex = 4;
            flowVerify.WrapContents = false;
            // 
            // btnVerifyAdmin
            // 
            btnVerifyAdmin.BackColor = Color.FromArgb(226, 242, 246);
            btnVerifyAdmin.FlatStyle = FlatStyle.Flat;
            btnVerifyAdmin.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVerifyAdmin.ForeColor = Color.FromArgb(20, 34, 51);
            btnVerifyAdmin.Margin = new Padding(0);
            btnVerifyAdmin.Name = "btnVerifyAdmin";
            btnVerifyAdmin.Size = new Size(100, 38);
            btnVerifyAdmin.TabIndex = 0;
            btnVerifyAdmin.Text = "Verify";
            btnVerifyAdmin.UseMnemonic = false;
            btnVerifyAdmin.UseVisualStyleBackColor = false;
            // 
            // lblOverrideStatus
            // 
            lblOverrideStatus.AutoSize = true;
            lblOverrideStatus.BackColor = Color.Transparent;
            lblOverrideStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOverrideStatus.ForeColor = Color.FromArgb(91, 107, 124);
            lblOverrideStatus.Margin = new Padding(12, 10, 0, 0);
            lblOverrideStatus.Name = "lblOverrideStatus";
            lblOverrideStatus.TabIndex = 1;
            lblOverrideStatus.Text = "";
            // 
            // pnlContactCard
            // 
            pnlContactCard.AutoSize = true;
            pnlContactCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlContactCard.BackColor = Color.White;
            pnlContactCard.Controls.Add(tlpContactCard);
            pnlContactCard.Dock = DockStyle.Top;
            pnlContactCard.Margin = new Padding(0, 0, 10, 0);
            pnlContactCard.Name = "pnlContactCard";
            pnlContactCard.Padding = new Padding(20, 18, 20, 4);
            pnlContactCard.TabIndex = 1;
            // 
            // tlpContactCard
            // 
            tlpContactCard.AutoSize = true;
            tlpContactCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpContactCard.BackColor = Color.Transparent;
            tlpContactCard.ColumnCount = 1;
            tlpContactCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpContactCard.Controls.Add(lblContactTitle, 0, 0);
            tlpContactCard.Controls.Add(tlpContact, 0, 1);
            tlpContactCard.Dock = DockStyle.Top;
            tlpContactCard.Margin = new Padding(0);
            tlpContactCard.Name = "tlpContactCard";
            tlpContactCard.RowCount = 2;
            tlpContactCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpContactCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpContactCard.TabIndex = 0;
            // 
            // lblContactTitle
            // 
            lblContactTitle.AutoSize = true;
            lblContactTitle.BackColor = Color.Transparent;
            lblContactTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblContactTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblContactTitle.Margin = new Padding(0, 0, 0, 14);
            lblContactTitle.Name = "lblContactTitle";
            lblContactTitle.TabIndex = 0;
            lblContactTitle.Text = "Contact and address";
            // 
            // tlpContact
            // 
            tlpContact.AutoSize = true;
            tlpContact.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpContact.BackColor = Color.Transparent;
            tlpContact.ColumnCount = 2;
            tlpContact.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpContact.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpContact.Controls.Add(lblPhone, 0, 0);
            tlpContact.Controls.Add(tlpPhone, 0, 1);
            tlpContact.Controls.Add(lblEmail, 1, 0);
            tlpContact.Controls.Add(txtEmail, 1, 1);
            tlpContact.Controls.Add(lblStreet, 0, 2);
            tlpContact.SetColumnSpan(lblStreet, 2);
            tlpContact.Controls.Add(txtStreet, 0, 3);
            tlpContact.SetColumnSpan(txtStreet, 2);
            tlpContact.Controls.Add(lblCity, 0, 4);
            tlpContact.Controls.Add(cboCity, 0, 5);
            tlpContact.Controls.Add(lblProvince, 1, 4);
            tlpContact.Controls.Add(txtProvince, 1, 5);
            tlpContact.Controls.Add(lblPostal, 0, 6);
            tlpContact.Controls.Add(txtPostalCode, 0, 7);
            tlpContact.Dock = DockStyle.Top;
            tlpContact.Margin = new Padding(0);
            tlpContact.Name = "tlpContact";
            tlpContact.RowCount = 8;
            tlpContact.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpContact.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpContact.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpContact.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpContact.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpContact.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpContact.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpContact.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpContact.TabIndex = 1;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.BackColor = Color.Transparent;
            lblPhone.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPhone.ForeColor = Color.FromArgb(20, 34, 51);
            lblPhone.Margin = new Padding(0, 0, 6, 6);
            lblPhone.Name = "lblPhone";
            lblPhone.TabIndex = 0;
            lblPhone.Text = "Mobile number *";
            // 
            // tlpPhone
            // 
            tlpPhone.BackColor = Color.Transparent;
            tlpPhone.ColumnCount = 2;
            tlpPhone.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 58F));
            tlpPhone.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPhone.Controls.Add(txtPhonePrefix, 0, 0);
            tlpPhone.Controls.Add(txtPhone, 1, 0);
            tlpPhone.Dock = DockStyle.Fill;
            tlpPhone.Margin = new Padding(0, 0, 6, 14);
            tlpPhone.Name = "tlpPhone";
            tlpPhone.RowCount = 1;
            tlpPhone.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPhone.TabIndex = 1;
            // 
            // txtPhonePrefix
            // 
            txtPhonePrefix.AutoSize = false;
            txtPhonePrefix.BackColor = Color.FromArgb(238, 242, 245);
            txtPhonePrefix.BorderStyle = BorderStyle.FixedSingle;
            txtPhonePrefix.Dock = DockStyle.Fill;
            txtPhonePrefix.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPhonePrefix.ForeColor = Color.FromArgb(20, 34, 51);
            txtPhonePrefix.Margin = new Padding(0, 0, 6, 0);
            txtPhonePrefix.MaxLength = 3;
            txtPhonePrefix.Name = "txtPhonePrefix";
            txtPhonePrefix.ReadOnly = true;
            txtPhonePrefix.Size = new Size(200, 40);
            txtPhonePrefix.TabIndex = 0;
            txtPhonePrefix.TabStop = false;
            txtPhonePrefix.Text = "+63";
            txtPhonePrefix.TextAlign = HorizontalAlignment.Center;
            // 
            // txtPhone
            // 
            txtPhone.AutoSize = false;
            txtPhone.BackColor = Color.White;
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Dock = DockStyle.Fill;
            txtPhone.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPhone.ForeColor = Color.FromArgb(20, 34, 51);
            txtPhone.Margin = new Padding(0);
            txtPhone.MaxLength = 25;
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "9XX XXX XXXX";
            txtPhone.Size = new Size(200, 40);
            txtPhone.TabIndex = 1;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.BackColor = Color.Transparent;
            lblEmail.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmail.ForeColor = Color.FromArgb(20, 34, 51);
            lblEmail.Margin = new Padding(6, 0, 0, 6);
            lblEmail.Name = "lblEmail";
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.AutoSize = false;
            txtEmail.BackColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Dock = DockStyle.Fill;
            txtEmail.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEmail.ForeColor = Color.FromArgb(20, 34, 51);
            txtEmail.Margin = new Padding(6, 0, 0, 14);
            txtEmail.MaxLength = 120;
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "name@example.com";
            txtEmail.Size = new Size(200, 40);
            txtEmail.TabIndex = 3;
            // 
            // lblStreet
            // 
            lblStreet.AutoSize = true;
            lblStreet.BackColor = Color.Transparent;
            lblStreet.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStreet.ForeColor = Color.FromArgb(20, 34, 51);
            lblStreet.Margin = new Padding(0, 0, 0, 6);
            lblStreet.Name = "lblStreet";
            lblStreet.TabIndex = 4;
            lblStreet.Text = "Street address";
            // 
            // txtStreet
            // 
            txtStreet.AutoSize = false;
            txtStreet.BackColor = Color.White;
            txtStreet.BorderStyle = BorderStyle.FixedSingle;
            txtStreet.Dock = DockStyle.Fill;
            txtStreet.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStreet.ForeColor = Color.FromArgb(20, 34, 51);
            txtStreet.Margin = new Padding(0, 0, 0, 14);
            txtStreet.MaxLength = 200;
            txtStreet.Name = "txtStreet";
            txtStreet.Size = new Size(200, 40);
            txtStreet.TabIndex = 5;
            // 
            // lblCity
            // 
            lblCity.AutoSize = true;
            lblCity.BackColor = Color.Transparent;
            lblCity.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCity.ForeColor = Color.FromArgb(20, 34, 51);
            lblCity.Margin = new Padding(0, 0, 6, 6);
            lblCity.Name = "lblCity";
            lblCity.TabIndex = 6;
            lblCity.Text = "City";
            // 
            // cboCity
            // 
            cboCity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboCity.DropDownStyle = ComboBoxStyle.DropDown;
            cboCity.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboCity.FormattingEnabled = true;
            cboCity.Margin = new Padding(0, 0, 6, 14);
            cboCity.Name = "cboCity";
            cboCity.Size = new Size(200, 32);
            cboCity.TabIndex = 7;
            // 
            // lblProvince
            // 
            lblProvince.AutoSize = true;
            lblProvince.BackColor = Color.Transparent;
            lblProvince.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblProvince.ForeColor = Color.FromArgb(20, 34, 51);
            lblProvince.Margin = new Padding(6, 0, 0, 6);
            lblProvince.Name = "lblProvince";
            lblProvince.TabIndex = 8;
            lblProvince.Text = "Province";
            // 
            // txtProvince
            // 
            txtProvince.AutoSize = false;
            txtProvince.BackColor = Color.White;
            txtProvince.BorderStyle = BorderStyle.FixedSingle;
            txtProvince.Dock = DockStyle.Fill;
            txtProvince.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtProvince.ForeColor = Color.FromArgb(20, 34, 51);
            txtProvince.Margin = new Padding(6, 0, 0, 14);
            txtProvince.MaxLength = 100;
            txtProvince.Name = "txtProvince";
            txtProvince.Size = new Size(200, 40);
            txtProvince.TabIndex = 9;
            // 
            // lblPostal
            // 
            lblPostal.AutoSize = true;
            lblPostal.BackColor = Color.Transparent;
            lblPostal.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPostal.ForeColor = Color.FromArgb(20, 34, 51);
            lblPostal.Margin = new Padding(0, 0, 6, 6);
            lblPostal.Name = "lblPostal";
            lblPostal.TabIndex = 10;
            lblPostal.Text = "Postal code";
            // 
            // txtPostalCode
            // 
            txtPostalCode.AutoSize = false;
            txtPostalCode.BackColor = Color.White;
            txtPostalCode.BorderStyle = BorderStyle.FixedSingle;
            txtPostalCode.Dock = DockStyle.Fill;
            txtPostalCode.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPostalCode.ForeColor = Color.FromArgb(20, 34, 51);
            txtPostalCode.Margin = new Padding(0, 0, 6, 14);
            txtPostalCode.MaxLength = 4;
            txtPostalCode.Name = "txtPostalCode";
            txtPostalCode.PlaceholderText = "1630";
            txtPostalCode.Size = new Size(200, 40);
            txtPostalCode.TabIndex = 11;
            // 
            // pnlMedicalCard
            // 
            pnlMedicalCard.AutoSize = true;
            pnlMedicalCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlMedicalCard.BackColor = Color.White;
            pnlMedicalCard.Controls.Add(tlpMedicalCard);
            pnlMedicalCard.Dock = DockStyle.Top;
            pnlMedicalCard.Margin = new Padding(10, 0, 0, 20);
            pnlMedicalCard.Name = "pnlMedicalCard";
            pnlMedicalCard.Padding = new Padding(20, 18, 20, 4);
            pnlMedicalCard.TabIndex = 2;
            // 
            // tlpMedicalCard
            // 
            tlpMedicalCard.AutoSize = true;
            tlpMedicalCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpMedicalCard.BackColor = Color.Transparent;
            tlpMedicalCard.ColumnCount = 1;
            tlpMedicalCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMedicalCard.Controls.Add(lblMedicalTitle, 0, 0);
            tlpMedicalCard.Controls.Add(tlpMedical, 0, 1);
            tlpMedicalCard.Dock = DockStyle.Top;
            tlpMedicalCard.Margin = new Padding(0);
            tlpMedicalCard.Name = "tlpMedicalCard";
            tlpMedicalCard.RowCount = 2;
            tlpMedicalCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedicalCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedicalCard.TabIndex = 0;
            // 
            // lblMedicalTitle
            // 
            lblMedicalTitle.AutoSize = true;
            lblMedicalTitle.BackColor = Color.Transparent;
            lblMedicalTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMedicalTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblMedicalTitle.Margin = new Padding(0, 0, 0, 14);
            lblMedicalTitle.Name = "lblMedicalTitle";
            lblMedicalTitle.TabIndex = 0;
            lblMedicalTitle.Text = "Medical information";
            // 
            // tlpMedical
            // 
            tlpMedical.AutoSize = true;
            tlpMedical.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpMedical.BackColor = Color.Transparent;
            tlpMedical.ColumnCount = 2;
            tlpMedical.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMedical.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMedical.Controls.Add(lblBloodType, 0, 0);
            tlpMedical.Controls.Add(cboBloodType, 0, 1);
            tlpMedical.Controls.Add(lblBloodSpacer, 1, 1);
            tlpMedical.Controls.Add(lblAllergies, 0, 2);
            tlpMedical.SetColumnSpan(lblAllergies, 2);
            tlpMedical.Controls.Add(txtAllergies, 0, 3);
            tlpMedical.SetColumnSpan(txtAllergies, 2);
            tlpMedical.Controls.Add(lblHistory, 0, 4);
            tlpMedical.SetColumnSpan(lblHistory, 2);
            tlpMedical.Controls.Add(txtMedicalHistory, 0, 5);
            tlpMedical.SetColumnSpan(txtMedicalHistory, 2);
            tlpMedical.Dock = DockStyle.Top;
            tlpMedical.Margin = new Padding(0);
            tlpMedical.Name = "tlpMedical";
            tlpMedical.RowCount = 6;
            tlpMedical.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedical.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpMedical.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedical.RowStyles.Add(new RowStyle(SizeType.Absolute, 124F));
            tlpMedical.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedical.RowStyles.Add(new RowStyle(SizeType.Absolute, 164F));
            tlpMedical.TabIndex = 1;
            // 
            // lblBloodType
            // 
            lblBloodType.AutoSize = true;
            lblBloodType.BackColor = Color.Transparent;
            lblBloodType.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBloodType.ForeColor = Color.FromArgb(20, 34, 51);
            lblBloodType.Margin = new Padding(0, 0, 6, 6);
            lblBloodType.Name = "lblBloodType";
            lblBloodType.TabIndex = 0;
            lblBloodType.Text = "Blood type";
            // 
            // cboBloodType
            // 
            cboBloodType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboBloodType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBloodType.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboBloodType.FormattingEnabled = true;
            cboBloodType.Margin = new Padding(0, 0, 6, 14);
            cboBloodType.Name = "cboBloodType";
            cboBloodType.Size = new Size(200, 32);
            cboBloodType.TabIndex = 1;
            // 
            // lblBloodSpacer
            // 
            lblBloodSpacer.AutoSize = true;
            lblBloodSpacer.BackColor = Color.Transparent;
            lblBloodSpacer.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBloodSpacer.ForeColor = Color.FromArgb(20, 34, 51);
            lblBloodSpacer.Margin = new Padding(6, 0, 0, 0);
            lblBloodSpacer.Name = "lblBloodSpacer";
            lblBloodSpacer.TabIndex = 2;
            lblBloodSpacer.Text = "";
            // 
            // lblAllergies
            // 
            lblAllergies.AutoSize = true;
            lblAllergies.BackColor = Color.Transparent;
            lblAllergies.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAllergies.ForeColor = Color.FromArgb(20, 34, 51);
            lblAllergies.Margin = new Padding(0, 0, 0, 6);
            lblAllergies.Name = "lblAllergies";
            lblAllergies.TabIndex = 3;
            lblAllergies.Text = "Allergies";
            // 
            // txtAllergies
            // 
            txtAllergies.AutoSize = false;
            txtAllergies.BackColor = Color.White;
            txtAllergies.BorderStyle = BorderStyle.FixedSingle;
            txtAllergies.Dock = DockStyle.Fill;
            txtAllergies.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAllergies.ForeColor = Color.FromArgb(20, 34, 51);
            txtAllergies.Margin = new Padding(0, 0, 0, 14);
            txtAllergies.MaxLength = 2000;
            txtAllergies.Multiline = true;
            txtAllergies.Name = "txtAllergies";
            txtAllergies.ScrollBars = ScrollBars.Vertical;
            txtAllergies.Size = new Size(200, 110);
            txtAllergies.TabIndex = 4;
            // 
            // lblHistory
            // 
            lblHistory.AutoSize = true;
            lblHistory.BackColor = Color.Transparent;
            lblHistory.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHistory.ForeColor = Color.FromArgb(20, 34, 51);
            lblHistory.Margin = new Padding(0, 0, 0, 6);
            lblHistory.Name = "lblHistory";
            lblHistory.TabIndex = 5;
            lblHistory.Text = "Medical history";
            // 
            // txtMedicalHistory
            // 
            txtMedicalHistory.AutoSize = false;
            txtMedicalHistory.BackColor = Color.White;
            txtMedicalHistory.BorderStyle = BorderStyle.FixedSingle;
            txtMedicalHistory.Dock = DockStyle.Fill;
            txtMedicalHistory.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMedicalHistory.ForeColor = Color.FromArgb(20, 34, 51);
            txtMedicalHistory.Margin = new Padding(0, 0, 0, 14);
            txtMedicalHistory.MaxLength = 2000;
            txtMedicalHistory.Multiline = true;
            txtMedicalHistory.Name = "txtMedicalHistory";
            txtMedicalHistory.ScrollBars = ScrollBars.Vertical;
            txtMedicalHistory.Size = new Size(200, 150);
            txtMedicalHistory.TabIndex = 6;
            // 
            // pnlEmergencyCard
            // 
            pnlEmergencyCard.AutoSize = true;
            pnlEmergencyCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlEmergencyCard.BackColor = Color.White;
            pnlEmergencyCard.Controls.Add(tlpEmergencyCard);
            pnlEmergencyCard.Dock = DockStyle.Top;
            pnlEmergencyCard.Margin = new Padding(10, 0, 0, 0);
            pnlEmergencyCard.Name = "pnlEmergencyCard";
            pnlEmergencyCard.Padding = new Padding(20, 18, 20, 4);
            pnlEmergencyCard.TabIndex = 3;
            // 
            // tlpEmergencyCard
            // 
            tlpEmergencyCard.AutoSize = true;
            tlpEmergencyCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpEmergencyCard.BackColor = Color.Transparent;
            tlpEmergencyCard.ColumnCount = 1;
            tlpEmergencyCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEmergencyCard.Controls.Add(lblEmergencyTitle, 0, 0);
            tlpEmergencyCard.Controls.Add(tlpEmergency, 0, 1);
            tlpEmergencyCard.Dock = DockStyle.Top;
            tlpEmergencyCard.Margin = new Padding(0);
            tlpEmergencyCard.Name = "tlpEmergencyCard";
            tlpEmergencyCard.RowCount = 2;
            tlpEmergencyCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEmergencyCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEmergencyCard.TabIndex = 0;
            // 
            // lblEmergencyTitle
            // 
            lblEmergencyTitle.AutoSize = true;
            lblEmergencyTitle.BackColor = Color.Transparent;
            lblEmergencyTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmergencyTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblEmergencyTitle.Margin = new Padding(0, 0, 0, 14);
            lblEmergencyTitle.Name = "lblEmergencyTitle";
            lblEmergencyTitle.TabIndex = 0;
            lblEmergencyTitle.Text = "Emergency contact";
            // 
            // tlpEmergency
            // 
            tlpEmergency.AutoSize = true;
            tlpEmergency.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpEmergency.BackColor = Color.Transparent;
            tlpEmergency.ColumnCount = 2;
            tlpEmergency.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpEmergency.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpEmergency.Controls.Add(lblEmergencyName, 0, 0);
            tlpEmergency.SetColumnSpan(lblEmergencyName, 2);
            tlpEmergency.Controls.Add(txtEmergencyName, 0, 1);
            tlpEmergency.SetColumnSpan(txtEmergencyName, 2);
            tlpEmergency.Controls.Add(lblRelationship, 0, 2);
            tlpEmergency.Controls.Add(cboRelationship, 0, 3);
            tlpEmergency.Controls.Add(lblEmergencyPhone, 1, 2);
            tlpEmergency.Controls.Add(tlpEmergencyPhone, 1, 3);
            tlpEmergency.Dock = DockStyle.Top;
            tlpEmergency.Margin = new Padding(0);
            tlpEmergency.Name = "tlpEmergency";
            tlpEmergency.RowCount = 4;
            tlpEmergency.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEmergency.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpEmergency.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEmergency.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpEmergency.TabIndex = 1;
            // 
            // lblEmergencyName
            // 
            lblEmergencyName.AutoSize = true;
            lblEmergencyName.BackColor = Color.Transparent;
            lblEmergencyName.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmergencyName.ForeColor = Color.FromArgb(20, 34, 51);
            lblEmergencyName.Margin = new Padding(0, 0, 0, 6);
            lblEmergencyName.Name = "lblEmergencyName";
            lblEmergencyName.TabIndex = 0;
            lblEmergencyName.Text = "Contact name";
            // 
            // txtEmergencyName
            // 
            txtEmergencyName.AutoSize = false;
            txtEmergencyName.BackColor = Color.White;
            txtEmergencyName.BorderStyle = BorderStyle.FixedSingle;
            txtEmergencyName.Dock = DockStyle.Fill;
            txtEmergencyName.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEmergencyName.ForeColor = Color.FromArgb(20, 34, 51);
            txtEmergencyName.Margin = new Padding(0, 0, 0, 14);
            txtEmergencyName.MaxLength = 60;
            txtEmergencyName.Name = "txtEmergencyName";
            txtEmergencyName.Size = new Size(200, 40);
            txtEmergencyName.TabIndex = 1;
            // 
            // lblRelationship
            // 
            lblRelationship.AutoSize = true;
            lblRelationship.BackColor = Color.Transparent;
            lblRelationship.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRelationship.ForeColor = Color.FromArgb(20, 34, 51);
            lblRelationship.Margin = new Padding(0, 0, 6, 6);
            lblRelationship.Name = "lblRelationship";
            lblRelationship.TabIndex = 2;
            lblRelationship.Text = "Relationship";
            // 
            // cboRelationship
            // 
            cboRelationship.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboRelationship.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRelationship.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboRelationship.FormattingEnabled = true;
            cboRelationship.Margin = new Padding(0, 0, 6, 14);
            cboRelationship.Name = "cboRelationship";
            cboRelationship.Size = new Size(200, 32);
            cboRelationship.TabIndex = 3;
            // 
            // lblEmergencyPhone
            // 
            lblEmergencyPhone.AutoSize = true;
            lblEmergencyPhone.BackColor = Color.Transparent;
            lblEmergencyPhone.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmergencyPhone.ForeColor = Color.FromArgb(20, 34, 51);
            lblEmergencyPhone.Margin = new Padding(6, 0, 0, 6);
            lblEmergencyPhone.Name = "lblEmergencyPhone";
            lblEmergencyPhone.TabIndex = 4;
            lblEmergencyPhone.Text = "Contact mobile number";
            // 
            // tlpEmergencyPhone
            // 
            tlpEmergencyPhone.BackColor = Color.Transparent;
            tlpEmergencyPhone.ColumnCount = 2;
            tlpEmergencyPhone.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 58F));
            tlpEmergencyPhone.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEmergencyPhone.Controls.Add(txtEmergencyPhonePrefix, 0, 0);
            tlpEmergencyPhone.Controls.Add(txtEmergencyPhone, 1, 0);
            tlpEmergencyPhone.Dock = DockStyle.Fill;
            tlpEmergencyPhone.Margin = new Padding(6, 0, 0, 14);
            tlpEmergencyPhone.Name = "tlpEmergencyPhone";
            tlpEmergencyPhone.RowCount = 1;
            tlpEmergencyPhone.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpEmergencyPhone.TabIndex = 5;
            // 
            // txtEmergencyPhonePrefix
            // 
            txtEmergencyPhonePrefix.AutoSize = false;
            txtEmergencyPhonePrefix.BackColor = Color.FromArgb(238, 242, 245);
            txtEmergencyPhonePrefix.BorderStyle = BorderStyle.FixedSingle;
            txtEmergencyPhonePrefix.Dock = DockStyle.Fill;
            txtEmergencyPhonePrefix.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEmergencyPhonePrefix.ForeColor = Color.FromArgb(20, 34, 51);
            txtEmergencyPhonePrefix.Margin = new Padding(0, 0, 6, 0);
            txtEmergencyPhonePrefix.MaxLength = 3;
            txtEmergencyPhonePrefix.Name = "txtEmergencyPhonePrefix";
            txtEmergencyPhonePrefix.ReadOnly = true;
            txtEmergencyPhonePrefix.Size = new Size(200, 40);
            txtEmergencyPhonePrefix.TabIndex = 0;
            txtEmergencyPhonePrefix.TabStop = false;
            txtEmergencyPhonePrefix.Text = "+63";
            txtEmergencyPhonePrefix.TextAlign = HorizontalAlignment.Center;
            // 
            // txtEmergencyPhone
            // 
            txtEmergencyPhone.AutoSize = false;
            txtEmergencyPhone.BackColor = Color.White;
            txtEmergencyPhone.BorderStyle = BorderStyle.FixedSingle;
            txtEmergencyPhone.Dock = DockStyle.Fill;
            txtEmergencyPhone.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtEmergencyPhone.ForeColor = Color.FromArgb(20, 34, 51);
            txtEmergencyPhone.Margin = new Padding(0);
            txtEmergencyPhone.MaxLength = 25;
            txtEmergencyPhone.Name = "txtEmergencyPhone";
            txtEmergencyPhone.PlaceholderText = "9XX XXX XXXX";
            txtEmergencyPhone.Size = new Size(200, 40);
            txtEmergencyPhone.TabIndex = 1;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(lblChangeSummary);
            pnlFooter.Controls.Add(flowButtons);
            pnlFooter.Dock = DockStyle.Fill;
            pnlFooter.Margin = new Padding(0, 20, 0, 0);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(0);
            pnlFooter.TabIndex = 2;
            // 
            // lblChangeSummary
            // 
            lblChangeSummary.AutoEllipsis = true;
            lblChangeSummary.BackColor = Color.Transparent;
            lblChangeSummary.Dock = DockStyle.Fill;
            lblChangeSummary.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblChangeSummary.ForeColor = Color.FromArgb(91, 107, 124);
            lblChangeSummary.Name = "lblChangeSummary";
            lblChangeSummary.Padding = new Padding(18, 0, 8, 0);
            lblChangeSummary.TabIndex = 0;
            lblChangeSummary.TextAlign = ContentAlignment.MiddleLeft;
            lblChangeSummary.UseMnemonic = false;
            // 
            // flowButtons
            // 
            flowButtons.AutoSize = true;
            flowButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowButtons.BackColor = Color.Transparent;
            flowButtons.Controls.Add(btnClose);
            flowButtons.Controls.Add(btnReset);
            flowButtons.Controls.Add(btnHistory);
            flowButtons.Controls.Add(btnSave);
            flowButtons.Dock = DockStyle.Right;
            flowButtons.Margin = new Padding(0);
            flowButtons.Name = "flowButtons";
            flowButtons.Padding = new Padding(0, 11, 18, 0);
            flowButtons.TabIndex = 1;
            flowButtons.WrapContents = false;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClose.ForeColor = Color.FromArgb(20, 34, 51);
            btnClose.Margin = new Padding(10, 0, 0, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 40);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            btnClose.UseMnemonic = false;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.White;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReset.ForeColor = Color.FromArgb(20, 34, 51);
            btnReset.Margin = new Padding(10, 0, 0, 0);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(130, 40);
            btnReset.TabIndex = 1;
            btnReset.Text = "Undo Changes";
            btnReset.UseMnemonic = false;
            btnReset.UseVisualStyleBackColor = false;
            // 
            // btnHistory
            // 
            btnHistory.BackColor = Color.White;
            btnHistory.FlatStyle = FlatStyle.Flat;
            btnHistory.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHistory.ForeColor = Color.FromArgb(20, 34, 51);
            btnHistory.Margin = new Padding(10, 0, 0, 0);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(180, 40);
            btnHistory.TabIndex = 2;
            btnHistory.Text = "View Change History";
            btnHistory.UseMnemonic = false;
            btnHistory.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(18, 122, 140);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Margin = new Padding(10, 0, 0, 0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 40);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save Changes";
            btnSave.UseMnemonic = false;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // PatientEditForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1280, 800);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            MinimumSize = new Size(1000, 640);
            Padding = new Padding(28, 24, 28, 20);
            CancelButton = btnClose;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Patient Profile";
            WindowState = FormWindowState.Maximized;
            Controls.Add(tlpRoot);
            Name = "PatientEditForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            tlpBody.ResumeLayout(false);
            tlpBody.PerformLayout();
            pnlIdentityCard.ResumeLayout(false);
            pnlIdentityCard.PerformLayout();
            tlpIdentityCard.ResumeLayout(false);
            tlpIdentityCard.PerformLayout();
            tlpIdentity.ResumeLayout(false);
            tlpIdentity.PerformLayout();
            tlpIdentity2.ResumeLayout(false);
            tlpIdentity2.PerformLayout();
            tlpOverride.ResumeLayout(false);
            tlpOverride.PerformLayout();
            pnlAdmin.ResumeLayout(false);
            pnlAdmin.PerformLayout();
            tlpAdmin.ResumeLayout(false);
            tlpAdmin.PerformLayout();
            flowVerify.ResumeLayout(false);
            flowVerify.PerformLayout();
            pnlContactCard.ResumeLayout(false);
            pnlContactCard.PerformLayout();
            tlpContactCard.ResumeLayout(false);
            tlpContactCard.PerformLayout();
            tlpContact.ResumeLayout(false);
            tlpContact.PerformLayout();
            tlpPhone.ResumeLayout(false);
            tlpPhone.PerformLayout();
            pnlMedicalCard.ResumeLayout(false);
            pnlMedicalCard.PerformLayout();
            tlpMedicalCard.ResumeLayout(false);
            tlpMedicalCard.PerformLayout();
            tlpMedical.ResumeLayout(false);
            tlpMedical.PerformLayout();
            pnlEmergencyCard.ResumeLayout(false);
            pnlEmergencyCard.PerformLayout();
            tlpEmergencyCard.ResumeLayout(false);
            tlpEmergencyCard.PerformLayout();
            tlpEmergency.ResumeLayout(false);
            tlpEmergency.PerformLayout();
            tlpEmergencyPhone.ResumeLayout(false);
            tlpEmergencyPhone.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            flowButtons.ResumeLayout(false);
            flowButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private System.Windows.Forms.Panel pnlIdentityCard;
        private System.Windows.Forms.TableLayoutPanel tlpIdentityCard;
        private System.Windows.Forms.Label lblIdentityTitle;
        private System.Windows.Forms.Label lblIdentityHint;
        private System.Windows.Forms.TableLayoutPanel tlpIdentity;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblRegistered;
        private System.Windows.Forms.TextBox txtRegistered;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TableLayoutPanel tlpIdentity2;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.Label lblDob;
        private System.Windows.Forms.DateTimePicker dtpDob;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TableLayoutPanel tlpOverride;
        private System.Windows.Forms.CheckBox chkAdminOverride;
        private System.Windows.Forms.Panel pnlAdmin;
        private System.Windows.Forms.TableLayoutPanel tlpAdmin;
        private System.Windows.Forms.Label lblAdminUser;
        private System.Windows.Forms.TextBox txtAdminUser;
        private System.Windows.Forms.Label lblAdminPassword;
        private System.Windows.Forms.TextBox txtAdminPassword;
        private System.Windows.Forms.FlowLayoutPanel flowVerify;
        private System.Windows.Forms.Button btnVerifyAdmin;
        private System.Windows.Forms.Label lblOverrideStatus;
        private System.Windows.Forms.Panel pnlContactCard;
        private System.Windows.Forms.TableLayoutPanel tlpContactCard;
        private System.Windows.Forms.Label lblContactTitle;
        private System.Windows.Forms.TableLayoutPanel tlpContact;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TableLayoutPanel tlpPhone;
        private System.Windows.Forms.TextBox txtPhonePrefix;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblStreet;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.ComboBox cboCity;
        private System.Windows.Forms.Label lblProvince;
        private System.Windows.Forms.TextBox txtProvince;
        private System.Windows.Forms.Label lblPostal;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.Panel pnlMedicalCard;
        private System.Windows.Forms.TableLayoutPanel tlpMedicalCard;
        private System.Windows.Forms.Label lblMedicalTitle;
        private System.Windows.Forms.TableLayoutPanel tlpMedical;
        private System.Windows.Forms.Label lblBloodType;
        private System.Windows.Forms.ComboBox cboBloodType;
        private System.Windows.Forms.Label lblBloodSpacer;
        private System.Windows.Forms.Label lblAllergies;
        private System.Windows.Forms.TextBox txtAllergies;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.TextBox txtMedicalHistory;
        private System.Windows.Forms.Panel pnlEmergencyCard;
        private System.Windows.Forms.TableLayoutPanel tlpEmergencyCard;
        private System.Windows.Forms.Label lblEmergencyTitle;
        private System.Windows.Forms.TableLayoutPanel tlpEmergency;
        private System.Windows.Forms.Label lblEmergencyName;
        private System.Windows.Forms.TextBox txtEmergencyName;
        private System.Windows.Forms.Label lblRelationship;
        private System.Windows.Forms.ComboBox cboRelationship;
        private System.Windows.Forms.Label lblEmergencyPhone;
        private System.Windows.Forms.TableLayoutPanel tlpEmergencyPhone;
        private System.Windows.Forms.TextBox txtEmergencyPhonePrefix;
        private System.Windows.Forms.TextBox txtEmergencyPhone;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblChangeSummary;
        private System.Windows.Forms.FlowLayoutPanel flowButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
