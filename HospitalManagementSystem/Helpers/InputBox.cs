using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// The mockup's input field: a white box with an 8 px rounded grey border and inner padding,
    /// a 2 px teal border while the field has focus, red while it shows a validation error and grey when read-only.
    ///
    /// A WinForms TextBox cannot draw a rounded border or inner padding itself, so ThemeHelper wraps every TextBox
    /// in one of these at start-up (InputBox.Wrap). The wrapper takes the TextBox's exact place - same bounds, Dock,
    /// Anchor, Margin, tab order and TableLayoutPanel cell - and the TextBox moves inside it without a border.
    /// All code keeps using the TextBox as before; FormValidator shows error icons next to the wrapper (HostOf).
    /// </summary>
    public sealed class InputBox : Panel
    {
        private static readonly ConditionalWeakTable<Control, InputBox> Hosts = new();

        private readonly int _preferredHeight;

        public TextBox Inner { get; }

        private InputBox(TextBox inner)
        {
            Inner = inner;
            _preferredHeight = inner.Height;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Cursor = Cursors.IBeam;
        }

        /// <summary>The wrapper of a wrapped TextBox (for error icons), otherwise the control itself.</summary>
        public static Control HostOf(Control control)
        {
            return Hosts.TryGetValue(control, out InputBox? box) ? box : control;
        }

        /// <summary>Puts a TextBox inside an InputBox at the same place. Does nothing when it is already wrapped.</summary>
        public static void Wrap(TextBox textBox)
        {
            if (Hosts.TryGetValue(textBox, out _) || textBox.Parent == null || textBox.Parent is InputBox) return;
            if (textBox.Tag is string tag && tag == "keep") return;

            Control parent = textBox.Parent;
            var box = new InputBox(textBox)
            {
                Name = textBox.Name + "Box",
                Bounds = textBox.Bounds,
                Dock = textBox.Dock,
                Anchor = textBox.Anchor,
                Margin = textBox.Margin,
                TabIndex = textBox.TabIndex,
                TabStop = false,
                Visible = IsSetVisible(textBox),
                MinimumSize = textBox.MinimumSize,
                MaximumSize = textBox.MaximumSize
            };

            parent.SuspendLayout();
            try
            {
                if (parent is TableLayoutPanel table)
                {
                    TableLayoutPanelCellPosition cell = table.GetCellPosition(textBox);
                    int columnSpan = table.GetColumnSpan(textBox);
                    int rowSpan = table.GetRowSpan(textBox);
                    table.Controls.Remove(textBox);
                    table.Controls.Add(box, cell.Column, cell.Row);
                    table.SetColumnSpan(box, columnSpan);
                    table.SetRowSpan(box, rowSpan);
                }
                else
                {
                    int index = parent.Controls.GetChildIndex(textBox);
                    parent.Controls.Remove(textBox);
                    parent.Controls.Add(box);
                    parent.Controls.SetChildIndex(box, index);
                }

                textBox.BorderStyle = BorderStyle.None;
                textBox.Dock = DockStyle.None;
                textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                textBox.Margin = Padding.Empty;
                textBox.MinimumSize = Size.Empty;
                textBox.MaximumSize = Size.Empty;
                textBox.Visible = true;
                box.Controls.Add(textBox);
                Hosts.Add(textBox, box);

                textBox.Enter += (s, e) => box.Invalidate();
                textBox.Leave += (s, e) => box.Invalidate();
                textBox.BackColorChanged += (s, e) => box.Invalidate();
                textBox.EnabledChanged += (s, e) => { SyncDisabledBack(textBox); box.Invalidate(); };
                SyncDisabledBack(textBox);
                textBox.ReadOnlyChanged += (s, e) => box.Invalidate();
                box.Click += (s, e) => { if (textBox.CanFocus) textBox.Focus(); };
            }
            finally
            {
                parent.ResumeLayout(true);
            }
        }

        /// <summary>
        /// The TextBox's own Visible setting. The Visible property itself reports false for every control while its
        /// form is not shown yet (wrapping happens in the form constructor), which would hide every wrapped field.
        /// </summary>
        private static bool IsSetVisible(Control control)
        {
            try
            {
                var getState = typeof(Control).GetMethod("GetState",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (getState != null)
                {
                    var parameter = getState.GetParameters();
                    if (parameter.Length == 1)
                    {
                        object flag = parameter[0].ParameterType.IsEnum
                            ? Enum.ToObject(parameter[0].ParameterType, 0x00000002)   // States.Visible
                            : 0x00000002;
                        return (bool)getState.Invoke(control, new[] { flag })!;
                    }
                }
            }
            catch
            {
                // Fall through: no field is hidden in the designers, so "visible" is the safe default.
            }
            return true;
        }

        /// <summary>
        /// A disabled TextBox still paints its own BackColor (white), which looked like a white box inside the grey
        /// locked frame. Give it the locked grey while disabled and the normal colour back when enabled again.
        /// </summary>
        private static void SyncDisabledBack(TextBox textBox)
        {
            if (!textBox.Enabled)
            {
                if (textBox.BackColor != ThemeHelper.InputErrorBack) textBox.BackColor = ThemeHelper.ReadOnlyBack;
            }
            else if (!textBox.ReadOnly && textBox.BackColor == ThemeHelper.ReadOnlyBack)
            {
                textBox.BackColor = ThemeHelper.InputBack;
            }
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            return new Size(Width, Math.Max(_preferredHeight, Inner.PreferredHeight + 6));
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            float scale = DeviceDpi / 96f;
            int padX = Math.Max(3, Math.Min((int)Math.Round(10 * scale), Width / 6));

            if (Inner.Multiline)
            {
                int padY = (int)Math.Round(7 * scale);
                Inner.SetBounds(padX, padY, Math.Max(1, Width - padX * 2), Math.Max(1, Height - padY * 2));
            }
            else
            {
                int height = Inner.PreferredHeight;
                Inner.SetBounds(padX, Math.Max(1, (Height - height) / 2), Math.Max(1, Width - padX * 2), height);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float scale = DeviceDpi / 96f;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(ThemeHelper.EffectiveBack(Parent));

            bool focused = Inner.Focused;
            bool error = Inner.BackColor == ThemeHelper.InputErrorBack;
            bool locked = !Inner.Enabled || Inner.ReadOnly;

            Color fill = Inner.Enabled ? Inner.BackColor : ThemeHelper.ReadOnlyBack;
            Color border = error ? ThemeHelper.Danger
                         : focused && !locked ? ThemeHelper.AccentBright
                         : locked ? ThemeHelper.Border
                         : ThemeHelper.InputBorder;
            float width = (focused && !locked) || error ? 2f * Math.Max(1f, scale * 0.8f) : 1f;

            float inset = width / 2f + 0.5f;
            var bounds = new RectangleF(inset, inset, Width - inset * 2 - 0.5f, Height - inset * 2 - 0.5f);
            using GraphicsPath path = ThemeHelper.RoundedRect(bounds, ThemeHelper.InputRadius * scale);
            using (var brush = new SolidBrush(fill)) g.FillPath(brush, path);
            using (var pen = new Pen(border, width)) g.DrawPath(pen, path);
        }
    }
}
