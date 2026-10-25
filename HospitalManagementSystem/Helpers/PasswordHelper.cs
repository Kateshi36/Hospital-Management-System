using System.Security.Cryptography;
using System.Text;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Password hashing with PBKDF2-HMAC-SHA256 (salted, slow by design).
    /// Passwords are NEVER stored or compared in plain text.
    ///
    /// Database columns: password_hash (Base64), password_salt (Base64), hash_iterations.
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>Iterations used for NEW hashes. Stored per user, so it can be raised later.</summary>
        public const int DefaultIterations = 210000;

        private const int SaltSize = 16;   // 128-bit random salt
        private const int HashSize = 32;   // 256-bit hash
        private const int MinimumLength = 8;

        /// <summary>Creates a new random salt and hashes the password with it.</summary>
        /// <returns>The Base64 hash. The salt and iteration count are returned through the out parameters.</returns>
        public static string HashPassword(string password, out string saltBase64, out int iterations)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            iterations = DefaultIterations;
            saltBase64 = Convert.ToBase64String(salt);
            return Convert.ToBase64String(ComputeHash(password, salt, iterations));
        }

        /// <summary>Checks a typed password against the stored hash/salt/iterations (constant-time compare).</summary>
        public static bool Verify(string password, string storedHashBase64, string storedSaltBase64, int iterations)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHashBase64) ||
                string.IsNullOrEmpty(storedSaltBase64) || iterations <= 0)
            {
                return false;
            }

            try
            {
                byte[] salt = Convert.FromBase64String(storedSaltBase64);
                byte[] expected = Convert.FromBase64String(storedHashBase64);
                byte[] actual = ComputeHash(password, salt, iterations);
                return CryptographicOperations.FixedTimeEquals(actual, expected);
            }
            catch (FormatException)
            {
                return false; // corrupted Base64 in the database
            }
        }

        /// <summary>
        /// Password policy for new/changed passwords.
        /// Returns null when the password is acceptable, otherwise a specific message.
        /// </summary>
        public static string? ValidateStrength(string password)
        {
            if (string.IsNullOrEmpty(password)) return "Password is required.";
            if (password.Length < MinimumLength) return $"Password must be at least {MinimumLength} characters long.";
            if (!password.Any(char.IsUpper)) return "Password must contain at least one uppercase letter.";
            if (!password.Any(char.IsLower)) return "Password must contain at least one lowercase letter.";
            if (!password.Any(char.IsDigit)) return "Password must contain at least one number.";
            if (password.All(char.IsLetterOrDigit)) return "Password must contain at least one special character (e.g. @ # $ !).";
            return null;
        }

        private static byte[] ComputeHash(string password, byte[] salt, int iterations)
        {
            return Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                HashSize);
        }
    }
}