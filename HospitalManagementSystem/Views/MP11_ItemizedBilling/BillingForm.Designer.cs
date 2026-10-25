namespace HospitalManagementSystem.Views.MP11_Billing
{
    partial class BillingForm
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

        // Redesign: page title + date line, Builder / All Bills toggle with the status on the right; (builder)
        // bill and add-a-charge cards on the left, items table card and summary card (inputs, totals, actions)
        // on the right; (all bills) one table card with filters in the header and actions in the footer.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblDate = new Label();
            lblTitle = new Label();
            tlpMode = new TableLayoutPanel();
            btnModeBuilder = new Button();
            btnModeList = new Button();
            lblStatus = new Label();
            pnlBody = new Panel();
            pnlList = new Panel();
            pnlListCard = new Panel();
            dgvBills = new DataGridView();
            pnlListFooter = new Panel();
            lblListCount = new Label();
            flowListActions = new FlowLayoutPanel();
            btnDeleteDraft = new Button();
            btnOpenBill = new Button();
            pnlListHeader = new Panel();
            lblListTitle = new Label();
            flowListFilter = new FlowLayoutPanel();
            txtListSearch = new TextBox();
            cboListStatus = new ComboBox();
            btnListSearch = new Button();
            btnListAll = new Button();
            pnlBuilder = new Panel();
            tlpBuilder = new TableLayoutPanel();
            tlpLeft = new TableLayoutPanel();
            pnlBillCard = new Panel();
            tlpBillCard = new TableLayoutPanel();
            lblBillTitle = new Label();
            tlpBill = new TableLayoutPanel();
            lblPatientSearch = new Label();
            tlpFind = new TableLayoutPanel();
            txtPatientSearch = new TextBox();
            btnPatientFind = new Button();
            lblPatient = new Label();
            cboPatient = new ComboBox();
            lblBillCode = new Label();
            txtBillCode = new TextBox();
            lblBillState = new Label();
            pnlAddCard = new Panel();
            tlpAddCard = new TableLayoutPanel();
            lblAddTitle = new Label();
            tlpAdd = new TableLayoutPanel();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            lblItem = new Label();
            cboItem = new ComboBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            lblUnitPrice = new Label();
            txtUnitPrice = new TextBox();
            tlpLine = new TableLayoutPanel();
            lblLineCaption = new Label();
            lblLineAmount = new Label();
            tlpAddButtons = new TableLayoutPanel();
            btnAddItem = new Button();
            btnRemoveItem = new Button();
            btnClearItem = new Button();
            tlpRight = new TableLayoutPanel();
            pnlItemsCard = new Panel();
            dgvItems = new DataGridView();
            pnlItemsFooter = new Panel();
            lblItemsInfo = new Label();
            flowItemsFooter = new FlowLayoutPanel();
            pnlItemsHeader = new Panel();
            lblItemsTitle = new Label();
            pnlSummaryCard = new Panel();
            tlpSummary = new TableLayoutPanel();
            lblSummaryTitle = new Label();
            tlpSummaryInputs = new TableLayoutPanel();
            lblTaxRateCap = new Label();
            txtTaxRate = new TextBox();
            lblDiscountTypeCap = new Label();
            cboDiscountType = new ComboBox();
            lblDiscountCap = new Label();
            txtDiscount = new TextBox();
            tlpTotals = new TableLayoutPanel();
            lblSubtotalCap = new Label();
            lblSubtotal = new Label();
            lblDiscountAmountCap = new Label();
            lblDiscountAmount = new Label();
            lblTaxableCap = new Label();
            lblTaxable = new Label();
            lblTaxCap = new Label();
            lblTaxAmount = new Label();
            lblGrandCap = new Label();
            lblGrandTotal = new Label();
            lblSummaryNote = new Label();
            tlpAuth = new TableLayoutPanel();
            lblAuthMessage = new Label();
            lblAuthorizedBy = new Label();
            txtAuthorizedBy = new TextBox();
            tlpSummaryButtons = new TableLayoutPanel();
            btnSaveDraft = new Button();
            btnFinalize = new Button();
            btnNewBill = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            tlpMode.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlList.SuspendLayout();
            pnlListCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBills).BeginInit();
            pnlListFooter.SuspendLayout();
            flowListActions.SuspendLayout();
            pnlListHeader.SuspendLayout();
            flowListFilter.SuspendLayout();
            pnlBuilder.SuspendLayout();
            tlpBuilder.SuspendLayout();
            tlpLeft.SuspendLayout();
            pnlBillCard.SuspendLayout();
            tlpBillCard.SuspendLayout();
            tlpBill.SuspendLayout();
            tlpFind.SuspendLayout();
            pnlAddCard.SuspendLayout();
            tlpAddCard.SuspendLayout();
            tlpAdd.SuspendLayout();
            tlpLine.SuspendLayout();
            tlpAddButtons.SuspendLayout();
            tlpRight.SuspendLayout();
            pnlItemsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            pnlItemsFooter.SuspendLayout();
            flowItemsFooter.SuspendLayout();
            pnlItemsHeader.SuspendLayout();
            pnlSummaryCard.SuspendLayout();
            tlpSummary.SuspendLayout();
            tlpSummaryInputs.SuspendLayout();
            tlpTotals.SuspendLayout();
            tlpAuth.SuspendLayout();
            tlpSummaryButtons.SuspendLayout();
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
            lblDate.Text = "Build a patient's bill item by item, then save it as a draft or finalize it.";
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
            lblTitle.Text = "Itemized Billing";
            lblTitle.UseMnemonic = false;
            // 
            // tlpMode
            // 
            tlpMode.BackColor = Color.Transparent;
            tlpMode.ColumnCount = 3;
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMode.Controls.Add(btnModeBuilder, 0, 0);
            tlpMode.Controls.Add(btnModeList, 1, 0);
            tlpMode.Controls.Add(lblStatus, 2, 0);
            tlpMode.Dock = DockStyle.Fill;
            tlpMode.Margin = new Padding(0, 0, 0, 18);
            tlpMode.Name = "tlpMode";
            tlpMode.RowCount = 1;
            tlpMode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMode.TabIndex = 1;
            // 
            // btnModeBuilder
            // 
            btnModeBuilder.BackColor = Color.FromArgb(18, 122, 140);
            btnModeBuilder.FlatStyle = FlatStyle.Flat;
            btnModeBuilder.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeBuilder.ForeColor = Color.White;
            btnModeBuilder.Margin = new Padding(0, 0, 8, 0);
            btnModeBuilder.Name = "btnModeBuilder";
            btnModeBuilder.Size = new Size(150, 38);
            btnModeBuilder.TabIndex = 0;
            btnModeBuilder.Text = "Bill Builder";
            btnModeBuilder.UseMnemonic = false;
            btnModeBuilder.UseVisualStyleBackColor = false;
            // 
            // btnModeList
            // 
            btnModeList.BackColor = Color.White;
            btnModeList.FlatStyle = FlatStyle.Flat;
            btnModeList.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeList.ForeColor = Color.FromArgb(20, 34, 51);
            btnModeList.Margin = new Padding(0);
            btnModeList.Name = "btnModeList";
            btnModeList.Size = new Size(150, 38);
            btnModeList.TabIndex = 1;
            btnModeList.Text = "All Bills";
            btnModeList.UseMnemonic = false;
            btnModeList.UseVisualStyleBackColor = false;
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
            pnlBody.Controls.Add(pnlList);
            pnlBody.Controls.Add(pnlBuilder);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Margin = new Padding(0);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(0);
            pnlBody.TabIndex = 2;
            // 
            // pnlList
            // 
            pnlList.BackColor = Color.Transparent;
            pnlList.Controls.Add(pnlListCard);
            pnlList.Dock = DockStyle.Fill;
            pnlList.Margin = new Padding(0);
            pnlList.Name = "pnlList";
            pnlList.Padding = new Padding(0);
            pnlList.TabIndex = 0;
            pnlList.Visible = false;
            // 
            // pnlListCard
            // 
            pnlListCard.BackColor = Color.White;
            pnlListCard.Controls.Add(dgvBills);
            pnlListCard.Controls.Add(pnlListFooter);
            pnlListCard.Controls.Add(pnlListHeader);
            pnlListCard.Dock = DockStyle.Fill;
            pnlListCard.Margin = new Padding(0);
            pnlListCard.Name = "pnlListCard";
            pnlListCard.Padding = new Padding(1, 6, 1, 1);
            pnlListCard.TabIndex = 0;
            // 
            // dgvBills
            // 
            dgvBills.AllowUserToAddRows = false;
            dgvBills.AllowUserToDeleteRows = false;
            dgvBills.AllowUserToResizeRows = false;
            dgvBills.BackgroundColor = Color.White;
            dgvBills.BorderStyle = BorderStyle.None;
            dgvBills.Dock = DockStyle.Fill;
            dgvBills.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvBills.Margin = new Padding(0);
            dgvBills.MultiSelect = false;
            dgvBills.Name = "dgvBills";
            dgvBills.ReadOnly = true;
            dgvBills.RowHeadersVisible = false;
            dgvBills.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBills.TabIndex = 0;
            // 
            // pnlListFooter
            // 
            pnlListFooter.BackColor = Color.White;
            pnlListFooter.Controls.Add(lblListCount);
            pnlListFooter.Controls.Add(flowListActions);
            pnlListFooter.Dock = DockStyle.Bottom;
            pnlListFooter.Margin = new Padding(0);
            pnlListFooter.Name = "pnlListFooter";
            pnlListFooter.Padding = new Padding(0);
            pnlListFooter.Size = new Size(600, 56);
            pnlListFooter.TabIndex = 1;
            // 
            // lblListCount
            // 
            lblListCount.AutoEllipsis = true;
            lblListCount.BackColor = Color.Transparent;
            lblListCount.Dock = DockStyle.Fill;
            lblListCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblListCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblListCount.Name = "lblListCount";
            lblListCount.Padding = new Padding(18, 0, 8, 0);
            lblListCount.TabIndex = 0;
            lblListCount.TextAlign = ContentAlignment.MiddleLeft;
            lblListCount.UseMnemonic = false;
            // 
            // flowListActions
            // 
            flowListActions.AutoSize = true;
            flowListActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowListActions.BackColor = Color.Transparent;
            flowListActions.Controls.Add(btnDeleteDraft);
            flowListActions.Controls.Add(btnOpenBill);
            flowListActions.Dock = DockStyle.Right;
            flowListActions.Margin = new Padding(0);
            flowListActions.Name = "flowListActions";
            flowListActions.Padding = new Padding(0, 9, 18, 0);
            flowListActions.TabIndex = 1;
            flowListActions.WrapContents = false;
            // 
            // btnDeleteDraft
            // 
            btnDeleteDraft.BackColor = Color.FromArgb(180, 35, 24);
            btnDeleteDraft.Enabled = false;
            btnDeleteDraft.FlatStyle = FlatStyle.Flat;
            btnDeleteDraft.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDeleteDraft.ForeColor = Color.White;
            btnDeleteDraft.Margin = new Padding(10, 0, 0, 0);
            btnDeleteDraft.Name = "btnDeleteDraft";
            btnDeleteDraft.Size = new Size(130, 38);
            btnDeleteDraft.TabIndex = 0;
            btnDeleteDraft.Text = "Delete Draft";
            btnDeleteDraft.UseMnemonic = false;
            btnDeleteDraft.UseVisualStyleBackColor = false;
            // 
            // btnOpenBill
            // 
            btnOpenBill.BackColor = Color.FromArgb(18, 122, 140);
            btnOpenBill.Enabled = false;
            btnOpenBill.FlatStyle = FlatStyle.Flat;
            btnOpenBill.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnOpenBill.ForeColor = Color.White;
            btnOpenBill.Margin = new Padding(10, 0, 0, 0);
            btnOpenBill.Name = "btnOpenBill";
            btnOpenBill.Size = new Size(120, 38);
            btnOpenBill.TabIndex = 1;
            btnOpenBill.Text = "Open Bill";
            btnOpenBill.UseMnemonic = false;
            btnOpenBill.UseVisualStyleBackColor = false;
            // 
            // pnlListHeader
            // 
            pnlListHeader.BackColor = Color.White;
            pnlListHeader.Controls.Add(lblListTitle);
            pnlListHeader.Controls.Add(flowListFilter);
            pnlListHeader.Dock = DockStyle.Top;
            pnlListHeader.Margin = new Padding(0);
            pnlListHeader.Name = "pnlListHeader";
            pnlListHeader.Padding = new Padding(0);
            pnlListHeader.Size = new Size(600, 46);
            pnlListHeader.TabIndex = 2;
            // 
            // lblListTitle
            // 
            lblListTitle.AutoEllipsis = true;
            lblListTitle.BackColor = Color.Transparent;
            lblListTitle.Dock = DockStyle.Fill;
            lblListTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblListTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblListTitle.Margin = new Padding(0);
            lblListTitle.Name = "lblListTitle";
            lblListTitle.Padding = new Padding(17, 0, 0, 0);
            lblListTitle.TabIndex = 0;
            lblListTitle.Text = "All bills";
            lblListTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblListTitle.UseMnemonic = false;
            // 
            // flowListFilter
            // 
            flowListFilter.AutoSize = true;
            flowListFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowListFilter.BackColor = Color.White;
            flowListFilter.Controls.Add(txtListSearch);
            flowListFilter.Controls.Add(cboListStatus);
            flowListFilter.Controls.Add(btnListSearch);
            flowListFilter.Controls.Add(btnListAll);
            flowListFilter.Dock = DockStyle.Right;
            flowListFilter.Margin = new Padding(0);
            flowListFilter.Name = "flowListFilter";
            flowListFilter.Padding = new Padding(0, 5, 18, 0);
            flowListFilter.TabIndex = 1;
            flowListFilter.WrapContents = false;
            // 
            // txtListSearch
            // 
            txtListSearch.AutoSize = false;
            txtListSearch.BackColor = Color.White;
            txtListSearch.BorderStyle = BorderStyle.FixedSingle;
            txtListSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtListSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtListSearch.Margin = new Padding(0);
            txtListSearch.MaxLength = 60;
            txtListSearch.Name = "txtListSearch";
            txtListSearch.PlaceholderText = "Bill code, patient name or PAT- code";
            txtListSearch.Size = new Size(240, 36);
            txtListSearch.TabIndex = 0;
            // 
            // cboListStatus
            // 
            cboListStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboListStatus.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboListStatus.FormattingEnabled = true;
            cboListStatus.Margin = new Padding(10, 0, 0, 0);
            cboListStatus.Name = "cboListStatus";
            cboListStatus.Size = new Size(150, 32);
            cboListStatus.TabIndex = 1;
            // 
            // btnListSearch
            // 
            btnListSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnListSearch.FlatStyle = FlatStyle.Flat;
            btnListSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnListSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnListSearch.Margin = new Padding(10, 0, 0, 0);
            btnListSearch.Name = "btnListSearch";
            btnListSearch.Size = new Size(86, 36);
            btnListSearch.TabIndex = 2;
            btnListSearch.Text = "Search";
            btnListSearch.UseMnemonic = false;
            btnListSearch.UseVisualStyleBackColor = false;
            // 
            // btnListAll
            // 
            btnListAll.BackColor = Color.White;
            btnListAll.FlatStyle = FlatStyle.Flat;
            btnListAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnListAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnListAll.Margin = new Padding(10, 0, 0, 0);
            btnListAll.Name = "btnListAll";
            btnListAll.Size = new Size(96, 36);
            btnListAll.TabIndex = 3;
            btnListAll.Text = "Show All";
            btnListAll.UseMnemonic = false;
            btnListAll.UseVisualStyleBackColor = false;
            // 
            // pnlBuilder
            // 
            pnlBuilder.BackColor = Color.Transparent;
            pnlBuilder.Controls.Add(tlpBuilder);
            pnlBuilder.Dock = DockStyle.Fill;
            pnlBuilder.Margin = new Padding(0);
            pnlBuilder.Name = "pnlBuilder";
            pnlBuilder.Padding = new Padding(0);
            pnlBuilder.TabIndex = 1;
            // 
            // tlpBuilder
            // 
            tlpBuilder.BackColor = Color.Transparent;
            tlpBuilder.ColumnCount = 2;
            tlpBuilder.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 460F));
            tlpBuilder.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBuilder.Controls.Add(tlpLeft, 0, 0);
            tlpBuilder.Controls.Add(tlpRight, 1, 0);
            tlpBuilder.Dock = DockStyle.Fill;
            tlpBuilder.Margin = new Padding(0);
            tlpBuilder.Name = "tlpBuilder";
            tlpBuilder.RowCount = 1;
            tlpBuilder.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBuilder.TabIndex = 0;
            // 
            // tlpLeft
            // 
            tlpLeft.BackColor = Color.Transparent;
            tlpLeft.ColumnCount = 1;
            tlpLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLeft.Controls.Add(pnlBillCard, 0, 0);
            tlpLeft.Controls.Add(pnlAddCard, 0, 1);
            tlpLeft.Dock = DockStyle.Fill;
            tlpLeft.Margin = new Padding(0, 0, 20, 0);
            tlpLeft.Name = "tlpLeft";
            tlpLeft.RowCount = 2;
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpLeft.TabIndex = 0;
            // 
            // pnlBillCard
            // 
            pnlBillCard.AutoSize = true;
            pnlBillCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlBillCard.BackColor = Color.White;
            pnlBillCard.Controls.Add(tlpBillCard);
            pnlBillCard.Dock = DockStyle.Fill;
            pnlBillCard.Margin = new Padding(0, 0, 0, 20);
            pnlBillCard.Name = "pnlBillCard";
            pnlBillCard.Padding = new Padding(20, 18, 20, 18);
            pnlBillCard.TabIndex = 0;
            // 
            // tlpBillCard
            // 
            tlpBillCard.AutoSize = true;
            tlpBillCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpBillCard.BackColor = Color.Transparent;
            tlpBillCard.ColumnCount = 1;
            tlpBillCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBillCard.Controls.Add(lblBillTitle, 0, 0);
            tlpBillCard.Controls.Add(tlpBill, 0, 1);
            tlpBillCard.Controls.Add(lblBillState, 0, 2);
            tlpBillCard.Dock = DockStyle.Top;
            tlpBillCard.Margin = new Padding(0);
            tlpBillCard.Name = "tlpBillCard";
            tlpBillCard.RowCount = 3;
            tlpBillCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBillCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBillCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBillCard.TabIndex = 0;
            // 
            // lblBillTitle
            // 
            lblBillTitle.AutoSize = true;
            lblBillTitle.BackColor = Color.Transparent;
            lblBillTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBillTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblBillTitle.Margin = new Padding(0, 0, 0, 14);
            lblBillTitle.Name = "lblBillTitle";
            lblBillTitle.TabIndex = 0;
            lblBillTitle.Text = "Bill";
            // 
            // tlpBill
            // 
            tlpBill.AutoSize = true;
            tlpBill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpBill.BackColor = Color.Transparent;
            tlpBill.ColumnCount = 2;
            tlpBill.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            tlpBill.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            tlpBill.Controls.Add(lblPatientSearch, 0, 0);
            tlpBill.SetColumnSpan(lblPatientSearch, 2);
            tlpBill.Controls.Add(tlpFind, 0, 1);
            tlpBill.SetColumnSpan(tlpFind, 2);
            tlpBill.Controls.Add(lblPatient, 0, 2);
            tlpBill.Controls.Add(cboPatient, 0, 3);
            tlpBill.Controls.Add(lblBillCode, 1, 2);
            tlpBill.Controls.Add(txtBillCode, 1, 3);
            tlpBill.Dock = DockStyle.Top;
            tlpBill.Margin = new Padding(0);
            tlpBill.Name = "tlpBill";
            tlpBill.RowCount = 4;
            tlpBill.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBill.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpBill.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBill.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpBill.TabIndex = 1;
            // 
            // lblPatientSearch
            // 
            lblPatientSearch.AutoSize = true;
            lblPatientSearch.BackColor = Color.Transparent;
            lblPatientSearch.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPatientSearch.ForeColor = Color.FromArgb(20, 34, 51);
            lblPatientSearch.Margin = new Padding(0, 0, 0, 6);
            lblPatientSearch.Name = "lblPatientSearch";
            lblPatientSearch.TabIndex = 0;
            lblPatientSearch.Text = "Find patient";
            lblPatientSearch.UseMnemonic = false;
            // 
            // tlpFind
            // 
            tlpFind.BackColor = Color.Transparent;
            tlpFind.ColumnCount = 2;
            tlpFind.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFind.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86F));
            tlpFind.Controls.Add(txtPatientSearch, 0, 0);
            tlpFind.Controls.Add(btnPatientFind, 1, 0);
            tlpFind.Dock = DockStyle.Fill;
            tlpFind.Margin = new Padding(0, 0, 0, 14);
            tlpFind.Name = "tlpFind";
            tlpFind.RowCount = 1;
            tlpFind.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFind.TabIndex = 1;
            // 
            // txtPatientSearch
            // 
            txtPatientSearch.AutoSize = false;
            txtPatientSearch.BackColor = Color.White;
            txtPatientSearch.BorderStyle = BorderStyle.FixedSingle;
            txtPatientSearch.Dock = DockStyle.Fill;
            txtPatientSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPatientSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtPatientSearch.Margin = new Padding(0);
            txtPatientSearch.MaxLength = 60;
            txtPatientSearch.Name = "txtPatientSearch";
            txtPatientSearch.PlaceholderText = "Name or PAT- code";
            txtPatientSearch.Size = new Size(200, 40);
            txtPatientSearch.TabIndex = 0;
            // 
            // btnPatientFind
            // 
            btnPatientFind.BackColor = Color.FromArgb(226, 242, 246);
            btnPatientFind.Dock = DockStyle.Fill;
            btnPatientFind.FlatStyle = FlatStyle.Flat;
            btnPatientFind.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPatientFind.ForeColor = Color.FromArgb(20, 34, 51);
            btnPatientFind.Margin = new Padding(8, 0, 0, 0);
            btnPatientFind.Name = "btnPatientFind";
            btnPatientFind.Size = new Size(120, 40);
            btnPatientFind.TabIndex = 1;
            btnPatientFind.Text = "Find";
            btnPatientFind.UseMnemonic = false;
            btnPatientFind.UseVisualStyleBackColor = false;
            // 
            // lblPatient
            // 
            lblPatient.AutoSize = true;
            lblPatient.BackColor = Color.Transparent;
            lblPatient.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPatient.ForeColor = Color.FromArgb(20, 34, 51);
            lblPatient.Margin = new Padding(0, 0, 6, 6);
            lblPatient.Name = "lblPatient";
            lblPatient.TabIndex = 2;
            lblPatient.Text = "Patient *";
            lblPatient.UseMnemonic = false;
            // 
            // cboPatient
            // 
            cboPatient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPatient.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboPatient.FormattingEnabled = true;
            cboPatient.Margin = new Padding(0, 0, 6, 14);
            cboPatient.Name = "cboPatient";
            cboPatient.Size = new Size(200, 32);
            cboPatient.TabIndex = 3;
            // 
            // lblBillCode
            // 
            lblBillCode.AutoSize = true;
            lblBillCode.BackColor = Color.Transparent;
            lblBillCode.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBillCode.ForeColor = Color.FromArgb(20, 34, 51);
            lblBillCode.Margin = new Padding(6, 0, 0, 6);
            lblBillCode.Name = "lblBillCode";
            lblBillCode.TabIndex = 4;
            lblBillCode.Text = "Bill code";
            lblBillCode.UseMnemonic = false;
            // 
            // txtBillCode
            // 
            txtBillCode.AutoSize = false;
            txtBillCode.BackColor = Color.White;
            txtBillCode.BorderStyle = BorderStyle.FixedSingle;
            txtBillCode.Dock = DockStyle.Fill;
            txtBillCode.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBillCode.ForeColor = Color.FromArgb(20, 34, 51);
            txtBillCode.Margin = new Padding(6, 0, 0, 14);
            txtBillCode.MaxLength = 20;
            txtBillCode.Name = "txtBillCode";
            txtBillCode.PlaceholderText = "Assigned when finalized";
            txtBillCode.ReadOnly = true;
            txtBillCode.Size = new Size(200, 40);
            txtBillCode.TabIndex = 5;
            // 
            // lblBillState
            // 
            lblBillState.BackColor = Color.FromArgb(255, 244, 224);
            lblBillState.Dock = DockStyle.Top;
            lblBillState.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBillState.ForeColor = Color.FromArgb(91, 107, 124);
            lblBillState.Margin = new Padding(0);
            lblBillState.Name = "lblBillState";
            lblBillState.Padding = new Padding(12, 0, 12, 0);
            lblBillState.Size = new Size(400, 36);
            lblBillState.TabIndex = 2;
            lblBillState.Text = "NEW BILL  -  not saved yet";
            lblBillState.TextAlign = ContentAlignment.MiddleLeft;
            lblBillState.UseMnemonic = false;
            // 
            // pnlAddCard
            // 
            pnlAddCard.BackColor = Color.White;
            pnlAddCard.Controls.Add(tlpAddCard);
            pnlAddCard.Dock = DockStyle.Fill;
            pnlAddCard.Margin = new Padding(0);
            pnlAddCard.Name = "pnlAddCard";
            pnlAddCard.Padding = new Padding(20, 18, 20, 18);
            pnlAddCard.TabIndex = 1;
            // 
            // tlpAddCard
            // 
            tlpAddCard.BackColor = Color.Transparent;
            tlpAddCard.ColumnCount = 1;
            tlpAddCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpAddCard.Controls.Add(lblAddTitle, 0, 0);
            tlpAddCard.Controls.Add(tlpAdd, 0, 1);
            tlpAddCard.Controls.Add(tlpLine, 0, 2);
            tlpAddCard.Controls.Add(tlpAddButtons, 0, 3);
            tlpAddCard.Dock = DockStyle.Fill;
            tlpAddCard.Margin = new Padding(0);
            tlpAddCard.Name = "tlpAddCard";
            tlpAddCard.RowCount = 4;
            tlpAddCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpAddCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpAddCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpAddCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpAddCard.TabIndex = 0;
            // 
            // lblAddTitle
            // 
            lblAddTitle.AutoSize = true;
            lblAddTitle.BackColor = Color.Transparent;
            lblAddTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAddTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblAddTitle.Margin = new Padding(0, 0, 0, 14);
            lblAddTitle.Name = "lblAddTitle";
            lblAddTitle.TabIndex = 0;
            lblAddTitle.Text = "Add a charge";
            // 
            // tlpAdd
            // 
            tlpAdd.AutoSize = true;
            tlpAdd.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpAdd.BackColor = Color.Transparent;
            tlpAdd.ColumnCount = 2;
            tlpAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpAdd.Controls.Add(lblCategory, 0, 0);
            tlpAdd.Controls.Add(cboCategory, 0, 1);
            tlpAdd.Controls.Add(lblItem, 1, 0);
            tlpAdd.Controls.Add(cboItem, 1, 1);
            tlpAdd.Controls.Add(lblDescription, 0, 2);
            tlpAdd.SetColumnSpan(lblDescription, 2);
            tlpAdd.Controls.Add(txtDescription, 0, 3);
            tlpAdd.SetColumnSpan(txtDescription, 2);
            tlpAdd.Controls.Add(lblQuantity, 0, 4);
            tlpAdd.Controls.Add(txtQuantity, 0, 5);
            tlpAdd.Controls.Add(lblUnitPrice, 1, 4);
            tlpAdd.Controls.Add(txtUnitPrice, 1, 5);
            tlpAdd.Dock = DockStyle.Top;
            tlpAdd.Margin = new Padding(0);
            tlpAdd.Name = "tlpAdd";
            tlpAdd.RowCount = 6;
            tlpAdd.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpAdd.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpAdd.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpAdd.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpAdd.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpAdd.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpAdd.TabIndex = 1;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.BackColor = Color.Transparent;
            lblCategory.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCategory.ForeColor = Color.FromArgb(20, 34, 51);
            lblCategory.Margin = new Padding(0, 0, 6, 6);
            lblCategory.Name = "lblCategory";
            lblCategory.TabIndex = 0;
            lblCategory.Text = "Category *";
            lblCategory.UseMnemonic = false;
            // 
            // cboCategory
            // 
            cboCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboCategory.FormattingEnabled = true;
            cboCategory.Margin = new Padding(0, 0, 6, 14);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(200, 32);
            cboCategory.TabIndex = 1;
            // 
            // lblItem
            // 
            lblItem.AutoSize = true;
            lblItem.BackColor = Color.Transparent;
            lblItem.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblItem.ForeColor = Color.FromArgb(20, 34, 51);
            lblItem.Margin = new Padding(6, 0, 0, 6);
            lblItem.Name = "lblItem";
            lblItem.TabIndex = 2;
            lblItem.Text = "Item *";
            lblItem.UseMnemonic = false;
            // 
            // cboItem
            // 
            cboItem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboItem.DropDownStyle = ComboBoxStyle.DropDownList;
            cboItem.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboItem.FormattingEnabled = true;
            cboItem.Margin = new Padding(6, 0, 0, 14);
            cboItem.Name = "cboItem";
            cboItem.Size = new Size(200, 32);
            cboItem.TabIndex = 3;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.BackColor = Color.Transparent;
            lblDescription.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDescription.ForeColor = Color.FromArgb(20, 34, 51);
            lblDescription.Margin = new Padding(0, 0, 0, 6);
            lblDescription.Name = "lblDescription";
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Description *";
            lblDescription.UseMnemonic = false;
            // 
            // txtDescription
            // 
            txtDescription.AutoSize = false;
            txtDescription.BackColor = Color.White;
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescription.ForeColor = Color.FromArgb(20, 34, 51);
            txtDescription.Margin = new Padding(0, 0, 0, 14);
            txtDescription.MaxLength = 200;
            txtDescription.Name = "txtDescription";
            txtDescription.PlaceholderText = "Pick an item above, or choose Other";
            txtDescription.Size = new Size(200, 40);
            txtDescription.TabIndex = 5;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.BackColor = Color.Transparent;
            lblQuantity.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblQuantity.ForeColor = Color.FromArgb(20, 34, 51);
            lblQuantity.Margin = new Padding(0, 0, 6, 6);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.TabIndex = 6;
            lblQuantity.Text = "Quantity *";
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
            txtQuantity.Margin = new Padding(0, 0, 6, 14);
            txtQuantity.MaxLength = 3;
            txtQuantity.Name = "txtQuantity";
            txtQuantity.PlaceholderText = "1";
            txtQuantity.Size = new Size(200, 40);
            txtQuantity.TabIndex = 7;
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.AutoSize = true;
            lblUnitPrice.BackColor = Color.Transparent;
            lblUnitPrice.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUnitPrice.ForeColor = Color.FromArgb(20, 34, 51);
            lblUnitPrice.Margin = new Padding(6, 0, 0, 6);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.TabIndex = 8;
            lblUnitPrice.Text = "Unit price *";
            lblUnitPrice.UseMnemonic = false;
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.AutoSize = false;
            txtUnitPrice.BackColor = Color.White;
            txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
            txtUnitPrice.Dock = DockStyle.Fill;
            txtUnitPrice.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUnitPrice.ForeColor = Color.FromArgb(20, 34, 51);
            txtUnitPrice.Margin = new Padding(6, 0, 0, 14);
            txtUnitPrice.MaxLength = 9;
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.PlaceholderText = "0.00";
            txtUnitPrice.Size = new Size(200, 40);
            txtUnitPrice.TabIndex = 9;
            // 
            // tlpLine
            // 
            tlpLine.AutoSize = true;
            tlpLine.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpLine.BackColor = Color.Transparent;
            tlpLine.ColumnCount = 2;
            tlpLine.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpLine.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLine.Controls.Add(lblLineCaption, 0, 0);
            tlpLine.Controls.Add(lblLineAmount, 1, 0);
            tlpLine.Dock = DockStyle.Top;
            tlpLine.Margin = new Padding(0);
            tlpLine.Name = "tlpLine";
            tlpLine.RowCount = 1;
            tlpLine.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLine.TabIndex = 2;
            // 
            // lblLineCaption
            // 
            lblLineCaption.AutoSize = true;
            lblLineCaption.BackColor = Color.Transparent;
            lblLineCaption.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLineCaption.ForeColor = Color.FromArgb(91, 107, 124);
            lblLineCaption.Margin = new Padding(0, 4, 10, 0);
            lblLineCaption.Name = "lblLineCaption";
            lblLineCaption.TabIndex = 0;
            lblLineCaption.Text = "Line amount";
            lblLineCaption.UseMnemonic = false;
            // 
            // lblLineAmount
            // 
            lblLineAmount.AutoSize = true;
            lblLineAmount.BackColor = Color.Transparent;
            lblLineAmount.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLineAmount.ForeColor = Color.FromArgb(20, 34, 51);
            lblLineAmount.Margin = new Padding(0);
            lblLineAmount.Name = "lblLineAmount";
            lblLineAmount.TabIndex = 1;
            lblLineAmount.Text = "0.00";
            lblLineAmount.UseMnemonic = false;
            // 
            // tlpAddButtons
            // 
            tlpAddButtons.BackColor = Color.Transparent;
            tlpAddButtons.ColumnCount = 3;
            tlpAddButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpAddButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlpAddButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpAddButtons.Controls.Add(btnAddItem, 0, 0);
            tlpAddButtons.Controls.Add(btnRemoveItem, 1, 0);
            tlpAddButtons.Controls.Add(btnClearItem, 2, 0);
            tlpAddButtons.Dock = DockStyle.Fill;
            tlpAddButtons.Margin = new Padding(0);
            tlpAddButtons.Name = "tlpAddButtons";
            tlpAddButtons.RowCount = 1;
            tlpAddButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpAddButtons.TabIndex = 3;
            // 
            // btnAddItem
            // 
            btnAddItem.BackColor = Color.FromArgb(18, 122, 140);
            btnAddItem.Dock = DockStyle.Fill;
            btnAddItem.FlatStyle = FlatStyle.Flat;
            btnAddItem.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAddItem.ForeColor = Color.White;
            btnAddItem.Margin = new Padding(0);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(120, 40);
            btnAddItem.TabIndex = 0;
            btnAddItem.Text = "Add Item";
            btnAddItem.UseMnemonic = false;
            btnAddItem.UseVisualStyleBackColor = false;
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.BackColor = Color.White;
            btnRemoveItem.Dock = DockStyle.Fill;
            btnRemoveItem.Enabled = false;
            btnRemoveItem.FlatStyle = FlatStyle.Flat;
            btnRemoveItem.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRemoveItem.ForeColor = Color.FromArgb(20, 34, 51);
            btnRemoveItem.Margin = new Padding(10, 0, 0, 0);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(120, 40);
            btnRemoveItem.TabIndex = 1;
            btnRemoveItem.Text = "Remove Item";
            btnRemoveItem.UseMnemonic = false;
            btnRemoveItem.UseVisualStyleBackColor = false;
            // 
            // btnClearItem
            // 
            btnClearItem.BackColor = Color.White;
            btnClearItem.Dock = DockStyle.Fill;
            btnClearItem.FlatStyle = FlatStyle.Flat;
            btnClearItem.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearItem.ForeColor = Color.FromArgb(20, 34, 51);
            btnClearItem.Margin = new Padding(10, 0, 0, 0);
            btnClearItem.Name = "btnClearItem";
            btnClearItem.Size = new Size(120, 40);
            btnClearItem.TabIndex = 2;
            btnClearItem.Text = "Clear";
            btnClearItem.UseMnemonic = false;
            btnClearItem.UseVisualStyleBackColor = false;
            // 
            // tlpRight
            // 
            tlpRight.BackColor = Color.Transparent;
            tlpRight.ColumnCount = 1;
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRight.Controls.Add(pnlItemsCard, 0, 0);
            tlpRight.Controls.Add(pnlSummaryCard, 0, 1);
            tlpRight.Dock = DockStyle.Fill;
            tlpRight.Margin = new Padding(0);
            tlpRight.Name = "tlpRight";
            tlpRight.RowCount = 2;
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRight.TabIndex = 1;
            // 
            // pnlItemsCard
            // 
            pnlItemsCard.BackColor = Color.White;
            pnlItemsCard.Controls.Add(dgvItems);
            pnlItemsCard.Controls.Add(pnlItemsFooter);
            pnlItemsCard.Controls.Add(pnlItemsHeader);
            pnlItemsCard.Dock = DockStyle.Fill;
            pnlItemsCard.Margin = new Padding(0, 0, 0, 20);
            pnlItemsCard.Name = "pnlItemsCard";
            pnlItemsCard.Padding = new Padding(1, 6, 1, 1);
            pnlItemsCard.TabIndex = 0;
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
            pnlItemsFooter.Controls.Add(lblItemsInfo);
            pnlItemsFooter.Controls.Add(flowItemsFooter);
            pnlItemsFooter.Dock = DockStyle.Bottom;
            pnlItemsFooter.Margin = new Padding(0);
            pnlItemsFooter.Name = "pnlItemsFooter";
            pnlItemsFooter.Padding = new Padding(0);
            pnlItemsFooter.Size = new Size(600, 44);
            pnlItemsFooter.TabIndex = 1;
            // 
            // lblItemsInfo
            // 
            lblItemsInfo.AutoEllipsis = true;
            lblItemsInfo.BackColor = Color.Transparent;
            lblItemsInfo.Dock = DockStyle.Fill;
            lblItemsInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblItemsInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblItemsInfo.Name = "lblItemsInfo";
            lblItemsInfo.Padding = new Padding(18, 0, 8, 0);
            lblItemsInfo.TabIndex = 0;
            lblItemsInfo.TextAlign = ContentAlignment.MiddleLeft;
            lblItemsInfo.UseMnemonic = false;
            // 
            // flowItemsFooter
            // 
            flowItemsFooter.AutoSize = true;
            flowItemsFooter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowItemsFooter.BackColor = Color.Transparent;
            flowItemsFooter.Dock = DockStyle.Right;
            flowItemsFooter.Margin = new Padding(0);
            flowItemsFooter.Name = "flowItemsFooter";
            flowItemsFooter.Padding = new Padding(0);
            flowItemsFooter.TabIndex = 1;
            flowItemsFooter.WrapContents = false;
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
            lblItemsTitle.Text = "Items on this bill";
            lblItemsTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblItemsTitle.UseMnemonic = false;
            // 
            // pnlSummaryCard
            // 
            pnlSummaryCard.AutoSize = true;
            pnlSummaryCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlSummaryCard.BackColor = Color.White;
            pnlSummaryCard.Controls.Add(tlpSummary);
            pnlSummaryCard.Dock = DockStyle.Fill;
            pnlSummaryCard.Margin = new Padding(0);
            pnlSummaryCard.Name = "pnlSummaryCard";
            pnlSummaryCard.Padding = new Padding(20, 18, 20, 18);
            pnlSummaryCard.TabIndex = 1;
            // 
            // tlpSummary
            // 
            tlpSummary.AutoSize = true;
            tlpSummary.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpSummary.BackColor = Color.Transparent;
            tlpSummary.ColumnCount = 1;
            tlpSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSummary.Controls.Add(lblSummaryTitle, 0, 0);
            tlpSummary.Controls.Add(tlpSummaryInputs, 0, 1);
            tlpSummary.Controls.Add(tlpTotals, 0, 2);
            tlpSummary.Controls.Add(lblSummaryNote, 0, 3);
            tlpSummary.Controls.Add(tlpAuth, 0, 4);
            tlpSummary.Controls.Add(tlpSummaryButtons, 0, 5);
            tlpSummary.Dock = DockStyle.Top;
            tlpSummary.Margin = new Padding(0);
            tlpSummary.Name = "tlpSummary";
            tlpSummary.RowCount = 6;
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpSummary.TabIndex = 0;
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.AutoSize = true;
            lblSummaryTitle.BackColor = Color.Transparent;
            lblSummaryTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSummaryTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblSummaryTitle.Margin = new Padding(0, 0, 0, 14);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "Summary";
            // 
            // tlpSummaryInputs
            // 
            tlpSummaryInputs.AutoSize = true;
            tlpSummaryInputs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpSummaryInputs.BackColor = Color.Transparent;
            tlpSummaryInputs.ColumnCount = 3;
            tlpSummaryInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpSummaryInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpSummaryInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333F));
            tlpSummaryInputs.Controls.Add(lblTaxRateCap, 0, 0);
            tlpSummaryInputs.Controls.Add(txtTaxRate, 0, 1);
            tlpSummaryInputs.Controls.Add(lblDiscountTypeCap, 1, 0);
            tlpSummaryInputs.Controls.Add(cboDiscountType, 1, 1);
            tlpSummaryInputs.Controls.Add(lblDiscountCap, 2, 0);
            tlpSummaryInputs.Controls.Add(txtDiscount, 2, 1);
            tlpSummaryInputs.Dock = DockStyle.Top;
            tlpSummaryInputs.Margin = new Padding(0);
            tlpSummaryInputs.Name = "tlpSummaryInputs";
            tlpSummaryInputs.RowCount = 2;
            tlpSummaryInputs.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpSummaryInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpSummaryInputs.TabIndex = 1;
            // 
            // lblTaxRateCap
            // 
            lblTaxRateCap.AutoSize = true;
            lblTaxRateCap.BackColor = Color.Transparent;
            lblTaxRateCap.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTaxRateCap.ForeColor = Color.FromArgb(20, 34, 51);
            lblTaxRateCap.Margin = new Padding(0, 0, 6, 6);
            lblTaxRateCap.Name = "lblTaxRateCap";
            lblTaxRateCap.TabIndex = 0;
            lblTaxRateCap.Text = "Tax rate (%) *";
            lblTaxRateCap.UseMnemonic = false;
            // 
            // txtTaxRate
            // 
            txtTaxRate.AutoSize = false;
            txtTaxRate.BackColor = Color.White;
            txtTaxRate.BorderStyle = BorderStyle.FixedSingle;
            txtTaxRate.Dock = DockStyle.Fill;
            txtTaxRate.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTaxRate.ForeColor = Color.FromArgb(20, 34, 51);
            txtTaxRate.Margin = new Padding(0, 0, 6, 14);
            txtTaxRate.MaxLength = 6;
            txtTaxRate.Name = "txtTaxRate";
            txtTaxRate.PlaceholderText = "12";
            txtTaxRate.Size = new Size(200, 40);
            txtTaxRate.TabIndex = 1;
            // 
            // lblDiscountTypeCap
            // 
            lblDiscountTypeCap.AutoSize = true;
            lblDiscountTypeCap.BackColor = Color.Transparent;
            lblDiscountTypeCap.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiscountTypeCap.ForeColor = Color.FromArgb(20, 34, 51);
            lblDiscountTypeCap.Margin = new Padding(6, 0, 6, 6);
            lblDiscountTypeCap.Name = "lblDiscountTypeCap";
            lblDiscountTypeCap.TabIndex = 2;
            lblDiscountTypeCap.Text = "Discount type";
            lblDiscountTypeCap.UseMnemonic = false;
            // 
            // cboDiscountType
            // 
            cboDiscountType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboDiscountType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDiscountType.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboDiscountType.FormattingEnabled = true;
            cboDiscountType.Margin = new Padding(6, 0, 6, 14);
            cboDiscountType.Name = "cboDiscountType";
            cboDiscountType.Size = new Size(200, 32);
            cboDiscountType.TabIndex = 3;
            // 
            // lblDiscountCap
            // 
            lblDiscountCap.AutoSize = true;
            lblDiscountCap.BackColor = Color.Transparent;
            lblDiscountCap.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiscountCap.ForeColor = Color.FromArgb(20, 34, 51);
            lblDiscountCap.Margin = new Padding(6, 0, 0, 6);
            lblDiscountCap.Name = "lblDiscountCap";
            lblDiscountCap.TabIndex = 4;
            lblDiscountCap.Text = "Discount (amount)";
            lblDiscountCap.UseMnemonic = false;
            // 
            // txtDiscount
            // 
            txtDiscount.AutoSize = false;
            txtDiscount.BackColor = Color.White;
            txtDiscount.BorderStyle = BorderStyle.FixedSingle;
            txtDiscount.Dock = DockStyle.Fill;
            txtDiscount.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDiscount.ForeColor = Color.FromArgb(20, 34, 51);
            txtDiscount.Margin = new Padding(6, 0, 0, 14);
            txtDiscount.MaxLength = 11;
            txtDiscount.Name = "txtDiscount";
            txtDiscount.PlaceholderText = "0.00";
            txtDiscount.Size = new Size(200, 40);
            txtDiscount.TabIndex = 5;
            // 
            // tlpTotals
            // 
            tlpTotals.AutoSize = true;
            tlpTotals.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpTotals.BackColor = Color.Transparent;
            tlpTotals.ColumnCount = 5;
            tlpTotals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tlpTotals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tlpTotals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tlpTotals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tlpTotals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tlpTotals.Controls.Add(lblSubtotalCap, 0, 0);
            tlpTotals.Controls.Add(lblSubtotal, 0, 1);
            tlpTotals.Controls.Add(lblDiscountAmountCap, 1, 0);
            tlpTotals.Controls.Add(lblDiscountAmount, 1, 1);
            tlpTotals.Controls.Add(lblTaxableCap, 2, 0);
            tlpTotals.Controls.Add(lblTaxable, 2, 1);
            tlpTotals.Controls.Add(lblTaxCap, 3, 0);
            tlpTotals.Controls.Add(lblTaxAmount, 3, 1);
            tlpTotals.Controls.Add(lblGrandCap, 4, 0);
            tlpTotals.Controls.Add(lblGrandTotal, 4, 1);
            tlpTotals.Dock = DockStyle.Top;
            tlpTotals.Margin = new Padding(0, 0, 0, 8);
            tlpTotals.Name = "tlpTotals";
            tlpTotals.RowCount = 2;
            tlpTotals.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTotals.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpTotals.TabIndex = 2;
            // 
            // lblSubtotalCap
            // 
            lblSubtotalCap.AutoSize = true;
            lblSubtotalCap.BackColor = Color.Transparent;
            lblSubtotalCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtotalCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblSubtotalCap.Margin = new Padding(0, 0, 8, 2);
            lblSubtotalCap.Name = "lblSubtotalCap";
            lblSubtotalCap.TabIndex = 0;
            lblSubtotalCap.Text = "Subtotal";
            lblSubtotalCap.UseMnemonic = false;
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.BackColor = Color.Transparent;
            lblSubtotal.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtotal.ForeColor = Color.FromArgb(20, 34, 51);
            lblSubtotal.Margin = new Padding(0, 0, 8, 0);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.TabIndex = 1;
            lblSubtotal.Text = "0.00";
            lblSubtotal.UseMnemonic = false;
            // 
            // lblDiscountAmountCap
            // 
            lblDiscountAmountCap.AutoSize = true;
            lblDiscountAmountCap.BackColor = Color.Transparent;
            lblDiscountAmountCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiscountAmountCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblDiscountAmountCap.Margin = new Padding(0, 0, 8, 2);
            lblDiscountAmountCap.Name = "lblDiscountAmountCap";
            lblDiscountAmountCap.TabIndex = 2;
            lblDiscountAmountCap.Text = "Discount amount";
            lblDiscountAmountCap.UseMnemonic = false;
            // 
            // lblDiscountAmount
            // 
            lblDiscountAmount.AutoSize = true;
            lblDiscountAmount.BackColor = Color.Transparent;
            lblDiscountAmount.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiscountAmount.ForeColor = Color.FromArgb(20, 34, 51);
            lblDiscountAmount.Margin = new Padding(0, 0, 8, 0);
            lblDiscountAmount.Name = "lblDiscountAmount";
            lblDiscountAmount.TabIndex = 3;
            lblDiscountAmount.Text = "0.00";
            lblDiscountAmount.UseMnemonic = false;
            // 
            // lblTaxableCap
            // 
            lblTaxableCap.AutoSize = true;
            lblTaxableCap.BackColor = Color.Transparent;
            lblTaxableCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTaxableCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblTaxableCap.Margin = new Padding(0, 0, 8, 2);
            lblTaxableCap.Name = "lblTaxableCap";
            lblTaxableCap.TabIndex = 4;
            lblTaxableCap.Text = "After discount";
            lblTaxableCap.UseMnemonic = false;
            // 
            // lblTaxable
            // 
            lblTaxable.AutoSize = true;
            lblTaxable.BackColor = Color.Transparent;
            lblTaxable.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTaxable.ForeColor = Color.FromArgb(20, 34, 51);
            lblTaxable.Margin = new Padding(0, 0, 8, 0);
            lblTaxable.Name = "lblTaxable";
            lblTaxable.TabIndex = 5;
            lblTaxable.Text = "0.00";
            lblTaxable.UseMnemonic = false;
            // 
            // lblTaxCap
            // 
            lblTaxCap.AutoSize = true;
            lblTaxCap.BackColor = Color.Transparent;
            lblTaxCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTaxCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblTaxCap.Margin = new Padding(0, 0, 8, 2);
            lblTaxCap.Name = "lblTaxCap";
            lblTaxCap.TabIndex = 6;
            lblTaxCap.Text = "Tax amount";
            lblTaxCap.UseMnemonic = false;
            // 
            // lblTaxAmount
            // 
            lblTaxAmount.AutoSize = true;
            lblTaxAmount.BackColor = Color.Transparent;
            lblTaxAmount.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTaxAmount.ForeColor = Color.FromArgb(20, 34, 51);
            lblTaxAmount.Margin = new Padding(0, 0, 8, 0);
            lblTaxAmount.Name = "lblTaxAmount";
            lblTaxAmount.TabIndex = 7;
            lblTaxAmount.Text = "0.00";
            lblTaxAmount.UseMnemonic = false;
            // 
            // lblGrandCap
            // 
            lblGrandCap.AutoSize = true;
            lblGrandCap.BackColor = Color.Transparent;
            lblGrandCap.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGrandCap.ForeColor = Color.FromArgb(91, 107, 124);
            lblGrandCap.Margin = new Padding(0, 0, 8, 2);
            lblGrandCap.Name = "lblGrandCap";
            lblGrandCap.TabIndex = 8;
            lblGrandCap.Text = "GRAND TOTAL";
            lblGrandCap.UseMnemonic = false;
            // 
            // lblGrandTotal
            // 
            lblGrandTotal.AutoSize = true;
            lblGrandTotal.BackColor = Color.Transparent;
            lblGrandTotal.Font = new Font("Segoe UI", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblGrandTotal.ForeColor = Color.FromArgb(18, 122, 140);
            lblGrandTotal.Margin = new Padding(0, 0, 8, 0);
            lblGrandTotal.Name = "lblGrandTotal";
            lblGrandTotal.TabIndex = 9;
            lblGrandTotal.Text = "0.00";
            lblGrandTotal.UseMnemonic = false;
            // 
            // lblSummaryNote
            // 
            lblSummaryNote.AutoSize = true;
            lblSummaryNote.BackColor = Color.Transparent;
            lblSummaryNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSummaryNote.ForeColor = Color.FromArgb(180, 35, 24);
            lblSummaryNote.Margin = new Padding(0, 0, 0, 8);
            lblSummaryNote.Name = "lblSummaryNote";
            lblSummaryNote.TabIndex = 3;
            lblSummaryNote.UseMnemonic = false;
            // 
            // tlpAuth
            // 
            tlpAuth.AutoSize = true;
            tlpAuth.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpAuth.BackColor = Color.Transparent;
            tlpAuth.ColumnCount = 2;
            tlpAuth.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpAuth.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpAuth.Controls.Add(lblAuthMessage, 0, 0);
            tlpAuth.SetColumnSpan(lblAuthMessage, 2);
            tlpAuth.Controls.Add(lblAuthorizedBy, 0, 1);
            tlpAuth.Controls.Add(txtAuthorizedBy, 1, 1);
            tlpAuth.Dock = DockStyle.Top;
            tlpAuth.Margin = new Padding(0);
            tlpAuth.Name = "tlpAuth";
            tlpAuth.RowCount = 2;
            tlpAuth.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpAuth.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpAuth.TabIndex = 4;
            tlpAuth.Visible = false;
            // 
            // lblAuthMessage
            // 
            lblAuthMessage.AutoSize = true;
            lblAuthMessage.BackColor = Color.Transparent;
            lblAuthMessage.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAuthMessage.ForeColor = Color.FromArgb(154, 91, 0);
            lblAuthMessage.Margin = new Padding(0, 0, 0, 8);
            lblAuthMessage.MaximumSize = new Size(700, 0);
            lblAuthMessage.Name = "lblAuthMessage";
            lblAuthMessage.TabIndex = 0;
            lblAuthMessage.UseMnemonic = false;
            // 
            // lblAuthorizedBy
            // 
            lblAuthorizedBy.AutoSize = true;
            lblAuthorizedBy.BackColor = Color.Transparent;
            lblAuthorizedBy.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAuthorizedBy.ForeColor = Color.FromArgb(20, 34, 51);
            lblAuthorizedBy.Margin = new Padding(0, 11, 8, 0);
            lblAuthorizedBy.Name = "lblAuthorizedBy";
            lblAuthorizedBy.TabIndex = 1;
            lblAuthorizedBy.Text = "Authorized by *";
            lblAuthorizedBy.UseMnemonic = false;
            // 
            // txtAuthorizedBy
            // 
            txtAuthorizedBy.AutoSize = false;
            txtAuthorizedBy.BackColor = Color.White;
            txtAuthorizedBy.BorderStyle = BorderStyle.FixedSingle;
            txtAuthorizedBy.Dock = DockStyle.Fill;
            txtAuthorizedBy.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAuthorizedBy.ForeColor = Color.FromArgb(20, 34, 51);
            txtAuthorizedBy.Margin = new Padding(0, 0, 0, 14);
            txtAuthorizedBy.MaxLength = 100;
            txtAuthorizedBy.Name = "txtAuthorizedBy";
            txtAuthorizedBy.PlaceholderText = "Name of the person who approved the discount";
            txtAuthorizedBy.Size = new Size(200, 40);
            txtAuthorizedBy.TabIndex = 2;
            // 
            // tlpSummaryButtons
            // 
            tlpSummaryButtons.BackColor = Color.Transparent;
            tlpSummaryButtons.ColumnCount = 3;
            tlpSummaryButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tlpSummaryButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpSummaryButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpSummaryButtons.Controls.Add(btnSaveDraft, 0, 0);
            tlpSummaryButtons.Controls.Add(btnFinalize, 1, 0);
            tlpSummaryButtons.Controls.Add(btnNewBill, 2, 0);
            tlpSummaryButtons.Dock = DockStyle.Fill;
            tlpSummaryButtons.Margin = new Padding(0);
            tlpSummaryButtons.Name = "tlpSummaryButtons";
            tlpSummaryButtons.RowCount = 1;
            tlpSummaryButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpSummaryButtons.TabIndex = 5;
            // 
            // btnSaveDraft
            // 
            btnSaveDraft.BackColor = Color.White;
            btnSaveDraft.Dock = DockStyle.Fill;
            btnSaveDraft.FlatStyle = FlatStyle.Flat;
            btnSaveDraft.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSaveDraft.ForeColor = Color.FromArgb(20, 34, 51);
            btnSaveDraft.Margin = new Padding(0);
            btnSaveDraft.Name = "btnSaveDraft";
            btnSaveDraft.Size = new Size(120, 40);
            btnSaveDraft.TabIndex = 0;
            btnSaveDraft.Text = "Save Draft";
            btnSaveDraft.UseMnemonic = false;
            btnSaveDraft.UseVisualStyleBackColor = false;
            // 
            // btnFinalize
            // 
            btnFinalize.BackColor = Color.FromArgb(18, 122, 140);
            btnFinalize.Dock = DockStyle.Fill;
            btnFinalize.FlatStyle = FlatStyle.Flat;
            btnFinalize.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFinalize.ForeColor = Color.White;
            btnFinalize.Margin = new Padding(10, 0, 0, 0);
            btnFinalize.Name = "btnFinalize";
            btnFinalize.Size = new Size(120, 40);
            btnFinalize.TabIndex = 1;
            btnFinalize.Text = "Finalize Bill";
            btnFinalize.UseMnemonic = false;
            btnFinalize.UseVisualStyleBackColor = false;
            // 
            // btnNewBill
            // 
            btnNewBill.BackColor = Color.White;
            btnNewBill.Dock = DockStyle.Fill;
            btnNewBill.FlatStyle = FlatStyle.Flat;
            btnNewBill.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNewBill.ForeColor = Color.FromArgb(20, 34, 51);
            btnNewBill.Margin = new Padding(10, 0, 0, 0);
            btnNewBill.Name = "btnNewBill";
            btnNewBill.Size = new Size(120, 40);
            btnNewBill.TabIndex = 2;
            btnNewBill.Text = "New Bill";
            btnNewBill.UseMnemonic = false;
            btnNewBill.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // BillingForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Itemized Billing Calculator";
            Controls.Add(tlpRoot);
            Name = "BillingForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tlpMode.ResumeLayout(false);
            tlpMode.PerformLayout();
            pnlBody.ResumeLayout(false);
            pnlBody.PerformLayout();
            pnlList.ResumeLayout(false);
            pnlList.PerformLayout();
            pnlListCard.ResumeLayout(false);
            pnlListCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBills).EndInit();
            pnlListFooter.ResumeLayout(false);
            pnlListFooter.PerformLayout();
            flowListActions.ResumeLayout(false);
            flowListActions.PerformLayout();
            pnlListHeader.ResumeLayout(false);
            pnlListHeader.PerformLayout();
            flowListFilter.ResumeLayout(false);
            flowListFilter.PerformLayout();
            pnlBuilder.ResumeLayout(false);
            pnlBuilder.PerformLayout();
            tlpBuilder.ResumeLayout(false);
            tlpBuilder.PerformLayout();
            tlpLeft.ResumeLayout(false);
            tlpLeft.PerformLayout();
            pnlBillCard.ResumeLayout(false);
            pnlBillCard.PerformLayout();
            tlpBillCard.ResumeLayout(false);
            tlpBillCard.PerformLayout();
            tlpBill.ResumeLayout(false);
            tlpBill.PerformLayout();
            tlpFind.ResumeLayout(false);
            tlpFind.PerformLayout();
            pnlAddCard.ResumeLayout(false);
            pnlAddCard.PerformLayout();
            tlpAddCard.ResumeLayout(false);
            tlpAddCard.PerformLayout();
            tlpAdd.ResumeLayout(false);
            tlpAdd.PerformLayout();
            tlpLine.ResumeLayout(false);
            tlpLine.PerformLayout();
            tlpAddButtons.ResumeLayout(false);
            tlpAddButtons.PerformLayout();
            tlpRight.ResumeLayout(false);
            tlpRight.PerformLayout();
            pnlItemsCard.ResumeLayout(false);
            pnlItemsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            pnlItemsFooter.ResumeLayout(false);
            pnlItemsFooter.PerformLayout();
            flowItemsFooter.ResumeLayout(false);
            flowItemsFooter.PerformLayout();
            pnlItemsHeader.ResumeLayout(false);
            pnlItemsHeader.PerformLayout();
            pnlSummaryCard.ResumeLayout(false);
            pnlSummaryCard.PerformLayout();
            tlpSummary.ResumeLayout(false);
            tlpSummary.PerformLayout();
            tlpSummaryInputs.ResumeLayout(false);
            tlpSummaryInputs.PerformLayout();
            tlpTotals.ResumeLayout(false);
            tlpTotals.PerformLayout();
            tlpAuth.ResumeLayout(false);
            tlpAuth.PerformLayout();
            tlpSummaryButtons.ResumeLayout(false);
            tlpSummaryButtons.PerformLayout();
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
        private System.Windows.Forms.Button btnModeBuilder;
        private System.Windows.Forms.Button btnModeList;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.Panel pnlListCard;
        private System.Windows.Forms.DataGridView dgvBills;
        private System.Windows.Forms.Panel pnlListFooter;
        private System.Windows.Forms.Label lblListCount;
        private System.Windows.Forms.FlowLayoutPanel flowListActions;
        private System.Windows.Forms.Button btnDeleteDraft;
        private System.Windows.Forms.Button btnOpenBill;
        private System.Windows.Forms.Panel pnlListHeader;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.FlowLayoutPanel flowListFilter;
        private System.Windows.Forms.TextBox txtListSearch;
        private System.Windows.Forms.ComboBox cboListStatus;
        private System.Windows.Forms.Button btnListSearch;
        private System.Windows.Forms.Button btnListAll;
        private System.Windows.Forms.Panel pnlBuilder;
        private System.Windows.Forms.TableLayoutPanel tlpBuilder;
        private System.Windows.Forms.TableLayoutPanel tlpLeft;
        private System.Windows.Forms.Panel pnlBillCard;
        private System.Windows.Forms.TableLayoutPanel tlpBillCard;
        private System.Windows.Forms.Label lblBillTitle;
        private System.Windows.Forms.TableLayoutPanel tlpBill;
        private System.Windows.Forms.Label lblPatientSearch;
        private System.Windows.Forms.TableLayoutPanel tlpFind;
        private System.Windows.Forms.TextBox txtPatientSearch;
        private System.Windows.Forms.Button btnPatientFind;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.ComboBox cboPatient;
        private System.Windows.Forms.Label lblBillCode;
        private System.Windows.Forms.TextBox txtBillCode;
        private System.Windows.Forms.Label lblBillState;
        private System.Windows.Forms.Panel pnlAddCard;
        private System.Windows.Forms.TableLayoutPanel tlpAddCard;
        private System.Windows.Forms.Label lblAddTitle;
        private System.Windows.Forms.TableLayoutPanel tlpAdd;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.ComboBox cboItem;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.TableLayoutPanel tlpLine;
        private System.Windows.Forms.Label lblLineCaption;
        private System.Windows.Forms.Label lblLineAmount;
        private System.Windows.Forms.TableLayoutPanel tlpAddButtons;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnClearItem;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.Panel pnlItemsCard;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Panel pnlItemsFooter;
        private System.Windows.Forms.Label lblItemsInfo;
        private System.Windows.Forms.FlowLayoutPanel flowItemsFooter;
        private System.Windows.Forms.Panel pnlItemsHeader;
        private System.Windows.Forms.Label lblItemsTitle;
        private System.Windows.Forms.Panel pnlSummaryCard;
        private System.Windows.Forms.TableLayoutPanel tlpSummary;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.TableLayoutPanel tlpSummaryInputs;
        private System.Windows.Forms.Label lblTaxRateCap;
        private System.Windows.Forms.TextBox txtTaxRate;
        private System.Windows.Forms.Label lblDiscountTypeCap;
        private System.Windows.Forms.ComboBox cboDiscountType;
        private System.Windows.Forms.Label lblDiscountCap;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TableLayoutPanel tlpTotals;
        private System.Windows.Forms.Label lblSubtotalCap;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscountAmountCap;
        private System.Windows.Forms.Label lblDiscountAmount;
        private System.Windows.Forms.Label lblTaxableCap;
        private System.Windows.Forms.Label lblTaxable;
        private System.Windows.Forms.Label lblTaxCap;
        private System.Windows.Forms.Label lblTaxAmount;
        private System.Windows.Forms.Label lblGrandCap;
        private System.Windows.Forms.Label lblGrandTotal;
        private System.Windows.Forms.Label lblSummaryNote;
        private System.Windows.Forms.TableLayoutPanel tlpAuth;
        private System.Windows.Forms.Label lblAuthMessage;
        private System.Windows.Forms.Label lblAuthorizedBy;
        private System.Windows.Forms.TextBox txtAuthorizedBy;
        private System.Windows.Forms.TableLayoutPanel tlpSummaryButtons;
        private System.Windows.Forms.Button btnSaveDraft;
        private System.Windows.Forms.Button btnFinalize;
        private System.Windows.Forms.Button btnNewBill;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
