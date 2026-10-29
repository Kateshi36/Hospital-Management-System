using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Repositories.Common;

namespace HospitalManagementSystem.Views.Main
{
    /// <summary>
    /// The dashboard's patient search box. Matching patients appear in a list under the box WHILE the user types a name,
    /// a PAT- code or part of a phone number (no Search button needed). Clicking a patient - or Enter on the highlighted
    /// one - asks the main window to open Patient Search &amp; Profile with that patient.
    /// Keys: Down / Up move through the list, Enter opens, Esc closes the list.
    /// </summary>
    public sealed class DashboardPatientSearch
    {
        private const int MaxResults = 8;

        private readonly Form _host;
        private readonly TextBox _box;
        private readonly ListBox _list;
        private readonly DashboardRepository _repository = new DashboardRepository();
        private List<LookupItem> _results = new List<LookupItem>();

        /// <summary>Raised with the patient ID the user picked.</summary>
        public event Action<string>? PatientPicked;

        public DashboardPatientSearch(Form host, TextBox box)
        {
            _host = host;
            _box = box;
            _box.PlaceholderText = "Search patient: name, PAT- code or phone";

            _list = new ListBox
            {
                Visible = false,
                BorderStyle = BorderStyle.FixedSingle,
                DrawMode = DrawMode.OwnerDrawFixed,
                IntegralHeight = false,
                Font = ThemeHelper.BaseFont,
                BackColor = Color.White,
                ForeColor = ThemeHelper.TextPrimary,
                TabStop = false
            };
            _list.DrawItem += DrawItem;
            _list.MouseClick += (s, e) =>
            {
                int index = _list.IndexFromPoint(e.Location);
                if (index >= 0) Pick(index);
            };
            _list.MouseMove += (s, e) =>
            {
                int index = _list.IndexFromPoint(e.Location);
                if (index >= 0 && index != _list.SelectedIndex) _list.SelectedIndex = index;
            };
            _list.KeyDown += OnListKeyDown;
            _list.Leave += (s, e) => HideSoon();
            host.Controls.Add(_list);

            SearchHelper.AttachLive(_box, Search, 250);
            _box.KeyDown += OnBoxKeyDown;
            _box.Leave += (s, e) => HideSoon();
            host.Resize += (s, e) => { if (_list.Visible) PlaceList(); };
        }

        private float Scale => _host.DeviceDpi / 96f;

        private void Search()
        {
            string text = _box.Text.Trim();
            if (text.Length < 2)
            {
                Hide();
                return;
            }

            try
            {
                _results = _repository.FindPatients(text, MaxResults);
            }
            catch (Exception ex)
            {
                _results = new List<LookupItem>();
                _box.PlaceholderText = "Search failed: " + DatabaseHelper.GetFriendlyMessage(ex);
            }

            _list.BeginUpdate();
            _list.Items.Clear();
            if (_results.Count == 0) _list.Items.Add("No patient matches \"" + text + "\"");
            else foreach (LookupItem item in _results) _list.Items.Add(item);
            _list.EndUpdate();

            PlaceList();
            _list.Visible = true;
            _list.BringToFront();
            if (_results.Count > 0) _list.SelectedIndex = 0;
        }

        private void PlaceList()
        {
            Control anchor = InputBox.HostOf(_box);
            Point topLeft = _host.PointToClient(anchor.PointToScreen(new Point(0, anchor.Height + (int)(4 * Scale))));
            int rowHeight = (int)(46 * Scale);
            _list.ItemHeight = Math.Min(255, rowHeight);
            int rows = Math.Max(1, _list.Items.Count);
            _list.SetBounds(topLeft.X, topLeft.Y, Math.Max(anchor.Width, (int)(380 * Scale)), rows * _list.ItemHeight + 2);
        }

        private void DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected && _results.Count > 0;
            using (var back = new SolidBrush(selected ? ThemeHelper.AccentSoft : Color.White))
                e.Graphics.FillRectangle(back, e.Bounds);

            int pad = (int)(12 * Scale);
            object item = _list.Items[e.Index];
            if (item is LookupItem patient)
            {
                var top = new Rectangle(e.Bounds.X + pad, e.Bounds.Y + (int)(4 * Scale), e.Bounds.Width - pad * 2, e.Bounds.Height / 2);
                var bottom = new Rectangle(top.X, e.Bounds.Y + e.Bounds.Height / 2, top.Width, e.Bounds.Height / 2 - (int)(4 * Scale));
                TextRenderer.DrawText(e.Graphics, patient.Name, ThemeHelper.BoldFont, top, ThemeHelper.TextPrimary,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                TextRenderer.DrawText(e.Graphics, patient.Extra, ThemeHelper.SmallFont, bottom, ThemeHelper.TextMuted,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
            }
            else
            {
                var all = new Rectangle(e.Bounds.X + pad, e.Bounds.Y, e.Bounds.Width - pad * 2, e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, item.ToString(), ThemeHelper.SmallFont, all, ThemeHelper.TextMuted,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
            }

            using var line = new Pen(ThemeHelper.RowLine);
            e.Graphics.DrawLine(line, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
        }

        private void OnBoxKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && _list.Visible && _results.Count > 0)
            {
                e.Handled = e.SuppressKeyPress = true;
                _list.Focus();
                _list.SelectedIndex = Math.Min(_results.Count - 1, Math.Max(0, _list.SelectedIndex));
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                if (!_list.Visible) Search();
                else if (_results.Count > 0) Pick(Math.Max(0, _list.SelectedIndex));
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = e.SuppressKeyPress = true;
                Hide();
            }
        }

        private void OnListKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _list.SelectedIndex >= 0)
            {
                e.Handled = true;
                Pick(_list.SelectedIndex);
            }
            else if (e.KeyCode == Keys.Escape || (e.KeyCode == Keys.Up && _list.SelectedIndex <= 0))
            {
                e.Handled = true;
                _box.Focus();
                if (e.KeyCode == Keys.Escape) Hide();
            }
        }

        private void Pick(int index)
        {
            if (index < 0 || index >= _results.Count) return;
            string patientId = _results[index].Id;
            Hide();
            _box.Clear();
            PatientPicked?.Invoke(patientId);
        }

        private void HideSoon()
        {
            // focus moving between the box and its own list must not close the list
            _host.BeginInvoke(new Action(() =>
            {
                if (!_box.Focused && !_list.Focused) Hide();
            }));
        }

        private void Hide() => _list.Visible = false;
    }
}
