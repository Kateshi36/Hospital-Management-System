using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP03_DoctorScheduling;
using HospitalManagementSystem.Repositories.MP03_DoctorScheduling;
using System.Data;

namespace HospitalManagementSystem.Views.MP03_DoctorScheduling
{
    /// <summary>
    /// MP03 (Part 7B) - Weekly Schedule Builder for ONE doctor, opened from the Doctor Directory.
    /// Staff add weekly availability blocks (day, start, end, minutes per visit, notes), see them in a
    /// 7-column Monday-Sunday grid, and mark single calendar dates as leave.
    ///
    /// Overlap rule: two blocks of the same doctor on the same day overlap when each one starts before the
    /// other ends (new.Start &lt; old.End AND new.End &gt; old.Start). Blocks that only touch (12:00 / 12:00) are allowed.
    /// The form checks the rules for early feedback; DoctorScheduleRepository checks them again inside the save
    /// transaction, which is the authority.
    ///
    /// All visuals live in ScheduleBuilderForm.Designer.cs; this file only holds behaviour.
    /// </summary>
    public partial class ScheduleBuilderForm : Views.Main.PageHost
    {
        private readonly DoctorScheduleRepository _repository = new DoctorScheduleRepository();
        private readonly FormValidator _validator;

        private readonly string _doctorId;
        private readonly string _doctorName;

        private List<ScheduleBlock> _blocks = new List<ScheduleBlock>();
        private int? _editingScheduleId;       // null = adding a new block
        private int _leaveCount;
        private string _lastAction = string.Empty;
        private bool _suppressSelection;       // true while the code (not the user) changes a grid selection

        public ScheduleBuilderForm(string doctorId, string doctorName)
        {
            InitializeComponent();
            Load += (s, e) => Views.Main.PageHost.ApplyMinimum(this);   // scroll instead of squeezing on small screens
            FormHelper.Prepare(this);        // drop-down lists line up with their boxes; logo as window icon

            _doctorId = doctorId;
            _doctorName = doctorName;

            Text = $"Weekly Schedule - {doctorName}";
            lblDoctorInfo.Text = $"{doctorId}   |   {doctorName}";

            _validator = new FormValidator(errorProvider);
            _validator.AutoClear(cboDay, dtpStart, dtpEnd, txtMinutes, dtpLeave, txtLeaveReason);

            BuildGridColumns();
            GridHelper.Apply(dgvBlocks);    // read-only guards + auto-size / wrap
            GridHelper.Apply(dgvLeaves);
            GridHelper.Apply(dgvWeek);
            dgvWeek.SelectionMode = DataGridViewSelectionMode.CellSelect;   // the weekly grid is a picture of blocks, not a list of rows

            ApplyInputRules();
            WireEvents();
            Load += (s, e) => InitializeScreen();
        }

        /// <summary>
        /// Creates the columns of the blocks list and the leave list in code (the Designer drops designer-made grid
        /// columns when it re-saves a large file). The weekly Mon-Sun grid keeps its 7 Designer columns.
        /// </summary>
        private void BuildGridColumns()
        {
            dgvBlocks.AutoGenerateColumns = false;
            dgvBlocks.Columns.Clear();
            colBDay = GridHelper.AddTextColumn(dgvBlocks, "colBDay", "Day", "day_name", 14);
            colBStart = GridHelper.AddTextColumn(dgvBlocks, "colBStart", "Start", "start_text", 12);
            colBEnd = GridHelper.AddTextColumn(dgvBlocks, "colBEnd", "End", "end_text", 12);
            colBHours = GridHelper.AddTextColumn(dgvBlocks, "colBHours", "Hours", "hours", 8, alignRight: true, format: "0.##");
            colBMinutes = GridHelper.AddTextColumn(dgvBlocks, "colBMinutes", "Min / Visit", "minutes", 10, alignRight: true);
            colBSlots = GridHelper.AddTextColumn(dgvBlocks, "colBSlots", "Visits", "slots", 8, alignRight: true);
            colBNotes = GridHelper.AddTextColumn(dgvBlocks, "colBNotes", "Notes", "notes", 26);

            dgvLeaves.AutoGenerateColumns = false;
            dgvLeaves.Columns.Clear();
            colLDate = GridHelper.AddTextColumn(dgvLeaves, "colLDate", "Date", "leave_date", 22, format: "MMM d, yyyy");
            colLDay = GridHelper.AddTextColumn(dgvLeaves, "colLDay", "Day", "day_name", 16);
            colLReason = GridHelper.AddTextColumn(dgvLeaves, "colLReason", "Reason", "reason", 42);
            colLStatus = GridHelper.AddTextColumn(dgvLeaves, "colLStatus", "Status", "leave_status", 14);
        }

        /// <summary>The day picked in the day drop-down (0 = Sunday ... 6 = Saturday), or null.</summary>
        private int? SelectedDay() =>
            cboDay.SelectedItem is LookupItem { HasValue: true } day ? int.Parse(day.Id) : null;

        private void ApplyInputRules()
        {
            InputHelper.Attach(txtMinutes, InputKind.Digits, 3);         // whole minutes only
            InputHelper.Attach(txtNotes, InputKind.Address, 200);        // letters, digits and , . - # / ' ( )
            InputHelper.Attach(txtLeaveReason, InputKind.Address, 200);
        }

        private void WireEvents()
        {
            btnSaveBlock.Click += (s, e) => SaveBlock();
            btnNewBlock.Click += (s, e) => StartNewBlock();
            btnDeleteBlock.Click += (s, e) => DeleteBlock();
            btnAddLeave.Click += (s, e) => AddLeave();
            btnRemoveLeave.Click += (s, e) => RemoveLeave();
            btnClose.Click += (s, e) => Close();

            dgvBlocks.SelectionChanged += (s, e) => OnBlocksSelectionChanged();
            dgvLeaves.SelectionChanged += (s, e) => btnRemoveLeave.Enabled = GridHelper.GetSelectedDataRow(dgvLeaves) != null;
            dgvWeek.CellClick += (s, e) => OnWeekCellClicked(e.RowIndex, e.ColumnIndex);
            dgvWeek.CellFormatting += OnWeekCellFormatting;
            dgvLeaves.CellFormatting += OnLeaveCellFormatting;

            // Live length / daily-total hint while the block form is being filled in.
            cboDay.SelectedIndexChanged += (s, e) => UpdateBlockInfo();
            dtpStart.ValueChanged += (s, e) => UpdateBlockInfo();
            dtpEnd.ValueChanged += (s, e) => UpdateBlockInfo();
            txtMinutes.TextChanged += (s, e) => UpdateBlockInfo();
        }

        // ------------------------------------------------------------------
        // Start-up and reloading
        // ------------------------------------------------------------------

        private void InitializeScreen()
        {
            try
            {
                cboDay.DataSource = ScheduleBlock.WeekOrder
                    .Select(d => new LookupItem { Id = ((int)d).ToString(), Name = ScheduleBlock.DayLabel(d) })
                    .ToList();
                cboDay.DisplayMember = nameof(LookupItem.Name);
                cboDay.ValueMember = nameof(LookupItem.Id);
                cboDay.SelectedIndex = -1;

                dtpLeave.MinDate = DateTime.Today;      // a leave date cannot be in the past
                dtpLeave.Value = DateTime.Today;

                ReloadAll(null);
                StartNewBlock();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The weekly schedule could not be loaded.", this);
            }
        }

        /// <summary>Reloads blocks, weekly grid and leave dates; optionally keeps one block selected.</summary>
        private void ReloadAll(int? selectScheduleId)
        {
            _blocks = _repository.GetBlocks(_doctorId);
            BindBlocksGrid(selectScheduleId);
            BuildWeeklyGrid();
            ReloadLeaves();
            UpdateSummary();
        }

        private void BindBlocksGrid(int? selectScheduleId)
        {
            var table = new DataTable();
            table.Columns.Add("schedule_id", typeof(int));
            table.Columns.Add("day_name", typeof(string));
            table.Columns.Add("start_text", typeof(string));
            table.Columns.Add("end_text", typeof(string));
            table.Columns.Add("hours", typeof(decimal));
            table.Columns.Add("minutes", typeof(int));
            table.Columns.Add("slots", typeof(int));
            table.Columns.Add("notes", typeof(string));

            foreach (ScheduleBlock b in _blocks)
            {
                table.Rows.Add(b.ScheduleId, b.DayName, ScheduleBlock.FormatTime(b.StartTime),
                               ScheduleBlock.FormatTime(b.EndTime), b.Hours, b.ConsultationMinutes, b.SlotCount, b.Notes);
            }

            _suppressSelection = true;
            try
            {
                dgvBlocks.DataSource = table;
                dgvBlocks.ClearSelection();
                lblBlocksTitle.Text = $"Availability blocks  ·  {table.Rows.Count}";

                if (selectScheduleId.HasValue) SelectBlockRow(selectScheduleId.Value);
            }
            finally
            {
                _suppressSelection = false;
            }
        }

        private void SelectBlockRow(int scheduleId)
        {
            foreach (DataGridViewRow row in dgvBlocks.Rows)
            {
                if (row.DataBoundItem is DataRowView view && Convert.ToInt32(view.Row["schedule_id"]) == scheduleId)
                {
                    dgvBlocks.CurrentCell = GridHelper.FirstVisibleCell(row);
                    row.Selected = true;
                    return;
                }
            }
        }

        /// <summary>Rebuilds the 7-column Monday-Sunday picture: one cell per block, day totals in the headings.</summary>
        private void BuildWeeklyGrid()
        {
            dgvWeek.Rows.Clear();

            var perDay = ScheduleBlock.WeekOrder
                .Select(d => _blocks.Where(b => b.Day == d).OrderBy(b => b.StartTime).ToList())
                .ToList();

            for (int c = 0; c < perDay.Count; c++)
            {
                decimal hours = perDay[c].Sum(b => b.Hours);
                string shortName = ScheduleBlock.ShortDayLabel(ScheduleBlock.WeekOrder[c]);
                dgvWeek.Columns[c].HeaderText = hours > 0 ? $"{shortName}  ({hours:0.##} h)" : $"{shortName}  (off)";
            }

            int rowCount = Math.Max(1, perDay.Max(list => list.Count));
            for (int r = 0; r < rowCount; r++)
            {
                int rowIndex = dgvWeek.Rows.Add();
                for (int c = 0; c < perDay.Count; c++)
                {
                    DataGridViewCell cell = dgvWeek.Rows[rowIndex].Cells[c];
                    if (r < perDay[c].Count)
                    {
                        ScheduleBlock b = perDay[c][r];
                        cell.Value = $"{b.TimeRangeText}\n{b.ConsultationMinutes} min visits";
                        cell.Tag = b.ScheduleId;
                    }
                    else
                    {
                        cell.Value = (r == 0 && perDay[c].Count == 0) ? "Off" : string.Empty;
                        cell.Tag = null;
                    }
                }
            }

            dgvWeek.ClearSelection();
            GridHelper.FitColumns(dgvWeek);
        }

        private void OnWeekCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridViewCell cell = dgvWeek[e.ColumnIndex, e.RowIndex];
            Color back;
            Color fore;

            if (cell.Tag is int scheduleId)
            {
                bool isSelected = _editingScheduleId == scheduleId;
                back = isSelected ? ThemeHelper.Accent : ThemeHelper.AccentSoft;
                fore = isSelected ? Color.White : ThemeHelper.AccentInk;
                e.CellStyle.Font = isSelected ? ThemeHelper.BoldFont : ThemeHelper.BaseFont;
            }
            else
            {
                back = ThemeHelper.Background;
                fore = ThemeHelper.TextMuted;
            }

            e.CellStyle.BackColor = back;
            e.CellStyle.ForeColor = fore;
            e.CellStyle.SelectionBackColor = back;      // no extra selection colour: the "selected" look is our own
            e.CellStyle.SelectionForeColor = fore;
        }

        private void OnWeekCellClicked(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || columnIndex < 0) return;
            if (dgvWeek[columnIndex, rowIndex].Tag is not int scheduleId) return;

            _suppressSelection = true;
            SelectBlockRow(scheduleId);
            _suppressSelection = false;

            LoadBlock(scheduleId);
            dgvWeek.ClearSelection();
        }

        // ------------------------------------------------------------------
        // Block form: new / edit mode
        // ------------------------------------------------------------------

        private void OnBlocksSelectionChanged()
        {
            if (_suppressSelection) return;
            if (dgvBlocks.SelectedRows.Count == 0 || dgvBlocks.SelectedRows[0].DataBoundItem is not DataRowView view) return;

            LoadBlock(Convert.ToInt32(view.Row["schedule_id"]));
        }

        private void StartNewBlock()
        {
            _validator.ClearAll();

            cboDay.SelectedIndex = -1;
            SetTime(dtpStart, new TimeSpan(8, 0, 0));
            SetTime(dtpEnd, new TimeSpan(12, 0, 0));
            txtMinutes.Text = _repository.DefaultConsultationMinutes.ToString();
            txtNotes.Clear();

            _editingScheduleId = null;

            _suppressSelection = true;
            dgvBlocks.ClearSelection();
            _suppressSelection = false;

            dgvWeek.Invalidate();
            SetBlockMode();
            UpdateBlockInfo();
            cboDay.Focus();
        }

        private void LoadBlock(int scheduleId)
        {
            ScheduleBlock? block = _blocks.FirstOrDefault(b => b.ScheduleId == scheduleId);
            if (block == null)
            {
                MessageHelper.Warning("This schedule block no longer exists. The schedule will be refreshed.", "Block not found", this);
                ReloadAll(null);
                StartNewBlock();
                return;
            }

            _validator.ClearAll();

            cboDay.SelectedValue = ((int)block.Day).ToString();
            SetTime(dtpStart, block.StartTime);
            SetTime(dtpEnd, block.EndTime);
            txtMinutes.Text = block.ConsultationMinutes.ToString();
            txtNotes.Text = block.Notes;

            _editingScheduleId = block.ScheduleId;

            dgvWeek.Invalidate();
            SetBlockMode();
            UpdateBlockInfo();
        }

        private void SetBlockMode()
        {
            ScheduleBlock? editing = _editingScheduleId.HasValue
                ? _blocks.FirstOrDefault(b => b.ScheduleId == _editingScheduleId.Value)
                : null;

            lblBlockMode.Text = editing != null
                ? $"Editing {editing.DayName} {editing.TimeRangeText}"
                : "New block - choose the day and times, then click Add Block";
            btnSaveBlock.Text = editing != null ? "Save Changes" : "Add Block";
            btnDeleteBlock.Enabled = editing != null;
        }

        /// <summary>Live hint under the block form: block length, visits that fit, and the day's total against the limit.</summary>
        private void UpdateBlockInfo()
        {
            TimeSpan start = GetTime(dtpStart);
            TimeSpan end = GetTime(dtpEnd);

            if (end <= start)
            {
                lblBlockInfo.ForeColor = ThemeHelper.Warning;
                lblBlockInfo.Text = "End Time must be after Start Time.";
                return;
            }

            int length = (int)(end - start).TotalMinutes;
            string text = $"Block length: {length / 60m:0.##} hours";

            if (ValidationHelper.TryParsePositiveInt(txtMinutes.Text, out int minutes) && minutes <= length)
            {
                text += $"  ({length / minutes} visits of {minutes} min)";
            }

            if (SelectedDay() is int dayId)
            {
                int otherMinutes = _blocks
                    .Where(b => (int)b.Day == dayId && b.ScheduleId != (_editingScheduleId ?? 0))
                    .Sum(b => b.DurationMinutes);
                decimal total = (otherMinutes + length) / 60m;
                text += $"\nTotal on this day with this block: {total:0.##} of {_repository.MaxDailyHours:0.##} hours allowed.";
                lblBlockInfo.ForeColor = total > _repository.MaxDailyHours ? ThemeHelper.Warning : ThemeHelper.TextMuted;
            }
            else
            {
                lblBlockInfo.ForeColor = ThemeHelper.TextMuted;
            }

            lblBlockInfo.Text = text;
        }

        // ------------------------------------------------------------------
        // Block validation and saving
        // ------------------------------------------------------------------

        private bool ValidateBlock()
        {
            _validator.Begin();

            _validator.Required(cboDay, "Day");

            bool dayChosen = SelectedDay().HasValue;
            bool minutesTyped = ValidationHelper.TryParsePositiveInt(txtMinutes.Text, out int minutes);
            if (!minutesTyped)
            {
                _validator.AddError(txtMinutes, "Consultation Duration must be a whole number of minutes (example: 15).");
            }

            if (dayChosen && minutesTyped)
            {
                // Every rule that needs the database (overlap, daily hours) plus end-after-start and the minimum minutes.
                foreach (ScheduleProblem problem in _repository.CheckBlock(BuildBlock()))
                {
                    _validator.AddError(ControlFor(problem.Field), problem.Message);
                }
            }
            else
            {
                // Without a day or a valid number the repository cannot check, so run the rules that do not depend on them.
                if (GetTime(dtpEnd) <= GetTime(dtpStart))
                {
                    _validator.AddError(dtpEnd, "End Time must be after Start Time.");
                }
                if (minutesTyped && minutes < _repository.MinConsultationMinutes)
                {
                    _validator.AddError(txtMinutes, $"Consultation Duration must be at least {_repository.MinConsultationMinutes} minutes.");
                }
            }

            if (_validator.IsValid) return true;

            _validator.ShowErrors(this);
            return false;
        }

        private Control ControlFor(ScheduleField field) => field switch
        {
            ScheduleField.Day => cboDay,
            ScheduleField.StartTime => dtpStart,
            ScheduleField.EndTime => dtpEnd,
            _ => txtMinutes
        };

        private ScheduleBlock BuildBlock()
        {
            ValidationHelper.TryParsePositiveInt(txtMinutes.Text, out int minutes);

            return new ScheduleBlock
            {
                ScheduleId = _editingScheduleId ?? 0,
                DoctorId = _doctorId,
                Day = (DayOfWeek)SelectedDay()!.Value,
                StartTime = GetTime(dtpStart),
                EndTime = GetTime(dtpEnd),
                ConsultationMinutes = minutes,
                Notes = txtNotes.Text.Trim()
            };
        }

        private void SaveBlock()
        {
            try
            {
                if (!ValidateBlock()) return;

                Cursor = Cursors.WaitCursor;
                ScheduleBlock block = BuildBlock();
                bool isNew = !_editingScheduleId.HasValue;

                if (isNew)
                {
                    block.ScheduleId = _repository.AddBlock(block);
                }
                else
                {
                    _repository.UpdateBlock(block);
                }
                Cursor = Cursors.Default;

                _lastAction = isNew
                    ? $"Added {block.DayName} {block.TimeRangeText}."
                    : $"Saved {block.DayName} {block.TimeRangeText}.";

                ReloadAll(isNew ? null : block.ScheduleId);

                if (isNew)
                {
                    StartNewBlock();          // ready for the next block
                }
                else
                {
                    LoadBlock(block.ScheduleId);   // stay on the block that was just saved
                }
            }
            catch (InvalidOperationException ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Warning(ex.Message, "Schedule block not saved", this);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Error(ex, "The schedule block could not be saved.", this);
            }
        }

        private void DeleteBlock()
        {
            if (_editingScheduleId is not int scheduleId) return;

            ScheduleBlock? block = _blocks.FirstOrDefault(b => b.ScheduleId == scheduleId);
            if (block == null) return;

            try
            {
                if (!MessageHelper.ConfirmDestructive(
                        $"Remove the {block.DayName} block {block.TimeRangeText} from {_doctorName}'s weekly schedule?\n\nThis cannot be undone.",
                        "Delete schedule block", this)) return;

                Cursor = Cursors.WaitCursor;
                _repository.DeleteBlock(scheduleId);
                Cursor = Cursors.Default;

                _lastAction = $"Removed {block.DayName} {block.TimeRangeText}.";
                ReloadAll(null);
                StartNewBlock();
            }
            catch (InvalidOperationException ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Warning(ex.Message, "Schedule block not removed", this);
                ReloadAll(null);
                StartNewBlock();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageHelper.Error(ex, "The schedule block could not be removed.", this);
            }
        }

        // ------------------------------------------------------------------
        // Leave dates
        // ------------------------------------------------------------------

        private void ReloadLeaves()
        {
            DataTable leaves = _repository.GetLeaves(_doctorId);
            _leaveCount = leaves.Rows.Count;

            dgvLeaves.DataSource = leaves;
            dgvLeaves.ClearSelection();
            btnRemoveLeave.Enabled = false;
            lblLeavesTitle.Text = $"Leave dates  ·  {_leaveCount}";
        }

        private void OnLeaveCellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvLeaves.Rows[e.RowIndex].DataBoundItem is not DataRowView view) return;

            string status = Convert.ToString(view.Row["leave_status"]) ?? string.Empty;
            if (status == "Past")
            {
                e.CellStyle.ForeColor = ThemeHelper.TextMuted;           // finished leave: dimmed
            }
            else if (status == "Today" && dgvLeaves.Columns[e.ColumnIndex].Name == "colLStatus")
            {
                e.CellStyle.ForeColor = ThemeHelper.Warning;
                e.CellStyle.Font = ThemeHelper.BoldFont;
            }
        }

        private void AddLeave()
        {
            try
            {
                DateTime date = dtpLeave.Value.Date;

                _validator.Begin();
                _validator.Required(txtLeaveReason, "Reason");
                if (date < DateTime.Today)
                {
                    _validator.AddError(dtpLeave, "A leave date cannot be in the past.");
                }
                else if (_repository.LeaveExists(_doctorId, date))
                {
                    _validator.AddError(dtpLeave, $"{date:MMM d, yyyy} is already marked as leave for this doctor.");
                }

                if (!_validator.IsValid)
                {
                    _validator.ShowErrors(this);
                    return;
                }

                int booked = _repository.CountBookedAppointmentsOn(_doctorId, date);
                if (booked > 0 && !MessageHelper.Confirm(
                        $"{_doctorName} already has {booked} booked appointment(s) on {date:dddd, MMM d, yyyy}.\n\n" +
                        "They are NOT cancelled automatically - the front desk must cancel or reschedule them.\n\n" +
                        "Mark this date as leave anyway?", "Booked appointments on this date", this)) return;

                _repository.AddLeave(new LeaveDate { DoctorId = _doctorId, Date = date, Reason = txtLeaveReason.Text.Trim() });

                txtLeaveReason.Clear();
                _lastAction = $"Leave added for {date:MMM d, yyyy}.";
                ReloadLeaves();
                UpdateSummary();
                txtLeaveReason.Focus();
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Leave date not saved", this);
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The leave date could not be saved.", this);
            }
        }

        private void RemoveLeave()
        {
            DataRow? row = GridHelper.GetSelectedDataRow(dgvLeaves);
            if (row == null) return;

            int leaveId = Convert.ToInt32(row["leave_id"]);
            DateTime date = Convert.ToDateTime(row["leave_date"]);

            try
            {
                if (!MessageHelper.ConfirmDestructive(
                        $"Remove the leave on {date:dddd, MMM d, yyyy} ({Convert.ToString(row["reason"])})?",
                        "Remove leave date", this)) return;

                _repository.RemoveLeave(leaveId);

                _lastAction = $"Leave removed for {date:MMM d, yyyy}.";
                ReloadLeaves();
                UpdateSummary();
            }
            catch (InvalidOperationException ex)
            {
                MessageHelper.Warning(ex.Message, "Leave date not removed", this);
                ReloadLeaves();
                UpdateSummary();
            }
            catch (Exception ex)
            {
                MessageHelper.Error(ex, "The leave date could not be removed.", this);
            }
        }

        // ------------------------------------------------------------------
        // Footer summary and small helpers
        // ------------------------------------------------------------------

        private void UpdateSummary()
        {
            int days = _blocks.Select(b => b.Day).Distinct().Count();
            decimal hours = _blocks.Sum(b => b.Hours);

            string summary = _blocks.Count == 0
                ? "No availability blocks yet."
                : $"{_blocks.Count} block(s) on {days} day(s), {hours:0.##} hours per week.";
            summary += $"  {_leaveCount} leave date(s).";

            lblSummary.Text = string.IsNullOrEmpty(_lastAction) ? summary : $"{summary}     Last action: {_lastAction}";
        }

        /// <summary>Puts a time of day into a time picker (the date part is irrelevant).</summary>
        private static void SetTime(DateTimePicker picker, TimeSpan time)
        {
            picker.Value = DateTime.Today.Add(time);
        }

        /// <summary>The picker's time of day, whole minutes only.</summary>
        private static TimeSpan GetTime(DateTimePicker picker)
        {
            return new TimeSpan(picker.Value.Hour, picker.Value.Minute, 0);
        }
    }
}