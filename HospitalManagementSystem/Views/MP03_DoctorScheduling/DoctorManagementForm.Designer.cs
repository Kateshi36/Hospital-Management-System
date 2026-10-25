namespace HospitalManagementSystem.Views.MP03_DoctorScheduling
{
    partial class DoctorManagementForm
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

        // Redesign: page title + hint, a 460 px doctor details card (numbered sections, Save / New / Schedule /
        // Delete at the bottom) beside the doctor directory table card with its search box in the header.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblRequiredHint = new Label();
            lblTitle = new Label();
            tlpMain = new TableLayoutPanel();
            pnlDetailsCard = new Panel();
            tlpDetails = new TableLayoutPanel();
            lblMode = new Label();
            pnlDetailsScroll = new Panel();
            tlpFields = new TableLayoutPanel();
            lblStep1 = new Label();
            tlpIdentity = new TableLayoutPanel();
            lblCode = new Label();
            txtCode = new TextBox();
            lblLicense = new Label();
            txtLicense = new TextBox();
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            lblLastName = new Label();
            txtLastName = new TextBox();
            lblStep2 = new Label();
            tlpPractice = new TableLayoutPanel();
            lblSpecialization = new Label();
            cboSpecialization = new ComboBox();
            lblDepartment = new Label();
            cboDepartment = new ComboBox();
            lblEmployment = new Label();
            cboEmployment = new ComboBox();
            lblFee = new Label();
            txtFee = new TextBox();
            lblStep3 = new Label();
            tlpContact = new TableLayoutPanel();
            lblPhone = new Label();
            tlpPhone = new TableLayoutPanel();
            txtPhonePrefix = new TextBox();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            tlpButtons = new TableLayoutPanel();
            btnSave = new Button();
            btnNew = new Button();
            btnSchedule = new Button();
            btnDelete = new Button();
            pnlDoctorsCard = new Panel();
            dgvDoctors = new DataGridView();
            pnlDoctorsHeader = new Panel();
            lblDoctorsTitle = new Label();
            flowFilter = new FlowLayoutPanel();
            txtFilter = new TextBox();
            btnClearFilter = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            tlpMain.SuspendLayout();
            pnlDetailsCard.SuspendLayout();
            tlpDetails.SuspendLayout();
            pnlDetailsScroll.SuspendLayout();
            tlpFields.SuspendLayout();
            tlpIdentity.SuspendLayout();
            tlpPractice.SuspendLayout();
            tlpContact.SuspendLayout();
            tlpPhone.SuspendLayout();
            tlpButtons.SuspendLayout();
            pnlDoctorsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDoctors).BeginInit();
            pnlDoctorsHeader.SuspendLayout();
            flowFilter.SuspendLayout();
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
            lblRequiredHint.Text = "Add or edit a doctor on the left, then set the weekly schedule. Fields marked * are required.";
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
            lblTitle.Text = "Doctor Directory";
            lblTitle.UseMnemonic = false;
            // 
            // tlpMain
            // 
            tlpMain.BackColor = Color.Transparent;
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 480F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(pnlDetailsCard, 0, 0);
            tlpMain.Controls.Add(pnlDoctorsCard, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Margin = new Padding(0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.TabIndex = 1;
            // 
            // pnlDetailsCard
            // 
            pnlDetailsCard.BackColor = Color.White;
            pnlDetailsCard.Controls.Add(tlpDetails);
            pnlDetailsCard.Dock = DockStyle.Fill;
            pnlDetailsCard.Margin = new Padding(0, 0, 20, 0);
            pnlDetailsCard.Name = "pnlDetailsCard";
            pnlDetailsCard.Padding = new Padding(20);
            pnlDetailsCard.TabIndex = 0;
            // 
            // tlpDetails
            // 
            tlpDetails.BackColor = Color.Transparent;
            tlpDetails.ColumnCount = 1;
            tlpDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDetails.Controls.Add(lblMode, 0, 0);
            tlpDetails.Controls.Add(pnlDetailsScroll, 0, 1);
            tlpDetails.Controls.Add(tlpButtons, 0, 2);
            tlpDetails.Dock = DockStyle.Fill;
            tlpDetails.Margin = new Padding(0);
            tlpDetails.Name = "tlpDetails";
            tlpDetails.RowCount = 3;
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDetails.TabIndex = 0;
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.BackColor = Color.Transparent;
            lblMode.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMode.ForeColor = Color.FromArgb(18, 122, 140);
            lblMode.Margin = new Padding(0, 0, 0, 14);
            lblMode.MaximumSize = new Size(420, 0);
            lblMode.Name = "lblMode";
            lblMode.TabIndex = 0;
            lblMode.Text = "New doctor";
            lblMode.UseMnemonic = false;
            // 
            // pnlDetailsScroll
            // 
            pnlDetailsScroll.AutoScroll = true;
            pnlDetailsScroll.BackColor = Color.Transparent;
            pnlDetailsScroll.Controls.Add(tlpFields);
            pnlDetailsScroll.Dock = DockStyle.Fill;
            pnlDetailsScroll.Margin = new Padding(0, 0, 0, 14);
            pnlDetailsScroll.Name = "pnlDetailsScroll";
            pnlDetailsScroll.Padding = new Padding(0, 0, 6, 0);
            pnlDetailsScroll.TabIndex = 1;
            // 
            // tlpFields
            // 
            tlpFields.AutoSize = true;
            tlpFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpFields.BackColor = Color.Transparent;
            tlpFields.ColumnCount = 1;
            tlpFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFields.Controls.Add(lblStep1, 0, 0);
            tlpFields.Controls.Add(tlpIdentity, 0, 1);
            tlpFields.Controls.Add(lblStep2, 0, 2);
            tlpFields.Controls.Add(tlpPractice, 0, 3);
            tlpFields.Controls.Add(lblStep3, 0, 4);
            tlpFields.Controls.Add(tlpContact, 0, 5);
            tlpFields.Dock = DockStyle.Top;
            tlpFields.Margin = new Padding(0);
            tlpFields.Name = "tlpFields";
            tlpFields.RowCount = 6;
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.TabIndex = 0;
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
            lblStep1.Text = "1  ·  IDENTITY";
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
            tlpIdentity.Controls.Add(lblLicense, 1, 0);
            tlpIdentity.Controls.Add(txtLicense, 1, 1);
            tlpIdentity.Controls.Add(lblFirstName, 0, 2);
            tlpIdentity.Controls.Add(txtFirstName, 0, 3);
            tlpIdentity.Controls.Add(lblLastName, 1, 2);
            tlpIdentity.Controls.Add(txtLastName, 1, 3);
            tlpIdentity.Dock = DockStyle.Top;
            tlpIdentity.Margin = new Padding(0);
            tlpIdentity.Name = "tlpIdentity";
            tlpIdentity.RowCount = 4;
            tlpIdentity.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpIdentity.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpIdentity.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpIdentity.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpIdentity.TabIndex = 1;
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
            lblCode.Text = "Doctor ID";
            lblCode.UseMnemonic = false;
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
            txtCode.MaxLength = 10;
            txtCode.Name = "txtCode";
            txtCode.PlaceholderText = "Generated on save";
            txtCode.ReadOnly = true;
            txtCode.Size = new Size(200, 40);
            txtCode.TabIndex = 1;
            txtCode.TabStop = false;
            // 
            // lblLicense
            // 
            lblLicense.AutoSize = true;
            lblLicense.BackColor = Color.Transparent;
            lblLicense.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLicense.ForeColor = Color.FromArgb(20, 34, 51);
            lblLicense.Margin = new Padding(6, 0, 0, 6);
            lblLicense.Name = "lblLicense";
            lblLicense.TabIndex = 2;
            lblLicense.Text = "License number *";
            lblLicense.UseMnemonic = false;
            // 
            // txtLicense
            // 
            txtLicense.AutoSize = false;
            txtLicense.BackColor = Color.White;
            txtLicense.BorderStyle = BorderStyle.FixedSingle;
            txtLicense.Dock = DockStyle.Fill;
            txtLicense.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLicense.ForeColor = Color.FromArgb(20, 34, 51);
            txtLicense.Margin = new Padding(6, 0, 0, 14);
            txtLicense.MaxLength = 30;
            txtLicense.Name = "txtLicense";
            txtLicense.PlaceholderText = "PRC-0000000";
            txtLicense.Size = new Size(200, 40);
            txtLicense.TabIndex = 3;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.BackColor = Color.Transparent;
            lblFirstName.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFirstName.ForeColor = Color.FromArgb(20, 34, 51);
            lblFirstName.Margin = new Padding(0, 0, 6, 6);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.TabIndex = 4;
            lblFirstName.Text = "First name *";
            lblFirstName.UseMnemonic = false;
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
            txtFirstName.PlaceholderText = "e.g. Maria";
            txtFirstName.Size = new Size(200, 40);
            txtFirstName.TabIndex = 5;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.BackColor = Color.Transparent;
            lblLastName.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLastName.ForeColor = Color.FromArgb(20, 34, 51);
            lblLastName.Margin = new Padding(6, 0, 0, 6);
            lblLastName.Name = "lblLastName";
            lblLastName.TabIndex = 6;
            lblLastName.Text = "Last name *";
            lblLastName.UseMnemonic = false;
            // 
            // txtLastName
            // 
            txtLastName.AutoSize = false;
            txtLastName.BackColor = Color.White;
            txtLastName.BorderStyle = BorderStyle.FixedSingle;
            txtLastName.Dock = DockStyle.Fill;
            txtLastName.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLastName.ForeColor = Color.FromArgb(20, 34, 51);
            txtLastName.Margin = new Padding(6, 0, 0, 14);
            txtLastName.MaxLength = 60;
            txtLastName.Name = "txtLastName";
            txtLastName.PlaceholderText = "e.g. Santos";
            txtLastName.Size = new Size(200, 40);
            txtLastName.TabIndex = 7;
            // 
            // lblStep2
            // 
            lblStep2.AutoSize = true;
            lblStep2.BackColor = Color.Transparent;
            lblStep2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep2.ForeColor = Color.FromArgb(18, 122, 140);
            lblStep2.Margin = new Padding(0, 4, 0, 14);
            lblStep2.Name = "lblStep2";
            lblStep2.TabIndex = 2;
            lblStep2.Text = "2  ·  PRACTICE";
            // 
            // tlpPractice
            // 
            tlpPractice.AutoSize = true;
            tlpPractice.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpPractice.BackColor = Color.Transparent;
            tlpPractice.ColumnCount = 2;
            tlpPractice.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPractice.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPractice.Controls.Add(lblSpecialization, 0, 0);
            tlpPractice.Controls.Add(cboSpecialization, 0, 1);
            tlpPractice.Controls.Add(lblDepartment, 1, 0);
            tlpPractice.Controls.Add(cboDepartment, 1, 1);
            tlpPractice.Controls.Add(lblEmployment, 0, 2);
            tlpPractice.Controls.Add(cboEmployment, 0, 3);
            tlpPractice.Controls.Add(lblFee, 1, 2);
            tlpPractice.Controls.Add(txtFee, 1, 3);
            tlpPractice.Dock = DockStyle.Top;
            tlpPractice.Margin = new Padding(0);
            tlpPractice.Name = "tlpPractice";
            tlpPractice.RowCount = 4;
            tlpPractice.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPractice.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpPractice.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPractice.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpPractice.TabIndex = 3;
            // 
            // lblSpecialization
            // 
            lblSpecialization.AutoSize = true;
            lblSpecialization.BackColor = Color.Transparent;
            lblSpecialization.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSpecialization.ForeColor = Color.FromArgb(20, 34, 51);
            lblSpecialization.Margin = new Padding(0, 0, 6, 6);
            lblSpecialization.Name = "lblSpecialization";
            lblSpecialization.TabIndex = 0;
            lblSpecialization.Text = "Specialization *";
            lblSpecialization.UseMnemonic = false;
            // 
            // cboSpecialization
            // 
            cboSpecialization.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboSpecialization.DropDownStyle = ComboBoxStyle.DropDown;
            cboSpecialization.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboSpecialization.FormattingEnabled = true;
            cboSpecialization.Margin = new Padding(0, 0, 6, 14);
            cboSpecialization.Name = "cboSpecialization";
            cboSpecialization.Size = new Size(200, 32);
            cboSpecialization.TabIndex = 1;
            // 
            // lblDepartment
            // 
            lblDepartment.AutoSize = true;
            lblDepartment.BackColor = Color.Transparent;
            lblDepartment.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDepartment.ForeColor = Color.FromArgb(20, 34, 51);
            lblDepartment.Margin = new Padding(6, 0, 0, 6);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.TabIndex = 2;
            lblDepartment.Text = "Department *";
            lblDepartment.UseMnemonic = false;
            // 
            // cboDepartment
            // 
            cboDepartment.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboDepartment.DropDownStyle = ComboBoxStyle.DropDown;
            cboDepartment.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboDepartment.FormattingEnabled = true;
            cboDepartment.Margin = new Padding(6, 0, 0, 14);
            cboDepartment.Name = "cboDepartment";
            cboDepartment.Size = new Size(200, 32);
            cboDepartment.TabIndex = 3;
            // 
            // lblEmployment
            // 
            lblEmployment.AutoSize = true;
            lblEmployment.BackColor = Color.Transparent;
            lblEmployment.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmployment.ForeColor = Color.FromArgb(20, 34, 51);
            lblEmployment.Margin = new Padding(0, 0, 6, 6);
            lblEmployment.Name = "lblEmployment";
            lblEmployment.TabIndex = 4;
            lblEmployment.Text = "Employment type *";
            lblEmployment.UseMnemonic = false;
            // 
            // cboEmployment
            // 
            cboEmployment.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboEmployment.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEmployment.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboEmployment.FormattingEnabled = true;
            cboEmployment.Margin = new Padding(0, 0, 6, 14);
            cboEmployment.Name = "cboEmployment";
            cboEmployment.Size = new Size(200, 32);
            cboEmployment.TabIndex = 5;
            // 
            // lblFee
            // 
            lblFee.AutoSize = true;
            lblFee.BackColor = Color.Transparent;
            lblFee.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFee.ForeColor = Color.FromArgb(20, 34, 51);
            lblFee.Margin = new Padding(6, 0, 0, 6);
            lblFee.Name = "lblFee";
            lblFee.TabIndex = 6;
            lblFee.Text = "Consultation fee (PHP) *";
            lblFee.UseMnemonic = false;
            // 
            // txtFee
            // 
            txtFee.AutoSize = false;
            txtFee.BackColor = Color.White;
            txtFee.BorderStyle = BorderStyle.FixedSingle;
            txtFee.Dock = DockStyle.Fill;
            txtFee.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFee.ForeColor = Color.FromArgb(20, 34, 51);
            txtFee.Margin = new Padding(6, 0, 0, 14);
            txtFee.MaxLength = 11;
            txtFee.Name = "txtFee";
            txtFee.PlaceholderText = "0.00";
            txtFee.Size = new Size(200, 40);
            txtFee.TabIndex = 7;
            txtFee.TextAlign = HorizontalAlignment.Right;
            // 
            // lblStep3
            // 
            lblStep3.AutoSize = true;
            lblStep3.BackColor = Color.Transparent;
            lblStep3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep3.ForeColor = Color.FromArgb(18, 122, 140);
            lblStep3.Margin = new Padding(0, 4, 0, 14);
            lblStep3.Name = "lblStep3";
            lblStep3.TabIndex = 4;
            lblStep3.Text = "3  ·  CONTACT";
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
            tlpContact.Dock = DockStyle.Top;
            tlpContact.Margin = new Padding(0);
            tlpContact.Name = "tlpContact";
            tlpContact.RowCount = 2;
            tlpContact.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpContact.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpContact.TabIndex = 5;
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
            lblPhone.UseMnemonic = false;
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
            lblEmail.UseMnemonic = false;
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
            txtEmail.PlaceholderText = "name@hospital.local";
            txtEmail.Size = new Size(200, 40);
            txtEmail.TabIndex = 3;
            // 
            // tlpButtons
            // 
            tlpButtons.BackColor = Color.Transparent;
            tlpButtons.ColumnCount = 2;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpButtons.Controls.Add(btnSave, 0, 0);
            tlpButtons.Controls.Add(btnNew, 1, 0);
            tlpButtons.Controls.Add(btnSchedule, 0, 1);
            tlpButtons.Controls.Add(btnDelete, 1, 1);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Margin = new Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.RowCount = 2;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpButtons.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(18, 122, 140);
            btnSave.Dock = DockStyle.Fill;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Margin = new Padding(0, 0, 0, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save Doctor";
            btnSave.UseMnemonic = false;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnNew
            // 
            btnNew.BackColor = Color.White;
            btnNew.Dock = DockStyle.Fill;
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNew.ForeColor = Color.FromArgb(20, 34, 51);
            btnNew.Margin = new Padding(10, 0, 0, 10);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(120, 40);
            btnNew.TabIndex = 1;
            btnNew.Text = "New Doctor";
            btnNew.UseMnemonic = false;
            btnNew.UseVisualStyleBackColor = false;
            // 
            // btnSchedule
            // 
            btnSchedule.BackColor = Color.FromArgb(226, 242, 246);
            btnSchedule.Dock = DockStyle.Fill;
            btnSchedule.Enabled = false;
            btnSchedule.FlatStyle = FlatStyle.Flat;
            btnSchedule.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSchedule.ForeColor = Color.FromArgb(20, 34, 51);
            btnSchedule.Margin = new Padding(0);
            btnSchedule.Name = "btnSchedule";
            btnSchedule.Size = new Size(120, 40);
            btnSchedule.TabIndex = 2;
            btnSchedule.Text = "Weekly Schedule...";
            btnSchedule.UseMnemonic = false;
            btnSchedule.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(180, 35, 24);
            btnDelete.Dock = DockStyle.Fill;
            btnDelete.Enabled = false;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.White;
            btnDelete.Margin = new Padding(10, 0, 0, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 40);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete Doctor";
            btnDelete.UseMnemonic = false;
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // pnlDoctorsCard
            // 
            pnlDoctorsCard.BackColor = Color.White;
            pnlDoctorsCard.Controls.Add(dgvDoctors);
            pnlDoctorsCard.Controls.Add(pnlDoctorsHeader);
            pnlDoctorsCard.Dock = DockStyle.Fill;
            pnlDoctorsCard.Margin = new Padding(0);
            pnlDoctorsCard.Name = "pnlDoctorsCard";
            pnlDoctorsCard.Padding = new Padding(1, 6, 1, 8);
            pnlDoctorsCard.TabIndex = 1;
            // 
            // dgvDoctors
            // 
            dgvDoctors.AllowUserToAddRows = false;
            dgvDoctors.AllowUserToDeleteRows = false;
            dgvDoctors.AllowUserToResizeRows = false;
            dgvDoctors.BackgroundColor = Color.White;
            dgvDoctors.BorderStyle = BorderStyle.None;
            dgvDoctors.Dock = DockStyle.Fill;
            dgvDoctors.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvDoctors.Margin = new Padding(0);
            dgvDoctors.MultiSelect = false;
            dgvDoctors.Name = "dgvDoctors";
            dgvDoctors.ReadOnly = true;
            dgvDoctors.RowHeadersVisible = false;
            dgvDoctors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoctors.TabIndex = 0;
            // 
            // pnlDoctorsHeader
            // 
            pnlDoctorsHeader.BackColor = Color.White;
            pnlDoctorsHeader.Controls.Add(lblDoctorsTitle);
            pnlDoctorsHeader.Controls.Add(flowFilter);
            pnlDoctorsHeader.Dock = DockStyle.Top;
            pnlDoctorsHeader.Margin = new Padding(0);
            pnlDoctorsHeader.Name = "pnlDoctorsHeader";
            pnlDoctorsHeader.Padding = new Padding(0);
            pnlDoctorsHeader.Size = new Size(600, 46);
            pnlDoctorsHeader.TabIndex = 1;
            // 
            // lblDoctorsTitle
            // 
            lblDoctorsTitle.AutoEllipsis = true;
            lblDoctorsTitle.BackColor = Color.Transparent;
            lblDoctorsTitle.Dock = DockStyle.Fill;
            lblDoctorsTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDoctorsTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblDoctorsTitle.Margin = new Padding(0);
            lblDoctorsTitle.Name = "lblDoctorsTitle";
            lblDoctorsTitle.Padding = new Padding(17, 0, 0, 0);
            lblDoctorsTitle.TabIndex = 0;
            lblDoctorsTitle.Text = "Doctor directory";
            lblDoctorsTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblDoctorsTitle.UseMnemonic = false;
            // 
            // flowFilter
            // 
            flowFilter.AutoSize = true;
            flowFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowFilter.BackColor = Color.White;
            flowFilter.Controls.Add(txtFilter);
            flowFilter.Controls.Add(btnClearFilter);
            flowFilter.Dock = DockStyle.Right;
            flowFilter.Margin = new Padding(0);
            flowFilter.Name = "flowFilter";
            flowFilter.Padding = new Padding(0, 5, 18, 0);
            flowFilter.TabIndex = 1;
            flowFilter.WrapContents = false;
            // 
            // txtFilter
            // 
            txtFilter.AutoSize = false;
            txtFilter.BackColor = Color.White;
            txtFilter.BorderStyle = BorderStyle.FixedSingle;
            txtFilter.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFilter.ForeColor = Color.FromArgb(20, 34, 51);
            txtFilter.Margin = new Padding(0);
            txtFilter.MaxLength = 60;
            txtFilter.Name = "txtFilter";
            txtFilter.PlaceholderText = "Name, code, license number or specialization";
            txtFilter.Size = new Size(280, 36);
            txtFilter.TabIndex = 0;
            // 
            // btnClearFilter
            // 
            btnClearFilter.BackColor = Color.White;
            btnClearFilter.FlatStyle = FlatStyle.Flat;
            btnClearFilter.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearFilter.ForeColor = Color.FromArgb(20, 34, 51);
            btnClearFilter.Margin = new Padding(10, 0, 0, 0);
            btnClearFilter.Name = "btnClearFilter";
            btnClearFilter.Size = new Size(80, 36);
            btnClearFilter.TabIndex = 1;
            btnClearFilter.Text = "Clear";
            btnClearFilter.UseMnemonic = false;
            btnClearFilter.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // DoctorManagementForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Doctor Directory";
            Controls.Add(tlpRoot);
            Name = "DoctorManagementForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            pnlDetailsCard.ResumeLayout(false);
            pnlDetailsCard.PerformLayout();
            tlpDetails.ResumeLayout(false);
            tlpDetails.PerformLayout();
            pnlDetailsScroll.ResumeLayout(false);
            pnlDetailsScroll.PerformLayout();
            tlpFields.ResumeLayout(false);
            tlpFields.PerformLayout();
            tlpIdentity.ResumeLayout(false);
            tlpIdentity.PerformLayout();
            tlpPractice.ResumeLayout(false);
            tlpPractice.PerformLayout();
            tlpContact.ResumeLayout(false);
            tlpContact.PerformLayout();
            tlpPhone.ResumeLayout(false);
            tlpPhone.PerformLayout();
            tlpButtons.ResumeLayout(false);
            tlpButtons.PerformLayout();
            pnlDoctorsCard.ResumeLayout(false);
            pnlDoctorsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDoctors).EndInit();
            pnlDoctorsHeader.ResumeLayout(false);
            pnlDoctorsHeader.PerformLayout();
            flowFilter.ResumeLayout(false);
            flowFilter.PerformLayout();
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
        private System.Windows.Forms.Panel pnlDetailsCard;
        private System.Windows.Forms.TableLayoutPanel tlpDetails;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Panel pnlDetailsScroll;
        private System.Windows.Forms.TableLayoutPanel tlpFields;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.TableLayoutPanel tlpIdentity;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblLicense;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.TableLayoutPanel tlpPractice;
        private System.Windows.Forms.Label lblSpecialization;
        private System.Windows.Forms.ComboBox cboSpecialization;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.Label lblEmployment;
        private System.Windows.Forms.ComboBox cboEmployment;
        private System.Windows.Forms.Label lblFee;
        private System.Windows.Forms.TextBox txtFee;
        private System.Windows.Forms.Label lblStep3;
        private System.Windows.Forms.TableLayoutPanel tlpContact;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TableLayoutPanel tlpPhone;
        private System.Windows.Forms.TextBox txtPhonePrefix;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pnlDoctorsCard;
        private System.Windows.Forms.DataGridView dgvDoctors;
        private System.Windows.Forms.Panel pnlDoctorsHeader;
        private System.Windows.Forms.Label lblDoctorsTitle;
        private System.Windows.Forms.FlowLayoutPanel flowFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLicense;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpecialization;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmployment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBlocks;
    }
}
