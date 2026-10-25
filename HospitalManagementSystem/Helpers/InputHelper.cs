using System.Windows.Forms;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>What a text box is allowed to contain.</summary>
    public enum InputKind
    {
        /// <summary>Names: letters, spaces, hyphen, apostrophe, period. Digits and symbols are rejected.</summary>
        PersonName,
        /// <summary>Name search box: same as PersonName plus a comma ("Santos, Maria").</summary>
        PersonNameSearch,
        /// <summary>Street/address text: letters, digits, space and , . - # / ' ( )</summary>
        Address,
        /// <summary>Email: letters, digits and . _ % + - @ (no spaces).</summary>
        Email,
        /// <summary>User name: letters, digits, dot, underscore, hyphen.</summary>
        Username,
        /// <summary>Record codes such as PAT-20261015-001: capital letters, digits, hyphen.</summary>
        Code,
        /// <summary>Digits only (postal code, whole-number quantities, ...). Use the maxLength argument.</summary>
        Digits,
        /// <summary>The 10 digits after the fixed +63 label of a Philippine mobile number (first digit 9).</summary>
        PhoneSubscriber,
        /// <summary>Partial phone number for searching: digits only, up to 12.</summary>
        PhoneSearch,
        /// <summary>Money / measurements: digits and one decimal point, at most 2 decimal places.</summary>
        Decimal2
    }

    /// <summary>
    /// Stops invalid characters at the keyboard AND when text is pasted, so a field can never hold
    /// something its data type does not allow. Call InputHelper.Attach(...) once per text box in the form
    /// constructor. (Business rules - required, ranges, formats - are still checked by FormValidator on save.)
    /// </summary>
    public static class InputHelper
    {
        private const int PhoneSearchMax = 12;

        public static void Attach(TextBoxBase box, InputKind kind, int maxLength = 0)
        {
            if (maxLength > 0 && kind != InputKind.PhoneSubscriber) box.MaxLength = maxLength;

            if (kind == InputKind.Code && box is TextBox codeBox)
            {
                codeBox.CharacterCasing = CharacterCasing.Upper;
            }

            box.KeyPress += (sender, e) =>
            {
                if (char.IsControl(e.KeyChar)) return;   // Backspace, Ctrl+C, Ctrl+V ...

                // Text as it will be right before the new character is inserted (selection is replaced).
                string current = box.Text;
                int start = Math.Min(box.SelectionStart, current.Length);
                int length = Math.Min(box.SelectionLength, current.Length - start);
                string before = current.Remove(start, length);

                if (!IsAllowed(kind, e.KeyChar, before, start)) e.Handled = true;
            };

            bool busy = false;
            box.TextChanged += (sender, e) =>
            {
                if (busy) return;

                string cleaned = Sanitize(box.Text, kind, maxLength);
                if (cleaned == box.Text) return;

                busy = true;
                int caret = box.SelectionStart;
                int removed = box.Text.Length - cleaned.Length;
                box.Text = cleaned;
                box.SelectionStart = Math.Max(0, Math.Min(cleaned.Length, caret - removed));
                busy = false;
            };
        }

        // ------------------------------------------------------------------
        // Typing rules
        // ------------------------------------------------------------------

        private static bool IsAllowed(InputKind kind, char c, string before, int position)
        {
            switch (kind)
            {
                case InputKind.PersonName:
                case InputKind.PersonNameSearch:
                    if (position == 0) return char.IsLetter(c);                       // must start with a letter
                    return char.IsLetter(c) || c == ' ' || c == '-' || c == '\'' || c == '.'
                           || (kind == InputKind.PersonNameSearch && c == ',');

                case InputKind.Address:
                    return char.IsLetterOrDigit(c) || " ,.-#/'()".IndexOf(c) >= 0;

                case InputKind.Email:
                    return (c < 128 && char.IsLetterOrDigit(c)) || "._%+-@".IndexOf(c) >= 0;

                case InputKind.Username:
                    return (c < 128 && char.IsLetterOrDigit(c)) || c == '.' || c == '_' || c == '-';

                case InputKind.Code:
                    return (c < 128 && char.IsLetterOrDigit(c)) || c == '-';

                case InputKind.Digits:
                    return c >= '0' && c <= '9';

                case InputKind.PhoneSubscriber:
                    if (c < '0' || c > '9') return false;
                    if (before.Length >= PhoneHelper.SubscriberDigits) return false;   // never more than 10 digits
                    return position != 0 || c == '9';                                  // a mobile number starts with 9

                case InputKind.PhoneSearch:
                    return c >= '0' && c <= '9' && before.Length < PhoneSearchMax;

                case InputKind.Decimal2:
                    if (c >= '0' && c <= '9')
                    {
                        int dot = before.IndexOf('.');
                        return dot < 0 || position <= dot || before.Length - dot - 1 < 2;
                    }
                    return c == '.' && before.IndexOf('.') < 0 && position > 0;

                default:
                    return true;
            }
        }

        // ------------------------------------------------------------------
        // Paste / programmatic text clean-up
        // ------------------------------------------------------------------

        public static string Sanitize(string text, InputKind kind, int maxLength = 0)
        {
            string result;

            switch (kind)
            {
                case InputKind.PersonName:
                case InputKind.PersonNameSearch:
                    result = Keep(text, c => char.IsLetter(c) || c == ' ' || c == '-' || c == '\'' || c == '.'
                                             || (kind == InputKind.PersonNameSearch && c == ','));
                    result = result.TrimStart(' ', '-', '\'', '.', ',');
                    break;

                case InputKind.Address:
                    result = Keep(text, c => char.IsLetterOrDigit(c) || " ,.-#/'()".IndexOf(c) >= 0);
                    break;

                case InputKind.Email:
                    result = Keep(text, c => (c < 128 && char.IsLetterOrDigit(c)) || "._%+-@".IndexOf(c) >= 0);
                    break;

                case InputKind.Username:
                    result = Keep(text, c => (c < 128 && char.IsLetterOrDigit(c)) || c == '.' || c == '_' || c == '-');
                    break;

                case InputKind.Code:
                    result = Keep(text, c => (c < 128 && char.IsLetterOrDigit(c)) || c == '-').ToUpperInvariant();
                    break;

                case InputKind.Digits:
                    result = Keep(text, c => c >= '0' && c <= '9');
                    break;

                case InputKind.PhoneSubscriber:
                    result = CleanSubscriber(text);
                    break;

                case InputKind.PhoneSearch:
                    result = Keep(text, c => c >= '0' && c <= '9');
                    if (result.Length > PhoneSearchMax) result = result.Substring(0, PhoneSearchMax);
                    break;

                case InputKind.Decimal2:
                    result = CleanDecimal(text);
                    break;

                default:
                    result = text;
                    break;
            }

            if (maxLength > 0 && kind != InputKind.PhoneSubscriber && result.Length > maxLength)
            {
                result = result.Substring(0, maxLength);
            }
            return result;
        }

        /// <summary>
        /// Pasted "+63 917 123 4567", "09171234567" or "917-123-4567" all become the 10 digits "9171234567".
        /// A typed leading 0 is dropped (people are used to the 09... form).
        /// </summary>
        private static string CleanSubscriber(string text)
        {
            string digits = Keep(text, c => c >= '0' && c <= '9');

            if (digits.Length >= 11 && digits.StartsWith("63")) digits = digits.Substring(2);
            while (digits.StartsWith("0")) digits = digits.Substring(1);

            return digits.Length > PhoneHelper.SubscriberDigits ? digits.Substring(0, PhoneHelper.SubscriberDigits) : digits;
        }

        private static string CleanDecimal(string text)
        {
            string kept = Keep(text, c => (c >= '0' && c <= '9') || c == '.');
            int dot = kept.IndexOf('.');
            if (dot < 0) return kept;

            string whole = kept.Substring(0, dot);
            string fraction = kept.Substring(dot + 1).Replace(".", string.Empty);
            if (fraction.Length > 2) fraction = fraction.Substring(0, 2);
            return whole + "." + fraction;
        }

        private static string Keep(string text, Func<char, bool> allowed)
        {
            return new string(text.Where(allowed).ToArray());
        }
    }
}