namespace HospitalManagementSystem.Views.MP08_MedicineStock
{
    partial class MedicineStockForm
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

        // Redesign: page title + date line, Catalog / Stock toggle with the status on the right; (catalog) the
        // medicine details card beside the catalog table card; (stock) the stock table card (search and the
        // alerts filter in its header) beside the batches card with Add / Adjust Stock.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblDate = new Label();
            lblTitle = new Label();
            tlpMode = new TableLayoutPanel();
            btnModeCatalog = new Button();
            btnModeStock = new Button();
            lblStatus = new Label();
            pnlBody = new Panel();
            pnlStock = new Panel();
            tlpStock = new TableLayoutPanel();
            pnlStockCard = new Panel();
            dgvStock = new DataGridView();
            pnlStockFooter = new Panel();
            lblStockCount = new Label();
            lblLegend = new Label();
            pnlStockHeader = new Panel();
            lblStockTitle = new Label();
            flowStockFilter = new FlowLayoutPanel();
            txtStockSearch = new TextBox();
            btnStockSearch = new Button();
            btnStockAll = new Button();
            chkAlertsOnly = new CheckBox();
            pnlBatchesCard = new Panel();
            tlpBatches = new TableLayoutPanel();
            lblBatchesTitle = new Label();
            lblBatchInfo = new Label();
            dgvBatches = new DataGridView();
            lblFifoNote = new Label();
            tlpStockButtons = new TableLayoutPanel();
            btnAddStock = new Button();
            btnAdjustStock = new Button();
            pnlCatalog = new Panel();
            tlpCatalog = new TableLayoutPanel();
            pnlMedicineCard = new Panel();
            tlpMedicineCard = new TableLayoutPanel();
            lblMedicineTitle = new Label();
            pnlMedicineScroll = new Panel();
            tlpDetailsFields = new TableLayoutPanel();
            tlpDetails = new TableLayoutPanel();
            lblCode = new Label();
            txtCode = new TextBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblName = new Label();
            txtName = new TextBox();
            lblGeneric = new Label();
            txtGeneric = new TextBox();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            lblUnitType = new Label();
            cboUnitType = new ComboBox();
            lblManufacturer = new Label();
            cboManufacturer = new ComboBox();
            lblReorder = new Label();
            txtReorder = new TextBox();
            lblStorage = new Label();
            txtStorage = new TextBox();
            tlpMedButtons = new TableLayoutPanel();
            btnMedSave = new Button();
            btnMedNew = new Button();
            btnMedDelete = new Button();
            pnlCatalogCard = new Panel();
            dgvMedicines = new DataGridView();
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
            pnlStock.SuspendLayout();
            tlpStock.SuspendLayout();
            pnlStockCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStock).BeginInit();
            pnlStockFooter.SuspendLayout();
            pnlStockHeader.SuspendLayout();
            flowStockFilter.SuspendLayout();
            pnlBatchesCard.SuspendLayout();
            tlpBatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBatches).BeginInit();
            tlpStockButtons.SuspendLayout();
            pnlCatalog.SuspendLayout();
            tlpCatalog.SuspendLayout();
            pnlMedicineCard.SuspendLayout();
            tlpMedicineCard.SuspendLayout();
            pnlMedicineScroll.SuspendLayout();
            tlpDetailsFields.SuspendLayout();
            tlpDetails.SuspendLayout();
            tlpMedButtons.SuspendLayout();
            pnlCatalogCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMedicines).BeginInit();
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
            lblDate.Text = "Keep the medicine catalog and stock up to date.";
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
            lblTitle.Text = "Medicine Catalog & Stock";
            lblTitle.UseMnemonic = false;
            // 
            // tlpMode
            // 
            tlpMode.BackColor = Color.Transparent;
            tlpMode.ColumnCount = 3;
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMode.Controls.Add(btnModeCatalog, 0, 0);
            tlpMode.Controls.Add(btnModeStock, 1, 0);
            tlpMode.Controls.Add(lblStatus, 2, 0);
            tlpMode.Dock = DockStyle.Fill;
            tlpMode.Margin = new Padding(0, 0, 0, 18);
            tlpMode.Name = "tlpMode";
            tlpMode.RowCount = 1;
            tlpMode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMode.TabIndex = 1;
            // 
            // btnModeCatalog
            // 
            btnModeCatalog.BackColor = Color.FromArgb(18, 122, 140);
            btnModeCatalog.FlatStyle = FlatStyle.Flat;
            btnModeCatalog.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeCatalog.ForeColor = Color.White;
            btnModeCatalog.Margin = new Padding(0, 0, 8, 0);
            btnModeCatalog.Name = "btnModeCatalog";
            btnModeCatalog.Size = new Size(180, 38);
            btnModeCatalog.TabIndex = 0;
            btnModeCatalog.Text = "Medicine Catalog";
            btnModeCatalog.UseMnemonic = false;
            btnModeCatalog.UseVisualStyleBackColor = false;
            // 
            // btnModeStock
            // 
            btnModeStock.BackColor = Color.White;
            btnModeStock.FlatStyle = FlatStyle.Flat;
            btnModeStock.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeStock.ForeColor = Color.FromArgb(20, 34, 51);
            btnModeStock.Margin = new Padding(0);
            btnModeStock.Name = "btnModeStock";
            btnModeStock.Size = new Size(180, 38);
            btnModeStock.TabIndex = 1;
            btnModeStock.Text = "Stock and Alerts";
            btnModeStock.UseMnemonic = false;
            btnModeStock.UseVisualStyleBackColor = false;
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
            pnlBody.Controls.Add(pnlStock);
            pnlBody.Controls.Add(pnlCatalog);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Margin = new Padding(0);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(0);
            pnlBody.TabIndex = 2;
            // 
            // pnlStock
            // 
            pnlStock.BackColor = Color.Transparent;
            pnlStock.Controls.Add(tlpStock);
            pnlStock.Dock = DockStyle.Fill;
            pnlStock.Margin = new Padding(0);
            pnlStock.Name = "pnlStock";
            pnlStock.Padding = new Padding(0);
            pnlStock.TabIndex = 0;
            pnlStock.Visible = false;
            // 
            // tlpStock
            // 
            tlpStock.BackColor = Color.Transparent;
            tlpStock.ColumnCount = 2;
            tlpStock.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpStock.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 460F));
            tlpStock.Controls.Add(pnlStockCard, 0, 0);
            tlpStock.Controls.Add(pnlBatchesCard, 1, 0);
            tlpStock.Dock = DockStyle.Fill;
            tlpStock.Margin = new Padding(0);
            tlpStock.Name = "tlpStock";
            tlpStock.RowCount = 1;
            tlpStock.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpStock.TabIndex = 0;
            // 
            // pnlStockCard
            // 
            pnlStockCard.BackColor = Color.White;
            pnlStockCard.Controls.Add(dgvStock);
            pnlStockCard.Controls.Add(pnlStockFooter);
            pnlStockCard.Controls.Add(pnlStockHeader);
            pnlStockCard.Dock = DockStyle.Fill;
            pnlStockCard.Margin = new Padding(0, 0, 20, 0);
            pnlStockCard.Name = "pnlStockCard";
            pnlStockCard.Padding = new Padding(1, 6, 1, 1);
            pnlStockCard.TabIndex = 0;
            // 
            // dgvStock
            // 
            dgvStock.AllowUserToAddRows = false;
            dgvStock.AllowUserToDeleteRows = false;
            dgvStock.AllowUserToResizeRows = false;
            dgvStock.BackgroundColor = Color.White;
            dgvStock.BorderStyle = BorderStyle.None;
            dgvStock.Dock = DockStyle.Fill;
            dgvStock.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvStock.Margin = new Padding(0);
            dgvStock.MultiSelect = false;
            dgvStock.Name = "dgvStock";
            dgvStock.ReadOnly = true;
            dgvStock.RowHeadersVisible = false;
            dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStock.TabIndex = 0;
            // 
            // pnlStockFooter
            // 
            pnlStockFooter.BackColor = Color.White;
            pnlStockFooter.Controls.Add(lblStockCount);
            pnlStockFooter.Controls.Add(lblLegend);
            pnlStockFooter.Dock = DockStyle.Bottom;
            pnlStockFooter.Margin = new Padding(0);
            pnlStockFooter.Name = "pnlStockFooter";
            pnlStockFooter.Padding = new Padding(0);
            pnlStockFooter.Size = new Size(600, 46);
            pnlStockFooter.TabIndex = 1;
            // 
            // lblStockCount
            // 
            lblStockCount.AutoEllipsis = true;
            lblStockCount.BackColor = Color.Transparent;
            lblStockCount.Dock = DockStyle.Fill;
            lblStockCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStockCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblStockCount.Name = "lblStockCount";
            lblStockCount.Padding = new Padding(18, 0, 8, 0);
            lblStockCount.TabIndex = 0;
            lblStockCount.TextAlign = ContentAlignment.MiddleLeft;
            lblStockCount.UseMnemonic = false;
            // 
            // lblLegend
            // 
            lblLegend.BackColor = Color.Transparent;
            lblLegend.Dock = DockStyle.Right;
            lblLegend.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLegend.ForeColor = Color.FromArgb(91, 107, 124);
            lblLegend.Margin = new Padding(0);
            lblLegend.Name = "lblLegend";
            lblLegend.Padding = new Padding(0, 0, 18, 0);
            lblLegend.Size = new Size(360, 46);
            lblLegend.TabIndex = 1;
            lblLegend.Text = "Statuses: Expired  ·  Expiring soon  ·  Low stock";
            lblLegend.TextAlign = ContentAlignment.MiddleRight;
            lblLegend.UseMnemonic = false;
            // 
            // pnlStockHeader
            // 
            pnlStockHeader.BackColor = Color.White;
            pnlStockHeader.Controls.Add(lblStockTitle);
            pnlStockHeader.Controls.Add(flowStockFilter);
            pnlStockHeader.Dock = DockStyle.Top;
            pnlStockHeader.Margin = new Padding(0);
            pnlStockHeader.Name = "pnlStockHeader";
            pnlStockHeader.Padding = new Padding(0);
            pnlStockHeader.Size = new Size(600, 46);
            pnlStockHeader.TabIndex = 2;
            // 
            // lblStockTitle
            // 
            lblStockTitle.AutoEllipsis = true;
            lblStockTitle.BackColor = Color.Transparent;
            lblStockTitle.Dock = DockStyle.Fill;
            lblStockTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStockTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblStockTitle.Margin = new Padding(0);
            lblStockTitle.Name = "lblStockTitle";
            lblStockTitle.Padding = new Padding(17, 0, 0, 0);
            lblStockTitle.TabIndex = 0;
            lblStockTitle.Text = "Stock per medicine";
            lblStockTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblStockTitle.UseMnemonic = false;
            // 
            // flowStockFilter
            // 
            flowStockFilter.AutoSize = true;
            flowStockFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowStockFilter.BackColor = Color.White;
            flowStockFilter.Controls.Add(txtStockSearch);
            flowStockFilter.Controls.Add(btnStockSearch);
            flowStockFilter.Controls.Add(btnStockAll);
            flowStockFilter.Controls.Add(chkAlertsOnly);
            flowStockFilter.Dock = DockStyle.Right;
            flowStockFilter.Margin = new Padding(0);
            flowStockFilter.Name = "flowStockFilter";
            flowStockFilter.Padding = new Padding(0, 5, 18, 0);
            flowStockFilter.TabIndex = 1;
            flowStockFilter.WrapContents = false;
            // 
            // txtStockSearch
            // 
            txtStockSearch.AutoSize = false;
            txtStockSearch.BackColor = Color.White;
            txtStockSearch.BorderStyle = BorderStyle.FixedSingle;
            txtStockSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStockSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtStockSearch.Margin = new Padding(0);
            txtStockSearch.MaxLength = 60;
            txtStockSearch.Name = "txtStockSearch";
            txtStockSearch.PlaceholderText = "Medicine name or code";
            txtStockSearch.Size = new Size(200, 36);
            txtStockSearch.TabIndex = 0;
            // 
            // btnStockSearch
            // 
            btnStockSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnStockSearch.FlatStyle = FlatStyle.Flat;
            btnStockSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStockSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnStockSearch.Margin = new Padding(10, 0, 0, 0);
            btnStockSearch.Name = "btnStockSearch";
            btnStockSearch.Size = new Size(86, 36);
            btnStockSearch.TabIndex = 1;
            btnStockSearch.Text = "Search";
            btnStockSearch.UseMnemonic = false;
            btnStockSearch.UseVisualStyleBackColor = false;
            // 
            // btnStockAll
            // 
            btnStockAll.BackColor = Color.White;
            btnStockAll.FlatStyle = FlatStyle.Flat;
            btnStockAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStockAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnStockAll.Margin = new Padding(10, 0, 0, 0);
            btnStockAll.Name = "btnStockAll";
            btnStockAll.Size = new Size(96, 36);
            btnStockAll.TabIndex = 2;
            btnStockAll.Text = "Show All";
            btnStockAll.UseMnemonic = false;
            btnStockAll.UseVisualStyleBackColor = false;
            // 
            // chkAlertsOnly
            // 
            chkAlertsOnly.AutoSize = true;
            chkAlertsOnly.BackColor = Color.Transparent;
            chkAlertsOnly.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkAlertsOnly.ForeColor = Color.FromArgb(20, 34, 51);
            chkAlertsOnly.Margin = new Padding(14, 8, 0, 0);
            chkAlertsOnly.Name = "chkAlertsOnly";
            chkAlertsOnly.TabIndex = 3;
            chkAlertsOnly.Text = "Show only alerts";
            chkAlertsOnly.UseMnemonic = false;
            chkAlertsOnly.UseVisualStyleBackColor = false;
            // 
            // pnlBatchesCard
            // 
            pnlBatchesCard.BackColor = Color.White;
            pnlBatchesCard.Controls.Add(tlpBatches);
            pnlBatchesCard.Dock = DockStyle.Fill;
            pnlBatchesCard.Margin = new Padding(0);
            pnlBatchesCard.Name = "pnlBatchesCard";
            pnlBatchesCard.Padding = new Padding(20);
            pnlBatchesCard.TabIndex = 1;
            // 
            // tlpBatches
            // 
            tlpBatches.BackColor = Color.Transparent;
            tlpBatches.ColumnCount = 1;
            tlpBatches.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBatches.Controls.Add(lblBatchesTitle, 0, 0);
            tlpBatches.Controls.Add(lblBatchInfo, 0, 1);
            tlpBatches.Controls.Add(dgvBatches, 0, 2);
            tlpBatches.Controls.Add(lblFifoNote, 0, 3);
            tlpBatches.Controls.Add(tlpStockButtons, 0, 4);
            tlpBatches.Dock = DockStyle.Fill;
            tlpBatches.Margin = new Padding(0);
            tlpBatches.Name = "tlpBatches";
            tlpBatches.RowCount = 5;
            tlpBatches.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBatches.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBatches.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBatches.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBatches.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpBatches.TabIndex = 0;
            // 
            // lblBatchesTitle
            // 
            lblBatchesTitle.AutoSize = true;
            lblBatchesTitle.BackColor = Color.Transparent;
            lblBatchesTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBatchesTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblBatchesTitle.Margin = new Padding(0, 0, 0, 6);
            lblBatchesTitle.Name = "lblBatchesTitle";
            lblBatchesTitle.TabIndex = 0;
            lblBatchesTitle.Text = "Batches of the selected medicine";
            // 
            // lblBatchInfo
            // 
            lblBatchInfo.AutoSize = true;
            lblBatchInfo.BackColor = Color.Transparent;
            lblBatchInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBatchInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblBatchInfo.Margin = new Padding(0, 0, 0, 12);
            lblBatchInfo.MaximumSize = new Size(420, 0);
            lblBatchInfo.Name = "lblBatchInfo";
            lblBatchInfo.TabIndex = 1;
            lblBatchInfo.Text = "Select a medicine to see its batches";
            lblBatchInfo.UseMnemonic = false;
            // 
            // dgvBatches
            // 
            dgvBatches.AllowUserToAddRows = false;
            dgvBatches.AllowUserToDeleteRows = false;
            dgvBatches.AllowUserToResizeRows = false;
            dgvBatches.BackgroundColor = Color.White;
            dgvBatches.BorderStyle = BorderStyle.None;
            dgvBatches.Dock = DockStyle.Fill;
            dgvBatches.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvBatches.Margin = new Padding(0, 0, 0, 12);
            dgvBatches.MultiSelect = false;
            dgvBatches.Name = "dgvBatches";
            dgvBatches.ReadOnly = true;
            dgvBatches.RowHeadersVisible = false;
            dgvBatches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBatches.TabIndex = 2;
            // 
            // lblFifoNote
            // 
            lblFifoNote.AutoSize = true;
            lblFifoNote.BackColor = Color.Transparent;
            lblFifoNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFifoNote.ForeColor = Color.FromArgb(91, 107, 124);
            lblFifoNote.Margin = new Padding(0, 0, 0, 14);
            lblFifoNote.MaximumSize = new Size(420, 0);
            lblFifoNote.Name = "lblFifoNote";
            lblFifoNote.TabIndex = 3;
            lblFifoNote.Text = "Use order = FIFO: the batch that expires first is used first. Empty and expired batches are skipped.";
            lblFifoNote.UseMnemonic = false;
            // 
            // tlpStockButtons
            // 
            tlpStockButtons.BackColor = Color.Transparent;
            tlpStockButtons.ColumnCount = 2;
            tlpStockButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpStockButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpStockButtons.Controls.Add(btnAddStock, 0, 0);
            tlpStockButtons.Controls.Add(btnAdjustStock, 1, 0);
            tlpStockButtons.Dock = DockStyle.Fill;
            tlpStockButtons.Margin = new Padding(0);
            tlpStockButtons.Name = "tlpStockButtons";
            tlpStockButtons.RowCount = 1;
            tlpStockButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpStockButtons.TabIndex = 4;
            // 
            // btnAddStock
            // 
            btnAddStock.BackColor = Color.FromArgb(18, 122, 140);
            btnAddStock.Dock = DockStyle.Fill;
            btnAddStock.Enabled = false;
            btnAddStock.FlatStyle = FlatStyle.Flat;
            btnAddStock.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAddStock.ForeColor = Color.White;
            btnAddStock.Margin = new Padding(0);
            btnAddStock.Name = "btnAddStock";
            btnAddStock.Size = new Size(120, 40);
            btnAddStock.TabIndex = 0;
            btnAddStock.Text = "Add Stock...";
            btnAddStock.UseMnemonic = false;
            btnAddStock.UseVisualStyleBackColor = false;
            // 
            // btnAdjustStock
            // 
            btnAdjustStock.BackColor = Color.White;
            btnAdjustStock.Dock = DockStyle.Fill;
            btnAdjustStock.Enabled = false;
            btnAdjustStock.FlatStyle = FlatStyle.Flat;
            btnAdjustStock.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAdjustStock.ForeColor = Color.FromArgb(20, 34, 51);
            btnAdjustStock.Margin = new Padding(10, 0, 0, 0);
            btnAdjustStock.Name = "btnAdjustStock";
            btnAdjustStock.Size = new Size(120, 40);
            btnAdjustStock.TabIndex = 1;
            btnAdjustStock.Text = "Adjust Stock...";
            btnAdjustStock.UseMnemonic = false;
            btnAdjustStock.UseVisualStyleBackColor = false;
            // 
            // pnlCatalog
            // 
            pnlCatalog.BackColor = Color.Transparent;
            pnlCatalog.Controls.Add(tlpCatalog);
            pnlCatalog.Dock = DockStyle.Fill;
            pnlCatalog.Margin = new Padding(0);
            pnlCatalog.Name = "pnlCatalog";
            pnlCatalog.Padding = new Padding(0);
            pnlCatalog.TabIndex = 1;
            // 
            // tlpCatalog
            // 
            tlpCatalog.BackColor = Color.Transparent;
            tlpCatalog.ColumnCount = 2;
            tlpCatalog.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 480F));
            tlpCatalog.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCatalog.Controls.Add(pnlMedicineCard, 0, 0);
            tlpCatalog.Controls.Add(pnlCatalogCard, 1, 0);
            tlpCatalog.Dock = DockStyle.Fill;
            tlpCatalog.Margin = new Padding(0);
            tlpCatalog.Name = "tlpCatalog";
            tlpCatalog.RowCount = 1;
            tlpCatalog.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCatalog.TabIndex = 0;
            // 
            // pnlMedicineCard
            // 
            pnlMedicineCard.BackColor = Color.White;
            pnlMedicineCard.Controls.Add(tlpMedicineCard);
            pnlMedicineCard.Dock = DockStyle.Fill;
            pnlMedicineCard.Margin = new Padding(0, 0, 20, 0);
            pnlMedicineCard.Name = "pnlMedicineCard";
            pnlMedicineCard.Padding = new Padding(20);
            pnlMedicineCard.TabIndex = 0;
            // 
            // tlpMedicineCard
            // 
            tlpMedicineCard.BackColor = Color.Transparent;
            tlpMedicineCard.ColumnCount = 1;
            tlpMedicineCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMedicineCard.Controls.Add(lblMedicineTitle, 0, 0);
            tlpMedicineCard.Controls.Add(pnlMedicineScroll, 0, 1);
            tlpMedicineCard.Controls.Add(tlpMedButtons, 0, 2);
            tlpMedicineCard.Dock = DockStyle.Fill;
            tlpMedicineCard.Margin = new Padding(0);
            tlpMedicineCard.Name = "tlpMedicineCard";
            tlpMedicineCard.RowCount = 3;
            tlpMedicineCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpMedicineCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMedicineCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpMedicineCard.TabIndex = 0;
            // 
            // lblMedicineTitle
            // 
            lblMedicineTitle.AutoSize = true;
            lblMedicineTitle.BackColor = Color.Transparent;
            lblMedicineTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMedicineTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblMedicineTitle.Margin = new Padding(0, 0, 0, 14);
            lblMedicineTitle.Name = "lblMedicineTitle";
            lblMedicineTitle.TabIndex = 0;
            lblMedicineTitle.Text = "Medicine details  ·  new medicine";
            lblMedicineTitle.UseMnemonic = false;
            // 
            // pnlMedicineScroll
            // 
            pnlMedicineScroll.AutoScroll = true;
            pnlMedicineScroll.BackColor = Color.Transparent;
            pnlMedicineScroll.Controls.Add(tlpDetailsFields);
            pnlMedicineScroll.Dock = DockStyle.Fill;
            pnlMedicineScroll.Margin = new Padding(0, 0, 0, 14);
            pnlMedicineScroll.Name = "pnlMedicineScroll";
            pnlMedicineScroll.Padding = new Padding(0, 0, 6, 0);
            pnlMedicineScroll.TabIndex = 1;
            // 
            // tlpDetailsFields
            // 
            tlpDetailsFields.AutoSize = true;
            tlpDetailsFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpDetailsFields.BackColor = Color.Transparent;
            tlpDetailsFields.ColumnCount = 1;
            tlpDetailsFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDetailsFields.Controls.Add(tlpDetails, 0, 0);
            tlpDetailsFields.Dock = DockStyle.Top;
            tlpDetailsFields.Margin = new Padding(0);
            tlpDetailsFields.Name = "tlpDetailsFields";
            tlpDetailsFields.RowCount = 1;
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
            tlpDetails.Controls.Add(lblGeneric, 0, 4);
            tlpDetails.SetColumnSpan(lblGeneric, 2);
            tlpDetails.Controls.Add(txtGeneric, 0, 5);
            tlpDetails.SetColumnSpan(txtGeneric, 2);
            tlpDetails.Controls.Add(lblCategory, 0, 6);
            tlpDetails.Controls.Add(cboCategory, 0, 7);
            tlpDetails.Controls.Add(lblUnitType, 1, 6);
            tlpDetails.Controls.Add(cboUnitType, 1, 7);
            tlpDetails.Controls.Add(lblManufacturer, 0, 8);
            tlpDetails.Controls.Add(cboManufacturer, 0, 9);
            tlpDetails.Controls.Add(lblReorder, 1, 8);
            tlpDetails.Controls.Add(txtReorder, 1, 9);
            tlpDetails.Controls.Add(lblStorage, 0, 10);
            tlpDetails.SetColumnSpan(lblStorage, 2);
            tlpDetails.Controls.Add(txtStorage, 0, 11);
            tlpDetails.SetColumnSpan(txtStorage, 2);
            tlpDetails.Dock = DockStyle.Top;
            tlpDetails.Margin = new Padding(0);
            tlpDetails.Name = "tlpDetails";
            tlpDetails.RowCount = 12;
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
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
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
            lblCode.Text = "Medicine ID";
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
            txtCode.TabStop = false;
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
            lblPrice.Text = "Unit price *";
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
            txtPrice.PlaceholderText = "above 0, 2 decimals";
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
            lblName.Text = "Medicine name *";
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
            txtName.PlaceholderText = "e.g. Paracetamol 500mg";
            txtName.Size = new Size(200, 40);
            txtName.TabIndex = 5;
            // 
            // lblGeneric
            // 
            lblGeneric.AutoSize = true;
            lblGeneric.BackColor = Color.Transparent;
            lblGeneric.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGeneric.ForeColor = Color.FromArgb(20, 34, 51);
            lblGeneric.Margin = new Padding(0, 0, 0, 6);
            lblGeneric.Name = "lblGeneric";
            lblGeneric.TabIndex = 6;
            lblGeneric.Text = "Generic name";
            lblGeneric.UseMnemonic = false;
            // 
            // txtGeneric
            // 
            txtGeneric.AutoSize = false;
            txtGeneric.BackColor = Color.White;
            txtGeneric.BorderStyle = BorderStyle.FixedSingle;
            txtGeneric.Dock = DockStyle.Fill;
            txtGeneric.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtGeneric.ForeColor = Color.FromArgb(20, 34, 51);
            txtGeneric.Margin = new Padding(0, 0, 0, 14);
            txtGeneric.MaxLength = 120;
            txtGeneric.Name = "txtGeneric";
            txtGeneric.Size = new Size(200, 40);
            txtGeneric.TabIndex = 7;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.BackColor = Color.Transparent;
            lblCategory.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCategory.ForeColor = Color.FromArgb(20, 34, 51);
            lblCategory.Margin = new Padding(0, 0, 6, 6);
            lblCategory.Name = "lblCategory";
            lblCategory.TabIndex = 8;
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
            cboCategory.TabIndex = 9;
            // 
            // lblUnitType
            // 
            lblUnitType.AutoSize = true;
            lblUnitType.BackColor = Color.Transparent;
            lblUnitType.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUnitType.ForeColor = Color.FromArgb(20, 34, 51);
            lblUnitType.Margin = new Padding(6, 0, 0, 6);
            lblUnitType.Name = "lblUnitType";
            lblUnitType.TabIndex = 10;
            lblUnitType.Text = "Unit type *";
            lblUnitType.UseMnemonic = false;
            // 
            // cboUnitType
            // 
            cboUnitType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboUnitType.DropDownStyle = ComboBoxStyle.DropDown;
            cboUnitType.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboUnitType.FormattingEnabled = true;
            cboUnitType.Margin = new Padding(6, 0, 0, 14);
            cboUnitType.Name = "cboUnitType";
            cboUnitType.Size = new Size(200, 32);
            cboUnitType.TabIndex = 11;
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.BackColor = Color.Transparent;
            lblManufacturer.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblManufacturer.ForeColor = Color.FromArgb(20, 34, 51);
            lblManufacturer.Margin = new Padding(0, 0, 6, 6);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.TabIndex = 12;
            lblManufacturer.Text = "Manufacturer";
            lblManufacturer.UseMnemonic = false;
            // 
            // cboManufacturer
            // 
            cboManufacturer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboManufacturer.DropDownStyle = ComboBoxStyle.DropDown;
            cboManufacturer.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboManufacturer.FormattingEnabled = true;
            cboManufacturer.Margin = new Padding(0, 0, 6, 14);
            cboManufacturer.Name = "cboManufacturer";
            cboManufacturer.Size = new Size(200, 32);
            cboManufacturer.TabIndex = 13;
            // 
            // lblReorder
            // 
            lblReorder.AutoSize = true;
            lblReorder.BackColor = Color.Transparent;
            lblReorder.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReorder.ForeColor = Color.FromArgb(20, 34, 51);
            lblReorder.Margin = new Padding(6, 0, 0, 6);
            lblReorder.Name = "lblReorder";
            lblReorder.TabIndex = 14;
            lblReorder.Text = "Reorder level *";
            lblReorder.UseMnemonic = false;
            // 
            // txtReorder
            // 
            txtReorder.AutoSize = false;
            txtReorder.BackColor = Color.White;
            txtReorder.BorderStyle = BorderStyle.FixedSingle;
            txtReorder.Dock = DockStyle.Fill;
            txtReorder.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtReorder.ForeColor = Color.FromArgb(20, 34, 51);
            txtReorder.Margin = new Padding(6, 0, 0, 14);
            txtReorder.MaxLength = 7;
            txtReorder.Name = "txtReorder";
            txtReorder.PlaceholderText = "whole number, 0 or more";
            txtReorder.Size = new Size(200, 40);
            txtReorder.TabIndex = 15;
            // 
            // lblStorage
            // 
            lblStorage.AutoSize = true;
            lblStorage.BackColor = Color.Transparent;
            lblStorage.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStorage.ForeColor = Color.FromArgb(20, 34, 51);
            lblStorage.Margin = new Padding(0, 0, 0, 6);
            lblStorage.Name = "lblStorage";
            lblStorage.TabIndex = 16;
            lblStorage.Text = "Storage instructions";
            lblStorage.UseMnemonic = false;
            // 
            // txtStorage
            // 
            txtStorage.AutoSize = false;
            txtStorage.BackColor = Color.White;
            txtStorage.BorderStyle = BorderStyle.FixedSingle;
            txtStorage.Dock = DockStyle.Fill;
            txtStorage.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtStorage.ForeColor = Color.FromArgb(20, 34, 51);
            txtStorage.Margin = new Padding(0, 0, 0, 14);
            txtStorage.MaxLength = 250;
            txtStorage.Multiline = true;
            txtStorage.Name = "txtStorage";
            txtStorage.ScrollBars = ScrollBars.Vertical;
            txtStorage.Size = new Size(200, 76);
            txtStorage.TabIndex = 17;
            // 
            // tlpMedButtons
            // 
            tlpMedButtons.BackColor = Color.Transparent;
            tlpMedButtons.ColumnCount = 3;
            tlpMedButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMedButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpMedButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpMedButtons.Controls.Add(btnMedSave, 0, 0);
            tlpMedButtons.Controls.Add(btnMedNew, 1, 0);
            tlpMedButtons.Controls.Add(btnMedDelete, 2, 0);
            tlpMedButtons.Dock = DockStyle.Fill;
            tlpMedButtons.Margin = new Padding(0);
            tlpMedButtons.Name = "tlpMedButtons";
            tlpMedButtons.RowCount = 1;
            tlpMedButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMedButtons.TabIndex = 2;
            // 
            // btnMedSave
            // 
            btnMedSave.BackColor = Color.FromArgb(18, 122, 140);
            btnMedSave.Dock = DockStyle.Fill;
            btnMedSave.FlatStyle = FlatStyle.Flat;
            btnMedSave.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMedSave.ForeColor = Color.White;
            btnMedSave.Margin = new Padding(0);
            btnMedSave.Name = "btnMedSave";
            btnMedSave.Size = new Size(120, 40);
            btnMedSave.TabIndex = 0;
            btnMedSave.Text = "Save Medicine";
            btnMedSave.UseMnemonic = false;
            btnMedSave.UseVisualStyleBackColor = false;
            // 
            // btnMedNew
            // 
            btnMedNew.BackColor = Color.White;
            btnMedNew.Dock = DockStyle.Fill;
            btnMedNew.FlatStyle = FlatStyle.Flat;
            btnMedNew.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMedNew.ForeColor = Color.FromArgb(20, 34, 51);
            btnMedNew.Margin = new Padding(10, 0, 0, 0);
            btnMedNew.Name = "btnMedNew";
            btnMedNew.Size = new Size(120, 40);
            btnMedNew.TabIndex = 1;
            btnMedNew.Text = "New";
            btnMedNew.UseMnemonic = false;
            btnMedNew.UseVisualStyleBackColor = false;
            // 
            // btnMedDelete
            // 
            btnMedDelete.BackColor = Color.FromArgb(180, 35, 24);
            btnMedDelete.Dock = DockStyle.Fill;
            btnMedDelete.Enabled = false;
            btnMedDelete.FlatStyle = FlatStyle.Flat;
            btnMedDelete.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMedDelete.ForeColor = Color.White;
            btnMedDelete.Margin = new Padding(10, 0, 0, 0);
            btnMedDelete.Name = "btnMedDelete";
            btnMedDelete.Size = new Size(120, 40);
            btnMedDelete.TabIndex = 2;
            btnMedDelete.Text = "Delete";
            btnMedDelete.UseMnemonic = false;
            btnMedDelete.UseVisualStyleBackColor = false;
            // 
            // pnlCatalogCard
            // 
            pnlCatalogCard.BackColor = Color.White;
            pnlCatalogCard.Controls.Add(dgvMedicines);
            pnlCatalogCard.Controls.Add(pnlCatalogFooter);
            pnlCatalogCard.Controls.Add(pnlCatalogHeader);
            pnlCatalogCard.Dock = DockStyle.Fill;
            pnlCatalogCard.Margin = new Padding(0);
            pnlCatalogCard.Name = "pnlCatalogCard";
            pnlCatalogCard.Padding = new Padding(1, 6, 1, 1);
            pnlCatalogCard.TabIndex = 1;
            // 
            // dgvMedicines
            // 
            dgvMedicines.AllowUserToAddRows = false;
            dgvMedicines.AllowUserToDeleteRows = false;
            dgvMedicines.AllowUserToResizeRows = false;
            dgvMedicines.BackgroundColor = Color.White;
            dgvMedicines.BorderStyle = BorderStyle.None;
            dgvMedicines.Dock = DockStyle.Fill;
            dgvMedicines.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvMedicines.Margin = new Padding(0);
            dgvMedicines.MultiSelect = false;
            dgvMedicines.Name = "dgvMedicines";
            dgvMedicines.ReadOnly = true;
            dgvMedicines.RowHeadersVisible = false;
            dgvMedicines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMedicines.TabIndex = 0;
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
            lblCatalogTitle.Text = "Medicines in the catalog";
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
            txtCatSearch.PlaceholderText = "Name, generic name or code";
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
            // MedicineStockForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Medicine Catalog & Stock Manager";
            Controls.Add(tlpRoot);
            Name = "MedicineStockForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tlpMode.ResumeLayout(false);
            tlpMode.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            pnlStock.ResumeLayout(false);
            pnlStock.PerformLayout();
            tlpStock.ResumeLayout(false);
            tlpStock.PerformLayout();
            pnlStockCard.ResumeLayout(false);
            pnlStockCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStock).EndInit();
            pnlStockFooter.ResumeLayout(false);
            pnlStockFooter.PerformLayout();
            pnlStockHeader.ResumeLayout(false);
            pnlStockHeader.PerformLayout();
            flowStockFilter.ResumeLayout(false);
            flowStockFilter.PerformLayout();
            pnlBatchesCard.ResumeLayout(false);
            pnlBatchesCard.PerformLayout();
            tlpBatches.ResumeLayout(false);
            tlpBatches.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBatches).EndInit();
            tlpStockButtons.ResumeLayout(false);
            tlpStockButtons.PerformLayout();
            pnlCatalog.ResumeLayout(false);
            pnlCatalog.PerformLayout();
            tlpCatalog.ResumeLayout(false);
            tlpCatalog.PerformLayout();
            pnlMedicineCard.ResumeLayout(false);
            pnlMedicineCard.PerformLayout();
            tlpMedicineCard.ResumeLayout(false);
            tlpMedicineCard.PerformLayout();
            pnlMedicineScroll.ResumeLayout(false);
            pnlMedicineScroll.PerformLayout();
            tlpDetailsFields.ResumeLayout(false);
            tlpDetailsFields.PerformLayout();
            tlpDetails.ResumeLayout(false);
            tlpDetails.PerformLayout();
            tlpMedButtons.ResumeLayout(false);
            tlpMedButtons.PerformLayout();
            pnlCatalogCard.ResumeLayout(false);
            pnlCatalogCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMedicines).EndInit();
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
        private System.Windows.Forms.Button btnModeStock;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlStock;
        private System.Windows.Forms.TableLayoutPanel tlpStock;
        private System.Windows.Forms.Panel pnlStockCard;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.Panel pnlStockFooter;
        private System.Windows.Forms.Label lblStockCount;
        private System.Windows.Forms.Label lblLegend;
        private System.Windows.Forms.Panel pnlStockHeader;
        private System.Windows.Forms.Label lblStockTitle;
        private System.Windows.Forms.FlowLayoutPanel flowStockFilter;
        private System.Windows.Forms.TextBox txtStockSearch;
        private System.Windows.Forms.Button btnStockSearch;
        private System.Windows.Forms.Button btnStockAll;
        private System.Windows.Forms.CheckBox chkAlertsOnly;
        private System.Windows.Forms.Panel pnlBatchesCard;
        private System.Windows.Forms.TableLayoutPanel tlpBatches;
        private System.Windows.Forms.Label lblBatchesTitle;
        private System.Windows.Forms.Label lblBatchInfo;
        private System.Windows.Forms.DataGridView dgvBatches;
        private System.Windows.Forms.Label lblFifoNote;
        private System.Windows.Forms.TableLayoutPanel tlpStockButtons;
        private System.Windows.Forms.Button btnAddStock;
        private System.Windows.Forms.Button btnAdjustStock;
        private System.Windows.Forms.Panel pnlCatalog;
        private System.Windows.Forms.TableLayoutPanel tlpCatalog;
        private System.Windows.Forms.Panel pnlMedicineCard;
        private System.Windows.Forms.TableLayoutPanel tlpMedicineCard;
        private System.Windows.Forms.Label lblMedicineTitle;
        private System.Windows.Forms.Panel pnlMedicineScroll;
        private System.Windows.Forms.TableLayoutPanel tlpDetailsFields;
        private System.Windows.Forms.TableLayoutPanel tlpDetails;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblGeneric;
        private System.Windows.Forms.TextBox txtGeneric;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblUnitType;
        private System.Windows.Forms.ComboBox cboUnitType;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.ComboBox cboManufacturer;
        private System.Windows.Forms.Label lblReorder;
        private System.Windows.Forms.TextBox txtReorder;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.TextBox txtStorage;
        private System.Windows.Forms.TableLayoutPanel tlpMedButtons;
        private System.Windows.Forms.Button btnMedSave;
        private System.Windows.Forms.Button btnMedNew;
        private System.Windows.Forms.Button btnMedDelete;
        private System.Windows.Forms.Panel pnlCatalogCard;
        private System.Windows.Forms.DataGridView dgvMedicines;
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
