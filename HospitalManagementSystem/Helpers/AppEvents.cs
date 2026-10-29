namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Notifications between modules that are open at the same time (each module is a tab, so a screen opened earlier
    /// keeps the lists it loaded). When a doctor is added, edited or deleted in MP03, every open screen that lists doctors
    /// or specializations (Booking, Queue Board, Consultations) reloads them, so the same doctor never shows a different
    /// specialization on different tabs.
    /// </summary>
    public static class AppEvents
    {
        /// <summary>Raised after a doctor (name, specialization, fee ...) was added, changed or deleted.</summary>
        public static event EventHandler? DoctorsChanged;

        public static void RaiseDoctorsChanged() => DoctorsChanged?.Invoke(null, EventArgs.Empty);

        /// <summary>Subscribes a form to DoctorsChanged and unsubscribes it automatically when the form is disposed.</summary>
        public static void OnDoctorsChanged(Form form, Action reload)
        {
            void Handler(object? sender, EventArgs e)
            {
                if (form.IsDisposed) return;
                if (form.InvokeRequired) form.BeginInvoke(reload);
                else reload();
            }

            DoctorsChanged += Handler;
            form.Disposed += (s, e) => DoctorsChanged -= Handler;
        }
    }
}
