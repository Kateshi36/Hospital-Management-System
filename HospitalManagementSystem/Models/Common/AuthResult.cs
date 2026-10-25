namespace HospitalManagementSystem.Models.Common
{
    /// <summary>Why a login attempt succeeded or failed.</summary>
    public enum AuthStatus
    {
        Success,
        InvalidCredentials,
        AccountLocked,
        AccountDisabled
    }

    /// <summary>
    /// Result of UserRepository.Authenticate. The message is safe to show on screen:
    /// it never reveals whether the username or the password was the wrong part.
    /// </summary>
    public class AuthResult
    {
        public AuthStatus Status { get; private set; }
        public User? User { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public int? AttemptsRemaining { get; private set; }
        public DateTime? LockedUntil { get; private set; }

        public bool IsSuccess => Status == AuthStatus.Success;

        public static AuthResult Success(User user) => new AuthResult
        {
            Status = AuthStatus.Success,
            User = user,
            Message = "Signed in."
        };

        public static AuthResult Invalid(int? attemptsRemaining = null)
        {
            string message = "Invalid username or password.";
            if (attemptsRemaining.HasValue && attemptsRemaining.Value > 0 && attemptsRemaining.Value <= 2)
            {
                message += attemptsRemaining.Value == 1
                    ? " 1 attempt remaining before the account is locked."
                    : $" {attemptsRemaining.Value} attempts remaining before the account is locked.";
            }

            return new AuthResult
            {
                Status = AuthStatus.InvalidCredentials,
                Message = message,
                AttemptsRemaining = attemptsRemaining
            };
        }

        public static AuthResult Locked(DateTime? lockedUntil) => new AuthResult
        {
            Status = AuthStatus.AccountLocked,
            LockedUntil = lockedUntil,
            Message = lockedUntil.HasValue
                ? $"This account is temporarily locked after too many failed attempts. Try again after {lockedUntil.Value:h:mm tt}, or contact the administrator."
                : "This account is temporarily locked after too many failed attempts. Contact the administrator."
        };

        public static AuthResult Disabled() => new AuthResult
        {
            Status = AuthStatus.AccountDisabled,
            Message = "This account has been disabled. Please contact the administrator."
        };
    }
}