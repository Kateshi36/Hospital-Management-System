using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Every DataGridView in the system is configured here, so the rules are identical everywhere:
    /// read-only, no add/delete rows, full-row selection, single selection, fills the available width,
    /// and auto-sizes so all text shows (columns never narrower than their content, text wraps, rows grow).
    /// Users can NEVER type inside a grid; all editing happens in form controls.
    /// </summary>
    public static class GridHelper
    {
        /// <summary>Applies the mandatory read-only rules and the theme to a grid.</summary>
        public static void Apply(DataGridView grid)
        {
            // ---- Mandatory restrictions ----
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToOrderColumns = false;
            grid.AllowUserToResizeRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.EditMode = DataGridViewEditMode.EditProgrammatically;

            // ---- Layout / look ----
            // AutoGenerateColumns is left as it is: grids with columns defined in code set it to false,
            // a plain new grid keeps the default (true) and builds its columns from the data source.
            grid.RowHeadersVisible = false;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = ThemeHelper.RowLine;
            grid.BackgroundColor = ThemeHelper.Panel;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ScrollBars = ScrollBars.Both;

            // ---- Auto-size so ALL text shows (team rule) ----
            //   AutoSizeColumnsMode = Fill  : columns share the full grid width ...
            //   MinimumWidth per column     : ... but never get narrower than their content (see FitColumns),
            //                                  so nothing is cut off; a horizontal scroll bar appears instead.
            //   WrapMode = True             : long text (notes, findings, reasons) wraps onto more lines ...
            //   AutoSizeRowsMode = AllCells : ... and each row grows tall enough to show every line.
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            float scale = grid.DeviceDpi / 96f;
            int minRowHeight = Math.Max((int)Math.Round(40 * scale), grid.Font.Height + (int)Math.Round(18 * scale));
            grid.RowTemplate.Height = minRowHeight;
            grid.RowTemplate.MinimumHeight = minRowHeight;   // mockup table rows: about 11 px above and below the text

            grid.ColumnHeadersHeight = Math.Max((int)Math.Round(38 * scale), grid.Font.Height + (int)Math.Round(16 * scale));
            grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Mockup table: pale grey header with muted semibold text, white rows, thin row lines,
            // soft teal selection with dark text, 12 px side padding in every cell.
            var cellPadding = new Padding((int)Math.Round(12 * scale), 0, (int)Math.Round(8 * scale), 0);
            grid.ColumnHeadersDefaultCellStyle.BackColor = ThemeHelper.PanelLight;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = ThemeHelper.TextMuted;
            grid.ColumnHeadersDefaultCellStyle.Font = ThemeHelper.SmallBoldFont;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeHelper.PanelLight;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeHelper.TextMuted;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.Padding = cellPadding;

            grid.DefaultCellStyle.BackColor = ThemeHelper.Panel;
            grid.DefaultCellStyle.ForeColor = ThemeHelper.TextPrimary;
            grid.DefaultCellStyle.Font = ThemeHelper.BaseFont;
            grid.DefaultCellStyle.SelectionBackColor = ThemeHelper.AccentSoft;
            grid.DefaultCellStyle.SelectionForeColor = ThemeHelper.TextPrimary;
            grid.DefaultCellStyle.Padding = cellPadding;
            grid.AlternatingRowsDefaultCellStyle.BackColor = ThemeHelper.Panel;
            grid.AlternatingRowsDefaultCellStyle.ForeColor = ThemeHelper.TextPrimary;   // older designers set light text here
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = ThemeHelper.AccentSoft;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemeHelper.TextPrimary;
            grid.RowsDefaultCellStyle.BackColor = ThemeHelper.Panel;
            grid.RowsDefaultCellStyle.ForeColor = ThemeHelper.TextPrimary;
            grid.RowsDefaultCellStyle.SelectionBackColor = ThemeHelper.AccentSoft;
            grid.RowsDefaultCellStyle.SelectionForeColor = ThemeHelper.TextPrimary;

            // ---- Belt and braces: even columns created later stay read-only, and edits are cancelled ----
            grid.DataBindingComplete -= OnDataBindingComplete;
            grid.DataBindingComplete += OnDataBindingComplete;
            grid.ColumnAdded -= OnColumnAdded;
            grid.ColumnAdded += OnColumnAdded;
            grid.CellBeginEdit -= OnCellBeginEdit;
            grid.CellBeginEdit += OnCellBeginEdit;

            // ---- Re-fit the column widths whenever the content changes ----
            grid.RowsAdded -= OnRowsChanged;
            grid.RowsAdded += OnRowsChanged;
            grid.RowsRemoved -= OnRowsChanged;
            grid.RowsRemoved += OnRowsChanged;
            grid.CellValueChanged -= OnCellValueChanged;
            grid.CellValueChanged += OnCellValueChanged;
            grid.HandleCreated -= OnGridRefitNeeded;
            grid.HandleCreated += OnGridRefitNeeded;
            grid.FontChanged -= OnGridRefitNeeded;
            grid.FontChanged += OnGridRefitNeeded;
            grid.DpiChangedAfterParent -= OnGridRefitNeeded;
            grid.DpiChangedAfterParent += OnGridRefitNeeded;

            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.ReadOnly = true;
                AllowWrap(column);
            }

            FitColumns(grid);
        }

        /// <summary>
        /// Gives every visible column a MinimumWidth equal to the width its content needs (header and all cells),
        /// capped at <see cref="MaxAutoColumnWidth"/> design pixels so one long note cannot make the grid huge;
        /// text wider than the cap wraps onto extra lines instead. Fill mode then shares any extra space.
        /// Called automatically after binding and whenever rows change; call it yourself after changing cell
        /// values in code (for example the weekly schedule grid).
        /// </summary>
        public static void FitColumns(DataGridView grid)
        {
            if (grid.IsDisposed || grid.Columns.Count == 0) return;

            float scale = grid.DeviceDpi / 96f;
            int floor = (int)Math.Round(MinAutoColumnWidth * scale);
            int cap = (int)Math.Round(MaxAutoColumnWidth * scale);

            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (!column.Visible) continue;

                // A column that was given its own explicit size mode keeps it.
                if (column.AutoSizeMode != DataGridViewAutoSizeColumnMode.NotSet &&
                    column.AutoSizeMode != DataGridViewAutoSizeColumnMode.Fill) continue;

                int content = column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                int wanted = Math.Min(Math.Max(content, floor), Math.Max(cap, floor));
                if (column.MinimumWidth != wanted) column.MinimumWidth = wanted;
            }
        }

        /// <summary>Narrowest automatic column width (design pixels at 100 % scaling).</summary>
        public const int MinAutoColumnWidth = 48;

        /// <summary>Widest automatic minimum (design pixels); longer text wraps instead of widening the column.</summary>
        public const int MaxAutoColumnWidth = 320;

        /// <summary>
        /// Adds one read-only text column bound to a property / DataTable column. Used by forms that build their grid
        /// columns in code (the Designer drops designer-made columns when it re-saves a large file).
        /// </summary>
        public static DataGridViewTextBoxColumn AddTextColumn(DataGridView grid, string name, string header,
                                                              string dataPropertyName, float fillWeight,
                                                              bool alignRight = false, string? format = null,
                                                              bool center = false)
        {
            var column = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = dataPropertyName,
                FillWeight = fillWeight,
                MinimumWidth = 6,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            if (!string.IsNullOrEmpty(format)) column.DefaultCellStyle.Format = format;
            if (alignRight)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (center)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            grid.Columns.Add(column);
            return column;
        }

        /// <summary>
        /// Shows data in a grid. Optionally renames headers (column name -> header text)
        /// and hides technical columns (ids, foreign keys).
        /// </summary>
        public static void Bind(DataGridView grid, object? dataSource,
                                IDictionary<string, string>? headers = null,
                                params string[] hiddenColumns)
        {
            Apply(grid);
            grid.DataSource = dataSource;

            foreach (string name in hiddenColumns)
            {
                if (grid.Columns.Contains(name)) grid.Columns[name]!.Visible = false;
            }

            if (headers != null)
            {
                foreach (var pair in headers)
                {
                    if (grid.Columns.Contains(pair.Key)) grid.Columns[pair.Key]!.HeaderText = pair.Value;
                }
            }

            grid.ClearSelection();
        }

        /// <summary>Colours the cells of one column by status text (Waiting, Completed, Expired ...).</summary>
        public static void EnableStatusColoring(DataGridView grid, string columnName)
        {
            // Mockup: the status is a small rounded "pill" inside the cell (soft colour, dark text), not a coloured cell.
            grid.CellPainting += (sender, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Graphics == null) return;
                if (grid.Columns[e.ColumnIndex].Name != columnName) return;

                string text = Convert.ToString(e.FormattedValue) ?? string.Empty;
                if (!ThemeHelper.TryGetStatusColors(text, out _, out _)) return;

                e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
                float scale = grid.DeviceDpi / 96f;
                Padding pad = e.CellStyle?.Padding ?? Padding.Empty;
                var area = new Rectangle(e.CellBounds.X + pad.Left, e.CellBounds.Y, e.CellBounds.Width - pad.Horizontal, e.CellBounds.Height);
                ThemeHelper.DrawPill(e.Graphics, area, text, scale);
                e.Handled = true;
            };
        }

        /// <summary>
        /// The first visible cell of a row, for setting CurrentCell. Cells[0] fails ("Current cell cannot be set to an
        /// invisible cell") when the first column is hidden, e.g. the Date column while the list shows one day.
        /// </summary>
        public static DataGridViewCell? FirstVisibleCell(DataGridViewRow row)
        {
            DataGridView? grid = row.DataGridView;
            if (grid == null) return null;
            DataGridViewColumn? column = grid.Columns.GetFirstColumn(DataGridViewElementStates.Visible);
            return column == null ? null : row.Cells[column.Index];
        }

        /// <summary>The DataRow behind the selected grid row (grid must be bound to a DataTable/DataView), or null.</summary>
        public static DataRow? GetSelectedDataRow(DataGridView grid)
        {
            if (grid.CurrentRow?.DataBoundItem is DataRowView view) return view.Row;
            return null;
        }

        /// <summary>Value of a column in the selected row, converted to T. Returns default if nothing is selected.</summary>
        public static T? GetSelectedValue<T>(DataGridView grid, string columnName)
        {
            DataRow? row = GetSelectedDataRow(grid);
            if (row == null || row[columnName] == DBNull.Value) return default;

            Type target = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            return (T)Convert.ChangeType(row[columnName], target);
        }

        // ---------------- Event handlers ----------------

        private static void OnDataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (sender is DataGridView grid)
            {
                foreach (DataGridViewColumn column in grid.Columns)
                {
                    column.ReadOnly = true;
                    column.SortMode = DataGridViewColumnSortMode.Automatic; // sorting is allowed, editing is not
                }
                FitColumns(grid);
            }
        }

        // Rows added one by one (unbound grids) would re-measure the grid for every row, so the re-fit
        // is queued once and runs when the current batch of changes is finished.
        private static readonly HashSet<DataGridView> PendingFits = new HashSet<DataGridView>();

        private static void QueueFit(DataGridView grid)
        {
            if (grid.IsDisposed) return;
            if (!grid.IsHandleCreated)
            {
                FitColumns(grid);
                return;
            }
            if (!PendingFits.Add(grid)) return;
            grid.BeginInvoke(new Action(() =>
            {
                PendingFits.Remove(grid);
                FitColumns(grid);
            }));
        }

        private static void OnRowsChanged(object? sender, EventArgs e)
        {
            if (sender is DataGridView grid) QueueFit(grid);
        }

        private static void OnCellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView grid) QueueFit(grid);
        }

        private static void OnGridRefitNeeded(object? sender, EventArgs e)
        {
            if (sender is DataGridView grid) QueueFit(grid);
        }

        private static void OnColumnAdded(object? sender, DataGridViewColumnEventArgs e)
        {
            e.Column.ReadOnly = true;
            AllowWrap(e.Column);
        }

        private static bool IsOldDark(Color color) =>
            !color.IsEmpty && color.A == 255 && (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255.0 < 0.5;

        private static bool IsLightText(Color color) =>
            !color.IsEmpty && color.R > 200 && color.G > 200 && color.B > 200;

        /// <summary>A column style that switched wrapping off (old designer styles) would hide text; let it inherit the grid's wrap.</summary>
        private static void AllowWrap(DataGridViewColumn column)
        {
            if (column.HasDefaultCellStyle && column.DefaultCellStyle.WrapMode == DataGridViewTriState.False)
                column.DefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
            if (column.HasDefaultCellStyle)
            {
                // Old designer column styles carried the dark theme's colours: let the grid's light style show through.
                DataGridViewCellStyle style = column.DefaultCellStyle;
                if (IsOldDark(style.BackColor)) style.BackColor = Color.Empty;
                if (IsOldDark(style.SelectionBackColor)) style.SelectionBackColor = Color.Empty;
                if (style.ForeColor == Color.White || IsLightText(style.ForeColor)) style.ForeColor = Color.Empty;
                if (style.SelectionForeColor == Color.White) style.SelectionForeColor = Color.Empty;
                if (style.Font != null && style.Font.Name == "Segoe UI" && style.Font.Bold && style.Font.Size <= 11f)
                    style.Font = new Font("Segoe UI Semibold", style.Font.Size, style.Font.Style & ~FontStyle.Bold, style.Font.Unit);
            }
            if (column.HeaderCell.HasStyle && column.HeaderCell.Style.WrapMode == DataGridViewTriState.False)
                column.HeaderCell.Style.WrapMode = DataGridViewTriState.NotSet;
        }

        private static void OnCellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}