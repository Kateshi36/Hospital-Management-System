namespace HospitalManagementSystem.Views.Admin
{
    partial class AuditTrailForm
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

        // Audit Trail: page title + date line, a filter card (search, action, record type, date range), the activity
        // table card and the selected-entry card underneath.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            pnlTitle = new Panel();
            lblDate = new Label();
            lblTitle = new Label();
            pnlFilterCard = new Panel();
            tlpFilters = new TableLayoutPanel();
            lblSearchCap = new Label();
            lblActionCap = new Label();
            lblTypeCap = new Label();
            lblFromCap = new Label();
            lblToCap = new Label();
            txtSearch = new TextBox();
            cboAction = new ComboBox();
            cboType = new ComboBox();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            btnSearch = new Button();
            btnShowAll = new Button();
            pnlResultsCard = new Panel();
            dgvAudit = new DataGridView();
            pnlResultsFooter = new Panel();
            lblCount = new Label();
            lblLegend = new Label();
            pnlResultsHeader = new Panel();
            lblResultsTitle = new Label();
            pnlDetailCard = new Panel();
            pnlDetailBody = new Panel();
            txtDetail = new TextBox();
            pnlDetailHeader = new Panel();
            lblDetailTitle = new Label();
            tlpRoot.SuspendLayout();
            pnlTitle.SuspendLayout();
            pnlFilterCard.SuspendLayout();
            tlpFilters.SuspendLayout();
            pnlResultsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAudit).BeginInit();
            pnlResultsFooter.SuspendLayout();
            pnlResultsHeader.SuspendLayout();
            pnlDetailCard.SuspendLayout();
            pnlDetailBody.SuspendLayout();
            pnlDetailHeader.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(pnlTitle, 0, 0);
            tlpRoot.Controls.Add(pnlFilterCard, 0, 1);
            tlpRoot.Controls.Add(pnlResultsCard, 0, 2);
            tlpRoot.Controls.Add(pnlDetailCard, 0, 3);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 4;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
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
            lblDate.Text = "Who created, changed, voided, refunded or opened which record - for the Data Privacy Act (RA 10173).";
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
            lblTitle.Text = "Audit Trail";
            lblTitle.UseMnemonic = false;
            // 
            // pnlFilterCard
            // 
            pnlFilterCard.AutoSize = true;
            pnlFilterCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlFilterCard.BackColor = Color.White;
            pnlFilterCard.Controls.Add(tlpFilters);
            pnlFilterCard.Dock = DockStyle.Fill;
            pnlFilterCard.Margin = new Padding(0, 0, 0, 20);
            pnlFilterCard.Name = "pnlFilterCard";
            pnlFilterCard.Padding = new Padding(20, 16, 20, 12);
            pnlFilterCard.TabIndex = 1;
            // 
            // tlpFilters
            // 
            tlpFilters.AutoSize = true;
            tlpFilters.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpFilters.BackColor = Color.Transparent;
            tlpFilters.ColumnCount = 7;
            tlpFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tlpFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            tlpFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpFilters.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilters.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpFilters.Controls.Add(lblSearchCap, 0, 0);
            tlpFilters.Controls.Add(lblActionCap, 1, 0);
            tlpFilters.Controls.Add(lblTypeCap, 2, 0);
            tlpFilters.Controls.Add(lblFromCap, 3, 0);
            tlpFilters.Controls.Add(lblToCap, 4, 0);
            tlpFilters.Controls.Add(txtSearch, 0, 1);
            tlpFilters.Controls.Add(cboAction, 1, 1);
            tlpFilters.Controls.Add(cboType, 2, 1);
            tlpFilters.Controls.Add(dtpFrom, 3, 1);
            tlpFilters.Controls.Add(dtpTo, 4, 1);
            tlpFilters.Controls.Add(btnSearch, 5, 1);
            tlpFilters.Controls.Add(btnShowAll, 6, 1);
            tlpFilters.Dock = DockStyle.Top;
            tlpFilters.Margin = new Padding(0);
            tlpFilters.Name = "tlpFilters";
            tlpFilters.RowCount = 2;
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFilters.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tlpFilters.TabIndex = 0;
            // 
            // lblSearchCap
            // 
            lblSearchCap.AutoSize = true;
            lblSearchCap.BackColor = Color.Transparent;
            lblSearchCap.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSearchCap.ForeColor = Color.FromArgb(20, 34, 51);
            lblSearchCap.Margin = new Padding(0, 0, 0, 6);
            lblSearchCap.Name = "lblSearchCap";
            lblSearchCap.TabIndex = 0;
            lblSearchCap.Text = "Search (record ID, patient, user or details)";
            // 
            // lblActionCap
            // 
            lblActionCap.AutoSize = true;
            lblActionCap.BackColor = Color.Transparent;
            lblActionCap.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblActionCap.ForeColor = Color.FromArgb(20, 34, 51);
            lblActionCap.Margin = new Padding(10, 0, 0, 6);
            lblActionCap.Name = "lblActionCap";
            lblActionCap.TabIndex = 1;
            lblActionCap.Text = "Action";
            // 
            // lblTypeCap
            // 
            lblTypeCap.AutoSize = true;
            lblTypeCap.BackColor = Color.Transparent;
            lblTypeCap.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTypeCap.ForeColor = Color.FromArgb(20, 34, 51);
            lblTypeCap.Margin = new Padding(10, 0, 0, 6);
            lblTypeCap.Name = "lblTypeCap";
            lblTypeCap.TabIndex = 2;
            lblTypeCap.Text = "Record type";
            // 
            // lblFromCap
            // 
            lblFromCap.AutoSize = true;
            lblFromCap.BackColor = Color.Transparent;
            lblFromCap.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFromCap.ForeColor = Color.FromArgb(20, 34, 51);
            lblFromCap.Margin = new Padding(10, 0, 0, 6);
            lblFromCap.Name = "lblFromCap";
            lblFromCap.TabIndex = 3;
            lblFromCap.Text = "From";
            // 
            // lblToCap
            // 
            lblToCap.AutoSize = true;
            lblToCap.BackColor = Color.Transparent;
            lblToCap.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblToCap.ForeColor = Color.FromArgb(20, 34, 51);
            lblToCap.Margin = new Padding(10, 0, 0, 6);
            lblToCap.Name = "lblToCap";
            lblToCap.TabIndex = 4;
            lblToCap.Text = "To";
            // 
            // txtSearch
            // 
            txtSearch.AutoSize = false;
            txtSearch.BackColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Dock = DockStyle.Fill;
            txtSearch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.ForeColor = Color.FromArgb(20, 34, 51);
            txtSearch.Margin = new Padding(0, 0, 0, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 40);
            txtSearch.TabIndex = 5;
            // 
            // cboAction
            // 
            cboAction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboAction.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboAction.Margin = new Padding(10, 0, 0, 4);
            cboAction.Name = "cboAction";
            cboAction.Size = new Size(200, 32);
            cboAction.TabIndex = 6;
            // 
            // cboType
            // 
            cboType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboType.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboType.Margin = new Padding(10, 0, 0, 4);
            cboType.Name = "cboType";
            cboType.Size = new Size(200, 32);
            cboType.TabIndex = 7;
            // 
            // dtpFrom
            // 
            dtpFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpFrom.Checked = false;
            dtpFrom.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Margin = new Padding(10, 4, 0, 4);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.ShowCheckBox = true;
            dtpFrom.Size = new Size(200, 31);
            dtpFrom.TabIndex = 8;
            // 
            // dtpTo
            // 
            dtpTo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpTo.Checked = false;
            dtpTo.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Margin = new Padding(10, 4, 0, 4);
            dtpTo.Name = "dtpTo";
            dtpTo.ShowCheckBox = true;
            dtpTo.Size = new Size(200, 31);
            dtpTo.TabIndex = 9;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(226, 242, 246);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.FromArgb(20, 34, 51);
            btnSearch.Margin = new Padding(10, 0, 0, 0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(90, 40);
            btnSearch.TabIndex = 10;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnShowAll
            // 
            btnShowAll.BackColor = Color.White;
            btnShowAll.FlatStyle = FlatStyle.Flat;
            btnShowAll.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnShowAll.ForeColor = Color.FromArgb(20, 34, 51);
            btnShowAll.Margin = new Padding(10, 0, 0, 0);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(100, 40);
            btnShowAll.TabIndex = 11;
            btnShowAll.Text = "Show All";
            btnShowAll.UseVisualStyleBackColor = false;
            // 
            // pnlResultsCard
            // 
            pnlResultsCard.BackColor = Color.White;
            pnlResultsCard.Controls.Add(dgvAudit);
            pnlResultsCard.Controls.Add(pnlResultsFooter);
            pnlResultsCard.Controls.Add(pnlResultsHeader);
            pnlResultsCard.Dock = DockStyle.Fill;
            pnlResultsCard.Margin = new Padding(0, 0, 0, 20);
            pnlResultsCard.Name = "pnlResultsCard";
            pnlResultsCard.Padding = new Padding(1, 6, 1, 1);
            pnlResultsCard.TabIndex = 2;
            // 
            // dgvAudit
            // 
            dgvAudit.BackgroundColor = Color.White;
            dgvAudit.BorderStyle = BorderStyle.None;
            dgvAudit.Dock = DockStyle.Fill;
            dgvAudit.Margin = new Padding(0);
            dgvAudit.Name = "dgvAudit";
            dgvAudit.RowHeadersVisible = false;
            dgvAudit.TabIndex = 0;
            // 
            // pnlResultsFooter
            // 
            pnlResultsFooter.BackColor = Color.White;
            pnlResultsFooter.Controls.Add(lblCount);
            pnlResultsFooter.Controls.Add(lblLegend);
            pnlResultsFooter.Dock = DockStyle.Bottom;
            pnlResultsFooter.Margin = new Padding(0);
            pnlResultsFooter.Name = "pnlResultsFooter";
            pnlResultsFooter.Padding = new Padding(0);
            pnlResultsFooter.Size = new Size(600, 46);
            pnlResultsFooter.TabIndex = 1;
            // 
            // lblCount
            // 
            lblCount.AutoEllipsis = true;
            lblCount.BackColor = Color.Transparent;
            lblCount.Dock = DockStyle.Fill;
            lblCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCount.ForeColor = Color.FromArgb(91, 107, 124);
            lblCount.Name = "lblCount";
            lblCount.Padding = new Padding(18, 0, 8, 0);
            lblCount.TabIndex = 0;
            lblCount.TextAlign = ContentAlignment.MiddleLeft;
            lblCount.UseMnemonic = false;
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
            lblLegend.Size = new Size(430, 46);
            lblLegend.TabIndex = 1;
            lblLegend.Text = "Rows are added automatically and can never be edited or deleted.";
            lblLegend.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlResultsHeader
            // 
            pnlResultsHeader.BackColor = Color.White;
            pnlResultsHeader.Controls.Add(lblResultsTitle);
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
            lblResultsTitle.Text = "Activity  ·  newest first";
            lblResultsTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblResultsTitle.UseMnemonic = false;
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
            pnlDetailCard.TabIndex = 3;
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
            lblDetailTitle.Text = "Selected entry";
            lblDetailTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblDetailTitle.UseMnemonic = false;
            // 
            // AuditTrailForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1176, 848);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(28, 24, 28, 24);
            FormBorderStyle = FormBorderStyle.None;
            Text = "Audit Trail";
            Controls.Add(tlpRoot);
            Name = "AuditTrailForm";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            pnlFilterCard.ResumeLayout(false);
            pnlFilterCard.PerformLayout();
            tlpFilters.ResumeLayout(false);
            tlpFilters.PerformLayout();
            pnlResultsCard.ResumeLayout(false);
            pnlResultsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAudit).EndInit();
            pnlResultsFooter.ResumeLayout(false);
            pnlResultsFooter.PerformLayout();
            pnlResultsHeader.ResumeLayout(false);
            pnlResultsHeader.PerformLayout();
            pnlDetailCard.ResumeLayout(false);
            pnlDetailCard.PerformLayout();
            pnlDetailBody.ResumeLayout(false);
            pnlDetailBody.PerformLayout();
            pnlDetailHeader.ResumeLayout(false);
            pnlDetailHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilterCard;
        private System.Windows.Forms.TableLayoutPanel tlpFilters;
        private System.Windows.Forms.Label lblSearchCap;
        private System.Windows.Forms.Label lblActionCap;
        private System.Windows.Forms.Label lblTypeCap;
        private System.Windows.Forms.Label lblFromCap;
        private System.Windows.Forms.Label lblToCap;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboAction;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Panel pnlResultsCard;
        private System.Windows.Forms.DataGridView dgvAudit;
        private System.Windows.Forms.Panel pnlResultsFooter;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblLegend;
        private System.Windows.Forms.Panel pnlResultsHeader;
        private System.Windows.Forms.Label lblResultsTitle;
        private System.Windows.Forms.Panel pnlDetailCard;
        private System.Windows.Forms.Panel pnlDetailBody;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Panel pnlDetailHeader;
        private System.Windows.Forms.Label lblDetailTitle;
    }
}
