using System.Windows.Forms;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Keeps every drop-down list lined up with its box.
    ///
    /// The list that opens under a ComboBox uses DropDownWidth. Several designer files had a fixed
    /// DropDownWidth (460 / 560 design pixels) left over from editing, so the list opened much wider than
    /// the box and looked shifted. This helper sets DropDownWidth to the box width, or to the longest item
    /// when an item would not fit, every time the list opens (so new items and DPI changes are always covered).
    ///
    /// Together with DPI awareness = PerMonitorV2 (ApplicationHighDpiMode in the .csproj) and
    /// AutoScaleMode = Dpi on every form, the list now opens directly under the box on any display scaling.
    ///
    /// Usage: FormHelper.Prepare(this) in a form's constructor calls AttachAll for you.
    /// </summary>
    public static class ComboBoxHelper
    {
        private const int ItemPaddingDesignPixels = 12;

        /// <summary>Attaches the width rule to every ComboBox inside the control, including ones added later.</summary>
        public static void AttachAll(Control root)
        {
            foreach (Control child in root.Controls)
            {
                if (child is ComboBox combo) Attach(combo);
                AttachAll(child);
            }

            root.ControlAdded -= OnControlAdded;
            root.ControlAdded += OnControlAdded;
        }

        /// <summary>Attaches the width rule to one ComboBox.</summary>
        public static void Attach(ComboBox combo)
        {
            combo.DropDown -= OnDropDown;
            combo.DropDown += OnDropDown;
            combo.SizeChanged -= OnChanged;
            combo.SizeChanged += OnChanged;
            combo.DataSourceChanged -= OnChanged;
            combo.DataSourceChanged += OnChanged;
            combo.HandleCreated -= OnChanged;
            combo.HandleCreated += OnChanged;

            FitDropDownWidth(combo);
        }

        /// <summary>
        /// DropDownWidth = the box width, or the width of the longest item (plus the scroll bar when the
        /// list scrolls) if that is wider. It never goes past the screen edge.
        /// </summary>
        public static void FitDropDownWidth(ComboBox combo)
        {
            if (combo.IsDisposed) return;

            int boxWidth = Math.Max(combo.Width, 1);
            int longest = 0;

            if (combo.Items.Count > 0)
            {
                foreach (object? item in combo.Items)
                {
                    string text = combo.GetItemText(item) ?? string.Empty;
                    if (text.Length == 0) continue;
                    int width = TextRenderer.MeasureText(text, combo.Font).Width;
                    if (width > longest) longest = width;
                }

                float scale = combo.DeviceDpi / 96f;
                longest += (int)Math.Round(ItemPaddingDesignPixels * scale);
                if (combo.Items.Count > combo.MaxDropDownItems)
                {
                    longest += SystemInformation.VerticalScrollBarWidth;
                }
            }

            int wanted = Math.Max(boxWidth, longest);

            try
            {
                int screenWidth = Screen.FromControl(combo).WorkingArea.Width;
                if (screenWidth > 0) wanted = Math.Min(wanted, screenWidth - 20);
            }
            catch (ArgumentException)
            {
                // No screen information yet (control not on a window): keep the computed width.
            }

            wanted = Math.Max(wanted, boxWidth);
            if (combo.DropDownWidth != wanted) combo.DropDownWidth = wanted;
        }

        private static void OnDropDown(object? sender, EventArgs e)
        {
            if (sender is ComboBox combo) FitDropDownWidth(combo);
        }

        private static void OnChanged(object? sender, EventArgs e)
        {
            if (sender is ComboBox combo) FitDropDownWidth(combo);
        }

        private static void OnControlAdded(object? sender, ControlEventArgs e)
        {
            if (e.Control == null) return;
            if (e.Control is ComboBox combo) Attach(combo);
            AttachAll(e.Control);
        }
    }
}
