using System.Reflection;
using System.Text;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Brings an older hospital_management_db up to date automatically when the app starts, so nobody has to run SQL by hand
    /// after pulling a new version. It checks for the parts each version added; when one is missing it runs that version's
    /// script (Database/upgrade_v2.sql, upgrade_v3.sql - embedded in the .exe), which only ALTERs / adds and never deletes data. A fresh import of schema.sql already has everything, so nothing runs.
    /// If the database cannot be reached yet, nothing happens here (the login screen reports the connection).
    /// </summary>
    public static class SchemaUpgrader
    {
        /// <summary>One upgrade script and the check that tells whether the database already has it.</summary>
        private sealed record Step(string Script, string CheckSql, int ExpectedCount);

        /// <summary>The upgrades in order. Each script only ALTERs / adds and can safely run more than once.</summary>
        private static readonly Step[] Steps =
        {
            new Step("upgrade_v2.sql",
                "SELECT (SELECT COUNT(*) FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'audit_log') + " +
                "       (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'payments' AND COLUMN_NAME = 'payment_type') + " +
                "       (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'bill_items' AND COLUMN_NAME = 'source_type') + " +
                "       (SELECT COUNT(*) FROM information_schema.VIEWS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'v_unbilled_charges')",
                4),
            new Step("upgrade_v3.sql",
                "SELECT (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'patients' AND COLUMN_NAME = 'discount_card_type') + " +
                "       (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'patients' AND COLUMN_NAME = 'coverage_type') + " +
                "       (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'bills' AND COLUMN_NAME = 'coverage_amount') + " +
                "       (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'v_bill_totals' AND COLUMN_NAME = 'gross_total')",
                4)
        };

        private static bool IsApplied(Step step) =>
            Convert.ToInt32(DatabaseHelper.ExecuteScalar(step.CheckSql) ?? 0) == step.ExpectedCount;

        /// <summary>True when the database already has every part of the newest schema version.</summary>
        public static bool IsCurrent() => Steps.All(IsApplied);

        /// <summary>
        /// Runs the upgrades that are still needed, oldest first. Returns null when nothing had to be done or the
        /// upgrade succeeded, otherwise a message for the user. Never throws.
        /// </summary>
        public static string? EnsureUpToDate()
        {
            string current = "the upgrade script";
            try
            {
                if (!DatabaseHelper.TestConnection(out _)) return null;   // the login screen shows the connection problem

                bool ranAny = false;
                foreach (Step step in Steps)
                {
                    if (IsApplied(step)) continue;
                    current = step.Script;
                    foreach (string statement in SplitStatements(LoadScript(step.Script)))
                    {
                        DatabaseHelper.ExecuteNonQuery(statement);
                    }
                    ranAny = true;
                    if (!IsApplied(step))
                        return $"The database upgrade ran but some parts are still missing. Import Database\\{step.Script} in phpMyAdmin.";
                }
                if (ranAny) SettingsHelper.Refresh();
                return null;
            }
            catch (Exception ex)
            {
                return "The database could not be upgraded automatically: " + DatabaseHelper.GetFriendlyMessage(ex) +
                       $"\n\nImport Database\\{current} in phpMyAdmin (it keeps all data), then start the system again.";
            }
        }

        private static string LoadScript(string fileName)
        {
            Assembly assembly = typeof(SchemaUpgrader).Assembly;
            string? name = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));
            if (name != null)
            {
                using Stream stream = assembly.GetManifestResourceStream(name)!;
                using var reader = new StreamReader(stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }

            // fall back to the file next to the .exe or in the project's Database folder
            foreach (string path in new[]
                     {
                         Path.Combine(AppContext.BaseDirectory, "Database", fileName),
                         Path.Combine(AppContext.BaseDirectory, fileName)
                     })
            {
                if (File.Exists(path)) return File.ReadAllText(path, Encoding.UTF8);
            }
            throw new FileNotFoundException(fileName + " is missing from the application.");
        }

        /// <summary>Splits the script into statements: comment lines are dropped, a statement ends with ';' at the end of a line.</summary>
        public static List<string> SplitStatements(string script)
        {
            var statements = new List<string>();
            var current = new StringBuilder();
            foreach (string raw in script.Replace("\r\n", "\n").Split('\n'))
            {
                string line = raw.TrimEnd();
                string trimmed = line.TrimStart();
                if (trimmed.Length == 0 || trimmed.StartsWith("--", StringComparison.Ordinal)) continue;
                current.AppendLine(line);
                if (!trimmed.EndsWith(";", StringComparison.Ordinal)) continue;

                string statement = current.ToString().Trim().TrimEnd(';').Trim();
                current.Clear();
                if (statement.StartsWith("USE ", StringComparison.OrdinalIgnoreCase)) continue;   // the connection already uses the database
                if (statement.Length > 0) statements.Add(statement);
            }
            return statements;
        }
    }
}
