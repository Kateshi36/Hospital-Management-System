namespace HospitalManagementSystem.Views.MP12_Payment
{
    partial class RefundDialog
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

        // Refund: title, payment line, a card with amount, method and reason; Cancel / Record Refund underneath.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            lblTitle = new Label();
            lblInfo = new Label();
            pnlCard = new Panel();
            tlpRefund = new TableLayoutPanel();
            tlpFields = new TableLayoutPanel();
            lblAmount = new Label();
            tlpAmount = new TableLayoutPanel();
            txtAmount = new TextBox();
            btnFull = new Button();
            lblMethod = new Label();
            txtMethod = new TextBox();
            lblReason = new Label();
            txtReason = new TextBox();
            lblNote = new Label();
            tlpButtons = new TableLayoutPanel();
            btnCancelDialog = new Button();
            btnConfirm = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlCard.SuspendLayout();
            tlpRefund.SuspendLayout();
            tlpFields.SuspendLayout();
            tlpAmount.SuspendLayout();
            tlpButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.BackColor = Color.FromArgb(243, 246, 249);
            tlpRoot.ColumnCount = 1;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.Controls.Add(lblTitle, 0, 0);
            tlpRoot.Controls.Add(lblInfo, 0, 1);
            tlpRoot.Controls.Add(pnlCard, 0, 2);
            tlpRoot.Controls.Add(tlpButtons, 0, 3);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 4;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tlpRoot.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(20, 34, 51);
            lblTitle.Margin = new Padding(0, 0, 0, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 0;
            lblTitle.UseMnemonic = false;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.BackColor = Color.Transparent;
            lblInfo.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblInfo.Margin = new Padding(0, 0, 0, 16);
            lblInfo.MaximumSize = new Size(640, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.TabIndex = 1;
            lblInfo.UseMnemonic = false;
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.White;
            pnlCard.Controls.Add(tlpRefund);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Margin = new Padding(0, 0, 0, 16);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(20, 18, 20, 4);
            pnlCard.TabIndex = 2;
            // 
            // tlpRefund
            // 
            tlpRefund.AutoSize = true;
            tlpRefund.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpRefund.BackColor = Color.Transparent;
            tlpRefund.ColumnCount = 1;
            tlpRefund.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRefund.Controls.Add(tlpFields, 0, 0);
            tlpRefund.Controls.Add(lblNote, 0, 1);
            tlpRefund.Dock = DockStyle.Top;
            tlpRefund.Margin = new Padding(0);
            tlpRefund.Name = "tlpRefund";
            tlpRefund.RowCount = 2;
            tlpRefund.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRefund.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRefund.TabIndex = 0;
            // 
            // tlpFields
            // 
            tlpFields.AutoSize = true;
            tlpFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpFields.BackColor = Color.Transparent;
            tlpFields.ColumnCount = 2;
            tlpFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFields.Controls.Add(lblAmount, 0, 0);
            tlpFields.Controls.Add(tlpAmount, 0, 1);
            tlpFields.Controls.Add(lblMethod, 1, 0);
            tlpFields.Controls.Add(txtMethod, 1, 1);
            tlpFields.Controls.Add(lblReason, 0, 2);
            tlpFields.SetColumnSpan(lblReason, 2);
            tlpFields.Controls.Add(txtReason, 0, 3);
            tlpFields.SetColumnSpan(txtReason, 2);
            tlpFields.Dock = DockStyle.Top;
            tlpFields.Margin = new Padding(0);
            tlpFields.Name = "tlpFields";
            tlpFields.RowCount = 4;
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 94F));
            tlpFields.TabIndex = 0;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.BackColor = Color.Transparent;
            lblAmount.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAmount.ForeColor = Color.FromArgb(20, 34, 51);
            lblAmount.Margin = new Padding(0, 0, 6, 6);
            lblAmount.Name = "lblAmount";
            lblAmount.TabIndex = 0;
            lblAmount.Text = "Amount to refund *";
            // 
            // tlpAmount
            // 
            tlpAmount.BackColor = Color.Transparent;
            tlpAmount.ColumnCount = 2;
            tlpAmount.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpAmount.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tlpAmount.Controls.Add(txtAmount, 0, 0);
            tlpAmount.Controls.Add(btnFull, 1, 0);
            tlpAmount.Dock = DockStyle.Fill;
            tlpAmount.Margin = new Padding(0, 0, 6, 14);
            tlpAmount.Name = "tlpAmount";
            tlpAmount.RowCount = 1;
            tlpAmount.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpAmount.TabIndex = 1;
            // 
            // txtAmount
            // 
            txtAmount.AutoSize = false;
            txtAmount.BackColor = Color.White;
            txtAmount.BorderStyle = BorderStyle.FixedSingle;
            txtAmount.Dock = DockStyle.Fill;
            txtAmount.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtAmount.ForeColor = Color.FromArgb(20, 34, 51);
            txtAmount.Margin = new Padding(0);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(200, 40);
            txtAmount.TabIndex = 0;
            // 
            // btnFull
            // 
            btnFull.BackColor = Color.FromArgb(226, 242, 246);
            btnFull.Dock = DockStyle.Fill;
            btnFull.FlatStyle = FlatStyle.Flat;
            btnFull.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFull.ForeColor = Color.FromArgb(20, 34, 51);
            btnFull.Margin = new Padding(8, 0, 0, 0);
            btnFull.Name = "btnFull";
            btnFull.Size = new Size(120, 40);
            btnFull.TabIndex = 1;
            btnFull.Text = "Full amount";
            btnFull.UseVisualStyleBackColor = false;
            // 
            // lblMethod
            // 
            lblMethod.AutoSize = true;
            lblMethod.BackColor = Color.Transparent;
            lblMethod.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMethod.ForeColor = Color.FromArgb(20, 34, 51);
            lblMethod.Margin = new Padding(6, 0, 0, 6);
            lblMethod.Name = "lblMethod";
            lblMethod.TabIndex = 2;
            lblMethod.Text = "Refunded by";
            // 
            // txtMethod
            // 
            txtMethod.AutoSize = false;
            txtMethod.BackColor = Color.White;
            txtMethod.BorderStyle = BorderStyle.FixedSingle;
            txtMethod.Dock = DockStyle.Fill;
            txtMethod.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMethod.ForeColor = Color.FromArgb(20, 34, 51);
            txtMethod.Margin = new Padding(6, 0, 0, 14);
            txtMethod.Name = "txtMethod";
            txtMethod.ReadOnly = true;
            txtMethod.Size = new Size(200, 40);
            txtMethod.TabIndex = 3;
            txtMethod.TabStop = false;
            // 
            // lblReason
            // 
            lblReason.AutoSize = true;
            lblReason.BackColor = Color.Transparent;
            lblReason.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReason.ForeColor = Color.FromArgb(20, 34, 51);
            lblReason.Margin = new Padding(0, 0, 0, 6);
            lblReason.Name = "lblReason";
            lblReason.TabIndex = 4;
            lblReason.Text = "Reason for the refund *";
            // 
            // txtReason
            // 
            txtReason.AutoSize = false;
            txtReason.BackColor = Color.White;
            txtReason.BorderStyle = BorderStyle.FixedSingle;
            txtReason.Dock = DockStyle.Fill;
            txtReason.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtReason.ForeColor = Color.FromArgb(20, 34, 51);
            txtReason.Margin = new Padding(0, 0, 0, 14);
            txtReason.Multiline = true;
            txtReason.Name = "txtReason";
            txtReason.ScrollBars = ScrollBars.Vertical;
            txtReason.Size = new Size(200, 80);
            txtReason.TabIndex = 5;
            // 
            // lblNote
            // 
            lblNote.AutoSize = true;
            lblNote.BackColor = Color.Transparent;
            lblNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNote.ForeColor = Color.FromArgb(91, 107, 124);
            lblNote.Margin = new Padding(0, 0, 0, 14);
            lblNote.MaximumSize = new Size(520, 0);
            lblNote.Name = "lblNote";
            lblNote.TabIndex = 1;
            // 
            // tlpButtons
            // 
            tlpButtons.BackColor = Color.Transparent;
            tlpButtons.ColumnCount = 3;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpButtons.Controls.Add(btnCancelDialog, 1, 0);
            tlpButtons.Controls.Add(btnConfirm, 2, 0);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Margin = new Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.RowCount = 1;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpButtons.TabIndex = 3;
            // 
            // btnCancelDialog
            // 
            btnCancelDialog.BackColor = Color.White;
            btnCancelDialog.FlatStyle = FlatStyle.Flat;
            btnCancelDialog.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancelDialog.ForeColor = Color.FromArgb(20, 34, 51);
            btnCancelDialog.Margin = new Padding(10, 0, 0, 0);
            btnCancelDialog.Name = "btnCancelDialog";
            btnCancelDialog.Size = new Size(110, 40);
            btnCancelDialog.TabIndex = 0;
            btnCancelDialog.Text = "Cancel";
            btnCancelDialog.UseVisualStyleBackColor = false;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.FromArgb(180, 35, 24);
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Margin = new Padding(10, 0, 0, 0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(160, 40);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "Record Refund";
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            // 
            // RefundDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(620, 460);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(24, 20, 24, 20);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Refund Payment";
            Controls.Add(tlpRoot);
            Name = "RefundDialog";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            tlpRefund.ResumeLayout(false);
            tlpRefund.PerformLayout();
            tlpFields.ResumeLayout(false);
            tlpFields.PerformLayout();
            tlpAmount.ResumeLayout(false);
            tlpAmount.PerformLayout();
            tlpButtons.ResumeLayout(false);
            tlpButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.TableLayoutPanel tlpRefund;
        private System.Windows.Forms.TableLayoutPanel tlpFields;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TableLayoutPanel tlpAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Button btnFull;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.TextBox txtMethod;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCancelDialog;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
