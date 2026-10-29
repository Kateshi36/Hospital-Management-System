namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Search-as-you-type: the results appear while the user is still typing a name or an ID, without pressing
    /// Enter or clicking Search. The search runs a short moment after the last key press (so typing "Santos" runs one
    /// query, not six) and again right away on Enter. The Search button keeps working as before.
    /// </summary>
    public static class SearchHelper
    {
        public const int DefaultDelayMs = 300;

        /// <summary>Runs <paramref name="search"/> after the text of <paramref name="box"/> stops changing for a moment.</summary>
        /// <param name="minLength">Searches only when at least this many characters are typed, or the box is empty (show all).</param>
        public static void AttachLive(TextBox box, Action search, int delayMs = DefaultDelayMs, int minLength = 1)
        {
            var timer = new System.Windows.Forms.Timer { Interval = delayMs };
            string lastRun = box.Text.Trim();

            void Run()
            {
                timer.Stop();
                if (box.IsDisposed) return;
                string text = box.Text.Trim();
                if (text == lastRun) return;
                if (text.Length > 0 && text.Length < minLength) return;
                lastRun = text;
                search();
            }

            timer.Tick += (s, e) => Run();
            box.TextChanged += (s, e) =>
            {
                if (!box.Focused) { lastRun = box.Text.Trim(); return; }   // text set by the code (Clear, Show All ...)
                timer.Stop();
                timer.Start();
            };
            box.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                timer.Stop();
                lastRun = box.Text.Trim();       // the form's own Enter handler runs the search
            };
            box.Disposed += (s, e) => timer.Dispose();
        }
    }
}
