namespace HospitalManagementSystem.Views.MP03_DoctorScheduling
{
    partial class ScheduleBuilderForm
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

        // Redesign: page title + doctor line; weekly grid and block list cards on the left, the block editor and
        // leave dates cards on the right, and a footer card with the summary and Close.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblDoctorInfo = new Label();
            lblTitle = new Label();
            tlpMain = new TableLayoutPanel();
            tlpLeft = new TableLayoutPanel();
            pnlWeekCard = new Panel();
            dgvWeek = new DataGridView();
            pnlWeekHeader = new Panel();
            lblWeekTitle = new Label();
            lblWeekHint = new Label();
            pnlBlocksCard = new Panel();
            dgvBlocks = new DataGridView();
            pnlBlocksHeader = new Panel();
            lblBlocksTitle = new Label();
            tlpRight = new TableLayoutPanel();
            pnlBlockCard = new Panel();
            tlpBlock = new TableLayoutPanel();
            lblBlockTitle = new Label();
            lblBlockMode = new Label();
            tlpBlockFields = new TableLayoutPanel();
            lblDay = new Label();
            cboDay = new ComboBox();
            lblMinutes = new Label();
            txtMinutes = new TextBox();
            lblStart = new Label();
            dtpStart = new DateTimePicker();
            lblEnd = new Label();
            dtpEnd = new DateTimePicker();
            lblNotes = new Label();
            txtNotes = new TextBox();
            lblBlockInfo = new Label();
            tlpBlockButtons = new TableLayoutPanel();
            btnSaveBlock = new Button();
            btnNewBlock = new Button();
            btnDeleteBlock = new Button();
            pnlLeavesCard = new Panel();
            tlpLeaves = new TableLayoutPanel();
            lblLeavesTitle = new Label();
            dgvLeaves = new DataGridView();
            tlpLeaveForm = new TableLayoutPanel();
            lblLeaveDate = new Label();
            dtpLeave = new DateTimePicker();
            lblLeaveReason = new Label();
            txtLeaveReason = new TextBox();
            tlpLeaveButtons = new TableLayoutPanel();
            btnAddLeave = new Button();
            btnRemoveLeave = new Button();
            pnlFooter = new Panel();
            lblSummary = new Label();
            flowFooter = new FlowLayoutPanel();
            btnClose = new Button();
            errorProvider = new ErrorProvider(components);
            colMon = new DataGridViewTextBoxColumn();
            colTue = new DataGridViewTextBoxColumn();
            colWed = new DataGridViewTextBoxColumn();
            colThu = new DataGridViewTextBoxColumn();
            colFri = new DataGridViewTextBoxColumn();
            colSat = new DataGridViewTextBoxColumn();
            colSun = new DataGridViewTextBoxColumn();
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            tlpMain.SuspendLayout();
            tlpLeft.SuspendLayout();
            pnlWeekCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWeek).BeginInit();
            pnlWeekHeader.SuspendLayout();
            pnlBlocksCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBlocks).BeginInit();
            pnlBlocksHeader.SuspendLayout();
            tlpRight.SuspendLayout();
            pnlBlockCard.SuspendLayout();
            tlpBlock.SuspendLayout();
            tlpBlockFields.SuspendLayout();
            tlpBlockButtons.SuspendLayout();
            pnlLeavesCard.SuspendLayout();
            tlpLeaves.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLeaves).BeginInit();
            tlpLeaveForm.SuspendLayout();
            tlpLeaveButtons.SuspendLayout();
            pnlFooter.SuspendLayout();
            flowFooter.SuspendLayout();
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
            pnlTitle.Controls.Add(lblDoctorInfo);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Dock = DockStyle.Fill;
            pnlTitle.Margin = new Padding(0, 0, 0, 18);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Padding = new Padding(0);
            pnlTitle.TabIndex = 0;
            // 
            // lblDoctorInfo
            // 
            lblDoctorInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDoctorInfo.AutoEllipsis = true;
            lblDoctorInfo.BackColor = Color.Transparent;
            lblDoctorInfo.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDoctorInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblDoctorInfo.Location = new Point(-2, 34);
            lblDoctorInfo.Name = "lblDoctorInfo";
            lblDoctorInfo.Size = new Size(900, 22);
            lblDoctorInfo.TabIndex = 0;
            lblDoctorInfo.Text = "";
            lblDoctorInfo.UseMnemonic = false;
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
            lblTitle.Text = "Weekly Schedule";
            lblTitle.UseMnemonic = false;
            // 
            // tlpMain
            // 
            tlpMain.BackColor = Color.Transparent;
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 440F));
            tlpMain.Controls.Add(tlpLeft, 0, 0);
            tlpMain.Controls.Add(tlpRight, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Margin = new Padding(0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.TabIndex = 1;
            // 
            // tlpLeft
            // 
            tlpLeft.BackColor = Color.Transparent;
            tlpLeft.ColumnCount = 1;
            tlpLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLeft.Controls.Add(pnlWeekCard, 0, 0);
            tlpLeft.Controls.Add(pnlBlocksCard, 0, 1);
            tlpLeft.Dock = DockStyle.Fill;
            tlpLeft.Margin = new Padding(0, 0, 20, 0);
            tlpLeft.Name = "tlpLeft";
            tlpLeft.RowCount = 2;
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpLeft.TabIndex = 0;
            // 
            // pnlWeekCard
            // 
            pnlWeekCard.BackColor = Color.White;
            pnlWeekCard.Controls.Add(dgvWeek);
            pnlWeekCard.Controls.Add(pnlWeekHeader);
            pnlWeekCard.Dock = DockStyle.Fill;
            pnlWeekCard.Margin = new Padding(0, 0, 0, 20);
            pnlWeekCard.Name = "pnlWeekCard";
            pnlWeekCard.Padding = new Padding(1, 6, 1, 8);
            pnlWeekCard.TabIndex = 0;
            // 
            // dgvWeek
            // 
            dgvWeek.AllowUserToAddRows = false;
            dgvWeek.AllowUserToDeleteRows = false;
            dgvWeek.AllowUserToResizeRows = false;
            dgvWeek.BackgroundColor = Color.White;
            dgvWeek.BorderStyle = BorderStyle.None;
            dgvWeek.Dock = DockStyle.Fill;
            dgvWeek.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvWeek.Margin = new Padding(0);
            dgvWeek.MultiSelect = false;
            dgvWeek.Name = "dgvWeek";
            dgvWeek.ReadOnly = true;
            dgvWeek.RowHeadersVisible = false;
            dgvWeek.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvWeek.TabIndex = 0;
            dgvWeek.Columns.AddRange(new DataGridViewColumn[] { colMon, colTue, colWed, colThu, colFri, colSat, colSun });
            // 
            // pnlWeekHeader
            // 
            pnlWeekHeader.BackColor = Color.White;
            pnlWeekHeader.Controls.Add(lblWeekTitle);
            pnlWeekHeader.Controls.Add(lblWeekHint);
            pnlWeekHeader.Dock = DockStyle.Top;
            pnlWeekHeader.Margin = new Padding(0);
            pnlWeekHeader.Name = "pnlWeekHeader";
            pnlWeekHeader.Padding = new Padding(0);
            pnlWeekHeader.Size = new Size(600, 46);
            pnlWeekHeader.TabIndex = 1;
            // 
            // lblWeekTitle
            // 
            lblWeekTitle.AutoEllipsis = true;
            lblWeekTitle.BackColor = Color.Transparent;
            lblWeekTitle.Dock = DockStyle.Fill;
            lblWeekTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWeekTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblWeekTitle.Margin = new Padding(0);
            lblWeekTitle.Name = "lblWeekTitle";
            lblWeekTitle.Padding = new Padding(17, 0, 0, 0);
            lblWeekTitle.TabIndex = 0;
            lblWeekTitle.Text = "Weekly availability";
            lblWeekTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblWeekTitle.UseMnemonic = false;
            // 
            // lblWeekHint
            // 
            lblWeekHint.BackColor = Color.Transparent;
            lblWeekHint.Dock = DockStyle.Right;
            lblWeekHint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWeekHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblWeekHint.Margin = new Padding(0);
            lblWeekHint.Name = "lblWeekHint";
            lblWeekHint.Padding = new Padding(0, 0, 18, 0);
            lblWeekHint.Size = new Size(220, 46);
            lblWeekHint.TabIndex = 1;
            lblWeekHint.Text = "Click a block to edit it";
            lblWeekHint.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlBlocksCard
            // 
            pnlBlocksCard.BackColor = Color.White;
            pnlBlocksCard.Controls.Add(dgvBlocks);
            pnlBlocksCard.Controls.Add(pnlBlocksHeader);
            pnlBlocksCard.Dock = DockStyle.Fill;
            pnlBlocksCard.Margin = new Padding(0);
            pnlBlocksCard.Name = "pnlBlocksCard";
            pnlBlocksCard.Padding = new Padding(1, 6, 1, 8);
            pnlBlocksCard.TabIndex = 1;
            // 
            // dgvBlocks
            // 
            dgvBlocks.AllowUserToAddRows = false;
            dgvBlocks.AllowUserToDeleteRows = false;
            dgvBlocks.AllowUserToResizeRows = false;
            dgvBlocks.BackgroundColor = Color.White;
            dgvBlocks.BorderStyle = BorderStyle.None;
            dgvBlocks.Dock = DockStyle.Fill;
            dgvBlocks.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvBlocks.Margin = new Padding(0);
            dgvBlocks.MultiSelect = false;
            dgvBlocks.Name = "dgvBlocks";
            dgvBlocks.ReadOnly = true;
            dgvBlocks.RowHeadersVisible = false;
            dgvBlocks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBlocks.TabIndex = 0;
            // 
            // pnlBlocksHeader
            // 
            pnlBlocksHeader.BackColor = Color.White;
            pnlBlocksHeader.Controls.Add(lblBlocksTitle);
            pnlBlocksHeader.Dock = DockStyle.Top;
            pnlBlocksHeader.Margin = new Padding(0);
            pnlBlocksHeader.Name = "pnlBlocksHeader";
            pnlBlocksHeader.Padding = new Padding(0);
            pnlBlocksHeader.Size = new Size(600, 46);
            pnlBlocksHeader.TabIndex = 1;
            // 
            // lblBlocksTitle
            // 
            lblBlocksTitle.AutoEllipsis = true;
            lblBlocksTitle.BackColor = Color.Transparent;
            lblBlocksTitle.Dock = DockStyle.Fill;
            lblBlocksTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBlocksTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblBlocksTitle.Margin = new Padding(0);
            lblBlocksTitle.Name = "lblBlocksTitle";
            lblBlocksTitle.Padding = new Padding(17, 0, 0, 0);
            lblBlocksTitle.TabIndex = 0;
            lblBlocksTitle.Text = "Availability blocks";
            lblBlocksTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblBlocksTitle.UseMnemonic = false;
            // 
            // tlpRight
            // 
            tlpRight.BackColor = Color.Transparent;
            tlpRight.ColumnCount = 1;
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRight.Controls.Add(pnlBlockCard, 0, 0);
            tlpRight.Controls.Add(pnlLeavesCard, 0, 1);
            tlpRight.Dock = DockStyle.Fill;
            tlpRight.Margin = new Padding(0);
            tlpRight.Name = "tlpRight";
            tlpRight.RowCount = 2;
            tlpRight.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRight.TabIndex = 1;
            // 
            // pnlBlockCard
            // 
            pnlBlockCard.AutoSize = true;
            pnlBlockCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlBlockCard.BackColor = Color.White;
            pnlBlockCard.Controls.Add(tlpBlock);
            pnlBlockCard.Dock = DockStyle.Fill;
            pnlBlockCard.Margin = new Padding(0, 0, 0, 20);
            pnlBlockCard.Name = "pnlBlockCard";
            pnlBlockCard.Padding = new Padding(20);
            pnlBlockCard.TabIndex = 0;
            // 
            // tlpBlock
            // 
            tlpBlock.AutoSize = true;
            tlpBlock.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpBlock.BackColor = Color.Transparent;
            tlpBlock.ColumnCount = 1;
            tlpBlock.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBlock.Controls.Add(lblBlockTitle, 0, 0);
            tlpBlock.Controls.Add(lblBlockMode, 0, 1);
            tlpBlock.Controls.Add(tlpBlockFields, 0, 2);
            tlpBlock.Controls.Add(tlpBlockButtons, 0, 3);
            tlpBlock.Dock = DockStyle.Top;
            tlpBlock.Margin = new Padding(0);
            tlpBlock.Name = "tlpBlock";
            tlpBlock.RowCount = 4;
            tlpBlock.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBlock.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBlock.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBlock.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpBlock.TabIndex = 0;
            // 
            // lblBlockTitle
            // 
            lblBlockTitle.AutoSize = true;
            lblBlockTitle.BackColor = Color.Transparent;
            lblBlockTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBlockTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblBlockTitle.Margin = new Padding(0, 0, 0, 4);
            lblBlockTitle.Name = "lblBlockTitle";
            lblBlockTitle.TabIndex = 0;
            lblBlockTitle.Text = "Availability block";
            // 
            // lblBlockMode
            // 
            lblBlockMode.AutoSize = true;
            lblBlockMode.BackColor = Color.Transparent;
            lblBlockMode.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBlockMode.ForeColor = Color.FromArgb(18, 122, 140);
            lblBlockMode.Margin = new Padding(0, 0, 0, 14);
            lblBlockMode.MaximumSize = new Size(420, 0);
            lblBlockMode.Name = "lblBlockMode";
            lblBlockMode.TabIndex = 1;
            lblBlockMode.Text = "New block";
            lblBlockMode.UseMnemonic = false;
            // 
            // tlpBlockFields
            // 
            tlpBlockFields.AutoSize = true;
            tlpBlockFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpBlockFields.BackColor = Color.Transparent;
            tlpBlockFields.ColumnCount = 2;
            tlpBlockFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBlockFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBlockFields.Controls.Add(lblDay, 0, 0);
            tlpBlockFields.Controls.Add(cboDay, 0, 1);
            tlpBlockFields.Controls.Add(lblMinutes, 1, 0);
            tlpBlockFields.Controls.Add(txtMinutes, 1, 1);
            tlpBlockFields.Controls.Add(lblStart, 0, 2);
            tlpBlockFields.Controls.Add(dtpStart, 0, 3);
            tlpBlockFields.Controls.Add(lblEnd, 1, 2);
            tlpBlockFields.Controls.Add(dtpEnd, 1, 3);
            tlpBlockFields.Controls.Add(lblNotes, 0, 4);
            tlpBlockFields.SetColumnSpan(lblNotes, 2);
            tlpBlockFields.Controls.Add(txtNotes, 0, 5);
            tlpBlockFields.SetColumnSpan(txtNotes, 2);
            tlpBlockFields.Controls.Add(lblBlockInfo, 0, 6);
            tlpBlockFields.SetColumnSpan(lblBlockInfo, 2);
            tlpBlockFields.Dock = DockStyle.Top;
            tlpBlockFields.Margin = new Padding(0);
            tlpBlockFields.Name = "tlpBlockFields";
            tlpBlockFields.RowCount = 7;
            tlpBlockFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBlockFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpBlockFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBlockFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpBlockFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpBlockFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpBlockFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpBlockFields.TabIndex = 2;
            // 
            // lblDay
            // 
            lblDay.AutoSize = true;
            lblDay.BackColor = Color.Transparent;
            lblDay.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDay.ForeColor = Color.FromArgb(20, 34, 51);
            lblDay.Margin = new Padding(0, 0, 6, 6);
            lblDay.Name = "lblDay";
            lblDay.TabIndex = 0;
            lblDay.Text = "Day *";
            lblDay.UseMnemonic = false;
            // 
            // cboDay
            // 
            cboDay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboDay.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDay.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboDay.FormattingEnabled = true;
            cboDay.Margin = new Padding(0, 0, 6, 14);
            cboDay.Name = "cboDay";
            cboDay.Size = new Size(200, 32);
            cboDay.TabIndex = 1;
            // 
            // lblMinutes
            // 
            lblMinutes.AutoSize = true;
            lblMinutes.BackColor = Color.Transparent;
            lblMinutes.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMinutes.ForeColor = Color.FromArgb(20, 34, 51);
            lblMinutes.Margin = new Padding(6, 0, 0, 6);
            lblMinutes.Name = "lblMinutes";
            lblMinutes.TabIndex = 2;
            lblMinutes.Text = "Minutes per visit *";
            lblMinutes.UseMnemonic = false;
            // 
            // txtMinutes
            // 
            txtMinutes.AutoSize = false;
            txtMinutes.BackColor = Color.White;
            txtMinutes.BorderStyle = BorderStyle.FixedSingle;
            txtMinutes.Dock = DockStyle.Fill;
            txtMinutes.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMinutes.ForeColor = Color.FromArgb(20, 34, 51);
            txtMinutes.Margin = new Padding(6, 0, 0, 14);
            txtMinutes.MaxLength = 3;
            txtMinutes.Name = "txtMinutes";
            txtMinutes.PlaceholderText = "e.g. 15";
            txtMinutes.Size = new Size(200, 40);
            txtMinutes.TabIndex = 3;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.BackColor = Color.Transparent;
            lblStart.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStart.ForeColor = Color.FromArgb(20, 34, 51);
            lblStart.Margin = new Padding(0, 0, 6, 6);
            lblStart.Name = "lblStart";
            lblStart.TabIndex = 4;
            lblStart.Text = "Start time *";
            lblStart.UseMnemonic = false;
            // 
            // dtpStart
            // 
            dtpStart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpStart.CustomFormat = "hh:mm tt";
            dtpStart.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Margin = new Padding(0, 0, 6, 14);
            dtpStart.Name = "dtpStart";
            dtpStart.ShowUpDown = true;
            dtpStart.Size = new Size(200, 31);
            dtpStart.TabIndex = 5;
            // 
            // lblEnd
            // 
            lblEnd.AutoSize = true;
            lblEnd.BackColor = Color.Transparent;
            lblEnd.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEnd.ForeColor = Color.FromArgb(20, 34, 51);
            lblEnd.Margin = new Padding(6, 0, 0, 6);
            lblEnd.Name = "lblEnd";
            lblEnd.TabIndex = 6;
            lblEnd.Text = "End time *";
            lblEnd.UseMnemonic = false;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpEnd.CustomFormat = "hh:mm tt";
            dtpEnd.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Margin = new Padding(6, 0, 0, 14);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.ShowUpDown = true;
            dtpEnd.Size = new Size(200, 31);
            dtpEnd.TabIndex = 7;
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.BackColor = Color.Transparent;
            lblNotes.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNotes.ForeColor = Color.FromArgb(20, 34, 51);
            lblNotes.Margin = new Padding(0, 0, 0, 6);
            lblNotes.Name = "lblNotes";
            lblNotes.TabIndex = 8;
            lblNotes.Text = "Notes";
            lblNotes.UseMnemonic = false;
            // 
            // txtNotes
            // 
            txtNotes.AutoSize = false;
            txtNotes.BackColor = Color.White;
            txtNotes.BorderStyle = BorderStyle.FixedSingle;
            txtNotes.Dock = DockStyle.Fill;
            txtNotes.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNotes.ForeColor = Color.FromArgb(20, 34, 51);
            txtNotes.Margin = new Padding(0, 0, 0, 14);
            txtNotes.MaxLength = 200;
            txtNotes.Name = "txtNotes";
            txtNotes.PlaceholderText = "optional, e.g. Morning clinic";
            txtNotes.Size = new Size(200, 40);
            txtNotes.TabIndex = 9;
            // 
            // lblBlockInfo
            // 
            lblBlockInfo.AutoSize = true;
            lblBlockInfo.BackColor = Color.Transparent;
            lblBlockInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBlockInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblBlockInfo.Margin = new Padding(0, 0, 0, 6);
            lblBlockInfo.MaximumSize = new Size(380, 0);
            lblBlockInfo.Name = "lblBlockInfo";
            lblBlockInfo.TabIndex = 10;
            lblBlockInfo.Text = "";
            lblBlockInfo.UseMnemonic = false;
            // 
            // tlpBlockButtons
            // 
            tlpBlockButtons.BackColor = Color.Transparent;
            tlpBlockButtons.ColumnCount = 3;
            tlpBlockButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpBlockButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpBlockButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpBlockButtons.Controls.Add(btnSaveBlock, 0, 0);
            tlpBlockButtons.Controls.Add(btnNewBlock, 1, 0);
            tlpBlockButtons.Controls.Add(btnDeleteBlock, 2, 0);
            tlpBlockButtons.Dock = DockStyle.Fill;
            tlpBlockButtons.Margin = new Padding(0);
            tlpBlockButtons.Name = "tlpBlockButtons";
            tlpBlockButtons.RowCount = 1;
            tlpBlockButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBlockButtons.TabIndex = 3;
            // 
            // btnSaveBlock
            // 
            btnSaveBlock.BackColor = Color.FromArgb(18, 122, 140);
            btnSaveBlock.Dock = DockStyle.Fill;
            btnSaveBlock.FlatStyle = FlatStyle.Flat;
            btnSaveBlock.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSaveBlock.ForeColor = Color.White;
            btnSaveBlock.Margin = new Padding(0);
            btnSaveBlock.Name = "btnSaveBlock";
            btnSaveBlock.Size = new Size(120, 40);
            btnSaveBlock.TabIndex = 0;
            btnSaveBlock.Text = "Add Block";
            btnSaveBlock.UseMnemonic = false;
            btnSaveBlock.UseVisualStyleBackColor = false;
            // 
            // btnNewBlock
            // 
            btnNewBlock.BackColor = Color.White;
            btnNewBlock.Dock = DockStyle.Fill;
            btnNewBlock.FlatStyle = FlatStyle.Flat;
            btnNewBlock.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNewBlock.ForeColor = Color.FromArgb(20, 34, 51);
            btnNewBlock.Margin = new Padding(10, 0, 0, 0);
            btnNewBlock.Name = "btnNewBlock";
            btnNewBlock.Size = new Size(120, 40);
            btnNewBlock.TabIndex = 1;
            btnNewBlock.Text = "New Block";
            btnNewBlock.UseMnemonic = false;
            btnNewBlock.UseVisualStyleBackColor = false;
            // 
            // btnDeleteBlock
            // 
            btnDeleteBlock.BackColor = Color.FromArgb(180, 35, 24);
            btnDeleteBlock.Dock = DockStyle.Fill;
            btnDeleteBlock.Enabled = false;
            btnDeleteBlock.FlatStyle = FlatStyle.Flat;
            btnDeleteBlock.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDeleteBlock.ForeColor = Color.White;
            btnDeleteBlock.Margin = new Padding(10, 0, 0, 0);
            btnDeleteBlock.Name = "btnDeleteBlock";
            btnDeleteBlock.Size = new Size(120, 40);
            btnDeleteBlock.TabIndex = 2;
            btnDeleteBlock.Text = "Delete Block";
            btnDeleteBlock.UseMnemonic = false;
            btnDeleteBlock.UseVisualStyleBackColor = false;
            // 
            // pnlLeavesCard
            // 
            pnlLeavesCard.BackColor = Color.White;
            pnlLeavesCard.Controls.Add(tlpLeaves);
            pnlLeavesCard.Dock = DockStyle.Fill;
            pnlLeavesCard.Margin = new Padding(0);
            pnlLeavesCard.Name = "pnlLeavesCard";
            pnlLeavesCard.Padding = new Padding(20);
            pnlLeavesCard.TabIndex = 1;
            // 
            // tlpLeaves
            // 
            tlpLeaves.BackColor = Color.Transparent;
            tlpLeaves.ColumnCount = 1;
            tlpLeaves.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLeaves.Controls.Add(lblLeavesTitle, 0, 0);
            tlpLeaves.Controls.Add(dgvLeaves, 0, 1);
            tlpLeaves.Controls.Add(tlpLeaveForm, 0, 2);
            tlpLeaves.Controls.Add(tlpLeaveButtons, 0, 3);
            tlpLeaves.Dock = DockStyle.Fill;
            tlpLeaves.Margin = new Padding(0);
            tlpLeaves.Name = "tlpLeaves";
            tlpLeaves.RowCount = 4;
            tlpLeaves.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeaves.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpLeaves.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeaves.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpLeaves.TabIndex = 0;
            // 
            // lblLeavesTitle
            // 
            lblLeavesTitle.AutoSize = true;
            lblLeavesTitle.BackColor = Color.Transparent;
            lblLeavesTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLeavesTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblLeavesTitle.Margin = new Padding(0, 0, 0, 10);
            lblLeavesTitle.Name = "lblLeavesTitle";
            lblLeavesTitle.TabIndex = 0;
            lblLeavesTitle.Text = "Leave dates";
            // 
            // dgvLeaves
            // 
            dgvLeaves.AllowUserToAddRows = false;
            dgvLeaves.AllowUserToDeleteRows = false;
            dgvLeaves.AllowUserToResizeRows = false;
            dgvLeaves.BackgroundColor = Color.White;
            dgvLeaves.BorderStyle = BorderStyle.None;
            dgvLeaves.Dock = DockStyle.Fill;
            dgvLeaves.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvLeaves.Margin = new Padding(0, 0, 0, 12);
            dgvLeaves.MultiSelect = false;
            dgvLeaves.Name = "dgvLeaves";
            dgvLeaves.ReadOnly = true;
            dgvLeaves.RowHeadersVisible = false;
            dgvLeaves.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLeaves.TabIndex = 1;
            // 
            // tlpLeaveForm
            // 
            tlpLeaveForm.AutoSize = true;
            tlpLeaveForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpLeaveForm.BackColor = Color.Transparent;
            tlpLeaveForm.ColumnCount = 2;
            tlpLeaveForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            tlpLeaveForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            tlpLeaveForm.Controls.Add(lblLeaveDate, 0, 0);
            tlpLeaveForm.Controls.Add(dtpLeave, 0, 1);
            tlpLeaveForm.Controls.Add(lblLeaveReason, 1, 0);
            tlpLeaveForm.Controls.Add(txtLeaveReason, 1, 1);
            tlpLeaveForm.Dock = DockStyle.Top;
            tlpLeaveForm.Margin = new Padding(0);
            tlpLeaveForm.Name = "tlpLeaveForm";
            tlpLeaveForm.RowCount = 2;
            tlpLeaveForm.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpLeaveForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpLeaveForm.TabIndex = 2;
            // 
            // lblLeaveDate
            // 
            lblLeaveDate.AutoSize = true;
            lblLeaveDate.BackColor = Color.Transparent;
            lblLeaveDate.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLeaveDate.ForeColor = Color.FromArgb(20, 34, 51);
            lblLeaveDate.Margin = new Padding(0, 0, 6, 6);
            lblLeaveDate.Name = "lblLeaveDate";
            lblLeaveDate.TabIndex = 0;
            lblLeaveDate.Text = "Date *";
            lblLeaveDate.UseMnemonic = false;
            // 
            // dtpLeave
            // 
            dtpLeave.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpLeave.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpLeave.Format = DateTimePickerFormat.Short;
            dtpLeave.Margin = new Padding(0, 0, 6, 14);
            dtpLeave.Name = "dtpLeave";
            dtpLeave.Size = new Size(200, 31);
            dtpLeave.TabIndex = 1;
            // 
            // lblLeaveReason
            // 
            lblLeaveReason.AutoSize = true;
            lblLeaveReason.BackColor = Color.Transparent;
            lblLeaveReason.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLeaveReason.ForeColor = Color.FromArgb(20, 34, 51);
            lblLeaveReason.Margin = new Padding(6, 0, 0, 6);
            lblLeaveReason.Name = "lblLeaveReason";
            lblLeaveReason.TabIndex = 2;
            lblLeaveReason.Text = "Reason *";
            lblLeaveReason.UseMnemonic = false;
            // 
            // txtLeaveReason
            // 
            txtLeaveReason.AutoSize = false;
            txtLeaveReason.BackColor = Color.White;
            txtLeaveReason.BorderStyle = BorderStyle.FixedSingle;
            txtLeaveReason.Dock = DockStyle.Fill;
            txtLeaveReason.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLeaveReason.ForeColor = Color.FromArgb(20, 34, 51);
            txtLeaveReason.Margin = new Padding(6, 0, 0, 14);
            txtLeaveReason.MaxLength = 200;
            txtLeaveReason.Name = "txtLeaveReason";
            txtLeaveReason.PlaceholderText = "e.g. Annual leave";
            txtLeaveReason.Size = new Size(200, 40);
            txtLeaveReason.TabIndex = 3;
            // 
            // tlpLeaveButtons
            // 
            tlpLeaveButtons.BackColor = Color.Transparent;
            tlpLeaveButtons.ColumnCount = 2;
            tlpLeaveButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLeaveButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLeaveButtons.Controls.Add(btnAddLeave, 0, 0);
            tlpLeaveButtons.Controls.Add(btnRemoveLeave, 1, 0);
            tlpLeaveButtons.Dock = DockStyle.Fill;
            tlpLeaveButtons.Margin = new Padding(0);
            tlpLeaveButtons.Name = "tlpLeaveButtons";
            tlpLeaveButtons.RowCount = 1;
            tlpLeaveButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpLeaveButtons.TabIndex = 3;
            // 
            // btnAddLeave
            // 
            btnAddLeave.BackColor = Color.FromArgb(226, 242, 246);
            btnAddLeave.Dock = DockStyle.Fill;
            btnAddLeave.FlatStyle = FlatStyle.Flat;
            btnAddLeave.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAddLeave.ForeColor = Color.FromArgb(20, 34, 51);
            btnAddLeave.Margin = new Padding(0);
            btnAddLeave.Name = "btnAddLeave";
            btnAddLeave.Size = new Size(120, 40);
            btnAddLeave.TabIndex = 0;
            btnAddLeave.Text = "Add Leave";
            btnAddLeave.UseMnemonic = false;
            btnAddLeave.UseVisualStyleBackColor = false;
            // 
            // btnRemoveLeave
            // 
            btnRemoveLeave.BackColor = Color.White;
            btnRemoveLeave.Dock = DockStyle.Fill;
            btnRemoveLeave.Enabled = false;
            btnRemoveLeave.FlatStyle = FlatStyle.Flat;
            btnRemoveLeave.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRemoveLeave.ForeColor = Color.FromArgb(20, 34, 51);
            btnRemoveLeave.Margin = new Padding(10, 0, 0, 0);
            btnRemoveLeave.Name = "btnRemoveLeave";
            btnRemoveLeave.Size = new Size(120, 40);
            btnRemoveLeave.TabIndex = 1;
            btnRemoveLeave.Text = "Remove Selected";
            btnRemoveLeave.UseMnemonic = false;
            btnRemoveLeave.UseVisualStyleBackColor = false;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(lblSummary);
            pnlFooter.Controls.Add(flowFooter);
            pnlFooter.Dock = DockStyle.Fill;
            pnlFooter.Margin = new Padding(0, 20, 0, 0);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(0);
            pnlFooter.TabIndex = 2;
            // 
            // lblSummary
            // 
            lblSummary.AutoEllipsis = true;
            lblSummary.BackColor = Color.Transparent;
            lblSummary.Dock = DockStyle.Fill;
            lblSummary.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSummary.ForeColor = Color.FromArgb(91, 107, 124);
            lblSummary.Name = "lblSummary";
            lblSummary.Padding = new Padding(18, 0, 8, 0);
            lblSummary.TabIndex = 0;
            lblSummary.TextAlign = ContentAlignment.MiddleLeft;
            lblSummary.UseMnemonic = false;
            // 
            // flowFooter
            // 
            flowFooter.AutoSize = true;
            flowFooter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowFooter.BackColor = Color.Transparent;
            flowFooter.Controls.Add(btnClose);
            flowFooter.Dock = DockStyle.Right;
            flowFooter.Margin = new Padding(0);
            flowFooter.Name = "flowFooter";
            flowFooter.Padding = new Padding(0, 11, 18, 0);
            flowFooter.TabIndex = 1;
            flowFooter.WrapContents = false;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClose.ForeColor = Color.FromArgb(20, 34, 51);
            btnClose.Margin = new Padding(10, 0, 0, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(110, 40);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            btnClose.UseMnemonic = false;
            btnClose.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // colMon
            // 
            colMon.HeaderText = "Mon";
            colMon.MinimumWidth = 6;
            colMon.Name = "colMon";
            colMon.ReadOnly = true;
            colMon.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colTue
            // 
            colTue.HeaderText = "Tue";
            colTue.MinimumWidth = 6;
            colTue.Name = "colTue";
            colTue.ReadOnly = true;
            colTue.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colWed
            // 
            colWed.HeaderText = "Wed";
            colWed.MinimumWidth = 6;
            colWed.Name = "colWed";
            colWed.ReadOnly = true;
            colWed.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colThu
            // 
            colThu.HeaderText = "Thu";
            colThu.MinimumWidth = 6;
            colThu.Name = "colThu";
            colThu.ReadOnly = true;
            colThu.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colFri
            // 
            colFri.HeaderText = "Fri";
            colFri.MinimumWidth = 6;
            colFri.Name = "colFri";
            colFri.ReadOnly = true;
            colFri.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colSat
            // 
            colSat.HeaderText = "Sat";
            colSat.MinimumWidth = 6;
            colSat.Name = "colSat";
            colSat.ReadOnly = true;
            colSat.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colSun
            // 
            colSun.HeaderText = "Sun";
            colSun.MinimumWidth = 6;
            colSun.Name = "colSun";
            colSun.ReadOnly = true;
            colSun.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // ScheduleBuilderForm
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
            Text = "Weekly Schedule";
            WindowState = FormWindowState.Maximized;
            Controls.Add(tlpRoot);
            Name = "ScheduleBuilderForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            tlpLeft.ResumeLayout(false);
            tlpLeft.PerformLayout();
            pnlWeekCard.ResumeLayout(false);
            pnlWeekCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWeek).EndInit();
            pnlWeekHeader.ResumeLayout(false);
            pnlWeekHeader.PerformLayout();
            pnlBlocksCard.ResumeLayout(false);
            pnlBlocksCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBlocks).EndInit();
            pnlBlocksHeader.ResumeLayout(false);
            pnlBlocksHeader.PerformLayout();
            tlpRight.ResumeLayout(false);
            tlpRight.PerformLayout();
            pnlBlockCard.ResumeLayout(false);
            pnlBlockCard.PerformLayout();
            tlpBlock.ResumeLayout(false);
            tlpBlock.PerformLayout();
            tlpBlockFields.ResumeLayout(false);
            tlpBlockFields.PerformLayout();
            tlpBlockButtons.ResumeLayout(false);
            tlpBlockButtons.PerformLayout();
            pnlLeavesCard.ResumeLayout(false);
            pnlLeavesCard.PerformLayout();
            tlpLeaves.ResumeLayout(false);
            tlpLeaves.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLeaves).EndInit();
            tlpLeaveForm.ResumeLayout(false);
            tlpLeaveForm.PerformLayout();
            tlpLeaveButtons.ResumeLayout(false);
            tlpLeaveButtons.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            flowFooter.ResumeLayout(false);
            flowFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblDoctorInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpLeft;
        private System.Windows.Forms.Panel pnlWeekCard;
        private System.Windows.Forms.DataGridView dgvWeek;
        private System.Windows.Forms.Panel pnlWeekHeader;
        private System.Windows.Forms.Label lblWeekTitle;
        private System.Windows.Forms.Label lblWeekHint;
        private System.Windows.Forms.Panel pnlBlocksCard;
        private System.Windows.Forms.DataGridView dgvBlocks;
        private System.Windows.Forms.Panel pnlBlocksHeader;
        private System.Windows.Forms.Label lblBlocksTitle;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.Panel pnlBlockCard;
        private System.Windows.Forms.TableLayoutPanel tlpBlock;
        private System.Windows.Forms.Label lblBlockTitle;
        private System.Windows.Forms.Label lblBlockMode;
        private System.Windows.Forms.TableLayoutPanel tlpBlockFields;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.ComboBox cboDay;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.TextBox txtMinutes;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblBlockInfo;
        private System.Windows.Forms.TableLayoutPanel tlpBlockButtons;
        private System.Windows.Forms.Button btnSaveBlock;
        private System.Windows.Forms.Button btnNewBlock;
        private System.Windows.Forms.Button btnDeleteBlock;
        private System.Windows.Forms.Panel pnlLeavesCard;
        private System.Windows.Forms.TableLayoutPanel tlpLeaves;
        private System.Windows.Forms.Label lblLeavesTitle;
        private System.Windows.Forms.DataGridView dgvLeaves;
        private System.Windows.Forms.TableLayoutPanel tlpLeaveForm;
        private System.Windows.Forms.Label lblLeaveDate;
        private System.Windows.Forms.DateTimePicker dtpLeave;
        private System.Windows.Forms.Label lblLeaveReason;
        private System.Windows.Forms.TextBox txtLeaveReason;
        private System.Windows.Forms.TableLayoutPanel tlpLeaveButtons;
        private System.Windows.Forms.Button btnAddLeave;
        private System.Windows.Forms.Button btnRemoveLeave;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.FlowLayoutPanel flowFooter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFri;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSun;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBMinutes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBSlots;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLStatus;
    }
}
