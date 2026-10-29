namespace HospitalManagementSystem.Views.MP01_PatientRegistration
{
    partial class PatientRegistrationForm
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

        // Layout follows the redesign mockup's language (see Appointment Booking): page title + hint, a 460 px entry
        // card with numbered sections (labels above 40 px fields) and Save / Clear at the bottom, and on the right the
        // registered-patients table card above the profile-card preview.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            lblStep5 = new Label();
            tlpBenefits = new TableLayoutPanel();
            lblDiscountCard = new Label();
            cboDiscountCard = new ComboBox();
            lblDiscountCardNo = new Label();
            txtDiscountCardNo = new TextBox();
            lblCoverage = new Label();
            cboCoverage = new ComboBox();
            lblCoverageNo = new Label();
            txtCoverageNo = new TextBox();
            lblHmoProvider = new Label();
            cboHmoProvider = new ComboBox();
            lblBenefitsNote = new Label();
            capBenefits = new Label();
            lblCardBenefits = new Label();
            pnlTitle = new Panel();
            lblRequiredHint = new Label();
            lblTitle = new Label();
            tlpMain = new TableLayoutPanel();
            pnlEntryCard = new Panel();
            tlpEntryCard = new TableLayoutPanel();
            pnlEntryScroll = new Panel();
            tlpForm = new TableLayoutPanel();
            lblStep1 = new Label();
            tlpPersonal = new TableLayoutPanel();
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            lblMiddleName = new Label();
            txtMiddleName = new TextBox();
            lblLastName = new Label();
            txtLastName = new TextBox();
            tlpBirth = new TableLayoutPanel();
            lblDateOfBirth = new Label();
            dtpDateOfBirth = new DateTimePicker();
            lblAge = new Label();
            txtAge = new TextBox();
            lblGender = new Label();
            cboGender = new ComboBox();
            lblBloodType = new Label();
            cboBloodType = new ComboBox();
            lblStep2 = new Label();
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
            lblPostalCode = new Label();
            txtPostalCode = new TextBox();
            lblStep3 = new Label();
            tlpMedical = new TableLayoutPanel();
            lblAllergies = new Label();
            txtAllergies = new TextBox();
            lblMedicalHistory = new Label();
            txtMedicalHistory = new TextBox();
            lblStep4 = new Label();
            tlpEmergency = new TableLayoutPanel();
            lblEmergencyName = new Label();
            txtEmergencyName = new TextBox();
            lblRelationship = new Label();
            cboRelationship = new ComboBox();
            lblEmergencyPhone = new Label();
            tlpEmergencyPhone = new TableLayoutPanel();
            txtEmergencyPhonePrefix = new TextBox();
            txtEmergencyPhone = new TextBox();
            lblEmergencyNote = new Label();
            tlpButtons = new TableLayoutPanel();
            btnSave = new Button();
            btnClear = new Button();
            tlpRight = new TableLayoutPanel();
            pnlPatientsCard = new Panel();
            dgvPatients = new DataGridView();
            pnlPatientsHeader = new Panel();
            lblPatientsTitle = new Label();
            pnlProfileOuter = new Panel();
            pnlProfileCard = new Panel();
            lblCardEmpty = new Label();
            tlpCardBody = new TableLayoutPanel();
            capName = new Label();
            lblCardName = new Label();
            capDob = new Label();
            lblCardDob = new Label();
            capAge = new Label();
            lblCardAge = new Label();
            capGender = new Label();
            lblCardGender = new Label();
            capBlood = new Label();
            lblCardBlood = new Label();
            capPhone = new Label();
            lblCardPhone = new Label();
            capEmail = new Label();
            lblCardEmail = new Label();
            capAddress = new Label();
            lblCardAddress = new Label();
            capAllergies = new Label();
            lblCardAllergies = new Label();
            capHistory = new Label();
            lblCardHistory = new Label();
            capEmergency = new Label();
            lblCardEmergency = new Label();
            capRegistered = new Label();
            lblCardRegistered = new Label();
            pnlCardHeader = new Panel();
            lblCardCode = new Label();
            lblCardTitle = new Label();
            lblCardHospital = new Label();
            pnlProfileHeader = new Panel();
            lblProfileTitle = new Label();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            tlpBenefits.SuspendLayout();
            pnlTitle.SuspendLayout();
            tlpMain.SuspendLayout();
            pnlEntryCard.SuspendLayout();
            tlpEntryCard.SuspendLayout();
            pnlEntryScroll.SuspendLayout();
            tlpForm.SuspendLayout();
            tlpPersonal.SuspendLayout();
            tlpBirth.SuspendLayout();
            tlpContact.SuspendLayout();
            tlpPhone.SuspendLayout();
            tlpMedical.SuspendLayout();
            tlpEmergency.SuspendLayout();
            tlpEmergencyPhone.SuspendLayout();
            tlpButtons.SuspendLayout();
            tlpRight.SuspendLayout();
            pnlPatientsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
            pnlPatientsHeader.SuspendLayout();
            pnlProfileOuter.SuspendLayout();
            pnlProfileCard.SuspendLayout();
            tlpCardBody.SuspendLayout();
            pnlCardHeader.SuspendLayout();
            pnlProfileHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlTitle, 0, 0);
            tlpRoot.Controls.Add(tlpMain, 0, 1);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 2;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.TabIndex = 0;
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Transparent;
            pnlTitle.Controls.Add(lblRequiredHint);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Dock = DockStyle.Fill;
            pnlTitle.Margin = new Padding(0, 0, 0, 18);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Padding = new Padding(0);
            pnlTitle.TabIndex = 0;
            // 
            // lblRequiredHint
            // 
            lblRequiredHint.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblRequiredHint.AutoEllipsis = true;
            lblRequiredHint.BackColor = Color.Transparent;
            lblRequiredHint.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRequiredHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblRequiredHint.Location = new Point(-2, 34);
            lblRequiredHint.Name = "lblRequiredHint";
            lblRequiredHint.Size = new Size(900, 22);
            lblRequiredHint.TabIndex = 0;
            lblRequiredHint.Text = "Register a new patient, then check the saved record on the right. Fields marked * are required.";
            lblRequiredHint.UseMnemonic = false;
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
            lblTitle.Text = "Patient Registration";
            lblTitle.UseMnemonic = false;
            // 
            // tlpMain
            // 
            tlpMain.BackColor = Color.Transparent;
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 480F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(pnlEntryCard, 0, 0);
            tlpMain.Controls.Add(tlpRight, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Margin = new Padding(0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.TabIndex = 1;
            // 
            // pnlEntryCard
            // 
            pnlEntryCard.BackColor = Color.White;
            pnlEntryCard.Controls.Add(tlpEntryCard);
            pnlEntryCard.Dock = DockStyle.Fill;
            pnlEntryCard.Margin = new Padding(0, 0, 20, 0);
            pnlEntryCard.Name = "pnlEntryCard";
            pnlEntryCard.Padding = new Padding(20);
            pnlEntryCard.TabIndex = 0;
            // 
            // tlpEntryCard
            // 
            tlpEntryCard.BackColor = Color.Transparent;
            tlpEntryCard.ColumnCount = 1;
            tlpEntryCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEntryCard.Controls.Add(pnlEntryScroll, 0, 0);
            tlpEntryCard.Controls.Add(tlpButtons, 0, 1);
            tlpEntryCard.Dock = DockStyle.Fill;
            tlpEntryCard.Margin = new Padding(0);
            tlpEntryCard.Name = "tlpEntryCard";
            tlpEntryCard.RowCount = 2;
            tlpEntryCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpEntryCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpEntryCard.TabIndex = 0;
            // 
            // pnlEntryScroll
            // 
            pnlEntryScroll.AutoScroll = true;
            pnlEntryScroll.BackColor = Color.Transparent;
            pnlEntryScroll.Controls.Add(tlpForm);
            pnlEntryScroll.Dock = DockStyle.Fill;
            pnlEntryScroll.Margin = new Padding(0, 0, 0, 14);
            pnlEntryScroll.Name = "pnlEntryScroll";
            pnlEntryScroll.Padding = new Padding(0, 0, 6, 0);
            pnlEntryScroll.TabIndex = 0;
            // 
            // tlpForm
            // 
            tlpForm.AutoSize = true;
            tlpForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpForm.BackColor = Color.Transparent;
            tlpForm.ColumnCount = 1;
            tlpForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpForm.Controls.Add(lblStep1, 0, 0);
            tlpForm.Controls.Add(tlpPersonal, 0, 1);
            tlpForm.Controls.Add(tlpBirth, 0, 2);
            tlpForm.Controls.Add(lblStep2, 0, 3);
            tlpForm.Controls.Add(tlpContact, 0, 4);
            tlpForm.Controls.Add(lblStep3, 0, 5);
            tlpForm.Controls.Add(tlpMedical, 0, 6);
            tlpForm.Controls.Add(lblStep4, 0, 7);
            tlpForm.Controls.Add(tlpEmergency, 0, 8);
            tlpForm.Controls.Add(lblStep5, 0, 9);
            tlpForm.Controls.Add(tlpBenefits, 0, 10);
            tlpForm.Dock = DockStyle.Top;
            tlpForm.Margin = new Padding(0);
            tlpForm.Name = "tlpForm";
            tlpForm.RowCount = 11;
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpForm.TabIndex = 0;
            // 
            // lblStep1
            // 
            lblStep1.AutoSize = true;
            lblStep1.BackColor = Color.Transparent;
            lblStep1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep1.ForeColor = Color.FromArgb(18, 122, 140);
            lblStep1.Margin = new Padding(0, 0, 0, 14);
            lblStep1.Name = "lblStep1";
            lblStep1.TabIndex = 0;
            lblStep1.Text = "1  ·  PERSONAL";
            // 
            // tlpPersonal
            // 
            tlpPersonal.AutoSize = true;
            tlpPersonal.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpPersonal.BackColor = Color.Transparent;
            tlpPersonal.ColumnCount = 2;
            tlpPersonal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPersonal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPersonal.Controls.Add(lblFirstName, 0, 0);
            tlpPersonal.Controls.Add(txtFirstName, 0, 1);
            tlpPersonal.Controls.Add(lblMiddleName, 1, 0);
            tlpPersonal.Controls.Add(txtMiddleName, 1, 1);
            tlpPersonal.Controls.Add(lblLastName, 0, 2);
            tlpPersonal.SetColumnSpan(lblLastName, 2);
            tlpPersonal.Controls.Add(txtLastName, 0, 3);
            tlpPersonal.SetColumnSpan(txtLastName, 2);
            tlpPersonal.Dock = DockStyle.Top;
            tlpPersonal.Margin = new Padding(0);
            tlpPersonal.Name = "tlpPersonal";
            tlpPersonal.RowCount = 4;
            tlpPersonal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPersonal.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpPersonal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPersonal.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpPersonal.TabIndex = 1;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.BackColor = Color.Transparent;
            lblFirstName.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFirstName.ForeColor = Color.FromArgb(20, 34, 51);
            lblFirstName.Margin = new Padding(0, 0, 6, 6);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.TabIndex = 0;
            lblFirstName.Text = "First name *";
            // 
            // txtFirstName
            // 
            txtFirstName.AutoSize = false;
            txtFirstName.BackColor = Color.White;
            txtFirstName.BorderStyle = BorderStyle.FixedSingle;
            txtFirstName.Dock = DockStyle.Fill;
            txtFirstName.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFirstName.ForeColor = Color.FromArgb(20, 34, 51);
            txtFirstName.Margin = new Padding(0, 0, 6, 14);
            txtFirstName.MaxLength = 60;
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(200, 40);
            txtFirstName.TabIndex = 1;
            // 
            // lblMiddleName
            // 
            lblMiddleName.AutoSize = true;
            lblMiddleName.BackColor = Color.Transparent;
            lblMiddleName.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMiddleName.ForeColor = Color.FromArgb(20, 34, 51);
            lblMiddleName.Margin = new Padding(6, 0, 0, 6);
            lblMiddleName.Name = "lblMiddleName";
            lblMiddleName.TabIndex = 2;
            lblMiddleName.Text = "Middle name";
            // 
            // txtMiddleName
            // 
            txtMiddleName.AutoSize = false;
            txtMiddleName.BackColor = Color.White;
            txtMiddleName.BorderStyle = BorderStyle.FixedSingle;
            txtMiddleName.Dock = DockStyle.Fill;
            txtMiddleName.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMiddleName.ForeColor = Color.FromArgb(20, 34, 51);
            txtMiddleName.Margin = new Padding(6, 0, 0, 14);
            txtMiddleName.MaxLength = 60;
            txtMiddleName.Name = "txtMiddleName";
            txtMiddleName.Size = new Size(200, 40);
            txtMiddleName.TabIndex = 3;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.BackColor = Color.Transparent;
            lblLastName.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLastName.ForeColor = Color.FromArgb(20, 34, 51);
            lblLastName.Margin = new Padding(0, 0, 0, 6);
            lblLastName.Name = "lblLastName";
            lblLastName.TabIndex = 4;
            lblLastName.Text = "Last name *";
            // 
            // txtLastName
            // 
            txtLastName.AutoSize = false;
            txtLastName.BackColor = Color.White;
            txtLastName.BorderStyle = BorderStyle.FixedSingle;
            txtLastName.Dock = DockStyle.Fill;
            txtLastName.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLastName.ForeColor = Color.FromArgb(20, 34, 51);
            txtLastName.Margin = new Padding(0, 0, 0, 14);
            txtLastName.MaxLength = 60;
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(200, 40);
            txtLastName.TabIndex = 5;
            // 
            // tlpBirth
            // 
            tlpBirth.AutoSize = true;
            tlpBirth.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpBirth.BackColor = Color.Transparent;
            tlpBirth.ColumnCount = 2;
            tlpBirth.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            tlpBirth.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            tlpBirth.Controls.Add(lblDateOfBirth, 0, 0);
            tlpBirth.Controls.Add(dtpDateOfBirth, 0, 1);
            tlpBirth.Controls.Add(lblAge, 1, 0);
            tlpBirth.Controls.Add(txtAge, 1, 1);
            tlpBirth.Controls.Add(lblGender, 0, 2);
            tlpBirth.Controls.Add(cboGender, 0, 3);
            tlpBirth.Controls.Add(lblBloodType, 1, 2);
            tlpBirth.Controls.Add(cboBloodType, 1, 3);
            tlpBirth.Dock = DockStyle.Top;
            tlpBirth.Margin = new Padding(0);
            tlpBirth.Name = "tlpBirth";
            tlpBirth.RowCount = 4;
            tlpBirth.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBirth.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpBirth.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBirth.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpBirth.TabIndex = 2;
            // 
            // lblDateOfBirth
            // 
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.BackColor = Color.Transparent;
            lblDateOfBirth.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDateOfBirth.ForeColor = Color.FromArgb(20, 34, 51);
            lblDateOfBirth.Margin = new Padding(0, 0, 6, 6);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.TabIndex = 0;
            lblDateOfBirth.Text = "Date of birth *";
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpDateOfBirth.Checked = false;
            dtpDateOfBirth.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            dtpDateOfBirth.Margin = new Padding(0, 0, 6, 14);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.ShowCheckBox = true;
            dtpDateOfBirth.Size = new Size(200, 31);
            dtpDateOfBirth.TabIndex = 1;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.BackColor = Color.Transparent;
            lblAge.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAge.ForeColor = Color.FromArgb(20, 34, 51);
            lblAge.Margin = new Padding(6, 0, 0, 6);
            lblAge.Name = "lblAge";
            lblAge.TabIndex = 2;
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
            txtAge.MaxLength = 100;
            txtAge.Name = "txtAge";
            txtAge.ReadOnly = true;
            txtAge.Size = new Size(200, 40);
            txtAge.TabIndex = 3;
            txtAge.TabStop = false;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.BackColor = Color.Transparent;
            lblGender.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGender.ForeColor = Color.FromArgb(20, 34, 51);
            lblGender.Margin = new Padding(0, 0, 6, 6);
            lblGender.Name = "lblGender";
            lblGender.TabIndex = 4;
            lblGender.Text = "Gender *";
            // 
            // cboGender
            // 
            cboGender.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboGender.FormattingEnabled = true;
            cboGender.Margin = new Padding(0, 0, 6, 14);
            cboGender.Name = "cboGender";
            cboGender.Size = new Size(200, 32);
            cboGender.TabIndex = 5;
            // 
            // lblBloodType
            // 
            lblBloodType.AutoSize = true;
            lblBloodType.BackColor = Color.Transparent;
            lblBloodType.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBloodType.ForeColor = Color.FromArgb(20, 34, 51);
            lblBloodType.Margin = new Padding(6, 0, 0, 6);
            lblBloodType.Name = "lblBloodType";
            lblBloodType.TabIndex = 6;
            lblBloodType.Text = "Blood type";
            // 
            // cboBloodType
            // 
            cboBloodType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboBloodType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBloodType.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboBloodType.FormattingEnabled = true;
            cboBloodType.Margin = new Padding(6, 0, 0, 14);
            cboBloodType.Name = "cboBloodType";
            cboBloodType.Size = new Size(200, 32);
            cboBloodType.TabIndex = 7;
            // 
            // lblStep2
            // 
            lblStep2.AutoSize = true;
            lblStep2.BackColor = Color.Transparent;
            lblStep2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep2.ForeColor = Color.FromArgb(18, 122, 140);
            lblStep2.Margin = new Padding(0, 4, 0, 14);
            lblStep2.Name = "lblStep2";
            lblStep2.TabIndex = 3;
            lblStep2.Text = "2  ·  CONTACT";
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
            tlpContact.Controls.Add(lblPostalCode, 0, 6);
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
            tlpContact.TabIndex = 4;
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
            lblProvince.Text = "Province (from city)";
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
            // lblPostalCode
            // 
            lblPostalCode.AutoSize = true;
            lblPostalCode.BackColor = Color.Transparent;
            lblPostalCode.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPostalCode.ForeColor = Color.FromArgb(20, 34, 51);
            lblPostalCode.Margin = new Padding(0, 0, 6, 6);
            lblPostalCode.Name = "lblPostalCode";
            lblPostalCode.TabIndex = 10;
            lblPostalCode.Text = "Postal code (4 digits)";
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
            // lblStep3
            // 
            lblStep3.AutoSize = true;
            lblStep3.BackColor = Color.Transparent;
            lblStep3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep3.ForeColor = Color.FromArgb(18, 122, 140);
            lblStep3.Margin = new Padding(0, 4, 0, 14);
            lblStep3.Name = "lblStep3";
            lblStep3.TabIndex = 5;
            lblStep3.Text = "3  ·  MEDICAL";
            // 
            // tlpMedical
            // 
            tlpMedical.AutoSize = true;
            tlpMedical.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpMedical.BackColor = Color.Transparent;
            tlpMedical.ColumnCount = 2;
            tlpMedical.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMedical.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMedical.Controls.Add(lblAllergies, 0, 0);
            tlpMedical.SetColumnSpan(lblAllergies, 2);
            tlpMedical.Controls.Add(txtAllergies, 0, 1);
            tlpMedical.SetColumnSpan(txtAllergies, 2);
            tlpMedical.Controls.Add(lblMedicalHistory, 0, 2);
            tlpMedical.SetColumnSpan(lblMedicalHistory, 2);
            tlpMedical.Controls.Add(txtMedicalHistory, 0, 3);
            tlpMedical.SetColumnSpan(txtMedicalHistory, 2);
            tlpMedical.Dock = DockStyle.Top;
            tlpMedical.Margin = new Padding(0);
            tlpMedical.Name = "tlpMedical";
            tlpMedical.RowCount = 4;
            tlpMedical.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedical.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tlpMedical.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedical.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tlpMedical.TabIndex = 6;
            // 
            // lblAllergies
            // 
            lblAllergies.AutoSize = true;
            lblAllergies.BackColor = Color.Transparent;
            lblAllergies.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAllergies.ForeColor = Color.FromArgb(20, 34, 51);
            lblAllergies.Margin = new Padding(0, 0, 0, 6);
            lblAllergies.Name = "lblAllergies";
            lblAllergies.TabIndex = 0;
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
            txtAllergies.Size = new Size(200, 76);
            txtAllergies.TabIndex = 1;
            // 
            // lblMedicalHistory
            // 
            lblMedicalHistory.AutoSize = true;
            lblMedicalHistory.BackColor = Color.Transparent;
            lblMedicalHistory.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMedicalHistory.ForeColor = Color.FromArgb(20, 34, 51);
            lblMedicalHistory.Margin = new Padding(0, 0, 0, 6);
            lblMedicalHistory.Name = "lblMedicalHistory";
            lblMedicalHistory.TabIndex = 2;
            lblMedicalHistory.Text = "Pre-existing conditions";
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
            txtMedicalHistory.Size = new Size(200, 76);
            txtMedicalHistory.TabIndex = 3;
            // 
            // lblStep4
            // 
            lblStep4.AutoSize = true;
            lblStep4.BackColor = Color.Transparent;
            lblStep4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep4.ForeColor = Color.FromArgb(18, 122, 140);
            lblStep4.Margin = new Padding(0, 4, 0, 14);
            lblStep4.Name = "lblStep4";
            lblStep4.TabIndex = 7;
            lblStep4.Text = "4  ·  EMERGENCY CONTACT";
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
            tlpEmergency.Controls.Add(lblEmergencyNote, 0, 4);
            tlpEmergency.SetColumnSpan(lblEmergencyNote, 2);
            tlpEmergency.Dock = DockStyle.Top;
            tlpEmergency.Margin = new Padding(0);
            tlpEmergency.Name = "tlpEmergency";
            tlpEmergency.RowCount = 5;
            tlpEmergency.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEmergency.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpEmergency.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEmergency.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpEmergency.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpEmergency.TabIndex = 8;
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
            txtEmergencyName.MaxLength = 120;
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
            // lblEmergencyNote
            // 
            lblEmergencyNote.AutoSize = true;
            lblEmergencyNote.BackColor = Color.Transparent;
            lblEmergencyNote.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmergencyNote.ForeColor = Color.FromArgb(91, 107, 124);
            lblEmergencyNote.Margin = new Padding(0, 0, 0, 4);
            lblEmergencyNote.Name = "lblEmergencyNote";
            lblEmergencyNote.TabIndex = 6;
            lblEmergencyNote.Text = "Fill all three fields or leave all empty.";
            // 
            // lblStep5
            // 
            lblStep5.AutoSize = true;
            lblStep5.BackColor = Color.Transparent;
            lblStep5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep5.ForeColor = Color.FromArgb(18, 122, 140);
            lblStep5.Margin = new Padding(0, 4, 0, 14);
            lblStep5.Name = "lblStep5";
            lblStep5.UseMnemonic = false;
            lblStep5.TabIndex = 9;
            lblStep5.Text = "5  ·  DISCOUNT CARD & COVERAGE";
            // 
            // tlpBenefits
            // 
            tlpBenefits.AutoSize = true;
            tlpBenefits.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpBenefits.BackColor = Color.Transparent;
            tlpBenefits.ColumnCount = 2;
            tlpBenefits.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBenefits.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBenefits.Controls.Add(lblDiscountCard, 0, 0);
            tlpBenefits.Controls.Add(cboDiscountCard, 0, 1);
            tlpBenefits.Controls.Add(lblDiscountCardNo, 1, 0);
            tlpBenefits.Controls.Add(txtDiscountCardNo, 1, 1);
            tlpBenefits.Controls.Add(lblCoverage, 0, 2);
            tlpBenefits.Controls.Add(cboCoverage, 0, 3);
            tlpBenefits.Controls.Add(lblCoverageNo, 1, 2);
            tlpBenefits.Controls.Add(txtCoverageNo, 1, 3);
            tlpBenefits.Controls.Add(lblHmoProvider, 0, 4);
            tlpBenefits.SetColumnSpan(lblHmoProvider, 2);
            tlpBenefits.Controls.Add(cboHmoProvider, 0, 5);
            tlpBenefits.SetColumnSpan(cboHmoProvider, 2);
            tlpBenefits.Controls.Add(lblBenefitsNote, 0, 6);
            tlpBenefits.SetColumnSpan(lblBenefitsNote, 2);
            tlpBenefits.Dock = DockStyle.Top;
            tlpBenefits.Margin = new Padding(0);
            tlpBenefits.Name = "tlpBenefits";
            tlpBenefits.RowCount = 7;
            tlpBenefits.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBenefits.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpBenefits.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBenefits.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpBenefits.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBenefits.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBenefits.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBenefits.TabIndex = 10;
            // 
            // lblDiscountCard
            // 
            lblDiscountCard.AutoSize = true;
            lblDiscountCard.BackColor = Color.Transparent;
            lblDiscountCard.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiscountCard.ForeColor = Color.FromArgb(20, 34, 51);
            lblDiscountCard.Margin = new Padding(0, 0, 6, 6);
            lblDiscountCard.Name = "lblDiscountCard";
            lblDiscountCard.TabIndex = 0;
            lblDiscountCard.Text = "Discount card";
            // 
            // cboDiscountCard
            // 
            cboDiscountCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboDiscountCard.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDiscountCard.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboDiscountCard.FormattingEnabled = true;
            cboDiscountCard.Margin = new Padding(0, 0, 6, 14);
            cboDiscountCard.Name = "cboDiscountCard";
            cboDiscountCard.Size = new Size(200, 32);
            cboDiscountCard.TabIndex = 1;
            // 
            // lblDiscountCardNo
            // 
            lblDiscountCardNo.AutoSize = true;
            lblDiscountCardNo.BackColor = Color.Transparent;
            lblDiscountCardNo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiscountCardNo.ForeColor = Color.FromArgb(20, 34, 51);
            lblDiscountCardNo.Margin = new Padding(6, 0, 0, 6);
            lblDiscountCardNo.Name = "lblDiscountCardNo";
            lblDiscountCardNo.TabIndex = 2;
            lblDiscountCardNo.Text = "ID number";
            // 
            // txtDiscountCardNo
            // 
            txtDiscountCardNo.AutoSize = false;
            txtDiscountCardNo.BackColor = Color.White;
            txtDiscountCardNo.BorderStyle = BorderStyle.FixedSingle;
            txtDiscountCardNo.Dock = DockStyle.Fill;
            txtDiscountCardNo.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDiscountCardNo.ForeColor = Color.FromArgb(20, 34, 51);
            txtDiscountCardNo.Margin = new Padding(6, 0, 0, 14);
            txtDiscountCardNo.MaxLength = 30;
            txtDiscountCardNo.Name = "txtDiscountCardNo";
            txtDiscountCardNo.Size = new Size(200, 40);
            txtDiscountCardNo.TabIndex = 3;
            // 
            // lblCoverage
            // 
            lblCoverage.AutoSize = true;
            lblCoverage.BackColor = Color.Transparent;
            lblCoverage.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCoverage.ForeColor = Color.FromArgb(20, 34, 51);
            lblCoverage.Margin = new Padding(0, 0, 6, 6);
            lblCoverage.Name = "lblCoverage";
            lblCoverage.TabIndex = 4;
            lblCoverage.Text = "PhilHealth / HMO";
            // 
            // cboCoverage
            // 
            cboCoverage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboCoverage.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCoverage.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboCoverage.FormattingEnabled = true;
            cboCoverage.Margin = new Padding(0, 0, 6, 14);
            cboCoverage.Name = "cboCoverage";
            cboCoverage.Size = new Size(200, 32);
            cboCoverage.TabIndex = 5;
            // 
            // lblCoverageNo
            // 
            lblCoverageNo.AutoSize = true;
            lblCoverageNo.BackColor = Color.Transparent;
            lblCoverageNo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCoverageNo.ForeColor = Color.FromArgb(20, 34, 51);
            lblCoverageNo.Margin = new Padding(6, 0, 0, 6);
            lblCoverageNo.Name = "lblCoverageNo";
            lblCoverageNo.TabIndex = 6;
            lblCoverageNo.Text = "Member no.";
            // 
            // txtCoverageNo
            // 
            txtCoverageNo.AutoSize = false;
            txtCoverageNo.BackColor = Color.White;
            txtCoverageNo.BorderStyle = BorderStyle.FixedSingle;
            txtCoverageNo.Dock = DockStyle.Fill;
            txtCoverageNo.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCoverageNo.ForeColor = Color.FromArgb(20, 34, 51);
            txtCoverageNo.Margin = new Padding(6, 0, 0, 14);
            txtCoverageNo.MaxLength = 30;
            txtCoverageNo.Name = "txtCoverageNo";
            txtCoverageNo.Size = new Size(200, 40);
            txtCoverageNo.TabIndex = 7;
            // 
            // lblHmoProvider
            // 
            lblHmoProvider.AutoSize = true;
            lblHmoProvider.BackColor = Color.Transparent;
            lblHmoProvider.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHmoProvider.ForeColor = Color.FromArgb(20, 34, 51);
            lblHmoProvider.Margin = new Padding(0, 0, 0, 6);
            lblHmoProvider.Name = "lblHmoProvider";
            lblHmoProvider.TabIndex = 8;
            lblHmoProvider.Text = "HMO company";
            lblHmoProvider.Visible = false;
            // 
            // cboHmoProvider
            // 
            cboHmoProvider.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboHmoProvider.DropDownStyle = ComboBoxStyle.DropDown;
            cboHmoProvider.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboHmoProvider.FormattingEnabled = true;
            cboHmoProvider.Margin = new Padding(0, 0, 0, 14);
            cboHmoProvider.Name = "cboHmoProvider";
            cboHmoProvider.Size = new Size(200, 32);
            cboHmoProvider.TabIndex = 9;
            cboHmoProvider.Visible = false;
            // 
            // lblBenefitsNote
            // 
            lblBenefitsNote.AutoSize = true;
            lblBenefitsNote.BackColor = Color.Transparent;
            lblBenefitsNote.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBenefitsNote.ForeColor = Color.FromArgb(91, 107, 124);
            lblBenefitsNote.Margin = new Padding(0, 0, 0, 4);
            lblBenefitsNote.Name = "lblBenefitsNote";
            lblBenefitsNote.TabIndex = 10;
            lblBenefitsNote.Text = "Senior Citizen / PWD: 20% off and VAT-exempt. PhilHealth / HMO coverage is deducted on the bill. Itemized Billing applies these automatically.";
            lblBenefitsNote.MaximumSize = new Size(520, 0);
            // 
            // capBenefits
            // 
            capBenefits.AutoSize = true;
            capBenefits.BackColor = Color.Transparent;
            capBenefits.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capBenefits.ForeColor = Color.FromArgb(91, 107, 124);
            capBenefits.Margin = new Padding(0, 7, 8, 7);
            capBenefits.Name = "capBenefits";
            capBenefits.TabIndex = 24;
            capBenefits.Text = "Discount / coverage";
            // 
            // lblCardBenefits
            // 
            lblCardBenefits.AutoSize = true;
            lblCardBenefits.BackColor = Color.Transparent;
            lblCardBenefits.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardBenefits.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardBenefits.Margin = new Padding(0, 6, 8, 6);
            lblCardBenefits.Name = "lblCardBenefits";
            lblCardBenefits.TabIndex = 25;
            lblCardBenefits.Text = "-";
            lblCardBenefits.UseMnemonic = false;
            // 
            // tlpButtons
            // 
            tlpButtons.BackColor = Color.Transparent;
            tlpButtons.ColumnCount = 2;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpButtons.Controls.Add(btnSave, 0, 0);
            tlpButtons.Controls.Add(btnClear, 1, 0);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Margin = new Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.RowCount = 1;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpButtons.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(18, 122, 140);
            btnSave.Dock = DockStyle.Fill;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Margin = new Padding(0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 44);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save Patient";
            btnSave.UseMnemonic = false;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.White;
            btnClear.Dock = DockStyle.Fill;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClear.ForeColor = Color.FromArgb(20, 34, 51);
            btnClear.Margin = new Padding(10, 0, 0, 0);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(120, 44);
            btnClear.TabIndex = 1;
            btnClear.Text = "Clear Form";
            btnClear.UseMnemonic = false;
            btnClear.UseVisualStyleBackColor = false;
            // 
            // tlpRight
            // 
            tlpRight.BackColor = Color.Transparent;
            tlpRight.ColumnCount = 1;
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRight.Controls.Add(pnlPatientsCard, 0, 0);
            tlpRight.Controls.Add(pnlProfileOuter, 0, 1);
            tlpRight.Dock = DockStyle.Fill;
            tlpRight.Margin = new Padding(0);
            tlpRight.Name = "tlpRight";
            tlpRight.RowCount = 2;
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            tlpRight.TabIndex = 1;
            // 
            // pnlPatientsCard
            // 
            pnlPatientsCard.BackColor = Color.White;
            pnlPatientsCard.Controls.Add(dgvPatients);
            pnlPatientsCard.Controls.Add(pnlPatientsHeader);
            pnlPatientsCard.Dock = DockStyle.Fill;
            pnlPatientsCard.Margin = new Padding(0, 0, 0, 20);
            pnlPatientsCard.Name = "pnlPatientsCard";
            pnlPatientsCard.Padding = new Padding(1, 6, 1, 8);
            pnlPatientsCard.TabIndex = 0;
            // 
            // dgvPatients
            // 
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.AllowUserToDeleteRows = false;
            dgvPatients.AllowUserToResizeRows = false;
            dgvPatients.BackgroundColor = Color.White;
            dgvPatients.BorderStyle = BorderStyle.None;
            dgvPatients.Dock = DockStyle.Fill;
            dgvPatients.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvPatients.Margin = new Padding(0);
            dgvPatients.MultiSelect = false;
            dgvPatients.Name = "dgvPatients";
            dgvPatients.ReadOnly = true;
            dgvPatients.RowHeadersVisible = false;
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.TabIndex = 0;
            // 
            // pnlPatientsHeader
            // 
            pnlPatientsHeader.BackColor = Color.White;
            pnlPatientsHeader.Controls.Add(lblPatientsTitle);
            pnlPatientsHeader.Dock = DockStyle.Top;
            pnlPatientsHeader.Margin = new Padding(0);
            pnlPatientsHeader.Name = "pnlPatientsHeader";
            pnlPatientsHeader.Padding = new Padding(0);
            pnlPatientsHeader.Size = new Size(600, 46);
            pnlPatientsHeader.TabIndex = 1;
            // 
            // lblPatientsTitle
            // 
            lblPatientsTitle.AutoEllipsis = true;
            lblPatientsTitle.BackColor = Color.Transparent;
            lblPatientsTitle.Dock = DockStyle.Fill;
            lblPatientsTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPatientsTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblPatientsTitle.Margin = new Padding(0);
            lblPatientsTitle.Name = "lblPatientsTitle";
            lblPatientsTitle.Padding = new Padding(17, 0, 0, 0);
            lblPatientsTitle.TabIndex = 0;
            lblPatientsTitle.Text = "Registered patients";
            lblPatientsTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblPatientsTitle.UseMnemonic = false;
            // 
            // pnlProfileOuter
            // 
            pnlProfileOuter.BackColor = Color.White;
            pnlProfileOuter.Controls.Add(pnlProfileCard);
            pnlProfileOuter.Controls.Add(pnlProfileHeader);
            pnlProfileOuter.Dock = DockStyle.Fill;
            pnlProfileOuter.Margin = new Padding(0);
            pnlProfileOuter.Name = "pnlProfileOuter";
            pnlProfileOuter.Padding = new Padding(1, 6, 1, 8);
            pnlProfileOuter.TabIndex = 1;
            // 
            // pnlProfileCard
            // 
            pnlProfileCard.AutoScroll = true;
            pnlProfileCard.BackColor = Color.White;
            pnlProfileCard.Controls.Add(lblCardEmpty);
            pnlProfileCard.Controls.Add(tlpCardBody);
            pnlProfileCard.Controls.Add(pnlCardHeader);
            pnlProfileCard.Dock = DockStyle.Fill;
            pnlProfileCard.Margin = new Padding(0);
            pnlProfileCard.Name = "pnlProfileCard";
            pnlProfileCard.Padding = new Padding(18, 4, 18, 12);
            pnlProfileCard.TabIndex = 0;
            // 
            // lblCardEmpty
            // 
            lblCardEmpty.BackColor = Color.Transparent;
            lblCardEmpty.Dock = DockStyle.Fill;
            lblCardEmpty.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardEmpty.ForeColor = Color.FromArgb(91, 107, 124);
            lblCardEmpty.Margin = new Padding(0);
            lblCardEmpty.Name = "lblCardEmpty";
            lblCardEmpty.TabIndex = 0;
            lblCardEmpty.Text = "Select a patient in the list to preview the profile card.";
            lblCardEmpty.TextAlign = ContentAlignment.MiddleCenter;
            lblCardEmpty.Visible = false;
            // 
            // tlpCardBody
            // 
            tlpCardBody.AutoSize = true;
            tlpCardBody.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpCardBody.BackColor = Color.White;
            tlpCardBody.ColumnCount = 4;
            tlpCardBody.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 116F));
            tlpCardBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCardBody.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
            tlpCardBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpCardBody.Controls.Add(capName, 0, 0);
            tlpCardBody.Controls.Add(lblCardName, 1, 0);
            tlpCardBody.SetColumnSpan(lblCardName, 3);
            tlpCardBody.Controls.Add(capDob, 0, 1);
            tlpCardBody.Controls.Add(lblCardDob, 1, 1);
            tlpCardBody.Controls.Add(capAge, 2, 1);
            tlpCardBody.Controls.Add(lblCardAge, 3, 1);
            tlpCardBody.Controls.Add(capGender, 0, 2);
            tlpCardBody.Controls.Add(lblCardGender, 1, 2);
            tlpCardBody.Controls.Add(capBlood, 2, 2);
            tlpCardBody.Controls.Add(lblCardBlood, 3, 2);
            tlpCardBody.Controls.Add(capPhone, 0, 3);
            tlpCardBody.Controls.Add(lblCardPhone, 1, 3);
            tlpCardBody.Controls.Add(capEmail, 2, 3);
            tlpCardBody.Controls.Add(lblCardEmail, 3, 3);
            tlpCardBody.Controls.Add(capAddress, 0, 4);
            tlpCardBody.Controls.Add(lblCardAddress, 1, 4);
            tlpCardBody.SetColumnSpan(lblCardAddress, 3);
            tlpCardBody.Controls.Add(capAllergies, 0, 5);
            tlpCardBody.Controls.Add(lblCardAllergies, 1, 5);
            tlpCardBody.SetColumnSpan(lblCardAllergies, 3);
            tlpCardBody.Controls.Add(capHistory, 0, 6);
            tlpCardBody.Controls.Add(lblCardHistory, 1, 6);
            tlpCardBody.SetColumnSpan(lblCardHistory, 3);
            tlpCardBody.Controls.Add(capEmergency, 0, 7);
            tlpCardBody.Controls.Add(lblCardEmergency, 1, 7);
            tlpCardBody.SetColumnSpan(lblCardEmergency, 3);
            tlpCardBody.Controls.Add(capBenefits, 0, 8);
            tlpCardBody.Controls.Add(lblCardBenefits, 1, 8);
            tlpCardBody.SetColumnSpan(lblCardBenefits, 3);
            tlpCardBody.Controls.Add(capRegistered, 0, 9);
            tlpCardBody.Controls.Add(lblCardRegistered, 1, 9);
            tlpCardBody.SetColumnSpan(lblCardRegistered, 3);
            tlpCardBody.Dock = DockStyle.Top;
            tlpCardBody.Margin = new Padding(0, 12, 0, 0);
            tlpCardBody.Name = "tlpCardBody";
            tlpCardBody.RowCount = 10;
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.TabIndex = 1;
            tlpCardBody.Visible = false;
            // 
            // capName
            // 
            capName.AutoSize = true;
            capName.BackColor = Color.Transparent;
            capName.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capName.ForeColor = Color.FromArgb(91, 107, 124);
            capName.Margin = new Padding(0, 7, 8, 7);
            capName.Name = "capName";
            capName.TabIndex = 0;
            capName.Text = "Full name";
            // 
            // lblCardName
            // 
            lblCardName.AutoSize = true;
            lblCardName.BackColor = Color.Transparent;
            lblCardName.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardName.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardName.Margin = new Padding(0, 6, 8, 6);
            lblCardName.Name = "lblCardName";
            lblCardName.TabIndex = 1;
            lblCardName.Text = "-";
            lblCardName.UseMnemonic = false;
            // 
            // capDob
            // 
            capDob.AutoSize = true;
            capDob.BackColor = Color.Transparent;
            capDob.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capDob.ForeColor = Color.FromArgb(91, 107, 124);
            capDob.Margin = new Padding(0, 7, 8, 7);
            capDob.Name = "capDob";
            capDob.TabIndex = 2;
            capDob.Text = "Date of birth";
            // 
            // lblCardDob
            // 
            lblCardDob.AutoSize = true;
            lblCardDob.BackColor = Color.Transparent;
            lblCardDob.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardDob.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardDob.Margin = new Padding(0, 6, 8, 6);
            lblCardDob.Name = "lblCardDob";
            lblCardDob.TabIndex = 3;
            lblCardDob.Text = "-";
            lblCardDob.UseMnemonic = false;
            // 
            // capAge
            // 
            capAge.AutoSize = true;
            capAge.BackColor = Color.Transparent;
            capAge.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capAge.ForeColor = Color.FromArgb(91, 107, 124);
            capAge.Margin = new Padding(0, 7, 8, 7);
            capAge.Name = "capAge";
            capAge.TabIndex = 4;
            capAge.Text = "Age";
            // 
            // lblCardAge
            // 
            lblCardAge.AutoSize = true;
            lblCardAge.BackColor = Color.Transparent;
            lblCardAge.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardAge.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardAge.Margin = new Padding(0, 6, 8, 6);
            lblCardAge.Name = "lblCardAge";
            lblCardAge.TabIndex = 5;
            lblCardAge.Text = "-";
            lblCardAge.UseMnemonic = false;
            // 
            // capGender
            // 
            capGender.AutoSize = true;
            capGender.BackColor = Color.Transparent;
            capGender.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capGender.ForeColor = Color.FromArgb(91, 107, 124);
            capGender.Margin = new Padding(0, 7, 8, 7);
            capGender.Name = "capGender";
            capGender.TabIndex = 6;
            capGender.Text = "Gender";
            // 
            // lblCardGender
            // 
            lblCardGender.AutoSize = true;
            lblCardGender.BackColor = Color.Transparent;
            lblCardGender.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardGender.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardGender.Margin = new Padding(0, 6, 8, 6);
            lblCardGender.Name = "lblCardGender";
            lblCardGender.TabIndex = 7;
            lblCardGender.Text = "-";
            lblCardGender.UseMnemonic = false;
            // 
            // capBlood
            // 
            capBlood.AutoSize = true;
            capBlood.BackColor = Color.Transparent;
            capBlood.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capBlood.ForeColor = Color.FromArgb(91, 107, 124);
            capBlood.Margin = new Padding(0, 7, 8, 7);
            capBlood.Name = "capBlood";
            capBlood.TabIndex = 8;
            capBlood.Text = "Blood type";
            // 
            // lblCardBlood
            // 
            lblCardBlood.AutoSize = true;
            lblCardBlood.BackColor = Color.Transparent;
            lblCardBlood.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardBlood.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardBlood.Margin = new Padding(0, 6, 8, 6);
            lblCardBlood.Name = "lblCardBlood";
            lblCardBlood.TabIndex = 9;
            lblCardBlood.Text = "-";
            lblCardBlood.UseMnemonic = false;
            // 
            // capPhone
            // 
            capPhone.AutoSize = true;
            capPhone.BackColor = Color.Transparent;
            capPhone.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capPhone.ForeColor = Color.FromArgb(91, 107, 124);
            capPhone.Margin = new Padding(0, 7, 8, 7);
            capPhone.Name = "capPhone";
            capPhone.TabIndex = 10;
            capPhone.Text = "Phone";
            // 
            // lblCardPhone
            // 
            lblCardPhone.AutoSize = true;
            lblCardPhone.BackColor = Color.Transparent;
            lblCardPhone.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardPhone.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardPhone.Margin = new Padding(0, 6, 8, 6);
            lblCardPhone.Name = "lblCardPhone";
            lblCardPhone.TabIndex = 11;
            lblCardPhone.Text = "-";
            lblCardPhone.UseMnemonic = false;
            // 
            // capEmail
            // 
            capEmail.AutoSize = true;
            capEmail.BackColor = Color.Transparent;
            capEmail.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capEmail.ForeColor = Color.FromArgb(91, 107, 124);
            capEmail.Margin = new Padding(0, 7, 8, 7);
            capEmail.Name = "capEmail";
            capEmail.TabIndex = 12;
            capEmail.Text = "Email";
            // 
            // lblCardEmail
            // 
            lblCardEmail.AutoSize = true;
            lblCardEmail.BackColor = Color.Transparent;
            lblCardEmail.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardEmail.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardEmail.Margin = new Padding(0, 6, 8, 6);
            lblCardEmail.Name = "lblCardEmail";
            lblCardEmail.TabIndex = 13;
            lblCardEmail.Text = "-";
            lblCardEmail.UseMnemonic = false;
            // 
            // capAddress
            // 
            capAddress.AutoSize = true;
            capAddress.BackColor = Color.Transparent;
            capAddress.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capAddress.ForeColor = Color.FromArgb(91, 107, 124);
            capAddress.Margin = new Padding(0, 7, 8, 7);
            capAddress.Name = "capAddress";
            capAddress.TabIndex = 14;
            capAddress.Text = "Address";
            // 
            // lblCardAddress
            // 
            lblCardAddress.AutoSize = true;
            lblCardAddress.BackColor = Color.Transparent;
            lblCardAddress.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardAddress.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardAddress.Margin = new Padding(0, 6, 8, 6);
            lblCardAddress.Name = "lblCardAddress";
            lblCardAddress.TabIndex = 15;
            lblCardAddress.Text = "-";
            lblCardAddress.UseMnemonic = false;
            // 
            // capAllergies
            // 
            capAllergies.AutoSize = true;
            capAllergies.BackColor = Color.Transparent;
            capAllergies.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capAllergies.ForeColor = Color.FromArgb(91, 107, 124);
            capAllergies.Margin = new Padding(0, 7, 8, 7);
            capAllergies.Name = "capAllergies";
            capAllergies.TabIndex = 16;
            capAllergies.Text = "Allergies";
            // 
            // lblCardAllergies
            // 
            lblCardAllergies.AutoSize = true;
            lblCardAllergies.BackColor = Color.Transparent;
            lblCardAllergies.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardAllergies.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardAllergies.Margin = new Padding(0, 6, 8, 6);
            lblCardAllergies.Name = "lblCardAllergies";
            lblCardAllergies.TabIndex = 17;
            lblCardAllergies.Text = "-";
            lblCardAllergies.UseMnemonic = false;
            // 
            // capHistory
            // 
            capHistory.AutoSize = true;
            capHistory.BackColor = Color.Transparent;
            capHistory.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capHistory.ForeColor = Color.FromArgb(91, 107, 124);
            capHistory.Margin = new Padding(0, 7, 8, 7);
            capHistory.Name = "capHistory";
            capHistory.TabIndex = 18;
            capHistory.Text = "Medical history";
            // 
            // lblCardHistory
            // 
            lblCardHistory.AutoSize = true;
            lblCardHistory.BackColor = Color.Transparent;
            lblCardHistory.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardHistory.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardHistory.Margin = new Padding(0, 6, 8, 6);
            lblCardHistory.Name = "lblCardHistory";
            lblCardHistory.TabIndex = 19;
            lblCardHistory.Text = "-";
            lblCardHistory.UseMnemonic = false;
            // 
            // capEmergency
            // 
            capEmergency.AutoSize = true;
            capEmergency.BackColor = Color.Transparent;
            capEmergency.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capEmergency.ForeColor = Color.FromArgb(91, 107, 124);
            capEmergency.Margin = new Padding(0, 7, 8, 7);
            capEmergency.Name = "capEmergency";
            capEmergency.TabIndex = 20;
            capEmergency.Text = "Emergency contact";
            // 
            // lblCardEmergency
            // 
            lblCardEmergency.AutoSize = true;
            lblCardEmergency.BackColor = Color.Transparent;
            lblCardEmergency.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardEmergency.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardEmergency.Margin = new Padding(0, 6, 8, 6);
            lblCardEmergency.Name = "lblCardEmergency";
            lblCardEmergency.TabIndex = 21;
            lblCardEmergency.Text = "-";
            lblCardEmergency.UseMnemonic = false;
            // 
            // capRegistered
            // 
            capRegistered.AutoSize = true;
            capRegistered.BackColor = Color.Transparent;
            capRegistered.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            capRegistered.ForeColor = Color.FromArgb(91, 107, 124);
            capRegistered.Margin = new Padding(0, 7, 8, 7);
            capRegistered.Name = "capRegistered";
            capRegistered.TabIndex = 22;
            capRegistered.Text = "Registered";
            // 
            // lblCardRegistered
            // 
            lblCardRegistered.AutoSize = true;
            lblCardRegistered.BackColor = Color.Transparent;
            lblCardRegistered.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardRegistered.ForeColor = Color.FromArgb(20, 34, 51);
            lblCardRegistered.Margin = new Padding(0, 6, 8, 6);
            lblCardRegistered.Name = "lblCardRegistered";
            lblCardRegistered.TabIndex = 23;
            lblCardRegistered.Text = "-";
            lblCardRegistered.UseMnemonic = false;
            // 
            // pnlCardHeader
            // 
            pnlCardHeader.BackColor = Color.FromArgb(27, 43, 63);
            pnlCardHeader.Controls.Add(lblCardCode);
            pnlCardHeader.Controls.Add(lblCardTitle);
            pnlCardHeader.Controls.Add(lblCardHospital);
            pnlCardHeader.Dock = DockStyle.Top;
            pnlCardHeader.Margin = new Padding(0);
            pnlCardHeader.Name = "pnlCardHeader";
            pnlCardHeader.Padding = new Padding(0);
            pnlCardHeader.Size = new Size(560, 100);
            pnlCardHeader.TabIndex = 2;
            pnlCardHeader.Visible = false;
            // 
            // lblCardCode
            // 
            lblCardCode.AutoSize = true;
            lblCardCode.BackColor = Color.Transparent;
            lblCardCode.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCardCode.ForeColor = Color.White;
            lblCardCode.Location = new Point(18, 52);
            lblCardCode.Margin = new Padding(0);
            lblCardCode.Name = "lblCardCode";
            lblCardCode.TabIndex = 0;
            lblCardCode.Text = "PAT-00000000-000";
            lblCardCode.UseMnemonic = false;
            // 
            // lblCardTitle
            // 
            lblCardTitle.AutoSize = true;
            lblCardTitle.BackColor = Color.Transparent;
            lblCardTitle.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCardTitle.ForeColor = Color.FromArgb(185, 203, 214);
            lblCardTitle.Location = new Point(18, 32);
            lblCardTitle.Margin = new Padding(0);
            lblCardTitle.Name = "lblCardTitle";
            lblCardTitle.TabIndex = 1;
            lblCardTitle.Text = "PATIENT PROFILE CARD";
            // 
            // lblCardHospital
            // 
            lblCardHospital.AutoSize = true;
            lblCardHospital.BackColor = Color.Transparent;
            lblCardHospital.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCardHospital.ForeColor = Color.FromArgb(127, 211, 224);
            lblCardHospital.Location = new Point(18, 12);
            lblCardHospital.Margin = new Padding(0);
            lblCardHospital.Name = "lblCardHospital";
            lblCardHospital.TabIndex = 2;
            lblCardHospital.Text = "HOSPITAL MANAGEMENT SYSTEM";
            // 
            // pnlProfileHeader
            // 
            pnlProfileHeader.BackColor = Color.White;
            pnlProfileHeader.Controls.Add(lblProfileTitle);
            pnlProfileHeader.Dock = DockStyle.Top;
            pnlProfileHeader.Margin = new Padding(0);
            pnlProfileHeader.Name = "pnlProfileHeader";
            pnlProfileHeader.Padding = new Padding(0);
            pnlProfileHeader.Size = new Size(600, 46);
            pnlProfileHeader.TabIndex = 1;
            // 
            // lblProfileTitle
            // 
            lblProfileTitle.AutoEllipsis = true;
            lblProfileTitle.BackColor = Color.Transparent;
            lblProfileTitle.Dock = DockStyle.Fill;
            lblProfileTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProfileTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblProfileTitle.Margin = new Padding(0);
            lblProfileTitle.Name = "lblProfileTitle";
            lblProfileTitle.Padding = new Padding(17, 0, 0, 0);
            lblProfileTitle.TabIndex = 0;
            lblProfileTitle.Text = "Patient profile card";
            lblProfileTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblProfileTitle.UseMnemonic = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // PatientRegistrationForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Patient Registration";
            Controls.Add(tlpRoot);
            Name = "PatientRegistrationForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            tlpBenefits.ResumeLayout(false);
            tlpBenefits.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            pnlEntryCard.ResumeLayout(false);
            pnlEntryCard.PerformLayout();
            tlpEntryCard.ResumeLayout(false);
            tlpEntryCard.PerformLayout();
            pnlEntryScroll.ResumeLayout(false);
            pnlEntryScroll.PerformLayout();
            tlpForm.ResumeLayout(false);
            tlpForm.PerformLayout();
            tlpPersonal.ResumeLayout(false);
            tlpPersonal.PerformLayout();
            tlpBirth.ResumeLayout(false);
            tlpBirth.PerformLayout();
            tlpContact.ResumeLayout(false);
            tlpContact.PerformLayout();
            tlpPhone.ResumeLayout(false);
            tlpPhone.PerformLayout();
            tlpMedical.ResumeLayout(false);
            tlpMedical.PerformLayout();
            tlpEmergency.ResumeLayout(false);
            tlpEmergency.PerformLayout();
            tlpEmergencyPhone.ResumeLayout(false);
            tlpEmergencyPhone.PerformLayout();
            tlpButtons.ResumeLayout(false);
            tlpButtons.PerformLayout();
            tlpRight.ResumeLayout(false);
            tlpRight.PerformLayout();
            pnlPatientsCard.ResumeLayout(false);
            pnlPatientsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
            pnlPatientsHeader.ResumeLayout(false);
            pnlPatientsHeader.PerformLayout();
            pnlProfileOuter.ResumeLayout(false);
            pnlProfileOuter.PerformLayout();
            pnlProfileCard.ResumeLayout(false);
            pnlProfileCard.PerformLayout();
            tlpCardBody.ResumeLayout(false);
            tlpCardBody.PerformLayout();
            pnlCardHeader.ResumeLayout(false);
            pnlCardHeader.PerformLayout();
            pnlProfileHeader.ResumeLayout(false);
            pnlProfileHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblRequiredHint;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlEntryCard;
        private System.Windows.Forms.TableLayoutPanel tlpEntryCard;
        private System.Windows.Forms.Panel pnlEntryScroll;
        private System.Windows.Forms.TableLayoutPanel tlpForm;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.TableLayoutPanel tlpPersonal;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblMiddleName;
        private System.Windows.Forms.TextBox txtMiddleName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TableLayoutPanel tlpBirth;
        private System.Windows.Forms.Label lblDateOfBirth;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.ComboBox cboGender;
        private System.Windows.Forms.Label lblBloodType;
        private System.Windows.Forms.ComboBox cboBloodType;
        private System.Windows.Forms.Label lblStep2;
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
        private System.Windows.Forms.Label lblPostalCode;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.Label lblStep3;
        private System.Windows.Forms.TableLayoutPanel tlpMedical;
        private System.Windows.Forms.Label lblAllergies;
        private System.Windows.Forms.TextBox txtAllergies;
        private System.Windows.Forms.Label lblMedicalHistory;
        private System.Windows.Forms.TextBox txtMedicalHistory;
        private System.Windows.Forms.Label lblStep4;
        private System.Windows.Forms.TableLayoutPanel tlpEmergency;
        private System.Windows.Forms.Label lblEmergencyName;
        private System.Windows.Forms.TextBox txtEmergencyName;
        private System.Windows.Forms.Label lblRelationship;
        private System.Windows.Forms.ComboBox cboRelationship;
        private System.Windows.Forms.Label lblEmergencyPhone;
        private System.Windows.Forms.TableLayoutPanel tlpEmergencyPhone;
        private System.Windows.Forms.TextBox txtEmergencyPhonePrefix;
        private System.Windows.Forms.TextBox txtEmergencyPhone;
        private System.Windows.Forms.Label lblEmergencyNote;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.Panel pnlPatientsCard;
        private System.Windows.Forms.DataGridView dgvPatients;
        private System.Windows.Forms.Panel pnlPatientsHeader;
        private System.Windows.Forms.Label lblPatientsTitle;
        private System.Windows.Forms.Panel pnlProfileOuter;
        private System.Windows.Forms.Panel pnlProfileCard;
        private System.Windows.Forms.Label lblCardEmpty;
        private System.Windows.Forms.TableLayoutPanel tlpCardBody;
        private System.Windows.Forms.Label capName;
        private System.Windows.Forms.Label lblCardName;
        private System.Windows.Forms.Label capDob;
        private System.Windows.Forms.Label lblCardDob;
        private System.Windows.Forms.Label capAge;
        private System.Windows.Forms.Label lblCardAge;
        private System.Windows.Forms.Label capGender;
        private System.Windows.Forms.Label lblCardGender;
        private System.Windows.Forms.Label capBlood;
        private System.Windows.Forms.Label lblCardBlood;
        private System.Windows.Forms.Label capPhone;
        private System.Windows.Forms.Label lblCardPhone;
        private System.Windows.Forms.Label capEmail;
        private System.Windows.Forms.Label lblCardEmail;
        private System.Windows.Forms.Label capAddress;
        private System.Windows.Forms.Label lblCardAddress;
        private System.Windows.Forms.Label capAllergies;
        private System.Windows.Forms.Label lblCardAllergies;
        private System.Windows.Forms.Label capHistory;
        private System.Windows.Forms.Label lblCardHistory;
        private System.Windows.Forms.Label capEmergency;
        private System.Windows.Forms.Label lblCardEmergency;
        private System.Windows.Forms.Label capRegistered;
        private System.Windows.Forms.Label lblCardRegistered;
        private System.Windows.Forms.Panel pnlCardHeader;
        private System.Windows.Forms.Label lblCardCode;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Label lblCardHospital;
        private System.Windows.Forms.Panel pnlProfileHeader;
        private System.Windows.Forms.Label lblProfileTitle;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.Label lblStep5;
        private System.Windows.Forms.TableLayoutPanel tlpBenefits;
        private System.Windows.Forms.Label lblDiscountCard;
        private System.Windows.Forms.ComboBox cboDiscountCard;
        private System.Windows.Forms.Label lblDiscountCardNo;
        private System.Windows.Forms.TextBox txtDiscountCardNo;
        private System.Windows.Forms.Label lblCoverage;
        private System.Windows.Forms.ComboBox cboCoverage;
        private System.Windows.Forms.Label lblCoverageNo;
        private System.Windows.Forms.TextBox txtCoverageNo;
        private System.Windows.Forms.Label lblHmoProvider;
        private System.Windows.Forms.ComboBox cboHmoProvider;
        private System.Windows.Forms.Label lblBenefitsNote;
        private System.Windows.Forms.Label capBenefits;
        private System.Windows.Forms.Label lblCardBenefits;
    }
}
