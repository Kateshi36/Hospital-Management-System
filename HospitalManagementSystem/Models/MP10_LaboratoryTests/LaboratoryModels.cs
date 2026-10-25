using System.Globalization;

namespace HospitalManagementSystem.Models.MP10_Laboratory
{
    /// <summary>The limits of Step 14. The form and the repository both use these constants.</summary>
    public static class LaboratoryRules
    {
        public const int MaxTestName = 120;
        public const int MaxRange = 40;
        public const int MaxUnit = 30;
        public const int MaxCategory = 60;          // lab_tests.category
        public const int MaxSampleType = 60;        // lab_tests.sample_type
        public const int MaxResultValue = 100;
        public const int MaxFindings = 500;
        public const decimal PriceMax = 9999999.99m;
        public const int TurnaroundMax = 720;       // hours (30 days)
        public const int HistoryLimit = 300;
        public const int OrderListLimit = 200;
    }

    /// <summary>
    /// THE ABNORMAL-VALUE COMPARISON (used live while typing, and again by the repository when the result is saved).
    ///
    ///   Numeric range  - the Low and High normal range both parse as numbers (12.0 and 17.5):
    ///                    the Result Value must be a number; below Low = Abnormal (LOW), above High = Abnormal (HIGH);
    ///                    a value equal to Low or High is Normal (the range is inclusive).
    ///   Descriptive    - the range is text (Negative / Non-reactive): the Result Value is Normal only when it equals
    ///                    the Low or the High text (ignoring capital letters and extra spaces); anything else is Abnormal.
    ///   No range       - both range fields empty (old data): nothing to compare, the result is never flagged.
    /// </summary>
    public static class RangeEvaluator
    {
        public static bool TryParseNumber(string? text, out decimal value)
        {
            return decimal.TryParse((text ?? string.Empty).Trim(), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out value);
        }

        /// <summary>True when the normal range is made of numbers (so the result value has to be a number too).</summary>
        public static bool IsNumericRange(string? low, string? high)
        {
            bool hasLow = !string.IsNullOrWhiteSpace(low);
            bool hasHigh = !string.IsNullOrWhiteSpace(high);
            if (!hasLow && !hasHigh) return false;
            return (!hasLow || TryParseNumber(low, out _)) && (!hasHigh || TryParseNumber(high, out _));
        }

        public static RangeCheck Evaluate(string? resultValue, string? low, string? high)
        {
            string value = (resultValue ?? string.Empty).Trim();
            bool hasLow = !string.IsNullOrWhiteSpace(low);
            bool hasHigh = !string.IsNullOrWhiteSpace(high);

            if (value.Length == 0) return new RangeCheck(RangeState.Empty, "Enter the result value to see the flag.");
            if (!hasLow && !hasHigh) return new RangeCheck(RangeState.NoRange, "This test has no normal range, so the result is not flagged.");

            if (IsNumericRange(low, high))
            {
                if (!TryParseNumber(value, out decimal number))
                    return new RangeCheck(RangeState.NotNumeric, "Enter a number - this test has a numeric range (e.g. 13.5)");

                if (hasLow && TryParseNumber(low, out decimal lowNumber) && number < lowNumber)
                    return new RangeCheck(RangeState.Low, $"ABNORMAL - LOW (below {low!.Trim()})");
                if (hasHigh && TryParseNumber(high, out decimal highNumber) && number > highNumber)
                    return new RangeCheck(RangeState.High, $"ABNORMAL - HIGH (above {high!.Trim()})");
                return new RangeCheck(RangeState.Normal, "NORMAL - within the normal range");
            }

            bool matches = Same(value, low) || Same(value, high);
            return matches
                ? new RangeCheck(RangeState.Normal, "NORMAL - matches the expected result")
                : new RangeCheck(RangeState.Different, $"ABNORMAL - expected {DescriptiveExpected(low, high)}");
        }

        private static bool Same(string value, string? expected)
        {
            if (string.IsNullOrWhiteSpace(expected)) return false;
            return string.Equals(Squash(value), Squash(expected), StringComparison.OrdinalIgnoreCase);
        }

        private static string Squash(string text) => string.Join(" ", text.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        private static string DescriptiveExpected(string? low, string? high)
        {
            string l = (low ?? string.Empty).Trim();
            string h = (high ?? string.Empty).Trim();
            if (l.Length == 0) return h;
            if (h.Length == 0 || string.Equals(l, h, StringComparison.OrdinalIgnoreCase)) return l;
            return l + " or " + h;
        }

        /// <summary>The normal range as one text: "12.0 - 17.5 g/dL", "Negative", "Not set".</summary>
        public static string FormatRange(string? low, string? high, string? unit)
        {
            string l = (low ?? string.Empty).Trim();
            string h = (high ?? string.Empty).Trim();
            string u = (unit ?? string.Empty).Trim();
            if (l.Length == 0 && h.Length == 0) return "Not set";

            string range;
            if (IsNumericRange(l, h))
                range = l.Length > 0 && h.Length > 0 ? $"{l} - {h}" : (l.Length > 0 ? $"{l} or more" : $"up to {h}");
            else
                range = string.Equals(l, h, StringComparison.OrdinalIgnoreCase) || h.Length == 0 ? (l.Length > 0 ? l : h) : $"{l} / {h}";

            return u.Length > 0 && IsNumericRange(l, h) ? $"{range} {u}" : range;
        }
    }

    public enum RangeState { Empty, NoRange, NotNumeric, Normal, Low, High, Different }

    /// <summary>The outcome of one comparison.</summary>
    public sealed class RangeCheck
    {
        public RangeState State { get; }
        public string Message { get; }

        public RangeCheck(RangeState state, string message)
        {
            State = state;
            Message = message;
        }

        public bool IsAbnormal => State == RangeState.Low || State == RangeState.High || State == RangeState.Different;
        public bool IsBlocking => State == RangeState.NotNumeric;
    }

    /// <summary>One catalog test as it is shown in the catalog grid and the edit form.</summary>
    public class LabTestRecord
    {
        /// <summary>TST-XXX</summary>
        public string TestId { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SampleType { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string NormalRangeLow { get; set; } = string.Empty;
        public string NormalRangeHigh { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int TurnaroundHours { get; set; }
        public bool RequiresVerification { get; set; }

        public string PriceText => Price.ToString("N2", CultureInfo.InvariantCulture);
        public string RangeText => RangeEvaluator.FormatRange(NormalRangeLow, NormalRangeHigh, Unit);
        public string TurnaroundText => TurnaroundHours + " h";
        public string VerificationText => RequiresVerification ? "Yes" : "No";
    }

    /// <summary>What the catalog form hands to the repository (an empty TestId means a new test).</summary>
    public class LabTestInput
    {
        public string TestId { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SampleType { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string NormalRangeLow { get; set; } = string.Empty;
        public string NormalRangeHigh { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int TurnaroundHours { get; set; }
        public bool RequiresVerification { get; set; }
    }

    /// <summary>A pending lab order that can receive a result.</summary>
    public class PendingOrder
    {
        /// <summary>LO-YYYYMMDD-XXX</summary>
        public string OrderId { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string TestId { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public string SampleTypeName { get; set; } = string.Empty;
        public string NormalRangeLow { get; set; } = string.Empty;
        public string NormalRangeHigh { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public bool RequiresVerification { get; set; }
        public string UrgencyName { get; set; } = string.Empty;
        public DateTime OrderedAt { get; set; }
        public string DoctorName { get; set; } = string.Empty;

        public string RangeText => RangeEvaluator.FormatRange(NormalRangeLow, NormalRangeHigh, Unit);
        public string OrderedText => OrderedAt.ToString("MMM d, h:mm tt", CultureInfo.InvariantCulture);
        public string VerifyText => RequiresVerification ? "Yes" : "No";
    }

    /// <summary>What the result form hands to the repository.</summary>
    public class LabResultInput
    {
        public string OrderId { get; set; } = string.Empty;
        public string ResultValue { get; set; } = string.Empty;
        public string Findings { get; set; } = string.Empty;
        public int TestedByUserId { get; set; }
        public int? VerifiedByUserId { get; set; }
        public DateTime TestDateTime { get; set; }
    }

    /// <summary>One row of the results history grid.</summary>
    public class LabResultRecord
    {
        /// <summary>LR-YYYYMMDD-XXX</summary>
        public string ResultId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public string ResultValue { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string NormalRangeLow { get; set; } = string.Empty;
        public string NormalRangeHigh { get; set; } = string.Empty;
        public bool IsAbnormal { get; set; }
        public string Findings { get; set; } = string.Empty;
        public string TestedBy { get; set; } = string.Empty;
        public string VerifiedBy { get; set; } = string.Empty;
        public DateTime TestDateTime { get; set; }

        public string ValueText => string.IsNullOrWhiteSpace(Unit) ? ResultValue : $"{ResultValue} {Unit}";
        public string RangeText => RangeEvaluator.FormatRange(NormalRangeLow, NormalRangeHigh, Unit);
        public string FlagText => IsAbnormal ? "Abnormal" : "Normal";
        public string VerifiedText => string.IsNullOrWhiteSpace(VerifiedBy) ? "-" : VerifiedBy;
        public string TestedAtText => TestDateTime.ToString("MMM d, yyyy h:mm tt", CultureInfo.InvariantCulture);
    }

    /// <summary>The result of a save: the generated result id and the flag that was stored.</summary>
    public class LabResultSaved
    {
        public string ResultId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public bool IsAbnormal { get; set; }
    }
}