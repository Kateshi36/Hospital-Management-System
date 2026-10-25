namespace HospitalManagementSystem.Views.Main
{
    partial class DashboardForm
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

        // Layout = redesign mockup, artboard "2 · Dashboard", content area (px at 96 DPI):
        //   28 px top/bottom and 32 px side padding; greeting + Refresh; four 20 px-spaced KPI cards (the last one navy);
        //   then "Today's appointments" (1.6 parts) beside "Queue right now" and "Stock alerts" (1 part).
        // Grid columns, queue tiles and stock rows are created in DashboardForm.cs from the data.

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpPage = new TableLayoutPanel();
            pnlHeader = new Panel();
            lblGreeting = new Label();
            lblDateLine = new Label();
            btnRefresh = new Button();
            tlpCards = new TableLayoutPanel();
            pnlCardPatients = new Panel();
            lblPatientsCaption = new Label();
            lblPatientsValue = new Label();
            lblPatientsSub = new Label();
            pnlCardAppointments = new Panel();
            lblAppointmentsCaption = new Label();
            lblAppointmentsValue = new Label();
            lblAppointmentsSub = new Label();
            pnlCardQueue = new Panel();
            lblQueueCaption = new Label();
            lblQueueValue = new Label();
            lblQueueSub = new Label();
            pnlCardRevenue = new Panel();
            lblRevenueCaption = new Label();
            lblRevenueValue = new Label();
            lblRevenueSub = new Label();
            tlpBottom = new TableLayoutPanel();
            pnlAppointmentsCard = new Panel();
            dgvAppointments = new DataGridView();
            pnlAppointmentsHeader = new Panel();
            lblAppointmentsTitle = new Label();
            lnkOpenBooking = new LinkLabel();
            tlpRightColumn = new TableLayoutPanel();
            pnlQueueCard = new Panel();
            lblQueueTitle = new Label();
            pnlStockCard = new Panel();
            lblStockTitle = new Label();
            tmrRefresh = new System.Windows.Forms.Timer(components);
            tlpPage.SuspendLayout();
            pnlHeader.SuspendLayout();
            tlpCards.SuspendLayout();
            pnlCardPatients.SuspendLayout();
            pnlCardAppointments.SuspendLayout();
            pnlCardQueue.SuspendLayout();
            pnlCardRevenue.SuspendLayout();
            tlpBottom.SuspendLayout();
            pnlAppointmentsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).BeginInit();
            pnlAppointmentsHeader.SuspendLayout();
            tlpRightColumn.SuspendLayout();
            pnlQueueCard.SuspendLayout();
            pnlStockCard.SuspendLayout();
            SuspendLayout();
            //
            // tlpPage
            //
            tlpPage.BackColor = Color.FromArgb(243, 246, 249);
            tlpPage.ColumnCount = 1;
            tlpPage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPage.Controls.Add(pnlHeader, 0, 0);
            tlpPage.Controls.Add(tlpCards, 0, 1);
            tlpPage.Controls.Add(tlpBottom, 0, 2);
            tlpPage.Dock = DockStyle.Fill;
            tlpPage.Location = new Point(32, 28);
            tlpPage.Margin = new Padding(0);
            tlpPage.Name = "tlpPage";
            tlpPage.RowCount = 3;
            tlpPage.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tlpPage.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            tlpPage.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPage.Size = new Size(1112, 792);
            tlpPage.TabIndex = 0;
            //
            // pnlHeader
            //
            pnlHeader.BackColor = Color.Transparent;
            pnlHeader.Controls.Add(btnRefresh);
            pnlHeader.Controls.Add(lblDateLine);
            pnlHeader.Controls.Add(lblGreeting);
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Margin = new Padding(0, 0, 0, 24);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1112, 56);
            pnlHeader.TabIndex = 0;
            //
            // lblGreeting
            //
            lblGreeting.AutoSize = true;
            lblGreeting.BackColor = Color.Transparent;
            lblGreeting.Font = new Font("Segoe UI", 19.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblGreeting.ForeColor = Color.FromArgb(20, 34, 51);
            lblGreeting.Location = new Point(-4, -2);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.TabIndex = 0;
            lblGreeting.Text = "Good evening, Administrator";
            lblGreeting.UseMnemonic = false;
            //
            // lblDateLine
            //
            lblDateLine.AutoSize = true;
            lblDateLine.BackColor = Color.Transparent;
            lblDateLine.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDateLine.ForeColor = Color.FromArgb(91, 107, 124);
            lblDateLine.Location = new Point(-2, 34);
            lblDateLine.Name = "lblDateLine";
            lblDateLine.TabIndex = 1;
            lblDateLine.Text = "Monday, October 26, 2026  ·  Here is today at a glance.";
            //
            // btnRefresh
            //
            btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRefresh.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRefresh.Location = new Point(1028, 16);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(84, 40);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            //
            // tlpCards
            //
            tlpCards.BackColor = Color.Transparent;
            tlpCards.ColumnCount = 4;
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.Controls.Add(pnlCardPatients, 0, 0);
            tlpCards.Controls.Add(pnlCardAppointments, 1, 0);
            tlpCards.Controls.Add(pnlCardQueue, 2, 0);
            tlpCards.Controls.Add(pnlCardRevenue, 3, 0);
            tlpCards.Dock = DockStyle.Fill;
            tlpCards.Margin = new Padding(0, 0, 0, 24);
            tlpCards.Name = "tlpCards";
            tlpCards.RowCount = 1;
            tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCards.Size = new Size(1112, 136);
            tlpCards.TabIndex = 1;
            //
            // pnlCardPatients
            //
            pnlCardPatients.BackColor = Color.White;
            pnlCardPatients.Controls.Add(lblPatientsSub);
            pnlCardPatients.Controls.Add(lblPatientsValue);
            pnlCardPatients.Controls.Add(lblPatientsCaption);
            pnlCardPatients.Dock = DockStyle.Fill;
            pnlCardPatients.Margin = new Padding(0, 0, 20, 0);
            pnlCardPatients.Name = "pnlCardPatients";
            pnlCardPatients.Size = new Size(258, 136);
            pnlCardPatients.TabIndex = 0;
            //
            // lblPatientsCaption
            //
            lblPatientsCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPatientsCaption.BackColor = Color.Transparent;
            lblPatientsCaption.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPatientsCaption.ForeColor = Color.FromArgb(91, 107, 124);
            lblPatientsCaption.Location = new Point(20, 18);
            lblPatientsCaption.Name = "lblPatientsCaption";
            lblPatientsCaption.Size = new Size(218, 21);
            lblPatientsCaption.TabIndex = 0;
            lblPatientsCaption.Text = "Patients registered";
            //
            // lblPatientsValue
            //
            lblPatientsValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPatientsValue.BackColor = Color.Transparent;
            lblPatientsValue.Font = new Font("Segoe UI", 25.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPatientsValue.ForeColor = Color.FromArgb(20, 34, 51);
            lblPatientsValue.Location = new Point(16, 42);
            lblPatientsValue.Name = "lblPatientsValue";
            lblPatientsValue.Size = new Size(222, 50);
            lblPatientsValue.TabIndex = 1;
            lblPatientsValue.Text = "0";
            //
            // lblPatientsSub
            //
            lblPatientsSub.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPatientsSub.BackColor = Color.Transparent;
            lblPatientsSub.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPatientsSub.ForeColor = Color.FromArgb(18, 122, 140);
            lblPatientsSub.Location = new Point(20, 98);
            lblPatientsSub.Name = "lblPatientsSub";
            lblPatientsSub.Size = new Size(218, 20);
            lblPatientsSub.TabIndex = 2;
            lblPatientsSub.Text = "0 new today";
            //
            // pnlCardAppointments
            //
            pnlCardAppointments.BackColor = Color.White;
            pnlCardAppointments.Controls.Add(lblAppointmentsSub);
            pnlCardAppointments.Controls.Add(lblAppointmentsValue);
            pnlCardAppointments.Controls.Add(lblAppointmentsCaption);
            pnlCardAppointments.Dock = DockStyle.Fill;
            pnlCardAppointments.Margin = new Padding(0, 0, 20, 0);
            pnlCardAppointments.Name = "pnlCardAppointments";
            pnlCardAppointments.Size = new Size(258, 136);
            pnlCardAppointments.TabIndex = 1;
            //
            // lblAppointmentsCaption
            //
            lblAppointmentsCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblAppointmentsCaption.BackColor = Color.Transparent;
            lblAppointmentsCaption.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAppointmentsCaption.ForeColor = Color.FromArgb(91, 107, 124);
            lblAppointmentsCaption.Location = new Point(20, 18);
            lblAppointmentsCaption.Name = "lblAppointmentsCaption";
            lblAppointmentsCaption.Size = new Size(218, 21);
            lblAppointmentsCaption.TabIndex = 0;
            lblAppointmentsCaption.Text = "Appointments today";
            //
            // lblAppointmentsValue
            //
            lblAppointmentsValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblAppointmentsValue.BackColor = Color.Transparent;
            lblAppointmentsValue.Font = new Font("Segoe UI", 25.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAppointmentsValue.ForeColor = Color.FromArgb(20, 34, 51);
            lblAppointmentsValue.Location = new Point(16, 42);
            lblAppointmentsValue.Name = "lblAppointmentsValue";
            lblAppointmentsValue.Size = new Size(222, 50);
            lblAppointmentsValue.TabIndex = 1;
            lblAppointmentsValue.Text = "0";
            //
            // lblAppointmentsSub
            //
            lblAppointmentsSub.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblAppointmentsSub.AutoEllipsis = true;
            lblAppointmentsSub.BackColor = Color.Transparent;
            lblAppointmentsSub.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAppointmentsSub.ForeColor = Color.FromArgb(91, 107, 124);
            lblAppointmentsSub.Location = new Point(20, 98);
            lblAppointmentsSub.Name = "lblAppointmentsSub";
            lblAppointmentsSub.Size = new Size(218, 20);
            lblAppointmentsSub.TabIndex = 2;
            lblAppointmentsSub.Text = "0 completed · 0 booked · 0 cancelled";
            //
            // pnlCardQueue
            //
            pnlCardQueue.BackColor = Color.White;
            pnlCardQueue.Controls.Add(lblQueueSub);
            pnlCardQueue.Controls.Add(lblQueueValue);
            pnlCardQueue.Controls.Add(lblQueueCaption);
            pnlCardQueue.Dock = DockStyle.Fill;
            pnlCardQueue.Margin = new Padding(0, 0, 20, 0);
            pnlCardQueue.Name = "pnlCardQueue";
            pnlCardQueue.Size = new Size(258, 136);
            pnlCardQueue.TabIndex = 2;
            //
            // lblQueueCaption
            //
            lblQueueCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblQueueCaption.BackColor = Color.Transparent;
            lblQueueCaption.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblQueueCaption.ForeColor = Color.FromArgb(91, 107, 124);
            lblQueueCaption.Location = new Point(20, 18);
            lblQueueCaption.Name = "lblQueueCaption";
            lblQueueCaption.Size = new Size(218, 21);
            lblQueueCaption.TabIndex = 0;
            lblQueueCaption.Text = "In the queue";
            //
            // lblQueueValue
            //
            lblQueueValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblQueueValue.BackColor = Color.Transparent;
            lblQueueValue.Font = new Font("Segoe UI", 25.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQueueValue.ForeColor = Color.FromArgb(20, 34, 51);
            lblQueueValue.Location = new Point(16, 42);
            lblQueueValue.Name = "lblQueueValue";
            lblQueueValue.Size = new Size(222, 50);
            lblQueueValue.TabIndex = 1;
            lblQueueValue.Text = "0";
            //
            // lblQueueSub
            //
            lblQueueSub.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblQueueSub.BackColor = Color.Transparent;
            lblQueueSub.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblQueueSub.ForeColor = Color.FromArgb(180, 35, 24);
            lblQueueSub.Location = new Point(20, 98);
            lblQueueSub.Name = "lblQueueSub";
            lblQueueSub.Size = new Size(218, 20);
            lblQueueSub.TabIndex = 2;
            lblQueueSub.Text = "0 emergency waiting";
            //
            // pnlCardRevenue
            //
            pnlCardRevenue.BackColor = Color.FromArgb(27, 43, 63);
            pnlCardRevenue.Controls.Add(lblRevenueSub);
            pnlCardRevenue.Controls.Add(lblRevenueValue);
            pnlCardRevenue.Controls.Add(lblRevenueCaption);
            pnlCardRevenue.Dock = DockStyle.Fill;
            pnlCardRevenue.Margin = new Padding(0);
            pnlCardRevenue.Name = "pnlCardRevenue";
            pnlCardRevenue.Size = new Size(278, 136);
            pnlCardRevenue.TabIndex = 3;
            //
            // lblRevenueCaption
            //
            lblRevenueCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblRevenueCaption.BackColor = Color.Transparent;
            lblRevenueCaption.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRevenueCaption.ForeColor = Color.FromArgb(185, 203, 214);
            lblRevenueCaption.Location = new Point(20, 18);
            lblRevenueCaption.Name = "lblRevenueCaption";
            lblRevenueCaption.Size = new Size(238, 21);
            lblRevenueCaption.TabIndex = 0;
            lblRevenueCaption.Text = "Collected today";
            //
            // lblRevenueValue
            //
            lblRevenueValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblRevenueValue.AutoEllipsis = true;
            lblRevenueValue.BackColor = Color.Transparent;
            lblRevenueValue.Font = new Font("Segoe UI", 25.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRevenueValue.ForeColor = Color.White;
            lblRevenueValue.Location = new Point(16, 42);
            lblRevenueValue.Name = "lblRevenueValue";
            lblRevenueValue.Size = new Size(242, 50);
            lblRevenueValue.TabIndex = 1;
            lblRevenueValue.Text = "₱0.00";
            lblRevenueValue.UseMnemonic = false;
            //
            // lblRevenueSub
            //
            lblRevenueSub.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblRevenueSub.BackColor = Color.Transparent;
            lblRevenueSub.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRevenueSub.ForeColor = Color.FromArgb(127, 211, 224);
            lblRevenueSub.Location = new Point(20, 98);
            lblRevenueSub.Name = "lblRevenueSub";
            lblRevenueSub.Size = new Size(238, 20);
            lblRevenueSub.TabIndex = 2;
            lblRevenueSub.Text = "0 payments";
            //
            // tlpBottom
            //
            tlpBottom.BackColor = Color.Transparent;
            tlpBottom.ColumnCount = 2;
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.5F));
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.5F));
            tlpBottom.Controls.Add(pnlAppointmentsCard, 0, 0);
            tlpBottom.Controls.Add(tlpRightColumn, 1, 0);
            tlpBottom.Dock = DockStyle.Fill;
            tlpBottom.Margin = new Padding(0);
            tlpBottom.Name = "tlpBottom";
            tlpBottom.RowCount = 1;
            tlpBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBottom.Size = new Size(1112, 552);
            tlpBottom.TabIndex = 2;
            //
            // pnlAppointmentsCard
            //
            pnlAppointmentsCard.BackColor = Color.White;
            pnlAppointmentsCard.Controls.Add(dgvAppointments);
            pnlAppointmentsCard.Controls.Add(pnlAppointmentsHeader);
            pnlAppointmentsCard.Dock = DockStyle.Fill;
            pnlAppointmentsCard.Margin = new Padding(0, 0, 20, 0);
            pnlAppointmentsCard.Name = "pnlAppointmentsCard";
            pnlAppointmentsCard.Padding = new Padding(1, 1, 1, 12);
            pnlAppointmentsCard.Size = new Size(664, 552);
            pnlAppointmentsCard.TabIndex = 0;
            //
            // dgvAppointments
            //
            dgvAppointments.BackgroundColor = Color.White;
            dgvAppointments.BorderStyle = BorderStyle.None;
            dgvAppointments.Dock = DockStyle.Fill;
            dgvAppointments.Name = "dgvAppointments";
            dgvAppointments.RowHeadersVisible = false;
            dgvAppointments.TabIndex = 1;
            //
            // pnlAppointmentsHeader
            //
            pnlAppointmentsHeader.BackColor = Color.White;
            pnlAppointmentsHeader.Controls.Add(lnkOpenBooking);
            pnlAppointmentsHeader.Controls.Add(lblAppointmentsTitle);
            pnlAppointmentsHeader.Dock = DockStyle.Top;
            pnlAppointmentsHeader.Name = "pnlAppointmentsHeader";
            pnlAppointmentsHeader.Size = new Size(662, 56);
            pnlAppointmentsHeader.TabIndex = 0;
            //
            // lblAppointmentsTitle
            //
            lblAppointmentsTitle.AutoSize = true;
            lblAppointmentsTitle.BackColor = Color.Transparent;
            lblAppointmentsTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAppointmentsTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblAppointmentsTitle.Location = new Point(18, 15);
            lblAppointmentsTitle.Name = "lblAppointmentsTitle";
            lblAppointmentsTitle.TabIndex = 0;
            lblAppointmentsTitle.Text = "Today's appointments";
            //
            // lnkOpenBooking
            //
            lnkOpenBooking.ActiveLinkColor = Color.FromArgb(13, 95, 109);
            lnkOpenBooking.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lnkOpenBooking.BackColor = Color.Transparent;
            lnkOpenBooking.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkOpenBooking.LinkBehavior = LinkBehavior.HoverUnderline;
            lnkOpenBooking.LinkColor = Color.FromArgb(18, 122, 140);
            lnkOpenBooking.Location = new Point(522, 17);
            lnkOpenBooking.Name = "lnkOpenBooking";
            lnkOpenBooking.Size = new Size(120, 24);
            lnkOpenBooking.TabIndex = 1;
            lnkOpenBooking.TabStop = true;
            lnkOpenBooking.Text = "Open booking";
            lnkOpenBooking.TextAlign = ContentAlignment.MiddleRight;
            //
            // tlpRightColumn
            //
            tlpRightColumn.BackColor = Color.Transparent;
            tlpRightColumn.ColumnCount = 1;
            tlpRightColumn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRightColumn.Controls.Add(pnlQueueCard, 0, 0);
            tlpRightColumn.Controls.Add(pnlStockCard, 0, 1);
            tlpRightColumn.Dock = DockStyle.Fill;
            tlpRightColumn.Margin = new Padding(0);
            tlpRightColumn.Name = "tlpRightColumn";
            tlpRightColumn.RowCount = 2;
            tlpRightColumn.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRightColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRightColumn.Size = new Size(428, 552);
            tlpRightColumn.TabIndex = 1;
            //
            // pnlQueueCard
            //
            pnlQueueCard.BackColor = Color.White;
            pnlQueueCard.Controls.Add(lblQueueTitle);
            pnlQueueCard.Dock = DockStyle.Top;
            pnlQueueCard.Margin = new Padding(0, 0, 0, 20);
            pnlQueueCard.Name = "pnlQueueCard";
            pnlQueueCard.Size = new Size(428, 190);
            pnlQueueCard.TabIndex = 0;
            //
            // lblQueueTitle
            //
            lblQueueTitle.AutoSize = true;
            lblQueueTitle.BackColor = Color.Transparent;
            lblQueueTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQueueTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblQueueTitle.Location = new Point(18, 14);
            lblQueueTitle.Name = "lblQueueTitle";
            lblQueueTitle.TabIndex = 0;
            lblQueueTitle.Text = "Queue right now";
            //
            // pnlStockCard
            //
            pnlStockCard.BackColor = Color.White;
            pnlStockCard.Controls.Add(lblStockTitle);
            pnlStockCard.Dock = DockStyle.Fill;
            pnlStockCard.Margin = new Padding(0);
            pnlStockCard.Name = "pnlStockCard";
            pnlStockCard.Size = new Size(428, 342);
            pnlStockCard.TabIndex = 1;
            //
            // lblStockTitle
            //
            lblStockTitle.AutoSize = true;
            lblStockTitle.BackColor = Color.Transparent;
            lblStockTitle.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStockTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblStockTitle.Location = new Point(18, 14);
            lblStockTitle.Name = "lblStockTitle";
            lblStockTitle.TabIndex = 0;
            lblStockTitle.Text = "Stock alerts";
            //
            // tmrRefresh
            //
            tmrRefresh.Enabled = true;
            tmrRefresh.Interval = 60000;
            //
            // DashboardForm
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Controls.Add(tlpPage);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DashboardForm";
            Padding = new Padding(32, 28, 32, 28);
            Text = "Dashboard";
            tlpPage.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tlpCards.ResumeLayout(false);
            pnlCardPatients.ResumeLayout(false);
            pnlCardAppointments.ResumeLayout(false);
            pnlCardQueue.ResumeLayout(false);
            pnlCardRevenue.ResumeLayout(false);
            tlpBottom.ResumeLayout(false);
            pnlAppointmentsCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAppointments).EndInit();
            pnlAppointmentsHeader.ResumeLayout(false);
            pnlAppointmentsHeader.PerformLayout();
            tlpRightColumn.ResumeLayout(false);
            pnlQueueCard.ResumeLayout(false);
            pnlQueueCard.PerformLayout();
            pnlStockCard.ResumeLayout(false);
            pnlStockCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPage;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblGreeting;
        private System.Windows.Forms.Label lblDateLine;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tlpCards;
        private System.Windows.Forms.Panel pnlCardPatients;
        private System.Windows.Forms.Label lblPatientsCaption;
        private System.Windows.Forms.Label lblPatientsValue;
        private System.Windows.Forms.Label lblPatientsSub;
        private System.Windows.Forms.Panel pnlCardAppointments;
        private System.Windows.Forms.Label lblAppointmentsCaption;
        private System.Windows.Forms.Label lblAppointmentsValue;
        private System.Windows.Forms.Label lblAppointmentsSub;
        private System.Windows.Forms.Panel pnlCardQueue;
        private System.Windows.Forms.Label lblQueueCaption;
        private System.Windows.Forms.Label lblQueueValue;
        private System.Windows.Forms.Label lblQueueSub;
        private System.Windows.Forms.Panel pnlCardRevenue;
        private System.Windows.Forms.Label lblRevenueCaption;
        private System.Windows.Forms.Label lblRevenueValue;
        private System.Windows.Forms.Label lblRevenueSub;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        private System.Windows.Forms.Panel pnlAppointmentsCard;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.Panel pnlAppointmentsHeader;
        private System.Windows.Forms.Label lblAppointmentsTitle;
        private System.Windows.Forms.LinkLabel lnkOpenBooking;
        private System.Windows.Forms.TableLayoutPanel tlpRightColumn;
        private System.Windows.Forms.Panel pnlQueueCard;
        private System.Windows.Forms.Label lblQueueTitle;
        private System.Windows.Forms.Panel pnlStockCard;
        private System.Windows.Forms.Label lblStockTitle;
        private System.Windows.Forms.Timer tmrRefresh;
    }
}
