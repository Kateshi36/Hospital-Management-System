using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// Pure, reusable validation rules (no UI). FormValidator below wires them to controls.
    /// </summary>
    public static class ValidationHelper
    {
        private static readonly Regex EmailPattern = new Regex(
            @"^[A-Za-z0-9._%+\-]+@[A-Za-z0-9\-]+(\.[A-Za-z0-9\-]+)*\.[A-Za-z]{2,}$",
            RegexOptions.Compiled);


        public static bool IsBlank(string? text) => string.IsNullOrWhiteSpace(text);

        /// <summary>Basic email shape: name@domain.tld</summary>
        public static bool IsValidEmail(string? email)
        {
            return !IsBlank(email) && EmailPattern.IsMatch(email!.Trim());
        }

        /// <summary>A Philippine mobile number (+63 + 10 digits starting with 9). See PhoneHelper.</summary>
        public static bool IsValidPhone(string? phone) => PhoneHelper.IsValid(phone);

        /// <summary>Letters, spaces, hyphens, apostrophes and periods only, starting with a letter. Digits are rejected.</summary>
        public static bool IsValidPersonName(string? name)
        {
            if (IsBlank(name)) return false;
            string value = name!.Trim();
            return char.IsLetter(value[0]) && value.All(c => char.IsLetter(c) || c == ' ' || c == '-' || c == '\'' || c == '.');
        }

        /// <summary>Philippine postal (ZIP) code: exactly 4 digits.</summary>
        public static bool IsValidPostalCode(string? code) =>
            !IsBlank(code) && code!.Trim().Length == 4 && code.Trim().All(c => c >= '0' && c <= '9');

        public static bool IsPastOrToday(DateTime date) => date.Date <= DateTime.Today;

        public static bool IsFuture(DateTime date) => date.Date > DateTime.Today;

        public static bool IsInRange(decimal value, decimal min, decimal max) => value >= min && value <= max;

        public static bool IsPositive(decimal value) => value > 0;

        /// <summary>True for whole numbers greater than zero ("3" yes, "0", "-1", "2.5", "abc" no).</summary>
        public static bool TryParsePositiveInt(string? text, out int value)
        {
            return int.TryParse(text?.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out value) && value > 0;
        }

        /// <summary>True when the text is a valid decimal number (invariant culture).</summary>
        public static bool TryParseDecimal(string? text, out decimal value)
        {
            return decimal.TryParse(text?.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        /// <summary>Creates an ErrorProvider that does not blink. Use one per form.</summary>
        public static ErrorProvider CreateErrorProvider(ContainerControl container)
        {
            return new ErrorProvider(container)
            {
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            };
        }
    }

    /// <summary>
    /// Collects every problem on a form, shows an inline error icon + message next to each bad field
    /// (ErrorProvider) and tints the field so it is easy to spot.
    ///
    /// Typical use (one instance per form):
    ///   _validator = new FormValidator(errorProvider1);
    ///   ...
    ///   _validator.Begin();
    ///   _validator.Required(txtFirstName, "First Name");
    ///   _validator.Phone(txtPhone, "Phone Number");
    ///   if (!_validator.IsValid) { _validator.ShowErrors(this); return; }
    /// </summary>
    public class FormValidator
    {
        private readonly ErrorProvider _provider;
        private readonly List<string> _errors = new List<string>();
        private readonly Dictionary<Control, Color> _originalColors = new Dictionary<Control, Color>();
        private Control? _firstInvalid;

        public FormValidator(ErrorProvider provider)
        {
            _provider = provider;
        }

        public bool IsValid => _errors.Count == 0;

        public IReadOnlyList<string> Errors => _errors;

        /// <summary>Clears previous results. Call at the start of every validation run.</summary>
        public void Begin()
        {
            _provider.Clear();
            foreach (var pair in _originalColors)
            {
                pair.Key.BackColor = pair.Value;
            }
            _originalColors.Clear();
            _errors.Clear();
            _firstInvalid = null;
        }

        /// <summary>Same as Begin() - use after "Clear Form" or a successful save.</summary>
        public void ClearAll() => Begin();

        /// <summary>
        /// Makes the error disappear as soon as the user edits the field again.
        /// Call once per control (e.g. in the form constructor).
        /// </summary>
        public void AutoClear(params Control[] controls)
        {
            foreach (Control control in controls)
            {
                switch (control)
                {
                    case ComboBox combo:
                        combo.SelectedIndexChanged += (s, e) => ClearError(control);
                        combo.TextChanged += (s, e) => ClearError(control);
                        break;
                    case NumericUpDown numeric:
                        numeric.ValueChanged += (s, e) => ClearError(control);
                        break;
                    case DateTimePicker picker:
                        picker.ValueChanged += (s, e) => ClearError(control);
                        break;
                    default:
                        control.TextChanged += (s, e) => ClearError(control);
                        break;
                }
            }
        }

        /// <summary>Removes the error from a single control.</summary>
        public void ClearError(Control control)
        {
            _provider.SetError(InputBox.HostOf(control), string.Empty);
            if (_originalColors.TryGetValue(control, out Color original))
            {
                control.BackColor = original;
                _originalColors.Remove(control);
            }
        }

        // ---------------- Rules ----------------

        /// <summary>Text box must not be blank; drop-down must have a selection.</summary>
        public FormValidator Required(Control control, string label)
        {
            if (control is ComboBox combo)
            {
                bool hasValue = combo.DropDownStyle == ComboBoxStyle.DropDownList
                    ? combo.SelectedIndex >= 0
                    : !ValidationHelper.IsBlank(combo.Text);
                if (!hasValue) AddError(control, $"Please select {label}.");
            }
            else if (ValidationHelper.IsBlank(control.Text))
            {
                AddError(control, $"{label} is required.");
            }
            return this;
        }

        /// <summary>Optional unless required = true. When filled it must look like an email.</summary>
        public FormValidator Email(Control control, string label, bool required = false)
        {
            if (ValidationHelper.IsBlank(control.Text))
            {
                if (required) AddError(control, $"{label} is required.");
            }
            else if (!ValidationHelper.IsValidEmail(control.Text))
            {
                AddError(control, $"{label} is not a valid email address (example: name@example.com).");
            }
            return this;
        }

        /// <summary>Philippine mobile number: +63 followed by 10 digits starting with 9.</summary>
        public FormValidator Phone(Control control, string label, bool required = true)
        {
            if (ValidationHelper.IsBlank(control.Text))
            {
                if (required) AddError(control, $"{label} is required.");
            }
            else if (!ValidationHelper.IsValidPhone(control.Text))
            {
                AddError(control, $"{label} must be {PhoneHelper.FormatHint}.");
            }
            return this;
        }

        /// <summary>Names must contain letters only (plus spaces, hyphens, apostrophes, periods) - no digits.</summary>
        public FormValidator PersonName(Control control, string label, bool required = true)
        {
            if (ValidationHelper.IsBlank(control.Text))
            {
                if (required) AddError(control, $"{label} is required.");
            }
            else if (!ValidationHelper.IsValidPersonName(control.Text))
            {
                AddError(control, $"{label} may contain letters, spaces, hyphens, apostrophes and periods only (no numbers).");
            }
            return this;
        }

        /// <summary>Philippine postal code: exactly 4 digits (optional unless required = true).</summary>
        public FormValidator PostalCode(Control control, string label, bool required = false)
        {
            if (ValidationHelper.IsBlank(control.Text))
            {
                if (required) AddError(control, $"{label} is required.");
            }
            else if (!ValidationHelper.IsValidPostalCode(control.Text))
            {
                AddError(control, $"{label} must be exactly 4 digits (example: 1630).");
            }
            return this;
        }

        public FormValidator NotFutureDate(DateTimePicker picker, string label)
        {
            if (ValidationHelper.IsFuture(picker.Value))
            {
                AddError(picker, $"{label} cannot be a future date.");
            }
            return this;
        }

        public FormValidator InRange(Control control, decimal value, decimal min, decimal max, string label, string unit = "")
        {
            if (!ValidationHelper.IsInRange(value, min, max))
            {
                string suffix = string.IsNullOrEmpty(unit) ? "" : " " + unit;
                AddError(control, $"{label} must be between {min.ToString("0.##", CultureInfo.InvariantCulture)} and {max.ToString("0.##", CultureInfo.InvariantCulture)}{suffix}.");
            }
            return this;
        }

        public FormValidator Positive(Control control, decimal value, string label)
        {
            if (!ValidationHelper.IsPositive(value))
            {
                AddError(control, $"{label} must be greater than zero.");
            }
            return this;
        }

        /// <summary>Whole number greater than zero typed in a text box.</summary>
        public FormValidator PositiveInteger(Control control, string label)
        {
            if (!ValidationHelper.TryParsePositiveInt(control.Text, out _))
            {
                AddError(control, $"{label} must be a positive whole number.");
            }
            return this;
        }

        /// <summary>Any custom rule: if isValid is false the message is attached to the control (or shown in the list only).</summary>
        public FormValidator Check(bool isValid, Control? control, string message)
        {
            if (!isValid) AddError(control, message);
            return this;
        }

        /// <summary>Records one problem and marks the control.</summary>
        public void AddError(Control? control, string message)
        {
            _errors.Add(message);
            if (control == null) return;

            string existing = _provider.GetError(InputBox.HostOf(control));
            _provider.SetError(InputBox.HostOf(control), string.IsNullOrEmpty(existing) ? message : existing + "\n" + message);

            if (!_originalColors.ContainsKey(control) &&
                (control is TextBoxBase || control is ComboBox || control is NumericUpDown))
            {
                _originalColors[control] = control.BackColor;
                control.BackColor = ThemeHelper.InputErrorBack;
            }

            _firstInvalid ??= control;
        }

        // ---------------- Output ----------------

        /// <summary>Moves the cursor to the first field with a problem.</summary>
        public void FocusFirstInvalid()
        {
            _firstInvalid?.Focus();
        }

        /// <summary>Shows the full list of problems and focuses the first bad field.</summary>
        public void ShowErrors(IWin32Window? owner = null)
        {
            if (IsValid) return;
            MessageHelper.ValidationErrors(_errors, owner: owner);
            FocusFirstInvalid();
        }
    }
}