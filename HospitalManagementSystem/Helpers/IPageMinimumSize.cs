namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// A module page that needs more room than the default page minimum (MainForm.PageMinimumSize) implements this.
    /// When the window is smaller than the page minimum, the page keeps the minimum size and scroll bars appear,
    /// so nothing is ever squeezed or cut off on small or zoomed screens (1366 x 768, 125 % / 150 % scaling).
    /// </summary>
    public interface IPageMinimumSize
    {
        /// <summary>The smallest size of the page in design pixels (100 % scaling).</summary>
        Size PageMinimumSize { get; }
    }
}
