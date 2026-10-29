using MySqlConnector;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Generates every formatted code in the system from ONE place:
    ///   dated     : PAT-20261015-001, APT-..., CON-..., PRE-..., MS-..., LO-..., LR-..., BILL-..., PAY-..., RFD-...
    ///   non-dated : DOC-001, MED-001, TST-001
    ///
    /// The running number lives in the `code_sequences` table (per prefix, per day),
    /// so it survives restarts and is safe if two workstations save at the same time.
    ///
    /// IMPORTANT: when the code is saved together with a record, call the overload that
    /// takes (conn, tx) INSIDE the same transaction. If the save fails and rolls back,
    /// the number is released too - so there are no gaps.
    /// </summary>
    public static class GeneratorHelper
    {
        /// <summary>Code prefixes used across the system.</summary>
        public static class Prefix
        {
            public const string Patient = "PAT";
            public const string Appointment = "APT";
            public const string Consultation = "CON";
            public const string Prescription = "PRE";
            public const string Dispensing = "MS";
            public const string LabOrder = "LO";
            public const string LabResult = "LR";
            public const string Bill = "BILL";
            public const string Payment = "PAY";
            public const string Refund = "RFD";
            public const string Doctor = "DOC";
            public const string Medicine = "MED";
            public const string LabTest = "TST";
        }

        // Stored in code_sequences.seq_date for codes that do not contain a date.
        private static readonly DateTime NonDatedSentinel = new DateTime(1970, 1, 1);

        private static readonly HashSet<string> NonDatedPrefixes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            Prefix.Doctor, Prefix.Medicine, Prefix.LabTest
        };

        /// <summary>
        /// Generates the next code on a connection/transaction you already have open.
        /// Use this when saving a record so code + record commit (or roll back) together.
        /// </summary>
        public static string Generate(string prefix, MySqlConnection conn, MySqlTransaction? tx)
        {
            return Generate(prefix, DateTime.Today, conn, tx);
        }

        /// <summary>Same as above but for a specific date (dated prefixes only).</summary>
        public static string Generate(string prefix, DateTime date, MySqlConnection conn, MySqlTransaction? tx)
        {
            bool isNonDated = NonDatedPrefixes.Contains(prefix);
            DateTime sequenceDate = isNonDated ? NonDatedSentinel : date.Date;

            // Atomic: creates the row with 1, or increments it. LAST_INSERT_ID(expr) makes the
            // new value readable on THIS connection only, so concurrent users never collide.
            const string sql =
                "INSERT INTO code_sequences (prefix, seq_date, last_number) " +
                "VALUES (@prefix, @seqDate, LAST_INSERT_ID(1)) " +
                "ON DUPLICATE KEY UPDATE last_number = LAST_INSERT_ID(last_number + 1)";

            DatabaseHelper.ExecuteNonQuery(conn, tx, sql,
                DatabaseHelper.Param("@prefix", prefix.ToUpperInvariant()),
                DatabaseHelper.Param("@seqDate", sequenceDate));

            long number = DatabaseHelper.ExecuteScalar<long>(conn, tx, "SELECT LAST_INSERT_ID()");

            return isNonDated
                ? Format(prefix, number)
                : Format(prefix, sequenceDate, number);
        }

        /// <summary>
        /// Convenience for a code you need on its own (opens its own connection + transaction).
        /// Prefer the (conn, tx) overload when saving a record.
        /// </summary>
        public static string Generate(string prefix)
        {
            return DatabaseHelper.ExecuteInTransaction((conn, tx) => Generate(prefix, conn, tx));
        }

        /// <summary>PAT-20261015-007</summary>
        public static string Format(string prefix, DateTime date, long number)
        {
            return $"{prefix.ToUpperInvariant()}-{date:yyyyMMdd}-{number:D3}";
        }

        /// <summary>DOC-007</summary>
        public static string Format(string prefix, long number)
        {
            return $"{prefix.ToUpperInvariant()}-{number:D3}";
        }
    }
}