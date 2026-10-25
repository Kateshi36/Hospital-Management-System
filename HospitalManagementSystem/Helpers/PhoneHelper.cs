using System.Text.RegularExpressions;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// ONE definition of a valid phone / contact number for the whole project: a Philippine MOBILE number.
    ///
    ///   Typed on screen : 10 digits after a fixed "+63" label, first digit 9   ->  917 123 4567
    ///   Stored in DB    : "+63" + 10 digits (13 characters, fits the existing VARCHAR(25))  ->  +639171234567
    ///   Shown on screen : +63 917 123 4567
    ///
    /// Also accepted when pasted or already stored (legacy data): 09171234567, 0917-123-4567,
    /// 63 917 123 4567, +63 917 123 4567 - all are converted to the stored form.
    /// </summary>
    public static class PhoneHelper
    {
        public const string CountryPrefix = "+63";
        public const int SubscriberDigits = 10;

        /// <summary>Text shown to users whenever a phone number is rejected.</summary>
        public const string FormatHint =
            "a Philippine mobile number: +63 followed by 10 digits starting with 9 (example: +63 917 123 4567)";

        // Only digits, spaces, dashes, brackets and one leading "+" may appear - letters and symbols are rejected.
        private static readonly Regex AllowedCharacters = new Regex(@"^\+?[0-9\s\-()]+$", RegexOptions.Compiled);

        /// <summary>Converts any accepted input to "+639XXXXXXXXX". Returns false for anything else.</summary>
        public static bool TryNormalize(string? input, out string canonical)
        {
            canonical = string.Empty;
            if (string.IsNullOrWhiteSpace(input)) return false;

            string text = input.Trim();
            if (!AllowedCharacters.IsMatch(text)) return false;

            string digits = new string(text.Where(char.IsDigit).ToArray());

            if (digits.Length == 12 && digits.StartsWith("63")) digits = digits.Substring(2);   // 63 9XX...
            else if (digits.Length == 11 && digits.StartsWith("0")) digits = digits.Substring(1); // 09XX...

            if (digits.Length != SubscriberDigits || digits[0] != '9') return false;

            canonical = CountryPrefix + digits;
            return true;
        }

        public static bool IsValid(string? input) => TryNormalize(input, out _);

        /// <summary>The 10 digits after +63 (what the user types in the input box), or "" if not valid.</summary>
        public static string ToSubscriberDigits(string? stored)
        {
            return TryNormalize(stored, out string canonical) ? canonical.Substring(CountryPrefix.Length) : string.Empty;
        }

        /// <summary>"+63 917 123 4567" for display. Text that is not a valid number is returned unchanged.</summary>
        public static string Format(string? stored)
        {
            if (!TryNormalize(stored, out string canonical)) return stored ?? string.Empty;

            string d = canonical.Substring(CountryPrefix.Length);
            return $"{CountryPrefix} {d.Substring(0, 3)} {d.Substring(3, 3)} {d.Substring(6)}";
        }

        /// <summary>
        /// Digits to use for a partial search: letters/symbols removed and a leading 0 or 63 dropped,
        /// so "0917", "+63917" and "917" all look for the same numbers.
        /// </summary>
        public static string ToSearchDigits(string? input)
        {
            string digits = new string((input ?? string.Empty).Where(char.IsDigit).ToArray());
            if (digits.StartsWith("63")) digits = digits.Substring(2);
            else if (digits.StartsWith("0")) digits = digits.Substring(1);
            return digits;
        }
    }
}