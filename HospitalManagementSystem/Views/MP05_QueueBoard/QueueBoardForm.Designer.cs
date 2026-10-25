namespace HospitalManagementSystem.Views.MP05_QueueBoard
{
    partial class QueueBoardForm
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

        // Layout = redesign mockup, artboard "4 · MP05 Patient Queue Board" (px at 96 DPI):
        //   page padding 24 / 28; title + one-line hint on the left, doctor drop-down (300 px) + Refresh on the right.
        //   Left column: the navy NOW SERVING panel (token, patient, details, Mark Complete / Skip / Call Next) above
        //   the waiting-list card. Right column (380 px): 2 x 2 stat tiles above the check-in card.
        // chkAutoRefresh stays (hidden, checked): the hint line says the board refreshes every 15 seconds.

        private void InitializeComponent()
        {
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblTitle = new Label();
            lblDate = new Label();
            flpToolbar = new FlowLayoutPanel();
            cboDoctor = new ComboBox();
            btnRefresh = new Button();
            chkAutoRefresh = new CheckBox();
            tlpBody = new TableLayoutPanel();
            tlpLeft = new TableLayoutPanel();
            pnlNow = new Panel();
            tlpNow = new TableLayoutPanel();
            pnlNowToken = new Panel();
            lblNowToken = new Label();
            lblNowCaption = new Label();
            pnlNowInfo = new Panel();
            flpNowElapsed = new FlowLayoutPanel();
            lblNowStarted = new Label();
            lblNowTimer = new Label();
            lblNowTimerSuffix = new Label();
            lblNowDetail = new Label();
            lblNowPatient = new Label();
            tlpActions = new TableLayoutPanel();
            btnComplete = new Button();
            btnSkip = new Button();
            btnCallNext = new Button();
            pnlWaitingCard = new Panel();
            dgvWaiting = new DataGridView();
            pnlWaitingHeader = new Panel();
            flpWaitingTitle = new FlowLayoutPanel();
            lblWaitingTitle = new Label();
            lblWaitingCount = new Label();
            lblStatus = new Label();
            tlpRight = new TableLayoutPanel();
            tlpStats = new TableLayoutPanel();
            pnlStatCompleted = new Panel();
            lblStatCompleted = new Label();
            lblStatCompletedCaption = new Label();
            pnlStatWaiting = new Panel();
            lblStatWaiting = new Label();
            lblStatWaitingCaption = new Label();
            pnlStatEmergency = new Panel();
            lblStatEmergency = new Label();
            lblStatEmergencyCaption = new Label();
            pnlStatAverage = new Panel();
            lblStatAverage = new Label();
            lblStatAverageCaption = new Label();
            pnlCheckInCard = new Panel();
            tlpCheckIn = new TableLayoutPanel();
            lblCheckInTitle = new Label();
            lblCheckInHint = new Label();
            dgvCheckIn = new DataGridView();
            chkEmergency = new CheckBox();
            btnCheckIn = new Button();
            btnWalkIn = new Button();
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            flpToolbar.SuspendLayout();
            tlpBody.SuspendLayout();
            tlpLeft.SuspendLayout();
            pnlNow.SuspendLayout();
            tlpNow.SuspendLayout();
            pnlNowToken.SuspendLayout();
            pnlNowInfo.SuspendLayout();
            flpNowElapsed.SuspendLayout();
            tlpActions.SuspendLayout();
            pnlWaitingCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWaiting).BeginInit();
            pnlWaitingHeader.SuspendLayout();
            flpWaitingTitle.SuspendLayout();
            tlpRight.SuspendLayout();
            tlpStats.SuspendLayout();
            pnlStatCompleted.SuspendLayout();
            pnlStatWaiting.SuspendLayout();
            pnlStatEmergency.SuspendLayout();
            pnlStatAverage.SuspendLayout();
            pnlCheckInCard.SuspendLayout();
            tlpCheckIn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCheckIn).BeginInit();
            SuspendLayout();
            //
            // tlpRoot
            //
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 2;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRoot.Controls.Add(pnlTitle, 0, 0);
            tlpRoot.Controls.Add(flpToolbar, 1, 0);
            tlpRoot.Controls.Add(tlpBody, 0, 1);
            tlpRoot.SetColumnSpan(tlpBody, 2);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 2;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.Size = new Size(1120, 800);
            tlpRoot.TabIndex = 0;
            //
            // pnlTitle
            //
            pnlTitle.BackColor = Color.Transparent;
            pnlTitle.Controls.Add(lblDate);
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Controls.Add(chkAutoRefresh);
            pnlTitle.Dock = DockStyle.Fill;
            pnlTitle.Margin = new Padding(0, 0, 16, 18);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(700, 56);
            pnlTitle.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblTitle.Location = new Point(-4, -2);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Patient Queue Board";
            //
            // lblDate
            //
            lblDate.AutoEllipsis = true;
            lblDate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblDate.BackColor = Color.Transparent;
            lblDate.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDate.ForeColor = Color.FromArgb(91, 107, 124);
            lblDate.Location = new Point(-2, 34);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(700, 22);
            lblDate.TabIndex = 1;
            lblDate.Text = "Emergency patients are called first, then lowest token. Refreshes every 15 seconds.";
            //
            // chkAutoRefresh
            //
            chkAutoRefresh.AutoSize = true;
            chkAutoRefresh.Checked = true;
            chkAutoRefresh.CheckState = CheckState.Checked;
            chkAutoRefresh.Location = new Point(600, 0);
            chkAutoRefresh.Name = "chkAutoRefresh";
            chkAutoRefresh.TabIndex = 2;
            chkAutoRefresh.TabStop = false;
            chkAutoRefresh.Text = "Auto-refresh (15 s)";
            chkAutoRefresh.Visible = false;
            //
            // flpToolbar
            //
            flpToolbar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flpToolbar.AutoSize = true;
            flpToolbar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpToolbar.BackColor = Color.Transparent;
            flpToolbar.Controls.Add(cboDoctor);
            flpToolbar.Controls.Add(btnRefresh);
            flpToolbar.Margin = new Padding(0, 8, 0, 0);
            flpToolbar.Name = "flpToolbar";
            flpToolbar.Size = new Size(404, 40);
            flpToolbar.TabIndex = 1;
            flpToolbar.WrapContents = false;
            //
            // cboDoctor
            //
            cboDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDoctor.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboDoctor.Margin = new Padding(0);
            cboDoctor.Name = "cboDoctor";
            cboDoctor.Size = new Size(320, 32);
            cboDoctor.TabIndex = 0;
            //
            // btnRefresh
            //
            btnRefresh.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRefresh.Margin = new Padding(10, 0, 0, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(88, 40);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            //
            // tlpBody
            //
            tlpBody.BackColor = Color.Transparent;
            tlpBody.ColumnCount = 2;
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBody.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 380F));
            tlpBody.Controls.Add(tlpLeft, 0, 0);
            tlpBody.Controls.Add(tlpRight, 1, 0);
            tlpBody.Dock = DockStyle.Fill;
            tlpBody.Margin = new Padding(0);
            tlpBody.Name = "tlpBody";
            tlpBody.RowCount = 1;
            tlpBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBody.Size = new Size(1120, 726);
            tlpBody.TabIndex = 2;
            //
            // tlpLeft
            //
            tlpLeft.BackColor = Color.Transparent;
            tlpLeft.ColumnCount = 1;
            tlpLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLeft.Controls.Add(pnlNow, 0, 0);
            tlpLeft.Controls.Add(pnlWaitingCard, 0, 1);
            tlpLeft.Dock = DockStyle.Fill;
            tlpLeft.Margin = new Padding(0, 0, 20, 0);
            tlpLeft.Name = "tlpLeft";
            tlpLeft.RowCount = 2;
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 218F));
            tlpLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpLeft.Size = new Size(720, 726);
            tlpLeft.TabIndex = 0;
            //
            // pnlNow
            //
            pnlNow.BackColor = Color.FromArgb(27, 43, 63);
            pnlNow.Controls.Add(tlpNow);
            pnlNow.Dock = DockStyle.Fill;
            pnlNow.Margin = new Padding(0, 0, 0, 20);
            pnlNow.Name = "pnlNow";
            pnlNow.Padding = new Padding(28, 24, 28, 24);
            pnlNow.Size = new Size(720, 198);
            pnlNow.TabIndex = 0;
            //
            // tlpNow
            //
            tlpNow.BackColor = Color.FromArgb(27, 43, 63);
            tlpNow.ColumnCount = 3;
            tlpNow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 222F));
            tlpNow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpNow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tlpNow.Controls.Add(pnlNowToken, 0, 0);
            tlpNow.Controls.Add(pnlNowInfo, 1, 0);
            tlpNow.Controls.Add(tlpActions, 2, 0);
            tlpNow.Dock = DockStyle.Fill;
            tlpNow.Margin = new Padding(0);
            tlpNow.Name = "tlpNow";
            tlpNow.RowCount = 1;
            tlpNow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpNow.Size = new Size(664, 150);
            tlpNow.TabIndex = 0;
            //
            // pnlNowToken
            //
            pnlNowToken.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pnlNowToken.BackColor = Color.FromArgb(27, 43, 63);
            pnlNowToken.Controls.Add(lblNowToken);
            pnlNowToken.Controls.Add(lblNowCaption);
            pnlNowToken.Margin = new Padding(0, 0, 32, 0);
            pnlNowToken.Name = "pnlNowToken";
            pnlNowToken.Size = new Size(190, 124);
            pnlNowToken.TabIndex = 0;
            //
            // lblNowCaption
            //
            lblNowCaption.BackColor = Color.Transparent;
            lblNowCaption.Dock = DockStyle.Top;
            lblNowCaption.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNowCaption.ForeColor = Color.FromArgb(127, 211, 224);
            lblNowCaption.Name = "lblNowCaption";
            lblNowCaption.Size = new Size(190, 22);
            lblNowCaption.TabIndex = 0;
            lblNowCaption.Text = "NOW SERVING";
            lblNowCaption.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblNowToken
            //
            lblNowToken.BackColor = Color.Transparent;
            lblNowToken.Dock = DockStyle.Fill;
            lblNowToken.Font = new Font("Segoe UI", 60F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNowToken.ForeColor = Color.White;
            lblNowToken.Name = "lblNowToken";
            lblNowToken.Size = new Size(190, 102);
            lblNowToken.TabIndex = 1;
            lblNowToken.Text = "-";
            lblNowToken.TextAlign = ContentAlignment.MiddleCenter;
            lblNowToken.UseCompatibleTextRendering = false;
            //
            // pnlNowInfo
            //
            pnlNowInfo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pnlNowInfo.BackColor = Color.FromArgb(27, 43, 63);
            pnlNowInfo.Controls.Add(flpNowElapsed);
            pnlNowInfo.Controls.Add(lblNowDetail);
            pnlNowInfo.Controls.Add(lblNowPatient);
            pnlNowInfo.Margin = new Padding(0, 0, 32, 0);
            pnlNowInfo.Name = "pnlNowInfo";
            pnlNowInfo.Size = new Size(210, 104);
            pnlNowInfo.TabIndex = 1;
            //
            // lblNowPatient
            //
            lblNowPatient.AutoEllipsis = true;
            lblNowPatient.BackColor = Color.Transparent;
            lblNowPatient.Dock = DockStyle.Top;
            lblNowPatient.Font = new Font("Segoe UI", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNowPatient.ForeColor = Color.White;
            lblNowPatient.Name = "lblNowPatient";
            lblNowPatient.Size = new Size(210, 44);
            lblNowPatient.TabIndex = 0;
            lblNowPatient.Text = "No patient in consultation";
            lblNowPatient.UseMnemonic = false;
            //
            // lblNowDetail
            //
            lblNowDetail.AutoEllipsis = true;
            lblNowDetail.BackColor = Color.Transparent;
            lblNowDetail.Dock = DockStyle.Top;
            lblNowDetail.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNowDetail.ForeColor = Color.FromArgb(185, 203, 214);
            lblNowDetail.Name = "lblNowDetail";
            lblNowDetail.Padding = new Padding(2, 4, 0, 0);
            lblNowDetail.Size = new Size(210, 30);
            lblNowDetail.TabIndex = 1;
            lblNowDetail.UseMnemonic = false;
            //
            // flpNowElapsed
            //
            flpNowElapsed.BackColor = Color.Transparent;
            flpNowElapsed.Controls.Add(lblNowStarted);
            flpNowElapsed.Controls.Add(lblNowTimer);
            flpNowElapsed.Controls.Add(lblNowTimerSuffix);
            flpNowElapsed.Dock = DockStyle.Top;
            flpNowElapsed.Name = "flpNowElapsed";
            flpNowElapsed.Padding = new Padding(2, 4, 0, 0);
            flpNowElapsed.Size = new Size(210, 30);
            flpNowElapsed.TabIndex = 2;
            flpNowElapsed.WrapContents = false;
            //
            // lblNowStarted
            //
            lblNowStarted.AutoSize = true;
            lblNowStarted.BackColor = Color.Transparent;
            lblNowStarted.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNowStarted.ForeColor = Color.FromArgb(185, 203, 214);
            lblNowStarted.Margin = new Padding(0);
            lblNowStarted.Name = "lblNowStarted";
            lblNowStarted.TabIndex = 0;
            lblNowStarted.UseMnemonic = false;
            //
            // lblNowTimer
            //
            lblNowTimer.AutoSize = true;
            lblNowTimer.BackColor = Color.Transparent;
            lblNowTimer.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNowTimer.ForeColor = Color.White;
            lblNowTimer.Margin = new Padding(0);
            lblNowTimer.Name = "lblNowTimer";
            lblNowTimer.TabIndex = 1;
            //
            // lblNowTimerSuffix
            //
            lblNowTimerSuffix.AutoSize = true;
            lblNowTimerSuffix.BackColor = Color.Transparent;
            lblNowTimerSuffix.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNowTimerSuffix.ForeColor = Color.FromArgb(185, 203, 214);
            lblNowTimerSuffix.Margin = new Padding(0);
            lblNowTimerSuffix.Name = "lblNowTimerSuffix";
            lblNowTimerSuffix.TabIndex = 2;
            //
            // tlpActions
            //
            tlpActions.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tlpActions.BackColor = Color.FromArgb(27, 43, 63);
            tlpActions.ColumnCount = 1;
            tlpActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpActions.Controls.Add(btnComplete, 0, 0);
            tlpActions.Controls.Add(btnSkip, 0, 1);
            tlpActions.Controls.Add(btnCallNext, 0, 2);
            tlpActions.Margin = new Padding(0);
            tlpActions.Name = "tlpActions";
            tlpActions.RowCount = 3;
            tlpActions.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlpActions.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tlpActions.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpActions.Size = new Size(200, 150);
            tlpActions.TabIndex = 2;
            //
            // btnComplete
            //
            btnComplete.Dock = DockStyle.Fill;
            btnComplete.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnComplete.Margin = new Padding(0, 0, 0, 10);
            btnComplete.Name = "btnComplete";
            btnComplete.Size = new Size(200, 46);
            btnComplete.TabIndex = 0;
            btnComplete.Text = "Mark Complete";
            //
            // btnSkip
            //
            btnSkip.Dock = DockStyle.Fill;
            btnSkip.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSkip.Margin = new Padding(0, 0, 0, 10);
            btnSkip.Name = "btnSkip";
            btnSkip.Size = new Size(200, 42);
            btnSkip.TabIndex = 1;
            btnSkip.Text = "Skip Patient";
            //
            // btnCallNext
            //
            btnCallNext.Dock = DockStyle.Fill;
            btnCallNext.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCallNext.Margin = new Padding(0);
            btnCallNext.Name = "btnCallNext";
            btnCallNext.Size = new Size(200, 42);
            btnCallNext.TabIndex = 2;
            btnCallNext.Text = "Call Next Patient";
            //
            // pnlWaitingCard
            //
            pnlWaitingCard.BackColor = Color.White;
            pnlWaitingCard.Controls.Add(dgvWaiting);
            pnlWaitingCard.Controls.Add(pnlWaitingHeader);
            pnlWaitingCard.Dock = DockStyle.Fill;
            pnlWaitingCard.Margin = new Padding(0);
            pnlWaitingCard.Name = "pnlWaitingCard";
            pnlWaitingCard.Padding = new Padding(1, 6, 1, 10);
            pnlWaitingCard.Size = new Size(720, 508);
            pnlWaitingCard.TabIndex = 1;
            //
            // dgvWaiting
            //
            dgvWaiting.BackgroundColor = Color.White;
            dgvWaiting.BorderStyle = BorderStyle.None;
            dgvWaiting.Dock = DockStyle.Fill;
            dgvWaiting.Name = "dgvWaiting";
            dgvWaiting.RowHeadersVisible = false;
            dgvWaiting.TabIndex = 1;
            //
            // pnlWaitingHeader
            //
            pnlWaitingHeader.BackColor = Color.White;
            pnlWaitingHeader.Controls.Add(flpWaitingTitle);
            pnlWaitingHeader.Controls.Add(lblStatus);
            pnlWaitingHeader.Dock = DockStyle.Top;
            pnlWaitingHeader.Name = "pnlWaitingHeader";
            pnlWaitingHeader.Padding = new Padding(19, 0, 20, 0);
            pnlWaitingHeader.Size = new Size(718, 45);
            pnlWaitingHeader.TabIndex = 0;
            //
            // flpWaitingTitle
            //
            flpWaitingTitle.BackColor = Color.Transparent;
            flpWaitingTitle.Controls.Add(lblWaitingTitle);
            flpWaitingTitle.Controls.Add(lblWaitingCount);
            flpWaitingTitle.Dock = DockStyle.Fill;
            flpWaitingTitle.Name = "flpWaitingTitle";
            flpWaitingTitle.Padding = new Padding(0, 10, 0, 0);
            flpWaitingTitle.TabIndex = 0;
            flpWaitingTitle.WrapContents = false;
            //
            // lblWaitingTitle
            //
            lblWaitingTitle.AutoSize = true;
            lblWaitingTitle.BackColor = Color.Transparent;
            lblWaitingTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWaitingTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblWaitingTitle.Margin = new Padding(0);
            lblWaitingTitle.Name = "lblWaitingTitle";
            lblWaitingTitle.TabIndex = 0;
            lblWaitingTitle.Text = "Waiting list";
            //
            // lblWaitingCount
            //
            lblWaitingCount.AutoSize = true;
            lblWaitingCount.BackColor = Color.Transparent;
            lblWaitingCount.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWaitingCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblWaitingCount.Margin = new Padding(6, 4, 0, 0);
            lblWaitingCount.Name = "lblWaitingCount";
            lblWaitingCount.TabIndex = 1;
            //
            // lblStatus
            //
            lblStatus.AutoEllipsis = true;
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Dock = DockStyle.Right;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(91, 107, 124);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(380, 45);
            lblStatus.TabIndex = 1;
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            lblStatus.UseMnemonic = false;
            //
            // tlpRight
            //
            tlpRight.BackColor = Color.Transparent;
            tlpRight.ColumnCount = 1;
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRight.Controls.Add(tlpStats, 0, 0);
            tlpRight.Controls.Add(pnlCheckInCard, 0, 1);
            tlpRight.Dock = DockStyle.Fill;
            tlpRight.Margin = new Padding(0);
            tlpRight.Name = "tlpRight";
            tlpRight.RowCount = 2;
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 204F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRight.Size = new Size(380, 726);
            tlpRight.TabIndex = 1;
            //
            // tlpStats
            //
            tlpStats.BackColor = Color.Transparent;
            tlpStats.ColumnCount = 2;
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpStats.Controls.Add(pnlStatCompleted, 0, 0);
            tlpStats.Controls.Add(pnlStatWaiting, 1, 0);
            tlpStats.Controls.Add(pnlStatEmergency, 0, 1);
            tlpStats.Controls.Add(pnlStatAverage, 1, 1);
            tlpStats.Dock = DockStyle.Fill;
            tlpStats.Margin = new Padding(0, 0, 0, 20);
            tlpStats.Name = "tlpStats";
            tlpStats.RowCount = 2;
            tlpStats.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpStats.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpStats.Size = new Size(380, 184);
            tlpStats.TabIndex = 0;
            //
            // pnlStatCompleted
            //
            pnlStatCompleted.BackColor = Color.White;
            pnlStatCompleted.Controls.Add(lblStatCompleted);
            pnlStatCompleted.Controls.Add(lblStatCompletedCaption);
            pnlStatCompleted.Dock = DockStyle.Fill;
            pnlStatCompleted.Margin = new Padding(0, 0, 6, 6);
            pnlStatCompleted.Name = "pnlStatCompleted";
            pnlStatCompleted.Padding = new Padding(16, 14, 16, 10);
            pnlStatCompleted.Size = new Size(184, 86);
            pnlStatCompleted.TabIndex = 0;
            //
            // lblStatCompletedCaption
            //
            lblStatCompletedCaption.BackColor = Color.Transparent;
            lblStatCompletedCaption.Dock = DockStyle.Top;
            lblStatCompletedCaption.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatCompletedCaption.ForeColor = Color.FromArgb(91, 107, 124);
            lblStatCompletedCaption.Name = "lblStatCompletedCaption";
            lblStatCompletedCaption.Size = new Size(152, 20);
            lblStatCompletedCaption.TabIndex = 0;
            lblStatCompletedCaption.Text = "Completed today";
            //
            // lblStatCompleted
            //
            lblStatCompleted.BackColor = Color.Transparent;
            lblStatCompleted.Dock = DockStyle.Fill;
            lblStatCompleted.Font = new Font("Segoe UI", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatCompleted.ForeColor = Color.FromArgb(20, 34, 51);
            lblStatCompleted.Name = "lblStatCompleted";
            lblStatCompleted.TabIndex = 1;
            lblStatCompleted.Text = "0";
            lblStatCompleted.TextAlign = ContentAlignment.MiddleLeft;
            //
            // pnlStatWaiting
            //
            pnlStatWaiting.BackColor = Color.White;
            pnlStatWaiting.Controls.Add(lblStatWaiting);
            pnlStatWaiting.Controls.Add(lblStatWaitingCaption);
            pnlStatWaiting.Dock = DockStyle.Fill;
            pnlStatWaiting.Margin = new Padding(6, 0, 0, 6);
            pnlStatWaiting.Name = "pnlStatWaiting";
            pnlStatWaiting.Padding = new Padding(16, 14, 16, 10);
            pnlStatWaiting.Size = new Size(184, 86);
            pnlStatWaiting.TabIndex = 1;
            //
            // lblStatWaitingCaption
            //
            lblStatWaitingCaption.BackColor = Color.Transparent;
            lblStatWaitingCaption.Dock = DockStyle.Top;
            lblStatWaitingCaption.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatWaitingCaption.ForeColor = Color.FromArgb(91, 107, 124);
            lblStatWaitingCaption.Name = "lblStatWaitingCaption";
            lblStatWaitingCaption.Size = new Size(152, 20);
            lblStatWaitingCaption.TabIndex = 0;
            lblStatWaitingCaption.Text = "In the queue";
            //
            // lblStatWaiting
            //
            lblStatWaiting.BackColor = Color.Transparent;
            lblStatWaiting.Dock = DockStyle.Fill;
            lblStatWaiting.Font = new Font("Segoe UI", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatWaiting.ForeColor = Color.FromArgb(20, 34, 51);
            lblStatWaiting.Name = "lblStatWaiting";
            lblStatWaiting.TabIndex = 1;
            lblStatWaiting.Text = "0";
            lblStatWaiting.TextAlign = ContentAlignment.MiddleLeft;
            //
            // pnlStatEmergency
            //
            pnlStatEmergency.BackColor = Color.White;
            pnlStatEmergency.Controls.Add(lblStatEmergency);
            pnlStatEmergency.Controls.Add(lblStatEmergencyCaption);
            pnlStatEmergency.Dock = DockStyle.Fill;
            pnlStatEmergency.Margin = new Padding(0, 6, 6, 0);
            pnlStatEmergency.Name = "pnlStatEmergency";
            pnlStatEmergency.Padding = new Padding(16, 14, 16, 10);
            pnlStatEmergency.Size = new Size(184, 86);
            pnlStatEmergency.TabIndex = 2;
            //
            // lblStatEmergencyCaption
            //
            lblStatEmergencyCaption.BackColor = Color.Transparent;
            lblStatEmergencyCaption.Dock = DockStyle.Top;
            lblStatEmergencyCaption.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatEmergencyCaption.ForeColor = Color.FromArgb(91, 107, 124);
            lblStatEmergencyCaption.Name = "lblStatEmergencyCaption";
            lblStatEmergencyCaption.Size = new Size(152, 20);
            lblStatEmergencyCaption.TabIndex = 0;
            lblStatEmergencyCaption.Text = "Emergencies";
            //
            // lblStatEmergency
            //
            lblStatEmergency.BackColor = Color.Transparent;
            lblStatEmergency.Dock = DockStyle.Fill;
            lblStatEmergency.Font = new Font("Segoe UI", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatEmergency.ForeColor = Color.FromArgb(20, 34, 51);
            lblStatEmergency.Name = "lblStatEmergency";
            lblStatEmergency.TabIndex = 1;
            lblStatEmergency.Text = "0";
            lblStatEmergency.TextAlign = ContentAlignment.MiddleLeft;
            //
            // pnlStatAverage
            //
            pnlStatAverage.BackColor = Color.White;
            pnlStatAverage.Controls.Add(lblStatAverage);
            pnlStatAverage.Controls.Add(lblStatAverageCaption);
            pnlStatAverage.Dock = DockStyle.Fill;
            pnlStatAverage.Margin = new Padding(6, 6, 0, 0);
            pnlStatAverage.Name = "pnlStatAverage";
            pnlStatAverage.Padding = new Padding(16, 14, 16, 10);
            pnlStatAverage.Size = new Size(184, 86);
            pnlStatAverage.TabIndex = 3;
            //
            // lblStatAverageCaption
            //
            lblStatAverageCaption.BackColor = Color.Transparent;
            lblStatAverageCaption.Dock = DockStyle.Top;
            lblStatAverageCaption.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatAverageCaption.ForeColor = Color.FromArgb(91, 107, 124);
            lblStatAverageCaption.Name = "lblStatAverageCaption";
            lblStatAverageCaption.Size = new Size(152, 20);
            lblStatAverageCaption.TabIndex = 0;
            lblStatAverageCaption.Text = "Avg. consultation";
            //
            // lblStatAverage
            //
            lblStatAverage.AutoEllipsis = true;
            lblStatAverage.BackColor = Color.Transparent;
            lblStatAverage.Dock = DockStyle.Fill;
            lblStatAverage.Font = new Font("Segoe UI", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatAverage.ForeColor = Color.FromArgb(20, 34, 51);
            lblStatAverage.Name = "lblStatAverage";
            lblStatAverage.TabIndex = 1;
            lblStatAverage.Text = "0";
            lblStatAverage.TextAlign = ContentAlignment.MiddleLeft;
            //
            // pnlCheckInCard
            //
            pnlCheckInCard.BackColor = Color.White;
            pnlCheckInCard.Controls.Add(tlpCheckIn);
            pnlCheckInCard.Dock = DockStyle.Fill;
            pnlCheckInCard.Margin = new Padding(0);
            pnlCheckInCard.Name = "pnlCheckInCard";
            pnlCheckInCard.Padding = new Padding(18, 16, 18, 16);
            pnlCheckInCard.Size = new Size(380, 502);
            pnlCheckInCard.TabIndex = 1;
            //
            // tlpCheckIn
            //
            tlpCheckIn.BackColor = Color.White;
            tlpCheckIn.ColumnCount = 1;
            tlpCheckIn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCheckIn.Controls.Add(lblCheckInTitle, 0, 0);
            tlpCheckIn.Controls.Add(lblCheckInHint, 0, 1);
            tlpCheckIn.Controls.Add(dgvCheckIn, 0, 2);
            tlpCheckIn.Controls.Add(chkEmergency, 0, 3);
            tlpCheckIn.Controls.Add(btnCheckIn, 0, 4);
            tlpCheckIn.Controls.Add(btnWalkIn, 0, 5);
            tlpCheckIn.Dock = DockStyle.Fill;
            tlpCheckIn.Margin = new Padding(0);
            tlpCheckIn.Name = "tlpCheckIn";
            tlpCheckIn.RowCount = 6;
            tlpCheckIn.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCheckIn.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCheckIn.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCheckIn.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCheckIn.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpCheckIn.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpCheckIn.Size = new Size(344, 470);
            tlpCheckIn.TabIndex = 0;
            //
            // lblCheckInTitle
            //
            lblCheckInTitle.AutoSize = true;
            lblCheckInTitle.BackColor = Color.Transparent;
            lblCheckInTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCheckInTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblCheckInTitle.Margin = new Padding(0, 0, 0, 10);
            lblCheckInTitle.Name = "lblCheckInTitle";
            lblCheckInTitle.TabIndex = 10;
            lblCheckInTitle.Text = "Check-in";
            //
            // lblCheckInHint
            //
            lblCheckInHint.AutoSize = true;
            lblCheckInHint.BackColor = Color.Transparent;
            lblCheckInHint.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCheckInHint.ForeColor = Color.FromArgb(91, 107, 124);
            lblCheckInHint.Margin = new Padding(0, 0, 0, 12);
            lblCheckInHint.MaximumSize = new Size(344, 0);
            lblCheckInHint.Name = "lblCheckInHint";
            lblCheckInHint.TabIndex = 11;
            lblCheckInHint.Text = "Today's booked appointments not yet arrived.";
            //
            // dgvCheckIn
            //
            dgvCheckIn.BackgroundColor = Color.White;
            dgvCheckIn.BorderStyle = BorderStyle.None;
            dgvCheckIn.ColumnHeadersVisible = false;
            dgvCheckIn.Dock = DockStyle.Fill;
            dgvCheckIn.Margin = new Padding(0, 0, 0, 12);
            dgvCheckIn.Name = "dgvCheckIn";
            dgvCheckIn.RowHeadersVisible = false;
            dgvCheckIn.TabIndex = 0;
            //
            // chkEmergency
            //
            chkEmergency.AutoSize = true;
            chkEmergency.BackColor = Color.Transparent;
            chkEmergency.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkEmergency.ForeColor = Color.FromArgb(20, 34, 51);
            chkEmergency.Margin = new Padding(0, 0, 0, 12);
            chkEmergency.Name = "chkEmergency";
            chkEmergency.TabIndex = 1;
            chkEmergency.Text = "Emergency";
            chkEmergency.UseVisualStyleBackColor = false;
            //
            // btnCheckIn
            //
            btnCheckIn.Dock = DockStyle.Fill;
            btnCheckIn.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCheckIn.Margin = new Padding(0, 0, 0, 12);
            btnCheckIn.Name = "btnCheckIn";
            btnCheckIn.Size = new Size(344, 42);
            btnCheckIn.TabIndex = 2;
            btnCheckIn.Text = "Check In Selected";
            //
            // btnWalkIn
            //
            btnWalkIn.Dock = DockStyle.Fill;
            btnWalkIn.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnWalkIn.Margin = new Padding(0);
            btnWalkIn.Name = "btnWalkIn";
            btnWalkIn.Size = new Size(344, 42);
            btnWalkIn.TabIndex = 3;
            btnWalkIn.Text = "Add Walk-in / Emergency...";
            //
            // QueueBoardForm
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Controls.Add(tlpRoot);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            FormBorderStyle = FormBorderStyle.None;
            Name = "QueueBoardForm";
            Padding = new Padding(28, 24, 28, 24);
            Text = "Patient Queue Board";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            flpToolbar.ResumeLayout(false);
            tlpBody.ResumeLayout(false);
            tlpLeft.ResumeLayout(false);
            pnlNow.ResumeLayout(false);
            tlpNow.ResumeLayout(false);
            pnlNowToken.ResumeLayout(false);
            pnlNowInfo.ResumeLayout(false);
            flpNowElapsed.ResumeLayout(false);
            flpNowElapsed.PerformLayout();
            tlpActions.ResumeLayout(false);
            pnlWaitingCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvWaiting).EndInit();
            pnlWaitingHeader.ResumeLayout(false);
            flpWaitingTitle.ResumeLayout(false);
            flpWaitingTitle.PerformLayout();
            tlpRight.ResumeLayout(false);
            tlpStats.ResumeLayout(false);
            pnlStatCompleted.ResumeLayout(false);
            pnlStatWaiting.ResumeLayout(false);
            pnlStatEmergency.ResumeLayout(false);
            pnlStatAverage.ResumeLayout(false);
            pnlCheckInCard.ResumeLayout(false);
            tlpCheckIn.ResumeLayout(false);
            tlpCheckIn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCheckIn).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.FlowLayoutPanel flpToolbar;
        private System.Windows.Forms.ComboBox cboDoctor;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private System.Windows.Forms.TableLayoutPanel tlpLeft;
        private System.Windows.Forms.Panel pnlNow;
        private System.Windows.Forms.TableLayoutPanel tlpNow;
        private System.Windows.Forms.Panel pnlNowToken;
        private System.Windows.Forms.Label lblNowCaption;
        private System.Windows.Forms.Label lblNowToken;
        private System.Windows.Forms.Panel pnlNowInfo;
        private System.Windows.Forms.Label lblNowPatient;
        private System.Windows.Forms.Label lblNowDetail;
        private System.Windows.Forms.FlowLayoutPanel flpNowElapsed;
        private System.Windows.Forms.Label lblNowStarted;
        private System.Windows.Forms.Label lblNowTimer;
        private System.Windows.Forms.Label lblNowTimerSuffix;
        private System.Windows.Forms.TableLayoutPanel tlpActions;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnCallNext;
        private System.Windows.Forms.Panel pnlWaitingCard;
        private System.Windows.Forms.Panel pnlWaitingHeader;
        private System.Windows.Forms.FlowLayoutPanel flpWaitingTitle;
        private System.Windows.Forms.Label lblWaitingTitle;
        private System.Windows.Forms.Label lblWaitingCount;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dgvWaiting;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.TableLayoutPanel tlpStats;
        private System.Windows.Forms.Panel pnlStatCompleted;
        private System.Windows.Forms.Label lblStatCompletedCaption;
        private System.Windows.Forms.Label lblStatCompleted;
        private System.Windows.Forms.Panel pnlStatWaiting;
        private System.Windows.Forms.Label lblStatWaitingCaption;
        private System.Windows.Forms.Label lblStatWaiting;
        private System.Windows.Forms.Panel pnlStatEmergency;
        private System.Windows.Forms.Label lblStatEmergencyCaption;
        private System.Windows.Forms.Label lblStatEmergency;
        private System.Windows.Forms.Panel pnlStatAverage;
        private System.Windows.Forms.Label lblStatAverageCaption;
        private System.Windows.Forms.Label lblStatAverage;
        private System.Windows.Forms.Panel pnlCheckInCard;
        private System.Windows.Forms.TableLayoutPanel tlpCheckIn;
        private System.Windows.Forms.Label lblCheckInTitle;
        private System.Windows.Forms.Label lblCheckInHint;
        private System.Windows.Forms.DataGridView dgvCheckIn;
        private System.Windows.Forms.CheckBox chkEmergency;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.Button btnWalkIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWPos = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWToken = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWPatient = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWCode = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWType = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWCheckIn = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWSkips = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWWait = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCTime = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCToken = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCPatient = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCCode = null!;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCAppointment = null!;
    }
}
