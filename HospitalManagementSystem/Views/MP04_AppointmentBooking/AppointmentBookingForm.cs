using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP04_AppointmentBooking;
using HospitalManagementSystem.Repositories.Common;
using HospitalManagementSystem.Repositories.MP04_AppointmentBooking;
using System.Data;
using System.Globalization;
using System.Text;

namespace HospitalManagementSystem.Views.MP04_AppointmentBooking
{
    /// <summary>
    /// MP04 (Parts 8A + 8B) - Appointment Booking and Management. Front-desk staff:
    ///   1. search for a patient and select one ("search then select"),
    ///   2. pick a doctor (optionally filtered by specialization), a date and one of the doctor's OPEN time slots,
    ///   3. choose the visit type, optionally write a reason, and book.
    /// On save the database generates the Appointment ID (APT-YYYYMMDD-XXX) and the next Token Number of that doctor
    /// on that date. The list on the right shows appointments with Date / Doctor / Patient / Status filters.
    ///
    /// Part 8B: select a row in the list and use the buttons below it:
    ///   Reschedule (new open slot of the same doctor), Cancel Appointment (reason required, blocked inside the
    ///   cancellation window), Mark Completed, Mark No-show. Every rule lives in AppointmentRepository.CheckAction;
    ///   the form asks GetBlockReason first so the clerk gets the explanation BEFORE a dialog opens, and the
    ///   repository checks again inside the save transaction.
    ///
    /// The open slots come from AppointmentRepository.GetAvailability: the doctor's weekly blocks for that weekday,
    /// minus leave dates, minus slots already used, minus times that have already passed today.
    /// The form checks the rules for early feedback; AppointmentRepository.Book checks them again inside the save
    /// transaction, which is the authority.
    ///
    /// The layout lives in AppointmentBookingForm.Designer.cs; this file holds behaviour plus the runtime parts of the
    /// redesign (time-slot buttons, patient chip, button kinds).
    /// </summary>
    public partial class AppointmentBookingForm : Views.Main.PageHost
    {
        private readonly AppointmentRepository _repository = new AppointmentRepository();
        private readonly LookupRepository _lookups = new LookupRepository();
        private readonly FormValidator _validator;
        private readonly System.Windows.Forms.Timer _filterTimer = new System.Windows.Forms.Timer { Interval = 300 };

        private string? _selectedPatientId;
        private string _selectedPatientText = string.Empty;
        private DayAvailability? _availability;
        private bool _loading;                       // true while the code (not the user) fills drop-downs
        private bool _suppressPatientSelection;      // true while the code (not the user) changes the patient grid
        private string _lastAction = string.Empty;
        private string _summaryText = string.Empty;

        public AppointmentBookingForm()
        {
            InitializeComponent();
            ThemeHelper.MakeCard(pnlBookingCard);
            ThemeHelper.MakeCard(pnlListCard);
            ThemeHelper.MakeCard(pnlPatientChip, 10, ThemeHelper.AccentLine);
            FormHelper.Prepare(this);        // theme, drop-down lists line up with their boxes, logo as window icon
            ApplyMockupStyles();

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(txtPatientSearch, cboDoctor, dtpDate, cboType);
            cboSlot.SelectedIndexChanged += (s, e) => _validator.ClearError(flpSlots);

            BuildGridColumns();
            GridHelper.Apply(dgvPatients);          // read-only guards + auto-size / wrap
            GridHelper.Apply(dgvAppointments);
            GridHelper.EnableStatusColoring(dgvAppointments, "colStatus");
            dgvPatients.ColumnHeadersVisible = false;   // the mockup shows the matches as a plain list

            ApplyInputRules();
            WireEvents();
            Load += (s, e) => InitializeScreen();
            AppEvents.OnDoctorsChanged(this, ReloadDoctorLists);   // a doctor was added / edited in Doctor Directory
            Disposed += (s, e) => _filterTimer.Dispose();
        }

        /// <summary>
        /// Creates the columns of the patient picker and the appointments list in code (the Designer drops
        /// designer-made grid columns when it re-saves a large file). DataPropertyName = the column names returned
        /// by AppointmentRepository.SearchPatients() and GetAppointments().
        /// </summary>
        private void BuildGridColumns()
        {
            dgvPatients.AutoGenerateColumns = false;
            dgvPatients.Columns.Clear();
            colPCode = GridHelper.AddTextColumn(dgvPatients, "colPCode", "Patient ID", "patient_id", 30);
            colPName = GridHelper.AddTextColumn(dgvPatients, "colPName", "Name", "full_name", 36);
            colPAge = GridHelper.AddTextColumn(dgvPatients, "colPAge", "Age", "age", 10, center: true);
            colPPhone = GridHelper.AddTextColumn(dgvPatients, "colPPhone", "Phone", "phone_text", 24);

            dgvAppointments.AutoGenerateColumns = false;
            dgvAppointments.Columns.Clear();
            // Mockup order: Time, Token, Appointment ID, Patient, Doctor, Reason, Status.
            // Date shows only when the list is not filtered to one day ("Show all"); the visit type stays in the
            // data (and in the booking form) but is not a column in the mockup.
            colDate = GridHelper.AddTextColumn(dgvAppointments, "colDate", "Date", "date_text", 11);
            colTime = GridHelper.AddTextColumn(dgvAppointments, "colTime", "Time", "time_text", 9);
            colToken = GridHelper.AddTextColumn(dgvAppointments, "colToken", "Token", "token_number", 6, center: true);
            colCode = GridHelper.AddTextColumn(dgvAppointments, "colCode", "Appointment ID", "appointment_id", 15);
            colPatient = GridHelper.AddTextColumn(dgvAppointments, "colPatient", "Patient", "patient_name", 16);
            colDoctor = GridHelper.AddTextColumn(dgvAppointments, "colDoctor", "Doctor", "doctor_name", 16);
            colType = GridHelper.AddTextColumn(dgvAppointments, "colType", "Type", "type_name", 11);
            colReason = GridHelper.AddTextColumn(dgvAppointments, "colReason", "Reason", "reason", 16);
            colStatus = GridHelper.AddTextColumn(dgvAppointments, "colStatus", "Status", "status_name", 11);
            colType.Visible = false;
            colPatient.DefaultCellStyle.Font = ThemeHelper.BoldFont;
            colCode.DefaultCellStyle.ForeColor = ThemeHelper.TextMuted;
            colTime.DefaultCellStyle.Font = ThemeHelper.BoldFont;
        }

        /// <summary>Button kinds, box heights, the avatar and the card dividers of the mockup.</summary>
        private void ApplyMockupStyles()
        {
            ThemeHelper.StyleButton(btnPatientSearch, ButtonKind.Soft);
            ThemeHelper.StyleButton(btnBook, ButtonKind.Primary);
            ThemeHelper.StyleButton(btnClearBooking, ButtonKind.Secondary);
            ThemeHelper.StyleButton(btnClearFilters, ButtonKind.Secondary);
            ThemeHelper.StyleButton(btnReschedule, ButtonKind.Secondary);
            ThemeHelper.StyleButton(btnNoShow, ButtonKind.Secondary);
            ThemeHelper.StyleButton(btnComplete, ButtonKind.Success);
            ThemeHelper.StyleButton(btnCancelAppt, ButtonKind.Danger);

            ThemeHelper.SetComboHeight(cboSpecialization, 40);
            ThemeHelper.SetComboHeight(cboDoctor, 40);
            ThemeHelper.SetComboHeight(cboType, 40);
            ThemeHelper.SetComboHeight(cboFilterDoctor, 36);
            ThemeHelper.SetComboHeight(cboFilterStatus, 36);

            ThemeHelper.MakeAvatar(lblPatientAvatar, ThemeHelper.Accent, Color.White);

            // 1 px dividers under the list header and above the action bar.
            pnlListHeader.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(0xE6, 0xEB, 0xF0));
                e.Graphics.DrawLine(pen, 0, pnlListHeader.Height - 1, pnlListHeader.Width, pnlListHeader.Height - 1);
            };
            pnlListFooter.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(0xE6, 0xEB, 0xF0));
                e.Graphics.DrawLine(pen, 0, 0, pnlListFooter.Width, 0);
            };
            pnlListHeader.Resize += (s, e) => pnlListHeader.Invalidate();
            pnlListFooter.Resize += (s, e) => pnlListFooter.Invalidate();

            flpSlots.Resize += (s, e) => SizeSlotButtons();
        }

        private void ApplyInputRules()
        {
            // Letters, digits, space and , . - # / ' ( ) : enough for a name, a patient code or a phone number.
            InputHelper.Attach(txtPatientSearch, InputKind.Address, 60);
            InputHelper.Attach(txtFilterPatient, InputKind.Address, 60);
            InputHelper.Attach(txtReason, InputKind.Address, 250);
        }

        private void WireEvents()
        {
            btnPatientSearch.Click += (s, e) => SearchPatients();
            txtPatientSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                e.Handled = true;
                SearchPatients();
            };
            SearchHelper.AttachLive(txtPatientSearch, () => { if (txtPatientSearch.Text.Trim().Length > 0) SearchPatients(); }, minLength: 2);
            dgvPatients.SelectionChanged += (s, e) => OnPatientSelectionChanged();

            cboSpecialization.SelectedIndexChanged += (s, e) => { if (!_loading) ReloadDoctors(); };
            cboDoctor.SelectedIndexChanged += (s, e) => RefreshSlots();
            cboSlot.SelectedIndexChanged += (s, e) => StyleSlotButtons();
            dtpDate.ValueChanged += (s, e) => RefreshSlots();

            btnBook.Click += (s, e) => Book();
            btnClearBooking.Click += (s, e) => ClearBookingForm();

            // Filters: wait a moment after the last change, then reload once.
            _filterTimer.Tick += (s, e) =>
            {
                _filterTimer.Stop();
                ReloadGrid(null);
            };
            dtpFilterDate.ValueChanged += (s, e) => OnFilterChanged();
            cboFilterDoctor.SelectedIndexChanged += (s, e) => OnFilterChanged();
            cboFilterStatus.SelectedIndexChanged += (s, e) => OnFilterChanged();
            txtFilterPatient.TextChanged += (s, e) => OnFilterChanged();
            btnClearFilters.Click += (s, e) => ShowAllAppointments();

            // Part 8B: actions on the selected appointment.
            dgvAppointments.SelectionChanged += (s, e) => UpdateActionButtons();
            btnReschedule.Click += (s, e) => RunAction(AppointmentAction.Reschedule);
            btnCancelAppt.Click += (s, e) => RunAction(AppointmentAction.Cancel);
            btnComplete.Click += (s, e) => RunAction(AppointmentAction.Complete);
            btnNoShow.Click += (s, e) => RunAction(AppointmentAction.NoShow);
        }

        // ------------------------------------------------------------------
        // Start-up
        // ------------------------------------------------------------------

        private void InitializeScreen()
        {
            _loading = true;
            try
            {
                var specializations = new List<LookupItem> { LookupItem.Placeholder("All specializations") };
                specializations.AddRange(_lookups.GetSpecializations());
                cboSpecialization.DataSource = specializations;
                cboSpecialization.DisplayMember = nameof(LookupItem.Name);
                cboSpecialization.ValueMember = nameof(LookupItem.Id);
                cboSpecialization.SelectedIndex = 0;

                cboType.DataSource = _repository.GetAppointmentTypes();
                cboType.DisplayMember = nameof(LookupItem.Name);
                cboType.ValueMember = nameof(LookupItem.Id);
                cboType.SelectedIndex = cboType.Items.Count > 0 ? 0 : -1;

                // No past dates and not more than a year ahead.
                dtpDate.MinDate = DateTime.Today;
                dtpDate.MaxDate = DateTime.Today.AddDays(AppointmentRepository.MaxAdvanceDays);
                dtpDate.Value = DateTime.Today;

                // Filters: every doctor / every status, today's date.
                var doctorFilter = new List<LookupItem> { LookupItem.Placeholder("All doctors") };
                doctorFilter.AddRange(_repository.GetDoctors(null)
                    .Select(d => new LookupItem { Id = d.DoctorId, Name = d.DisplayName }));
                cboFilterDoctor.DataSource = doctorFilter;
                cboFilterDoctor.DisplayMember = nameof(LookupItem.Name);
                cboFilterDoctor.ValueMember = nameof(LookupItem.Id);
                cboFilterDoctor.SelectedIndex = 0;

                var statusFilter = new List<LookupItem> { LookupItem.Placeholder("All statuses") };
                statusFilter.AddRange(_repository.GetStatuses());
                cboFilterStatus.DataSource = statusFilter;
                cboFilterStatus.DisplayMember = nameof(LookupItem.Name);
                cboFilterStatus.ValueMember = nameof(LookupItem.Id);
                cboFilterStatus.SelectedIndex = 0;

                dtpFilterDate.Value = DateTime.Today;
                dtpFilterDate.Checked = true;
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The appointment screen could not be loaded.", this);
                return;
            }
            finally
            {
                _loading = false;
            }

            ReloadDoctors();
            ReloadGrid(null);
            SetPatientLabel("No patient selected yet", ThemeHelper.TextMuted);
        }

        // ------------------------------------------------------------------
        // Step 1 - patient picker
        // ------------------------------------------------------------------

        private void SearchPatients()
        {
            _validator.Begin();

            string text = txtPatientSearch.Text.Trim();
            if (text.Length == 0)
            {
                _validator.AddError(txtPatientSearch, "Type part of the patient's name, patient code or phone number, then press Search.");
                txtPatientSearch.Focus();
                return;
            }

            try
            {
                DataTable table = _repository.SearchPatients(text);
                BindPatients(table);
                dgvPatients.Visible = table.Rows.Count > 1;   // the result list only appears when there is a choice

                if (table.Rows.Count == 0)
                {
                    SetPatientLabel("No patient found. Register the patient first under Patient Registration.", ThemeHelper.Warning);
                }
                else if (table.Rows.Count == 1)
                {
                    SelectPatient(table.Rows[0]);             // exactly one match: select it for the clerk
                }
                else
                {
                    SetPatientLabel($"{table.Rows.Count} patients found - click one to select.", ThemeHelper.TextMuted);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The patient search failed.", this);
            }
        }

        /// <summary>Shows search results with nothing selected (the grid would otherwise highlight the first row).</summary>
        private void BindPatients(DataTable? table)
        {
            _suppressPatientSelection = true;
            try
            {
                dgvPatients.DataSource = table;
                if (table == null) dgvPatients.Visible = false;
                dgvPatients.CurrentCell = null;
                dgvPatients.ClearSelection();
            }
            finally
            {
                _suppressPatientSelection = false;
            }

            _selectedPatientId = null;
            _selectedPatientText = string.Empty;
            UpdateSlotInfo();
        }

        private void OnPatientSelectionChanged()
        {
            if (_suppressPatientSelection) return;

            if (dgvPatients.SelectedRows.Count == 0 || dgvPatients.SelectedRows[0].DataBoundItem is not DataRowView view)
            {
                _selectedPatientId = null;
                _selectedPatientText = string.Empty;
                SetPatientLabel("No patient selected yet", ThemeHelper.TextMuted);
                UpdateSlotInfo();
                return;
            }

            SelectPatient(view.Row);
        }

        /// <summary>Makes one search result the patient of the booking and shows the patient chip.</summary>
        private void SelectPatient(DataRow row)
        {
            string name = DatabaseHelper.GetString(row, "full_name");
            string code = DatabaseHelper.GetString(row, "patient_id");
            string phone = DatabaseHelper.GetString(row, "phone_text");

            _selectedPatientId = code;
            _selectedPatientText = $"{name} ({code})";

            // Patient chip of the mockup: initials, name, "ID · age · phone".
            lblPatientAvatar.Text = Initials(name);
            lblPatientName.Text = name;
            var detail = new StringBuilder($"{code}  ·  {DatabaseHelper.GetInt(row, "age")} yrs");
            if (phone.Length > 0) detail.Append("  ·  " + phone);
            lblPatientDetail.Text = detail.ToString();
            lblSelectedPatient.Visible = false;
            pnlPatientChip.Visible = true;
            lblPatientAvatar.Invalidate();

            _validator.ClearError(txtPatientSearch);
            UpdateSlotInfo();      // the "already booked with this doctor today" hint depends on the patient
        }

        private static string Initials(string name)
        {
            // The search returns "Last, First M." - put it back in "First Last" order for the initials.
            int comma = name.IndexOf(',');
            if (comma > 0) name = name.Substring(comma + 1).Trim() + " " + name.Substring(0, comma).Trim();
            string[] parts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 2 && parts[^2].EndsWith(".")) parts = parts.Where(x => !x.EndsWith(".")).ToArray();
            if (parts.Length == 0) return "?";
            string first = parts[0].Substring(0, 1);
            string last = parts.Length > 1 ? parts[^1].Substring(0, 1) : string.Empty;
            return (first + last).ToUpperInvariant();
        }

        /// <summary>A message instead of the patient chip (nothing selected, nothing found, several found).</summary>
        private void SetPatientLabel(string text, Color color)
        {
            lblSelectedPatient.Text = text;
            lblSelectedPatient.ForeColor = color;
            lblSelectedPatient.Visible = true;
            pnlPatientChip.Visible = false;
        }

        /// <summary>
        /// Reloads the specialization list, the doctor filter and the doctor drop-down after a doctor changed in MP03,
        /// keeping what was selected, so this tab shows the same names and specializations as the Doctor Directory.
        /// </summary>
        private void ReloadDoctorLists()
        {
            if (cboSpecialization.DataSource == null) return;   // not initialized yet
            string? keepSpec = cboSpecialization.SelectedItem is LookupItem { HasValue: true } s ? s.Id : null;
            string? keepFilter = cboFilterDoctor.SelectedItem is LookupItem { HasValue: true } f ? f.Id : null;

            _loading = true;
            try
            {
                var specializations = new List<LookupItem> { LookupItem.Placeholder("All specializations") };
                specializations.AddRange(_lookups.GetSpecializations());
                cboSpecialization.DataSource = specializations;
                int specIndex = keepSpec == null ? 0 : specializations.FindIndex(x => x.HasValue && string.Equals(x.Id, keepSpec, StringComparison.OrdinalIgnoreCase));
                cboSpecialization.SelectedIndex = specIndex < 0 ? 0 : specIndex;

                var doctorFilter = new List<LookupItem> { LookupItem.Placeholder("All doctors") };
                doctorFilter.AddRange(_repository.GetDoctors(null).Select(d => new LookupItem { Id = d.DoctorId, Name = d.DisplayName }));
                cboFilterDoctor.DataSource = doctorFilter;
                int filterIndex = keepFilter == null ? 0 : doctorFilter.FindIndex(x => x.Id == keepFilter);
                cboFilterDoctor.SelectedIndex = filterIndex < 0 ? 0 : filterIndex;
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The doctor lists could not be refreshed.", this);
            }
            finally
            {
                _loading = false;
            }
            ReloadDoctors();
            ReloadGrid(null);
        }

        // ------------------------------------------------------------------
        // Step 2 - doctor, date, slots
        // ------------------------------------------------------------------

        /// <summary>Fills the doctor drop-down for the chosen specialization and keeps the current doctor when still listed.</summary>
        private void ReloadDoctors()
        {
            string? keepDoctorId = (cboDoctor.SelectedItem as DoctorOption)?.DoctorId;
            string? specialization = cboSpecialization.SelectedItem is LookupItem { HasValue: true } spec ? spec.Id : null;

            _loading = true;
            try
            {
                List<DoctorOption> doctors = _repository.GetDoctors(specialization);
                cboDoctor.DataSource = doctors;
                cboDoctor.DisplayMember = nameof(DoctorOption.Display);
                cboDoctor.SelectedIndex = keepDoctorId != null ? doctors.FindIndex(d => d.DoctorId == keepDoctorId) : -1;
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The doctor list could not be loaded.", this);
            }
            finally
            {
                _loading = false;
            }

            RefreshSlots();
        }

        /// <summary>Recomputes the open time slots for the chosen doctor and date.</summary>
        private void RefreshSlots()
        {
            if (_loading) return;

            _availability = null;
            var open = new List<SlotInfo>();

            if (cboDoctor.SelectedItem is DoctorOption doctor)
            {
                try
                {
                    _availability = _repository.GetAvailability(doctor.DoctorId, dtpDate.Value.Date);
                    open = _availability.Slots.Where(s => s.IsBookable).ToList();
                }
                catch (Exception ex)
                {
                    MessageHelper.Error(ex, "The time slots could not be loaded.", this);
                }
            }

            cboSlot.DataSource = open;
            cboSlot.DisplayMember = nameof(SlotInfo.Text);
            cboSlot.SelectedIndex = -1;           // the clerk chooses; nothing is pre-selected

            BuildSlotButtons();
            UpdateSlotInfo();
        }

        // ------------------------------------------------------------------
        // Slot buttons (the mockup's "Time slot" grid). cboSlot (hidden) stays the selected value, so booking,
        // validation and the repository calls are exactly as before.
        // ------------------------------------------------------------------

        private const int SlotColumns = 5;
        private const int SlotHeight = 34;
        private const int SlotGap = 6;
        private const int SlotVisibleRows = 4;
        private readonly List<Button> _slotButtons = new List<Button>();
        private static readonly Font SlotFont = new Font("Segoe UI", 9f, FontStyle.Regular);

        private void BuildSlotButtons()
        {
            flpSlots.SuspendLayout();
            foreach (Button old in _slotButtons) old.Dispose();
            _slotButtons.Clear();

            DayAvailability? a = _availability;
            bool showSlots = a != null && !a.OnLeave && a.WorksThisDay;
            if (showSlots)
            {
                foreach (SlotInfo slot in a!.Slots.Where(x => x.IsBookable))   // the mockup lists the open slots
                {
                    var button = new Button
                    {
                        Text = slot.Text,
                        Tag = slot,
                        Font = SlotFont,
                        Margin = new Padding(0)
                    };
                    button.Click += (s, e) => cboSlot.SelectedItem = slot;
                    flpSlots.Controls.Add(button);
                    _slotButtons.Add(button);
                }
            }

            lblSlotCount.Text = showSlots ? $"·  {a!.OpenCount} open, {a.TakenCount} taken" : string.Empty;
            SizeSlotButtons();
            StyleSlotButtons();
            flpSlots.ResumeLayout(true);
        }

        /// <summary>Five equal columns, 34 px rows, 6 px gaps; at most four rows before the grid scrolls.</summary>
        private void SizeSlotButtons()
        {
            if (_slotButtons.Count == 0)
            {
                flpSlots.Height = 0;
                flpSlots.Visible = false;
                return;
            }

            float scale = DeviceDpi / 96f;
            int gap = (int)Math.Round(SlotGap * scale);
            int height = (int)Math.Round(SlotHeight * scale);
            int rows = (_slotButtons.Count + SlotColumns - 1) / SlotColumns;
            int width = flpSlots.Width - (rows > SlotVisibleRows ? SystemInformation.VerticalScrollBarWidth : 0);
            int buttonWidth = Math.Max(40, (width - gap * (SlotColumns - 1)) / SlotColumns);

            for (int i = 0; i < _slotButtons.Count; i++)
            {
                Button button = _slotButtons[i];
                button.Size = new Size(buttonWidth, height);
                bool lastColumn = i % SlotColumns == SlotColumns - 1;
                button.Margin = new Padding(0, 0, lastColumn ? 0 : gap, gap);
            }

            flpSlots.Visible = true;
            flpSlots.Height = Math.Min(rows, SlotVisibleRows) * (height + gap);
        }

        /// <summary>The chosen slot is the solid teal button; the others are outlined.</summary>
        private void StyleSlotButtons()
        {
            object? chosen = cboSlot.SelectedItem;
            foreach (Button button in _slotButtons)
            {
                ThemeHelper.StyleButton(button, ReferenceEquals(button.Tag, chosen) ? ButtonKind.Primary : ButtonKind.Secondary);
            }
        }

        /// <summary>The hint under the form: working hours, open / taken counts, leave, or the "already booked" warning.</summary>
        private void UpdateSlotInfo()
        {
            if (cboDoctor.SelectedItem is not DoctorOption doctor)
            {
                SetSlotInfo("Choose a doctor and a date to see the open time slots.", warning: false);
                return;
            }
            if (_availability == null)
            {
                SetSlotInfo(string.Empty, warning: false);
                return;
            }

            DayAvailability a = _availability;
            string dayText = a.Date.ToString("dddd, MMM d, yyyy", CultureInfo.InvariantCulture);
            var text = new StringBuilder();
            bool warning;

            // the doctor's calendar: clinic days, the next leave dates and the next open clinic day
            List<(DateTime Date, string Reason)> leaves = new List<(DateTime, string)>();
            DateTime? nextClinic = null;
            try
            {
                (leaves, nextClinic) = _repository.GetDoctorCalendar(doctor.DoctorId, a.Date.AddDays(1));
            }
            catch
            {
                // the hint is optional; booking is checked again when saving
            }
            string nextText = nextClinic.HasValue
                ? "  Next clinic day: " + nextClinic.Value.ToString("ddd, MMM d", CultureInfo.InvariantCulture) + "."
                : string.Empty;

            if (a.OnLeave)
            {
                text.Append($"{doctor.DisplayName} is on leave on {dayText} ({a.LeaveReason}). Choose another date.{nextText}");
                warning = true;
            }
            else if (!a.WorksThisDay)
            {
                text.Append(a.WorkingDaysText.Length == 0
                    ? $"{doctor.DisplayName} has no weekly schedule yet. Add one under Doctor Directory & Schedule."
                    : $"{doctor.DisplayName} does not see patients on {a.Date.DayOfWeek}s. Working days: {a.WorkingDaysText}.{nextText}");
                warning = true;
            }
            else
            {
                // Open / taken counts are in the "Time slot" heading; the buttons are the open slots.
                text.Append($"Working hours {a.WorkingHoursText}  ·  {a.Minutes}-minute visits");
                if (a.PastCount > 0) text.Append($"  ·  {a.PastCount} already passed");
                if (a.OpenCount == 0) text.Append("\nFully booked - no open slot left on this date." + nextText);
                warning = a.OpenCount == 0;
            }

            if (a.WorkingDaysText.Length > 0)
            {
                text.Append($"\nClinic days: {a.WorkingDaysText}");
                var otherLeaves = leaves.Where(l => l.Date != a.Date.Date).ToList();
                text.Append(otherLeaves.Count == 0
                    ? "  ·  no leave scheduled"
                    : "  ·  On leave: " + string.Join(", ", otherLeaves.Select(l => l.Date.ToString("MMM d", CultureInfo.InvariantCulture))));
            }

            if (_selectedPatientId is string patientId && !a.OnLeave)
            {
                try
                {
                    if (_repository.PatientHasActiveAppointment(patientId, doctor.DoctorId, a.Date))
                    {
                        text.Append($"\nThis patient already has an appointment with {doctor.DisplayName} on this date.");
                        warning = true;
                    }
                }
                catch
                {
                    // The hint is optional; Book() checks the rule again when saving.
                }
            }

            SetSlotInfo(text.ToString(), warning);
        }

        private void SetSlotInfo(string text, bool warning)
        {
            lblSlotInfo.Text = text;
            lblSlotInfo.ForeColor = warning ? ThemeHelper.Warning : ThemeHelper.TextMuted;
        }

        // ------------------------------------------------------------------
        // Step 3 - book
        // ------------------------------------------------------------------

        private void Book()
        {
            BookingRequest? request = BuildValidRequest();
            if (request == null) return;

            var doctor = (DoctorOption)cboDoctor.SelectedItem!;
            var slot = (SlotInfo)cboSlot.SelectedItem!;
            string patientText = _selectedPatientText;

            try
            {
                BookingResult result = _repository.Book(request);

                string dayText = request.Date.ToString("dddd, MMM d, yyyy", CultureInfo.InvariantCulture);
                MessageHelper.Success(
                    $"Appointment booked.\n\n" +
                    $"Appointment ID:  {result.AppointmentId}\n" +
                    $"Token number:  {result.TokenNumber}\n" +
                    $"Patient:  {patientText}\n" +
                    $"Doctor:  {doctor.DisplayName}\n" +
                    $"When:  {dayText}, {slot.Text}",
                    "Appointment booked", this);

                _lastAction = $"Booked {result.AppointmentId} (token {result.TokenNumber}) for {patientText}";
                ResetAfterBooking(request.Date, result.AppointmentId);
            }
            catch (InvalidOperationException ex)
            {
                // A booking rule failed (slot just taken, leave added meanwhile, ...): explain, then show the fresh slots.
                MessageHelper.Warning(ex.Message, "This appointment cannot be booked", this);
                RefreshSlots();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The appointment could not be saved.", this);
            }
        }

        /// <summary>Checks the form (inline red marks + one list). Returns the request, or null when something is wrong.</summary>
        private BookingRequest? BuildValidRequest()
        {
            _validator.Begin();

            var doctor = cboDoctor.SelectedItem as DoctorOption;
            var slot = cboSlot.SelectedItem as SlotInfo;
            var type = cboType.SelectedItem as LookupItem;
            DateTime date = dtpDate.Value.Date;

            _validator.Check(_selectedPatientId != null, txtPatientSearch, "Search for a patient and select one from the list.");
            _validator.Check(doctor != null, cboDoctor, "Choose a doctor.");
            _validator.Check(date >= DateTime.Today, dtpDate, "The appointment date cannot be in the past.");

            bool noOpenSlots = _availability != null && _availability.OpenCount == 0;
            _validator.Check(doctor == null || slot != null, flpSlots,
                noOpenSlots
                    ? "This doctor has no open time slot on this date. Choose another date or another doctor."
                    : "Choose an available time slot.");
            _validator.Check(type != null, cboType, "Choose the visit type.");

            if (_validator.IsValid && doctor != null && _selectedPatientId is string patientId)
            {
                try
                {
                    _validator.Check(!_repository.PatientHasActiveAppointment(patientId, doctor.DoctorId, date), cboDoctor,
                        $"This patient already has an appointment with {doctor.DisplayName} on that day.");
                }
                catch (Exception ex)
                {
                    MessageHelper.Error(ex, "The booking could not be checked.", this);
                    return null;
                }
            }

            if (!_validator.IsValid)
            {
                _validator.ShowErrors(this);
                return null;
            }

            return new BookingRequest
            {
                PatientId = _selectedPatientId!,
                DoctorId = doctor!.DoctorId,
                Date = date,
                TimeSlot = slot!.Time,
                AppointmentType = type!.Id,
                Reason = txtReason.Text.Trim()
            };
        }

        /// <summary>After a successful save: clear the patient and reason, keep doctor and date for the next booking.</summary>
        private void ResetAfterBooking(DateTime bookedDate, string newAppointmentId)
        {
            _validator.Begin();

            _loading = true;
            try
            {
                txtPatientSearch.Clear();
                txtReason.Clear();
                cboType.SelectedIndex = cboType.Items.Count > 0 ? 0 : -1;

                // Make sure the new appointment is visible in the list on the right.
                dtpFilterDate.Value = bookedDate;
                dtpFilterDate.Checked = true;
                cboFilterDoctor.SelectedIndex = 0;
                cboFilterStatus.SelectedIndex = 0;
                txtFilterPatient.Clear();
            }
            finally
            {
                _loading = false;
            }

            BindPatients(null);
            SetPatientLabel("No patient selected yet", ThemeHelper.TextMuted);
            RefreshSlots();
            ReloadGrid(newAppointmentId);
        }

        private void ClearBookingForm()
        {
            _validator.Begin();

            _loading = true;
            try
            {
                txtPatientSearch.Clear();
                txtReason.Clear();
                cboSpecialization.SelectedIndex = 0;
                cboType.SelectedIndex = cboType.Items.Count > 0 ? 0 : -1;
                dtpDate.Value = DateTime.Today;
            }
            finally
            {
                _loading = false;
            }

            BindPatients(null);
            SetPatientLabel("No patient selected yet", ThemeHelper.TextMuted);
            ReloadDoctors();       // also refreshes the slot list (no doctor selected)
        }

        // ------------------------------------------------------------------
        // Appointments list
        // ------------------------------------------------------------------

        private void OnFilterChanged()
        {
            if (_loading) return;
            _filterTimer.Stop();
            _filterTimer.Start();
        }

        /// <summary>"Show All": no date, doctor, patient or status filter.</summary>
        private void ShowAllAppointments()
        {
            _loading = true;
            try
            {
                dtpFilterDate.Checked = false;
                cboFilterDoctor.SelectedIndex = 0;
                cboFilterStatus.SelectedIndex = 0;
                txtFilterPatient.Clear();
            }
            finally
            {
                _loading = false;
            }
            ReloadGrid(null);
        }

        private AppointmentFilter ReadFilter()
        {
            return new AppointmentFilter
            {
                Date = dtpFilterDate.Checked ? dtpFilterDate.Value.Date : null,
                DoctorId = cboFilterDoctor.SelectedItem is LookupItem { HasValue: true } doctor ? doctor.Id : null,
                Status = cboFilterStatus.SelectedItem is LookupItem { HasValue: true } status ? status.Id : null,
                PatientText = txtFilterPatient.Text.Trim()
            };
        }

        /// <summary>Reloads the grid from the current filters; optionally keeps one appointment selected.</summary>
        private void ReloadGrid(string? selectAppointmentId)
        {
            try
            {
                DataTable table = _repository.GetAppointments(ReadFilter());
                dgvAppointments.DataSource = table;
                dgvAppointments.ClearSelection();
                lblListTitle.Text = $"Appointments  ·  {table.Rows.Count}";
                colDate.Visible = !dtpFilterDate.Checked;      // one day shown: the date is in the filter

                if (selectAppointmentId != null) SelectAppointmentRow(selectAppointmentId);
                UpdateSummary(table);
                UpdateActionButtons();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The appointment list could not be loaded.", this);
            }
        }

        private void SelectAppointmentRow(string appointmentId)
        {
            foreach (DataGridViewRow row in dgvAppointments.Rows)
            {
                if (row.DataBoundItem is DataRowView view && Convert.ToString(view.Row["appointment_id"]) == appointmentId)
                {
                    dgvAppointments.CurrentCell = GridHelper.FirstVisibleCell(row);
                    row.Selected = true;
                    return;
                }
            }
        }

        // ------------------------------------------------------------------
        // Part 8B - Reschedule / Cancel / Mark Completed / Mark No-show
        // ------------------------------------------------------------------

        private IWin32Window DialogOwner() => (IWin32Window?)TopLevelControl ?? this;

        /// <summary>The DataRow of the selected list row (a real selection, not just the current cell), or null.</summary>
        private DataRow? GetSelectedAppointmentRow()
        {
            return dgvAppointments.SelectedRows.Count > 0 && dgvAppointments.SelectedRows[0].DataBoundItem is DataRowView view
                ? view.Row
                : null;
        }

        /// <summary>
        /// The four buttons are available whenever a row is selected. A row that is not Booked is still selectable on
        /// purpose: pressing a button then explains WHY it is not allowed (for example "Completed is final").
        /// </summary>
        private void UpdateActionButtons()
        {
            bool hasSelection = GetSelectedAppointmentRow() != null;
            btnReschedule.Enabled = hasSelection;
            btnCancelAppt.Enabled = hasSelection;
            btnComplete.Enabled = hasSelection;
            btnNoShow.Enabled = hasSelection;
            ShowSummaryOrSelection();
        }

        /// <summary>Footer text: "Selected: APT-... · patient" when a row is selected, otherwise the counts.</summary>
        private void ShowSummaryOrSelection()
        {
            DataRow? row = GetSelectedAppointmentRow();
            if (row != null)
            {
                lblSummary.Text = $"Selected:  {DatabaseHelper.GetString(row, "appointment_id")}  ·  {DatabaseHelper.GetString(row, "patient_name")}";
            }
            else
            {
                lblSummary.Text = _summaryText;
            }
        }

        private static string ActionTitle(AppointmentAction action) => action switch
        {
            AppointmentAction.Cancel => "Cancel appointment",
            AppointmentAction.Reschedule => "Reschedule appointment",
            AppointmentAction.Complete => "Mark as Completed",
            _ => "Mark as No-show"
        };

        /// <summary>Common flow: find the row, load it fresh, ask the rules, then run the chosen action.</summary>
        private void RunAction(AppointmentAction action)
        {
            DataRow? row = GetSelectedAppointmentRow();
            if (row == null)
            {
                MessageHelper.Warning("Select an appointment in the list first.", ActionTitle(action), this);
                return;
            }

            string appointmentId = DatabaseHelper.GetString(row, "appointment_id");
            string title = ActionTitle(action);

            try
            {
                AppointmentRecord? appointment = _repository.GetAppointment(appointmentId);
                if (appointment == null)
                {
                    MessageHelper.Warning("This appointment no longer exists. The list will be refreshed.", title, this);
                    ReloadGrid(null);
                    return;
                }

                // Explain first, open the dialog second.
                string? blocked = _repository.GetBlockReason(appointmentId, action);
                if (blocked != null)
                {
                    MessageHelper.Warning(blocked, title + " - not possible", this);
                    ReloadGrid(appointmentId);        // the list may be out of date (someone else changed it)
                    return;
                }

                switch (action)
                {
                    case AppointmentAction.Cancel: CancelAppointment(appointment); break;
                    case AppointmentAction.Reschedule: RescheduleAppointment(appointment); break;
                    case AppointmentAction.Complete: CompleteAppointment(appointment); break;
                    default: MarkNoShow(appointment); break;
                }
            }
            catch (InvalidOperationException ex)
            {
                // A rule failed inside the save transaction (someone else was faster): explain, then show fresh data.
                MessageHelper.Warning(ex.Message, title + " - not possible", this);
                ReloadGrid(appointmentId);
                RefreshSlots();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The appointment could not be updated.", this);
            }
        }

        private void CancelAppointment(AppointmentRecord appointment)
        {
            using var dialog = new AppointmentCancelDialog(appointment);
            if (dialog.ShowDialog(DialogOwner()) != DialogResult.OK) return;

            _repository.Cancel(appointment.AppointmentId, dialog.Reason);

            _lastAction = $"Cancelled {appointment.AppointmentId}";
            ReloadGrid(appointment.AppointmentId);
            RefreshSlots();           // the freed slot is offered again in the booking form

            MessageHelper.Success(
                $"Appointment {appointment.AppointmentId} was cancelled.\n\n" +
                $"The {appointment.WhenText} slot of {appointment.DoctorName} is free again.",
                "Appointment cancelled", this);
        }

        private void RescheduleAppointment(AppointmentRecord appointment)
        {
            using var dialog = new AppointmentRescheduleDialog(_repository, appointment);
            if (dialog.ShowDialog(DialogOwner()) != DialogResult.OK) return;

            RescheduleResult result = _repository.Reschedule(appointment.AppointmentId, dialog.NewDate, dialog.NewTimeSlot);

            _lastAction = $"Rescheduled {result.AppointmentId} to {result.NewWhenText}";
            ShowDateInList(dialog.NewDate);
            ReloadGrid(appointment.AppointmentId);
            RefreshSlots();

            string tokenText = result.TokenChanged
                ? $"\nNew token number:  {result.NewToken} (the date changed)"
                : $"\nToken number:  {result.NewToken} (unchanged)";
            MessageHelper.Success(
                $"Appointment {result.AppointmentId} was moved.\n\n" +
                $"From:  {result.OldWhenText}\n" +
                $"To:  {result.NewWhenText}" + tokenText + "\n\n" +
                "The old time slot is free again.",
                "Appointment rescheduled", this);
        }

        private void CompleteAppointment(AppointmentRecord appointment)
        {
            if (!MessageHelper.Confirm(
                    $"Mark {appointment.AppointmentId} ({appointment.PatientName}, {appointment.WhenText}) as Completed?\n\n" +
                    "A completed appointment can no longer be cancelled, rescheduled or marked No-show.",
                    "Mark as Completed", this))
                return;

            _repository.MarkCompleted(appointment.AppointmentId);

            _lastAction = $"Completed {appointment.AppointmentId}";
            ReloadGrid(appointment.AppointmentId);
        }

        private void MarkNoShow(AppointmentRecord appointment)
        {
            if (!MessageHelper.ConfirmDestructive(
                    $"Mark {appointment.AppointmentId} ({appointment.PatientName}, {appointment.WhenText}) as No-show?\n\n" +
                    "The patient did not come. This cannot be undone and the time slot stays used.",
                    "Mark as No-show", this))
                return;

            _repository.MarkNoShow(appointment.AppointmentId);

            _lastAction = $"No-show {appointment.AppointmentId}";
            ReloadGrid(appointment.AppointmentId);
        }

        /// <summary>Makes the list show one date with every other filter cleared, so a moved appointment stays visible.</summary>
        private void ShowDateInList(DateTime date)
        {
            _loading = true;
            try
            {
                dtpFilterDate.Value = date < dtpFilterDate.MinDate ? dtpFilterDate.MinDate : date;
                dtpFilterDate.Checked = true;
                cboFilterDoctor.SelectedIndex = 0;
                cboFilterStatus.SelectedIndex = 0;
                txtFilterPatient.Clear();
            }
            finally
            {
                _loading = false;
            }
        }

        private void UpdateSummary(DataTable table)
        {
            var summary = new StringBuilder();

            if (table.Rows.Count == 0)
            {
                summary.Append("No appointments match the filters.");
            }
            else
            {
                int Count(string status) => table.Rows.Cast<DataRow>()
                    .Count(r => string.Equals(DatabaseHelper.GetString(r, "status_name"), status, StringComparison.OrdinalIgnoreCase));

                summary.Append($"{table.Rows.Count} shown  ·  Booked {Count("Booked")}  ·  " +
                               $"Completed {Count("Completed")}  ·  Cancelled {Count("Cancelled")}  ·  No-show {Count("No-show")}");
            }

            if (_lastAction.Length > 0) summary.Append("   ·   " + _lastAction);
            _summaryText = summary.ToString();
            ShowSummaryOrSelection();
        }
    }
}