using System.Data;
using System.Globalization;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Reads the configurable business constants from the app_settings table
    /// (cancellation window, expiry days, discount thresholds, ...).
    /// Values are loaded once and cached; call Refresh() after changing a setting.
    /// </summary>
    public static class SettingsHelper
    {
        private static Dictionary<string, string>? _cache;
        private static readonly object _lock = new object();

        // Well-known keys (avoid typos in the rest of the code)
        public const string HospitalName = "hospital_name";
        public const string CurrencySymbol = "currency_symbol";
        public const string CancellationWindowHours = "cancellation_window_hours";
        public const string DefaultConsultationMinutes = "default_consultation_minutes";
        public const string ExpiringSoonDays = "expiring_soon_days";
        public const string PrescriptionValidityDays = "prescription_validity_days";
        public const string MinConsultationMinutes = "min_consultation_minutes";
        public const string MaxDailyWorkingHours = "max_daily_working_hours";
        public const string DefaultTaxRate = "default_tax_rate";
        public const string DiscountThresholdPercent = "discount_threshold_percent";
        public const string DiscountThresholdAmount = "discount_threshold_amount";
        public const string MaxLoginAttempts = "max_login_attempts";
        public const string LockoutMinutes = "lockout_minutes";

        /// <summary>Reloads every setting from the database.</summary>
        public static void Refresh()
        {
            DataTable table = DatabaseHelper.ExecuteQuery("SELECT setting_key, setting_value FROM app_settings");
            var fresh = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow row in table.Rows)
            {
                fresh[DatabaseHelper.GetString(row, "setting_key")] = DatabaseHelper.GetString(row, "setting_value");
            }

            lock (_lock)
            {
                _cache = fresh;
            }
        }

        public static string GetString(string key, string defaultValue = "")
        {
            EnsureLoaded();
            lock (_lock)
            {
                return _cache!.TryGetValue(key, out string? value) ? value : defaultValue;
            }
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            return int.TryParse(GetString(key), NumberStyles.Integer, CultureInfo.InvariantCulture, out int result)
                ? result
                : defaultValue;
        }

        public static decimal GetDecimal(string key, decimal defaultValue = 0m)
        {
            return decimal.TryParse(GetString(key), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result)
                ? result
                : defaultValue;
        }

        /// <summary>Formats an amount with the configured currency symbol, e.g. "₱1,234.50".</summary>
        public static string FormatMoney(decimal amount)
        {
            return GetString(CurrencySymbol, "PHP ") + amount.ToString("N2", CultureInfo.InvariantCulture);
        }

        private static void EnsureLoaded()
        {
            if (_cache == null) Refresh();
        }
    }
}