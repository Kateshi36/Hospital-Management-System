using System.Data;
using HospitalManagementSystem.Config;
using MySqlConnector;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// The ONLY class that creates database connections. Repositories call these
    /// methods and always pass values through parameters (never string concatenation).
    ///
    /// Two usage styles:
    ///   1) Simple     : DatabaseHelper.ExecuteQuery("SELECT ... WHERE id=@id", Param("@id", 5));
    ///   2) Transaction: DatabaseHelper.ExecuteInTransaction((conn, tx) => { ... });
    /// </summary>
    public static class DatabaseHelper
    {
        // ------------------------------------------------------------------
        // Connection
        // ------------------------------------------------------------------

        /// <summary>Creates a NEW, not-yet-opened connection. Caller must dispose it.</summary>
        public static MySqlConnection CreateConnection()
        {
            return new MySqlConnection(AppConfig.ConnectionString);
        }

        /// <summary>Checks that the database is reachable. Returns false with a readable message on failure.</summary>
        public static bool TestConnection(out string message)
        {
            try
            {
                using var conn = CreateConnection();
                conn.Open();
                message = "Connected to MySQL/MariaDB server version " + conn.ServerVersion +
                          ", database '" + conn.Database + "'.";
                return true;
            }
            catch (Exception ex)
            {
                message = GetFriendlyMessage(ex);
                return false;
            }
        }

        /// <summary>Builds a parameter; C# null becomes SQL NULL.</summary>
        public static MySqlParameter Param(string name, object? value)
        {
            return new MySqlParameter(name, value ?? DBNull.Value);
        }

        // ------------------------------------------------------------------
        // Simple (auto-open / auto-close) operations
        // ------------------------------------------------------------------

        /// <summary>Runs a SELECT and returns the rows as a DataTable.</summary>
        public static DataTable ExecuteQuery(string sql, params MySqlParameter[] parameters)
        {
            using var conn = CreateConnection();
            conn.Open();
            return ExecuteQuery(conn, null, sql, parameters);
        }

        /// <summary>Runs INSERT/UPDATE/DELETE. Returns the number of affected rows.</summary>
        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using var conn = CreateConnection();
            conn.Open();
            return ExecuteNonQuery(conn, null, sql, parameters);
        }

        /// <summary>Runs a query and returns the first column of the first row (or null).</summary>
        public static object? ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using var conn = CreateConnection();
            conn.Open();
            return ExecuteScalar(conn, null, sql, parameters);
        }

        /// <summary>Scalar query converted to T. Returns default when NULL / no row.</summary>
        public static T? ExecuteScalar<T>(string sql, params MySqlParameter[] parameters)
        {
            return ConvertScalar<T>(ExecuteScalar(sql, parameters));
        }

        /// <summary>Runs an INSERT and returns the auto-generated primary key.</summary>
        public static long ExecuteInsert(string sql, params MySqlParameter[] parameters)
        {
            using var conn = CreateConnection();
            conn.Open();
            return ExecuteInsert(conn, null, sql, parameters);
        }

        // ------------------------------------------------------------------
        // Operations on an existing connection / transaction
        // (used inside ExecuteInTransaction so several statements commit together)
        // ------------------------------------------------------------------

        public static DataTable ExecuteQuery(MySqlConnection conn, MySqlTransaction? tx, string sql, params MySqlParameter[] parameters)
        {
            using var cmd = BuildCommand(conn, tx, sql, parameters);
            using var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public static int ExecuteNonQuery(MySqlConnection conn, MySqlTransaction? tx, string sql, params MySqlParameter[] parameters)
        {
            using var cmd = BuildCommand(conn, tx, sql, parameters);
            return cmd.ExecuteNonQuery();
        }

        public static object? ExecuteScalar(MySqlConnection conn, MySqlTransaction? tx, string sql, params MySqlParameter[] parameters)
        {
            using var cmd = BuildCommand(conn, tx, sql, parameters);
            object? result = cmd.ExecuteScalar();
            return result == DBNull.Value ? null : result;
        }

        public static T? ExecuteScalar<T>(MySqlConnection conn, MySqlTransaction? tx, string sql, params MySqlParameter[] parameters)
        {
            return ConvertScalar<T>(ExecuteScalar(conn, tx, sql, parameters));
        }

        public static long ExecuteInsert(MySqlConnection conn, MySqlTransaction? tx, string sql, params MySqlParameter[] parameters)
        {
            using var cmd = BuildCommand(conn, tx, sql, parameters);
            cmd.ExecuteNonQuery();
            return cmd.LastInsertedId;
        }

        // ------------------------------------------------------------------
        // Transactions
        // ------------------------------------------------------------------

        /// <summary>
        /// Runs the work in one transaction: commits if the work finishes,
        /// rolls back and re-throws if anything fails.
        /// </summary>
        public static void ExecuteInTransaction(Action<MySqlConnection, MySqlTransaction> work)
        {
            using var conn = CreateConnection();
            conn.Open();
            using var tx = conn.BeginTransaction();
            try
            {
                work(conn, tx);
                tx.Commit();
            }
            catch
            {
                try { tx.Rollback(); } catch { /* connection already broken - nothing to roll back */ }
                throw;
            }
        }

        /// <summary>Transaction overload that returns a value from the work.</summary>
        public static T ExecuteInTransaction<T>(Func<MySqlConnection, MySqlTransaction, T> work)
        {
            using var conn = CreateConnection();
            conn.Open();
            using var tx = conn.BeginTransaction();
            try
            {
                T result = work(conn, tx);
                tx.Commit();
                return result;
            }
            catch
            {
                try { tx.Rollback(); } catch { /* connection already broken - nothing to roll back */ }
                throw;
            }
        }

        // ------------------------------------------------------------------
        // Row reading helpers (NULL-safe) for DataRow values
        // ------------------------------------------------------------------

        public static string GetString(DataRow row, string column)
        {
            object value = row[column];
            return value == DBNull.Value ? string.Empty : Convert.ToString(value) ?? string.Empty;
        }

        public static int GetInt(DataRow row, string column)
        {
            object value = row[column];
            return value == DBNull.Value ? 0 : Convert.ToInt32(value);
        }

        public static decimal GetDecimal(DataRow row, string column)
        {
            object value = row[column];
            return value == DBNull.Value ? 0m : Convert.ToDecimal(value);
        }

        public static DateTime? GetNullableDate(DataRow row, string column)
        {
            object value = row[column];
            return value == DBNull.Value ? null : Convert.ToDateTime(value);
        }

        public static bool GetBool(DataRow row, string column)
        {
            object value = row[column];
            return value != DBNull.Value && Convert.ToInt32(value) != 0;
        }

        // ------------------------------------------------------------------
        // Friendly error text (shown by MessageHelper)
        // ------------------------------------------------------------------

        /// <summary>Turns a technical exception into a clear message for staff.</summary>
        public static string GetFriendlyMessage(Exception ex)
        {
            if (ex is MySqlException my)
            {
                switch (my.Number)
                {
                    case 1045: return "The database rejected the user name or password. Check the connection string in appsettings.json.";
                    case 1049: return "The database 'hospital_management_db' does not exist. Import schema.sql with phpMyAdmin first.";
                    case 1062: return "That value already exists and must be unique (duplicate entry).";
                    case 1451: return "This record cannot be deleted because other records depend on it.";
                    case 1452: return "A related record does not exist (invalid reference).";
                    case 3819:
                    case 4025: return "A value is outside the allowed range (database check constraint failed).";
                    case 1146: return "A required database table is missing. Re-import schema.sql.";
                    case 1213: return "The database was busy and rolled the operation back. Please try again.";
                }

                if (my.InnerException is System.Net.Sockets.SocketException || my.Message.Contains("Unable to connect", StringComparison.OrdinalIgnoreCase))
                {
                    return "Cannot reach the MySQL server. Open XAMPP and make sure the MySQL module is running (port 3306).";
                }
                return "Database error (" + my.Number + "): " + my.Message;
            }

            if (ex is TimeoutException)
            {
                return "The database did not respond in time. Check that MySQL is running in XAMPP.";
            }

            return ex.Message;
        }

        // ------------------------------------------------------------------
        // Internals
        // ------------------------------------------------------------------

        private static MySqlCommand BuildCommand(MySqlConnection conn, MySqlTransaction? tx, string sql, MySqlParameter[] parameters)
        {
            var cmd = new MySqlCommand(sql, conn, tx);
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }
            return cmd;
        }

        private static T? ConvertScalar<T>(object? value)
        {
            if (value == null || value == DBNull.Value) return default;

            Type target = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            return (T)Convert.ChangeType(value, target);
        }
    }
}