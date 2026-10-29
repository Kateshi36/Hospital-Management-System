using HospitalManagementSystem.Helpers;

namespace HospitalManagementSystem.Views.Main
{
    /// <summary>
    /// Base class of every module page (and of the full-screen dialogs) so that small or zoomed screens work:
    ///
    ///   * <see cref="ApplyMinimum"/>: the page is laid out at least at its minimum size (design pixels). When the window is
    ///     smaller - a 1366 x 768 laptop, or Windows scaling at 125 % / 150 % - the page keeps that size and scroll bars appear,
    ///     so no field or button is squeezed, cut off or hidden.
    ///   * Clicking or tabbing into a field that is already visible no longer makes the page jump (ScrollToControl);
    ///     a field that is out of view is scrolled up / down into view (never sideways).
    ///
    /// A page that needs more room than the default implements <see cref="IPageMinimumSize"/>.
    /// </summary>
    public class PageHost : Form
    {
        /// <summary>The default minimum page size in design pixels (the pages are designed at 1176 x 848).</summary>
        public static readonly Size DefaultMinimum = new Size(1150, 860);

        /// <summary>Turns on page scrolling with the page's minimum size (call again after a DPI change).</summary>
        public static void ApplyMinimum(Form form)
        {
            Size minimum = form is IPageMinimumSize page ? page.PageMinimumSize : DefaultMinimum;
            float scale = form.DeviceDpi / 96f;
            form.AutoScroll = true;
            form.AutoScrollMinSize = new Size((int)Math.Round(minimum.Width * scale), (int)Math.Round(minimum.Height * scale));
        }

        protected override Point ScrollToControl(Control activeControl)
        {
            Rectangle visible = ClientRectangle;
            Rectangle field = RectangleToClient(activeControl.RectangleToScreen(activeControl.ClientRectangle));
            if (visible.Contains(field) || (field.Height > visible.Height && visible.IntersectsWith(field)))
                return DisplayRectangle.Location;   // already in view: keep the position the user scrolled to
            // out of view: scroll up / down to it, but leave the sideways position alone (no jumps to the left or right)
            Point wanted = base.ScrollToControl(activeControl);
            return new Point(DisplayRectangle.X, wanted.Y);
        }
    }
}
