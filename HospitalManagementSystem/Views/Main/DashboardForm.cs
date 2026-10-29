using System.Data;
using System.Drawing.Drawing2D;
using System.Globalization;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Repositories.Common;

namespace HospitalManagementSystem.Views.Main
{
    /// <summary>
    /// Dashboard (redesign mockup, artboard "2 · Dashboard"): greeting, four headline cards, today's appointments,
    /// the queue right now and the stock alerts. Layout lives in DashboardForm.Designer.cs; this file loads the data
    /// and builds the rows that depend on it. MainForm calls RefreshData() whenever the tab is shown.
    /// </summary>
    public partial class DashboardForm : PageHost
    {
        private const int MaxStockRows = 6;

        private readonly DashboardRepository _repository = new DashboardRepository();
        private readonly List<Control> _queueRows = new List<Control>();
        private readonly List<Control> _stockRows = new List<Control>();

        /// <summary>Raised when the dashboard asks the main window to open a module (the "Open booking" link).</summary>
        public event Action<string>? OpenModuleRequested;

        /// <summary>Raised with a patient ID picked in the dashboard search (the main window opens Patient Search &amp; Profile).</summary>
        public event Action<string>? PatientSearchRequested;

        private DashboardPatientSearch? _patientSearch;

        public DashboardForm()
        {
            InitializeComponent();
            AddPatientSearchBox();
            ThemeHelper.MakeCard(pnlCardRevenue, ThemeHelper.CardRadius, ThemeHelper.Navy);   // the navy KPI card
            FormHelper.Prepare(this);
            ThemeHelper.StyleButton(btnRefresh, ButtonKind.Secondary);

            BuildGridColumns();
            GridHelper.Apply(dgvAppointments);
            GridHelper.EnableStatusColoring(dgvAppointments, "colApStatus");
            pnlAppointmentsHeader.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(230, 235, 240), 1f);
                e.Graphics.DrawLine(pen, 0, pnlAppointmentsHeader.Height - 1, pnlAppointmentsHeader.Width, pnlAppointmentsHeader.Height - 1);
            };

            btnRefresh.Click += (s, e) => RefreshData();
            lnkOpenBooking.LinkClicked += (s, e) => OpenModuleRequested?.Invoke("MP04");
            tmrRefresh.Tick += (s, e) =>
            {
                if (Visible) RefreshData();   // do not hit the database while the tab is hidden
            };
            pnlStockCard.Resize += (s, e) => LayoutStockRows();
            Load += (s, e) => RefreshData();
        }

        /// <summary>
        /// The patient search box in the header (left of Refresh), for the roles that may open Patient Search &amp; Profile.
        /// It is created before the theme runs, so it gets the same rounded input look as every other box.
        /// </summary>
        private void AddPatientSearchBox()
        {
            bool allowed = SessionManager.IsAdministrator ||
                           SessionManager.HasRole(SessionManager.RoleReceptionist, SessionManager.RoleDoctor);
            if (!allowed) return;

            var box = new TextBox
            {
                Name = "txtPatientQuickSearch",
                Font = new Font("Segoe UI", 10.5F),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Size = new Size(340, 40),
                Location = new Point(btnRefresh.Left - 12 - 340, btnRefresh.Top),
                MaxLength = 60,
                TabIndex = 1
            };
            pnlHeader.Controls.Add(box);
            box.BringToFront();
            InputHelper.Attach(box, InputKind.Address, 60);

            _patientSearch = new DashboardPatientSearch(this, box);
            _patientSearch.PatientPicked += id => PatientSearchRequested?.Invoke(id);
        }

        /// <summary>Time / Patient (semibold) / Doctor / Status pill - the mockup's table.</summary>
        private void BuildGridColumns()
        {
            dgvAppointments.AutoGenerateColumns = false;
            dgvAppointments.Columns.Clear();
            GridHelper.AddTextColumn(dgvAppointments, "colApTime", "Time", "appt_time", 16);
            DataGridViewTextBoxColumn patient = GridHelper.AddTextColumn(dgvAppointments, "colApPatient", "Patient", "patient_name", 30);
            patient.DefaultCellStyle.Font = ThemeHelper.BoldFont;
            GridHelper.AddTextColumn(dgvAppointments, "colApDoctor", "Doctor", "doctor_name", 34);
            GridHelper.AddTextColumn(dgvAppointments, "colApStatus", "Status", "status_name", 20);
        }

        /// <summary>Reloads every card and list from the database.</summary>
        public void RefreshData()
        {
            ShowGreeting();
            try
            {
                DashboardStats stats = _repository.GetStats();

                lblPatientsValue.Text = stats.TotalPatients.ToString("N0", CultureInfo.InvariantCulture);
                lblPatientsSub.Text = $"{stats.RegistrationsToday} new today";

                lblAppointmentsValue.Text = stats.AppointmentsToday.ToString(CultureInfo.InvariantCulture);
                var parts = new List<string>
                {
                    $"{stats.CompletedToday} completed",
                    $"{stats.BookedToday} booked",
                    $"{stats.CancelledToday} cancelled"
                };
                if (stats.NoShowToday > 0) parts.Add($"{stats.NoShowToday} no-show");
                lblAppointmentsSub.Text = string.Join(" · ", parts);

                lblQueueValue.Text = stats.ActiveQueue.ToString(CultureInfo.InvariantCulture);
                if (stats.EmergenciesWaiting > 0)
                {
                    lblQueueSub.Text = stats.EmergenciesWaiting == 1 ? "1 emergency waiting" : $"{stats.EmergenciesWaiting} emergencies waiting";
                    lblQueueSub.ForeColor = ThemeHelper.Danger;
                }
                else
                {
                    lblQueueSub.Text = "No emergency waiting";
                    lblQueueSub.ForeColor = ThemeHelper.TextMuted;
                }

                lblRevenueValue.Text = SettingsHelper.FormatMoney(stats.RevenueToday);
                lblRevenueSub.Text = stats.PaymentsToday == 1 ? "1 payment" : $"{stats.PaymentsToday} payments";

                dgvAppointments.DataSource = _repository.GetTodayAppointments();
                dgvAppointments.ClearSelection();

                BuildQueueRows(_repository.GetQueueHighlights());
                BuildStockRows(_repository.GetStockAlerts());
            }
            catch (Exception ex)
            {
                tmrRefresh.Enabled = true; // keep trying on the next tick
                lblDateLine.Text = "Could not refresh the dashboard: " + DatabaseHelper.GetFriendlyMessage(ex);
            }
        }

        /// <summary>"Good evening, Administrator" and today's date.</summary>
        private void ShowGreeting()
        {
            int hour = DateTime.Now.Hour;
            string part = hour < 12 ? "Good morning" : hour < 18 ? "Good afternoon" : "Good evening";

            var user = SessionManager.CurrentUser;
            string who;
            if (user == null) who = "there";
            else if (SessionManager.IsAdministrator) who = "Administrator";
            else if (user.FullName.StartsWith("Dr.", StringComparison.OrdinalIgnoreCase))
                who = "Dr. " + user.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Last();
            else who = user.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? user.FullName;

            lblGreeting.Text = $"{part}, {who}";
            lblDateLine.Text = DateTime.Today.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture) +
                               "  ·  Here is today at a glance.";
        }

        // ------------------------------------------------------------------
        // "Queue right now": 56 px token tile + patient + one line of detail
        // ------------------------------------------------------------------

        private void BuildQueueRows(DataTable rows)
        {
            float scale = DeviceDpi / 96f;
            int S(int px) => (int)Math.Round(px * scale);

            pnlQueueCard.SuspendLayout();
            foreach (Control old in _queueRows)
            {
                pnlQueueCard.Controls.Remove(old);
                old.Dispose();
            }
            _queueRows.Clear();

            int y = lblQueueTitle.Bottom + S(12);
            int x = S(18);

            if (rows.Rows.Count == 0)
            {
                var empty = new Label
                {
                    AutoSize = false,
                    Text = "Nobody is waiting right now.",
                    ForeColor = ThemeHelper.TextMuted,
                    BackColor = Color.Transparent,
                    Font = ThemeHelper.SmallFont,
                    Location = new Point(x, y),
                    Size = new Size(pnlQueueCard.Width - x * 2, S(24)),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                pnlQueueCard.Controls.Add(empty);
                _queueRows.Add(empty);
                y += S(24);
            }

            int waitingPosition = 0;
            foreach (DataRow row in rows.Rows)
            {
                string status = DatabaseHelper.GetString(row, "queue_status");
                bool emergency = DatabaseHelper.GetBool(row, "is_emergency");
                bool serving = status == "In Progress";
                if (!serving) waitingPosition++;

                string token = DatabaseHelper.GetInt(row, "token_number").ToString("000", CultureInfo.InvariantCulture);
                string doctor = DatabaseHelper.GetString(row, "doctor_name");
                string detail;
                Color detailColor = ThemeHelper.TextMuted;
                bool detailBold = false;
                if (serving)
                {
                    int minutes = Math.Max(0, DatabaseHelper.GetInt(row, "minutes_in"));
                    detail = $"{doctor} · in consultation {minutes} min";
                }
                else if (emergency)
                {
                    detail = waitingPosition == 1 ? "Emergency · next in line" : $"Emergency · {doctor}";
                    detailColor = ThemeHelper.DangerInk;
                    detailBold = true;
                }
                else
                {
                    detail = $"{doctor} · waiting";
                }

                var tile = new Label
                {
                    Text = token,
                    Location = new Point(x, y),
                    Size = new Size(S(56), S(56)),
                    Font = new Font("Segoe UI", 16.5F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Color tileBack = emergency && !serving ? ThemeHelper.DangerSoft : ThemeHelper.AccentSoft;
                Color tileFore = emergency && !serving ? ThemeHelper.DangerInk : ThemeHelper.AccentInk;
                tile.Paint += (s, e) => PaintTile(tile, e.Graphics, tileBack, tileFore);

                var name = new Label
                {
                    Text = DatabaseHelper.GetString(row, "patient_name"),
                    AutoEllipsis = true,
                    UseMnemonic = false,
                    BackColor = Color.Transparent,
                    ForeColor = ThemeHelper.TextPrimary,
                    Font = new Font("Segoe UI Semibold", 11.25F),
                    Location = new Point(x + S(70), y + S(8)),
                    Size = new Size(pnlQueueCard.Width - x * 2 - S(70), S(22)),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                var info = new Label
                {
                    Text = detail,
                    AutoEllipsis = true,
                    UseMnemonic = false,
                    BackColor = Color.Transparent,
                    ForeColor = detailColor,
                    Font = detailBold ? ThemeHelper.SmallBoldFont : ThemeHelper.SmallFont,
                    Location = new Point(x + S(70), y + S(30)),
                    Size = new Size(pnlQueueCard.Width - x * 2 - S(70), S(20)),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                foreach (Control control in new Control[] { tile, name, info })
                {
                    pnlQueueCard.Controls.Add(control);
                    _queueRows.Add(control);
                }
                y += S(56) + S(12);
            }

            if (rows.Rows.Count > 0) y -= S(12);
            pnlQueueCard.Height = y + S(16);
            pnlQueueCard.ResumeLayout(true);
            pnlQueueCard.Invalidate();
        }

        private static void PaintTile(Label tile, Graphics g, Color back, Color fore)
        {
            float scale = tile.DeviceDpi / 96f;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(ThemeHelper.EffectiveBack(tile.Parent));
            using (GraphicsPath path = ThemeHelper.RoundedRect(new RectangleF(0, 0, tile.Width - 1, tile.Height - 1), 12 * scale))
            using (var brush = new SolidBrush(back))
            {
                g.FillPath(brush, path);
            }
            TextRenderer.DrawText(g, tile.Text, tile.Font, tile.ClientRectangle, fore,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }

        // ------------------------------------------------------------------
        // "Stock alerts": name on the left, status on the right, thin line between rows
        // ------------------------------------------------------------------

        private DataTable? _stockData;

        private void BuildStockRows(DataTable rows)
        {
            _stockData = rows;
            LayoutStockRows();
        }

        private void LayoutStockRows()
        {
            if (_stockData == null) return;
            float scale = DeviceDpi / 96f;
            int S(int px) => (int)Math.Round(px * scale);

            pnlStockCard.SuspendLayout();
            foreach (Control old in _stockRows)
            {
                pnlStockCard.Controls.Remove(old);
                old.Dispose();
            }
            _stockRows.Clear();

            int x = S(18);
            int width = pnlStockCard.Width - x * 2;
            int y = lblStockTitle.Bottom + S(10);
            int rowHeight = S(34);
            int fit = Math.Max(1, (pnlStockCard.Height - y - S(12)) / rowHeight);
            int count = Math.Min(Math.Min(MaxStockRows, fit), _stockData.Rows.Count);

            if (_stockData.Rows.Count == 0)
            {
                var ok = new Label
                {
                    Text = "All medicines are in stock and in date.",
                    BackColor = Color.Transparent,
                    ForeColor = ThemeHelper.TextMuted,
                    Font = ThemeHelper.SmallFont,
                    Location = new Point(x, y),
                    Size = new Size(width, S(24))
                };
                pnlStockCard.Controls.Add(ok);
                _stockRows.Add(ok);
            }

            for (int i = 0; i < count; i++)
            {
                DataRow row = _stockData.Rows[i];
                string status = DatabaseHelper.GetString(row, "stock_status");
                (string text, Color color) = status switch
                {
                    "Expired" => ("Expired", ThemeHelper.DangerInk),
                    "Expiring Soon" => ("Expiring soon", ThemeHelper.Warning),
                    _ => ("Low stock", ThemeHelper.TextSecondary)
                };

                bool last = i == count - 1;
                var line = new Panel { Location = new Point(x, y), Size = new Size(width, rowHeight), BackColor = Color.White };
                if (!last)
                {
                    line.Paint += (s, e) =>
                    {
                        using var pen = new Pen(ThemeHelper.RowLine, 1f);
                        e.Graphics.DrawLine(pen, 0, line.Height - 1, line.Width, line.Height - 1);
                    };
                }
                var name = new Label
                {
                    Text = DatabaseHelper.GetString(row, "medicine_name"),
                    AutoEllipsis = true,
                    UseMnemonic = false,
                    BackColor = Color.Transparent,
                    ForeColor = ThemeHelper.TextPrimary,
                    Font = ThemeHelper.BaseFont,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(0, 0),
                    Size = new Size(width - S(120), rowHeight - 1)
                };
                var state = new Label
                {
                    Text = text,
                    BackColor = Color.Transparent,
                    ForeColor = color,
                    Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleRight,
                    Location = new Point(width - S(120), 0),
                    Size = new Size(S(120), rowHeight - 1)
                };
                line.Controls.Add(name);
                line.Controls.Add(state);
                pnlStockCard.Controls.Add(line);
                _stockRows.Add(line);
                y += rowHeight;
            }

            if (_stockData.Rows.Count > count && count > 0)
            {
                var more = new Label
                {
                    Text = $"+ {_stockData.Rows.Count - count} more in Medicine Catalog & Stock",
                    BackColor = Color.Transparent,
                    ForeColor = ThemeHelper.TextMuted,
                    Font = ThemeHelper.SmallFont,
                    UseMnemonic = false,
                    Location = new Point(x, y + S(6)),
                    Size = new Size(width, S(20))
                };
                pnlStockCard.Controls.Add(more);
                _stockRows.Add(more);
            }

            pnlStockCard.ResumeLayout(true);
            pnlStockCard.Invalidate();
        }
    }
}
