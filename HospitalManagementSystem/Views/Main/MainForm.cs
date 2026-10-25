using System.Drawing.Drawing2D;
using HospitalManagementSystem.Config;
using HospitalManagementSystem.Helpers;

namespace HospitalManagementSystem.Views.Main
{
    /// <summary>
    /// The application shell (redesign mockup, artboard "2 · Dashboard"):
    ///   - navy sidebar: logo + "HMS", the Dashboard item, four sections of module items, the signed-in user and Log out;
    ///   - white tab strip: every opened module is a tab (Dashboard cannot be closed, the others have an x);
    ///   - content area: the module of the active tab, embedded as a borderless Form.
    /// The sidebar items are built here from <see cref="Sections"/> and <see cref="ModuleRegistry"/>, so the menu and the
    /// role rules (who may open what) always agree. Containers and fixed parts live in MainForm.Designer.cs.
    /// </summary>
    public partial class MainForm : Form, IMessageFilter
    {
        private const string DashboardKey = "DASHBOARD";
        private const int TabMaxWidth = 260;   // design pixels at 100 % scaling
        private const int TabMinWidth = 110;   // below this the tabs stop shrinking and the < > buttons appear
        private const int TabGap = 4;

        /// <summary>The sidebar sections in mockup order: caption and the module keys under it.</summary>
        private static readonly (string Caption, string[] Keys)[] Sections =
        {
            ("PATIENTS & REGISTRATION", new[] { "MP01", "MP02" }),
            ("DOCTORS & APPOINTMENTS", new[] { "MP03", "MP04", "MP05" }),
            ("CLINICAL", new[] { "MP06", "MP07", "MP10" }),
            ("PHARMACY & BILLING", new[] { "MP08", "MP09", "MP11", "MP12" })
        };

        private sealed class TabEntry
        {
            public string Key = string.Empty;
            public Form Form = null!;
            public Panel Header = null!;
            public Label Title = null!;
            public Label? Close;
            public string FullTitle = string.Empty;
            public bool Active;
            public bool CloseHover;
        }

        private sealed class NavItem
        {
            public string Key = string.Empty;
            public Button Button = null!;
            public bool IsDashboard;
            public bool Hover;
        }

        private readonly Dictionary<string, TabEntry> _tabs = new Dictionary<string, TabEntry>();
        private readonly List<string> _history = new List<string>();   // most recently used tab last
        private readonly List<string> _tabOrder = new List<string>();  // left-to-right order of the tab headers
        private int _firstVisibleTab;

        private readonly List<NavItem> _navItems = new List<NavItem>();
        private readonly List<(Label Label, List<NavItem> Items)> _sections = new List<(Label, List<NavItem>)>();

        // Slim sidebar scroll bar (only shown when the menu is taller than the window).
        private int _menuScroll;
        private int _menuMaxScroll;
        private int _menuContentHeight;
        private bool _draggingThumb;
        private int _dragStartCursorY;
        private int _dragStartScroll;

        private string _activeKey = DashboardKey;
        private DashboardForm? _dashboard;
        private bool _checkingConnection;
        private bool _logoutHover;

        /// <summary>True when the user chose "Log out" (Program.cs then shows the login again).</summary>
        public bool LogoutRequested { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            FormHelper.Prepare(this, theme: false);   // this shell paints its own look; icon + drop-downs only

            Text = AppConfig.AppName;
            LogoHelper.ShowInLabel(lblBrandLogo);
            ShowUserInfo();

            BuildMenu();
            StyleChrome();
            CreateDashboardTab();
            WireEvents();
        }

        // ------------------------------------------------------------------
        // Setup
        // ------------------------------------------------------------------

        private void ShowUserInfo()
        {
            var user = SessionManager.CurrentUser;
            string name = user?.FullName ?? "Guest";
            lblUserName.Text = name;
            lblUserRole.Text = user?.Role ?? string.Empty;
            tipTabs.SetToolTip(lblUserName, name);

            string initials = string.Concat(name.Replace("Dr.", string.Empty)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Take(2)
                .Select(part => char.ToUpperInvariant(part[0])));
            lblAvatar.Text = initials.Length == 0 ? "?" : initials;
            ThemeHelper.MakeAvatar(lblAvatar, ThemeHelper.NavyRaised, Color.White);
        }

        /// <summary>Lines under the brand and above the user footer, the tab strip border, the logout icon, tab nav buttons.</summary>
        private void StyleChrome()
        {
            pnlBrand.Paint += (s, e) => DrawLine(e.Graphics, pnlBrand, top: false, ThemeHelper.NavyLine);
            pnlUserFooter.Paint += (s, e) => DrawLine(e.Graphics, pnlUserFooter, top: true, ThemeHelper.NavyLine);
            pnlTabBar.Paint += (s, e) => DrawLine(e.Graphics, pnlTabBar, top: false, ThemeHelper.Border);
            flowTabs.Paint += (s, e) => DrawLine(e.Graphics, flowTabs, top: false, ThemeHelper.Border);

            btnLogout.Paint += OnLogoutPaint;
            btnLogout.MouseEnter += (s, e) => { _logoutHover = true; btnLogout.Invalidate(); };
            btnLogout.MouseLeave += (s, e) => { _logoutHover = false; btnLogout.Invalidate(); };
            tipTabs.SetToolTip(btnLogout, "Log out");
            btnLogout.AccessibleName = "Log out";

            ThemeHelper.StyleButton(btnTabPrev, ButtonKind.Secondary);
            ThemeHelper.StyleButton(btnTabNext, ButtonKind.Secondary);
        }

        private static void DrawLine(Graphics g, Control control, bool top, Color color)
        {
            using var pen = new Pen(color, 1f);
            int y = top ? 0 : control.Height - 1;
            g.DrawLine(pen, 0, y, control.Width, y);
        }

        private void WireEvents()
        {
            btnLogout.Click += (s, e) => ConfirmLogout();

            Load += (s, e) =>
            {
                Application.AddMessageFilter(this);
                LayoutMenu();
                LayoutTabs(true);
            };
            FormClosed += (s, e) => Application.RemoveMessageFilter(this);
            DpiChanged += (s, e) => BeginInvoke(new Action(() =>
            {
                LayoutMenu();
                LayoutTabs(true);
            }));

            pnlMenu.Resize += (s, e) => LayoutMenu();
            pnlScrollTrack.MouseDown += OnScrollTrackMouseDown;
            pnlScrollThumb.MouseDown += OnScrollThumbMouseDown;
            pnlScrollThumb.MouseMove += OnScrollThumbMouseMove;
            pnlScrollThumb.MouseUp += OnScrollThumbMouseUp;
            pnlScrollThumb.MouseEnter += (s, e) => { if (!_draggingThumb) pnlScrollThumb.BackColor = ThemeHelper.AccentBright; };
            pnlScrollThumb.MouseLeave += (s, e) => { if (!_draggingThumb) pnlScrollThumb.BackColor = Color.FromArgb(52, 80, 106); };

            pnlTabBar.Resize += (s, e) => LayoutTabs(true);
            btnTabPrev.Click += (s, e) => ScrollTabs(-1);
            btnTabNext.Click += (s, e) => ScrollTabs(1);

            tmrConnection.Tick += (s, e) => CheckConnectionAsync();
            Shown += (s, e) => CheckConnectionAsync();
            FormClosing += OnFormClosing;
        }

        // ------------------------------------------------------------------
        // Sidebar menu
        // ------------------------------------------------------------------

        /// <summary>Creates the Dashboard item, the section captions and one item per module the user may open.</summary>
        private void BuildMenu()
        {
            pnlMenuContent.SuspendLayout();

            var dashboard = new NavItem { Key = DashboardKey, IsDashboard = true, Button = CreateNavButton("Dashboard") };
            dashboard.Button.Click += (s, e) => ActivateTab(DashboardKey);
            _navItems.Add(dashboard);
            AttachNavPainting(dashboard);

            foreach ((string caption, string[] keys) in Sections)
            {
                var items = new List<NavItem>();
                foreach (string key in keys)
                {
                    ModuleInfo? info = ModuleRegistry.Find(key);
                    if (info == null) continue;
                    if (info.Roles.Length > 0 && !SessionManager.HasRole(info.Roles)) continue;

                    var item = new NavItem { Key = key, Button = CreateNavButton(info.Title) };
                    string captured = key;
                    item.Button.Click += (s, e) => OpenModule(captured);
                    AttachNavPainting(item);
                    _navItems.Add(item);
                    items.Add(item);
                }
                if (items.Count == 0) continue;   // a section with nothing the user may open is not shown

                var label = new Label
                {
                    Text = caption,
                    UseMnemonic = false,
                    BackColor = Color.Transparent,
                    ForeColor = ThemeHelper.NavySection,
                    Font = new Font("Segoe UI", 8.25F, FontStyle.Bold),
                    TextAlign = ContentAlignment.BottomLeft,
                    AutoSize = false
                };
                pnlMenuContent.Controls.Add(label);
                _sections.Add((label, items));
            }

            pnlMenuContent.ResumeLayout(false);
        }

        private Button CreateNavButton(string text)
        {
            var button = new Button
            {
                Text = text,
                UseMnemonic = false,
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeHelper.Navy,
                ForeColor = ThemeHelper.NavyText,
                Font = new Font("Segoe UI", 10.5F),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand,
                TabStop = false,
                UseVisualStyleBackColor = false
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = ThemeHelper.Navy;
            button.FlatAppearance.MouseDownBackColor = ThemeHelper.Navy;
            pnlMenuContent.Controls.Add(button);
            tipTabs.SetToolTip(button, text);
            return button;
        }

        private void AttachNavPainting(NavItem item)
        {
            item.Button.Paint += (s, e) => PaintNavItem(item, e.Graphics);
            item.Button.MouseEnter += (s, e) => { item.Hover = true; item.Button.Invalidate(); };
            item.Button.MouseLeave += (s, e) => { item.Hover = false; item.Button.Invalidate(); };
        }

        /// <summary>
        /// Mockup sizes: content inset 12 px; Dashboard 42 px high with a grid icon; captions 11 px bold caps
        /// (16 px above the first, 14 px above the others, 6 px below); items 36 px high, text 42 px in.
        /// </summary>
        private void LayoutMenu()
        {
            float scale = DeviceDpi / 96f;
            int S(int px) => (int)Math.Round(px * scale);

            int width = pnlMenu.ClientSize.Width - (pnlScrollTrack.Visible ? pnlScrollTrack.Width : 0);
            int inset = S(12);
            int itemWidth = Math.Max(10, width - inset * 2);
            int y = S(14);

            NavItem dashboard = _navItems[0];
            dashboard.Button.SetBounds(inset, y, itemWidth, S(42));
            dashboard.Button.Padding = new Padding(S(42), 0, S(8), 0);
            y += S(42) + S(2);

            for (int i = 0; i < _sections.Count; i++)
            {
                (Label label, List<NavItem> items) = _sections[i];
                int above = i == 0 ? S(16) : S(14);
                int captionHeight = above + label.Font.Height + S(6);
                label.SetBounds(inset + S(12), y, itemWidth - S(12), captionHeight - S(6));
                y += captionHeight;

                foreach (NavItem item in items)
                {
                    item.Button.SetBounds(inset, y, itemWidth, S(36));
                    item.Button.Padding = new Padding(S(42), 0, S(8), 0);
                    y += S(36) + S(2);
                }
            }

            _menuContentHeight = y + S(14);
            UpdateMenuScroll();
        }

        private void PaintNavItem(NavItem item, Graphics g)
        {
            Button button = item.Button;
            float scale = button.DeviceDpi / 96f;
            bool active = item.Key == _activeKey;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(ThemeHelper.Navy);

            if (active || item.Hover)
            {
                using GraphicsPath path = ThemeHelper.RoundedRect(new RectangleF(0, 0, button.Width - 1, button.Height - 1), 8 * scale);
                using var brush = new SolidBrush(active ? ThemeHelper.AccentBright : Color.FromArgb(36, 56, 80));
                g.FillPath(brush, path);
            }

            Color fore = active ? Color.White : ThemeHelper.NavyText;
            using Font font = active ? new Font("Segoe UI Semibold", button.Font.Size) : new Font("Segoe UI", button.Font.Size);

            if (item.IsDashboard)
            {
                // 2x2 grid icon (18 px) at x = 12, like the mockup.
                float size = 18 * scale;
                float x = 12 * scale;
                float top = (button.Height - size) / 2f;
                float cell = 7 * scale;
                using var pen = new Pen(fore, 2f * scale * 0.85f);
                foreach ((float cx, float cy) in new[] { (0f, 0f), (11f, 0f), (0f, 11f), (11f, 11f) })
                {
                    using GraphicsPath box = ThemeHelper.RoundedRect(new RectangleF(x + cx * scale, top + cy * scale, cell, cell), 1.2f * scale);
                    g.DrawPath(pen, box);
                }
            }

            var textBounds = new Rectangle(button.Padding.Left, 0, button.Width - button.Padding.Horizontal, button.Height);
            TextRenderer.DrawText(g, button.Text, font, textBounds, fore,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine);
        }

        private void OnLogoutPaint(object? sender, PaintEventArgs e)
        {
            float scale = btnLogout.DeviceDpi / 96f;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(ThemeHelper.Navy);
            if (_logoutHover)
            {
                using GraphicsPath back = ThemeHelper.RoundedRect(new RectangleF(0, 0, btnLogout.Width - 1, btnLogout.Height - 1), 8 * scale);
                using var brush = new SolidBrush(Color.FromArgb(36, 56, 80));
                g.FillPath(brush, back);
            }

            // Door with an arrow pointing out (18 px icon, stroke 2) - the mockup's log-out symbol.
            float s = 18 * scale / 24f;
            float ox = (btnLogout.Width - 24 * s) / 2f;
            float oy = (btnLogout.Height - 24 * s) / 2f;
            PointF P(float x, float y) => new PointF(ox + x * s, oy + y * s);
            using var pen = new Pen(_logoutHover ? Color.White : ThemeHelper.NavyText, 2f * s) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round };
            g.DrawLines(pen, new[] { P(9, 21), P(5, 21), P(3, 19), P(3, 5), P(5, 3), P(9, 3) });
            g.DrawLines(pen, new[] { P(16, 17), P(21, 12), P(16, 7) });
            g.DrawLine(pen, P(21, 12), P(9, 12));
        }

        private void HighlightNav()
        {
            foreach (NavItem item in _navItems) item.Button.Invalidate();
        }

        // ------------------------------------------------------------------
        // Sidebar scrolling
        // ------------------------------------------------------------------

        private void SetMenuScroll(int value)
        {
            _menuScroll = value;
            UpdateMenuScroll();
        }

        private void UpdateMenuScroll()
        {
            float scale = DeviceDpi / 96f;
            int viewport = pnlMenu.ClientSize.Height;
            _menuMaxScroll = Math.Max(0, _menuContentHeight - viewport);
            _menuScroll = Math.Clamp(_menuScroll, 0, _menuMaxScroll);

            bool needsBar = _menuMaxScroll > 0;
            if (pnlScrollTrack.Visible != needsBar)
            {
                pnlScrollTrack.Visible = needsBar;
                BeginInvoke(new Action(LayoutMenu));   // the item width depends on the bar
            }

            int width = pnlMenu.ClientSize.Width - (needsBar ? pnlScrollTrack.Width : 0);
            pnlMenuContent.SetBounds(0, -_menuScroll, width, Math.Max(_menuContentHeight, viewport));

            if (needsBar)
            {
                int trackHeight = pnlScrollTrack.ClientSize.Height;
                int thumbHeight = Math.Min(trackHeight, Math.Max((int)(28 * scale), (int)((long)trackHeight * viewport / Math.Max(1, _menuContentHeight))));
                int travel = trackHeight - thumbHeight;
                int thumbTop = _menuMaxScroll == 0 ? 0 : (int)((long)travel * _menuScroll / _menuMaxScroll);
                pnlScrollThumb.SetBounds(1, thumbTop, Math.Max(2, pnlScrollTrack.ClientSize.Width - 2), thumbHeight);
            }
        }

        private void OnScrollTrackMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            int page = pnlMenu.ClientSize.Height;
            if (e.Y < pnlScrollThumb.Top) SetMenuScroll(_menuScroll - page);
            else if (e.Y > pnlScrollThumb.Bottom) SetMenuScroll(_menuScroll + page);
        }

        private void OnScrollThumbMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _draggingThumb = true;
            _dragStartCursorY = Cursor.Position.Y;
            _dragStartScroll = _menuScroll;
            pnlScrollThumb.Capture = true;
            pnlScrollThumb.BackColor = ThemeHelper.AccentBright;
        }

        private void OnScrollThumbMouseMove(object? sender, MouseEventArgs e)
        {
            if (!_draggingThumb) return;
            int travel = pnlScrollTrack.ClientSize.Height - pnlScrollThumb.Height;
            if (travel <= 0) return;
            int delta = Cursor.Position.Y - _dragStartCursorY;
            SetMenuScroll(_dragStartScroll + (int)((long)delta * _menuMaxScroll / travel));
        }

        private void OnScrollThumbMouseUp(object? sender, MouseEventArgs e)
        {
            _draggingThumb = false;
            pnlScrollThumb.Capture = false;
            bool inside = pnlScrollThumb.ClientRectangle.Contains(pnlScrollThumb.PointToClient(Cursor.Position));
            pnlScrollThumb.BackColor = inside ? ThemeHelper.AccentBright : Color.FromArgb(52, 80, 106);
        }

        /// <summary>Mouse wheel over the sidebar scrolls the menu even when another control has keyboard focus.</summary>
        public bool PreFilterMessage(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x020A;
            if (m.Msg != WM_MOUSEWHEEL || _menuMaxScroll <= 0 || ActiveForm != this) return false;

            long lParam = m.LParam.ToInt64();
            var screenPoint = new Point((short)(lParam & 0xFFFF), (short)((lParam >> 16) & 0xFFFF));
            if (!pnlMenu.RectangleToScreen(pnlMenu.ClientRectangle).Contains(screenPoint)) return false;

            int delta = (short)((m.WParam.ToInt64() >> 16) & 0xFFFF);
            SetMenuScroll(_menuScroll - (int)(delta / 120f * 56 * (DeviceDpi / 96f)));
            return true;
        }

        // ------------------------------------------------------------------
        // Tabs
        // ------------------------------------------------------------------

        private void CreateDashboardTab()
        {
            _dashboard = new DashboardForm();
            _dashboard.OpenModuleRequested += OpenModule;
            EmbedForm(_dashboard);
            CreateTab(DashboardKey, "Dashboard", _dashboard, closable: false);
            ActivateTab(DashboardKey);
        }

        /// <summary>Opens a module in a new tab, or switches to its tab if it is already open.</summary>
        private void OpenModule(string key)
        {
            if (_tabs.ContainsKey(key))
            {
                ActivateTab(key);
                return;
            }

            ModuleInfo? info = ModuleRegistry.Find(key);
            if (info == null) return;

            Cursor = Cursors.WaitCursor;
            try
            {
                Form form = info.CreateForm();
                EmbedForm(form);
                CreateTab(key, info.Title, form, closable: true);
                ActivateTab(key);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, $"Could not open '{info.Title}'.", this);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>Makes a form behave like a page inside the content area (no border, fills the tab).</summary>
        private void EmbedForm(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Normal;
            form.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(form);
            form.Show();
        }

        private TabEntry CreateTab(string key, string title, Form form, bool closable)
        {
            float scale = DeviceDpi / 96f;

            var header = new Panel { Cursor = Cursors.Hand, BackColor = Color.White, Margin = Padding.Empty };
            var titleLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = title,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding((int)(16 * scale), 0, 0, 0),
                UseMnemonic = false,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = ThemeHelper.TextMuted,
                BackColor = Color.Transparent
            };
            Label? closeLabel = null;
            if (closable)
            {
                closeLabel = new Label
                {
                    Dock = DockStyle.Right,
                    Width = (int)(28 * scale),
                    Text = "×",
                    Font = new Font("Segoe UI", 12F),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand,
                    ForeColor = ThemeHelper.TextFaint,
                    BackColor = Color.Transparent
                };
            }

            header.Controls.Add(titleLabel);
            if (closeLabel != null) header.Controls.Add(closeLabel);
            flowTabs.Controls.Add(header);

            var entry = new TabEntry { Key = key, Form = form, Header = header, Title = titleLabel, Close = closeLabel, FullTitle = title };
            _tabs[key] = entry;
            _tabOrder.Add(key);
            tipTabs.SetToolTip(titleLabel, title);

            header.Paint += (s, e) => PaintTab(entry, e.Graphics);
            header.Click += (s, e) => ActivateTab(key);
            titleLabel.Click += (s, e) => ActivateTab(key);
            if (closeLabel != null)
            {
                closeLabel.Click += (s, e) => CloseTab(key);
                closeLabel.MouseEnter += (s, e) => { entry.CloseHover = true; StyleTab(entry); };
                closeLabel.MouseLeave += (s, e) => { entry.CloseHover = false; StyleTab(entry); };
                tipTabs.SetToolTip(closeLabel, "Close " + title + " (Ctrl+W)");
            }
            return entry;
        }

        /// <summary>Active tab: page-grey background, 1 px border on top and sides, 8 px rounded top corners.</summary>
        private static void PaintTab(TabEntry entry, Graphics g)
        {
            Panel header = entry.Header;
            float scale = header.DeviceDpi / 96f;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            if (entry.Active)
            {
                float r = 8 * scale;
                float w = header.Width - 1;
                float h = header.Height;
                using var path = new GraphicsPath();
                path.AddLine(0, h, 0, r);
                path.AddArc(0, 0, r * 2, r * 2, 180, 90);
                path.AddArc(w - r * 2, 0, r * 2, r * 2, 270, 90);
                path.AddLine(w, r, w, h);
                using (var brush = new SolidBrush(ThemeHelper.Background)) g.FillPath(brush, path);
                using (var pen = new Pen(ThemeHelper.Border, 1f)) g.DrawPath(pen, path);
            }
            else
            {
                using var pen = new Pen(ThemeHelper.Border, 1f);
                g.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
            }
        }

        private void ActivateTab(string key)
        {
            if (!_tabs.TryGetValue(key, out TabEntry? target)) return;

            foreach (TabEntry entry in _tabs.Values)
            {
                entry.Active = ReferenceEquals(entry, target);
                entry.Form.Visible = entry.Active;
                StyleTab(entry);
            }

            target.Form.BringToFront();
            _activeKey = key;
            _history.Remove(key);
            _history.Add(key);
            HighlightNav();
            LayoutTabs(true);

            if (key == DashboardKey) _dashboard?.RefreshData();
        }

        private static void StyleTab(TabEntry entry)
        {
            entry.Title.ForeColor = entry.Active ? ThemeHelper.TextPrimary : ThemeHelper.TextMuted;
            Font wanted = entry.Active ? new Font("Segoe UI Semibold", 10.5F) : new Font("Segoe UI", 10.5F);
            Font old = entry.Title.Font;
            entry.Title.Font = wanted;
            old.Dispose();

            if (entry.Close != null)
            {
                entry.Close.ForeColor = entry.CloseHover ? ThemeHelper.Danger : ThemeHelper.TextFaint;
            }
            entry.Header.Invalidate();
        }

        /// <summary>
        /// Tabs are as wide as their title (like the mockup, up to TabMaxWidth). When they do not fit, all shrink evenly
        /// down to TabMinWidth; when that is still not enough, a window of tabs is shown and the &lt; &gt; buttons move it.
        /// </summary>
        private void LayoutTabs(bool keepActiveVisible)
        {
            int count = _tabOrder.Count;
            if (count == 0 || pnlTabBar.ClientSize.Width <= 0) return;

            float scale = DeviceDpi / 96f;
            int minWidth = (int)(TabMinWidth * scale);
            int maxWidth = (int)(TabMaxWidth * scale);
            int gap = (int)Math.Ceiling(TabGap * scale);
            int height = (int)Math.Round(40 * scale);
            int strip = pnlTabBar.ClientSize.Width - flowTabs.Padding.Horizontal - (lblConnection.Visible ? lblConnection.Width : 0);

            var natural = new int[count];
            for (int i = 0; i < count; i++)
            {
                TabEntry entry = _tabs[_tabOrder[i]];
                using var bold = new Font("Segoe UI Semibold", 10.5F);
                int text = TextRenderer.MeasureText(entry.FullTitle, bold, Size.Empty, TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine).Width;
                natural[i] = Math.Min(maxWidth, entry.Title.Padding.Left + text + (entry.Close?.Width ?? (int)(16 * scale)) + (int)(6 * scale));
            }

            int totalNatural = natural.Sum() + gap * count;
            bool overflow = false;
            int visibleCount = count;
            int[] widths = (int[])natural.Clone();

            if (totalNatural > strip)
            {
                int even = (strip - gap * count) / count;
                if (even >= minWidth)
                {
                    for (int i = 0; i < count; i++) widths[i] = Math.Min(natural[i], even);
                }
                else
                {
                    overflow = true;
                    strip -= pnlTabNav.Width;
                    visibleCount = Math.Max(1, Math.Min(count, strip / (minWidth + gap)));
                    int each = Math.Max(60, strip / visibleCount - gap);
                    for (int i = 0; i < count; i++) widths[i] = Math.Min(natural[i], each);
                }
            }

            int maxFirst = count - visibleCount;
            if (keepActiveVisible)
            {
                int active = _tabOrder.IndexOf(_activeKey);
                if (active >= 0)
                {
                    if (active < _firstVisibleTab) _firstVisibleTab = active;
                    else if (active >= _firstVisibleTab + visibleCount) _firstVisibleTab = active - visibleCount + 1;
                }
            }
            _firstVisibleTab = Math.Clamp(_firstVisibleTab, 0, maxFirst);

            flowTabs.SuspendLayout();
            for (int i = 0; i < count; i++)
            {
                TabEntry entry = _tabs[_tabOrder[i]];
                entry.Header.Visible = i >= _firstVisibleTab && i < _firstVisibleTab + visibleCount;
                entry.Header.Margin = new Padding(0, 0, gap, 0);
                entry.Header.Size = new Size(widths[i], height);
                FitTabText(entry, widths[i]);
            }
            flowTabs.ResumeLayout(true);

            pnlTabNav.Visible = overflow;
            btnTabPrev.Enabled = _firstVisibleTab > 0;
            btnTabNext.Enabled = _firstVisibleTab < maxFirst;
        }

        /// <summary>Shortens a title that does not fit with an ellipsis (the tooltip keeps the full title).</summary>
        private static void FitTabText(TabEntry entry, int tabWidth)
        {
            Label label = entry.Title;
            int available = tabWidth - (entry.Close?.Width ?? 0) - label.Padding.Horizontal - 4;
            const TextFormatFlags flags = TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine;

            string text = entry.FullTitle;
            if (TextRenderer.MeasureText(text, label.Font, new Size(int.MaxValue, int.MaxValue), flags).Width > available)
            {
                int length = text.Length;
                while (length > 1 &&
                       TextRenderer.MeasureText(text.Substring(0, length).TrimEnd() + "…", label.Font,
                           new Size(int.MaxValue, int.MaxValue), flags).Width > available)
                {
                    length--;
                }
                text = text.Substring(0, length).TrimEnd() + "…";
            }
            if (label.Text != text) label.Text = text;
        }

        private void ScrollTabs(int direction)
        {
            _firstVisibleTab += direction;
            LayoutTabs(false);
        }

        private void CloseTab(string key)
        {
            if (key == DashboardKey || !_tabs.TryGetValue(key, out TabEntry? entry)) return;

            entry.Form.Close();
            if (!entry.Form.IsDisposed) return;   // the module cancelled closing (e.g. unsaved changes)

            flowTabs.Controls.Remove(entry.Header);
            entry.Header.Dispose();
            pnlContent.Controls.Remove(entry.Form);
            _tabs.Remove(key);
            _tabOrder.Remove(key);
            _history.Remove(key);

            string next = _history.Count > 0 ? _history[_history.Count - 1] : DashboardKey;
            ActivateTab(next);
        }

        // ------------------------------------------------------------------
        // Log out / close / connection
        // ------------------------------------------------------------------

        private void ConfirmLogout()
        {
            if (MessageHelper.Confirm("Log out of the system?", "Log out", this))
            {
                LogoutRequested = true;
                Close();
            }
        }

        private void OnFormClosing(object? sender, FormClosingEventArgs e)
        {
            if (LogoutRequested || e.CloseReason != CloseReason.UserClosing) return;
            if (!MessageHelper.Confirm("Exit " + AppConfig.AppName + "?", "Exit", this)) e.Cancel = true;
        }

        /// <summary>Checks the database in the background; a red note appears in the tab strip only while it is unreachable.</summary>
        private async void CheckConnectionAsync()
        {
            if (_checkingConnection) return;
            _checkingConnection = true;
            try
            {
                bool connected = await Task.Run(() => DatabaseHelper.TestConnection(out string _));
                if (IsDisposed) return;
                if (lblConnection.Visible == connected)
                {
                    lblConnection.Visible = !connected;
                    LayoutTabs(true);
                }
            }
            finally
            {
                _checkingConnection = false;
            }
        }

        /// <summary>Ctrl+W closes the current tab.</summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.W))
            {
                CloseTab(_activeKey);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
