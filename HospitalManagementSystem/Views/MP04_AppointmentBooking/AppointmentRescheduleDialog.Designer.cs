namespace HospitalManagementSystem.Views.MP04_AppointmentBooking
{
    partial class AppointmentRescheduleDialog
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

        // Redesign: title, appointment line, a card with the new date and slot, Close / Move underneath.
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlpRoot = new TableLayoutPanel();
            lblTitle = new Label();
            lblInfo = new Label();
            pnlCard = new Panel();
            tlpCardBody = new TableLayoutPanel();
            tlpFields = new TableLayoutPanel();
            lblDate = new Label();
            dtpNewDate = new DateTimePicker();
            lblSlot = new Label();
            cboNewSlot = new ComboBox();
            lblSlotInfo = new Label();
            tlpButtons = new TableLayoutPanel();
            btnKeep = new Button();
            btnConfirm = new Button();
            errorProvider = new ErrorProvider(components);
            tlpRoot.SuspendLayout();
            pnlCard.SuspendLayout();
            tlpCardBody.SuspendLayout();
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
            lblTitle.Text = "Reschedule appointment";
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
            pnlCard.Controls.Add(tlpCardBody);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Margin = new Padding(0, 0, 0, 16);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(20, 18, 20, 4);
            pnlCard.TabIndex = 2;
            // 
            // tlpCardBody
            // 
            tlpCardBody.AutoSize = true;
            tlpCardBody.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpCardBody.BackColor = Color.Transparent;
            tlpCardBody.ColumnCount = 1;
            tlpCardBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpCardBody.Controls.Add(tlpFields, 0, 0);
            tlpCardBody.Controls.Add(lblSlotInfo, 0, 1);
            tlpCardBody.Dock = DockStyle.Top;
            tlpCardBody.Margin = new Padding(0);
            tlpCardBody.Name = "tlpCardBody";
            tlpCardBody.RowCount = 2;
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpCardBody.TabIndex = 0;
            // 
            // tlpFields
            // 
            tlpFields.AutoSize = true;
            tlpFields.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpFields.BackColor = Color.Transparent;
            tlpFields.ColumnCount = 2;
            tlpFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpFields.Controls.Add(lblDate, 0, 0);
            tlpFields.Controls.Add(dtpNewDate, 0, 1);
            tlpFields.Controls.Add(lblSlot, 1, 0);
            tlpFields.Controls.Add(cboNewSlot, 1, 1);
            tlpFields.Dock = DockStyle.Top;
            tlpFields.Margin = new Padding(0);
            tlpFields.Name = "tlpFields";
            tlpFields.RowCount = 2;
            tlpFields.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tlpFields.TabIndex = 0;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.BackColor = Color.Transparent;
            lblDate.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDate.ForeColor = Color.FromArgb(20, 34, 51);
            lblDate.Margin = new Padding(0, 0, 6, 6);
            lblDate.Name = "lblDate";
            lblDate.TabIndex = 0;
            lblDate.Text = "New date *";
            lblDate.UseMnemonic = false;
            // 
            // dtpNewDate
            // 
            dtpNewDate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dtpNewDate.CustomFormat = "ddd, MMM dd, yyyy";
            dtpNewDate.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpNewDate.Format = DateTimePickerFormat.Custom;
            dtpNewDate.Margin = new Padding(0, 0, 6, 14);
            dtpNewDate.Name = "dtpNewDate";
            dtpNewDate.Size = new Size(200, 31);
            dtpNewDate.TabIndex = 1;
            // 
            // lblSlot
            // 
            lblSlot.AutoSize = true;
            lblSlot.BackColor = Color.Transparent;
            lblSlot.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSlot.ForeColor = Color.FromArgb(20, 34, 51);
            lblSlot.Margin = new Padding(6, 0, 0, 6);
            lblSlot.Name = "lblSlot";
            lblSlot.TabIndex = 2;
            lblSlot.Text = "New time slot *";
            lblSlot.UseMnemonic = false;
            // 
            // cboNewSlot
            // 
            cboNewSlot.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboNewSlot.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNewSlot.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboNewSlot.FormattingEnabled = true;
            cboNewSlot.Margin = new Padding(6, 0, 0, 14);
            cboNewSlot.Name = "cboNewSlot";
            cboNewSlot.Size = new Size(200, 32);
            cboNewSlot.TabIndex = 3;
            // 
            // lblSlotInfo
            // 
            lblSlotInfo.AutoSize = true;
            lblSlotInfo.BackColor = Color.Transparent;
            lblSlotInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSlotInfo.ForeColor = Color.FromArgb(91, 107, 124);
            lblSlotInfo.Margin = new Padding(0, 0, 0, 14);
            lblSlotInfo.MaximumSize = new Size(460, 0);
            lblSlotInfo.Name = "lblSlotInfo";
            lblSlotInfo.TabIndex = 1;
            lblSlotInfo.UseMnemonic = false;
            // 
            // tlpButtons
            // 
            tlpButtons.BackColor = Color.Transparent;
            tlpButtons.ColumnCount = 3;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpButtons.Controls.Add(btnKeep, 1, 0);
            tlpButtons.Controls.Add(btnConfirm, 2, 0);
            tlpButtons.Dock = DockStyle.Fill;
            tlpButtons.Margin = new Padding(0);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.RowCount = 1;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpButtons.TabIndex = 3;
            // 
            // btnKeep
            // 
            btnKeep.BackColor = Color.White;
            btnKeep.FlatStyle = FlatStyle.Flat;
            btnKeep.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnKeep.ForeColor = Color.FromArgb(20, 34, 51);
            btnKeep.Margin = new Padding(10, 0, 0, 0);
            btnKeep.Name = "btnKeep";
            btnKeep.Size = new Size(110, 40);
            btnKeep.TabIndex = 0;
            btnKeep.Text = "Close";
            btnKeep.UseMnemonic = false;
            btnKeep.UseVisualStyleBackColor = false;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.FromArgb(18, 122, 140);
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Margin = new Padding(10, 0, 0, 0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(170, 40);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "Move Appointment";
            btnConfirm.UseMnemonic = false;
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // AppointmentRescheduleDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(580, 400);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            Padding = new Padding(24, 20, 24, 20);
            CancelButton = btnKeep;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reschedule Appointment";
            Controls.Add(tlpRoot);
            Name = "AppointmentRescheduleDialog";
            tlpRoot.ResumeLayout(false);
            tlpRoot.PerformLayout();
            pnlCard.ResumeLayout(false);
            pnlCard.PerformLayout();
            tlpCardBody.ResumeLayout(false);
            tlpCardBody.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tlpCardBody;
        private System.Windows.Forms.TableLayoutPanel tlpFields;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpNewDate;
        private System.Windows.Forms.Label lblSlot;
        private System.Windows.Forms.ComboBox cboNewSlot;
        private System.Windows.Forms.Label lblSlotInfo;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnKeep;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
