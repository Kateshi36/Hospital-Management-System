namespace HospitalManagementSystem.Views.MP08_MedicineStock
{
    partial class StockAdjustDialog
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

        // Redesign: title, medicine line, a card with batch / direction / quantity / reason, Cancel / Save underneath.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            lblTitle = new Label();
            lblInfo = new Label();
            pnlCard = new Panel();
            tlpFields = new TableLayoutPanel();
            lblBatch = new Label();
            cboBatch = new ComboBox();
            lblDirection = new Label();
            cboDirection = new ComboBox();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            lblReason = new Label();
            txtReason = new TextBox();
            tlpButtons = new TableLayoutPanel();
            btnCancelDialog = new Button();
            btnConfirm = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlCard.SuspendLayout();
            tlpFields.SuspendLayout();
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
            lblTitle.Text = "Adjust stock";
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
            pnlCard.Controls.Add(tlpFields);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Margin = new Padding(0, 0, 0, 16);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(20, 18, 20, 4);
            pnlCard.TabIndex = 2;
            // 
            // tlpFields
            // 
            tlpFields.AutoSize = true;
            tlpFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpFields.BackColor = Color.Transparent;
            tlpFields.ColumnCount = 2;
            tlpFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFields.Controls.Add(lblBatch, 0, 0);
            tlpFields.SetColumnSpan(lblBatch, 2);
            tlpFields.Controls.Add(cboBatch, 0, 1);
            tlpFields.SetColumnSpan(cboBatch, 2);
            tlpFields.Controls.Add(lblDirection, 0, 2);
            tlpFields.Controls.Add(cboDirection, 0, 3);
            tlpFields.Controls.Add(lblQuantity, 1, 2);
            tlpFields.Controls.Add(txtQuantity, 1, 3);
            tlpFields.Controls.Add(lblReason, 0, 4);
            tlpFields.SetColumnSpan(lblReason, 2);
            tlpFields.Controls.Add(txtReason, 0, 5);
            tlpFields.SetColumnSpan(txtReason, 2);
            tlpFields.Dock = DockStyle.Top;
            tlpFields.Margin = new Padding(0);
            tlpFields.Name = "tlpFields";
            tlpFields.RowCount = 6;
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            tlpFields.TabIndex = 0;
            // 
            // lblBatch
            // 
            lblBatch.AutoSize = true;
            lblBatch.BackColor = Color.Transparent;
            lblBatch.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBatch.ForeColor = Color.FromArgb(20, 34, 51);
            lblBatch.Margin = new Padding(0, 0, 0, 6);
            lblBatch.Name = "lblBatch";
            lblBatch.TabIndex = 0;
            lblBatch.Text = "Batch *";
            lblBatch.UseMnemonic = false;
            // 
            // cboBatch
            // 
            cboBatch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboBatch.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBatch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboBatch.FormattingEnabled = true;
            cboBatch.Margin = new Padding(0, 0, 0, 14);
            cboBatch.Name = "cboBatch";
            cboBatch.Size = new Size(200, 32);
            cboBatch.TabIndex = 1;
            // 
            // lblDirection
            // 
            lblDirection.AutoSize = true;
            lblDirection.BackColor = Color.Transparent;
            lblDirection.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDirection.ForeColor = Color.FromArgb(20, 34, 51);
            lblDirection.Margin = new Padding(0, 0, 6, 6);
            lblDirection.Name = "lblDirection";
            lblDirection.TabIndex = 2;
            lblDirection.Text = "Adjustment *";
            lblDirection.UseMnemonic = false;
            // 
            // cboDirection
            // 
            cboDirection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDirection.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboDirection.FormattingEnabled = true;
            cboDirection.Margin = new Padding(0, 0, 6, 14);
            cboDirection.Name = "cboDirection";
            cboDirection.Size = new Size(200, 32);
            cboDirection.TabIndex = 3;
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
            txtQuantity.Margin = new Padding(6, 0, 0, 14);
            txtQuantity.MaxLength = 7;
            txtQuantity.Name = "txtQuantity";
            txtQuantity.PlaceholderText = "whole number above 0";
            txtQuantity.Size = new Size(200, 40);
            txtQuantity.TabIndex = 5;
            // 
            // lblReason
            // 
            lblReason.AutoSize = true;
            lblReason.BackColor = Color.Transparent;
            lblReason.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblReason.ForeColor = Color.FromArgb(20, 34, 51);
            lblReason.Margin = new Padding(0, 0, 0, 6);
            lblReason.Name = "lblReason";
            lblReason.TabIndex = 6;
            lblReason.Text = "Reason *";
            lblReason.UseMnemonic = false;
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
            txtReason.MaxLength = 250;
            txtReason.Multiline = true;
            txtReason.Name = "txtReason";
            txtReason.PlaceholderText = "Required - e.g. damaged packaging, lost, expired stock disposal";
            txtReason.ScrollBars = ScrollBars.Vertical;
            txtReason.Size = new Size(200, 90);
            txtReason.TabIndex = 7;
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
            btnCancelDialog.UseMnemonic = false;
            btnCancelDialog.UseVisualStyleBackColor = false;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.FromArgb(18, 122, 140);
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Margin = new Padding(10, 0, 0, 0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(160, 40);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "Save Adjustment";
            btnConfirm.UseMnemonic = false;
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // StockAdjustDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(600, 500);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(24, 20, 24, 20);
            CancelButton = btnCancelDialog;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Adjust stock";
            Controls.Add(tlpRoot);
            Name = "StockAdjustDialog";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            tlpFields.ResumeLayout(false);
            tlpFields.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tlpFields;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.ComboBox cboBatch;
        private System.Windows.Forms.Label lblDirection;
        private System.Windows.Forms.ComboBox cboDirection;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCancelDialog;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
