using System.Data;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.Common
{
    /// <summary>
    /// All SQL that deals with users and sign-in.
    ///
    /// Security rules implemented here:
    ///  * Passwords are verified against a salted PBKDF2 hash - never compared in plain text.
    ///  * Unknown user and wrong password give the SAME message (no username guessing).
    ///  * Too many failed attempts lock the account (limits come from app_settings).
    ///  * Every attempt is written to login_history.
    ///  * The whole attempt runs in one transaction with the user row locked, so two
    ///    simultaneous wrong guesses cannot sneak past the attempt counter.
    /// </summary>
    public class UserRepository
    {
        // Used so an unknown username takes as long as a real one (hides which usernames exist).
        private static readonly Lazy<(string Hash, string Salt, int Iterations)> DummyCredential =
            new Lazy<(string, string, int)>(() =>
            {
                string hash = PasswordHelper.HashPassword("dummy-password-for-timing", out string salt, out int iterations);
                return (hash, salt, iterations);
            });

        /// <summary>Checks the credentials, updates lockout counters and records the attempt.</summary>
        public AuthResult Authenticate(string username, string password)
        {
            username = (username ?? string.Empty).Trim();
            password ??= string.Empty;

            int maxAttempts = Math.Max(1, SettingsHelper.GetInt(SettingsHelper.MaxLoginAttempts, 5));
            int lockoutMinutes = Math.Max(1, SettingsHelper.GetInt(SettingsHelper.LockoutMinutes, 15));

            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                DataTable table = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT u.user_id, u.username, u.full_name, u.email, u.role, " +
                    "       u.password_hash, u.password_salt, u.hash_iterations, u.is_active, " +
                    "       u.failed_login_attempts, u.locked_until, " +
                    "       (u.locked_until IS NOT NULL AND u.locked_until >  NOW()) AS is_locked, " +
                    "       (u.locked_until IS NOT NULL AND u.locked_until <= NOW()) AS lock_expired " +
                    "FROM users u " +
                    "WHERE u.username = @username FOR UPDATE",
                    DatabaseHelper.Param("@username", username));

                // ---- Unknown username ----
                if (table.Rows.Count == 0)
                {
                    var dummy = DummyCredential.Value;
                    PasswordHelper.Verify(password, dummy.Hash, dummy.Salt, dummy.Iterations);
                    LogAttempt(conn, tx, null, username, false);
                    return AuthResult.Invalid();
                }

                DataRow row = table.Rows[0];
                int userId = DatabaseHelper.GetInt(row, "user_id");

                // ---- Currently locked: do not even check the password ----
                if (DatabaseHelper.GetBool(row, "is_locked"))
                {
                    LogAttempt(conn, tx, userId, username, false);
                    return AuthResult.Locked(DatabaseHelper.GetNullableDate(row, "locked_until"));
                }

                bool passwordOk = PasswordHelper.Verify(
                    password,
                    DatabaseHelper.GetString(row, "password_hash"),
                    DatabaseHelper.GetString(row, "password_salt"),
                    DatabaseHelper.GetInt(row, "hash_iterations"));

                // A lock that has expired starts the counter again from zero.
                int attempts = DatabaseHelper.GetBool(row, "lock_expired")
                    ? 0
                    : DatabaseHelper.GetInt(row, "failed_login_attempts");

                // ---- Wrong password ----
                if (!passwordOk)
                {
                    attempts++;
                    bool lockNow = attempts >= maxAttempts;

                    DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "UPDATE users SET failed_login_attempts = @attempts, " +
                        "       locked_until = CASE WHEN @lockNow = 1 THEN DATE_ADD(NOW(), INTERVAL @minutes MINUTE) ELSE NULL END " +
                        "WHERE user_id = @id",
                        DatabaseHelper.Param("@attempts", attempts),
                        DatabaseHelper.Param("@lockNow", lockNow ? 1 : 0),
                        DatabaseHelper.Param("@minutes", lockoutMinutes),
                        DatabaseHelper.Param("@id", userId));

                    LogAttempt(conn, tx, userId, username, false);

                    return lockNow
                        ? AuthResult.Locked(DateTime.Now.AddMinutes(lockoutMinutes))
                        : AuthResult.Invalid(maxAttempts - attempts);
                }

                // ---- Correct password but the account is switched off ----
                if (!DatabaseHelper.GetBool(row, "is_active"))
                {
                    LogAttempt(conn, tx, userId, username, false);
                    return AuthResult.Disabled();
                }

                // ---- Success ----
                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "UPDATE users SET failed_login_attempts = 0, locked_until = NULL, last_login_at = NOW() WHERE user_id = @id",
                    DatabaseHelper.Param("@id", userId));

                LogAttempt(conn, tx, userId, username, true);

                string email = DatabaseHelper.GetString(row, "email");
                return AuthResult.Success(new User
                {
                    UserId = userId,
                    Username = DatabaseHelper.GetString(row, "username"),
                    FullName = DatabaseHelper.GetString(row, "full_name"),
                    Email = string.IsNullOrWhiteSpace(email) ? null : email,
                    Role = DatabaseHelper.GetString(row, "role")
                });
            });
        }

        private static void LogAttempt(MySqlConnection conn, MySqlTransaction tx, int? userId, string usernameAttempted, bool success)
        {
            string shortName = usernameAttempted.Length > 50 ? usernameAttempted.Substring(0, 50) : usernameAttempted;

            DatabaseHelper.ExecuteNonQuery(conn, tx,
                "INSERT INTO login_history (user_id, username_attempted, was_successful) " +
                "VALUES (@userId, @username, @success)",
                DatabaseHelper.Param("@userId", userId),
                DatabaseHelper.Param("@username", shortName),
                DatabaseHelper.Param("@success", success ? 1 : 0));
        }
    }
}