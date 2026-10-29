namespace HospitalManagementSystem.Models.MP01_PatientRegistration
{
    /// <summary>A patient with every detail captured at registration.</summary>
    public class Patient
    {
        /// <summary>PAT-YYYYMMDD-XXX - generated on registration; also the primary key.</summary>
        public string PatientId { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public string Allergies { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;

        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;
        public string EmergencyContactRelation { get; set; } = string.Empty;

        public DateTime RegisteredAt { get; set; }

        // Discount card and insurance coverage (applied automatically in Itemized Billing)
        public string DiscountCardType { get; set; } = BenefitRules.None;     // None / Senior Citizen / PWD
        public string DiscountCardNumber { get; set; } = string.Empty;        // OSCA ID / PWD ID number
        public string CoverageType { get; set; } = BenefitRules.None;         // None / PhilHealth / HMO
        public string CoverageNumber { get; set; } = string.Empty;            // PhilHealth PIN / HMO card number
        public string CoverageProvider { get; set; } = string.Empty;          // HMO company

        public bool HasDiscountCard => DiscountCardType != BenefitRules.None && DiscountCardType.Length > 0;
        public bool HasCoverage => CoverageType != BenefitRules.None && CoverageType.Length > 0;

        /// <summary>"Senior Citizen (OSCA-123)  ·  PhilHealth 12-345678901-2", or "None".</summary>
        public string BenefitsText => BenefitRules.Describe(DiscountCardType, DiscountCardNumber, CoverageType, CoverageNumber, CoverageProvider);

        /// <summary>Age in whole years, computed from the date of birth (never typed or stored).</summary>
        public int Age => CalculateAge(DateOfBirth, DateTime.Today);

        /// <summary>"Maria Clara Santos"</summary>
        public string FullName =>
            string.Join(" ", new[] { FirstName, MiddleName, LastName }.Where(part => !string.IsNullOrWhiteSpace(part)));

        /// <summary>True when an emergency contact was entered (all three parts are required together).</summary>
        public bool HasEmergencyContact =>
            !string.IsNullOrWhiteSpace(EmergencyContactName) &&
            !string.IsNullOrWhiteSpace(EmergencyContactPhone) &&
            !string.IsNullOrWhiteSpace(EmergencyContactRelation);

        /// <summary>Whole years between two dates (birthday-aware).</summary>
        public static int CalculateAge(DateTime dateOfBirth, DateTime today)
        {
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return Math.Max(age, 0);
        }
    }

    /// <summary>
    /// Discount cards and coverage a patient can have on file. The registration form, the profile editor and
    /// Itemized Billing all use these values and the same checks.
    ///   * Senior Citizen (RA 9994, age 60+) / PWD (RA 10754): 20 % discount and VAT-exempt; the ID number is required.
    ///   * PhilHealth: the 12-digit PhilHealth Identification Number (PIN), shown as 12-345678901-2.
    ///   * HMO: the card number and the HMO company.
    /// </summary>
    public static class BenefitRules
    {
        public const string None = "None";
        public const string Senior = "Senior Citizen";
        public const string Pwd = "PWD";
        public const string PhilHealth = "PhilHealth";
        public const string Hmo = "HMO";

        public static readonly string[] DiscountCards = { None, Senior, Pwd };
        public static readonly string[] Coverages = { None, PhilHealth, Hmo };

        public const int SeniorAge = 60;
        public const int MaxCardNumber = 30;
        public const int MaxProvider = 80;

        /// <summary>Common HMO companies for the drop-down (a different one can be typed).</summary>
        public static readonly string[] HmoProviders =
        {
            "Maxicare", "Intellicare", "Medicard", "PhilCare", "Cocolife", "AXA", "Pacific Cross", "Avega", "EastWest Healthcare", "ValuCare"
        };

        /// <summary>Card / ID numbers: letters, digits, space, '-' and '/'.</summary>
        public static bool IsValidCardNumber(string text) =>
            text.Length > 0 && text.Length <= MaxCardNumber && text.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '/' || c == ' ');

        /// <summary>The digits of a PhilHealth number (12 = valid).</summary>
        public static string PhilHealthDigits(string text) => new string((text ?? string.Empty).Where(char.IsDigit).ToArray());

        /// <summary>12-345678901-2 when the text has 12 digits; otherwise the text as typed.</summary>
        public static string FormatPhilHealth(string text)
        {
            string d = PhilHealthDigits(text);
            return d.Length == 12 ? $"{d.Substring(0, 2)}-{d.Substring(2, 9)}-{d.Substring(11, 1)}" : (text ?? string.Empty).Trim();
        }

        /// <summary>What the "ID number" box is called for a discount card.</summary>
        public static string CardNumberLabel(string cardType) =>
            cardType == Senior ? "OSCA / Senior ID no." : cardType == Pwd ? "PWD ID no." : "ID number";

        /// <summary>What the "number" box is called for a coverage.</summary>
        public static string CoverageNumberLabel(string coverageType) =>
            coverageType == PhilHealth ? "PhilHealth PIN" : coverageType == Hmo ? "HMO card no." : "Member no.";

        /// <summary>
        /// Checks the discount card and coverage of a patient. Returns (which part, message) for every problem:
        /// part is "card", "cardNumber", "coverage", "coverageNumber" or "provider".
        /// </summary>
        public static List<(string Part, string Message)> Validate(string cardType, string cardNumber, string coverageType,
                                                                   string coverageNumber, string provider, DateTime? dateOfBirth)
        {
            var issues = new List<(string, string)>();
            cardType = string.IsNullOrEmpty(cardType) ? None : cardType;
            coverageType = string.IsNullOrEmpty(coverageType) ? None : coverageType;
            cardNumber = (cardNumber ?? string.Empty).Trim();
            coverageNumber = (coverageNumber ?? string.Empty).Trim();
            provider = (provider ?? string.Empty).Trim();

            if (!DiscountCards.Contains(cardType)) issues.Add(("card", "Please choose a valid discount card."));
            else if (cardType != None)
            {
                if (cardNumber.Length == 0)
                    issues.Add(("cardNumber", $"Enter the {CardNumberLabel(cardType)} printed on the {cardType} card."));
                else if (!IsValidCardNumber(cardNumber))
                    issues.Add(("cardNumber", $"The {CardNumberLabel(cardType)} may contain letters, digits, spaces, '-' and '/' only (at most {MaxCardNumber} characters)."));

                if (cardType == Senior && dateOfBirth.HasValue && Patient.CalculateAge(dateOfBirth.Value, DateTime.Today) < SeniorAge)
                    issues.Add(("card", $"A Senior Citizen card is only for patients aged {SeniorAge} or older."));
            }

            if (!Coverages.Contains(coverageType)) issues.Add(("coverage", "Please choose a valid coverage."));
            else if (coverageType == PhilHealth)
            {
                if (coverageNumber.Length == 0) issues.Add(("coverageNumber", "Enter the 12-digit PhilHealth Identification Number (PIN)."));
                else if (PhilHealthDigits(coverageNumber).Length != 12 || coverageNumber.Any(c => !char.IsDigit(c) && c != '-' && c != ' '))
                    issues.Add(("coverageNumber", "The PhilHealth PIN has exactly 12 digits (for example 12-345678901-2)."));
            }
            else if (coverageType == Hmo)
            {
                if (coverageNumber.Length == 0) issues.Add(("coverageNumber", "Enter the HMO card number."));
                else if (!IsValidCardNumber(coverageNumber))
                    issues.Add(("coverageNumber", $"The HMO card number may contain letters, digits, spaces, '-' and '/' only (at most {MaxCardNumber} characters)."));
                if (provider.Length == 0) issues.Add(("provider", "Choose or type the HMO company (for example Maxicare)."));
                else if (provider.Length > MaxProvider) issues.Add(("provider", $"The HMO company can have at most {MaxProvider} characters."));
            }
            return issues;
        }

        public static string Describe(string cardType, string cardNumber, string coverageType, string coverageNumber, string provider)
        {
            var parts = new List<string>();
            if (!string.IsNullOrEmpty(cardType) && cardType != None)
                parts.Add($"{cardType} ({cardNumber})");
            if (coverageType == PhilHealth) parts.Add($"PhilHealth {FormatPhilHealth(coverageNumber)}");
            else if (coverageType == Hmo) parts.Add($"HMO {provider} ({coverageNumber})");
            return parts.Count == 0 ? "None" : string.Join("  ·  ", parts);
        }
    }
}
