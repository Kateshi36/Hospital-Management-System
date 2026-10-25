namespace HospitalManagementSystem.Views.MP08_MedicineStock
{
    partial class StockAddDialog
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

        // Redesign: title, medicine line, a card with the batch fields, Cancel / Add Stock underneath.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            lblTitle = new Label();
            lblInfo = new Label();
            pnlCard = new Panel();
            tlpFields = new TableLayoutPanel();
            lblBatch = new Label();
            txtBatch = new TextBox();
            lblQuantity = new Label();
            txtQuantity = new TextBox();
            lblExpiry = new Label();
            dtpExpiry = new DateTimePicker();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblSupplier = new Label();
            cboSupplier = new ComboBox();
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
            lblTitle.Text = "Add stock (new batch)";
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
            tlpFields.Controls.Add(txtBatch, 0, 1);
            tlpFields.Controls.Add(lblQuantity, 1, 0);
            tlpFields.Controls.Add(txtQuantity, 1, 1);
            tlpFields.Controls.Add(lblExpiry, 0, 2);
            tlpFields.Controls.Add(dtpExpiry, 0, 3);
            tlpFields.Controls.Add(lblPrice, 1, 2);
            tlpFields.Controls.Add(txtPrice, 1, 3);
            tlpFields.Controls.Add(lblSupplier, 0, 4);
            tlpFields.SetColumnSpan(lblSupplier, 2);
            tlpFields.Controls.Add(cboSupplier, 0, 5);
            tlpFields.SetColumnSpan(cboSupplier, 2);
            tlpFields.Dock = DockStyle.Top;
            tlpFields.Margin = new Padding(0);
            tlpFields.Name = "tlpFields";
            tlpFields.RowCount = 6;
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpFields.TabIndex = 0;
            // 
            // lblBatch
            // 
            lblBatch.AutoSize = true;
            lblBatch.BackColor = Color.Transparent;
            lblBatch.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBatch.ForeColor = Color.FromArgb(20, 34, 51);
            lblBatch.Margin = new Padding(0, 0, 6, 6);
            lblBatch.Name = "lblBatch";
            lblBatch.TabIndex = 0;
            lblBatch.Text = "Batch number *";
            lblBatch.UseMnemonic = false;
            // 
            // txtBatch
            // 
            txtBatch.AutoSize = false;
            txtBatch.BackColor = Color.White;
            txtBatch.BorderStyle = BorderStyle.FixedSingle;
            txtBatch.Dock = DockStyle.Fill;
            txtBatch.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBatch.ForeColor = Color.FromArgb(20, 34, 51);
            txtBatch.Margin = new Padding(0, 0, 6, 14);
            txtBatch.MaxLength = 50;
            txtBatch.Name = "txtBatch";
            txtBatch.PlaceholderText = "e.g. PCM-2606";
            txtBatch.Size = new Size(200, 40);
            txtBatch.TabIndex = 1;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.BackColor = Color.Transparent;
            lblQuantity.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblQuantity.ForeColor = Color.FromArgb(20, 34, 51);
            lblQuantity.Margin = new Padding(6, 0, 0, 6);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.TabIndex = 2;
            lblQuantity.Text = "Quantity received *";
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
            txtQuantity.TabIndex = 3;
            // 
            // lblExpiry
            // 
            lblExpiry.AutoSize = true;
            lblExpiry.BackColor = Color.Transparent;
            lblExpiry.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblExpiry.ForeColor = Color.FromArgb(20, 34, 51);
            lblExpiry.Margin = new Padding(0, 0, 6, 6);
            lblExpiry.Name = "lblExpiry";
            lblExpiry.TabIndex = 4;
            lblExpiry.Text = "Expiry date *";
            lblExpiry.UseMnemonic = false;
            // 
            // dtpExpiry
            // 
            dtpExpiry.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpExpiry.CustomFormat = "ddd, MMM dd, yyyy";
            dtpExpiry.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpExpiry.Format = DateTimePickerFormat.Custom;
            dtpExpiry.Margin = new Padding(0, 0, 6, 14);
            dtpExpiry.Name = "dtpExpiry";
            dtpExpiry.Size = new Size(200, 31);
            dtpExpiry.TabIndex = 5;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.BackColor = Color.Transparent;
            lblPrice.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPrice.ForeColor = Color.FromArgb(20, 34, 51);
            lblPrice.Margin = new Padding(6, 0, 0, 6);
            lblPrice.Name = "lblPrice";
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Purchase price (each) *";
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
            txtPrice.TabIndex = 7;
            // 
            // lblSupplier
            // 
            lblSupplier.AutoSize = true;
            lblSupplier.BackColor = Color.Transparent;
            lblSupplier.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSupplier.ForeColor = Color.FromArgb(20, 34, 51);
            lblSupplier.Margin = new Padding(0, 0, 0, 6);
            lblSupplier.Name = "lblSupplier";
            lblSupplier.TabIndex = 8;
            lblSupplier.Text = "Supplier *";
            lblSupplier.UseMnemonic = false;
            // 
            // cboSupplier
            // 
            cboSupplier.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboSupplier.DropDownStyle = ComboBoxStyle.DropDown;
            cboSupplier.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboSupplier.FormattingEnabled = true;
            cboSupplier.Margin = new Padding(0, 0, 0, 14);
            cboSupplier.Name = "cboSupplier";
            cboSupplier.Size = new Size(200, 32);
            cboSupplier.TabIndex = 9;
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
            btnConfirm.Size = new Size(140, 40);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "Add Stock";
            btnConfirm.UseMnemonic = false;
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // StockAddDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(600, 470);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(24, 20, 24, 20);
            CancelButton = btnCancelDialog;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add stock (new batch)";
            Controls.Add(tlpRoot);
            Name = "StockAddDialog";
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
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCancelDialog;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
