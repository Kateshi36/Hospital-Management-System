namespace HospitalManagementSystem.Views.MP07_Prescription
{
    partial class PrescriptionForm
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

        // Redesign: page title + date line, New / Issued toggle with the status on the right; (new) consultation
        // and details cards on the left, medicine-line card, lines table card and Save / Clear on the right;
        // (issued) one table card with search in the header and Preview in the footer.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblDate = new Label();
            lblTitle = new Label();
            tlpMode = new TableLayoutPanel();
            btnModeNew = new Button();
            btnModeIssued = new Button();
            lblStatus = new Label();
            pnlBody = new Panel();
            pnlIssued = new Panel();
            pnlIssuedCard = new Panel();
            dgvIssued = new DataGridView();
            pnlIssuedFooter = new Panel();
            lblIssuedCount = new Label();
            flowIssuedActions = new FlowLayoutPanel();
            btnPreview = new Button();
            pnlIssuedHeader = new Panel();
            lblIssuedTitle = new Label();
            flowIssuedFilter = new FlowLayoutPanel();
            txtIssuedSearch = new TextBox();
            btnIssuedSearch = new Button();
            btnIssuedAll = new Button();
            pnlNew = new Panel();
            tlpNew = new TableLayoutPanel();
            tlpNewLeft = new TableLayoutPanel();
            pnlConsultationCard = new Panel();
            dgvConsultations = new DataGridView();
            pnlSelectedBar = new Panel();
            lblSelected = new Label();
            pnlConsultationHeader = new Panel();
            lblConsultationTitle = new Label();
            flowConsultFilter = new FlowLayoutPanel();
            txtConsultSearch = new TextBox();
            btnConsultSearch = new Button();
            pnlDetailsCard = new Panel();
            tlpHeader = new TableLayoutPanel();
            lblDetailsStep = new Label();
            tlpHeaderFields = new TableLayoutPanel();
            lblInstructions = new Label();
            txtInstructions = new TextBox();
            lblValid = new Label();
            dtpValid = new DateTimePicker();
            lblIssuedInfo = new Label();
            tlpNewRight = new TableLayoutPanel();
            pnlMedicineCard = new Panel();
            tlpMedicine = new TableLayoutPanel();
            lblMedicineStep = new Label();
            tlpMedicineA = new TableLayoutPanel();
            lblMedicine = new Label();
            cboMedicine = new ComboBox();
            lblDosage = new Label();
            txtDosage = new TextBox();
            tlpMedicineB = new TableLayoutPanel();
            lblFrequency = new Label();
            txtFrequency = new TextBox();
            lblDuration = new Label();
            txtDuration = new TextBox();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            lblLineInstructions = new Label();
            txtLineInstructions = new TextBox();
            lblStockHint = new Label();
            tlpLineButtons = new TableLayoutPanel();
            btnAddLine = new Button();
            btnCancelEdit = new Button();
            pnlItemsCard = new Panel();
            dgvItems = new DataGridView();
            pnlItemsFooter = new Panel();
            lblTotals = new Label();
            flowItemButtons = new FlowLayoutPanel();
            btnEditLine = new Button();
            btnRemoveLine = new Button();
            pnlItemsHeader = new Panel();
            lblItemsTitle = new Label();
            tlpNewButtons = new TableLayoutPanel();
            btnSave = new Button();
            btnClear = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            tlpMode.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlIssued.SuspendLayout();
            pnlIssuedCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIssued).BeginInit();
            pnlIssuedFooter.SuspendLayout();
            flowIssuedActions.SuspendLayout();
            pnlIssuedHeader.SuspendLayout();
            flowIssuedFilter.SuspendLayout();
            pnlNew.SuspendLayout();
            tlpNew.SuspendLayout();
            tlpNewLeft.SuspendLayout();
            pnlConsultationCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConsultations).BeginInit();
            pnlSelectedBar.SuspendLayout();
            pnlConsultationHeader.SuspendLayout();
            flowConsultFilter.SuspendLayout();
            pnlDetailsCard.SuspendLayout();
            tlpHeader.SuspendLayout();
            tlpHeaderFields.SuspendLayout();
            tlpNewRight.SuspendLayout();
            pnlMedicineCard.SuspendLayout();
            tlpMedicine.SuspendLayout();
            tlpMedicineA.SuspendLayout();
            tlpMedicineB.SuspendLayout();
            tlpLineButtons.SuspendLayout();
            pnlItemsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            pnlItemsFooter.SuspendLayout();
            flowItemButtons.SuspendLayout();
            pnlItemsHeader.SuspendLayout();
            tlpNewButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlTitle, 0, 0);
            tlpRoot.Controls.Add(tlpMode, 0, 1);
            tlpRoot.Controls.Add(pnlBody, 0, 2);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 3;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.TabIndex = 0;
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Transparent;
            pnlTitle.Controls.Add(lblDate);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Dock = DockStyle.Fill;
            pnlTitle.Margin = new Padding(0, 0, 0, 18);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Padding = new Padding(0);
            pnlTitle.TabIndex = 0;
            // 
            // lblDate
            // 
            lblDate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDate.AutoEllipsis = true;
            lblDate.BackColor = Color.Transparent;
            lblDate.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDate.ForeColor = Color.FromArgb(91, 107, 124);
            lblDate.Location = new Point(-2, 34);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(900, 22);
            lblDate.TabIndex = 0;
            lblDate.Text = "Write a prescription for a recent consultation.";
            lblDate.UseMnemonic = false;
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
            lblTitle.Text = "Prescription Builder";
            lblTitle.UseMnemonic = false;
            // 
            // tlpMode
            // 
            tlpMode.BackColor = Color.Transparent;
            tlpMode.ColumnCount = 3;
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMode.Controls.Add(btnModeNew, 0, 0);
            tlpMode.Controls.Add(btnModeIssued, 1, 0);
            tlpMode.Controls.Add(lblStatus, 2, 0);
            tlpMode.Dock = DockStyle.Fill;
            tlpMode.Margin = new Padding(0, 0, 0, 18);
            tlpMode.Name = "tlpMode";
            tlpMode.RowCount = 1;
            tlpMode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMode.TabIndex = 1;
            // 
            // btnModeNew
            // 
            btnModeNew.BackColor = Color.FromArgb(18, 122, 140);
            btnModeNew.FlatStyle = FlatStyle.Flat;
            btnModeNew.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeNew.ForeColor = Color.White;
            btnModeNew.Margin = new Padding(0, 0, 8, 0);
            btnModeNew.Name = "btnModeNew";
            btnModeNew.Size = new Size(180, 38);
            btnModeNew.TabIndex = 0;
            btnModeNew.Text = "New Prescription";
            btnModeNew.UseMnemonic = false;
            btnModeNew.UseVisualStyleBackColor = false;
            // 
            // btnModeIssued
            // 
            btnModeIssued.BackColor = Color.White;
            btnModeIssued.FlatStyle = FlatStyle.Flat;
            btnModeIssued.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeIssued.ForeColor = Color.FromArgb(20, 34, 51);
            btnModeIssued.Margin = new Padding(0);
            btnModeIssued.Name = "btnModeIssued";
            btnModeIssued.Size = new Size(190, 38);
            btnModeIssued.TabIndex = 1;
            btnModeIssued.Text = "Issued Prescriptions";
            btnModeIssued.UseMnemonic = false;
            btnModeIssued.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoEllipsis = true;
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(91, 107, 124);
            lblStatus.Margin = new Padding(0);
            lblStatus.Name = "lblStatus";
            lblStatus.TabIndex = 2;
            lblStatus.Text = "";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            lblStatus.UseMnemonic = false;
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.Transparent;
            pnlBody.Controls.Add(pnlIssued);
            pnlBody.Controls.Add(pnlNew);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Margin = new Padding(0);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(0);
            pnlBody.TabIndex = 2;
            // 
            // pnlIssued
            // 
            pnlIssued.BackColor = Color.Transparent;
            pnlIssued.Controls.Add(pnlIssuedCard);
            pnlIssued.Dock = DockStyle.Fill;
            pnlIssued.Margin = new Padding(0);
            pnlIssued.Name = "pnlIssued";
            pnlIssued.Padding = new Padding(0);
            pnlIssued.TabIndex = 0;
            pnlIssued.Visible = false;
            // 
            // pnlIssuedCard
            // 
            pnlIssuedCard.BackColor = Color.White;
            pnlIssuedCard.Controls.Add(dgvIssued);
            pnlIssuedCard.Controls.Add(pnlIssuedFooter);
            pnlIssuedCard.Controls.Add(pnlIssuedHeader);
            pnlIssuedCard.Dock = DockStyle.Fill;
            pnlIssuedCard.Margin = new Padding(0);
            pnlIssuedCard.Name = "pnlIssuedCard";
            pnlIssuedCard.Padding = new Padding(1, 6, 1, 1);
            pnlIssuedCard.TabIndex = 0;
            // 
            // dgvIssued
            // 
            dgvIssued.AllowUserToAddRows = false;
            dgvIssued.AllowUserToDeleteRows = false;
            dgvIssued.AllowUserToResizeRows = false;
            dgvIssued.BackgroundColor = Color.White;
            dgvIssued.BorderStyle = BorderStyle.None;
            dgvIssued.Dock = DockStyle.Fill;
            dgvIssued.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvIssued.Margin = new Padding(0);
            dgvIssued.MultiSelect = false;
            dgvIssued.Name = "dgvIssued";
            dgvIssued.ReadOnly = true;
            dgvIssued.RowHeadersVisible = false;
            dgvIssued.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIssued.TabIndex = 0;
            // 
            // pnlIssuedFooter
            // 
            pnlIssuedFooter.BackColor = Color.White;
            pnlIssuedFooter.Controls.Add(lblIssuedCount);
            pnlIssuedFooter.Controls.Add(flowIssuedActions);
            pnlIssuedFooter.Dock = DockStyle.Bottom;
            pnlIssuedFooter.Margin = new Padding(0);
            pnlIssuedFooter.Name = "pnlIssuedFooter";
            pnlIssuedFooter.Padding = new Padding(0);
            pnlIssuedFooter.Size = new Size(600, 56);
            pnlIssuedFooter.TabIndex = 1;
            // 
            // lblIssuedCount
            // 
            lblIssuedCount.AutoEllipsis = true;
            lblIssuedCount.BackColor = Color.Transparent;
            lblIssuedCount.Dock = DockStyle.Fill;
            lblIssuedCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblIssuedCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblIssuedCount.Name = "lblIssuedCount";
            lblIssuedCount.Padding = new Padding(18, 0, 8, 0);
            lblIssuedCount.TabIndex = 0;
            lblIssuedCount.TextAlign = ContentAlignment.MiddleLeft;
            lblIssuedCount.UseMnemonic = false;
            // 
            // flowIssuedActions
            // 
            flowIssuedActions.AutoSize = true;
            flowIssuedActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowIssuedActions.BackColor = Color.Transparent;
            flowIssuedActions.Controls.Add(btnPreview);
            flowIssuedActions.Dock = DockStyle.Right;
            flowIssuedActions.Margin = new Padding(0);
            flowIssuedActions.Name = "flowIssuedActions";
            flowIssuedActions.Padding = new Padding(0, 9, 18, 0);
            flowIssuedActions.TabIndex = 1;
            flowIssuedActions.WrapContents = false;
            // 
            // btnPreview
            // 
            btnPreview.BackColor = Color.FromArgb(18, 122, 140);
            btnPreview.Enabled = false;
            btnPreview.FlatStyle = FlatStyle.Flat;
            btnPreview.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPreview.ForeColor = Color.White;
            btnPreview.Margin = new Padding(10, 0, 0, 0);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(170, 38);
            btnPreview.TabIndex = 0;
            btnPreview.Text = "Preview / Print View";
            btnPreview.UseMnemonic = false;
            btnPreview.UseVisualStyleBackColor = false;
            // 
            // pnlIssuedHeader
            // 
            pnlIssuedHeader.BackColor = Color.White;
            pnlIssuedHeader.Controls.Add(lblIssuedTitle);
            pnlIssuedHeader.Controls.Add(flowIssuedFilter);
            pnlIssuedHeader.Dock = DockStyle.Top;
            pnlIssuedHeader.Margin = new Padding(0);
            pnlIssuedHeader.Name = "pnlIssuedHeader";
            pnlIssuedHeader.Padding = new Padding(0);
            pnlIssuedHeader.Size = new Size(600, 46);
            pnlIssuedHeader.TabIndex = 2;
            // 
            // lblIssuedTitle
            // 
            lblIssuedTitle.AutoEllipsis = true;
            lblIssuedTitle.BackColor = Color.Transparent;
            lblIssuedTitle.Dock = DockStyle.Fill;
            lblIssuedTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIssuedTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblIssuedTitle.Margin = new Padding(0);
            lblIssuedTitle.Name = "lblIssuedTitle";
            lblIssuedTitle.Padding = new Padding(17, 0, 0, 0);
            lblIssuedTitle.TabIndex = 0;
            lblIssuedTitle.Text = "Issued prescriptions";
            lblIssuedTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblIssuedTitle.UseMnemonic = false;
            // 
            // flowIssuedFilter
            // 
            flowIssuedFilter.AutoSize = true;
            flowIssuedFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowIssuedFilter.BackColor = Color.White;
            flowIssuedFilter.Controls.Add(txtIssuedSearch);
            flowIssuedFilter.Controls.Add(btnIssuedSearch);
            flowIssuedFilter.Controls.Add(btnIssuedAll);
            flowIssuedFilter.Dock = DockStyle.Right;
            flowIssuedFilter.Margin = new Padding(0);
            flowIssuedFilter.Name = "flowIssuedFilter";
            flowIssuedFilter.Padding = new Padding(0, 5, 18, 0);
            flowIssuedFilter.TabIndex = 1;
            flowIssuedFilter.WrapContents = false;
            // 
            // txtIssuedSearch
            // 
            txtIssuedSearch.AutoSize = false;
            txtIssuedSearch.BackColor = Color.White;
            txtIssuedSearch.BorderStyle = BorderStyle.FixedSingle;
            txtIssuedSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtIssuedSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtIssuedSearch.Margin = new Padding(0);
            txtIssuedSearch.MaxLength = 60;
            txtIssuedSearch.Name = "txtIssuedSearch";
            txtIssuedSearch.PlaceholderText = "Name or patient code";
            txtIssuedSearch.Size = new Size(220, 36);
            txtIssuedSearch.TabIndex = 0;
            // 
            // btnIssuedSearch
            // 
            btnIssuedSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnIssuedSearch.FlatStyle = FlatStyle.Flat;
            btnIssuedSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnIssuedSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnIssuedSearch.Margin = new Padding(10, 0, 0, 0);
            btnIssuedSearch.Name = "btnIssuedSearch";
            btnIssuedSearch.Size = new Size(86, 36);
            btnIssuedSearch.TabIndex = 1;
            btnIssuedSearch.Text = "Search";
            btnIssuedSearch.UseMnemonic = false;
            btnIssuedSearch.UseVisualStyleBackColor = false;
            // 
            // btnIssuedAll
            // 
            btnIssuedAll.BackColor = Color.White;
            btnIssuedAll.FlatStyle = FlatStyle.Flat;
            btnIssuedAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnIssuedAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnIssuedAll.Margin = new Padding(10, 0, 0, 0);
            btnIssuedAll.Name = "btnIssuedAll";
            btnIssuedAll.Size = new Size(96, 36);
            btnIssuedAll.TabIndex = 2;
            btnIssuedAll.Text = "Show All";
            btnIssuedAll.UseMnemonic = false;
            btnIssuedAll.UseVisualStyleBackColor = false;
            // 
            // pnlNew
            // 
            pnlNew.BackColor = Color.Transparent;
            pnlNew.Controls.Add(tlpNew);
            pnlNew.Dock = DockStyle.Fill;
            pnlNew.Margin = new Padding(0);
            pnlNew.Name = "pnlNew";
            pnlNew.Padding = new Padding(0);
            pnlNew.TabIndex = 1;
            // 
            // tlpNew
            // 
            tlpNew.BackColor = Color.Transparent;
            tlpNew.ColumnCount = 2;
            tlpNew.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpNew.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 540F));
            tlpNew.Controls.Add(tlpNewLeft, 0, 0);
            tlpNew.Controls.Add(tlpNewRight, 1, 0);
            tlpNew.Dock = DockStyle.Fill;
            tlpNew.Margin = new Padding(0);
            tlpNew.Name = "tlpNew";
            tlpNew.RowCount = 1;
            tlpNew.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNew.TabIndex = 0;
            // 
            // tlpNewLeft
            // 
            tlpNewLeft.BackColor = Color.Transparent;
            tlpNewLeft.ColumnCount = 1;
            tlpNewLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpNewLeft.Controls.Add(pnlConsultationCard, 0, 0);
            tlpNewLeft.Controls.Add(pnlDetailsCard, 0, 1);
            tlpNewLeft.Dock = DockStyle.Fill;
            tlpNewLeft.Margin = new Padding(0, 0, 20, 0);
            tlpNewLeft.Name = "tlpNewLeft";
            tlpNewLeft.RowCount = 2;
            tlpNewLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNewLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpNewLeft.TabIndex = 0;
            // 
            // pnlConsultationCard
            // 
            pnlConsultationCard.BackColor = Color.White;
            pnlConsultationCard.Controls.Add(dgvConsultations);
            pnlConsultationCard.Controls.Add(pnlSelectedBar);
            pnlConsultationCard.Controls.Add(pnlConsultationHeader);
            pnlConsultationCard.Dock = DockStyle.Fill;
            pnlConsultationCard.Margin = new Padding(0, 0, 0, 20);
            pnlConsultationCard.Name = "pnlConsultationCard";
            pnlConsultationCard.Padding = new Padding(1, 6, 1, 1);
            pnlConsultationCard.TabIndex = 0;
            // 
            // dgvConsultations
            // 
            dgvConsultations.AllowUserToAddRows = false;
            dgvConsultations.AllowUserToDeleteRows = false;
            dgvConsultations.AllowUserToResizeRows = false;
            dgvConsultations.BackgroundColor = Color.White;
            dgvConsultations.BorderStyle = BorderStyle.None;
            dgvConsultations.Dock = DockStyle.Fill;
            dgvConsultations.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvConsultations.Margin = new Padding(0);
            dgvConsultations.MultiSelect = false;
            dgvConsultations.Name = "dgvConsultations";
            dgvConsultations.ReadOnly = true;
            dgvConsultations.RowHeadersVisible = false;
            dgvConsultations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConsultations.TabIndex = 0;
            // 
            // pnlSelectedBar
            // 
            pnlSelectedBar.BackColor = Color.White;
            pnlSelectedBar.Controls.Add(lblSelected);
            pnlSelectedBar.Dock = DockStyle.Bottom;
            pnlSelectedBar.Margin = new Padding(0);
            pnlSelectedBar.Name = "pnlSelectedBar";
            pnlSelectedBar.Padding = new Padding(0);
            pnlSelectedBar.Size = new Size(600, 60);
            pnlSelectedBar.TabIndex = 1;
            // 
            // lblSelected
            // 
            lblSelected.BackColor = Color.Transparent;
            lblSelected.Dock = DockStyle.Fill;
            lblSelected.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSelected.ForeColor = Color.FromArgb(91, 107, 124);
            lblSelected.Margin = new Padding(0);
            lblSelected.Name = "lblSelected";
            lblSelected.Padding = new Padding(18, 0, 18, 0);
            lblSelected.TabIndex = 0;
            lblSelected.Text = "No consultation selected yet";
            lblSelected.TextAlign = ContentAlignment.MiddleLeft;
            lblSelected.UseMnemonic = false;
            // 
            // pnlConsultationHeader
            // 
            pnlConsultationHeader.BackColor = Color.White;
            pnlConsultationHeader.Controls.Add(lblConsultationTitle);
            pnlConsultationHeader.Controls.Add(flowConsultFilter);
            pnlConsultationHeader.Dock = DockStyle.Top;
            pnlConsultationHeader.Margin = new Padding(0);
            pnlConsultationHeader.Name = "pnlConsultationHeader";
            pnlConsultationHeader.Padding = new Padding(0);
            pnlConsultationHeader.Size = new Size(600, 46);
            pnlConsultationHeader.TabIndex = 2;
            // 
            // lblConsultationTitle
            // 
            lblConsultationTitle.AutoEllipsis = true;
            lblConsultationTitle.BackColor = Color.Transparent;
            lblConsultationTitle.Dock = DockStyle.Fill;
            lblConsultationTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblConsultationTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblConsultationTitle.Margin = new Padding(0);
            lblConsultationTitle.Name = "lblConsultationTitle";
            lblConsultationTitle.Padding = new Padding(17, 0, 0, 0);
            lblConsultationTitle.TabIndex = 0;
            lblConsultationTitle.Text = "1  ·  Select the consultation (last 60 days)";
            lblConsultationTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblConsultationTitle.UseMnemonic = false;
            // 
            // flowConsultFilter
            // 
            flowConsultFilter.AutoSize = true;
            flowConsultFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowConsultFilter.BackColor = Color.White;
            flowConsultFilter.Controls.Add(txtConsultSearch);
            flowConsultFilter.Controls.Add(btnConsultSearch);
            flowConsultFilter.Dock = DockStyle.Right;
            flowConsultFilter.Margin = new Padding(0);
            flowConsultFilter.Name = "flowConsultFilter";
            flowConsultFilter.Padding = new Padding(0, 5, 18, 0);
            flowConsultFilter.TabIndex = 1;
            flowConsultFilter.WrapContents = false;
            // 
            // txtConsultSearch
            // 
            txtConsultSearch.AutoSize = false;
            txtConsultSearch.BackColor = Color.White;
            txtConsultSearch.BorderStyle = BorderStyle.FixedSingle;
            txtConsultSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtConsultSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtConsultSearch.Margin = new Padding(0);
            txtConsultSearch.MaxLength = 60;
            txtConsultSearch.Name = "txtConsultSearch";
            txtConsultSearch.PlaceholderText = "Name or patient code";
            txtConsultSearch.Size = new Size(220, 36);
            txtConsultSearch.TabIndex = 0;
            // 
            // btnConsultSearch
            // 
            btnConsultSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnConsultSearch.FlatStyle = FlatStyle.Flat;
            btnConsultSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConsultSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnConsultSearch.Margin = new Padding(10, 0, 0, 0);
            btnConsultSearch.Name = "btnConsultSearch";
            btnConsultSearch.Size = new Size(86, 36);
            btnConsultSearch.TabIndex = 1;
            btnConsultSearch.Text = "Search";
            btnConsultSearch.UseMnemonic = false;
            btnConsultSearch.UseVisualStyleBackColor = false;
            // 
            // pnlDetailsCard
            // 
            pnlDetailsCard.AutoSize = true;
            pnlDetailsCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlDetailsCard.BackColor = Color.White;
            pnlDetailsCard.Controls.Add(tlpHeader);
            pnlDetailsCard.Dock = DockStyle.Fill;
            pnlDetailsCard.Margin = new Padding(0);
            pnlDetailsCard.Name = "pnlDetailsCard";
            pnlDetailsCard.Padding = new Padding(20, 18, 20, 4);
            pnlDetailsCard.TabIndex = 1;
            // 
            // tlpHeader
            // 
            tlpHeader.AutoSize = true;
            tlpHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpHeader.BackColor = Color.Transparent;
            tlpHeader.ColumnCount = 1;
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpHeader.Controls.Add(lblDetailsStep, 0, 0);
            tlpHeader.Controls.Add(tlpHeaderFields, 0, 1);
            tlpHeader.Dock = DockStyle.Top;
            tlpHeader.Margin = new Padding(0);
            tlpHeader.Name = "tlpHeader";
            tlpHeader.RowCount = 2;
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpHeader.TabIndex = 0;
            // 
            // lblDetailsStep
            // 
            lblDetailsStep.AutoSize = true;
            lblDetailsStep.BackColor = Color.Transparent;
            lblDetailsStep.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetailsStep.ForeColor = Color.FromArgb(18, 122, 140);
            lblDetailsStep.Margin = new Padding(0, 0, 0, 14);
            lblDetailsStep.Name = "lblDetailsStep";
            lblDetailsStep.TabIndex = 0;
            lblDetailsStep.Text = "2  ·  PRESCRIPTION DETAILS";
            // 
            // tlpHeaderFields
            // 
            tlpHeaderFields.AutoSize = true;
            tlpHeaderFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpHeaderFields.BackColor = Color.Transparent;
            tlpHeaderFields.ColumnCount = 2;
            tlpHeaderFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpHeaderFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpHeaderFields.Controls.Add(lblInstructions, 0, 0);
            tlpHeaderFields.SetColumnSpan(lblInstructions, 2);
            tlpHeaderFields.Controls.Add(txtInstructions, 0, 1);
            tlpHeaderFields.SetColumnSpan(txtInstructions, 2);
            tlpHeaderFields.Controls.Add(lblValid, 0, 2);
            tlpHeaderFields.Controls.Add(dtpValid, 0, 3);
            tlpHeaderFields.Controls.Add(lblIssuedInfo, 1, 3);
            tlpHeaderFields.Dock = DockStyle.Top;
            tlpHeaderFields.Margin = new Padding(0);
            tlpHeaderFields.Name = "tlpHeaderFields";
            tlpHeaderFields.RowCount = 4;
            tlpHeaderFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpHeaderFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            tlpHeaderFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpHeaderFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpHeaderFields.TabIndex = 1;
            // 
            // lblInstructions
            // 
            lblInstructions.AutoSize = true;
            lblInstructions.BackColor = Color.Transparent;
            lblInstructions.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInstructions.ForeColor = Color.FromArgb(20, 34, 51);
            lblInstructions.Margin = new Padding(0, 0, 0, 6);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.TabIndex = 0;
            lblInstructions.Text = "General instructions (optional)";
            lblInstructions.UseMnemonic = false;
            // 
            // txtInstructions
            // 
            txtInstructions.AutoSize = false;
            txtInstructions.BackColor = Color.White;
            txtInstructions.BorderStyle = BorderStyle.FixedSingle;
            txtInstructions.Dock = DockStyle.Fill;
            txtInstructions.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtInstructions.ForeColor = Color.FromArgb(20, 34, 51);
            txtInstructions.Margin = new Padding(0, 0, 0, 14);
            txtInstructions.MaxLength = 1000;
            txtInstructions.Multiline = true;
            txtInstructions.Name = "txtInstructions";
            txtInstructions.PlaceholderText = "For example: take after meals, drink plenty of fluids";
            txtInstructions.ScrollBars = ScrollBars.Vertical;
            txtInstructions.Size = new Size(200, 64);
            txtInstructions.TabIndex = 1;
            // 
            // lblValid
            // 
            lblValid.AutoSize = true;
            lblValid.BackColor = Color.Transparent;
            lblValid.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValid.ForeColor = Color.FromArgb(20, 34, 51);
            lblValid.Margin = new Padding(0, 0, 6, 6);
            lblValid.Name = "lblValid";
            lblValid.TabIndex = 2;
            lblValid.Text = "Valid until";
            lblValid.UseMnemonic = false;
            // 
            // dtpValid
            // 
            dtpValid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpValid.CustomFormat = "ddd, MMM dd, yyyy";
            dtpValid.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpValid.Format = DateTimePickerFormat.Custom;
            dtpValid.Margin = new Padding(0, 0, 6, 14);
            dtpValid.Name = "dtpValid";
            dtpValid.Size = new Size(200, 31);
            dtpValid.TabIndex = 3;
            // 
            // lblIssuedInfo
            // 
            lblIssuedInfo.AutoSize = true;
            lblIssuedInfo.BackColor = Color.Transparent;
            lblIssuedInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblIssuedInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblIssuedInfo.Margin = new Padding(6, 10, 0, 0);
            lblIssuedInfo.Name = "lblIssuedInfo";
            lblIssuedInfo.TabIndex = 4;
            lblIssuedInfo.Text = "";
            lblIssuedInfo.UseMnemonic = false;
            // 
            // tlpNewRight
            // 
            tlpNewRight.BackColor = Color.Transparent;
            tlpNewRight.ColumnCount = 1;
            tlpNewRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpNewRight.Controls.Add(pnlMedicineCard, 0, 0);
            tlpNewRight.Controls.Add(pnlItemsCard, 0, 1);
            tlpNewRight.Controls.Add(tlpNewButtons, 0, 2);
            tlpNewRight.Dock = DockStyle.Fill;
            tlpNewRight.Margin = new Padding(0);
            tlpNewRight.Name = "tlpNewRight";
            tlpNewRight.RowCount = 3;
            tlpNewRight.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpNewRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNewRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpNewRight.TabIndex = 1;
            // 
            // pnlMedicineCard
            // 
            pnlMedicineCard.AutoSize = true;
            pnlMedicineCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlMedicineCard.BackColor = Color.White;
            pnlMedicineCard.Controls.Add(tlpMedicine);
            pnlMedicineCard.Dock = DockStyle.Fill;
            pnlMedicineCard.Margin = new Padding(0, 0, 0, 20);
            pnlMedicineCard.Name = "pnlMedicineCard";
            pnlMedicineCard.Padding = new Padding(20, 18, 20, 18);
            pnlMedicineCard.TabIndex = 0;
            // 
            // tlpMedicine
            // 
            tlpMedicine.AutoSize = true;
            tlpMedicine.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpMedicine.BackColor = Color.Transparent;
            tlpMedicine.ColumnCount = 1;
            tlpMedicine.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMedicine.Controls.Add(lblMedicineStep, 0, 0);
            tlpMedicine.Controls.Add(tlpMedicineA, 0, 1);
            tlpMedicine.Controls.Add(tlpMedicineB, 0, 2);
            tlpMedicine.Controls.Add(lblStockHint, 0, 3);
            tlpMedicine.Controls.Add(tlpLineButtons, 0, 4);
            tlpMedicine.Dock = DockStyle.Top;
            tlpMedicine.Margin = new Padding(0);
            tlpMedicine.Name = "tlpMedicine";
            tlpMedicine.RowCount = 5;
            tlpMedicine.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedicine.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedicine.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedicine.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedicine.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpMedicine.TabIndex = 0;
            // 
            // lblMedicineStep
            // 
            lblMedicineStep.AutoSize = true;
            lblMedicineStep.BackColor = Color.Transparent;
            lblMedicineStep.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMedicineStep.ForeColor = Color.FromArgb(18, 122, 140);
            lblMedicineStep.Margin = new Padding(0, 0, 0, 14);
            lblMedicineStep.Name = "lblMedicineStep";
            lblMedicineStep.TabIndex = 0;
            lblMedicineStep.Text = "3  ·  ADD A MEDICINE LINE";
            // 
            // tlpMedicineA
            // 
            tlpMedicineA.AutoSize = true;
            tlpMedicineA.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpMedicineA.BackColor = Color.Transparent;
            tlpMedicineA.ColumnCount = 2;
            tlpMedicineA.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            tlpMedicineA.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            tlpMedicineA.Controls.Add(lblMedicine, 0, 0);
            tlpMedicineA.Controls.Add(cboMedicine, 0, 1);
            tlpMedicineA.Controls.Add(lblDosage, 1, 0);
            tlpMedicineA.Controls.Add(txtDosage, 1, 1);
            tlpMedicineA.Dock = DockStyle.Top;
            tlpMedicineA.Margin = new Padding(0);
            tlpMedicineA.Name = "tlpMedicineA";
            tlpMedicineA.RowCount = 2;
            tlpMedicineA.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedicineA.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpMedicineA.TabIndex = 1;
            // 
            // lblMedicine
            // 
            lblMedicine.AutoSize = true;
            lblMedicine.BackColor = Color.Transparent;
            lblMedicine.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMedicine.ForeColor = Color.FromArgb(20, 34, 51);
            lblMedicine.Margin = new Padding(0, 0, 6, 6);
            lblMedicine.Name = "lblMedicine";
            lblMedicine.TabIndex = 0;
            lblMedicine.Text = "Medicine *";
            lblMedicine.UseMnemonic = false;
            // 
            // cboMedicine
            // 
            cboMedicine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboMedicine.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMedicine.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboMedicine.FormattingEnabled = true;
            cboMedicine.Margin = new Padding(0, 0, 6, 14);
            cboMedicine.Name = "cboMedicine";
            cboMedicine.Size = new Size(200, 32);
            cboMedicine.TabIndex = 1;
            // 
            // lblDosage
            // 
            lblDosage.AutoSize = true;
            lblDosage.BackColor = Color.Transparent;
            lblDosage.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDosage.ForeColor = Color.FromArgb(20, 34, 51);
            lblDosage.Margin = new Padding(6, 0, 0, 6);
            lblDosage.Name = "lblDosage";
            lblDosage.TabIndex = 2;
            lblDosage.Text = "Dosage *";
            lblDosage.UseMnemonic = false;
            // 
            // txtDosage
            // 
            txtDosage.AutoSize = false;
            txtDosage.BackColor = Color.White;
            txtDosage.BorderStyle = BorderStyle.FixedSingle;
            txtDosage.Dock = DockStyle.Fill;
            txtDosage.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDosage.ForeColor = Color.FromArgb(20, 34, 51);
            txtDosage.Margin = new Padding(6, 0, 0, 14);
            txtDosage.MaxLength = 50;
            txtDosage.Name = "txtDosage";
            txtDosage.PlaceholderText = "e.g. 500mg";
            txtDosage.Size = new Size(200, 40);
            txtDosage.TabIndex = 3;
            // 
            // tlpMedicineB
            // 
            tlpMedicineB.AutoSize = true;
            tlpMedicineB.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpMedicineB.BackColor = Color.Transparent;
            tlpMedicineB.ColumnCount = 3;
            tlpMedicineB.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpMedicineB.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpMedicineB.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpMedicineB.Controls.Add(lblFrequency, 0, 0);
            tlpMedicineB.Controls.Add(txtFrequency, 0, 1);
            tlpMedicineB.Controls.Add(lblDuration, 1, 0);
            tlpMedicineB.Controls.Add(txtDuration, 1, 1);
            tlpMedicineB.Controls.Add(lblQuantity, 2, 0);
            tlpMedicineB.Controls.Add(txtQuantity, 2, 1);
            tlpMedicineB.Controls.Add(lblLineInstructions, 0, 2);
            tlpMedicineB.SetColumnSpan(lblLineInstructions, 3);
            tlpMedicineB.Controls.Add(txtLineInstructions, 0, 3);
            tlpMedicineB.SetColumnSpan(txtLineInstructions, 3);
            tlpMedicineB.Dock = DockStyle.Top;
            tlpMedicineB.Margin = new Padding(0);
            tlpMedicineB.Name = "tlpMedicineB";
            tlpMedicineB.RowCount = 4;
            tlpMedicineB.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedicineB.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpMedicineB.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedicineB.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpMedicineB.TabIndex = 2;
            // 
            // lblFrequency
            // 
            lblFrequency.AutoSize = true;
            lblFrequency.BackColor = Color.Transparent;
            lblFrequency.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFrequency.ForeColor = Color.FromArgb(20, 34, 51);
            lblFrequency.Margin = new Padding(0, 0, 6, 6);
            lblFrequency.Name = "lblFrequency";
            lblFrequency.TabIndex = 0;
            lblFrequency.Text = "Times per day *";
            lblFrequency.UseMnemonic = false;
            // 
            // txtFrequency
            // 
            txtFrequency.AutoSize = false;
            txtFrequency.BackColor = Color.White;
            txtFrequency.BorderStyle = BorderStyle.FixedSingle;
            txtFrequency.Dock = DockStyle.Fill;
            txtFrequency.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFrequency.ForeColor = Color.FromArgb(20, 34, 51);
            txtFrequency.Margin = new Padding(0, 0, 6, 14);
            txtFrequency.MaxLength = 2;
            txtFrequency.Name = "txtFrequency";
            txtFrequency.PlaceholderText = "1 - 24";
            txtFrequency.Size = new Size(200, 40);
            txtFrequency.TabIndex = 1;
            // 
            // lblDuration
            // 
            lblDuration.AutoSize = true;
            lblDuration.BackColor = Color.Transparent;
            lblDuration.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDuration.ForeColor = Color.FromArgb(20, 34, 51);
            lblDuration.Margin = new Padding(6, 0, 6, 6);
            lblDuration.Name = "lblDuration";
            lblDuration.TabIndex = 2;
            lblDuration.Text = "Duration (days) *";
            lblDuration.UseMnemonic = false;
            // 
            // txtDuration
            // 
            txtDuration.AutoSize = false;
            txtDuration.BackColor = Color.White;
            txtDuration.BorderStyle = BorderStyle.FixedSingle;
            txtDuration.Dock = DockStyle.Fill;
            txtDuration.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDuration.ForeColor = Color.FromArgb(20, 34, 51);
            txtDuration.Margin = new Padding(6, 0, 6, 14);
            txtDuration.MaxLength = 3;
            txtDuration.Name = "txtDuration";
            txtDuration.PlaceholderText = "1 - 365";
            txtDuration.Size = new Size(200, 40);
            txtDuration.TabIndex = 3;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.BackColor = Color.Transparent;
            lblQuantity.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblQuantity.ForeColor = Color.FromArgb(20, 34, 51);
            lblQuantity.Margin = new Padding(6, 0, 0, 6);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.TabIndex = 4;
            lblQuantity.Text = "Quantity (auto)";
            lblQuantity.UseMnemonic = false;
            // 
            // txtQuantity
            // 
            txtQuantity.AutoSize = false;
            txtQuantity.BackColor = Color.White;
            txtQuantity.BorderStyle = BorderStyle.FixedSingle;
            txtQuantity.Dock = DockStyle.Fill;
            txtQuantity.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtQuantity.ForeColor = Color.FromArgb(20, 34, 51);
            txtQuantity.Margin = new Padding(6, 0, 0, 14);
            txtQuantity.MaxLength = 6;
            txtQuantity.Name = "txtQuantity";
            txtQuantity.ReadOnly = true;
            txtQuantity.Size = new Size(200, 40);
            txtQuantity.TabIndex = 5;
            txtQuantity.TabStop = false;
            // 
            // lblLineInstructions
            // 
            lblLineInstructions.AutoSize = true;
            lblLineInstructions.BackColor = Color.Transparent;
            lblLineInstructions.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLineInstructions.ForeColor = Color.FromArgb(20, 34, 51);
            lblLineInstructions.Margin = new Padding(0, 0, 0, 6);
            lblLineInstructions.Name = "lblLineInstructions";
            lblLineInstructions.TabIndex = 6;
            lblLineInstructions.Text = "Instructions";
            lblLineInstructions.UseMnemonic = false;
            // 
            // txtLineInstructions
            // 
            txtLineInstructions.AutoSize = false;
            txtLineInstructions.BackColor = Color.White;
            txtLineInstructions.BorderStyle = BorderStyle.FixedSingle;
            txtLineInstructions.Dock = DockStyle.Fill;
            txtLineInstructions.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLineInstructions.ForeColor = Color.FromArgb(20, 34, 51);
            txtLineInstructions.Margin = new Padding(0, 0, 0, 14);
            txtLineInstructions.MaxLength = 250;
            txtLineInstructions.Name = "txtLineInstructions";
            txtLineInstructions.PlaceholderText = "e.g. After meals";
            txtLineInstructions.Size = new Size(200, 40);
            txtLineInstructions.TabIndex = 7;
            // 
            // lblStockHint
            // 
            lblStockHint.AutoSize = true;
            lblStockHint.BackColor = Color.Transparent;
            lblStockHint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStockHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblStockHint.Margin = new Padding(0, 0, 0, 12);
            lblStockHint.MaximumSize = new Size(470, 0);
            lblStockHint.Name = "lblStockHint";
            lblStockHint.TabIndex = 3;
            lblStockHint.Text = "Choose a medicine to see its stock.";
            lblStockHint.UseMnemonic = false;
            // 
            // tlpLineButtons
            // 
            tlpLineButtons.BackColor = Color.Transparent;
            tlpLineButtons.ColumnCount = 2;
            tlpLineButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tlpLineButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpLineButtons.Controls.Add(btnAddLine, 0, 0);
            tlpLineButtons.Controls.Add(btnCancelEdit, 1, 0);
            tlpLineButtons.Dock = DockStyle.Fill;
            tlpLineButtons.Margin = new Padding(0);
            tlpLineButtons.Name = "tlpLineButtons";
            tlpLineButtons.RowCount = 1;
            tlpLineButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpLineButtons.TabIndex = 4;
            // 
            // btnAddLine
            // 
            btnAddLine.BackColor = Color.FromArgb(226, 242, 246);
            btnAddLine.Dock = DockStyle.Fill;
            btnAddLine.FlatStyle = FlatStyle.Flat;
            btnAddLine.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAddLine.ForeColor = Color.FromArgb(20, 34, 51);
            btnAddLine.Margin = new Padding(0);
            btnAddLine.Name = "btnAddLine";
            btnAddLine.Size = new Size(120, 40);
            btnAddLine.TabIndex = 0;
            btnAddLine.Text = "Add Line";
            btnAddLine.UseMnemonic = false;
            btnAddLine.UseVisualStyleBackColor = false;
            // 
            // btnCancelEdit
            // 
            btnCancelEdit.BackColor = Color.White;
            btnCancelEdit.Dock = DockStyle.Fill;
            btnCancelEdit.FlatStyle = FlatStyle.Flat;
            btnCancelEdit.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancelEdit.ForeColor = Color.FromArgb(20, 34, 51);
            btnCancelEdit.Margin = new Padding(10, 0, 0, 0);
            btnCancelEdit.Name = "btnCancelEdit";
            btnCancelEdit.Size = new Size(120, 40);
            btnCancelEdit.TabIndex = 1;
            btnCancelEdit.Text = "Cancel Edit";
            btnCancelEdit.UseMnemonic = false;
            btnCancelEdit.UseVisualStyleBackColor = false;
            // 
            // pnlItemsCard
            // 
            pnlItemsCard.BackColor = Color.White;
            pnlItemsCard.Controls.Add(dgvItems);
            pnlItemsCard.Controls.Add(pnlItemsFooter);
            pnlItemsCard.Controls.Add(pnlItemsHeader);
            pnlItemsCard.Dock = DockStyle.Fill;
            pnlItemsCard.Margin = new Padding(0, 0, 0, 14);
            pnlItemsCard.Name = "pnlItemsCard";
            pnlItemsCard.Padding = new Padding(1, 6, 1, 1);
            pnlItemsCard.TabIndex = 1;
            // 
            // dgvItems
            // 
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.BackgroundColor = Color.White;
            dgvItems.BorderStyle = BorderStyle.None;
            dgvItems.Dock = DockStyle.Fill;
            dgvItems.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvItems.Margin = new Padding(0);
            dgvItems.MultiSelect = false;
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersVisible = false;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.TabIndex = 0;
            // 
            // pnlItemsFooter
            // 
            pnlItemsFooter.BackColor = Color.White;
            pnlItemsFooter.Controls.Add(lblTotals);
            pnlItemsFooter.Controls.Add(flowItemButtons);
            pnlItemsFooter.Dock = DockStyle.Bottom;
            pnlItemsFooter.Margin = new Padding(0);
            pnlItemsFooter.Name = "pnlItemsFooter";
            pnlItemsFooter.Padding = new Padding(0);
            pnlItemsFooter.Size = new Size(600, 56);
            pnlItemsFooter.TabIndex = 1;
            // 
            // lblTotals
            // 
            lblTotals.AutoEllipsis = true;
            lblTotals.BackColor = Color.Transparent;
            lblTotals.Dock = DockStyle.Fill;
            lblTotals.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotals.ForeColor = Color.FromArgb(91, 107, 124);
            lblTotals.Name = "lblTotals";
            lblTotals.Padding = new Padding(18, 0, 8, 0);
            lblTotals.TabIndex = 0;
            lblTotals.TextAlign = ContentAlignment.MiddleLeft;
            lblTotals.UseMnemonic = false;
            // 
            // flowItemButtons
            // 
            flowItemButtons.AutoSize = true;
            flowItemButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowItemButtons.BackColor = Color.Transparent;
            flowItemButtons.Controls.Add(btnEditLine);
            flowItemButtons.Controls.Add(btnRemoveLine);
            flowItemButtons.Dock = DockStyle.Right;
            flowItemButtons.Margin = new Padding(0);
            flowItemButtons.Name = "flowItemButtons";
            flowItemButtons.Padding = new Padding(0, 10, 18, 0);
            flowItemButtons.TabIndex = 1;
            flowItemButtons.WrapContents = false;
            // 
            // btnEditLine
            // 
            btnEditLine.BackColor = Color.White;
            btnEditLine.Enabled = false;
            btnEditLine.FlatStyle = FlatStyle.Flat;
            btnEditLine.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEditLine.ForeColor = Color.FromArgb(20, 34, 51);
            btnEditLine.Margin = new Padding(10, 0, 0, 0);
            btnEditLine.Name = "btnEditLine";
            btnEditLine.Size = new Size(96, 36);
            btnEditLine.TabIndex = 0;
            btnEditLine.Text = "Edit Line";
            btnEditLine.UseMnemonic = false;
            btnEditLine.UseVisualStyleBackColor = false;
            // 
            // btnRemoveLine
            // 
            btnRemoveLine.BackColor = Color.FromArgb(180, 35, 24);
            btnRemoveLine.Enabled = false;
            btnRemoveLine.FlatStyle = FlatStyle.Flat;
            btnRemoveLine.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRemoveLine.ForeColor = Color.White;
            btnRemoveLine.Margin = new Padding(10, 0, 0, 0);
            btnRemoveLine.Name = "btnRemoveLine";
            btnRemoveLine.Size = new Size(116, 36);
            btnRemoveLine.TabIndex = 1;
            btnRemoveLine.Text = "Remove Line";
            btnRemoveLine.UseMnemonic = false;
            btnRemoveLine.UseVisualStyleBackColor = false;
            // 
            // pnlItemsHeader
            // 
            pnlItemsHeader.BackColor = Color.White;
            pnlItemsHeader.Controls.Add(lblItemsTitle);
            pnlItemsHeader.Dock = DockStyle.Top;
            pnlItemsHeader.Margin = new Padding(0);
            pnlItemsHeader.Name = "pnlItemsHeader";
            pnlItemsHeader.Padding = new Padding(0);
            pnlItemsHeader.Size = new Size(600, 46);
            pnlItemsHeader.TabIndex = 2;
            // 
            // lblItemsTitle
            // 
            lblItemsTitle.AutoEllipsis = true;
            lblItemsTitle.BackColor = Color.Transparent;
            lblItemsTitle.Dock = DockStyle.Fill;
            lblItemsTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblItemsTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblItemsTitle.Margin = new Padding(0);
            lblItemsTitle.Name = "lblItemsTitle";
            lblItemsTitle.Padding = new Padding(17, 0, 0, 0);
            lblItemsTitle.TabIndex = 0;
            lblItemsTitle.Text = "Medicines in this prescription";
            lblItemsTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblItemsTitle.UseMnemonic = false;
            // 
            // tlpNewButtons
            // 
            tlpNewButtons.BackColor = Color.Transparent;
            tlpNewButtons.ColumnCount = 2;
            tlpNewButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpNewButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tlpNewButtons.Controls.Add(btnSave, 0, 0);
            tlpNewButtons.Controls.Add(btnClear, 1, 0);
            tlpNewButtons.Dock = DockStyle.Fill;
            tlpNewButtons.Margin = new Padding(0);
            tlpNewButtons.Name = "tlpNewButtons";
            tlpNewButtons.RowCount = 1;
            tlpNewButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNewButtons.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(18, 122, 140);
            btnSave.Dock = DockStyle.Fill;
            btnSave.Enabled = false;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Margin = new Padding(0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save Prescription";
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
            btnClear.Size = new Size(120, 40);
            btnClear.TabIndex = 1;
            btnClear.Text = "Clear Form";
            btnClear.UseMnemonic = false;
            btnClear.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // PrescriptionForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Prescription Builder";
            Controls.Add(tlpRoot);
            Name = "PrescriptionForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tlpMode.ResumeLayout(false);
            tlpMode.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            pnlIssued.ResumeLayout(false);
            pnlIssued.PerformLayout();
            pnlIssuedCard.ResumeLayout(false);
            pnlIssuedCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIssued).EndInit();
            pnlIssuedFooter.ResumeLayout(false);
            pnlIssuedFooter.PerformLayout();
            flowIssuedActions.ResumeLayout(false);
            flowIssuedActions.PerformLayout();
            pnlIssuedHeader.ResumeLayout(false);
            pnlIssuedHeader.PerformLayout();
            flowIssuedFilter.ResumeLayout(false);
            flowIssuedFilter.PerformLayout();
            pnlNew.ResumeLayout(false);
            pnlNew.PerformLayout();
            tlpNew.ResumeLayout(false);
            tlpNew.PerformLayout();
            tlpNewLeft.ResumeLayout(false);
            tlpNewLeft.PerformLayout();
            pnlConsultationCard.ResumeLayout(false);
            pnlConsultationCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConsultations).EndInit();
            pnlSelectedBar.ResumeLayout(false);
            pnlSelectedBar.PerformLayout();
            pnlConsultationHeader.ResumeLayout(false);
            pnlConsultationHeader.PerformLayout();
            flowConsultFilter.ResumeLayout(false);
            flowConsultFilter.PerformLayout();
            pnlDetailsCard.ResumeLayout(false);
            pnlDetailsCard.PerformLayout();
            tlpHeader.ResumeLayout(false);
            tlpHeader.PerformLayout();
            tlpHeaderFields.ResumeLayout(false);
            tlpHeaderFields.PerformLayout();
            tlpNewRight.ResumeLayout(false);
            tlpNewRight.PerformLayout();
            pnlMedicineCard.ResumeLayout(false);
            pnlMedicineCard.PerformLayout();
            tlpMedicine.ResumeLayout(false);
            tlpMedicine.PerformLayout();
            tlpMedicineA.ResumeLayout(false);
            tlpMedicineA.PerformLayout();
            tlpMedicineB.ResumeLayout(false);
            tlpMedicineB.PerformLayout();
            tlpLineButtons.ResumeLayout(false);
            tlpLineButtons.PerformLayout();
            pnlItemsCard.ResumeLayout(false);
            pnlItemsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            pnlItemsFooter.ResumeLayout(false);
            pnlItemsFooter.PerformLayout();
            flowItemButtons.ResumeLayout(false);
            flowItemButtons.PerformLayout();
            pnlItemsHeader.ResumeLayout(false);
            pnlItemsHeader.PerformLayout();
            tlpNewButtons.ResumeLayout(false);
            tlpNewButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tlpMode;
        private System.Windows.Forms.Button btnModeNew;
        private System.Windows.Forms.Button btnModeIssued;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlIssued;
        private System.Windows.Forms.Panel pnlIssuedCard;
        private System.Windows.Forms.DataGridView dgvIssued;
        private System.Windows.Forms.Panel pnlIssuedFooter;
        private System.Windows.Forms.Label lblIssuedCount;
        private System.Windows.Forms.FlowLayoutPanel flowIssuedActions;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Panel pnlIssuedHeader;
        private System.Windows.Forms.Label lblIssuedTitle;
        private System.Windows.Forms.FlowLayoutPanel flowIssuedFilter;
        private System.Windows.Forms.TextBox txtIssuedSearch;
        private System.Windows.Forms.Button btnIssuedSearch;
        private System.Windows.Forms.Button btnIssuedAll;
        private System.Windows.Forms.Panel pnlNew;
        private System.Windows.Forms.TableLayoutPanel tlpNew;
        private System.Windows.Forms.TableLayoutPanel tlpNewLeft;
        private System.Windows.Forms.Panel pnlConsultationCard;
        private System.Windows.Forms.DataGridView dgvConsultations;
        private System.Windows.Forms.Panel pnlSelectedBar;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Panel pnlConsultationHeader;
        private System.Windows.Forms.Label lblConsultationTitle;
        private System.Windows.Forms.FlowLayoutPanel flowConsultFilter;
        private System.Windows.Forms.TextBox txtConsultSearch;
        private System.Windows.Forms.Button btnConsultSearch;
        private System.Windows.Forms.Panel pnlDetailsCard;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.Label lblDetailsStep;
        private System.Windows.Forms.TableLayoutPanel tlpHeaderFields;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.Label lblValid;
        private System.Windows.Forms.DateTimePicker dtpValid;
        private System.Windows.Forms.Label lblIssuedInfo;
        private System.Windows.Forms.TableLayoutPanel tlpNewRight;
        private System.Windows.Forms.Panel pnlMedicineCard;
        private System.Windows.Forms.TableLayoutPanel tlpMedicine;
        private System.Windows.Forms.Label lblMedicineStep;
        private System.Windows.Forms.TableLayoutPanel tlpMedicineA;
        private System.Windows.Forms.Label lblMedicine;
        private System.Windows.Forms.ComboBox cboMedicine;
        private System.Windows.Forms.Label lblDosage;
        private System.Windows.Forms.TextBox txtDosage;
        private System.Windows.Forms.TableLayoutPanel tlpMedicineB;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.TextBox txtFrequency;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblLineInstructions;
        private System.Windows.Forms.TextBox txtLineInstructions;
        private System.Windows.Forms.Label lblStockHint;
        private System.Windows.Forms.TableLayoutPanel tlpLineButtons;
        private System.Windows.Forms.Button btnAddLine;
        private System.Windows.Forms.Button btnCancelEdit;
        private System.Windows.Forms.Panel pnlItemsCard;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Panel pnlItemsFooter;
        private System.Windows.Forms.Label lblTotals;
        private System.Windows.Forms.FlowLayoutPanel flowItemButtons;
        private System.Windows.Forms.Button btnEditLine;
        private System.Windows.Forms.Button btnRemoveLine;
        private System.Windows.Forms.Panel pnlItemsHeader;
        private System.Windows.Forms.Label lblItemsTitle;
        private System.Windows.Forms.TableLayoutPanel tlpNewButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
