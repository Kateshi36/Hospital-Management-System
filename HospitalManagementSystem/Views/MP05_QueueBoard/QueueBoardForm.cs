using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.MP04_AppointmentBooking;
using HospitalManagementSystem.Models.MP05_QueueBoard;
using HospitalManagementSystem.Repositories.MP05_QueueBoard;
using Microsoft.VisualBasic;
using System.Globalization;

namespace HospitalManagementSystem.Views.MP05_QueueBoard
{
    /// <summary>
    /// MP05 (Step 9) - Daily Patient Queue Board. One board = one doctor, today:
    ///   - NOW SERVING panel (large token, patient, running consultation time),
    ///   - the waiting list in CALL ORDER with position and estimated wait,
    ///   - Call Next Patient / Skip Patient / Mark Complete,
    ///   - stats (completed, waiting, emergencies, running average consultation time),
    ///   - check-in of today's Booked appointments and walk-in / emergency patients.
    ///
    /// Rules (all in QueueRepository, repeated inside each save transaction):
    ///   - only one patient In Progress per doctor;
    ///   - Call Next takes an emergency patient first, otherwise the lowest token (FIFO);
    ///   - Skip needs a reason, is logged, and sends the patient back to Waiting behind everybody not skipped yet;
    ///   - Mark Complete stores the actual time taken, updates the average and completes the linked appointment;
    ///   - estimated wait = position x the doctor's average consultation time today (default setting until the first visit ends).
    ///
    /// The board refreshes itself every 15 seconds (switch off with the check box) and the running time ticks every second.
    /// The layout lives in QueueBoardForm.Designer.cs; this file holds behaviour plus the runtime parts of the redesign
    /// (button kinds, the two-line patient cell, emergency row tint, the check-in list drawn as selectable rows).
    /// </summary>
    public partial class QueueBoardForm : Form
    {
        private const int AutoRefreshSeconds = 15;

        private readonly QueueRepository _repository = new QueueRepository();
        private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer { Interval = 1000 };

        private QueueBoard? _board;
        private List<CheckInCandidate> _candidates = new List<CheckInCandidate>();
        private bool _loading;                  // true while the code (not the user) fills the doctor list
        private bool _busy;                     // true while an action or dialog is open: no automatic refresh then
        private int _secondsSinceRefresh;
        private readonly ToolTip tipStats = new ToolTip();

        public QueueBoardForm()
        {
            InitializeComponent();
            ThemeHelper.MakeCard(pnlNow, 16, ThemeHelper.Navy);
            ThemeHelper.MakeCard(pnlWaitingCard);
            ThemeHelper.MakeCard(pnlStatCompleted);
            ThemeHelper.MakeCard(pnlStatWaiting);
            ThemeHelper.MakeCard(pnlStatEmergency);
            ThemeHelper.MakeCard(pnlStatAverage);
            ThemeHelper.MakeCard(pnlCheckInCard);
            FormHelper.Prepare(this);        // theme, drop-down lists line up with their boxes, logo as window icon
            ApplyMockupStyles();

            BuildGridColumns();
            GridHelper.Apply(dgvWaiting);       // read-only guards + auto-size / wrap
            GridHelper.Apply(dgvCheckIn);
            GridHelper.EnableStatusColoring(dgvWaiting, nameof(colWType));   // Emergency / Appointment / Walk-in pill
            SetupWaitingGrid();
            SetupCheckInList();

            WireEvents();
            Load += (s, e) => InitializeScreen();
            Disposed += (s, e) => { _timer.Dispose(); tipStats.Dispose(); };
        }

        /// <summary>
        /// Creates the waiting-list and check-in columns in code (the Designer drops designer-made grid columns when it
        /// re-saves a large file). DataPropertyName = properties of QueueEntryRecord / CheckInCandidate.
        /// </summary>
        private void BuildGridColumns()
        {
            dgvWaiting.AutoGenerateColumns = false;
            dgvWaiting.Columns.Clear();
            colWPos = GridHelper.AddTextColumn(dgvWaiting, nameof(colWPos), "Pos", nameof(QueueEntryRecord.Position), 6, center: true);
            colWToken = GridHelper.AddTextColumn(dgvWaiting, nameof(colWToken), "Token", nameof(QueueEntryRecord.TokenText), 8, center: true);
            colWPatient = GridHelper.AddTextColumn(dgvWaiting, nameof(colWPatient), "Patient", nameof(QueueEntryRecord.PatientName), 30);
            colWCode = GridHelper.AddTextColumn(dgvWaiting, nameof(colWCode), "Patient ID", nameof(QueueEntryRecord.PatientId), 17);
            colWCode.Visible = false;     // the mockup shows the patient ID under the name (see OnWaitingCellPainting)
            colWType = GridHelper.AddTextColumn(dgvWaiting, nameof(colWType), "Type", nameof(QueueEntryRecord.TypeText), 12, center: true);
            colWCheckIn = GridHelper.AddTextColumn(dgvWaiting, nameof(colWCheckIn), "Checked in", nameof(QueueEntryRecord.CheckedInText), 11);
            colWSkips = GridHelper.AddTextColumn(dgvWaiting, nameof(colWSkips), "Skips", nameof(QueueEntryRecord.SkipText), 10);
            colWWait = GridHelper.AddTextColumn(dgvWaiting, nameof(colWWait), "Est. wait", nameof(QueueEntryRecord.EstimatedWaitText), 10);

            dgvCheckIn.AutoGenerateColumns = false;
            dgvCheckIn.Columns.Clear();
            colCTime = GridHelper.AddTextColumn(dgvCheckIn, nameof(colCTime), "Time", nameof(CheckInCandidate.TimeText), 14);
            colCToken = GridHelper.AddTextColumn(dgvCheckIn, nameof(colCToken), "Token", nameof(CheckInCandidate.TokenNumber), 10, center: true, format: "000");
            colCPatient = GridHelper.AddTextColumn(dgvCheckIn, nameof(colCPatient), "Patient", nameof(CheckInCandidate.PatientName), 30);
            colCCode = GridHelper.AddTextColumn(dgvCheckIn, nameof(colCCode), "Patient ID", nameof(CheckInCandidate.PatientId), 23);
            colCAppointment = GridHelper.AddTextColumn(dgvCheckIn, nameof(colCAppointment), "Appointment ID", nameof(CheckInCandidate.AppointmentId), 23);
        }

        /// <summary>Button kinds, box heights and the divider of the mockup.</summary>
        private void ApplyMockupStyles()
        {
            ThemeHelper.StyleButton(btnRefresh, ButtonKind.Secondary);
            ThemeHelper.StyleButton(btnComplete, ButtonKind.Bright);
            ThemeHelper.StyleButton(btnSkip, ButtonKind.Ghost);
            ThemeHelper.StyleButton(btnCallNext, ButtonKind.Ghost);
            ThemeHelper.StyleButton(btnCheckIn, ButtonKind.Primary);
            ThemeHelper.StyleButton(btnWalkIn, ButtonKind.Secondary);
            ThemeHelper.SetComboHeight(cboDoctor, 40);

            // The theme turns light text dark on light backgrounds; the navy panel keeps its own colours.
            lblNowCaption.ForeColor = ThemeHelper.NavyAccentText;
            lblNowPatient.ForeColor = Color.White;
            lblNowTimer.ForeColor = Color.White;
            lblNowDetail.ForeColor = ThemeHelper.NavySoftText;
            lblNowStarted.ForeColor = ThemeHelper.NavySoftText;
            lblNowTimerSuffix.ForeColor = ThemeHelper.NavySoftText;
            chkEmergency.ForeColor = ThemeHelper.TextPrimary;

            pnlWaitingHeader.Paint += (s, e) =>
            {
                using var pen = new Pen(CardDivider);
                e.Graphics.DrawLine(pen, 0, pnlWaitingHeader.Height - 1, pnlWaitingHeader.Width, pnlWaitingHeader.Height - 1);
            };
            pnlWaitingHeader.Resize += (s, e) => pnlWaitingHeader.Invalidate();
        }

        private static readonly Color CardDivider = Color.FromArgb(0xE6, 0xEB, 0xF0);
        private static readonly Color EmergencyOnNavy = Color.FromArgb(0xFF, 0x9C, 0x94);

        /// <summary>Waiting list rows: two-line patient cell, so a fixed 60 px row instead of auto height.</summary>
        private void SetupWaitingGrid()
        {
            float scale = DeviceDpi / 96f;
            dgvWaiting.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvWaiting.RowTemplate.Height = (int)Math.Round(60 * scale);
            dgvWaiting.RowTemplate.MinimumHeight = (int)Math.Round(60 * scale);
            dgvWaiting.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvWaiting.DefaultCellStyle.SelectionBackColor = ThemeHelper.Panel;      // the list is for reading, not selecting
            dgvWaiting.DefaultCellStyle.SelectionForeColor = ThemeHelper.TextPrimary;
            dgvWaiting.RowsDefaultCellStyle.SelectionBackColor = ThemeHelper.Panel;
            dgvWaiting.AlternatingRowsDefaultCellStyle.SelectionBackColor = ThemeHelper.Panel;
            colWPos.DefaultCellStyle.Font = ThemeHelper.BoldFont;
            colWToken.DefaultCellStyle.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            colWWait.DefaultCellStyle.Font = ThemeHelper.BoldFont;
            colWPos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colWToken.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colWType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colWPos.HeaderCell.Style.Padding = new Padding((int)Math.Round(20 * scale), 0, 0, 0);
            colWPos.DefaultCellStyle.Padding = new Padding((int)Math.Round(20 * scale), 0, 0, 0);
            dgvWaiting.CellPainting += OnWaitingCellPainting;
        }

        /// <summary>Patient cell: name (semibold) with the patient ID under it in small muted text.</summary>
        private void OnWaitingCellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Graphics == null || dgvWaiting.Columns[e.ColumnIndex].Name != nameof(colWPatient)) return;
            if (dgvWaiting.Rows[e.RowIndex].DataBoundItem is not QueueEntryRecord entry) return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
            Padding pad = e.CellStyle?.Padding ?? Padding.Empty;
            int x = e.CellBounds.X + pad.Left;
            int width = e.CellBounds.Width - pad.Horizontal;
            int nameHeight = ThemeHelper.BoldFont.Height;
            int idHeight = SmallIdFont.Height;
            int top = e.CellBounds.Y + (e.CellBounds.Height - nameHeight - idHeight) / 2;
            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine;
            TextRenderer.DrawText(e.Graphics, entry.PatientName, ThemeHelper.BoldFont, new Rectangle(x, top, width, nameHeight), ThemeHelper.TextPrimary, flags);
            TextRenderer.DrawText(e.Graphics, entry.PatientId, SmallIdFont, new Rectangle(x, top + nameHeight, width, idHeight), ThemeHelper.TextMuted, flags);
            e.Handled = true;
        }

        private static readonly Font SmallIdFont = new Font("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>
        /// The check-in list of the mockup: no header, each appointment a rounded row (time | name over
        /// "Token 006 · APT-..."), the selected one tinted teal. The grid stays a grid (binding, selection, keyboard).
        /// </summary>
        private void SetupCheckInList()
        {
            float scale = DeviceDpi / 96f;
            dgvCheckIn.ColumnHeadersVisible = false;
            dgvCheckIn.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvCheckIn.ScrollBars = ScrollBars.Vertical;
            dgvCheckIn.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvCheckIn.RowTemplate.Height = (int)Math.Round(64 * scale);
            dgvCheckIn.RowTemplate.MinimumHeight = (int)Math.Round(64 * scale);
            colCToken.Visible = false;
            colCCode.Visible = false;
            colCAppointment.Visible = false;
            colCTime.Visible = false;
            colCPatient.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCPatient.MinimumWidth = 20;

            dgvCheckIn.CellPainting += (s, e) => { if (e.RowIndex >= 0) e.Handled = true; };   // the row is drawn below
            dgvCheckIn.RowPostPaint += OnCheckInRowPaint;
            dgvCheckIn.SelectionChanged += (s, e) => dgvCheckIn.Invalidate();
            dgvCheckIn.Paint += (s, e) =>
            {
                if (dgvCheckIn.Rows.Count > 0) return;
                var area = new Rectangle(0, 0, dgvCheckIn.Width, (int)Math.Round(48 * scale));
                TextRenderer.DrawText(e.Graphics, SelectedDoctor == null ? string.Empty : "Everyone booked for today has checked in.",
                    ThemeHelper.SmallFont, area, ThemeHelper.TextFaint, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
            };
        }

        private void OnCheckInRowPaint(object? sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dgvCheckIn.Rows[e.RowIndex].DataBoundItem is not CheckInCandidate candidate) return;

            float scale = DeviceDpi / 96f;
            Graphics g = e.Graphics;
            Rectangle row = new Rectangle(0, e.RowBounds.Y, dgvCheckIn.ClientSize.Width - (dgvCheckIn.DisplayedRowCount(false) < dgvCheckIn.RowCount ? SystemInformation.VerticalScrollBarWidth : 0), e.RowBounds.Height);
            using (var clear = new SolidBrush(ThemeHelper.Panel)) g.FillRectangle(clear, row);

            bool selected = dgvCheckIn.Rows[e.RowIndex].Selected;
            int gap = (int)Math.Round(6 * scale);
            var card = new RectangleF(row.X + 0.5f, row.Y + 0.5f, row.Width - 1.5f, row.Height - gap - 1f);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var path = ThemeHelper.RoundedRect(card, 8 * scale))
            using (var fill = new SolidBrush(selected ? ThemeHelper.AccentTint : ThemeHelper.Panel))
            using (var pen = new Pen(selected ? ThemeHelper.AccentLine : ThemeHelper.Border))
            {
                g.FillPath(fill, path);
                g.DrawPath(pen, path);
            }

            int padX = (int)Math.Round(12 * scale);
            int timeWidth = (int)Math.Round(70 * scale);
            int textX = (int)card.X + padX;
            Rectangle cardBox = Rectangle.Round(card);
            const TextFormatFlags one = TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine;

            TextRenderer.DrawText(g, candidate.TimeText, ThemeHelper.BoldFont,
                new Rectangle(textX, cardBox.Y, timeWidth, cardBox.Height), ThemeHelper.TextPrimary, one | TextFormatFlags.VerticalCenter);

            int infoX = textX + timeWidth + padX;
            int infoWidth = cardBox.Right - padX - infoX;
            int nameHeight = ThemeHelper.BoldFont.Height;
            int subHeight = SmallIdFont.Height;
            int top = cardBox.Y + (cardBox.Height - nameHeight - subHeight) / 2;
            TextRenderer.DrawText(g, candidate.PatientName, ThemeHelper.BoldFont, new Rectangle(infoX, top, infoWidth, nameHeight), ThemeHelper.TextPrimary, one);
            TextRenderer.DrawText(g, $"Token {candidate.TokenNumber:000}  ·  {candidate.AppointmentId}", SmallIdFont,
                new Rectangle(infoX, top + nameHeight, infoWidth, subHeight), ThemeHelper.TextMuted, one);
        }

        private DoctorOption? SelectedDoctor => cboDoctor.SelectedItem as DoctorOption;

        private IWin32Window DialogOwner() => (IWin32Window?)TopLevelControl ?? this;

        private void WireEvents()
        {
            btnRefresh.Click += (s, e) => ReloadBoard(false);
            cboDoctor.SelectedIndexChanged += (s, e) => { if (!_loading) ReloadBoard(false); };

            btnCallNext.Click += (s, e) => CallNext();
            btnSkip.Click += (s, e) => SkipCurrent();
            btnComplete.Click += (s, e) => CompleteCurrent();

            btnCheckIn.Click += (s, e) => CheckInSelected();
            btnWalkIn.Click += (s, e) => AddWalkIn();
            dgvCheckIn.SelectionChanged += (s, e) => UpdateButtons();
            dgvCheckIn.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) CheckInSelected(); };

            dgvWaiting.CellFormatting += OnWaitingCellFormatting;

            _timer.Tick += (s, e) => OnTick();
            VisibleChanged += (s, e) => { if (Visible && _board != null && !_busy) ReloadBoard(true); };
        }

        // ------------------------------------------------------------------
        // Loading
        // ------------------------------------------------------------------

        private void InitializeScreen()
        {
            _loading = true;
            try
            {
                List<DoctorOption> doctors = _repository.GetDoctors();
                cboDoctor.DataSource = doctors;
                cboDoctor.DisplayMember = nameof(DoctorOption.Display);
                cboDoctor.ValueMember = nameof(DoctorOption.DoctorId);

                if (doctors.Count > 0)
                {
                    // Start on the doctor with the busiest queue today.
                    string? busiest = _repository.GetBusiestDoctorId();
                    int index = busiest != null ? doctors.FindIndex(d => d.DoctorId == busiest) : 0;
                    cboDoctor.SelectedIndex = index < 0 ? 0 : index;
                }
                else
                {
                    cboDoctor.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The doctor list could not be loaded.", DialogOwner());
            }
            finally
            {
                _loading = false;
            }

            ReloadBoard(false);
            _timer.Start();
        }

        /// <summary>Loads the board and the check-in list of the selected doctor. quiet = automatic refresh (no pop-up on failure).</summary>
        private void ReloadBoard(bool quiet)
        {

            DoctorOption? doctor = SelectedDoctor;
            if (doctor == null)
            {
                _board = null;
                _candidates = new List<CheckInCandidate>();
                Render();
                SetStatus("No doctors are registered yet. Add a doctor under Doctor Directory & Schedule.", ThemeHelper.Warning);
                return;
            }

            try
            {
                _board = _repository.LoadBoard(doctor.DoctorId);
                _candidates = _repository.GetCheckInCandidates(doctor.DoctorId);
                _secondsSinceRefresh = 0;
                Render();
                SetStatus($"Updated {DateTime.Now:hh:mm:ss tt}", ThemeHelper.TextMuted);
            }
            catch (Exception ex)
            {
                if (quiet) SetStatus("Could not refresh: " + DatabaseHelper.GetFriendlyMessage(ex), ThemeHelper.Danger);
                else MessageHelper.Error(ex, "The queue board could not be loaded.", DialogOwner());
            }
        }

        // ------------------------------------------------------------------
        // Drawing the board
        // ------------------------------------------------------------------

        private void Render()
        {
            QueueBoard board = _board ?? new QueueBoard();

            // Waiting list.
            dgvWaiting.DataSource = null;
            dgvWaiting.DataSource = board.Waiting;
            dgvWaiting.ClearSelection();
            lblWaitingCount.Text = board.Waiting.Count == 1 ? "·  1 patient" : $"·  {board.Waiting.Count} patients";

            // Check-in list: keep the same appointment selected after a refresh.
            string? selectedAppointment = (dgvCheckIn.SelectedRows.Count > 0 ? dgvCheckIn.SelectedRows[0].DataBoundItem as CheckInCandidate : null)?.AppointmentId;
            dgvCheckIn.DataSource = null;
            dgvCheckIn.DataSource = _candidates;
            dgvCheckIn.ClearSelection();
            if (selectedAppointment != null)
            {
                foreach (DataGridViewRow row in dgvCheckIn.Rows)
                {
                    if (row.DataBoundItem is CheckInCandidate c && c.AppointmentId == selectedAppointment)
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }

            // Stats.
            QueueStats stats = board.Stats;
            lblStatCompleted.Text = stats.Completed.ToString(CultureInfo.InvariantCulture);
            lblStatWaiting.Text = stats.Waiting.ToString(CultureInfo.InvariantCulture);
            lblStatEmergency.Text = stats.EmergenciesWaiting.ToString(CultureInfo.InvariantCulture);
            lblStatEmergency.ForeColor = stats.EmergenciesWaiting > 0 ? ThemeHelper.Danger : ThemeHelper.TextPrimary;
            lblStatAverage.Text = $"{stats.AverageMinutes:0.#} min";        // mockup tile: "16 min"
            tipStats.SetToolTip(lblStatAverage, stats.AverageIsDefault
                ? "Default value: no consultation has been completed today yet."
                : "Average consultation time today.");

            RenderNowServing(board);
            UpdateButtons();
        }

        private void RenderNowServing(QueueBoard board)
        {
            QueueEntryRecord? now = board.NowServing;

            if (now == null)
            {
                lblNowToken.Text = "—";
                lblNowToken.ForeColor = Color.White;
                lblNowPatient.Text = "No patient in consultation";

                if (board.Waiting.Count == 0)
                {
                    lblNowDetail.Text = "Nobody is waiting.";
                    SetElapsed("Check patients in on the right.", string.Empty, string.Empty);
                }
                else
                {
                    QueueEntryRecord next = board.Waiting[0];
                    lblNowDetail.Text = $"Next in line: token {next.TokenText}  ·  {next.PatientName}" +
                                        (next.IsEmergency ? "  (EMERGENCY)" : string.Empty);
                    SetElapsed("Press Call Next Patient to start the consultation.", string.Empty, string.Empty);
                }
                return;
            }

            lblNowToken.Text = now.TokenText;
            lblNowToken.ForeColor = now.IsEmergency ? EmergencyOnNavy : Color.White;
            lblNowPatient.Text = now.PatientName + (now.IsEmergency ? "  (EMERGENCY)" : string.Empty);
            var detail = new System.Text.StringBuilder(now.PatientId);
            detail.Append("  ·  " + (now.AppointmentId ?? "Walk-in"));
            if (now.VisitReason.Length > 0) detail.Append("  ·  " + now.VisitReason);
            lblNowDetail.Text = detail.ToString();
            tipStats.SetToolTip(lblNowDetail, lblNowDetail.Text);
            UpdateElapsed();
        }

        /// <summary>Re-draws the detail line of the Now Serving panel (called every second).</summary>
        private void UpdateElapsed()
        {
            QueueEntryRecord? now = _board?.NowServing;
            if (now == null) return;

            if (!now.StartedAt.HasValue)
            {
                SetElapsed("In consultation", string.Empty, string.Empty);
                return;
            }

            // Mockup: "Started 9:42 PM  ·  10:24 in consultation" with the running mm:ss in bold white.
            string started = now.StartedAt.Value.ToString("h:mm tt", CultureInfo.InvariantCulture);
            TimeSpan span = DateTime.Now - now.StartedAt.Value;
            if (span < TimeSpan.Zero) span = TimeSpan.Zero;
            SetElapsed($"Started {started}  ·  ", $"{(int)span.TotalMinutes}:{span.Seconds:00}", " in consultation");
        }

        private void SetElapsed(string prefix, string timer, string suffix)
        {
            if (lblNowStarted.Text != prefix) lblNowStarted.Text = prefix;
            if (lblNowTimer.Text != timer) lblNowTimer.Text = timer;
            if (lblNowTimerSuffix.Text != suffix) lblNowTimerSuffix.Text = suffix;
        }

        private void UpdateButtons()
        {
            bool hasDoctor = SelectedDoctor != null;
            bool serving = _board?.NowServing != null;
            bool waiting = (_board?.Waiting.Count ?? 0) > 0;

            btnCallNext.Enabled = hasDoctor && !serving && waiting;
            btnSkip.Enabled = serving;
            btnComplete.Enabled = serving;

            bool canCheckIn = hasDoctor && dgvCheckIn.SelectedRows.Count > 0;
            btnCheckIn.Enabled = canCheckIn;
            chkEmergency.Enabled = canCheckIn;
            if (!canCheckIn) chkEmergency.Checked = false;
            btnWalkIn.Enabled = hasDoctor;
        }

        private void SetStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }

        /// <summary>Emergency rows get a soft red tint (the Type pill itself is solid red); no skips shows a dash.</summary>
        private void OnWaitingCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvWaiting.Rows.Count || e.CellStyle == null) return;
            if (dgvWaiting.Rows[e.RowIndex].DataBoundItem is not QueueEntryRecord entry) return;

            if (dgvWaiting.Columns[e.ColumnIndex].Name == nameof(colWSkips))
            {
                if (entry.SkipCount == 0)
                {
                    e.Value = "—";
                    e.FormattingApplied = true;
                }
                e.CellStyle.ForeColor = ThemeHelper.TextMuted;
            }

            if (!entry.IsEmergency) return;
            e.CellStyle.BackColor = ThemeHelper.DangerTint;
            e.CellStyle.SelectionBackColor = ThemeHelper.DangerTint;
        }

        private void OnTick()
        {
            if (!Visible || _board == null) return;

            UpdateElapsed();

            _secondsSinceRefresh++;
            if (chkAutoRefresh.Checked && !_busy && _secondsSinceRefresh >= AutoRefreshSeconds)
                ReloadBoard(true);
        }

        // ------------------------------------------------------------------
        // Actions
        // ------------------------------------------------------------------

        /// <summary>
        /// Runs one change on the queue. The action returns the success text for the status line.
        /// A rule violation (InvalidOperationException from the repository) is shown as a warning and the board is
        /// refreshed, because someone else may have changed the queue in the meantime.
        /// </summary>
        private void Execute(string errorContext, Func<string?> action)
        {
            _busy = true;
            try
            {
                string? message = action();
                ReloadBoard(false);
                if (message != null) SetStatus(message, ThemeHelper.Success);
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Patient Queue Board", DialogOwner());
                ReloadBoard(true);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, errorContext, DialogOwner());
            }
            finally
            {
                _busy = false;
            }
        }

        private void CallNext()
        {
            DoctorOption? doctor = SelectedDoctor;
            if (doctor == null) return;

            Execute("The next patient could not be called.", () =>
            {
                QueueEntryRecord called = _repository.CallNext(doctor.DoctorId);
                return $"Called token {called.TokenText}: {called.PatientName}" + (called.IsEmergency ? " (emergency)" : string.Empty);
            });
        }

        private void SkipCurrent()
        {
            QueueEntryRecord? now = _board?.NowServing;
            if (now == null) return;

            _busy = true;
            string reason;
            try
            {
                using var dialog = new QueueSkipDialog(now);
                if (dialog.ShowDialog(DialogOwner()) != DialogResult.OK) return;
                reason = dialog.Reason;
            }
            finally
            {
                _busy = false;
            }

            Execute("The patient could not be skipped.", () =>
            {
                QueueEntryRecord skipped = _repository.Skip(now.QueueId, reason);
                return $"Token {skipped.TokenText} ({skipped.PatientName}) skipped and returned to the waiting list.";
            });
        }

        private void CompleteCurrent()
        {
            QueueEntryRecord? now = _board?.NowServing;
            if (now == null) return;

            _busy = true;
            bool confirmed;
            try
            {
                confirmed = MessageHelper.Confirm(
                    $"Mark the consultation of token {now.TokenText} ({now.PatientName}) as complete?",
                    "Mark Complete", DialogOwner());
            }
            finally
            {
                _busy = false;
            }
            if (!confirmed) return;

            Execute("The consultation could not be completed.", () =>
            {
                CompleteResult result = _repository.Complete(now.QueueId);
                string summary = $"Token {result.Entry.TokenText} ({result.Entry.PatientName}) completed in {result.MinutesTaken:0.#} min. " +
                                 $"New average: {result.NewAverageMinutes:0.#} min.";
                return summary;
            });
        }

        private void CheckInSelected()
        {
            if (dgvCheckIn.SelectedRows.Count == 0 || dgvCheckIn.SelectedRows[0].DataBoundItem is not CheckInCandidate candidate) return;

            bool emergency = chkEmergency.Checked;
            Execute("The patient could not be checked in.", () =>
            {
                QueueEntryRecord entry = _repository.CheckInAppointment(candidate.AppointmentId, emergency);
                chkEmergency.Checked = false;
                return $"Token {entry.TokenText} ({entry.PatientName}) added to the queue" +
                       (entry.IsEmergency ? " as an EMERGENCY - next to be called." : ".");
            });
        }

        private void AddWalkIn()
        {
            DoctorOption? doctor = SelectedDoctor;
            if (doctor == null) return;

            _busy = true;
            string patientId;
            bool emergency;
            try
            {
                using var dialog = new QueueWalkInDialog(_repository, doctor);
                if (dialog.ShowDialog(DialogOwner()) != DialogResult.OK) return;
                patientId = dialog.PatientId;
                emergency = dialog.IsEmergency;
            }
            finally
            {
                _busy = false;
            }

            Execute("The walk-in patient could not be added.", () =>
            {
                QueueEntryRecord entry = _repository.CheckInWalkIn(patientId, doctor.DoctorId, emergency);
                return $"Token {entry.TokenText} ({entry.PatientName}) added to the queue" +
                       (entry.IsEmergency ? " as an EMERGENCY - next to be called." : " as a walk-in.");
            });
        }
    }
}