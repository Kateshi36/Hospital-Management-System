namespace HospitalManagementSystem.Views.MP09_Dispensing
{
    partial class DispensingForm
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

        // Redesign: page title + date line, Dispense / History toggle with the status on the right; the three
        // wizard steps are cards (list beside the details / quantity / payment card) above a navigation card;
        // history is one table card with search in the header and View Slip in the footer.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblDate = new Label();
            lblTitle = new Label();
            tlpMode = new TableLayoutPanel();
            btnModeDispense = new Button();
            btnModeHistory = new Button();
            lblStatus = new Label();
            pnlBody = new Panel();
            pnlHistory = new Panel();
            pnlHistoryCard = new Panel();
            dgvHistory = new DataGridView();
            pnlHistoryFooter = new Panel();
            lblHistCount = new Label();
            flowHistoryActions = new FlowLayoutPanel();
            btnViewSlip = new Button();
            pnlHistoryHeader = new Panel();
            lblHistoryTitle = new Label();
            flowHistFilter = new FlowLayoutPanel();
            txtHistSearch = new TextBox();
            btnHistSearch = new Button();
            btnHistAll = new Button();
            pnlDispense = new Panel();
            tlpWizard = new TableLayoutPanel();
            lblStep = new Label();
            pnlSteps = new Panel();
            pnlStep3 = new Panel();
            tlpStep3 = new TableLayoutPanel();
            pnlSummaryCard = new Panel();
            dgvSummary = new DataGridView();
            lblStep3Info = new Label();
            pnlPayCard = new Panel();
            tlpPay = new TableLayoutPanel();
            lblPayTitle = new Label();
            lblPayment = new Label();
            cboPayment = new ComboBox();
            lblTotal = new Label();
            lblStep3Note = new Label();
            pnlStep2 = new Panel();
            tlpStep2 = new TableLayoutPanel();
            pnlDispenseCard = new Panel();
            dgvDispense = new DataGridView();
            lblStep2Info = new Label();
            pnlQtyCard = new Panel();
            tlpQty = new TableLayoutPanel();
            lblQtyTitle = new Label();
            lblSelLine = new Label();
            lblQty = new Label();
            nudQty = new NumericUpDown();
            tlpQtyButtons = new TableLayoutPanel();
            btnMax = new Button();
            btnNone = new Button();
            lblQtyHint = new Label();
            pnlStep1 = new Panel();
            tlpStep1 = new TableLayoutPanel();
            pnlRxCard = new Panel();
            dgvPrescriptions = new DataGridView();
            pnlRxFooter = new Panel();
            lblRxCount = new Label();
            flowRxFooter = new FlowLayoutPanel();
            pnlRxOptions = new Panel();
            chkOpenOnly = new CheckBox();
            pnlRxHeader = new Panel();
            lblRxTitle = new Label();
            flowRxFilter = new FlowLayoutPanel();
            txtRxSearch = new TextBox();
            btnRxSearch = new Button();
            btnRxAll = new Button();
            pnlRxDetailCard = new Panel();
            dgvRxLines = new DataGridView();
            lblBlocked = new Label();
            lblSelectedInfo = new Label();
            pnlRxDetailHeader = new Panel();
            lblRxDetailTitle = new Label();
            pnlNavCard = new Panel();
            tlpNav = new TableLayoutPanel();
            btnStartOver = new Button();
            lblNavHint = new Label();
            btnBack = new Button();
            btnNext = new Button();
            btnConfirm = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            tlpMode.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlHistory.SuspendLayout();
            pnlHistoryCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            pnlHistoryFooter.SuspendLayout();
            flowHistoryActions.SuspendLayout();
            pnlHistoryHeader.SuspendLayout();
            flowHistFilter.SuspendLayout();
            pnlDispense.SuspendLayout();
            tlpWizard.SuspendLayout();
            pnlSteps.SuspendLayout();
            pnlStep3.SuspendLayout();
            tlpStep3.SuspendLayout();
            pnlSummaryCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSummary).BeginInit();
            pnlPayCard.SuspendLayout();
            tlpPay.SuspendLayout();
            pnlStep2.SuspendLayout();
            tlpStep2.SuspendLayout();
            pnlDispenseCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDispense).BeginInit();
            pnlQtyCard.SuspendLayout();
            tlpQty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQty).BeginInit();
            tlpQtyButtons.SuspendLayout();
            pnlStep1.SuspendLayout();
            tlpStep1.SuspendLayout();
            pnlRxCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrescriptions).BeginInit();
            pnlRxFooter.SuspendLayout();
            flowRxFooter.SuspendLayout();
            pnlRxOptions.SuspendLayout();
            pnlRxHeader.SuspendLayout();
            flowRxFilter.SuspendLayout();
            pnlRxDetailCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRxLines).BeginInit();
            pnlRxDetailHeader.SuspendLayout();
            pnlNavCard.SuspendLayout();
            tlpNav.SuspendLayout();
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
            lblDate.Text = "Dispense the medicines of a prescription in three steps.";
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
            lblTitle.Text = "Medicine Dispensing";
            lblTitle.UseMnemonic = false;
            // 
            // tlpMode
            // 
            tlpMode.BackColor = Color.Transparent;
            tlpMode.ColumnCount = 3;
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMode.Controls.Add(btnModeDispense, 0, 0);
            tlpMode.Controls.Add(btnModeHistory, 1, 0);
            tlpMode.Controls.Add(lblStatus, 2, 0);
            tlpMode.Dock = DockStyle.Fill;
            tlpMode.Margin = new Padding(0, 0, 0, 18);
            tlpMode.Name = "tlpMode";
            tlpMode.RowCount = 1;
            tlpMode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMode.TabIndex = 1;
            // 
            // btnModeDispense
            // 
            btnModeDispense.BackColor = Color.FromArgb(18, 122, 140);
            btnModeDispense.FlatStyle = FlatStyle.Flat;
            btnModeDispense.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeDispense.ForeColor = Color.White;
            btnModeDispense.Margin = new Padding(0, 0, 8, 0);
            btnModeDispense.Name = "btnModeDispense";
            btnModeDispense.Size = new Size(200, 38);
            btnModeDispense.TabIndex = 0;
            btnModeDispense.Text = "Dispense Prescription";
            btnModeDispense.UseMnemonic = false;
            btnModeDispense.UseVisualStyleBackColor = false;
            // 
            // btnModeHistory
            // 
            btnModeHistory.BackColor = Color.White;
            btnModeHistory.FlatStyle = FlatStyle.Flat;
            btnModeHistory.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnModeHistory.ForeColor = Color.FromArgb(20, 34, 51);
            btnModeHistory.Margin = new Padding(0);
            btnModeHistory.Name = "btnModeHistory";
            btnModeHistory.Size = new Size(180, 38);
            btnModeHistory.TabIndex = 1;
            btnModeHistory.Text = "Dispensing History";
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
            lblStatus.TabIndex = 2;
            lblStatus.Text = "";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            lblStatus.UseMnemonic = false;
            // 
            // pnlBody
            // 
            pnlBody.BackColor = Color.Transparent;
            pnlBody.Controls.Add(pnlHistory);
            pnlBody.Controls.Add(pnlDispense);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Margin = new Padding(0);
            pnlBody.Name = "pnlBody";
            pnlBody.Padding = new Padding(0);
            pnlBody.TabIndex = 2;
            // 
            // pnlHistory
            // 
            pnlHistory.BackColor = Color.Transparent;
            pnlHistory.Controls.Add(pnlHistoryCard);
            pnlHistory.Dock = DockStyle.Fill;
            pnlHistory.Margin = new Padding(0);
            pnlHistory.Name = "pnlHistory";
            pnlHistory.Padding = new Padding(0);
            pnlHistory.TabIndex = 0;
            pnlHistory.Visible = false;
            // 
            // pnlHistoryCard
            // 
            pnlHistoryCard.BackColor = Color.White;
            pnlHistoryCard.Controls.Add(dgvHistory);
            pnlHistoryCard.Controls.Add(pnlHistoryFooter);
            pnlHistoryCard.Controls.Add(pnlHistoryHeader);
            pnlHistoryCard.Dock = DockStyle.Fill;
            pnlHistoryCard.Margin = new Padding(0);
            pnlHistoryCard.Name = "pnlHistoryCard";
            pnlHistoryCard.Padding = new Padding(1, 6, 1, 1);
            pnlHistoryCard.TabIndex = 0;
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvHistory.Margin = new Padding(0);
            dgvHistory.MultiSelect = false;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.TabIndex = 0;
            // 
            // pnlHistoryFooter
            // 
            pnlHistoryFooter.BackColor = Color.White;
            pnlHistoryFooter.Controls.Add(lblHistCount);
            pnlHistoryFooter.Controls.Add(flowHistoryActions);
            pnlHistoryFooter.Dock = DockStyle.Bottom;
            pnlHistoryFooter.Margin = new Padding(0);
            pnlHistoryFooter.Name = "pnlHistoryFooter";
            pnlHistoryFooter.Padding = new Padding(0);
            pnlHistoryFooter.Size = new Size(600, 56);
            pnlHistoryFooter.TabIndex = 1;
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
            lblHistCount.TextAlign = ContentAlignment.MiddleLeft;
            lblHistCount.UseMnemonic = false;
            // 
            // flowHistoryActions
            // 
            flowHistoryActions.AutoSize = true;
            flowHistoryActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowHistoryActions.BackColor = Color.Transparent;
            flowHistoryActions.Controls.Add(btnViewSlip);
            flowHistoryActions.Dock = DockStyle.Right;
            flowHistoryActions.Margin = new Padding(0);
            flowHistoryActions.Name = "flowHistoryActions";
            flowHistoryActions.Padding = new Padding(0, 9, 18, 0);
            flowHistoryActions.TabIndex = 1;
            flowHistoryActions.WrapContents = false;
            // 
            // btnViewSlip
            // 
            btnViewSlip.BackColor = Color.FromArgb(18, 122, 140);
            btnViewSlip.Enabled = false;
            btnViewSlip.FlatStyle = FlatStyle.Flat;
            btnViewSlip.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnViewSlip.ForeColor = Color.White;
            btnViewSlip.Margin = new Padding(10, 0, 0, 0);
            btnViewSlip.Name = "btnViewSlip";
            btnViewSlip.Size = new Size(120, 38);
            btnViewSlip.TabIndex = 0;
            btnViewSlip.Text = "View Slip";
            btnViewSlip.UseMnemonic = false;
            btnViewSlip.UseVisualStyleBackColor = false;
            // 
            // pnlHistoryHeader
            // 
            pnlHistoryHeader.BackColor = Color.White;
            pnlHistoryHeader.Controls.Add(lblHistoryTitle);
            pnlHistoryHeader.Controls.Add(flowHistFilter);
            pnlHistoryHeader.Dock = DockStyle.Top;
            pnlHistoryHeader.Margin = new Padding(0);
            pnlHistoryHeader.Name = "pnlHistoryHeader";
            pnlHistoryHeader.Padding = new Padding(0);
            pnlHistoryHeader.Size = new Size(600, 46);
            pnlHistoryHeader.TabIndex = 2;
            // 
            // lblHistoryTitle
            // 
            lblHistoryTitle.AutoEllipsis = true;
            lblHistoryTitle.BackColor = Color.Transparent;
            lblHistoryTitle.Dock = DockStyle.Fill;
            lblHistoryTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHistoryTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblHistoryTitle.Margin = new Padding(0);
            lblHistoryTitle.Name = "lblHistoryTitle";
            lblHistoryTitle.Padding = new Padding(17, 0, 0, 0);
            lblHistoryTitle.TabIndex = 0;
            lblHistoryTitle.Text = "Dispensing history";
            lblHistoryTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblHistoryTitle.UseMnemonic = false;
            // 
            // flowHistFilter
            // 
            flowHistFilter.AutoSize = true;
            flowHistFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowHistFilter.BackColor = Color.White;
            flowHistFilter.Controls.Add(txtHistSearch);
            flowHistFilter.Controls.Add(btnHistSearch);
            flowHistFilter.Controls.Add(btnHistAll);
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
            txtHistSearch.PlaceholderText = "Patient, prescription or dispensing code";
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
            btnHistAll.Size = new Size(90, 36);
            btnHistAll.TabIndex = 2;
            btnHistAll.Text = "Show all";
            btnHistAll.UseMnemonic = false;
            btnHistAll.UseVisualStyleBackColor = false;
            // 
            // pnlDispense
            // 
            pnlDispense.BackColor = Color.Transparent;
            pnlDispense.Controls.Add(tlpWizard);
            pnlDispense.Dock = DockStyle.Fill;
            pnlDispense.Margin = new Padding(0);
            pnlDispense.Name = "pnlDispense";
            pnlDispense.Padding = new Padding(0);
            pnlDispense.TabIndex = 1;
            // 
            // tlpWizard
            // 
            tlpWizard.BackColor = Color.Transparent;
            tlpWizard.ColumnCount = 1;
            tlpWizard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpWizard.Controls.Add(lblStep, 0, 0);
            tlpWizard.Controls.Add(pnlSteps, 0, 1);
            tlpWizard.Controls.Add(pnlNavCard, 0, 2);
            tlpWizard.Dock = DockStyle.Fill;
            tlpWizard.Margin = new Padding(0);
            tlpWizard.Name = "tlpWizard";
            tlpWizard.RowCount = 3;
            tlpWizard.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tlpWizard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpWizard.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            tlpWizard.TabIndex = 0;
            // 
            // lblStep
            // 
            lblStep.BackColor = Color.Transparent;
            lblStep.Dock = DockStyle.Fill;
            lblStep.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStep.ForeColor = Color.FromArgb(18, 122, 140);
            lblStep.Margin = new Padding(0);
            lblStep.Name = "lblStep";
            lblStep.TabIndex = 0;
            lblStep.Text = "Step 1 of 3  -  Select a prescription";
            lblStep.TextAlign = ContentAlignment.TopLeft;
            lblStep.UseMnemonic = false;
            // 
            // pnlSteps
            // 
            pnlSteps.BackColor = Color.Transparent;
            pnlSteps.Controls.Add(pnlStep3);
            pnlSteps.Controls.Add(pnlStep2);
            pnlSteps.Controls.Add(pnlStep1);
            pnlSteps.Dock = DockStyle.Fill;
            pnlSteps.Margin = new Padding(0);
            pnlSteps.Name = "pnlSteps";
            pnlSteps.Padding = new Padding(0);
            pnlSteps.TabIndex = 1;
            // 
            // pnlStep3
            // 
            pnlStep3.BackColor = Color.Transparent;
            pnlStep3.Controls.Add(tlpStep3);
            pnlStep3.Dock = DockStyle.Fill;
            pnlStep3.Margin = new Padding(0);
            pnlStep3.Name = "pnlStep3";
            pnlStep3.Padding = new Padding(0);
            pnlStep3.TabIndex = 0;
            pnlStep3.Visible = false;
            // 
            // tlpStep3
            // 
            tlpStep3.BackColor = Color.Transparent;
            tlpStep3.ColumnCount = 2;
            tlpStep3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpStep3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F));
            tlpStep3.Controls.Add(pnlSummaryCard, 0, 0);
            tlpStep3.Controls.Add(pnlPayCard, 1, 0);
            tlpStep3.Dock = DockStyle.Fill;
            tlpStep3.Margin = new Padding(0);
            tlpStep3.Name = "tlpStep3";
            tlpStep3.RowCount = 1;
            tlpStep3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpStep3.TabIndex = 0;
            // 
            // pnlSummaryCard
            // 
            pnlSummaryCard.BackColor = Color.White;
            pnlSummaryCard.Controls.Add(dgvSummary);
            pnlSummaryCard.Controls.Add(lblStep3Info);
            pnlSummaryCard.Dock = DockStyle.Fill;
            pnlSummaryCard.Margin = new Padding(0, 0, 20, 0);
            pnlSummaryCard.Name = "pnlSummaryCard";
            pnlSummaryCard.Padding = new Padding(1, 6, 1, 1);
            pnlSummaryCard.TabIndex = 0;
            // 
            // dgvSummary
            // 
            dgvSummary.AllowUserToAddRows = false;
            dgvSummary.AllowUserToDeleteRows = false;
            dgvSummary.AllowUserToResizeRows = false;
            dgvSummary.BackgroundColor = Color.White;
            dgvSummary.BorderStyle = BorderStyle.None;
            dgvSummary.Dock = DockStyle.Fill;
            dgvSummary.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvSummary.Margin = new Padding(0);
            dgvSummary.MultiSelect = false;
            dgvSummary.Name = "dgvSummary";
            dgvSummary.ReadOnly = true;
            dgvSummary.RowHeadersVisible = false;
            dgvSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSummary.TabIndex = 0;
            // 
            // lblStep3Info
            // 
            lblStep3Info.AutoSize = false;
            lblStep3Info.BackColor = Color.Transparent;
            lblStep3Info.Dock = DockStyle.Top;
            lblStep3Info.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep3Info.ForeColor = Color.FromArgb(20, 34, 51);
            lblStep3Info.Margin = new Padding(0);
            lblStep3Info.Name = "lblStep3Info";
            lblStep3Info.Padding = new Padding(17, 0, 18, 0);
            lblStep3Info.Size = new Size(400, 50);
            lblStep3Info.TabIndex = 1;
            lblStep3Info.TextAlign = ContentAlignment.MiddleLeft;
            lblStep3Info.UseMnemonic = false;
            // 
            // pnlPayCard
            // 
            pnlPayCard.BackColor = Color.White;
            pnlPayCard.Controls.Add(tlpPay);
            pnlPayCard.Dock = DockStyle.Fill;
            pnlPayCard.Margin = new Padding(0);
            pnlPayCard.Name = "pnlPayCard";
            pnlPayCard.Padding = new Padding(20);
            pnlPayCard.TabIndex = 1;
            // 
            // tlpPay
            // 
            tlpPay.AutoSize = true;
            tlpPay.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpPay.BackColor = Color.Transparent;
            tlpPay.ColumnCount = 1;
            tlpPay.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPay.Controls.Add(lblPayTitle, 0, 0);
            tlpPay.Controls.Add(lblPayment, 0, 1);
            tlpPay.Controls.Add(cboPayment, 0, 2);
            tlpPay.Controls.Add(lblTotal, 0, 3);
            tlpPay.Controls.Add(lblStep3Note, 0, 4);
            tlpPay.Dock = DockStyle.Top;
            tlpPay.Margin = new Padding(0);
            tlpPay.Name = "tlpPay";
            tlpPay.RowCount = 5;
            tlpPay.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPay.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPay.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpPay.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPay.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpPay.TabIndex = 0;
            // 
            // lblPayTitle
            // 
            lblPayTitle.AutoSize = true;
            lblPayTitle.BackColor = Color.Transparent;
            lblPayTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPayTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblPayTitle.Margin = new Padding(0, 0, 0, 14);
            lblPayTitle.Name = "lblPayTitle";
            lblPayTitle.TabIndex = 0;
            lblPayTitle.Text = "Payment";
            // 
            // lblPayment
            // 
            lblPayment.AutoSize = true;
            lblPayment.BackColor = Color.Transparent;
            lblPayment.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPayment.ForeColor = Color.FromArgb(20, 34, 51);
            lblPayment.Margin = new Padding(0, 0, 0, 6);
            lblPayment.Name = "lblPayment";
            lblPayment.TabIndex = 1;
            lblPayment.Text = "Payment method";
            lblPayment.UseMnemonic = false;
            // 
            // cboPayment
            // 
            cboPayment.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboPayment.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPayment.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboPayment.FormattingEnabled = true;
            cboPayment.Margin = new Padding(0, 0, 0, 14);
            cboPayment.Name = "cboPayment";
            cboPayment.Size = new Size(200, 32);
            cboPayment.TabIndex = 2;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.BackColor = Color.Transparent;
            lblTotal.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = Color.FromArgb(20, 34, 51);
            lblTotal.Margin = new Padding(0, 6, 0, 10);
            lblTotal.Name = "lblTotal";
            lblTotal.TabIndex = 3;
            lblTotal.UseMnemonic = false;
            // 
            // lblStep3Note
            // 
            lblStep3Note.AutoSize = true;
            lblStep3Note.BackColor = Color.Transparent;
            lblStep3Note.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStep3Note.ForeColor = Color.FromArgb(91, 107, 124);
            lblStep3Note.Margin = new Padding(0, 0, 0, 14);
            lblStep3Note.MaximumSize = new Size(350, 0);
            lblStep3Note.Name = "lblStep3Note";
            lblStep3Note.TabIndex = 4;
            lblStep3Note.UseMnemonic = false;
            // 
            // pnlStep2
            // 
            pnlStep2.BackColor = Color.Transparent;
            pnlStep2.Controls.Add(tlpStep2);
            pnlStep2.Dock = DockStyle.Fill;
            pnlStep2.Margin = new Padding(0);
            pnlStep2.Name = "pnlStep2";
            pnlStep2.Padding = new Padding(0);
            pnlStep2.TabIndex = 1;
            pnlStep2.Visible = false;
            // 
            // tlpStep2
            // 
            tlpStep2.BackColor = Color.Transparent;
            tlpStep2.ColumnCount = 2;
            tlpStep2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpStep2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F));
            tlpStep2.Controls.Add(pnlDispenseCard, 0, 0);
            tlpStep2.Controls.Add(pnlQtyCard, 1, 0);
            tlpStep2.Dock = DockStyle.Fill;
            tlpStep2.Margin = new Padding(0);
            tlpStep2.Name = "tlpStep2";
            tlpStep2.RowCount = 1;
            tlpStep2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpStep2.TabIndex = 0;
            // 
            // pnlDispenseCard
            // 
            pnlDispenseCard.BackColor = Color.White;
            pnlDispenseCard.Controls.Add(dgvDispense);
            pnlDispenseCard.Controls.Add(lblStep2Info);
            pnlDispenseCard.Dock = DockStyle.Fill;
            pnlDispenseCard.Margin = new Padding(0, 0, 20, 0);
            pnlDispenseCard.Name = "pnlDispenseCard";
            pnlDispenseCard.Padding = new Padding(1, 6, 1, 1);
            pnlDispenseCard.TabIndex = 0;
            // 
            // dgvDispense
            // 
            dgvDispense.AllowUserToAddRows = false;
            dgvDispense.AllowUserToDeleteRows = false;
            dgvDispense.AllowUserToResizeRows = false;
            dgvDispense.BackgroundColor = Color.White;
            dgvDispense.BorderStyle = BorderStyle.None;
            dgvDispense.Dock = DockStyle.Fill;
            dgvDispense.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvDispense.Margin = new Padding(0);
            dgvDispense.MultiSelect = false;
            dgvDispense.Name = "dgvDispense";
            dgvDispense.ReadOnly = true;
            dgvDispense.RowHeadersVisible = false;
            dgvDispense.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDispense.TabIndex = 0;
            // 
            // lblStep2Info
            // 
            lblStep2Info.AutoSize = false;
            lblStep2Info.BackColor = Color.Transparent;
            lblStep2Info.Dock = DockStyle.Top;
            lblStep2Info.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStep2Info.ForeColor = Color.FromArgb(20, 34, 51);
            lblStep2Info.Margin = new Padding(0);
            lblStep2Info.Name = "lblStep2Info";
            lblStep2Info.Padding = new Padding(17, 0, 18, 0);
            lblStep2Info.Size = new Size(400, 50);
            lblStep2Info.TabIndex = 1;
            lblStep2Info.TextAlign = ContentAlignment.MiddleLeft;
            lblStep2Info.UseMnemonic = false;
            // 
            // pnlQtyCard
            // 
            pnlQtyCard.BackColor = Color.White;
            pnlQtyCard.Controls.Add(tlpQty);
            pnlQtyCard.Dock = DockStyle.Fill;
            pnlQtyCard.Margin = new Padding(0);
            pnlQtyCard.Name = "pnlQtyCard";
            pnlQtyCard.Padding = new Padding(20);
            pnlQtyCard.TabIndex = 1;
            // 
            // tlpQty
            // 
            tlpQty.AutoSize = true;
            tlpQty.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpQty.BackColor = Color.Transparent;
            tlpQty.ColumnCount = 1;
            tlpQty.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpQty.Controls.Add(lblQtyTitle, 0, 0);
            tlpQty.Controls.Add(lblSelLine, 0, 1);
            tlpQty.Controls.Add(lblQty, 0, 2);
            tlpQty.Controls.Add(nudQty, 0, 3);
            tlpQty.Controls.Add(tlpQtyButtons, 0, 4);
            tlpQty.Controls.Add(lblQtyHint, 0, 5);
            tlpQty.Dock = DockStyle.Top;
            tlpQty.Margin = new Padding(0);
            tlpQty.Name = "tlpQty";
            tlpQty.RowCount = 6;
            tlpQty.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpQty.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpQty.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpQty.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpQty.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlpQty.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpQty.TabIndex = 0;
            // 
            // lblQtyTitle
            // 
            lblQtyTitle.AutoSize = true;
            lblQtyTitle.BackColor = Color.Transparent;
            lblQtyTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQtyTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblQtyTitle.Margin = new Padding(0, 0, 0, 8);
            lblQtyTitle.Name = "lblQtyTitle";
            lblQtyTitle.TabIndex = 0;
            lblQtyTitle.Text = "Quantity for the selected medicine";
            // 
            // lblSelLine
            // 
            lblSelLine.AutoSize = true;
            lblSelLine.BackColor = Color.Transparent;
            lblSelLine.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSelLine.ForeColor = Color.FromArgb(20, 34, 51);
            lblSelLine.Margin = new Padding(0, 0, 0, 14);
            lblSelLine.MaximumSize = new Size(350, 0);
            lblSelLine.Name = "lblSelLine";
            lblSelLine.TabIndex = 1;
            lblSelLine.Text = "Select a medicine in the list.";
            lblSelLine.UseMnemonic = false;
            // 
            // lblQty
            // 
            lblQty.AutoSize = true;
            lblQty.BackColor = Color.Transparent;
            lblQty.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblQty.ForeColor = Color.FromArgb(20, 34, 51);
            lblQty.Margin = new Padding(0, 0, 0, 6);
            lblQty.Name = "lblQty";
            lblQty.TabIndex = 2;
            lblQty.Text = "Quantity to dispense";
            lblQty.UseMnemonic = false;
            // 
            // nudQty
            // 
            nudQty.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            nudQty.BorderStyle = BorderStyle.FixedSingle;
            nudQty.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudQty.Margin = new Padding(0, 0, 0, 14);
            nudQty.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQty.Name = "nudQty";
            nudQty.Size = new Size(160, 31);
            nudQty.TabIndex = 3;
            nudQty.TextAlign = HorizontalAlignment.Right;
            // 
            // tlpQtyButtons
            // 
            tlpQtyButtons.BackColor = Color.Transparent;
            tlpQtyButtons.ColumnCount = 2;
            tlpQtyButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpQtyButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpQtyButtons.Controls.Add(btnMax, 0, 0);
            tlpQtyButtons.Controls.Add(btnNone, 1, 0);
            tlpQtyButtons.Dock = DockStyle.Fill;
            tlpQtyButtons.Margin = new Padding(0);
            tlpQtyButtons.Name = "tlpQtyButtons";
            tlpQtyButtons.RowCount = 1;
            tlpQtyButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpQtyButtons.TabIndex = 4;
            // 
            // btnMax
            // 
            btnMax.BackColor = Color.FromArgb(226, 242, 246);
            btnMax.Dock = DockStyle.Fill;
            btnMax.Enabled = false;
            btnMax.FlatStyle = FlatStyle.Flat;
            btnMax.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMax.ForeColor = Color.FromArgb(20, 34, 51);
            btnMax.Margin = new Padding(0, 0, 0, 10);
            btnMax.Name = "btnMax";
            btnMax.Size = new Size(120, 40);
            btnMax.TabIndex = 0;
            btnMax.Text = "Dispense maximum";
            btnMax.UseMnemonic = false;
            btnMax.UseVisualStyleBackColor = false;
            // 
            // btnNone
            // 
            btnNone.BackColor = Color.White;
            btnNone.Dock = DockStyle.Fill;
            btnNone.Enabled = false;
            btnNone.FlatStyle = FlatStyle.Flat;
            btnNone.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNone.ForeColor = Color.FromArgb(20, 34, 51);
            btnNone.Margin = new Padding(10, 0, 0, 10);
            btnNone.Name = "btnNone";
            btnNone.Size = new Size(120, 40);
            btnNone.TabIndex = 1;
            btnNone.Text = "Dispense none";
            btnNone.UseMnemonic = false;
            btnNone.UseVisualStyleBackColor = false;
            // 
            // lblQtyHint
            // 
            lblQtyHint.AutoSize = true;
            lblQtyHint.BackColor = Color.Transparent;
            lblQtyHint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblQtyHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblQtyHint.Margin = new Padding(0, 4, 0, 0);
            lblQtyHint.MaximumSize = new Size(350, 0);
            lblQtyHint.Name = "lblQtyHint";
            lblQtyHint.TabIndex = 5;
            lblQtyHint.UseMnemonic = false;
            // 
            // pnlStep1
            // 
            pnlStep1.BackColor = Color.Transparent;
            pnlStep1.Controls.Add(tlpStep1);
            pnlStep1.Dock = DockStyle.Fill;
            pnlStep1.Margin = new Padding(0);
            pnlStep1.Name = "pnlStep1";
            pnlStep1.Padding = new Padding(0);
            pnlStep1.TabIndex = 2;
            // 
            // tlpStep1
            // 
            tlpStep1.BackColor = Color.Transparent;
            tlpStep1.ColumnCount = 2;
            tlpStep1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            tlpStep1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            tlpStep1.Controls.Add(pnlRxCard, 0, 0);
            tlpStep1.Controls.Add(pnlRxDetailCard, 1, 0);
            tlpStep1.Dock = DockStyle.Fill;
            tlpStep1.Margin = new Padding(0);
            tlpStep1.Name = "tlpStep1";
            tlpStep1.RowCount = 1;
            tlpStep1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpStep1.TabIndex = 0;
            // 
            // pnlRxCard
            // 
            pnlRxCard.BackColor = Color.White;
            pnlRxCard.Controls.Add(dgvPrescriptions);
            pnlRxCard.Controls.Add(pnlRxFooter);
            pnlRxCard.Controls.Add(pnlRxOptions);
            pnlRxCard.Controls.Add(pnlRxHeader);
            pnlRxCard.Dock = DockStyle.Fill;
            pnlRxCard.Margin = new Padding(0, 0, 20, 0);
            pnlRxCard.Name = "pnlRxCard";
            pnlRxCard.Padding = new Padding(1, 6, 1, 1);
            pnlRxCard.TabIndex = 0;
            // 
            // dgvPrescriptions
            // 
            dgvPrescriptions.AllowUserToAddRows = false;
            dgvPrescriptions.AllowUserToDeleteRows = false;
            dgvPrescriptions.AllowUserToResizeRows = false;
            dgvPrescriptions.BackgroundColor = Color.White;
            dgvPrescriptions.BorderStyle = BorderStyle.None;
            dgvPrescriptions.Dock = DockStyle.Fill;
            dgvPrescriptions.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvPrescriptions.Margin = new Padding(0);
            dgvPrescriptions.MultiSelect = false;
            dgvPrescriptions.Name = "dgvPrescriptions";
            dgvPrescriptions.ReadOnly = true;
            dgvPrescriptions.RowHeadersVisible = false;
            dgvPrescriptions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrescriptions.TabIndex = 0;
            // 
            // pnlRxFooter
            // 
            pnlRxFooter.BackColor = Color.White;
            pnlRxFooter.Controls.Add(lblRxCount);
            pnlRxFooter.Controls.Add(flowRxFooter);
            pnlRxFooter.Dock = DockStyle.Bottom;
            pnlRxFooter.Margin = new Padding(0);
            pnlRxFooter.Name = "pnlRxFooter";
            pnlRxFooter.Padding = new Padding(0);
            pnlRxFooter.Size = new Size(600, 46);
            pnlRxFooter.TabIndex = 1;
            // 
            // lblRxCount
            // 
            lblRxCount.AutoEllipsis = true;
            lblRxCount.BackColor = Color.Transparent;
            lblRxCount.Dock = DockStyle.Fill;
            lblRxCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRxCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblRxCount.Name = "lblRxCount";
            lblRxCount.Padding = new Padding(18, 0, 8, 0);
            lblRxCount.TabIndex = 0;
            lblRxCount.TextAlign = ContentAlignment.MiddleLeft;
            lblRxCount.UseMnemonic = false;
            // 
            // flowRxFooter
            // 
            flowRxFooter.AutoSize = true;
            flowRxFooter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowRxFooter.BackColor = Color.Transparent;
            flowRxFooter.Dock = DockStyle.Right;
            flowRxFooter.Margin = new Padding(0);
            flowRxFooter.Name = "flowRxFooter";
            flowRxFooter.Padding = new Padding(0);
            flowRxFooter.TabIndex = 1;
            flowRxFooter.WrapContents = false;
            // 
            // pnlRxOptions
            // 
            pnlRxOptions.BackColor = Color.White;
            pnlRxOptions.Controls.Add(chkOpenOnly);
            pnlRxOptions.Dock = DockStyle.Top;
            pnlRxOptions.Margin = new Padding(0);
            pnlRxOptions.Name = "pnlRxOptions";
            pnlRxOptions.Padding = new Padding(0);
            pnlRxOptions.Size = new Size(600, 32);
            pnlRxOptions.TabIndex = 2;
            // 
            // chkOpenOnly
            // 
            chkOpenOnly.AutoSize = true;
            chkOpenOnly.BackColor = Color.Transparent;
            chkOpenOnly.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkOpenOnly.ForeColor = Color.FromArgb(20, 34, 51);
            chkOpenOnly.Location = new Point(18, 4);
            chkOpenOnly.Margin = new Padding(0);
            chkOpenOnly.Name = "chkOpenOnly";
            chkOpenOnly.TabIndex = 0;
            chkOpenOnly.Text = "Hide expired and fully dispensed";
            chkOpenOnly.UseMnemonic = false;
            chkOpenOnly.UseVisualStyleBackColor = false;
            // 
            // pnlRxHeader
            // 
            pnlRxHeader.BackColor = Color.White;
            pnlRxHeader.Controls.Add(lblRxTitle);
            pnlRxHeader.Controls.Add(flowRxFilter);
            pnlRxHeader.Dock = DockStyle.Top;
            pnlRxHeader.Margin = new Padding(0);
            pnlRxHeader.Name = "pnlRxHeader";
            pnlRxHeader.Padding = new Padding(0);
            pnlRxHeader.Size = new Size(600, 46);
            pnlRxHeader.TabIndex = 3;
            // 
            // lblRxTitle
            // 
            lblRxTitle.AutoEllipsis = true;
            lblRxTitle.BackColor = Color.Transparent;
            lblRxTitle.Dock = DockStyle.Fill;
            lblRxTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRxTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblRxTitle.Margin = new Padding(0);
            lblRxTitle.Name = "lblRxTitle";
            lblRxTitle.Padding = new Padding(17, 0, 0, 0);
            lblRxTitle.TabIndex = 0;
            lblRxTitle.Text = "Prescriptions";
            lblRxTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblRxTitle.UseMnemonic = false;
            // 
            // flowRxFilter
            // 
            flowRxFilter.AutoSize = true;
            flowRxFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowRxFilter.BackColor = Color.White;
            flowRxFilter.Controls.Add(txtRxSearch);
            flowRxFilter.Controls.Add(btnRxSearch);
            flowRxFilter.Controls.Add(btnRxAll);
            flowRxFilter.Dock = DockStyle.Right;
            flowRxFilter.Margin = new Padding(0);
            flowRxFilter.Name = "flowRxFilter";
            flowRxFilter.Padding = new Padding(0, 5, 18, 0);
            flowRxFilter.TabIndex = 1;
            flowRxFilter.WrapContents = false;
            // 
            // txtRxSearch
            // 
            txtRxSearch.AutoSize = false;
            txtRxSearch.BackColor = Color.White;
            txtRxSearch.BorderStyle = BorderStyle.FixedSingle;
            txtRxSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRxSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtRxSearch.Margin = new Padding(0);
            txtRxSearch.MaxLength = 60;
            txtRxSearch.Name = "txtRxSearch";
            txtRxSearch.PlaceholderText = "Name, patient code or prescription code";
            txtRxSearch.Size = new Size(200, 36);
            txtRxSearch.TabIndex = 0;
            // 
            // btnRxSearch
            // 
            btnRxSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnRxSearch.FlatStyle = FlatStyle.Flat;
            btnRxSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRxSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnRxSearch.Margin = new Padding(10, 0, 0, 0);
            btnRxSearch.Name = "btnRxSearch";
            btnRxSearch.Size = new Size(86, 36);
            btnRxSearch.TabIndex = 1;
            btnRxSearch.Text = "Search";
            btnRxSearch.UseMnemonic = false;
            btnRxSearch.UseVisualStyleBackColor = false;
            // 
            // btnRxAll
            // 
            btnRxAll.BackColor = Color.White;
            btnRxAll.FlatStyle = FlatStyle.Flat;
            btnRxAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRxAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnRxAll.Margin = new Padding(10, 0, 0, 0);
            btnRxAll.Name = "btnRxAll";
            btnRxAll.Size = new Size(90, 36);
            btnRxAll.TabIndex = 2;
            btnRxAll.Text = "Show all";
            btnRxAll.UseMnemonic = false;
            btnRxAll.UseVisualStyleBackColor = false;
            // 
            // pnlRxDetailCard
            // 
            pnlRxDetailCard.BackColor = Color.White;
            pnlRxDetailCard.Controls.Add(dgvRxLines);
            pnlRxDetailCard.Controls.Add(lblBlocked);
            pnlRxDetailCard.Controls.Add(lblSelectedInfo);
            pnlRxDetailCard.Controls.Add(pnlRxDetailHeader);
            pnlRxDetailCard.Dock = DockStyle.Fill;
            pnlRxDetailCard.Margin = new Padding(0);
            pnlRxDetailCard.Name = "pnlRxDetailCard";
            pnlRxDetailCard.Padding = new Padding(1, 6, 1, 1);
            pnlRxDetailCard.TabIndex = 1;
            // 
            // dgvRxLines
            // 
            dgvRxLines.AllowUserToAddRows = false;
            dgvRxLines.AllowUserToDeleteRows = false;
            dgvRxLines.AllowUserToResizeRows = false;
            dgvRxLines.BackgroundColor = Color.White;
            dgvRxLines.BorderStyle = BorderStyle.None;
            dgvRxLines.Dock = DockStyle.Fill;
            dgvRxLines.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvRxLines.Margin = new Padding(0);
            dgvRxLines.MultiSelect = false;
            dgvRxLines.Name = "dgvRxLines";
            dgvRxLines.ReadOnly = true;
            dgvRxLines.RowHeadersVisible = false;
            dgvRxLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRxLines.TabIndex = 0;
            // 
            // lblBlocked
            // 
            lblBlocked.BackColor = Color.Transparent;
            lblBlocked.Dock = DockStyle.Bottom;
            lblBlocked.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBlocked.ForeColor = Color.FromArgb(154, 91, 0);
            lblBlocked.Margin = new Padding(0);
            lblBlocked.Name = "lblBlocked";
            lblBlocked.Padding = new Padding(18, 0, 18, 0);
            lblBlocked.Size = new Size(400, 44);
            lblBlocked.TabIndex = 1;
            lblBlocked.TextAlign = ContentAlignment.MiddleLeft;
            lblBlocked.UseMnemonic = false;
            // 
            // lblSelectedInfo
            // 
            lblSelectedInfo.AutoSize = false;
            lblSelectedInfo.BackColor = Color.Transparent;
            lblSelectedInfo.Dock = DockStyle.Top;
            lblSelectedInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSelectedInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblSelectedInfo.Margin = new Padding(0);
            lblSelectedInfo.Name = "lblSelectedInfo";
            lblSelectedInfo.Padding = new Padding(18, 6, 18, 6);
            lblSelectedInfo.Size = new Size(400, 52);
            lblSelectedInfo.TabIndex = 2;
            lblSelectedInfo.TextAlign = ContentAlignment.MiddleLeft;
            lblSelectedInfo.UseMnemonic = false;
            // 
            // pnlRxDetailHeader
            // 
            pnlRxDetailHeader.BackColor = Color.White;
            pnlRxDetailHeader.Controls.Add(lblRxDetailTitle);
            pnlRxDetailHeader.Dock = DockStyle.Top;
            pnlRxDetailHeader.Margin = new Padding(0);
            pnlRxDetailHeader.Name = "pnlRxDetailHeader";
            pnlRxDetailHeader.Padding = new Padding(0);
            pnlRxDetailHeader.Size = new Size(600, 46);
            pnlRxDetailHeader.TabIndex = 3;
            // 
            // lblRxDetailTitle
            // 
            lblRxDetailTitle.AutoEllipsis = true;
            lblRxDetailTitle.BackColor = Color.Transparent;
            lblRxDetailTitle.Dock = DockStyle.Fill;
            lblRxDetailTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRxDetailTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblRxDetailTitle.Margin = new Padding(0);
            lblRxDetailTitle.Name = "lblRxDetailTitle";
            lblRxDetailTitle.Padding = new Padding(17, 0, 0, 0);
            lblRxDetailTitle.TabIndex = 0;
            lblRxDetailTitle.Text = "Selected prescription";
            lblRxDetailTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblRxDetailTitle.UseMnemonic = false;
            // 
            // pnlNavCard
            // 
            pnlNavCard.BackColor = Color.White;
            pnlNavCard.Controls.Add(tlpNav);
            pnlNavCard.Dock = DockStyle.Fill;
            pnlNavCard.Margin = new Padding(0, 16, 0, 0);
            pnlNavCard.Name = "pnlNavCard";
            pnlNavCard.Padding = new Padding(16, 11, 16, 11);
            pnlNavCard.TabIndex = 2;
            // 
            // tlpNav
            // 
            tlpNav.BackColor = Color.Transparent;
            tlpNav.ColumnCount = 5;
            tlpNav.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpNav.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpNav.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpNav.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpNav.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpNav.Controls.Add(btnStartOver, 0, 0);
            tlpNav.Controls.Add(lblNavHint, 1, 0);
            tlpNav.Controls.Add(btnBack, 2, 0);
            tlpNav.Controls.Add(btnNext, 3, 0);
            tlpNav.Controls.Add(btnConfirm, 4, 0);
            tlpNav.Dock = DockStyle.Fill;
            tlpNav.Margin = new Padding(0);
            tlpNav.Name = "tlpNav";
            tlpNav.RowCount = 1;
            tlpNav.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNav.TabIndex = 0;
            // 
            // btnStartOver
            // 
            btnStartOver.BackColor = Color.White;
            btnStartOver.FlatStyle = FlatStyle.Flat;
            btnStartOver.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnStartOver.ForeColor = Color.FromArgb(20, 34, 51);
            btnStartOver.Margin = new Padding(0, 0, 14, 0);
            btnStartOver.Name = "btnStartOver";
            btnStartOver.Size = new Size(120, 40);
            btnStartOver.TabIndex = 0;
            btnStartOver.Text = "Start Over";
            btnStartOver.UseMnemonic = false;
            btnStartOver.UseVisualStyleBackColor = false;
            // 
            // lblNavHint
            // 
            lblNavHint.AutoEllipsis = true;
            lblNavHint.BackColor = Color.Transparent;
            lblNavHint.Dock = DockStyle.Fill;
            lblNavHint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNavHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblNavHint.Margin = new Padding(0);
            lblNavHint.Name = "lblNavHint";
            lblNavHint.TabIndex = 1;
            lblNavHint.TextAlign = ContentAlignment.MiddleLeft;
            lblNavHint.UseMnemonic = false;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBack.ForeColor = Color.FromArgb(20, 34, 51);
            btnBack.Margin = new Padding(10, 0, 0, 0);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(110, 40);
            btnBack.TabIndex = 2;
            btnBack.Text = "<  Back";
            btnBack.UseMnemonic = false;
            btnBack.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.FromArgb(18, 122, 140);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNext.ForeColor = Color.White;
            btnNext.Margin = new Padding(10, 0, 0, 0);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(220, 40);
            btnNext.TabIndex = 3;
            btnNext.Text = "Next  >";
            btnNext.UseMnemonic = false;
            btnNext.UseVisualStyleBackColor = false;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.FromArgb(18, 122, 140);
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Margin = new Padding(10, 0, 0, 0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(230, 40);
            btnConfirm.TabIndex = 4;
            btnConfirm.Text = "Confirm and Dispense";
            btnConfirm.UseMnemonic = false;
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // DispensingForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Medicine Dispensing";
            Controls.Add(tlpRoot);
            Name = "DispensingForm";
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
            pnlHistoryCard.ResumeLayout(false);
            pnlHistoryCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            pnlHistoryFooter.ResumeLayout(false);
            pnlHistoryFooter.PerformLayout();
            flowHistoryActions.ResumeLayout(false);
            flowHistoryActions.PerformLayout();
            pnlHistoryHeader.ResumeLayout(false);
            pnlHistoryHeader.PerformLayout();
            flowHistFilter.ResumeLayout(false);
            flowHistFilter.PerformLayout();
            pnlDispense.ResumeLayout(false);
            pnlDispense.PerformLayout();
            tlpWizard.ResumeLayout(false);
            tlpWizard.PerformLayout();
            pnlSteps.ResumeLayout(false);
            pnlSteps.PerformLayout();
            pnlStep3.ResumeLayout(false);
            pnlStep3.PerformLayout();
            tlpStep3.ResumeLayout(false);
            tlpStep3.PerformLayout();
            pnlSummaryCard.ResumeLayout(false);
            pnlSummaryCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSummary).EndInit();
            pnlPayCard.ResumeLayout(false);
            pnlPayCard.PerformLayout();
            tlpPay.ResumeLayout(false);
            tlpPay.PerformLayout();
            pnlStep2.ResumeLayout(false);
            pnlStep2.PerformLayout();
            tlpStep2.ResumeLayout(false);
            tlpStep2.PerformLayout();
            pnlDispenseCard.ResumeLayout(false);
            pnlDispenseCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDispense).EndInit();
            pnlQtyCard.ResumeLayout(false);
            pnlQtyCard.PerformLayout();
            tlpQty.ResumeLayout(false);
            tlpQty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudQty).EndInit();
            tlpQtyButtons.ResumeLayout(false);
            tlpQtyButtons.PerformLayout();
            pnlStep1.ResumeLayout(false);
            pnlStep1.PerformLayout();
            tlpStep1.ResumeLayout(false);
            tlpStep1.PerformLayout();
            pnlRxCard.ResumeLayout(false);
            pnlRxCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrescriptions).EndInit();
            pnlRxFooter.ResumeLayout(false);
            pnlRxFooter.PerformLayout();
            flowRxFooter.ResumeLayout(false);
            flowRxFooter.PerformLayout();
            pnlRxOptions.ResumeLayout(false);
            pnlRxOptions.PerformLayout();
            pnlRxHeader.ResumeLayout(false);
            pnlRxHeader.PerformLayout();
            flowRxFilter.ResumeLayout(false);
            flowRxFilter.PerformLayout();
            pnlRxDetailCard.ResumeLayout(false);
            pnlRxDetailCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRxLines).EndInit();
            pnlRxDetailHeader.ResumeLayout(false);
            pnlRxDetailHeader.PerformLayout();
            pnlNavCard.ResumeLayout(false);
            pnlNavCard.PerformLayout();
            tlpNav.ResumeLayout(false);
            tlpNav.PerformLayout();
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
        private System.Windows.Forms.Button btnModeDispense;
        private System.Windows.Forms.Button btnModeHistory;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlHistory;
        private System.Windows.Forms.Panel pnlHistoryCard;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Panel pnlHistoryFooter;
        private System.Windows.Forms.Label lblHistCount;
        private System.Windows.Forms.FlowLayoutPanel flowHistoryActions;
        private System.Windows.Forms.Button btnViewSlip;
        private System.Windows.Forms.Panel pnlHistoryHeader;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.FlowLayoutPanel flowHistFilter;
        private System.Windows.Forms.TextBox txtHistSearch;
        private System.Windows.Forms.Button btnHistSearch;
        private System.Windows.Forms.Button btnHistAll;
        private System.Windows.Forms.Panel pnlDispense;
        private System.Windows.Forms.TableLayoutPanel tlpWizard;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Panel pnlSteps;
        private System.Windows.Forms.Panel pnlStep3;
        private System.Windows.Forms.TableLayoutPanel tlpStep3;
        private System.Windows.Forms.Panel pnlSummaryCard;
        private System.Windows.Forms.DataGridView dgvSummary;
        private System.Windows.Forms.Label lblStep3Info;
        private System.Windows.Forms.Panel pnlPayCard;
        private System.Windows.Forms.TableLayoutPanel tlpPay;
        private System.Windows.Forms.Label lblPayTitle;
        private System.Windows.Forms.Label lblPayment;
        private System.Windows.Forms.ComboBox cboPayment;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblStep3Note;
        private System.Windows.Forms.Panel pnlStep2;
        private System.Windows.Forms.TableLayoutPanel tlpStep2;
        private System.Windows.Forms.Panel pnlDispenseCard;
        private System.Windows.Forms.DataGridView dgvDispense;
        private System.Windows.Forms.Label lblStep2Info;
        private System.Windows.Forms.Panel pnlQtyCard;
        private System.Windows.Forms.TableLayoutPanel tlpQty;
        private System.Windows.Forms.Label lblQtyTitle;
        private System.Windows.Forms.Label lblSelLine;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.NumericUpDown nudQty;
        private System.Windows.Forms.TableLayoutPanel tlpQtyButtons;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Label lblQtyHint;
        private System.Windows.Forms.Panel pnlStep1;
        private System.Windows.Forms.TableLayoutPanel tlpStep1;
        private System.Windows.Forms.Panel pnlRxCard;
        private System.Windows.Forms.DataGridView dgvPrescriptions;
        private System.Windows.Forms.Panel pnlRxFooter;
        private System.Windows.Forms.Label lblRxCount;
        private System.Windows.Forms.FlowLayoutPanel flowRxFooter;
        private System.Windows.Forms.Panel pnlRxOptions;
        private System.Windows.Forms.CheckBox chkOpenOnly;
        private System.Windows.Forms.Panel pnlRxHeader;
        private System.Windows.Forms.Label lblRxTitle;
        private System.Windows.Forms.FlowLayoutPanel flowRxFilter;
        private System.Windows.Forms.TextBox txtRxSearch;
        private System.Windows.Forms.Button btnRxSearch;
        private System.Windows.Forms.Button btnRxAll;
        private System.Windows.Forms.Panel pnlRxDetailCard;
        private System.Windows.Forms.DataGridView dgvRxLines;
        private System.Windows.Forms.Label lblBlocked;
        private System.Windows.Forms.Label lblSelectedInfo;
        private System.Windows.Forms.Panel pnlRxDetailHeader;
        private System.Windows.Forms.Label lblRxDetailTitle;
        private System.Windows.Forms.Panel pnlNavCard;
        private System.Windows.Forms.TableLayoutPanel tlpNav;
        private System.Windows.Forms.Button btnStartOver;
        private System.Windows.Forms.Label lblNavHint;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
