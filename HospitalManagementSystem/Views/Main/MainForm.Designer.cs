namespace HospitalManagementSystem.Views.Main
{
    partial class MainForm
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

        // Layout = redesign mockup, artboard "2 · Dashboard" (px at 96 DPI):
        //   left   : 264 px navy sidebar - brand (76 px), menu (sections + items, built in MainForm.cs from the
        //            module list so role filtering and the menu can never disagree), user footer with Log out
        //   top    : 52 px white tab strip with a 1 px bottom border; tabs are 40 px high, bottom aligned
        //   content: the open module fills the rest (light grey page)

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlSidebar = new Panel();
            pnlMenu = new Panel();
            pnlMenuContent = new Panel();
            pnlScrollTrack = new Panel();
            pnlScrollThumb = new Panel();
            pnlUserFooter = new Panel();
            lblAvatar = new Label();
            lblUserName = new Label();
            lblUserRole = new Label();
            btnLogout = new Button();
            pnlBrand = new Panel();
            lblBrandLogo = new Label();
            lblBrandName = new Label();
            lblBrandSub = new Label();
            pnlMain = new Panel();
            pnlContent = new Panel();
            pnlTabBar = new Panel();
            flowTabs = new FlowLayoutPanel();
            pnlTabNav = new Panel();
            btnTabNext = new Button();
            btnTabPrev = new Button();
            lblConnection = new Label();
            tipTabs = new ToolTip(components);
            tmrConnection = new System.Windows.Forms.Timer(components);
            pnlSidebar.SuspendLayout();
            pnlMenu.SuspendLayout();
            pnlScrollTrack.SuspendLayout();
            pnlUserFooter.SuspendLayout();
            pnlBrand.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlTabBar.SuspendLayout();
            pnlTabNav.SuspendLayout();
            SuspendLayout();
            //
            // pnlSidebar
            //
            pnlSidebar.BackColor = Color.FromArgb(27, 43, 63);
            pnlSidebar.Controls.Add(pnlMenu);
            pnlSidebar.Controls.Add(pnlUserFooter);
            pnlSidebar.Controls.Add(pnlBrand);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(264, 900);
            pnlSidebar.TabIndex = 0;
            //
            // pnlMenu
            //
            pnlMenu.BackColor = Color.FromArgb(27, 43, 63);
            pnlMenu.Controls.Add(pnlMenuContent);
            pnlMenu.Controls.Add(pnlScrollTrack);
            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.Location = new Point(0, 76);
            pnlMenu.Margin = new Padding(0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(264, 759);
            pnlMenu.TabIndex = 1;
            //
            // pnlMenuContent
            //
            pnlMenuContent.BackColor = Color.FromArgb(27, 43, 63);
            pnlMenuContent.Location = new Point(0, 0);
            pnlMenuContent.Margin = new Padding(0);
            pnlMenuContent.Name = "pnlMenuContent";
            pnlMenuContent.Size = new Size(258, 759);
            pnlMenuContent.TabIndex = 0;
            //
            // pnlScrollTrack
            //
            pnlScrollTrack.BackColor = Color.FromArgb(27, 43, 63);
            pnlScrollTrack.Controls.Add(pnlScrollThumb);
            pnlScrollTrack.Dock = DockStyle.Right;
            pnlScrollTrack.Location = new Point(258, 0);
            pnlScrollTrack.Margin = new Padding(0);
            pnlScrollTrack.Name = "pnlScrollTrack";
            pnlScrollTrack.Size = new Size(6, 759);
            pnlScrollTrack.TabIndex = 1;
            pnlScrollTrack.Visible = false;
            //
            // pnlScrollThumb
            //
            pnlScrollThumb.BackColor = Color.FromArgb(52, 80, 106);
            pnlScrollThumb.Cursor = Cursors.Hand;
            pnlScrollThumb.Location = new Point(1, 0);
            pnlScrollThumb.Margin = new Padding(0);
            pnlScrollThumb.Name = "pnlScrollThumb";
            pnlScrollThumb.Size = new Size(4, 120);
            pnlScrollThumb.TabIndex = 0;
            //
            // pnlUserFooter
            //
            pnlUserFooter.BackColor = Color.FromArgb(27, 43, 63);
            pnlUserFooter.Controls.Add(btnLogout);
            pnlUserFooter.Controls.Add(lblUserRole);
            pnlUserFooter.Controls.Add(lblUserName);
            pnlUserFooter.Controls.Add(lblAvatar);
            pnlUserFooter.Dock = DockStyle.Bottom;
            pnlUserFooter.Location = new Point(0, 835);
            pnlUserFooter.Margin = new Padding(0);
            pnlUserFooter.Name = "pnlUserFooter";
            pnlUserFooter.Size = new Size(264, 65);
            pnlUserFooter.TabIndex = 2;
            //
            // lblAvatar
            //
            lblAvatar.BackColor = Color.Transparent;
            lblAvatar.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAvatar.ForeColor = Color.White;
            lblAvatar.Location = new Point(20, 15);
            lblAvatar.Name = "lblAvatar";
            lblAvatar.Size = new Size(36, 36);
            lblAvatar.TabIndex = 0;
            lblAvatar.Text = "SA";
            lblAvatar.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblUserName
            //
            lblUserName.AutoEllipsis = true;
            lblUserName.BackColor = Color.Transparent;
            lblUserName.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUserName.ForeColor = Color.White;
            lblUserName.Location = new Point(68, 13);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(136, 21);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "System Administrator";
            lblUserName.UseMnemonic = false;
            //
            // lblUserRole
            //
            lblUserRole.BackColor = Color.Transparent;
            lblUserRole.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUserRole.ForeColor = Color.FromArgb(143, 165, 180);
            lblUserRole.Location = new Point(68, 34);
            lblUserRole.Name = "lblUserRole";
            lblUserRole.Size = new Size(136, 18);
            lblUserRole.TabIndex = 2;
            lblUserRole.Text = "Administrator";
            //
            // btnLogout
            //
            btnLogout.BackColor = Color.FromArgb(27, 43, 63);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.ForeColor = Color.FromArgb(198, 212, 222);
            btnLogout.Location = new Point(208, 15);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(36, 36);
            btnLogout.TabIndex = 3;
            btnLogout.TabStop = false;
            btnLogout.UseVisualStyleBackColor = false;
            //
            // pnlBrand
            //
            pnlBrand.BackColor = Color.FromArgb(27, 43, 63);
            pnlBrand.Controls.Add(lblBrandSub);
            pnlBrand.Controls.Add(lblBrandName);
            pnlBrand.Controls.Add(lblBrandLogo);
            pnlBrand.Dock = DockStyle.Top;
            pnlBrand.Location = new Point(0, 0);
            pnlBrand.Margin = new Padding(0);
            pnlBrand.Name = "pnlBrand";
            pnlBrand.Size = new Size(264, 76);
            pnlBrand.TabIndex = 0;
            //
            // lblBrandLogo
            //
            lblBrandLogo.BackColor = Color.Transparent;
            lblBrandLogo.Font = new Font("Segoe UI Symbol", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBrandLogo.ForeColor = Color.FromArgb(34, 163, 184);
            lblBrandLogo.Location = new Point(20, 18);
            lblBrandLogo.Name = "lblBrandLogo";
            lblBrandLogo.Size = new Size(40, 40);
            lblBrandLogo.TabIndex = 0;
            lblBrandLogo.Text = "✚";
            lblBrandLogo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblBrandName
            //
            lblBrandName.BackColor = Color.Transparent;
            lblBrandName.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBrandName.ForeColor = Color.White;
            lblBrandName.Location = new Point(70, 17);
            lblBrandName.Name = "lblBrandName";
            lblBrandName.Size = new Size(180, 22);
            lblBrandName.TabIndex = 1;
            lblBrandName.Text = "HMS";
            //
            // lblBrandSub
            //
            lblBrandSub.BackColor = Color.Transparent;
            lblBrandSub.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBrandSub.ForeColor = Color.FromArgb(143, 165, 180);
            lblBrandSub.Location = new Point(70, 39);
            lblBrandSub.Name = "lblBrandSub";
            lblBrandSub.Size = new Size(180, 18);
            lblBrandSub.TabIndex = 2;
            lblBrandSub.Text = "Hospital Management";
            //
            // pnlMain
            //
            pnlMain.BackColor = Color.FromArgb(243, 246, 249);
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlTabBar);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(264, 0);
            pnlMain.Margin = new Padding(0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1176, 900);
            pnlMain.TabIndex = 1;
            //
            // pnlContent
            //
            pnlContent.BackColor = Color.FromArgb(243, 246, 249);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 52);
            pnlContent.Margin = new Padding(0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1176, 848);
            pnlContent.TabIndex = 1;
            //
            // pnlTabBar
            //
            pnlTabBar.BackColor = Color.White;
            pnlTabBar.Controls.Add(flowTabs);
            pnlTabBar.Controls.Add(lblConnection);
            pnlTabBar.Controls.Add(pnlTabNav);
            pnlTabBar.Dock = DockStyle.Top;
            pnlTabBar.Location = new Point(0, 0);
            pnlTabBar.Margin = new Padding(0);
            pnlTabBar.Name = "pnlTabBar";
            pnlTabBar.Size = new Size(1176, 52);
            pnlTabBar.TabIndex = 0;
            //
            // flowTabs
            //
            flowTabs.BackColor = Color.White;
            flowTabs.Dock = DockStyle.Fill;
            flowTabs.Location = new Point(0, 0);
            flowTabs.Margin = new Padding(0);
            flowTabs.Name = "flowTabs";
            flowTabs.Padding = new Padding(20, 12, 0, 0);
            flowTabs.Size = new Size(976, 51);
            flowTabs.TabIndex = 0;
            flowTabs.WrapContents = false;
            //
            // lblConnection
            //
            lblConnection.BackColor = Color.Transparent;
            lblConnection.Dock = DockStyle.Right;
            lblConnection.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblConnection.ForeColor = Color.FromArgb(155, 28, 20);
            lblConnection.Location = new Point(976, 0);
            lblConnection.Name = "lblConnection";
            lblConnection.Padding = new Padding(0, 0, 20, 0);
            lblConnection.Size = new Size(200, 51);
            lblConnection.TabIndex = 2;
            lblConnection.Text = "Database disconnected";
            lblConnection.TextAlign = ContentAlignment.MiddleRight;
            lblConnection.Visible = false;
            //
            // pnlTabNav
            //
            pnlTabNav.BackColor = Color.White;
            pnlTabNav.Controls.Add(btnTabPrev);
            pnlTabNav.Controls.Add(btnTabNext);
            pnlTabNav.Dock = DockStyle.Right;
            pnlTabNav.Location = new Point(1096, 0);
            pnlTabNav.Margin = new Padding(0);
            pnlTabNav.Name = "pnlTabNav";
            pnlTabNav.Size = new Size(80, 51);
            pnlTabNav.TabIndex = 1;
            pnlTabNav.Visible = false;
            //
            // btnTabPrev
            //
            btnTabPrev.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTabPrev.Location = new Point(4, 12);
            btnTabPrev.Name = "btnTabPrev";
            btnTabPrev.Size = new Size(34, 32);
            btnTabPrev.TabIndex = 0;
            btnTabPrev.TabStop = false;
            btnTabPrev.Text = "<";
            //
            // btnTabNext
            //
            btnTabNext.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTabNext.Location = new Point(42, 12);
            btnTabNext.Name = "btnTabNext";
            btnTabNext.Size = new Size(34, 32);
            btnTabNext.TabIndex = 1;
            btnTabNext.TabStop = false;
            btnTabNext.Text = ">";
            //
            // tmrConnection
            //
            tmrConnection.Enabled = true;
            tmrConnection.Interval = 30000;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 246, 249);
            ClientSize = new Size(1440, 900);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.FromArgb(20, 34, 51);
            MinimumSize = new Size(1100, 700);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hospital Management System";
            WindowState = FormWindowState.Maximized;
            pnlSidebar.ResumeLayout(false);
            pnlMenu.ResumeLayout(false);
            pnlScrollTrack.ResumeLayout(false);
            pnlUserFooter.ResumeLayout(false);
            pnlBrand.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            pnlTabBar.ResumeLayout(false);
            pnlTabNav.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlMenuContent;
        private System.Windows.Forms.Panel pnlScrollTrack;
        private System.Windows.Forms.Panel pnlScrollThumb;
        private System.Windows.Forms.Panel pnlUserFooter;
        private System.Windows.Forms.Label lblAvatar;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Label lblBrandLogo;
        private System.Windows.Forms.Label lblBrandName;
        private System.Windows.Forms.Label lblBrandSub;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlTabBar;
        private System.Windows.Forms.FlowLayoutPanel flowTabs;
        private System.Windows.Forms.Panel pnlTabNav;
        private System.Windows.Forms.Button btnTabNext;
        private System.Windows.Forms.Button btnTabPrev;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.ToolTip tipTabs;
        private System.Windows.Forms.Timer tmrConnection;
    }
}
