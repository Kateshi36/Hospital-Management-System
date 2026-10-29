using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP01_PatientRegistration;

namespace HospitalManagementSystem.Views.Shared
{
    /// <summary>
    /// The "Discount card &amp; coverage" fields of a patient, shared by Patient Registration (MP01) and the
    /// Profile Editor (MP02): filling the drop-downs, showing only the boxes that apply, and the checks on save.
    /// </summary>
    public static class BenefitFields
    {
        /// <summary>Fills the discount card, coverage and HMO company drop-downs ("None" chosen).</summary>
        public static void Bind(ComboBox card, ComboBox coverage, ComboBox hmoProvider)
        {
            card.DataSource = new List<LookupItem>
            {
                new LookupItem { Id = BenefitRules.None, Name = "None (no discount card)" },
                new LookupItem { Id = BenefitRules.Senior, Name = "Senior Citizen (OSCA ID)" },
                new LookupItem { Id = BenefitRules.Pwd, Name = "PWD (PWD ID)" }
            };
            card.DisplayMember = nameof(LookupItem.Name);
            card.ValueMember = nameof(LookupItem.Id);
            card.SelectedIndex = 0;

            coverage.DataSource = new List<LookupItem>
            {
                new LookupItem { Id = BenefitRules.None, Name = "None (no PhilHealth / HMO)" },
                new LookupItem { Id = BenefitRules.PhilHealth, Name = "PhilHealth" },
                new LookupItem { Id = BenefitRules.Hmo, Name = "HMO (health card)" }
            };
            coverage.DisplayMember = nameof(LookupItem.Name);
            coverage.ValueMember = nameof(LookupItem.Id);
            coverage.SelectedIndex = 0;

            hmoProvider.Items.Clear();
            hmoProvider.Items.AddRange(BenefitRules.HmoProviders.Cast<object>().ToArray());
            hmoProvider.Text = string.Empty;
        }

        public static string Card(ComboBox card) => card.SelectedItem is LookupItem item ? item.Id : BenefitRules.None;
        public static string Coverage(ComboBox coverage) => coverage.SelectedItem is LookupItem item ? item.Id : BenefitRules.None;

        /// <summary>Selects the stored value ("None" when blank or unknown).</summary>
        public static void Select(ComboBox box, string? value)
        {
            string wanted = string.IsNullOrEmpty(value) ? BenefitRules.None : value;
            foreach (object item in box.Items)
            {
                if (item is LookupItem lookup && lookup.Id == wanted)
                {
                    box.SelectedItem = item;
                    return;
                }
            }
            box.SelectedIndex = 0;
        }

        /// <summary>
        /// Number boxes are editable only when a card / coverage is chosen; the HMO company appears only for an HMO.
        /// The note suggests the Senior Citizen card when the patient is 60 or older.
        /// </summary>
        public static void Update(ComboBox card, Label cardNoLabel, TextBox cardNo, ComboBox coverage, Label coverageNoLabel,
                                  TextBox coverageNo, Label providerLabel, ComboBox provider, Label note, DateTime? dateOfBirth)
        {
            string cardType = Card(card);
            string coverageType = Coverage(coverage);

            bool hasCard = cardType != BenefitRules.None;
            cardNoLabel.Text = BenefitRules.CardNumberLabel(cardType) + (hasCard ? " *" : string.Empty);
            ThemeHelper.SetReadOnlyLook(cardNo, !hasCard);
            cardNo.TabStop = hasCard;
            cardNo.PlaceholderText = hasCard ? "as printed on the ID card" : string.Empty;

            bool hasCoverage = coverageType != BenefitRules.None;
            coverageNoLabel.Text = BenefitRules.CoverageNumberLabel(coverageType) + (hasCoverage ? " *" : string.Empty);
            ThemeHelper.SetReadOnlyLook(coverageNo, !hasCoverage);
            coverageNo.TabStop = hasCoverage;
            coverageNo.PlaceholderText = coverageType == BenefitRules.PhilHealth ? "12-345678901-2"
                                       : coverageType == BenefitRules.Hmo ? "card number" : string.Empty;

            bool hmo = coverageType == BenefitRules.Hmo;
            providerLabel.Visible = hmo;
            provider.Visible = hmo;
            providerLabel.Text = "HMO company *";

            int age = dateOfBirth.HasValue ? Patient.CalculateAge(dateOfBirth.Value, DateTime.Today) : -1;
            if (cardType == BenefitRules.None && age >= BenefitRules.SeniorAge)
            {
                note.ForeColor = ThemeHelper.Warning;
                note.Text = $"This patient is {age} years old. If they show an OSCA / Senior Citizen ID, choose \"Senior Citizen\" " +
                            "so the 20% discount is applied automatically on their bills.";
            }
            else
            {
                note.ForeColor = ThemeHelper.TextMuted;
                note.Text = "Senior Citizen / PWD: 20% off and VAT-exempt (RA 9994 / RA 10754). PhilHealth / HMO coverage is " +
                            "deducted on the bill. Itemized Billing applies these automatically.";
            }
        }

        /// <summary>Adds an error for every problem with the discount card / coverage (see BenefitRules.Validate).</summary>
        public static void Validate(FormValidator validator, ComboBox card, TextBox cardNo, ComboBox coverage, TextBox coverageNo,
                                    ComboBox provider, DateTime? dateOfBirth)
        {
            foreach (var (part, message) in BenefitRules.Validate(Card(card), cardNo.Text, Coverage(coverage), coverageNo.Text,
                                                                     provider.Text, dateOfBirth))
            {
                Control target = part switch
                {
                    "card" => card,
                    "cardNumber" => cardNo,
                    "coverage" => coverage,
                    "coverageNumber" => coverageNo,
                    _ => provider
                };
                validator.AddError(target, message);
            }
        }
    }
}
