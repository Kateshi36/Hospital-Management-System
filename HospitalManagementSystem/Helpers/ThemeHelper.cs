using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>Visual style of a button.</summary>
    public enum ButtonKind
    {
        Primary,    // dark teal, white text - the main action of a screen (Save, Book, Sign in)
        Secondary,  // white with a grey border - neutral action (Clear, Close, Refresh, Reschedule)
        Success,    // soft green - confirm / complete
        Warning,    // soft amber - caution
        Danger,     // red, white text - destructive (Delete, Cancel appointment)
        Soft,       // soft teal - small helper action next to a field (Search)
        Bright,     // bright teal, navy text - main action on a navy panel (Mark Complete)
        Ghost       // transparent with a light border, white text - secondary action on a navy panel
    }

    /// <summary>
    /// The light "clinical" look from the redesign mockup (HMS Redesign Mockup canvas):
    /// light grey page, white rounded cards, navy sidebar, one teal accent, soft status pills.
    ///
    /// FormHelper.Prepare(this) calls ApplyTheme(form) right after InitializeComponent(), so every form gets the
    /// look automatically - including the colours still written in older .Designer.cs files, which are mapped
    /// from the old dark palette to the new light one here (Remap). Controls whose Tag is the text "keep" are skipped.
    /// </summary>
    public static class ThemeHelper
    {
        // ---------------- Palette (hex values = the mockup) ----------------
        public static readonly Color Background = Hex("#F3F6F9");     // page
        public static readonly Color Panel = Hex("#FFFFFF");          // card
        public static readonly Color PanelLight = Hex("#F7F9FB");     // table header, subtle fills
        public static readonly Color SidebarHeader = Hex("#F7F9FB");  // kept for older code: grid header background

        public static readonly Color Accent = Hex("#127A8C");         // buttons, links, active text (4.9:1 on white)
        public static readonly Color AccentBright = Hex("#22A3B8");   // logo teal: active sidebar item, decoration
        public static readonly Color AccentSoft = Hex("#E2F2F6");     // selected row, "Booked" pill, soft buttons
        public static readonly Color AccentInk = Hex("#0D5F6D");      // text on AccentSoft
        public static readonly Color AccentTint = Hex("#F3FAFB");     // selected card
        public static readonly Color AccentLine = Hex("#BFE3EA");     // border of a selected card

        public static readonly Color Navy = Hex("#1B2B3F");           // sidebar, brand panel, "Now Serving"
        public static readonly Color NavyLine = Hex("#263A52");
        public static readonly Color NavyRaised = Hex("#2C4460");     // avatar circle
        public static readonly Color NavyText = Hex("#C6D4DE");
        public static readonly Color NavyMuted = Hex("#8FA5B4");
        public static readonly Color NavySection = Hex("#7F96A7");
        public static readonly Color NavySoftText = Hex("#B9CBD6");
        public static readonly Color NavyAccentText = Hex("#7FD3E0");

        public static readonly Color Success = Hex("#1E7A46");        // text / strong fill
        public static readonly Color SuccessSoft = Hex("#E3F4EA");
        public static readonly Color SuccessInk = Hex("#1E6B3F");
        public static readonly Color Warning = Hex("#9A5B00");
        public static readonly Color WarningSoft = Hex("#FFF4E0");
        public static readonly Color Danger = Hex("#B42318");
        public static readonly Color DangerSoft = Hex("#FBE7E6");
        public static readonly Color DangerInk = Hex("#9B1C14");
        public static readonly Color DangerTint = Hex("#FDF1F0");     // emergency row
        public static readonly Color Info = Hex("#2563A8");

        public static readonly Color TextPrimary = Hex("#142233");
        public static readonly Color TextSecondary = Hex("#3A4A5B");
        public static readonly Color TextMuted = Hex("#5B6B7C");
        public static readonly Color TextFaint = Hex("#8A97A3");

        public static readonly Color Border = Hex("#DCE3EA");         // card border
        public static readonly Color InputBorder = Hex("#C9D3DD");    // input / secondary button border
        public static readonly Color RowLine = Hex("#EEF2F5");        // line between table rows
        public static readonly Color InputBack = Hex("#FFFFFF");
        public static readonly Color ReadOnlyBack = Hex("#EEF2F5");   // gray = locked field
        public static readonly Color InputErrorBack = Hex("#FBE7E6"); // field with a validation error
        public static readonly Color GridAltRow = Hex("#FFFFFF");

        // ---------------- Fonts (px in the mockup x 0.75 = pt) ----------------
        public static readonly Font BaseFont = new Font("Segoe UI", 10.5f, FontStyle.Regular);          // 14 px
        public static readonly Font BoldFont = new Font("Segoe UI Semibold", 10.5f, FontStyle.Regular); // 14 px, 600
        public static readonly Font SmallFont = new Font("Segoe UI", 9.75f, FontStyle.Regular);         // 13 px
        public static readonly Font SmallBoldFont = new Font("Segoe UI Semibold", 9.75f, FontStyle.Regular);
        public static readonly Font CaptionFont = new Font("Segoe UI", 9f, FontStyle.Bold);             // 12 px caps
        public static readonly Font HeaderFont = new Font("Segoe UI", 12.75f, FontStyle.Bold);          // 17 px card title
        public static readonly Font TitleFont = new Font("Segoe UI", 18f, FontStyle.Bold);              // 24 px page title
        public static readonly Font PillFont = new Font("Segoe UI", 9f, FontStyle.Bold);                // 12 px pill

        public const int CardRadius = 12;
        public const int ButtonRadius = 8;
        public const int InputRadius = 8;

        private const string KeepTag = "keep";

        private static readonly ConditionalWeakTable<Button, ButtonState> _buttons = new();
        private static readonly ConditionalWeakTable<Control, object> _painted = new();

        // ======================================================================
        // Public API
        // ======================================================================

        /// <summary>Styles a form and everything inside it (recursively). Safe to call more than once.</summary>
        public static void ApplyTheme(Form form)
        {
            form.BackColor = Remap(form.BackColor, isBack: true, Background);
            form.ForeColor = TextPrimary;
            foreach (Control child in form.Controls.Cast<Control>().ToList())
            {
                ApplyToControl(child);
            }
        }

        /// <summary>Styles one control and its children (use for dynamically added controls).</summary>
        public static void ApplyToControl(Control control)
        {
            if (control.Tag is string tag && tag == KeepTag) return;

            switch (control)
            {
                case DataGridView grid:
                    GridHelper.Apply(grid);
                    return;

                case Button button:
                    if (!_buttons.TryGetValue(button, out _)) StyleButton(button, GuessKind(button));
                    return;

                case TextBox textBox:
                    bool locked = textBox.ReadOnly || Remap(textBox.BackColor, true, InputBack) == ReadOnlyBack;
                    textBox.BackColor = locked ? ReadOnlyBack : InputBack;
                    textBox.ForeColor = TextPrimary;
                    InputBox.Wrap(textBox);
                    return;

                case RichTextBox richText:
                    // Print-style previews (white paper) keep their own look; dark note boxes become light.
                    if (IsDark(richText.BackColor))
                    {
                        richText.BackColor = PanelLight;
                        richText.ForeColor = TextPrimary;
                    }
                    richText.BorderStyle = BorderStyle.None;
                    return;

                case ComboBox combo:
                    StyleComboBox(combo);
                    return;

                case NumericUpDown numeric:
                    numeric.BorderStyle = BorderStyle.FixedSingle;
                    numeric.BackColor = InputBack;
                    numeric.ForeColor = TextPrimary;
                    return;

                case DateTimePicker picker:
                    picker.CalendarMonthBackground = InputBack;
                    picker.CalendarForeColor = TextPrimary;
                    picker.CalendarTitleBackColor = Accent;
                    picker.CalendarTitleForeColor = Color.White;
                    picker.CalendarTrailingForeColor = TextFaint;
                    return;

                case ListBox listBox:
                    listBox.BorderStyle = BorderStyle.FixedSingle;
                    listBox.BackColor = InputBack;
                    listBox.ForeColor = TextPrimary;
                    return;

                case Label label:
                    StyleText(label);
                    return;

                case CheckBox check:
                    StyleText(check);
                    return;

                case RadioButton radio:
                    StyleText(radio);
                    return;

                case GroupBox group:
                    group.BackColor = Remap(group.BackColor, true, Panel);
                    group.ForeColor = TextPrimary;
                    MakeCard(group);
                    break; // children are themed below

                case StatusStrip strip:
                    strip.BackColor = Panel;
                    strip.ForeColor = TextMuted;
                    break;

                default:
                    if (control is ScrollableControl || control is SplitContainer)
                    {
                        control.BackColor = Remap(control.BackColor, true, control.BackColor);
                        control.ForeColor = Remap(control.ForeColor, false, TextPrimary);
                        // A white panel sitting directly on the grey page is a card: give it the rounded border.
                        if (control is Panel && !(control is TableLayoutPanel) && !(control is FlowLayoutPanel) &&
                            control.BackColor == Panel && EffectiveBack(control.Parent) == Background)
                        {
                            MakeCard(control);
                        }
                    }
                    break;
            }

            foreach (Control child in control.Controls.Cast<Control>().ToList())
            {
                ApplyToControl(child);
            }
        }

        /// <summary>Styles a button as one of the mockup's button kinds (rounded, with hover and pressed states).</summary>
        public static void StyleButton(Button button, ButtonKind kind)
        {
            if (!_buttons.TryGetValue(button, out ButtonState? state))
            {
                state = new ButtonState();
                _buttons.Add(button, state);
                button.Paint += OnButtonPaint;
                button.MouseEnter += (s, e) => { state.Hover = true; button.Invalidate(); };
                button.MouseLeave += (s, e) => { state.Hover = false; state.Pressed = false; button.Invalidate(); };
                button.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { state.Pressed = true; button.Invalidate(); } };
                button.MouseUp += (s, e) => { state.Pressed = false; button.Invalidate(); };
                button.EnabledChanged += (s, e) => button.Invalidate();
                button.TextChanged += (s, e) => button.Invalidate();
                button.ParentChanged += (s, e) => SyncButtonBack(button);
            }

            state.Kind = kind;
            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            SyncButtonBack(button);
            button.Invalidate();
        }

        /// <summary>Gray background + ReadOnly for locked fields; normal look when editable.</summary>
        public static void SetReadOnlyLook(TextBoxBase box, bool readOnly)
        {
            box.ReadOnly = readOnly;
            box.BackColor = readOnly ? ReadOnlyBack : InputBack;
            box.Parent?.Invalidate();
        }

        /// <summary>Big page title (e.g. "Patient Registration").</summary>
        public static void StyleTitle(Label label)
        {
            label.Font = TitleFont;
            label.ForeColor = TextPrimary;
            label.BackColor = Color.Transparent;
        }

        /// <summary>Section header text.</summary>
        public static void StyleHeader(Label label)
        {
            label.Font = HeaderFont;
            label.ForeColor = TextPrimary;
            label.BackColor = Color.Transparent;
        }

        /// <summary>Dimmed helper / hint text.</summary>
        public static void StyleMuted(Label label)
        {
            label.ForeColor = TextMuted;
            label.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Pill colours for a status word (Waiting, Completed, Expired, Fully Paid ...): soft background, dark text.
        /// Returns false for an unknown status so the caller keeps the default look.
        /// </summary>
        public static bool TryGetStatusColors(string? status, out Color back, out Color fore)
        {
            back = Color.Empty;
            fore = TextPrimary;
            if (string.IsNullOrWhiteSpace(status)) return false;

            switch (status.Trim().ToLowerInvariant())
            {
                case "completed":
                case "fully paid":
                case "paid":
                case "normal":
                case "fully dispensed":
                    back = SuccessSoft; fore = SuccessInk; return true;

                case "booked":
                case "in progress":
                case "finalized":
                case "ready":
                case "appointment":
                case "upcoming":
                    back = AccentSoft; fore = AccentInk; return true;

                case "waiting":
                case "pending":
                case "partially paid":
                case "partially dispensed":
                case "expiring soon":
                case "today":
                case "urgent":
                    back = WarningSoft; fore = Warning; return true;

                case "skipped":
                case "cancelled":
                case "no-show":
                case "expired":
                case "outstanding":
                case "abnormal":
                case "stat":
                    back = DangerSoft; fore = DangerInk; return true;

                case "emergency":
                    back = Danger; fore = Color.White; return true;

                case "voided":
                case "void":
                case "refund":
                case "refunded":
                    back = DangerSoft; fore = DangerInk; return true;

                case "partly refunded":
                    back = WarningSoft; fore = Warning; return true;

                case "low stock":
                case "draft":
                case "walk-in":
                case "routine":
                case "past":
                    back = ReadOnlyBack; fore = TextSecondary; return true;

                default:
                    return false;
            }
        }

        /// <summary>A rounded rectangle path (used by every painter).</summary>
        public static GraphicsPath RoundedRect(RectangleF bounds, float radius)
        {
            var path = new GraphicsPath();
            float d = Math.Max(0.1f, Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height)));
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>Draws a status pill (soft rounded label) inside a rectangle, vertically centred, left aligned.</summary>
        public static void DrawPill(Graphics g, Rectangle area, string text, float dpiScale)
        {
            if (!TryGetStatusColors(text, out Color back, out Color fore)) return;

            Size textSize = TextRenderer.MeasureText(g, text, PillFont, Size.Empty, TextFormatFlags.NoPadding);
            int padX = (int)Math.Round(10 * dpiScale);
            int padY = (int)Math.Round(3 * dpiScale);
            int width = Math.Min(area.Width, textSize.Width + padX * 2);
            int height = textSize.Height + padY * 2;
            var pill = new Rectangle(area.X, area.Y + (area.Height - height) / 2, width, height);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = RoundedRect(pill, height / 2f))
            using (var brush = new SolidBrush(back))
            {
                g.FillPath(brush, path);
            }
            TextRenderer.DrawText(g, text, PillFont, pill, fore,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
        }

        /// <summary>Turns a control into a rounded card (white fill, 1 px border, 12 px corners).</summary>
        public static void MakeCard(Control control, int radius = CardRadius, Color? border = null)
        {
            if (_painted.TryGetValue(control, out _)) return;
            _painted.Add(control, new object());

            Color borderColor = border ?? Border;
            if (control is GroupBox group)
            {
                group.FlatStyle = FlatStyle.Standard;
                group.Paint += (s, e) => PaintGroupCard(group, e.Graphics, radius, borderColor);
            }
            else
            {
                control.Paint += (s, e) => PaintCard(control, e.Graphics, radius, borderColor);
                control.Resize += (s, e) => control.Invalidate();
            }
            EnableDoubleBuffer(control);
        }

        /// <summary>Paints a round avatar with initials (sidebar user, selected patient).</summary>
        public static void MakeAvatar(Label label, Color back, Color fore)
        {
            label.BackColor = Color.Transparent;
            label.ForeColor = fore;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.Clear(EffectiveBack(label.Parent));
                int size = Math.Min(label.Width, label.Height) - 1;
                var circle = new Rectangle((label.Width - size) / 2, (label.Height - size) / 2, size, size);
                using (var brush = new SolidBrush(back)) e.Graphics.FillEllipse(brush, circle);
                TextRenderer.DrawText(e.Graphics, label.Text, label.Font, circle, fore,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            };
        }

        /// <summary>The first non-transparent background colour from a control up through its parents.</summary>
        public static Color EffectiveBack(Control? control)
        {
            while (control != null)
            {
                if (control.BackColor.A == 255) return control.BackColor;
                control = control.Parent;
            }
            return Background;
        }

        /// <summary>
        /// Maps a colour from the old dark palette (still written in older .Designer.cs files) to the new light palette.
        /// Colours that are not part of the old palette are returned unchanged (or <paramref name="fallback"/> when given
        /// and the colour is dark).
        /// </summary>
        public static Color Remap(Color color, bool isBack, Color fallback)
        {
            if (color.IsEmpty) return fallback;
            int key = (color.R << 16) | (color.G << 8) | color.B;

            if (isBack)
            {
                switch (key)
                {
                    case 0x14202E: return Background;   // old Background
                    case 0x101B27: return Panel;        // old header bar
                    case 0x1C2E40: return Panel;        // old Panel
                    case 0x243B52: return Panel;        // old PanelLight
                    case 0x26405A: return InputBack;    // old InputBack
                    case 0x223750: return Panel;        // old grid alternate row
                    case 0x3A4A5A: return ReadOnlyBack; // old read-only field
                    case 0x1E3246: return ReadOnlyBack; // old display-only field
                    case 0x1B6F7A: return Navy;         // old teal header strip
                    case 0x3D607A: return Panel;
                    case 0x325066: return PanelLight;
                    case 0x34506A: return Border;
                    case 0x1F8A70: return Panel;
                    case 0x187696: return Navy;
                    case 0x182838: return PanelLight;
                    case 0x3E301C: return WarningSoft;  // old warning message background
                    case 0x5E2B33: return InputErrorBack;
                    case 0x1FA2B8: return Accent;
                    case 0x2E9E5B: return SuccessSoft;
                    case 0xC62828: return Danger;
                    case 0xF0A020: return WarningSoft;
                }
                return IsDark(color) && fallback != color ? fallback : color;
            }

            switch (key)
            {
                case 0xE8F1F5: return TextPrimary;   // old primary text
                case 0xE1EEF2: return TextPrimary;
                case 0xD7EBF0: return TextPrimary;
                case 0x9FB3C2: return TextMuted;     // old muted text
                case 0x1FA2B8: return Accent;
                case 0xFF8A80: return Danger;
                case 0xFFAAA0: return Danger;
                case 0xF0A020: return Warning;
                case 0xFFC86E: return Warning;
                case 0x5ED68C: return Success;
                case 0x53AF78: return Success;
            }
            return color;
        }

        // ======================================================================
        // Internals
        // ======================================================================

        private static Color Hex(string hex) => ColorTranslator.FromHtml(hex);

        private static bool IsDark(Color color)
        {
            if (color.A < 255) return false;
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255.0;
            return luminance < 0.45;
        }

        private static void EnableDoubleBuffer(Control control)
        {
            typeof(Control).GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?
                .SetValue(control, true);
        }

        /// <summary>Labels, check boxes, radio buttons: transparent, dark text on light backgrounds, Semibold instead of Bold.</summary>
        private static void StyleText(Control control)
        {
            Color mappedBack = Remap(control.BackColor, true, control.BackColor);
            control.BackColor = mappedBack.A == 255 && mappedBack != EffectiveBack(control.Parent) ? mappedBack : Color.Transparent;

            Color fore = Remap(control.ForeColor, false, control.ForeColor);
            bool onDark = IsDark(EffectiveBack(control));
            if (!onDark && (fore == Color.White || (fore.R > 200 && fore.G > 200 && fore.B > 200))) fore = TextPrimary;
            control.ForeColor = fore;

            // Mockup labels are Semibold (600), not Bold (700). Same metrics, so nothing gets clipped.
            Font font = control.Font;
            if (font.Name == "Segoe UI" && font.Bold && font.Size <= 11f)
            {
                control.Font = new Font("Segoe UI Semibold", font.Size, font.Style & ~FontStyle.Bold, font.Unit);
            }
        }

        private static ButtonKind GuessKind(Button button)
        {
            int key = (button.BackColor.R << 16) | (button.BackColor.G << 8) | button.BackColor.B;
            return key switch
            {
                // older designers
                0x1FA2B8 => ButtonKind.Primary,
                0x2E9E5B => ButtonKind.Success,
                0xC62828 => ButtonKind.Danger,
                0xF0A020 => ButtonKind.Warning,
                // redesigned designers use the mockup colours directly
                0x127A8C => ButtonKind.Primary,
                0xE3F4EA => ButtonKind.Success,
                0xB42318 => ButtonKind.Danger,
                0xFFF4E0 => ButtonKind.Warning,
                0xE2F2F6 => ButtonKind.Soft,
                0x22A3B8 => ButtonKind.Bright,
                _ => ButtonKind.Secondary
            };
        }

        /// <summary>Drop-down lists: white, taller rows with padding, teal highlight - like the mockup's open list.</summary>
        private static void StyleComboBox(ComboBox combo)
        {
            combo.FlatStyle = FlatStyle.Standard;
            combo.BackColor = InputBack;
            combo.ForeColor = TextPrimary;
            if (combo.DrawMode != DrawMode.Normal) return;

            combo.DrawMode = DrawMode.OwnerDrawFixed;
            void FitHeight()
            {
                // Every screen now uses the mockup's 40 px input height; SetComboHeight overrides it (e.g. 36 px filters).
                // Filter drop-downs in a header toolbar (a FlowLayoutPanel) match the 36 px toolbar buttons.
                int fallback = combo.Parent is FlowLayoutPanel ? ToolbarComboHeight : DefaultComboHeight;
                int designHeight = ComboHeights.TryGetValue(combo, out object? wanted) ? (int)wanted : fallback;
                int fromDesign = designHeight > 0 ? (int)Math.Round(designHeight * combo.DeviceDpi / 96f) - 10 : 0;   // box = item height + borders
                combo.ItemHeight = Math.Max(combo.Font.Height + 2, fromDesign);
                SyncHeight();
            }
            // An owner-drawn drop-down keeps the height it was given in the designer (or by its layout) even when the
            // item height grows, so the bottom of the box was cut off. Keep the control exactly as tall as the box.
            bool syncing = false;
            void SyncHeight()
            {
                if (syncing || combo.DropDownStyle == ComboBoxStyle.Simple) return;
                int wanted = combo.PreferredHeight;
                if (combo.Height == wanted) return;
                syncing = true;
                try { combo.Height = wanted; }
                finally { syncing = false; }
            }
            combo.SizeChanged += (s, e) => SyncHeight();
            FitHeight();
            combo.HandleCreated += (s, e) => FitHeight();
            combo.DpiChangedAfterParent += (s, e) => FitHeight();
            combo.FontChanged += (s, e) => FitHeight();
            combo.ParentChanged += (s, e) => FitHeight();
            ComboRefitters.AddOrUpdate(combo, FitHeight);
            combo.DrawItem += (s, e) =>
            {
                if (e.Index < 0)
                {
                    // Nothing chosen yet: an empty white box (the system highlight drew a solid blue bar when focused).
                    using (var empty = new SolidBrush(combo.Enabled ? combo.BackColor : ReadOnlyBack)) e.Graphics.FillRectangle(empty, e.Bounds);
                    return;
                }
                bool inEditBox = (e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit;
                bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected && !inEditBox;
                Color back = !combo.Enabled ? ReadOnlyBack : selected ? AccentSoft : (inEditBox ? combo.BackColor : InputBack);
                Color fore = !combo.Enabled ? TextFaint : selected ? AccentInk : TextPrimary;
                using (var brush = new SolidBrush(back)) e.Graphics.FillRectangle(brush, e.Bounds);
                var textBounds = new Rectangle(e.Bounds.X + (inEditBox ? 2 : 8), e.Bounds.Y, e.Bounds.Width - 10, e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, combo.GetItemText(combo.Items[e.Index]), selected ? BoldFont : combo.Font,
                    textBounds, fore, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
            };
        }

        private static void PaintCard(Control control, Graphics g, int radius, Color border)
        {
            float scale = control.DeviceDpi / 96f;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(EffectiveBack(control.Parent));
            var bounds = new RectangleF(0.5f, 0.5f, control.Width - 1.5f, control.Height - 1.5f);
            using GraphicsPath path = RoundedRect(bounds, radius * scale);
            using (var brush = new SolidBrush(control.BackColor)) g.FillPath(brush, path);
            using (var pen = new Pen(border, 1f)) g.DrawPath(pen, path);
        }

        private static void PaintGroupCard(GroupBox group, Graphics g, int radius, Color border)
        {
            float scale = group.DeviceDpi / 96f;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(EffectiveBack(group.Parent));
            var bounds = new RectangleF(0.5f, 0.5f, group.Width - 1.5f, group.Height - 1.5f);
            using (GraphicsPath path = RoundedRect(bounds, radius * scale))
            {
                using (var brush = new SolidBrush(group.BackColor)) g.FillPath(brush, path);
                using (var pen = new Pen(border, 1f)) g.DrawPath(pen, path);
            }
            if (!string.IsNullOrEmpty(group.Text))
            {
                var titleBounds = new Rectangle((int)(14 * scale), (int)(4 * scale), group.Width - (int)(28 * scale), group.DisplayRectangle.Top - (int)(4 * scale));
                TextRenderer.DrawText(g, group.Text, SmallBoldFont, titleBounds, TextPrimary,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
            }
        }

        private const int DefaultComboHeight = 40;
        private const int ToolbarComboHeight = 36;
        private static readonly ConditionalWeakTable<ComboBox, object> ComboHeights = new();
        private static readonly ConditionalWeakTable<ComboBox, Action> ComboRefitters = new();

        /// <summary>Makes a drop-down box the given height in design pixels (the mockup's inputs are 40 px).</summary>
        public static void SetComboHeight(ComboBox combo, int designPixels)
        {
            ComboHeights.AddOrUpdate(combo, designPixels);
            if (ComboRefitters.TryGetValue(combo, out Action? refit)) refit();
        }

        private sealed class ButtonState
        {
            public ButtonKind Kind;
            public bool Hover;
            public bool Pressed;
        }

        private static void SyncButtonBack(Button button)
        {
            Color back = EffectiveBack(button.Parent);
            button.BackColor = back;
            button.FlatAppearance.MouseOverBackColor = back;
            button.FlatAppearance.MouseDownBackColor = back;
            button.FlatAppearance.BorderColor = back;
        }

        private static void OnButtonPaint(object? sender, PaintEventArgs e)
        {
            if (sender is not Button button || !_buttons.TryGetValue(button, out ButtonState? state)) return;

            float scale = button.DeviceDpi / 96f;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(EffectiveBack(button.Parent));

            (Color fill, Color text, Color border, Color hover, Color pressed, bool semibold) = state.Kind switch
            {
                ButtonKind.Primary => (Accent, Color.White, Color.Empty, Hex("#0F6B7B"), AccentInk, true),
                ButtonKind.Success => (SuccessSoft, SuccessInk, Color.Empty, Hex("#D3EDDC"), Hex("#C2E5CF"), true),
                ButtonKind.Warning => (WarningSoft, Warning, Color.Empty, Hex("#FBE9C8"), Hex("#F6DDB0"), true),
                ButtonKind.Danger => (Danger, Color.White, Color.Empty, DangerInk, Hex("#7F1710"), true),
                ButtonKind.Soft => (AccentSoft, AccentInk, Color.Empty, Hex("#D2EBF1"), Hex("#C1E3EB"), true),
                ButtonKind.Bright => (AccentBright, Hex("#0E1C2A"), Color.Empty, Hex("#3DB3C6"), Hex("#1A8FA2"), true),
                ButtonKind.Ghost => (Color.Transparent, Color.White, Hex("#4A6582"), Hex("#243A52"), Hex("#2C4460"), false),
                _ => (Panel, TextPrimary, InputBorder, Background, Hex("#E6EBF0"), false)
            };

            if (!button.Enabled)
            {
                if (state.Kind is ButtonKind.Ghost or ButtonKind.Bright)
                {
                    fill = Color.Transparent; text = NavySection; border = Hex("#34506A");
                }
                else
                {
                    fill = ReadOnlyBack; text = TextFaint; border = Color.Empty;
                }
            }
            else if (state.Pressed) fill = pressed;
            else if (state.Hover) fill = hover;

            var bounds = new RectangleF(0.5f, 0.5f, button.Width - 1.5f, button.Height - 1.5f);
            using (GraphicsPath path = RoundedRect(bounds, ButtonRadius * scale))
            {
                if (fill.A > 0)
                {
                    using var brush = new SolidBrush(fill);
                    g.FillPath(brush, path);
                }
                if (!border.IsEmpty)
                {
                    using var pen = new Pen(border, 1f);
                    g.DrawPath(pen, path);
                }
            }

            Font font = semibold
                ? new Font("Segoe UI Semibold", button.Font.Size, FontStyle.Regular, button.Font.Unit)
                : new Font("Segoe UI", button.Font.Size, FontStyle.Regular, button.Font.Unit);
            try
            {
                Rectangle textBounds = Rectangle.Inflate(button.ClientRectangle, -(int)(4 * scale), 0);
                textBounds = new Rectangle(textBounds.X + button.Padding.Left, textBounds.Y + button.Padding.Top,
                    textBounds.Width - button.Padding.Horizontal, textBounds.Height - button.Padding.Vertical);
                // One line whenever the caption fits; wrap only when it really does not (e.g. "10:00 AM" in a slot button).
                int oneLine = TextRenderer.MeasureText(g, button.Text, font, Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine).Width;
                TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis |
                                        (oneLine <= textBounds.Width ? TextFormatFlags.SingleLine | TextFormatFlags.NoPadding : TextFormatFlags.WordBreak);
                flags |= button.TextAlign switch
                {
                    ContentAlignment.MiddleLeft or ContentAlignment.TopLeft or ContentAlignment.BottomLeft => TextFormatFlags.Left,
                    ContentAlignment.MiddleRight or ContentAlignment.TopRight or ContentAlignment.BottomRight => TextFormatFlags.Right,
                    _ => TextFormatFlags.HorizontalCenter
                };
                if (!button.UseMnemonic) flags |= TextFormatFlags.NoPrefix;
                TextRenderer.DrawText(g, button.Text, font, textBounds, text, flags);
            }
            finally
            {
                font.Dispose();
            }
        }
    }
}
