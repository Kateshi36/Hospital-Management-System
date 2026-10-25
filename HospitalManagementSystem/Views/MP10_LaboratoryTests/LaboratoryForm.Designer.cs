namespace HospitalManagementSystem.Views.MP10_Laboratory
{
    partial class LaboratoryForm
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

        // Redesign: page title + date line, Catalog / Entry / History toggle with the status on the right;
        // catalog = test details card + catalog table card, entry = pending orders table card + result entry
        // card, history = results table card above the selected-result card.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblDate = new Label();
            lblTitle = new Label();
            tlpMode = new TableLayoutPanel();
            btnModeCatalog = new Button();
            btnModeEntry = new Button();
            btnModeHistory = new Button();
            lblStatus = new Label();
            pnlBody = new Panel();
            pnlHistory = new Panel();
            tlpHistory = new TableLayoutPanel();
            pnlResultsCard = new Panel();
            dgvResults = new DataGridView();
            pnlResultsFooter = new Panel();
            lblHistCount = new Label();
            lblHistLegend = new Label();
            pnlResultsHeader = new Panel();
            lblResultsTitle = new Label();
            flowHistFilter = new FlowLayoutPanel();
            txtHistSearch = new TextBox();
            btnHistSearch = new Button();
            btnHistAll = new Button();
            chkAbnormalOnly = new CheckBox();
            pnlDetailCard = new Panel();
            pnlDetailBody = new Panel();
            txtDetail = new TextBox();
            pnlDetailHeader = new Panel();
            lblDetailTitle = new Label();
            pnlEntry = new Panel();
            tlpEntry = new TableLayoutPanel();
            pnlOrdersCard = new Panel();
            dgvOrders = new DataGridView();
            pnlOrdersFooter = new Panel();
            lblOrderCount = new Label();
            flowOrdersFooter = new FlowLayoutPanel();
            pnlOrdersHeader = new Panel();
            lblOrdersTitle = new Label();
            flowOrderFilter = new FlowLayoutPanel();
            txtOrderSearch = new TextBox();
            btnOrderSearch = new Button();
            btnOrderAll = new Button();
            pnlEntryCard = new Panel();
            tlpEntryCard = new TableLayoutPanel();
            lblEntryTitle = new Label();
            pnlEntryScroll = new Panel();
            tlpEntryFields = new TableLayoutPanel();
            tlpEntryA = new TableLayoutPanel();
            lblOrderCode = new Label();
            txtOrderCode = new TextBox();
            lblPatient = new Label();
            txtPatient = new TextBox();
            lblTest = new Label();
            txtTest = new TextBox();
            lblRange = new Label();
            txtRange = new TextBox();
            lblValue = new Label();
            txtValue = new TextBox();
            lblFlag = new Label();
            tlpEntryB = new TableLayoutPanel();
            lblTested = new Label();
            dtpTested = new DateTimePicker();
            lblTestedBy = new Label();
            cboTestedBy = new ComboBox();
            lblVerifiedBy = new Label();
            cboVerifiedBy = new ComboBox();
            lblFindings = new Label();
            txtFindings = new TextBox();
            tlpEntryButtons = new TableLayoutPanel();
            btnEntrySave = new Button();
            btnEntryClear = new Button();
            pnlCatalog = new Panel();
            tlpCatalog = new TableLayoutPanel();
            pnlTestCard = new Panel();
            tlpTestCard = new TableLayoutPanel();
            lblTestTitle = new Label();
            pnlTestScroll = new Panel();
            tlpDetailsFields = new TableLayoutPanel();
            tlpDetails = new TableLayoutPanel();
            lblCode = new Label();
            txtCode = new TextBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblName = new Label();
            txtName = new TextBox();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            lblSample = new Label();
            cboSample = new ComboBox();
            lblLow = new Label();
            txtLow = new TextBox();
            lblHigh = new Label();
            txtHigh = new TextBox();
            lblUnit = new Label();
            txtUnit = new TextBox();
            lblTurnaround = new Label();
            txtTurnaround = new TextBox();
            chkVerify = new CheckBox();
            lblRangeHint = new Label();
            tlpTestButtons = new TableLayoutPanel();
            btnTestSave = new Button();
            btnTestNew = new Button();
            btnTestDelete = new Button();
            pnlCatalogCard = new Panel();
            dgvTests = new DataGridView();
            pnlCatalogFooter = new Panel();
            lblCatCount = new Label();
            flowCatalogFooter = new FlowLayoutPanel();
            pnlCatalogHeader = new Panel();
            lblCatalogTitle = new Label();
            flowCatalogFilter = new FlowLayoutPanel();
            txtCatSearch = new TextBox();
            btnCatSearch = new Button();
            btnCatAll = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            tlpMode.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlHistory.SuspendLayout();
            tlpHistory.SuspendLayout();
            pnlResultsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            pnlResultsFooter.SuspendLayout();
            pnlResultsHeader.SuspendLayout();
            flowHistFilter.SuspendLayout();
            pnlDetailCard.SuspendLayout();
            pnlDetailBody.SuspendLayout();
            pnlDetailHeader.SuspendLayout();
            pnlEntry.SuspendLayout();
            tlpEntry.SuspendLayout();
            pnlOrdersCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            pnlOrdersFooter.SuspendLayout();
            flowOrdersFooter.SuspendLayout();
            pnlOrdersHeader.SuspendLayout();
            flowOrderFilter.SuspendLayout();
            pnlEntryCard.SuspendLayout();
            tlpEntryCard.SuspendLayout();
            pnlEntryScroll.SuspendLayout();
            tlpEntryFields.SuspendLayout();
            tlpEntryA.SuspendLayout();
            tlpEntryB.SuspendLayout();
            tlpEntryButtons.SuspendLayout();
            pnlCatalog.SuspendLayout();
            tlpCatalog.SuspendLayout();
            pnlTestCard.SuspendLayout();
            tlpTestCard.SuspendLayout();
            pnlTestScroll.SuspendLayout();
            tlpDetailsFields.SuspendLayout();
            tlpDetails.SuspendLayout();
            tlpTestButtons.SuspendLayout();
            pnlCatalogCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTests).BeginInit();
            pnlCatalogFooter.SuspendLayout();
            flowCatalogFooter.SuspendLayout();
            pnlCatalogHeader.SuspendLayout();
            flowCatalogFilter.SuspendLayout();
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
            lblDate.Text = "Manage the test catalog, enter results and look up past results.";
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
            lblTitle.Text = "Laboratory";
            lblTitle.UseMnemonic = false;
            // 
            // tlpMode
            // 
            tlpMode.BackColor = Color.Transparent;
            tlpMode.ColumnCount = 4;
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMode.Controls.Add(btnModeCatalog, 0, 0);
            tlpMode.Controls.Add(btnModeEntry, 1, 0);
            tlpMode.Controls.Add(btnModeHistory, 2, 0);
            tlpMode.Controls.Add(lblStatus, 3, 0);
            tlpMode.Dock = DockStyle.Fill;
            tlpMode.Margin = new Padding(0, 0, 0, 18);
            tlpMode.Name = "tlpMode";
            tlpMode.RowCount = 1;
            tlpMode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMode.TabIndex = 1;
            // 
            // btnModeCatalog
            // 
            btnModeCatalog.BackColor = Color.White;
            btnModeCatalog.FlatStyle = FlatStyle.Flat;
            btnModeCatalog.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeCatalog.ForeColor = Color.FromArgb(20, 34, 51);
            btnModeCatalog.Margin = new Padding(0, 0, 8, 0);
            btnModeCatalog.Name = "btnModeCatalog";
            btnModeCatalog.Size = new Size(150, 38);
            btnModeCatalog.TabIndex = 0;
            btnModeCatalog.Text = "Test Catalog";
            btnModeCatalog.UseMnemonic = false;
            btnModeCatalog.UseVisualStyleBackColor = false;
            // 
            // btnModeEntry
            // 
            btnModeEntry.BackColor = Color.FromArgb(18, 122, 140);
            btnModeEntry.FlatStyle = FlatStyle.Flat;
            btnModeEntry.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeEntry.ForeColor = Color.White;
            btnModeEntry.Margin = new Padding(0, 0, 8, 0);
            btnModeEntry.Name = "btnModeEntry";
            btnModeEntry.Size = new Size(150, 38);
            btnModeEntry.TabIndex = 1;
            btnModeEntry.Text = "Result Entry";
            btnModeEntry.UseMnemonic = false;
            btnModeEntry.UseVisualStyleBackColor = false;
            // 
            // btnModeHistory
            // 
            btnModeHistory.BackColor = Color.White;
            btnModeHistory.FlatStyle = FlatStyle.Flat;
            btnModeHistory.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeHistory.ForeColor = Color.FromArgb(20, 34, 51);
            btnModeHistory.Margin = new Padding(0);
            btnModeHistory.Name = "btnModeHistory";
            btnModeHistory.Size = new Size(160, 38);
            btnModeHistory.TabIndex = 2;
            btnModeHistory.Text = "Results History";
            btnModeHistory.UseMnemonic = false;
            btnModeHistory.UseVisualStyleBackColor = false;
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
            lblStatus.TabIndex = 3;
            lblStatus.Text = "";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            lblStatus.UseMnemonic = false;
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.Transparent;
            pnlBody.Controls.Add(pnlHistory);
            pnlBody.Controls.Add(pnlEntry);
            pnlBody.Controls.Add(pnlCatalog);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Margin = new Padding(0);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(0);
            pnlBody.TabIndex = 2;
            // 
            // pnlHistory
            // 
            pnlHistory.BackColor = Color.Transparent;
            pnlHistory.Controls.Add(tlpHistory);
            pnlHistory.Dock = DockStyle.Fill;
            pnlHistory.Margin = new Padding(0);
            pnlHistory.Name = "pnlHistory";
            pnlHistory.Padding = new Padding(0);
            pnlHistory.TabIndex = 0;
            pnlHistory.Visible = false;
            // 
            // tlpHistory
            // 
            tlpHistory.BackColor = Color.Transparent;
            tlpHistory.ColumnCount = 1;
            tlpHistory.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpHistory.Controls.Add(pnlResultsCard, 0, 0);
            tlpHistory.Controls.Add(pnlDetailCard, 0, 1);
            tlpHistory.Dock = DockStyle.Fill;
            tlpHistory.Margin = new Padding(0);
            tlpHistory.Name = "tlpHistory";
            tlpHistory.RowCount = 2;
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpHistory.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
            tlpHistory.TabIndex = 0;
            // 
            // pnlResultsCard
            // 
            pnlResultsCard.BackColor = Color.White;
            pnlResultsCard.Controls.Add(dgvResults);
            pnlResultsCard.Controls.Add(pnlResultsFooter);
            pnlResultsCard.Controls.Add(pnlResultsHeader);
            pnlResultsCard.Dock = DockStyle.Fill;
            pnlResultsCard.Margin = new Padding(0, 0, 0, 20);
            pnlResultsCard.Name = "pnlResultsCard";
            pnlResultsCard.Padding = new Padding(1, 6, 1, 1);
            pnlResultsCard.TabIndex = 0;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.AllowUserToResizeRows = false;
            dgvResults.BackgroundColor = Color.White;
            dgvResults.BorderStyle = BorderStyle.None;
            dgvResults.Dock = DockStyle.Fill;
            dgvResults.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvResults.Margin = new Padding(0);
            dgvResults.MultiSelect = false;
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.TabIndex = 0;
            // 
            // pnlResultsFooter
            // 
            pnlResultsFooter.BackColor = Color.White;
            pnlResultsFooter.Controls.Add(lblHistCount);
            pnlResultsFooter.Controls.Add(lblHistLegend);
            pnlResultsFooter.Dock = DockStyle.Bottom;
            pnlResultsFooter.Margin = new Padding(0);
            pnlResultsFooter.Name = "pnlResultsFooter";
            pnlResultsFooter.Padding = new Padding(0);
            pnlResultsFooter.Size = new Size(600, 46);
            pnlResultsFooter.TabIndex = 1;
            // 
            // lblHistCount
            // 
            lblHistCount.AutoEllipsis = true;
            lblHistCount.BackColor = Color.Transparent;
            lblHistCount.Dock = DockStyle.Fill;
            lblHistCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHistCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblHistCount.Name = "lblHistCount";
            lblHistCount.Padding = new Padding(18, 0, 8, 0);
            lblHistCount.TabIndex = 0;
            lblHistCount.Text = "";
            lblHistCount.TextAlign = ContentAlignment.MiddleLeft;
            lblHistCount.UseMnemonic = false;
            // 
            // lblHistLegend
            // 
            lblHistLegend.BackColor = Color.Transparent;
            lblHistLegend.Dock = DockStyle.Right;
            lblHistLegend.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHistLegend.ForeColor = Color.FromArgb(91, 107, 124);
            lblHistLegend.Margin = new Padding(0);
            lblHistLegend.Name = "lblHistLegend";
            lblHistLegend.Padding = new Padding(0, 0, 18, 0);
            lblHistLegend.Size = new Size(320, 46);
            lblHistLegend.TabIndex = 1;
            lblHistLegend.Text = "Abnormal results are shown in red";
            lblHistLegend.TextAlign = ContentAlignment.MiddleRight;
            lblHistLegend.UseMnemonic = false;
            // 
            // pnlResultsHeader
            // 
            pnlResultsHeader.BackColor = Color.White;
            pnlResultsHeader.Controls.Add(lblResultsTitle);
            pnlResultsHeader.Controls.Add(flowHistFilter);
            pnlResultsHeader.Dock = DockStyle.Top;
            pnlResultsHeader.Margin = new Padding(0);
            pnlResultsHeader.Name = "pnlResultsHeader";
            pnlResultsHeader.Padding = new Padding(0);
            pnlResultsHeader.Size = new Size(600, 46);
            pnlResultsHeader.TabIndex = 2;
            // 
            // lblResultsTitle
            // 
            lblResultsTitle.AutoEllipsis = true;
            lblResultsTitle.BackColor = Color.Transparent;
            lblResultsTitle.Dock = DockStyle.Fill;
            lblResultsTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblResultsTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblResultsTitle.Margin = new Padding(0);
            lblResultsTitle.Name = "lblResultsTitle";
            lblResultsTitle.Padding = new Padding(17, 0, 0, 0);
            lblResultsTitle.TabIndex = 0;
            lblResultsTitle.Text = "Results history";
            lblResultsTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblResultsTitle.UseMnemonic = false;
            // 
            // flowHistFilter
            // 
            flowHistFilter.AutoSize = true;
            flowHistFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowHistFilter.BackColor = Color.White;
            flowHistFilter.Controls.Add(txtHistSearch);
            flowHistFilter.Controls.Add(btnHistSearch);
            flowHistFilter.Controls.Add(btnHistAll);
            flowHistFilter.Controls.Add(chkAbnormalOnly);
            flowHistFilter.Dock = DockStyle.Right;
            flowHistFilter.Margin = new Padding(0);
            flowHistFilter.Name = "flowHistFilter";
            flowHistFilter.Padding = new Padding(0, 5, 18, 0);
            flowHistFilter.TabIndex = 1;
            flowHistFilter.WrapContents = false;
            // 
            // txtHistSearch
            // 
            txtHistSearch.AutoSize = false;
            txtHistSearch.BackColor = Color.White;
            txtHistSearch.BorderStyle = BorderStyle.FixedSingle;
            txtHistSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtHistSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtHistSearch.Margin = new Padding(0);
            txtHistSearch.MaxLength = 60;
            txtHistSearch.Name = "txtHistSearch";
            txtHistSearch.PlaceholderText = "Result, order, patient or test";
            txtHistSearch.Size = new Size(220, 36);
            txtHistSearch.TabIndex = 0;
            // 
            // btnHistSearch
            // 
            btnHistSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnHistSearch.FlatStyle = FlatStyle.Flat;
            btnHistSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHistSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnHistSearch.Margin = new Padding(10, 0, 0, 0);
            btnHistSearch.Name = "btnHistSearch";
            btnHistSearch.Size = new Size(86, 36);
            btnHistSearch.TabIndex = 1;
            btnHistSearch.Text = "Search";
            btnHistSearch.UseMnemonic = false;
            btnHistSearch.UseVisualStyleBackColor = false;
            // 
            // btnHistAll
            // 
            btnHistAll.BackColor = Color.White;
            btnHistAll.FlatStyle = FlatStyle.Flat;
            btnHistAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHistAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnHistAll.Margin = new Padding(10, 0, 0, 0);
            btnHistAll.Name = "btnHistAll";
            btnHistAll.Size = new Size(96, 36);
            btnHistAll.TabIndex = 2;
            btnHistAll.Text = "Show All";
            btnHistAll.UseMnemonic = false;
            btnHistAll.UseVisualStyleBackColor = false;
            // 
            // chkAbnormalOnly
            // 
            chkAbnormalOnly.AutoSize = true;
            chkAbnormalOnly.BackColor = Color.Transparent;
            chkAbnormalOnly.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkAbnormalOnly.ForeColor = Color.FromArgb(20, 34, 51);
            chkAbnormalOnly.Margin = new Padding(14, 8, 0, 0);
            chkAbnormalOnly.Name = "chkAbnormalOnly";
            chkAbnormalOnly.TabIndex = 3;
            chkAbnormalOnly.Text = "Abnormal only";
            chkAbnormalOnly.UseMnemonic = false;
            chkAbnormalOnly.UseVisualStyleBackColor = false;
            // 
            // pnlDetailCard
            // 
            pnlDetailCard.BackColor = Color.White;
            pnlDetailCard.Controls.Add(pnlDetailBody);
            pnlDetailCard.Controls.Add(pnlDetailHeader);
            pnlDetailCard.Dock = DockStyle.Fill;
            pnlDetailCard.Margin = new Padding(0);
            pnlDetailCard.Name = "pnlDetailCard";
            pnlDetailCard.Padding = new Padding(1, 6, 1, 1);
            pnlDetailCard.TabIndex = 1;
            // 
            // pnlDetailBody
            // 
            pnlDetailBody.BackColor = Color.White;
            pnlDetailBody.Controls.Add(txtDetail);
            pnlDetailBody.Dock = DockStyle.Fill;
            pnlDetailBody.Margin = new Padding(0);
            pnlDetailBody.Name = "pnlDetailBody";
            pnlDetailBody.Padding = new Padding(18, 4, 18, 14);
            pnlDetailBody.TabIndex = 0;
            // 
            // txtDetail
            // 
            txtDetail.AutoSize = false;
            txtDetail.BackColor = Color.White;
            txtDetail.BorderStyle = BorderStyle.None;
            txtDetail.Dock = DockStyle.Fill;
            txtDetail.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDetail.ForeColor = Color.FromArgb(20, 34, 51);
            txtDetail.Margin = new Padding(0);
            txtDetail.MaxLength = 4000;
            txtDetail.Multiline = true;
            txtDetail.Name = "txtDetail";
            txtDetail.ReadOnly = true;
            txtDetail.ScrollBars = ScrollBars.Vertical;
            txtDetail.Size = new Size(200, 40);
            txtDetail.TabIndex = 0;
            // 
            // pnlDetailHeader
            // 
            pnlDetailHeader.BackColor = Color.White;
            pnlDetailHeader.Controls.Add(lblDetailTitle);
            pnlDetailHeader.Dock = DockStyle.Top;
            pnlDetailHeader.Margin = new Padding(0);
            pnlDetailHeader.Name = "pnlDetailHeader";
            pnlDetailHeader.Padding = new Padding(0);
            pnlDetailHeader.Size = new Size(600, 42);
            pnlDetailHeader.TabIndex = 1;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoEllipsis = true;
            lblDetailTitle.BackColor = Color.Transparent;
            lblDetailTitle.Dock = DockStyle.Fill;
            lblDetailTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetailTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblDetailTitle.Margin = new Padding(0);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Padding = new Padding(17, 0, 0, 0);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "Selected result";
            lblDetailTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblDetailTitle.UseMnemonic = false;
            // 
            // pnlEntry
            // 
            pnlEntry.BackColor = Color.Transparent;
            pnlEntry.Controls.Add(tlpEntry);
            pnlEntry.Dock = DockStyle.Fill;
            pnlEntry.Margin = new Padding(0);
            pnlEntry.Name = "pnlEntry";
            pnlEntry.Padding = new Padding(0);
            pnlEntry.TabIndex = 1;
            // 
            // tlpEntry
            // 
            tlpEntry.BackColor = Color.Transparent;
            tlpEntry.ColumnCount = 2;
            tlpEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEntry.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 480F));
            tlpEntry.Controls.Add(pnlOrdersCard, 0, 0);
            tlpEntry.Controls.Add(pnlEntryCard, 1, 0);
            tlpEntry.Dock = DockStyle.Fill;
            tlpEntry.Margin = new Padding(0);
            tlpEntry.Name = "tlpEntry";
            tlpEntry.RowCount = 1;
            tlpEntry.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpEntry.TabIndex = 0;
            // 
            // pnlOrdersCard
            // 
            pnlOrdersCard.BackColor = Color.White;
            pnlOrdersCard.Controls.Add(dgvOrders);
            pnlOrdersCard.Controls.Add(pnlOrdersFooter);
            pnlOrdersCard.Controls.Add(pnlOrdersHeader);
            pnlOrdersCard.Dock = DockStyle.Fill;
            pnlOrdersCard.Margin = new Padding(0, 0, 20, 0);
            pnlOrdersCard.Name = "pnlOrdersCard";
            pnlOrdersCard.Padding = new Padding(1, 6, 1, 1);
            pnlOrdersCard.TabIndex = 0;
            // 
            // dgvOrders
            // 
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AllowUserToResizeRows = false;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.Dock = DockStyle.Fill;
            dgvOrders.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvOrders.Margin = new Padding(0);
            dgvOrders.MultiSelect = false;
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.TabIndex = 0;
            // 
            // pnlOrdersFooter
            // 
            pnlOrdersFooter.BackColor = Color.White;
            pnlOrdersFooter.Controls.Add(lblOrderCount);
            pnlOrdersFooter.Controls.Add(flowOrdersFooter);
            pnlOrdersFooter.Dock = DockStyle.Bottom;
            pnlOrdersFooter.Margin = new Padding(0);
            pnlOrdersFooter.Name = "pnlOrdersFooter";
            pnlOrdersFooter.Padding = new Padding(0);
            pnlOrdersFooter.Size = new Size(600, 46);
            pnlOrdersFooter.TabIndex = 1;
            // 
            // lblOrderCount
            // 
            lblOrderCount.AutoEllipsis = true;
            lblOrderCount.BackColor = Color.Transparent;
            lblOrderCount.Dock = DockStyle.Fill;
            lblOrderCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOrderCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblOrderCount.Name = "lblOrderCount";
            lblOrderCount.Padding = new Padding(18, 0, 8, 0);
            lblOrderCount.TabIndex = 0;
            lblOrderCount.Text = "";
            lblOrderCount.TextAlign = ContentAlignment.MiddleLeft;
            lblOrderCount.UseMnemonic = false;
            // 
            // flowOrdersFooter
            // 
            flowOrdersFooter.AutoSize = true;
            flowOrdersFooter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowOrdersFooter.BackColor = Color.Transparent;
            flowOrdersFooter.Dock = DockStyle.Right;
            flowOrdersFooter.Margin = new Padding(0);
            flowOrdersFooter.Name = "flowOrdersFooter";
            flowOrdersFooter.Padding = new Padding(0);
            flowOrdersFooter.TabIndex = 1;
            flowOrdersFooter.WrapContents = false;
            // 
            // pnlOrdersHeader
            // 
            pnlOrdersHeader.BackColor = Color.White;
            pnlOrdersHeader.Controls.Add(lblOrdersTitle);
            pnlOrdersHeader.Controls.Add(flowOrderFilter);
            pnlOrdersHeader.Dock = DockStyle.Top;
            pnlOrdersHeader.Margin = new Padding(0);
            pnlOrdersHeader.Name = "pnlOrdersHeader";
            pnlOrdersHeader.Padding = new Padding(0);
            pnlOrdersHeader.Size = new Size(600, 46);
            pnlOrdersHeader.TabIndex = 2;
            // 
            // lblOrdersTitle
            // 
            lblOrdersTitle.AutoEllipsis = true;
            lblOrdersTitle.BackColor = Color.Transparent;
            lblOrdersTitle.Dock = DockStyle.Fill;
            lblOrdersTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOrdersTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblOrdersTitle.Margin = new Padding(0);
            lblOrdersTitle.Name = "lblOrdersTitle";
            lblOrdersTitle.Padding = new Padding(17, 0, 0, 0);
            lblOrdersTitle.TabIndex = 0;
            lblOrdersTitle.Text = "Pending lab orders  ·  STAT first";
            lblOrdersTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblOrdersTitle.UseMnemonic = false;
            // 
            // flowOrderFilter
            // 
            flowOrderFilter.AutoSize = true;
            flowOrderFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowOrderFilter.BackColor = Color.White;
            flowOrderFilter.Controls.Add(txtOrderSearch);
            flowOrderFilter.Controls.Add(btnOrderSearch);
            flowOrderFilter.Controls.Add(btnOrderAll);
            flowOrderFilter.Dock = DockStyle.Right;
            flowOrderFilter.Margin = new Padding(0);
            flowOrderFilter.Name = "flowOrderFilter";
            flowOrderFilter.Padding = new Padding(0, 5, 18, 0);
            flowOrderFilter.TabIndex = 1;
            flowOrderFilter.WrapContents = false;
            // 
            // txtOrderSearch
            // 
            txtOrderSearch.AutoSize = false;
            txtOrderSearch.BackColor = Color.White;
            txtOrderSearch.BorderStyle = BorderStyle.FixedSingle;
            txtOrderSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtOrderSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtOrderSearch.Margin = new Padding(0);
            txtOrderSearch.MaxLength = 60;
            txtOrderSearch.Name = "txtOrderSearch";
            txtOrderSearch.PlaceholderText = "Order, patient or test";
            txtOrderSearch.Size = new Size(220, 36);
            txtOrderSearch.TabIndex = 0;
            // 
            // btnOrderSearch
            // 
            btnOrderSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnOrderSearch.FlatStyle = FlatStyle.Flat;
            btnOrderSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnOrderSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnOrderSearch.Margin = new Padding(10, 0, 0, 0);
            btnOrderSearch.Name = "btnOrderSearch";
            btnOrderSearch.Size = new Size(86, 36);
            btnOrderSearch.TabIndex = 1;
            btnOrderSearch.Text = "Search";
            btnOrderSearch.UseMnemonic = false;
            btnOrderSearch.UseVisualStyleBackColor = false;
            // 
            // btnOrderAll
            // 
            btnOrderAll.BackColor = Color.White;
            btnOrderAll.FlatStyle = FlatStyle.Flat;
            btnOrderAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnOrderAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnOrderAll.Margin = new Padding(10, 0, 0, 0);
            btnOrderAll.Name = "btnOrderAll";
            btnOrderAll.Size = new Size(96, 36);
            btnOrderAll.TabIndex = 2;
            btnOrderAll.Text = "Show All";
            btnOrderAll.UseMnemonic = false;
            btnOrderAll.UseVisualStyleBackColor = false;
            // 
            // pnlEntryCard
            // 
            pnlEntryCard.BackColor = Color.White;
            pnlEntryCard.Controls.Add(tlpEntryCard);
            pnlEntryCard.Dock = DockStyle.Fill;
            pnlEntryCard.Margin = new Padding(0);
            pnlEntryCard.Name = "pnlEntryCard";
            pnlEntryCard.Padding = new Padding(20);
            pnlEntryCard.TabIndex = 1;
            // 
            // tlpEntryCard
            // 
            tlpEntryCard.BackColor = Color.Transparent;
            tlpEntryCard.ColumnCount = 1;
            tlpEntryCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEntryCard.Controls.Add(lblEntryTitle, 0, 0);
            tlpEntryCard.Controls.Add(pnlEntryScroll, 0, 1);
            tlpEntryCard.Controls.Add(tlpEntryButtons, 0, 2);
            tlpEntryCard.Dock = DockStyle.Fill;
            tlpEntryCard.Margin = new Padding(0);
            tlpEntryCard.Name = "tlpEntryCard";
            tlpEntryCard.RowCount = 3;
            tlpEntryCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpEntryCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpEntryCard.TabIndex = 0;
            // 
            // lblEntryTitle
            // 
            lblEntryTitle.AutoSize = true;
            lblEntryTitle.BackColor = Color.Transparent;
            lblEntryTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEntryTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblEntryTitle.Margin = new Padding(0, 0, 0, 14);
            lblEntryTitle.Name = "lblEntryTitle";
            lblEntryTitle.TabIndex = 0;
            lblEntryTitle.Text = "Result entry";
            lblEntryTitle.UseMnemonic = false;
            // 
            // pnlEntryScroll
            // 
            pnlEntryScroll.AutoScroll = true;
            pnlEntryScroll.BackColor = Color.Transparent;
            pnlEntryScroll.Controls.Add(tlpEntryFields);
            pnlEntryScroll.Dock = DockStyle.Fill;
            pnlEntryScroll.Margin = new Padding(0, 0, 0, 14);
            pnlEntryScroll.Name = "pnlEntryScroll";
            pnlEntryScroll.Padding = new Padding(0, 0, 6, 0);
            pnlEntryScroll.TabIndex = 1;
            // 
            // tlpEntryFields
            // 
            tlpEntryFields.AutoSize = true;
            tlpEntryFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpEntryFields.BackColor = Color.Transparent;
            tlpEntryFields.ColumnCount = 1;
            tlpEntryFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEntryFields.Controls.Add(tlpEntryA, 0, 0);
            tlpEntryFields.Controls.Add(lblFlag, 0, 1);
            tlpEntryFields.Controls.Add(tlpEntryB, 0, 2);
            tlpEntryFields.Dock = DockStyle.Top;
            tlpEntryFields.Margin = new Padding(0);
            tlpEntryFields.Name = "tlpEntryFields";
            tlpEntryFields.RowCount = 3;
            tlpEntryFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryFields.TabIndex = 0;
            // 
            // tlpEntryA
            // 
            tlpEntryA.AutoSize = true;
            tlpEntryA.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpEntryA.BackColor = Color.Transparent;
            tlpEntryA.ColumnCount = 2;
            tlpEntryA.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpEntryA.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpEntryA.Controls.Add(lblOrderCode, 0, 0);
            tlpEntryA.Controls.Add(txtOrderCode, 0, 1);
            tlpEntryA.Controls.Add(lblPatient, 1, 0);
            tlpEntryA.Controls.Add(txtPatient, 1, 1);
            tlpEntryA.Controls.Add(lblTest, 0, 2);
            tlpEntryA.SetColumnSpan(lblTest, 2);
            tlpEntryA.Controls.Add(txtTest, 0, 3);
            tlpEntryA.SetColumnSpan(txtTest, 2);
            tlpEntryA.Controls.Add(lblRange, 0, 4);
            tlpEntryA.Controls.Add(txtRange, 0, 5);
            tlpEntryA.Controls.Add(lblValue, 1, 4);
            tlpEntryA.Controls.Add(txtValue, 1, 5);
            tlpEntryA.Dock = DockStyle.Top;
            tlpEntryA.Margin = new Padding(0);
            tlpEntryA.Name = "tlpEntryA";
            tlpEntryA.RowCount = 6;
            tlpEntryA.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryA.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpEntryA.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryA.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpEntryA.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryA.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpEntryA.TabIndex = 0;
            // 
            // lblOrderCode
            // 
            lblOrderCode.AutoSize = true;
            lblOrderCode.BackColor = Color.Transparent;
            lblOrderCode.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOrderCode.ForeColor = Color.FromArgb(20, 34, 51);
            lblOrderCode.Margin = new Padding(0, 0, 6, 6);
            lblOrderCode.Name = "lblOrderCode";
            lblOrderCode.TabIndex = 0;
            lblOrderCode.Text = "Lab order";
            lblOrderCode.UseMnemonic = false;
            // 
            // txtOrderCode
            // 
            txtOrderCode.AutoSize = false;
            txtOrderCode.BackColor = Color.White;
            txtOrderCode.BorderStyle = BorderStyle.FixedSingle;
            txtOrderCode.Dock = DockStyle.Fill;
            txtOrderCode.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtOrderCode.ForeColor = Color.FromArgb(20, 34, 51);
            txtOrderCode.Margin = new Padding(0, 0, 6, 14);
            txtOrderCode.MaxLength = 20;
            txtOrderCode.Name = "txtOrderCode";
            txtOrderCode.PlaceholderText = "Select a pending order";
            txtOrderCode.ReadOnly = true;
            txtOrderCode.Size = new Size(200, 40);
            txtOrderCode.TabIndex = 1;
            // 
            // lblPatient
            // 
            lblPatient.AutoSize = true;
            lblPatient.BackColor = Color.Transparent;
            lblPatient.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPatient.ForeColor = Color.FromArgb(20, 34, 51);
            lblPatient.Margin = new Padding(6, 0, 0, 6);
            lblPatient.Name = "lblPatient";
            lblPatient.TabIndex = 2;
            lblPatient.Text = "Patient";
            lblPatient.UseMnemonic = false;
            // 
            // txtPatient
            // 
            txtPatient.AutoSize = false;
            txtPatient.BackColor = Color.White;
            txtPatient.BorderStyle = BorderStyle.FixedSingle;
            txtPatient.Dock = DockStyle.Fill;
            txtPatient.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPatient.ForeColor = Color.FromArgb(20, 34, 51);
            txtPatient.Margin = new Padding(6, 0, 0, 14);
            txtPatient.MaxLength = 150;
            txtPatient.Name = "txtPatient";
            txtPatient.ReadOnly = true;
            txtPatient.Size = new Size(200, 40);
            txtPatient.TabIndex = 3;
            // 
            // lblTest
            // 
            lblTest.AutoSize = true;
            lblTest.BackColor = Color.Transparent;
            lblTest.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTest.ForeColor = Color.FromArgb(20, 34, 51);
            lblTest.Margin = new Padding(0, 0, 0, 6);
            lblTest.Name = "lblTest";
            lblTest.TabIndex = 4;
            lblTest.Text = "Test";
            lblTest.UseMnemonic = false;
            // 
            // txtTest
            // 
            txtTest.AutoSize = false;
            txtTest.BackColor = Color.White;
            txtTest.BorderStyle = BorderStyle.FixedSingle;
            txtTest.Dock = DockStyle.Fill;
            txtTest.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTest.ForeColor = Color.FromArgb(20, 34, 51);
            txtTest.Margin = new Padding(0, 0, 0, 14);
            txtTest.MaxLength = 200;
            txtTest.Name = "txtTest";
            txtTest.ReadOnly = true;
            txtTest.Size = new Size(200, 40);
            txtTest.TabIndex = 5;
            // 
            // lblRange
            // 
            lblRange.AutoSize = true;
            lblRange.BackColor = Color.Transparent;
            lblRange.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRange.ForeColor = Color.FromArgb(20, 34, 51);
            lblRange.Margin = new Padding(0, 0, 6, 6);
            lblRange.Name = "lblRange";
            lblRange.TabIndex = 6;
            lblRange.Text = "Normal range";
            lblRange.UseMnemonic = false;
            // 
            // txtRange
            // 
            txtRange.AutoSize = false;
            txtRange.BackColor = Color.White;
            txtRange.BorderStyle = BorderStyle.FixedSingle;
            txtRange.Dock = DockStyle.Fill;
            txtRange.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRange.ForeColor = Color.FromArgb(20, 34, 51);
            txtRange.Margin = new Padding(0, 0, 6, 14);
            txtRange.MaxLength = 100;
            txtRange.Name = "txtRange";
            txtRange.ReadOnly = true;
            txtRange.Size = new Size(200, 40);
            txtRange.TabIndex = 7;
            // 
            // lblValue
            // 
            lblValue.AutoSize = true;
            lblValue.BackColor = Color.Transparent;
            lblValue.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValue.ForeColor = Color.FromArgb(20, 34, 51);
            lblValue.Margin = new Padding(6, 0, 0, 6);
            lblValue.Name = "lblValue";
            lblValue.TabIndex = 8;
            lblValue.Text = "Result value *";
            lblValue.UseMnemonic = false;
            // 
            // txtValue
            // 
            txtValue.AutoSize = false;
            txtValue.BackColor = Color.White;
            txtValue.BorderStyle = BorderStyle.FixedSingle;
            txtValue.Dock = DockStyle.Fill;
            txtValue.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtValue.ForeColor = Color.FromArgb(20, 34, 51);
            txtValue.Margin = new Padding(6, 0, 0, 14);
            txtValue.MaxLength = 100;
            txtValue.Name = "txtValue";
            txtValue.PlaceholderText = "Number or text, e.g. 13.5 or Negative";
            txtValue.Size = new Size(200, 40);
            txtValue.TabIndex = 9;
            // 
            // lblFlag
            // 
            lblFlag.BackColor = Color.FromArgb(247, 249, 251);
            lblFlag.Dock = DockStyle.Top;
            lblFlag.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFlag.ForeColor = Color.FromArgb(91, 107, 124);
            lblFlag.Margin = new Padding(0, 0, 0, 14);
            lblFlag.Name = "lblFlag";
            lblFlag.Padding = new Padding(12, 0, 12, 0);
            lblFlag.Size = new Size(400, 36);
            lblFlag.TabIndex = 1;
            lblFlag.Text = "Select a pending order to begin";
            lblFlag.TextAlign = ContentAlignment.MiddleLeft;
            lblFlag.UseMnemonic = false;
            // 
            // tlpEntryB
            // 
            tlpEntryB.AutoSize = true;
            tlpEntryB.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpEntryB.BackColor = Color.Transparent;
            tlpEntryB.ColumnCount = 2;
            tlpEntryB.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpEntryB.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpEntryB.Controls.Add(lblTested, 0, 0);
            tlpEntryB.SetColumnSpan(lblTested, 2);
            tlpEntryB.Controls.Add(dtpTested, 0, 1);
            tlpEntryB.SetColumnSpan(dtpTested, 2);
            tlpEntryB.Controls.Add(lblTestedBy, 0, 2);
            tlpEntryB.Controls.Add(cboTestedBy, 0, 3);
            tlpEntryB.Controls.Add(lblVerifiedBy, 1, 2);
            tlpEntryB.Controls.Add(cboVerifiedBy, 1, 3);
            tlpEntryB.Controls.Add(lblFindings, 0, 4);
            tlpEntryB.SetColumnSpan(lblFindings, 2);
            tlpEntryB.Controls.Add(txtFindings, 0, 5);
            tlpEntryB.SetColumnSpan(txtFindings, 2);
            tlpEntryB.Dock = DockStyle.Top;
            tlpEntryB.Margin = new Padding(0);
            tlpEntryB.Name = "tlpEntryB";
            tlpEntryB.RowCount = 6;
            tlpEntryB.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryB.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpEntryB.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryB.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpEntryB.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpEntryB.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            tlpEntryB.TabIndex = 2;
            // 
            // lblTested
            // 
            lblTested.AutoSize = true;
            lblTested.BackColor = Color.Transparent;
            lblTested.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTested.ForeColor = Color.FromArgb(20, 34, 51);
            lblTested.Margin = new Padding(0, 0, 0, 6);
            lblTested.Name = "lblTested";
            lblTested.TabIndex = 0;
            lblTested.Text = "Test date / time *";
            lblTested.UseMnemonic = false;
            // 
            // dtpTested
            // 
            dtpTested.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpTested.CustomFormat = "ddd, MMM dd, yyyy  hh:mm tt";
            dtpTested.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpTested.Format = DateTimePickerFormat.Custom;
            dtpTested.Margin = new Padding(0, 0, 0, 14);
            dtpTested.Name = "dtpTested";
            dtpTested.Size = new Size(200, 31);
            dtpTested.TabIndex = 1;
            // 
            // lblTestedBy
            // 
            lblTestedBy.AutoSize = true;
            lblTestedBy.BackColor = Color.Transparent;
            lblTestedBy.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTestedBy.ForeColor = Color.FromArgb(20, 34, 51);
            lblTestedBy.Margin = new Padding(0, 0, 6, 6);
            lblTestedBy.Name = "lblTestedBy";
            lblTestedBy.TabIndex = 2;
            lblTestedBy.Text = "Tested by *";
            lblTestedBy.UseMnemonic = false;
            // 
            // cboTestedBy
            // 
            cboTestedBy.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboTestedBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTestedBy.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboTestedBy.FormattingEnabled = true;
            cboTestedBy.Margin = new Padding(0, 0, 6, 14);
            cboTestedBy.Name = "cboTestedBy";
            cboTestedBy.Size = new Size(200, 32);
            cboTestedBy.TabIndex = 3;
            // 
            // lblVerifiedBy
            // 
            lblVerifiedBy.AutoSize = true;
            lblVerifiedBy.BackColor = Color.Transparent;
            lblVerifiedBy.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVerifiedBy.ForeColor = Color.FromArgb(20, 34, 51);
            lblVerifiedBy.Margin = new Padding(6, 0, 0, 6);
            lblVerifiedBy.Name = "lblVerifiedBy";
            lblVerifiedBy.TabIndex = 4;
            lblVerifiedBy.Text = "Verified by (optional)";
            lblVerifiedBy.UseMnemonic = false;
            // 
            // cboVerifiedBy
            // 
            cboVerifiedBy.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboVerifiedBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVerifiedBy.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboVerifiedBy.FormattingEnabled = true;
            cboVerifiedBy.Margin = new Padding(6, 0, 0, 14);
            cboVerifiedBy.Name = "cboVerifiedBy";
            cboVerifiedBy.Size = new Size(200, 32);
            cboVerifiedBy.TabIndex = 5;
            // 
            // lblFindings
            // 
            lblFindings.AutoSize = true;
            lblFindings.BackColor = Color.Transparent;
            lblFindings.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFindings.ForeColor = Color.FromArgb(20, 34, 51);
            lblFindings.Margin = new Padding(0, 0, 0, 6);
            lblFindings.Name = "lblFindings";
            lblFindings.TabIndex = 6;
            lblFindings.Text = "Findings";
            lblFindings.UseMnemonic = false;
            // 
            // txtFindings
            // 
            txtFindings.AutoSize = false;
            txtFindings.BackColor = Color.White;
            txtFindings.BorderStyle = BorderStyle.FixedSingle;
            txtFindings.Dock = DockStyle.Fill;
            txtFindings.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtFindings.ForeColor = Color.FromArgb(20, 34, 51);
            txtFindings.Margin = new Padding(0, 0, 0, 14);
            txtFindings.MaxLength = 500;
            txtFindings.Multiline = true;
            txtFindings.Name = "txtFindings";
            txtFindings.PlaceholderText = "Findings / observations (optional)";
            txtFindings.ScrollBars = ScrollBars.Vertical;
            txtFindings.Size = new Size(200, 64);
            txtFindings.TabIndex = 7;
            // 
            // tlpEntryButtons
            // 
            tlpEntryButtons.BackColor = Color.Transparent;
            tlpEntryButtons.ColumnCount = 2;
            tlpEntryButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpEntryButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlpEntryButtons.Controls.Add(btnEntrySave, 0, 0);
            tlpEntryButtons.Controls.Add(btnEntryClear, 1, 0);
            tlpEntryButtons.Dock = DockStyle.Fill;
            tlpEntryButtons.Margin = new Padding(0);
            tlpEntryButtons.Name = "tlpEntryButtons";
            tlpEntryButtons.RowCount = 1;
            tlpEntryButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpEntryButtons.TabIndex = 2;
            // 
            // btnEntrySave
            // 
            btnEntrySave.BackColor = Color.FromArgb(18, 122, 140);
            btnEntrySave.Dock = DockStyle.Fill;
            btnEntrySave.Enabled = false;
            btnEntrySave.FlatStyle = FlatStyle.Flat;
            btnEntrySave.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEntrySave.ForeColor = Color.White;
            btnEntrySave.Margin = new Padding(0);
            btnEntrySave.Name = "btnEntrySave";
            btnEntrySave.Size = new Size(120, 40);
            btnEntrySave.TabIndex = 0;
            btnEntrySave.Text = "Save Result";
            btnEntrySave.UseMnemonic = false;
            btnEntrySave.UseVisualStyleBackColor = false;
            // 
            // btnEntryClear
            // 
            btnEntryClear.BackColor = Color.White;
            btnEntryClear.Dock = DockStyle.Fill;
            btnEntryClear.FlatStyle = FlatStyle.Flat;
            btnEntryClear.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEntryClear.ForeColor = Color.FromArgb(20, 34, 51);
            btnEntryClear.Margin = new Padding(10, 0, 0, 0);
            btnEntryClear.Name = "btnEntryClear";
            btnEntryClear.Size = new Size(120, 40);
            btnEntryClear.TabIndex = 1;
            btnEntryClear.Text = "Clear";
            btnEntryClear.UseMnemonic = false;
            btnEntryClear.UseVisualStyleBackColor = false;
            // 
            // pnlCatalog
            // 
            pnlCatalog.BackColor = Color.Transparent;
            pnlCatalog.Controls.Add(tlpCatalog);
            pnlCatalog.Dock = DockStyle.Fill;
            pnlCatalog.Margin = new Padding(0);
            pnlCatalog.Name = "pnlCatalog";
            pnlCatalog.Padding = new Padding(0);
            pnlCatalog.TabIndex = 2;
            pnlCatalog.Visible = false;
            // 
            // tlpCatalog
            // 
            tlpCatalog.BackColor = Color.Transparent;
            tlpCatalog.ColumnCount = 2;
            tlpCatalog.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 480F));
            tlpCatalog.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCatalog.Controls.Add(pnlTestCard, 0, 0);
            tlpCatalog.Controls.Add(pnlCatalogCard, 1, 0);
            tlpCatalog.Dock = DockStyle.Fill;
            tlpCatalog.Margin = new Padding(0);
            tlpCatalog.Name = "tlpCatalog";
            tlpCatalog.RowCount = 1;
            tlpCatalog.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCatalog.TabIndex = 0;
            // 
            // pnlTestCard
            // 
            pnlTestCard.BackColor = Color.White;
            pnlTestCard.Controls.Add(tlpTestCard);
            pnlTestCard.Dock = DockStyle.Fill;
            pnlTestCard.Margin = new Padding(0, 0, 20, 0);
            pnlTestCard.Name = "pnlTestCard";
            pnlTestCard.Padding = new Padding(20);
            pnlTestCard.TabIndex = 0;
            // 
            // tlpTestCard
            // 
            tlpTestCard.BackColor = Color.Transparent;
            tlpTestCard.ColumnCount = 1;
            tlpTestCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpTestCard.Controls.Add(lblTestTitle, 0, 0);
            tlpTestCard.Controls.Add(pnlTestScroll, 0, 1);
            tlpTestCard.Controls.Add(tlpTestButtons, 0, 2);
            tlpTestCard.Dock = DockStyle.Fill;
            tlpTestCard.Margin = new Padding(0);
            tlpTestCard.Name = "tlpTestCard";
            tlpTestCard.RowCount = 3;
            tlpTestCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTestCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTestCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpTestCard.TabIndex = 0;
            // 
            // lblTestTitle
            // 
            lblTestTitle.AutoSize = true;
            lblTestTitle.BackColor = Color.Transparent;
            lblTestTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTestTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblTestTitle.Margin = new Padding(0, 0, 0, 14);
            lblTestTitle.Name = "lblTestTitle";
            lblTestTitle.TabIndex = 0;
            lblTestTitle.Text = "Test details  ·  new test";
            lblTestTitle.UseMnemonic = false;
            // 
            // pnlTestScroll
            // 
            pnlTestScroll.AutoScroll = true;
            pnlTestScroll.BackColor = Color.Transparent;
            pnlTestScroll.Controls.Add(tlpDetailsFields);
            pnlTestScroll.Dock = DockStyle.Fill;
            pnlTestScroll.Margin = new Padding(0, 0, 0, 14);
            pnlTestScroll.Name = "pnlTestScroll";
            pnlTestScroll.Padding = new Padding(0, 0, 6, 0);
            pnlTestScroll.TabIndex = 1;
            // 
            // tlpDetailsFields
            // 
            tlpDetailsFields.AutoSize = true;
            tlpDetailsFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpDetailsFields.BackColor = Color.Transparent;
            tlpDetailsFields.ColumnCount = 1;
            tlpDetailsFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDetailsFields.Controls.Add(tlpDetails, 0, 0);
            tlpDetailsFields.Controls.Add(lblRangeHint, 0, 1);
            tlpDetailsFields.Dock = DockStyle.Top;
            tlpDetailsFields.Margin = new Padding(0);
            tlpDetailsFields.Name = "tlpDetailsFields";
            tlpDetailsFields.RowCount = 2;
            tlpDetailsFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDetailsFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDetailsFields.TabIndex = 0;
            // 
            // tlpDetails
            // 
            tlpDetails.AutoSize = true;
            tlpDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpDetails.BackColor = Color.Transparent;
            tlpDetails.ColumnCount = 2;
            tlpDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpDetails.Controls.Add(lblCode, 0, 0);
            tlpDetails.Controls.Add(txtCode, 0, 1);
            tlpDetails.Controls.Add(lblPrice, 1, 0);
            tlpDetails.Controls.Add(txtPrice, 1, 1);
            tlpDetails.Controls.Add(lblName, 0, 2);
            tlpDetails.SetColumnSpan(lblName, 2);
            tlpDetails.Controls.Add(txtName, 0, 3);
            tlpDetails.SetColumnSpan(txtName, 2);
            tlpDetails.Controls.Add(lblCategory, 0, 4);
            tlpDetails.Controls.Add(cboCategory, 0, 5);
            tlpDetails.Controls.Add(lblSample, 1, 4);
            tlpDetails.Controls.Add(cboSample, 1, 5);
            tlpDetails.Controls.Add(lblLow, 0, 6);
            tlpDetails.Controls.Add(txtLow, 0, 7);
            tlpDetails.Controls.Add(lblHigh, 1, 6);
            tlpDetails.Controls.Add(txtHigh, 1, 7);
            tlpDetails.Controls.Add(lblUnit, 0, 8);
            tlpDetails.Controls.Add(txtUnit, 0, 9);
            tlpDetails.Controls.Add(lblTurnaround, 1, 8);
            tlpDetails.Controls.Add(txtTurnaround, 1, 9);
            tlpDetails.Controls.Add(chkVerify, 0, 10);
            tlpDetails.SetColumnSpan(chkVerify, 2);
            tlpDetails.Dock = DockStyle.Top;
            tlpDetails.Margin = new Padding(0);
            tlpDetails.Name = "tlpDetails";
            tlpDetails.RowCount = 11;
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpDetails.TabIndex = 0;
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
            lblCode.Text = "Test code";
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
            txtCode.PlaceholderText = "Assigned when saved";
            txtCode.ReadOnly = true;
            txtCode.Size = new Size(200, 40);
            txtCode.TabIndex = 1;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.BackColor = Color.Transparent;
            lblPrice.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPrice.ForeColor = Color.FromArgb(20, 34, 51);
            lblPrice.Margin = new Padding(6, 0, 0, 6);
            lblPrice.Name = "lblPrice";
            lblPrice.TabIndex = 2;
            lblPrice.Text = "Price *";
            lblPrice.UseMnemonic = false;
            // 
            // txtPrice
            // 
            txtPrice.AutoSize = false;
            txtPrice.BackColor = Color.White;
            txtPrice.BorderStyle = BorderStyle.FixedSingle;
            txtPrice.Dock = DockStyle.Fill;
            txtPrice.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrice.ForeColor = Color.FromArgb(20, 34, 51);
            txtPrice.Margin = new Padding(6, 0, 0, 14);
            txtPrice.MaxLength = 10;
            txtPrice.Name = "txtPrice";
            txtPrice.PlaceholderText = "e.g. 150.00";
            txtPrice.Size = new Size(200, 40);
            txtPrice.TabIndex = 3;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblName.ForeColor = Color.FromArgb(20, 34, 51);
            lblName.Margin = new Padding(0, 0, 0, 6);
            lblName.Name = "lblName";
            lblName.TabIndex = 4;
            lblName.Text = "Test name *";
            lblName.UseMnemonic = false;
            // 
            // txtName
            // 
            txtName.AutoSize = false;
            txtName.BackColor = Color.White;
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Dock = DockStyle.Fill;
            txtName.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtName.ForeColor = Color.FromArgb(20, 34, 51);
            txtName.Margin = new Padding(0, 0, 0, 14);
            txtName.MaxLength = 120;
            txtName.Name = "txtName";
            txtName.PlaceholderText = "e.g. Hemoglobin";
            txtName.Size = new Size(200, 40);
            txtName.TabIndex = 5;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.BackColor = Color.Transparent;
            lblCategory.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCategory.ForeColor = Color.FromArgb(20, 34, 51);
            lblCategory.Margin = new Padding(0, 0, 6, 6);
            lblCategory.Name = "lblCategory";
            lblCategory.TabIndex = 6;
            lblCategory.Text = "Category *";
            lblCategory.UseMnemonic = false;
            // 
            // cboCategory
            // 
            cboCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboCategory.DropDownStyle = ComboBoxStyle.DropDown;
            cboCategory.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboCategory.FormattingEnabled = true;
            cboCategory.Margin = new Padding(0, 0, 6, 14);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(200, 32);
            cboCategory.TabIndex = 7;
            // 
            // lblSample
            // 
            lblSample.AutoSize = true;
            lblSample.BackColor = Color.Transparent;
            lblSample.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSample.ForeColor = Color.FromArgb(20, 34, 51);
            lblSample.Margin = new Padding(6, 0, 0, 6);
            lblSample.Name = "lblSample";
            lblSample.TabIndex = 8;
            lblSample.Text = "Sample type *";
            lblSample.UseMnemonic = false;
            // 
            // cboSample
            // 
            cboSample.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboSample.DropDownStyle = ComboBoxStyle.DropDown;
            cboSample.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboSample.FormattingEnabled = true;
            cboSample.Margin = new Padding(6, 0, 0, 14);
            cboSample.Name = "cboSample";
            cboSample.Size = new Size(200, 32);
            cboSample.TabIndex = 9;
            // 
            // lblLow
            // 
            lblLow.AutoSize = true;
            lblLow.BackColor = Color.Transparent;
            lblLow.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLow.ForeColor = Color.FromArgb(20, 34, 51);
            lblLow.Margin = new Padding(0, 0, 6, 6);
            lblLow.Name = "lblLow";
            lblLow.TabIndex = 10;
            lblLow.Text = "Normal range - low *";
            lblLow.UseMnemonic = false;
            // 
            // txtLow
            // 
            txtLow.AutoSize = false;
            txtLow.BackColor = Color.White;
            txtLow.BorderStyle = BorderStyle.FixedSingle;
            txtLow.Dock = DockStyle.Fill;
            txtLow.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLow.ForeColor = Color.FromArgb(20, 34, 51);
            txtLow.Margin = new Padding(0, 0, 6, 14);
            txtLow.MaxLength = 40;
            txtLow.Name = "txtLow";
            txtLow.PlaceholderText = "e.g. 12.0  or  Negative";
            txtLow.Size = new Size(200, 40);
            txtLow.TabIndex = 11;
            // 
            // lblHigh
            // 
            lblHigh.AutoSize = true;
            lblHigh.BackColor = Color.Transparent;
            lblHigh.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHigh.ForeColor = Color.FromArgb(20, 34, 51);
            lblHigh.Margin = new Padding(6, 0, 0, 6);
            lblHigh.Name = "lblHigh";
            lblHigh.TabIndex = 12;
            lblHigh.Text = "Normal range - high *";
            lblHigh.UseMnemonic = false;
            // 
            // txtHigh
            // 
            txtHigh.AutoSize = false;
            txtHigh.BackColor = Color.White;
            txtHigh.BorderStyle = BorderStyle.FixedSingle;
            txtHigh.Dock = DockStyle.Fill;
            txtHigh.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtHigh.ForeColor = Color.FromArgb(20, 34, 51);
            txtHigh.Margin = new Padding(6, 0, 0, 14);
            txtHigh.MaxLength = 40;
            txtHigh.Name = "txtHigh";
            txtHigh.PlaceholderText = "e.g. 17.5  or  Negative";
            txtHigh.Size = new Size(200, 40);
            txtHigh.TabIndex = 13;
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.BackColor = Color.Transparent;
            lblUnit.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUnit.ForeColor = Color.FromArgb(20, 34, 51);
            lblUnit.Margin = new Padding(0, 0, 6, 6);
            lblUnit.Name = "lblUnit";
            lblUnit.TabIndex = 14;
            lblUnit.Text = "Unit";
            lblUnit.UseMnemonic = false;
            // 
            // txtUnit
            // 
            txtUnit.AutoSize = false;
            txtUnit.BackColor = Color.White;
            txtUnit.BorderStyle = BorderStyle.FixedSingle;
            txtUnit.Dock = DockStyle.Fill;
            txtUnit.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUnit.ForeColor = Color.FromArgb(20, 34, 51);
            txtUnit.Margin = new Padding(0, 0, 6, 14);
            txtUnit.MaxLength = 30;
            txtUnit.Name = "txtUnit";
            txtUnit.PlaceholderText = "e.g. g/dL (optional)";
            txtUnit.Size = new Size(200, 40);
            txtUnit.TabIndex = 15;
            // 
            // lblTurnaround
            // 
            lblTurnaround.AutoSize = true;
            lblTurnaround.BackColor = Color.Transparent;
            lblTurnaround.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTurnaround.ForeColor = Color.FromArgb(20, 34, 51);
            lblTurnaround.Margin = new Padding(6, 0, 0, 6);
            lblTurnaround.Name = "lblTurnaround";
            lblTurnaround.TabIndex = 16;
            lblTurnaround.Text = "Turnaround (hours) *";
            lblTurnaround.UseMnemonic = false;
            // 
            // txtTurnaround
            // 
            txtTurnaround.AutoSize = false;
            txtTurnaround.BackColor = Color.White;
            txtTurnaround.BorderStyle = BorderStyle.FixedSingle;
            txtTurnaround.Dock = DockStyle.Fill;
            txtTurnaround.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTurnaround.ForeColor = Color.FromArgb(20, 34, 51);
            txtTurnaround.Margin = new Padding(6, 0, 0, 14);
            txtTurnaround.MaxLength = 3;
            txtTurnaround.Name = "txtTurnaround";
            txtTurnaround.PlaceholderText = "e.g. 4";
            txtTurnaround.Size = new Size(200, 40);
            txtTurnaround.TabIndex = 17;
            // 
            // chkVerify
            // 
            chkVerify.AutoSize = true;
            chkVerify.BackColor = Color.Transparent;
            chkVerify.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkVerify.ForeColor = Color.FromArgb(20, 34, 51);
            chkVerify.Margin = new Padding(0, 0, 0, 8);
            chkVerify.Name = "chkVerify";
            chkVerify.TabIndex = 18;
            chkVerify.Text = "Requires verification by a second person";
            chkVerify.UseMnemonic = false;
            chkVerify.UseVisualStyleBackColor = false;
            // 
            // lblRangeHint
            // 
            lblRangeHint.AutoSize = true;
            lblRangeHint.BackColor = Color.Transparent;
            lblRangeHint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRangeHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblRangeHint.Margin = new Padding(0, 0, 0, 4);
            lblRangeHint.MaximumSize = new Size(420, 0);
            lblRangeHint.Name = "lblRangeHint";
            lblRangeHint.TabIndex = 1;
            lblRangeHint.Text = "Numeric test: enter two numbers (12.0 and 17.5); a result outside them is flagged Abnormal.\nDescriptive test: type the same word in both boxes (Negative and Negative); any other result is flagged Abnormal.";
            lblRangeHint.UseMnemonic = false;
            // 
            // tlpTestButtons
            // 
            tlpTestButtons.BackColor = Color.Transparent;
            tlpTestButtons.ColumnCount = 3;
            tlpTestButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpTestButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTestButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTestButtons.Controls.Add(btnTestSave, 0, 0);
            tlpTestButtons.Controls.Add(btnTestNew, 1, 0);
            tlpTestButtons.Controls.Add(btnTestDelete, 2, 0);
            tlpTestButtons.Dock = DockStyle.Fill;
            tlpTestButtons.Margin = new Padding(0);
            tlpTestButtons.Name = "tlpTestButtons";
            tlpTestButtons.RowCount = 1;
            tlpTestButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpTestButtons.TabIndex = 2;
            // 
            // btnTestSave
            // 
            btnTestSave.BackColor = Color.FromArgb(18, 122, 140);
            btnTestSave.Dock = DockStyle.Fill;
            btnTestSave.FlatStyle = FlatStyle.Flat;
            btnTestSave.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTestSave.ForeColor = Color.White;
            btnTestSave.Margin = new Padding(0, 0, 0, 0);
            btnTestSave.Name = "btnTestSave";
            btnTestSave.Size = new Size(120, 40);
            btnTestSave.TabIndex = 0;
            btnTestSave.Text = "Save Test";
            btnTestSave.UseMnemonic = false;
            btnTestSave.UseVisualStyleBackColor = false;
            // 
            // btnTestNew
            // 
            btnTestNew.BackColor = Color.White;
            btnTestNew.Dock = DockStyle.Fill;
            btnTestNew.FlatStyle = FlatStyle.Flat;
            btnTestNew.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTestNew.ForeColor = Color.FromArgb(20, 34, 51);
            btnTestNew.Margin = new Padding(10, 0, 0, 0);
            btnTestNew.Name = "btnTestNew";
            btnTestNew.Size = new Size(120, 40);
            btnTestNew.TabIndex = 1;
            btnTestNew.Text = "New";
            btnTestNew.UseMnemonic = false;
            btnTestNew.UseVisualStyleBackColor = false;
            // 
            // btnTestDelete
            // 
            btnTestDelete.BackColor = Color.FromArgb(180, 35, 24);
            btnTestDelete.Dock = DockStyle.Fill;
            btnTestDelete.Enabled = false;
            btnTestDelete.FlatStyle = FlatStyle.Flat;
            btnTestDelete.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTestDelete.ForeColor = Color.White;
            btnTestDelete.Margin = new Padding(10, 0, 0, 0);
            btnTestDelete.Name = "btnTestDelete";
            btnTestDelete.Size = new Size(120, 40);
            btnTestDelete.TabIndex = 2;
            btnTestDelete.Text = "Delete";
            btnTestDelete.UseMnemonic = false;
            btnTestDelete.UseVisualStyleBackColor = false;
            // 
            // pnlCatalogCard
            // 
            pnlCatalogCard.BackColor = Color.White;
            pnlCatalogCard.Controls.Add(dgvTests);
            pnlCatalogCard.Controls.Add(pnlCatalogFooter);
            pnlCatalogCard.Controls.Add(pnlCatalogHeader);
            pnlCatalogCard.Dock = DockStyle.Fill;
            pnlCatalogCard.Margin = new Padding(0);
            pnlCatalogCard.Name = "pnlCatalogCard";
            pnlCatalogCard.Padding = new Padding(1, 6, 1, 1);
            pnlCatalogCard.TabIndex = 1;
            // 
            // dgvTests
            // 
            dgvTests.AllowUserToAddRows = false;
            dgvTests.AllowUserToDeleteRows = false;
            dgvTests.AllowUserToResizeRows = false;
            dgvTests.BackgroundColor = Color.White;
            dgvTests.BorderStyle = BorderStyle.None;
            dgvTests.Dock = DockStyle.Fill;
            dgvTests.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvTests.Margin = new Padding(0);
            dgvTests.MultiSelect = false;
            dgvTests.Name = "dgvTests";
            dgvTests.ReadOnly = true;
            dgvTests.RowHeadersVisible = false;
            dgvTests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTests.TabIndex = 0;
            // 
            // pnlCatalogFooter
            // 
            pnlCatalogFooter.BackColor = Color.White;
            pnlCatalogFooter.Controls.Add(lblCatCount);
            pnlCatalogFooter.Controls.Add(flowCatalogFooter);
            pnlCatalogFooter.Dock = DockStyle.Bottom;
            pnlCatalogFooter.Margin = new Padding(0);
            pnlCatalogFooter.Name = "pnlCatalogFooter";
            pnlCatalogFooter.Padding = new Padding(0);
            pnlCatalogFooter.Size = new Size(600, 46);
            pnlCatalogFooter.TabIndex = 1;
            // 
            // lblCatCount
            // 
            lblCatCount.AutoEllipsis = true;
            lblCatCount.BackColor = Color.Transparent;
            lblCatCount.Dock = DockStyle.Fill;
            lblCatCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCatCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblCatCount.Name = "lblCatCount";
            lblCatCount.Padding = new Padding(18, 0, 8, 0);
            lblCatCount.TabIndex = 0;
            lblCatCount.Text = "";
            lblCatCount.TextAlign = ContentAlignment.MiddleLeft;
            lblCatCount.UseMnemonic = false;
            // 
            // flowCatalogFooter
            // 
            flowCatalogFooter.AutoSize = true;
            flowCatalogFooter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowCatalogFooter.BackColor = Color.Transparent;
            flowCatalogFooter.Dock = DockStyle.Right;
            flowCatalogFooter.Margin = new Padding(0);
            flowCatalogFooter.Name = "flowCatalogFooter";
            flowCatalogFooter.Padding = new Padding(0);
            flowCatalogFooter.TabIndex = 1;
            flowCatalogFooter.WrapContents = false;
            // 
            // pnlCatalogHeader
            // 
            pnlCatalogHeader.BackColor = Color.White;
            pnlCatalogHeader.Controls.Add(lblCatalogTitle);
            pnlCatalogHeader.Controls.Add(flowCatalogFilter);
            pnlCatalogHeader.Dock = DockStyle.Top;
            pnlCatalogHeader.Margin = new Padding(0);
            pnlCatalogHeader.Name = "pnlCatalogHeader";
            pnlCatalogHeader.Padding = new Padding(0);
            pnlCatalogHeader.Size = new Size(600, 46);
            pnlCatalogHeader.TabIndex = 2;
            // 
            // lblCatalogTitle
            // 
            lblCatalogTitle.AutoEllipsis = true;
            lblCatalogTitle.BackColor = Color.Transparent;
            lblCatalogTitle.Dock = DockStyle.Fill;
            lblCatalogTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCatalogTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblCatalogTitle.Margin = new Padding(0);
            lblCatalogTitle.Name = "lblCatalogTitle";
            lblCatalogTitle.Padding = new Padding(17, 0, 0, 0);
            lblCatalogTitle.TabIndex = 0;
            lblCatalogTitle.Text = "Laboratory test catalog";
            lblCatalogTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblCatalogTitle.UseMnemonic = false;
            // 
            // flowCatalogFilter
            // 
            flowCatalogFilter.AutoSize = true;
            flowCatalogFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowCatalogFilter.BackColor = Color.White;
            flowCatalogFilter.Controls.Add(txtCatSearch);
            flowCatalogFilter.Controls.Add(btnCatSearch);
            flowCatalogFilter.Controls.Add(btnCatAll);
            flowCatalogFilter.Dock = DockStyle.Right;
            flowCatalogFilter.Margin = new Padding(0);
            flowCatalogFilter.Name = "flowCatalogFilter";
            flowCatalogFilter.Padding = new Padding(0, 5, 18, 0);
            flowCatalogFilter.TabIndex = 1;
            flowCatalogFilter.WrapContents = false;
            // 
            // txtCatSearch
            // 
            txtCatSearch.AutoSize = false;
            txtCatSearch.BackColor = Color.White;
            txtCatSearch.BorderStyle = BorderStyle.FixedSingle;
            txtCatSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCatSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtCatSearch.Margin = new Padding(0);
            txtCatSearch.MaxLength = 60;
            txtCatSearch.Name = "txtCatSearch";
            txtCatSearch.PlaceholderText = "Test ID, name or category";
            txtCatSearch.Size = new Size(220, 36);
            txtCatSearch.TabIndex = 0;
            // 
            // btnCatSearch
            // 
            btnCatSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnCatSearch.FlatStyle = FlatStyle.Flat;
            btnCatSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCatSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnCatSearch.Margin = new Padding(10, 0, 0, 0);
            btnCatSearch.Name = "btnCatSearch";
            btnCatSearch.Size = new Size(86, 36);
            btnCatSearch.TabIndex = 1;
            btnCatSearch.Text = "Search";
            btnCatSearch.UseMnemonic = false;
            btnCatSearch.UseVisualStyleBackColor = false;
            // 
            // btnCatAll
            // 
            btnCatAll.BackColor = Color.White;
            btnCatAll.FlatStyle = FlatStyle.Flat;
            btnCatAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCatAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnCatAll.Margin = new Padding(10, 0, 0, 0);
            btnCatAll.Name = "btnCatAll";
            btnCatAll.Size = new Size(96, 36);
            btnCatAll.TabIndex = 2;
            btnCatAll.Text = "Show All";
            btnCatAll.UseMnemonic = false;
            btnCatAll.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // LaboratoryForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Laboratory Test Catalog & Result Entry";
            Controls.Add(tlpRoot);
            Name = "LaboratoryForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tlpMode.ResumeLayout(false);
            tlpMode.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            pnlHistory.ResumeLayout(false);
            pnlHistory.PerformLayout();
            tlpHistory.ResumeLayout(false);
            tlpHistory.PerformLayout();
            pnlResultsCard.ResumeLayout(false);
            pnlResultsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            pnlResultsFooter.ResumeLayout(false);
            pnlResultsFooter.PerformLayout();
            pnlResultsHeader.ResumeLayout(false);
            pnlResultsHeader.PerformLayout();
            flowHistFilter.ResumeLayout(false);
            flowHistFilter.PerformLayout();
            pnlDetailCard.ResumeLayout(false);
            pnlDetailCard.PerformLayout();
            pnlDetailBody.ResumeLayout(false);
            pnlDetailBody.PerformLayout();
            pnlDetailHeader.ResumeLayout(false);
            pnlDetailHeader.PerformLayout();
            pnlEntry.ResumeLayout(false);
            pnlEntry.PerformLayout();
            tlpEntry.ResumeLayout(false);
            tlpEntry.PerformLayout();
            pnlOrdersCard.ResumeLayout(false);
            pnlOrdersCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            pnlOrdersFooter.ResumeLayout(false);
            pnlOrdersFooter.PerformLayout();
            flowOrdersFooter.ResumeLayout(false);
            flowOrdersFooter.PerformLayout();
            pnlOrdersHeader.ResumeLayout(false);
            pnlOrdersHeader.PerformLayout();
            flowOrderFilter.ResumeLayout(false);
            flowOrderFilter.PerformLayout();
            pnlEntryCard.ResumeLayout(false);
            pnlEntryCard.PerformLayout();
            tlpEntryCard.ResumeLayout(false);
            tlpEntryCard.PerformLayout();
            pnlEntryScroll.ResumeLayout(false);
            pnlEntryScroll.PerformLayout();
            tlpEntryFields.ResumeLayout(false);
            tlpEntryFields.PerformLayout();
            tlpEntryA.ResumeLayout(false);
            tlpEntryA.PerformLayout();
            tlpEntryB.ResumeLayout(false);
            tlpEntryB.PerformLayout();
            tlpEntryButtons.ResumeLayout(false);
            tlpEntryButtons.PerformLayout();
            pnlCatalog.ResumeLayout(false);
            pnlCatalog.PerformLayout();
            tlpCatalog.ResumeLayout(false);
            tlpCatalog.PerformLayout();
            pnlTestCard.ResumeLayout(false);
            pnlTestCard.PerformLayout();
            tlpTestCard.ResumeLayout(false);
            tlpTestCard.PerformLayout();
            pnlTestScroll.ResumeLayout(false);
            pnlTestScroll.PerformLayout();
            tlpDetailsFields.ResumeLayout(false);
            tlpDetailsFields.PerformLayout();
            tlpDetails.ResumeLayout(false);
            tlpDetails.PerformLayout();
            tlpTestButtons.ResumeLayout(false);
            tlpTestButtons.PerformLayout();
            pnlCatalogCard.ResumeLayout(false);
            pnlCatalogCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTests).EndInit();
            pnlCatalogFooter.ResumeLayout(false);
            pnlCatalogFooter.PerformLayout();
            flowCatalogFooter.ResumeLayout(false);
            flowCatalogFooter.PerformLayout();
            pnlCatalogHeader.ResumeLayout(false);
            pnlCatalogHeader.PerformLayout();
            flowCatalogFilter.ResumeLayout(false);
            flowCatalogFilter.PerformLayout();
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
        private System.Windows.Forms.Button btnModeCatalog;
        private System.Windows.Forms.Button btnModeEntry;
        private System.Windows.Forms.Button btnModeHistory;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlHistory;
        private System.Windows.Forms.TableLayoutPanel tlpHistory;
        private System.Windows.Forms.Panel pnlResultsCard;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Panel pnlResultsFooter;
        private System.Windows.Forms.Label lblHistCount;
        private System.Windows.Forms.Label lblHistLegend;
        private System.Windows.Forms.Panel pnlResultsHeader;
        private System.Windows.Forms.Label lblResultsTitle;
        private System.Windows.Forms.FlowLayoutPanel flowHistFilter;
        private System.Windows.Forms.TextBox txtHistSearch;
        private System.Windows.Forms.Button btnHistSearch;
        private System.Windows.Forms.Button btnHistAll;
        private System.Windows.Forms.CheckBox chkAbnormalOnly;
        private System.Windows.Forms.Panel pnlDetailCard;
        private System.Windows.Forms.Panel pnlDetailBody;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Panel pnlDetailHeader;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Panel pnlEntry;
        private System.Windows.Forms.TableLayoutPanel tlpEntry;
        private System.Windows.Forms.Panel pnlOrdersCard;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Panel pnlOrdersFooter;
        private System.Windows.Forms.Label lblOrderCount;
        private System.Windows.Forms.FlowLayoutPanel flowOrdersFooter;
        private System.Windows.Forms.Panel pnlOrdersHeader;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.FlowLayoutPanel flowOrderFilter;
        private System.Windows.Forms.TextBox txtOrderSearch;
        private System.Windows.Forms.Button btnOrderSearch;
        private System.Windows.Forms.Button btnOrderAll;
        private System.Windows.Forms.Panel pnlEntryCard;
        private System.Windows.Forms.TableLayoutPanel tlpEntryCard;
        private System.Windows.Forms.Label lblEntryTitle;
        private System.Windows.Forms.Panel pnlEntryScroll;
        private System.Windows.Forms.TableLayoutPanel tlpEntryFields;
        private System.Windows.Forms.TableLayoutPanel tlpEntryA;
        private System.Windows.Forms.Label lblOrderCode;
        private System.Windows.Forms.TextBox txtOrderCode;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.TextBox txtPatient;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.TextBox txtTest;
        private System.Windows.Forms.Label lblRange;
        private System.Windows.Forms.TextBox txtRange;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblFlag;
        private System.Windows.Forms.TableLayoutPanel tlpEntryB;
        private System.Windows.Forms.Label lblTested;
        private System.Windows.Forms.DateTimePicker dtpTested;
        private System.Windows.Forms.Label lblTestedBy;
        private System.Windows.Forms.ComboBox cboTestedBy;
        private System.Windows.Forms.Label lblVerifiedBy;
        private System.Windows.Forms.ComboBox cboVerifiedBy;
        private System.Windows.Forms.Label lblFindings;
        private System.Windows.Forms.TextBox txtFindings;
        private System.Windows.Forms.TableLayoutPanel tlpEntryButtons;
        private System.Windows.Forms.Button btnEntrySave;
        private System.Windows.Forms.Button btnEntryClear;
        private System.Windows.Forms.Panel pnlCatalog;
        private System.Windows.Forms.TableLayoutPanel tlpCatalog;
        private System.Windows.Forms.Panel pnlTestCard;
        private System.Windows.Forms.TableLayoutPanel tlpTestCard;
        private System.Windows.Forms.Label lblTestTitle;
        private System.Windows.Forms.Panel pnlTestScroll;
        private System.Windows.Forms.TableLayoutPanel tlpDetailsFields;
        private System.Windows.Forms.TableLayoutPanel tlpDetails;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblSample;
        private System.Windows.Forms.ComboBox cboSample;
        private System.Windows.Forms.Label lblLow;
        private System.Windows.Forms.TextBox txtLow;
        private System.Windows.Forms.Label lblHigh;
        private System.Windows.Forms.TextBox txtHigh;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label lblTurnaround;
        private System.Windows.Forms.TextBox txtTurnaround;
        private System.Windows.Forms.CheckBox chkVerify;
        private System.Windows.Forms.Label lblRangeHint;
        private System.Windows.Forms.TableLayoutPanel tlpTestButtons;
        private System.Windows.Forms.Button btnTestSave;
        private System.Windows.Forms.Button btnTestNew;
        private System.Windows.Forms.Button btnTestDelete;
        private System.Windows.Forms.Panel pnlCatalogCard;
        private System.Windows.Forms.DataGridView dgvTests;
        private System.Windows.Forms.Panel pnlCatalogFooter;
        private System.Windows.Forms.Label lblCatCount;
        private System.Windows.Forms.FlowLayoutPanel flowCatalogFooter;
        private System.Windows.Forms.Panel pnlCatalogHeader;
        private System.Windows.Forms.Label lblCatalogTitle;
        private System.Windows.Forms.FlowLayoutPanel flowCatalogFilter;
        private System.Windows.Forms.TextBox txtCatSearch;
        private System.Windows.Forms.Button btnCatSearch;
        private System.Windows.Forms.Button btnCatAll;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
