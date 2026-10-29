using System.Data;
using System.Globalization;
using System.Text;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP01_PatientRegistration;
using HospitalManagementSystem.Models.MP11_Billing;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.MP11_Billing
{
    /// <summary>
    /// All SQL for Itemized Billing (MP11, Step 15).
    ///
    /// A bill is Draft (editable) or Finalized (locked). Its id BILL-YYYYMMDD-XXX is assigned when it is first saved.
    ///
    /// SAVE (one transaction, used by "Save Draft" and by "Finalize Bill")
    ///   1. a new bill gets its id from GeneratorHelper INSIDE this transaction and is inserted; an existing bill row is locked (FOR UPDATE) and must still be a Draft;
    ///   2. every rule is checked again (Validate): patient, item amounts above 0, discount not above the subtotal,
    ///      tax rate 0-100 %, Authorized By when the discount is above the threshold;
    ///   3. the items are replaced (delete + insert), so the saved bill always equals what the screen showed;
    ///   4. when finalizing: at least one item, the status becomes Finalized and finalized_at is set.
    /// Once Finalized the repository refuses every further change, so the lock does not depend on the screen alone.
    ///
    /// LINKED CHARGES: consultations, medicines dispensed with "Charge to Bill" and lab orders that are not on a bill yet
    /// come from the view v_unbilled_charges (GetUnbilledCharges). Such an item keeps source_type / source_id, and Save checks
    /// again that the charge belongs to the patient and is not on another bill, so a service is never billed twice.
    ///
    /// CORRECTIONS: a finalized bill is never edited. VoidBill marks it Voided (reason, who, when - only when nothing is left
    /// paid on it, so payments must be refunded first) which also releases its linked charges, and can create a new Draft
    /// copy (corrects_bill_id) to be fixed and finalized again. Every save, finalize, void and delete is written to audit_log.
    /// </summary>
    public class BillingRepository
    {
        private const string BillSelect =
            "SELECT b.bill_id, b.patient_id, CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
            "       b.status, b.discount_type, b.discount_value, b.discount_id_number, b.tax_rate, b.authorized_by, " +
            "       b.created_at, b.finalized_at, u.full_name AS created_by, " +
            "       b.voided_at, b.void_reason, vu.full_name AS voided_by, b.corrects_bill_id, " +
            "       b.coverage_type, b.coverage_number, b.coverage_provider, b.coverage_amount " +
            "FROM bills b " +
            "JOIN patients p ON p.patient_id = b.patient_id " +
            "LEFT JOIN users u ON u.user_id = b.created_by_user_id " +
            "LEFT JOIN users vu ON vu.user_id = b.voided_by_user_id ";

        // ------------------------------------------------------------------
        // Lists
        // ------------------------------------------------------------------

        public List<LookupItem> GetCategories() => BillingRules.Categories.Select(LookupItem.Of).ToList();

        public List<LookupItem> GetDiscountTypes() => BillingRules.DiscountTypes.Select(LookupItem.Of).ToList();

        private static List<LookupItem> LoadLookup(string sql, params MySqlParameter[] parameters)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(sql, parameters);
            return table.Rows.Cast<DataRow>()
                .Select(r => new LookupItem { Id = DatabaseHelper.GetString(r, "id"), Name = DatabaseHelper.GetString(r, "name") })
                .ToList();
        }

        /// <summary>Adds "AND (col1 LIKE @w0 OR col2 LIKE @w0) AND (...)" for every word typed in a search box.</summary>
        private static void AppendWords(StringBuilder sql, List<MySqlParameter> parameters, string? text, params string[] columns)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                string name = "@w" + i.ToString(CultureInfo.InvariantCulture);
                sql.Append(" AND (").Append(string.Join(" OR ", columns.Select(c => c + " LIKE " + name))).Append(')');
                string escaped = words[i].Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");
                parameters.Add(DatabaseHelper.Param(name, "%" + escaped + "%"));
            }
        }

        /// <summary>Patients matching the typed words (patient ID, first or last name). The text is shown as "PAT-... - Last, First".</summary>
        public List<LookupItem> GetPatients(string? text)
        {
            var sql = new StringBuilder(
                "SELECT patient_id AS id, CONCAT(patient_id, '  -  ', last_name, ', ', first_name) AS name FROM patients WHERE 1 = 1");
            var parameters = new List<MySqlParameter>();
            AppendWords(sql, parameters, text, "patient_id", "first_name", "last_name");
            sql.Append(" ORDER BY last_name, first_name LIMIT ").Append(BillingRules.PatientListLimit.ToString(CultureInfo.InvariantCulture));
            return LoadLookup(sql.ToString(), parameters.ToArray());
        }

        public LookupItem? GetPatient(string patientId)
        {
            List<LookupItem> list = LoadLookup(
                "SELECT patient_id AS id, CONCAT(patient_id, '  -  ', last_name, ', ', first_name) AS name FROM patients WHERE patient_id = @id",
                DatabaseHelper.Param("@id", patientId));
            return list.FirstOrDefault();
        }

        /// <summary>The price list of a category: services (consultation, procedure, miscellaneous), medicines, lab tests or room types.</summary>
        public List<PriceListItem> GetPriceList(string category)
        {
            string sql;
            string suffix = string.Empty;
            switch (category)
            {
                case BillingRules.CategoryMedicine:
                    sql = "SELECT medicine_id AS id, medicine_name AS name, unit_price AS price FROM medicines ORDER BY medicine_name";
                    break;
                case BillingRules.CategoryLabTest:
                    sql = "SELECT test_id AS id, test_name AS name, price AS price FROM lab_tests ORDER BY test_name";
                    break;
                case BillingRules.CategoryRoom:
                    sql = "SELECT room_type AS id, room_type AS name, daily_rate AS price FROM room_types ORDER BY daily_rate";
                    suffix = " Room";
                    break;
                default:
                    sql = "SELECT service_name AS id, service_name AS name, unit_price AS price FROM service_catalog " +
                          "WHERE bill_category = @category ORDER BY service_name";
                    break;
            }

            string unit = category == BillingRules.CategoryRoom ? " per day" : string.Empty;
            DataTable table = DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.Param("@category", category));
            return table.Rows.Cast<DataRow>().Select(r =>
            {
                string name = DatabaseHelper.GetString(r, "name") + suffix;
                decimal price = DatabaseHelper.GetDecimal(r, "price");
                return new PriceListItem
                {
                    Id = DatabaseHelper.GetString(r, "id"),
                    Description = name,
                    UnitPrice = price,
                    DisplayText = name + "   -   " + SettingsHelper.FormatMoney(price) + unit
                };
            }).ToList();
        }

        // ------------------------------------------------------------------
        // Reading bills
        // ------------------------------------------------------------------

        public Bill? GetBill(string billId)
        {
            DataTable header = DatabaseHelper.ExecuteQuery(BillSelect + "WHERE b.bill_id = @id", DatabaseHelper.Param("@id", billId));
            if (header.Rows.Count == 0) return null;
            return ReadBill(header.Rows[0], LoadItems(null, null, billId));
        }

        private static List<BillItem> LoadItems(MySqlConnection? conn, MySqlTransaction? tx, string billId)
        {
            const string sql =
                "SELECT bi.bill_item_id, bi.bill_category, bi.description, bi.quantity, bi.unit_price, bi.source_type, bi.source_id " +
                "FROM bill_items bi " +
                "WHERE bi.bill_id = @id ORDER BY bi.bill_item_id";
            DataTable table = conn == null
                ? DatabaseHelper.ExecuteQuery(sql, DatabaseHelper.Param("@id", billId))
                : DatabaseHelper.ExecuteQuery(conn, tx, sql, DatabaseHelper.Param("@id", billId));

            return table.Rows.Cast<DataRow>().Select(r => new BillItem
            {
                BillItemId = DatabaseHelper.GetInt(r, "bill_item_id"),
                Category = DatabaseHelper.GetString(r, "bill_category"),
                Description = DatabaseHelper.GetString(r, "description"),
                Quantity = DatabaseHelper.GetInt(r, "quantity"),
                UnitPrice = DatabaseHelper.GetDecimal(r, "unit_price"),
                SourceType = NullIfEmpty(DatabaseHelper.GetString(r, "source_type")),
                SourceId = NullIfEmpty(DatabaseHelper.GetString(r, "source_id"))
            }).ToList();
        }

        private static string? NullIfEmpty(string value) => string.IsNullOrEmpty(value) ? null : value;

        private static Bill ReadBill(DataRow row, List<BillItem> items) => new Bill
        {
            BillId = DatabaseHelper.GetString(row, "bill_id"),
            PatientId = DatabaseHelper.GetString(row, "patient_id"),
            PatientName = DatabaseHelper.GetString(row, "patient_name"),
            Status = DatabaseHelper.GetString(row, "status"),
            DiscountType = DatabaseHelper.GetString(row, "discount_type"),
            DiscountValue = DatabaseHelper.GetDecimal(row, "discount_value"),
            DiscountIdNumber = DatabaseHelper.GetString(row, "discount_id_number"),
            TaxRate = DatabaseHelper.GetDecimal(row, "tax_rate"),
            AuthorizedBy = DatabaseHelper.GetString(row, "authorized_by"),
            CreatedAt = DatabaseHelper.GetNullableDate(row, "created_at") ?? DateTime.Now,
            FinalizedAt = DatabaseHelper.GetNullableDate(row, "finalized_at"),
            CreatedBy = DatabaseHelper.GetString(row, "created_by"),
            VoidedAt = DatabaseHelper.GetNullableDate(row, "voided_at"),
            VoidedBy = DatabaseHelper.GetString(row, "voided_by"),
            VoidReason = DatabaseHelper.GetString(row, "void_reason"),
            CorrectsBillId = DatabaseHelper.GetString(row, "corrects_bill_id"),
            CoverageType = DatabaseHelper.GetString(row, "coverage_type") is { Length: > 0 } coverage ? coverage : BillingRules.CoverageNone,
            CoverageNumber = DatabaseHelper.GetString(row, "coverage_number"),
            CoverageProvider = DatabaseHelper.GetString(row, "coverage_provider"),
            CoverageAmount = DatabaseHelper.GetDecimal(row, "coverage_amount"),
            Items = items
        };

        // ------------------------------------------------------------------
        // Charges from other modules (consultation, dispensing, laboratory)
        // ------------------------------------------------------------------

        /// <summary>The patient's consultations, "Charge to Bill" medicines and lab orders that are not on any bill yet (oldest first).</summary>
        public List<UnbilledCharge> GetUnbilledCharges(string patientId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT source_type, source_id, bill_category, description, quantity, unit_price, charge_date " +
                "FROM v_unbilled_charges WHERE patient_id = @patient ORDER BY charge_date, source_type, source_id",
                DatabaseHelper.Param("@patient", patientId));
            return table.Rows.Cast<DataRow>().Select(r => new UnbilledCharge
            {
                SourceType = DatabaseHelper.GetString(r, "source_type"),
                SourceId = DatabaseHelper.GetString(r, "source_id"),
                Category = DatabaseHelper.GetString(r, "bill_category"),
                Description = DatabaseHelper.GetString(r, "description"),
                Quantity = DatabaseHelper.GetInt(r, "quantity"),
                UnitPrice = DatabaseHelper.GetDecimal(r, "unit_price"),
                ChargeDate = DatabaseHelper.GetNullableDate(r, "charge_date") ?? DateTime.Now
            }).ToList();
        }

        /// <summary>How many unbilled charges the patient has (shown on the "Unbilled charges" button).</summary>
        public int CountUnbilledCharges(string patientId)
        {
            object? value = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM v_unbilled_charges WHERE patient_id = @patient", DatabaseHelper.Param("@patient", patientId));
            return value == null || value == DBNull.Value ? 0 : Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        /// <summary>The "All Bills" list. An empty status = every status; the totals come from the view v_bill_totals.</summary>
        public List<BillListRow> GetBills(string? text, string status)
        {
            var sql = new StringBuilder(
                "SELECT b.bill_id, b.status, p.patient_id, " +
                "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, ps.payment_status, ps.amount_paid, " +
                "       t.subtotal, t.discount_amount, t.tax_amount, t.grand_total, b.created_at, " +
                "       (SELECT COUNT(*) FROM bill_items bi WHERE bi.bill_id = b.bill_id) AS item_count " +
                "FROM bills b " +
                "JOIN patients p ON p.patient_id = b.patient_id " +
                "JOIN v_bill_totals t ON t.bill_id = b.bill_id " +
                "JOIN v_bill_payment_summary ps ON ps.bill_id = b.bill_id " +
                "WHERE 1 = 1");
            var parameters = new List<MySqlParameter>();
            if (!string.IsNullOrEmpty(status))
            {
                sql.Append(" AND b.status = @status");
                parameters.Add(DatabaseHelper.Param("@status", status));
            }
            AppendWords(sql, parameters, text, "b.bill_id", "p.patient_id", "p.first_name", "p.last_name");
            sql.Append(" ORDER BY b.created_at DESC, b.bill_id DESC LIMIT ").Append(BillingRules.ListLimit.ToString(CultureInfo.InvariantCulture));

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(r => new BillListRow
            {
                BillId = DatabaseHelper.GetString(r, "bill_id"),
                PatientId = DatabaseHelper.GetString(r, "patient_id"),
                PatientName = DatabaseHelper.GetString(r, "patient_name"),
                Status = DatabaseHelper.GetString(r, "status"),
                PaymentStatus = DatabaseHelper.GetString(r, "payment_status"),
                ItemCount = DatabaseHelper.GetInt(r, "item_count"),
                Subtotal = DatabaseHelper.GetDecimal(r, "subtotal"),
                DiscountAmount = DatabaseHelper.GetDecimal(r, "discount_amount"),
                TaxAmount = DatabaseHelper.GetDecimal(r, "tax_amount"),
                GrandTotal = DatabaseHelper.GetDecimal(r, "grand_total"),
                AmountPaid = DatabaseHelper.GetDecimal(r, "amount_paid"),
                CreatedAt = DatabaseHelper.GetNullableDate(r, "created_at") ?? DateTime.Now
            }).ToList();
        }

        // ------------------------------------------------------------------
        // Rules
        // ------------------------------------------------------------------

        /// <summary>True when the discount of this bill is above the configured threshold (Authorized By is then required to finalize).</summary>
        public static bool NeedsAuthorization(Bill bill)
        {
            decimal percentThreshold = SettingsHelper.GetDecimal(SettingsHelper.DiscountThresholdPercent, 20m);
            decimal amountThreshold = SettingsHelper.GetDecimal(SettingsHelper.DiscountThresholdAmount, 5000m);
            return BillCalculator.NeedsAuthorization(bill.Subtotal, bill.DiscountType, bill.DiscountValue, percentThreshold, amountThreshold);
        }

        /// <summary>
        /// Every rule that needs no database. The screen calls this before saving and shows each message next to its field;
        /// the repository calls it again, so the rules also hold if another screen ever saves a bill.
        /// </summary>
        public static List<BillingIssue> Validate(Bill bill, bool finalize)
        {
            var issues = new List<BillingIssue>();

            if (string.IsNullOrEmpty(bill.PatientId)) issues.Add(new BillingIssue(BillingField.Patient, "Choose the patient for this bill."));

            if (bill.Items.Count > BillingRules.MaxItems)
                issues.Add(new BillingIssue(BillingField.Items, $"A bill can have at most {BillingRules.MaxItems} items."));

            int number = 0;
            foreach (BillItem item in bill.Items)
            {
                number++;
                string label = "Item " + number.ToString(CultureInfo.InvariantCulture);
                string description = (item.Description ?? string.Empty).Trim();

                if (description.Length == 0)
                    issues.Add(new BillingIssue(BillingField.Items, label + ": the description is required."));
                else if (description.Length > BillingRules.MaxDescription)
                    issues.Add(new BillingIssue(BillingField.Items, $"{label}: the description can have at most {BillingRules.MaxDescription} characters."));

                if (!BillingRules.Categories.Contains(item.Category))
                    issues.Add(new BillingIssue(BillingField.Items, label + ": the category is not valid."));

                int maxQuantity = item.Category == BillingRules.CategoryRoom ? BillingRules.MaxRoomDays : BillingRules.MaxQuantity;
                if (item.Quantity < 1 || item.Quantity > maxQuantity)
                    issues.Add(new BillingIssue(BillingField.Items,
                        $"{label} ({description}): the {(item.Category == BillingRules.CategoryRoom ? "number of days" : "quantity")} must be a whole number from 1 to {maxQuantity}."));

                if (item.UnitPrice <= 0 || item.UnitPrice > BillingRules.MaxUnitPrice)
                    issues.Add(new BillingIssue(BillingField.Items,
                        $"{label} ({description}): the price must be greater than 0 and at most {BillingRules.MaxUnitPrice:N2}."));
                else if (BillCalculator.Round2(item.UnitPrice) != item.UnitPrice)
                    issues.Add(new BillingIssue(BillingField.Items, $"{label} ({description}): the price can have at most 2 decimal places."));
            }

            if (finalize && bill.Items.Count == 0)
                issues.Add(new BillingIssue(BillingField.Items, "A bill must contain at least one item before it can be finalized."));

            var duplicate = bill.Items.Where(i => i.IsLinked)
                .GroupBy(i => (i.SourceType, i.SourceId)).FirstOrDefault(g => g.Count() > 1);
            if (duplicate != null)
                issues.Add(new BillingIssue(BillingField.Items, $"\"{duplicate.First().Description}\" is on this bill more than once."));

            // ---- discount ----
            decimal subtotal = bill.Subtotal;
            bool discountOk = true;
            if (!BillingRules.DiscountTypes.Contains(bill.DiscountType))
            {
                discountOk = false;
                issues.Add(new BillingIssue(BillingField.Discount, "Choose the discount type."));
            }
            else if (BillingRules.IsStatutory(bill.DiscountType))
            {
                // Senior Citizen / PWD: fixed percentage, VAT-exempt, and the ID card number is the proof
                decimal statutory = StatutoryPercent();
                if (bill.DiscountValue != statutory || bill.TaxRate != 0m)
                {
                    discountOk = false;
                    issues.Add(new BillingIssue(BillingField.Discount,
                        $"The {bill.DiscountType} discount is fixed at {statutory:0.##}% and the bill is VAT-exempt (tax 0%)."));
                }
                string id = (bill.DiscountIdNumber ?? string.Empty).Trim();
                if (id.Length == 0)
                    issues.Add(new BillingIssue(BillingField.DiscountId,
                        bill.DiscountType == BillingRules.DiscountSenior
                            ? "Enter the Senior Citizen (OSCA) ID number shown on the card."
                            : "Enter the PWD ID number shown on the card."));
                else if (id.Length > BillingRules.MaxDiscountId)
                    issues.Add(new BillingIssue(BillingField.DiscountId, $"The ID number can have at most {BillingRules.MaxDiscountId} characters."));
                else if (!id.All(c => char.IsLetterOrDigit(c) || c == '-' || c == ' ' || c == '/'))
                    issues.Add(new BillingIssue(BillingField.DiscountId, "The ID number may contain letters, digits, spaces, '-' and '/' only."));
            }
            else if (bill.DiscountValue < 0)
            {
                discountOk = false;
                issues.Add(new BillingIssue(BillingField.Discount, "The discount cannot be negative."));
            }
            else if (BillCalculator.Round2(bill.DiscountValue) != bill.DiscountValue)
            {
                discountOk = false;
                issues.Add(new BillingIssue(BillingField.Discount, "The discount can have at most 2 decimal places."));
            }
            else if (bill.DiscountType == BillingRules.DiscountPercent && bill.DiscountValue > 100m)
            {
                discountOk = false;
                issues.Add(new BillingIssue(BillingField.Discount, "A percentage discount cannot be more than 100% (the discount cannot exceed the subtotal)."));
            }
            else if (bill.DiscountType == BillingRules.DiscountFlat &&
                     (bill.DiscountValue > subtotal || bill.DiscountValue > BillingRules.MaxFlatDiscount))
            {
                discountOk = false;
                issues.Add(new BillingIssue(BillingField.Discount,
                    $"The discount ({SettingsHelper.FormatMoney(bill.DiscountValue)}) cannot be more than the subtotal ({SettingsHelper.FormatMoney(subtotal)})."));
            }

            // ---- tax ----
            if (bill.TaxRate < 0m || bill.TaxRate > 1m)
                issues.Add(new BillingIssue(BillingField.Tax, "The tax rate must be between 0% and 100%."));

            // ---- authorization ----
            if (discountOk && NeedsAuthorization(bill))
            {
                string who = (bill.AuthorizedBy ?? string.Empty).Trim();
                if (who.Length == 0)
                {
                    if (finalize)
                        issues.Add(new BillingIssue(BillingField.Authorization,
                            "This discount is above the authorization threshold. Enter who authorized it in \"Authorized By\" before finalizing."));
                }
                else if (!ValidationHelper.IsValidPersonName(who))
                    issues.Add(new BillingIssue(BillingField.Authorization,
                        "Authorized By may contain letters, spaces, hyphens, apostrophes and periods only (no numbers)."));
                else if (who.Length > BillingRules.MaxAuthorizedBy)
                    issues.Add(new BillingIssue(BillingField.Authorization,
                        $"Authorized By can have at most {BillingRules.MaxAuthorizedBy} characters."));
            }

            // ---- PhilHealth / HMO coverage ----
            string coverageType = string.IsNullOrEmpty(bill.CoverageType) ? BillingRules.CoverageNone : bill.CoverageType;
            if (!BillingRules.CoverageTypes.Contains(coverageType))
                issues.Add(new BillingIssue(BillingField.Coverage, "Choose None, PhilHealth or HMO."));
            else if (coverageType == BillingRules.CoverageNone)
            {
                if (bill.CoverageAmount != 0m)
                    issues.Add(new BillingIssue(BillingField.CoverageAmount, "Choose PhilHealth or HMO before entering a covered amount."));
            }
            else
            {
                foreach (var (part, message) in BenefitRules.Validate(BenefitRules.None, string.Empty, coverageType,
                                                                       bill.CoverageNumber, bill.CoverageProvider, null))
                {
                    issues.Add(new BillingIssue(part == "provider" ? BillingField.CoverageProvider : BillingField.CoverageNumber, message));
                }

                BillTotals totals = BillCalculator.Calculate(bill.Subtotal, bill.DiscountType, bill.DiscountValue, bill.TaxRate);
                if (bill.CoverageAmount < 0m)
                    issues.Add(new BillingIssue(BillingField.CoverageAmount, "The covered amount cannot be negative."));
                else if (BillCalculator.Round2(bill.CoverageAmount) != bill.CoverageAmount)
                    issues.Add(new BillingIssue(BillingField.CoverageAmount, "The covered amount can have at most 2 decimal places."));
                else if (discountOk && bill.CoverageAmount > totals.GrossTotal)
                    issues.Add(new BillingIssue(BillingField.CoverageAmount,
                        $"The {coverageType} coverage ({SettingsHelper.FormatMoney(bill.CoverageAmount)}) cannot be more than the bill total ({SettingsHelper.FormatMoney(totals.GrossTotal)})."));
                else if (finalize && bill.CoverageAmount == 0m)
                    issues.Add(new BillingIssue(BillingField.CoverageAmount,
                        $"Enter the amount {coverageType} covers (from the approved claim / letter of authorization), or choose None if it does not apply to this bill."));
            }

            return issues;
        }

        /// <summary>The Senior Citizen / PWD discount percentage (app_settings, 20 by law).</summary>
        public static decimal StatutoryPercent() => SettingsHelper.GetDecimal(SettingsHelper.SeniorPwdDiscountPercent, 20m);

        // ------------------------------------------------------------------
        // Saving
        // ------------------------------------------------------------------

        public Bill SaveDraft(Bill bill) => Save(bill, finalize: false);

        public Bill Finalize(Bill bill) => Save(bill, finalize: true);

        private Bill Save(Bill bill, bool finalize)
        {
            List<BillingIssue> issues = Validate(bill, finalize);
            if (issues.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, issues.Select(i => i.Message)));

            // The authorization name is kept only when the discount really needs it; the ID number only for Senior Citizen / PWD.
            string? authorizedBy = NeedsAuthorization(bill) && bill.AuthorizedBy.Trim().Length > 0 ? bill.AuthorizedBy.Trim() : null;
            string? discountId = BillingRules.IsStatutory(bill.DiscountType) ? bill.DiscountIdNumber.Trim() : null;
            bool isNew = bill.BillId.Length == 0;
            bool covered = bill.HasCoverage;
            string coverageType = covered ? bill.CoverageType : BillingRules.CoverageNone;
            string? coverageNumber = !covered ? null
                : coverageType == BillingRules.CoveragePhilHealth ? BenefitRules.FormatPhilHealth(bill.CoverageNumber) : bill.CoverageNumber.Trim();
            string? coverageProvider = coverageType == BillingRules.CoverageHmo ? bill.CoverageProvider.Trim() : null;
            decimal coverageAmount = covered ? bill.CoverageAmount : 0m;

            string billId = DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                object? patient = DatabaseHelper.ExecuteScalar(conn, tx,
                    "SELECT patient_id FROM patients WHERE patient_id = @id", DatabaseHelper.Param("@id", bill.PatientId));
                if (patient == null) throw new InvalidOperationException("This patient no longer exists.");

                string id = bill.BillId;
                if (id.Length == 0)
                {
                    // The id is generated INSIDE this transaction, so a failed save leaves no gap in the numbers.
                    id = GeneratorHelper.Generate(GeneratorHelper.Prefix.Bill, conn, tx);
                    DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "INSERT INTO bills (bill_id, patient_id, status, discount_type, discount_value, discount_id_number, tax_rate, authorized_by, created_by_user_id, corrects_bill_id, " +
                        "                   coverage_type, coverage_number, coverage_provider, coverage_amount) " +
                        "VALUES (@id, @patient, @status, @dtype, @dvalue, @did, @tax, @auth, @user, @corrects, @ctype, @cno, @cprov, @camount)",
                        DatabaseHelper.Param("@id", id),
                        DatabaseHelper.Param("@patient", bill.PatientId),
                        DatabaseHelper.Param("@status", BillingRules.StatusDraft),
                        DatabaseHelper.Param("@dtype", bill.DiscountType),
                        DatabaseHelper.Param("@dvalue", bill.DiscountValue),
                        DatabaseHelper.Param("@did", discountId),
                        DatabaseHelper.Param("@tax", bill.TaxRate),
                        DatabaseHelper.Param("@auth", authorizedBy),
                        DatabaseHelper.Param("@user", SessionManager.CurrentUserId),
                        DatabaseHelper.Param("@corrects", string.IsNullOrEmpty(bill.CorrectsBillId) ? null : bill.CorrectsBillId),
                        DatabaseHelper.Param("@ctype", coverageType),
                        DatabaseHelper.Param("@cno", coverageNumber),
                        DatabaseHelper.Param("@cprov", coverageProvider),
                        DatabaseHelper.Param("@camount", coverageAmount));
                }
                else
                {
                    DataTable locked = DatabaseHelper.ExecuteQuery(conn, tx,
                        "SELECT status FROM bills WHERE bill_id = @id FOR UPDATE", DatabaseHelper.Param("@id", id));
                    if (locked.Rows.Count == 0)
                        throw new InvalidOperationException("This bill no longer exists. Open the list of bills again.");
                    string current = DatabaseHelper.GetString(locked.Rows[0], "status");
                    if (current == BillingRules.StatusVoided)
                        throw new InvalidOperationException("This bill has been voided and can no longer be changed.");
                    if (current != BillingRules.StatusDraft)
                        throw new InvalidOperationException("This bill has already been finalized and can no longer be changed.");

                    DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "UPDATE bills SET patient_id = @patient, discount_type = @dtype, discount_value = @dvalue, " +
                        "  discount_id_number = @did, tax_rate = @tax, authorized_by = @auth, " +
                        "  coverage_type = @ctype, coverage_number = @cno, coverage_provider = @cprov, coverage_amount = @camount WHERE bill_id = @id",
                        DatabaseHelper.Param("@patient", bill.PatientId),
                        DatabaseHelper.Param("@dtype", bill.DiscountType),
                        DatabaseHelper.Param("@dvalue", bill.DiscountValue),
                        DatabaseHelper.Param("@did", discountId),
                        DatabaseHelper.Param("@tax", bill.TaxRate),
                        DatabaseHelper.Param("@auth", authorizedBy),
                        DatabaseHelper.Param("@ctype", coverageType),
                        DatabaseHelper.Param("@cno", coverageNumber),
                        DatabaseHelper.Param("@cprov", coverageProvider),
                        DatabaseHelper.Param("@camount", coverageAmount),
                        DatabaseHelper.Param("@id", id));

                    DatabaseHelper.ExecuteNonQuery(conn, tx, "DELETE FROM bill_items WHERE bill_id = @id", DatabaseHelper.Param("@id", id));
                }

                foreach (BillItem item in bill.Items)
                {
                    if (item.IsLinked)
                    {
                        // the charge must still be unbilled (this bill's own old items were deleted above) and be this patient's
                        object? free = DatabaseHelper.ExecuteScalar(conn, tx,
                            "SELECT 1 FROM v_unbilled_charges WHERE source_type = @type AND source_id = @source AND patient_id = @patient",
                            DatabaseHelper.Param("@type", item.SourceType),
                            DatabaseHelper.Param("@source", item.SourceId),
                            DatabaseHelper.Param("@patient", bill.PatientId));
                        if (free == null)
                            throw new InvalidOperationException(
                                $"\"{item.Description}\" is already on another bill (or does not belong to this patient). Remove it from this bill.");
                    }

                    DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "INSERT INTO bill_items (bill_id, bill_category, description, quantity, unit_price, source_type, source_id) " +
                        "VALUES (@bill, @category, @description, @quantity, @price, @stype, @sid)",
                        DatabaseHelper.Param("@bill", id),
                        DatabaseHelper.Param("@category", item.Category),
                        DatabaseHelper.Param("@description", item.Description.Trim()),
                        DatabaseHelper.Param("@quantity", item.Quantity),
                        DatabaseHelper.Param("@price", item.UnitPrice),
                        DatabaseHelper.Param("@stype", item.IsLinked ? item.SourceType : null),
                        DatabaseHelper.Param("@sid", item.IsLinked ? item.SourceId : null));
                }

                if (finalize)
                {
                    int changed = DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "UPDATE bills SET status = @final, finalized_at = NOW() " +
                        "WHERE bill_id = @id AND status = @draft",
                        DatabaseHelper.Param("@final", BillingRules.StatusFinalized),
                        DatabaseHelper.Param("@id", id),
                        DatabaseHelper.Param("@draft", BillingRules.StatusDraft));
                    if (changed != 1) throw new InvalidOperationException("The bill could not be finalized because it was changed by someone else.");
                }

                BillTotals totals = BillCalculator.Calculate(bill);
                string summary = $"{bill.Items.Count} item(s), grand total {totals.GrandTotal.ToString("N2", CultureInfo.InvariantCulture)}" +
                                 (totals.DiscountAmount > 0 ? $", {bill.DiscountType} discount {totals.DiscountAmount.ToString("N2", CultureInfo.InvariantCulture)}" : string.Empty) +
                                 (discountId != null ? $" (ID {discountId})" : string.Empty) +
                                 (totals.CoverageAmount > 0 ? $", {coverageType} {coverageNumber} covers {totals.CoverageAmount.ToString("N2", CultureInfo.InvariantCulture)}" : string.Empty);
                if (isNew) AuditHelper.Log(conn, tx, AuditHelper.Create, AuditHelper.Bill, id, bill.PatientId, "Draft saved: " + summary);
                else if (!finalize) AuditHelper.Log(conn, tx, AuditHelper.Update, AuditHelper.Bill, id, bill.PatientId, "Draft saved: " + summary);
                if (finalize) AuditHelper.Log(conn, tx, AuditHelper.Finalize, AuditHelper.Bill, id, bill.PatientId, "Finalized: " + summary);

                return id;
            });

            return GetBill(billId) ?? throw new InvalidOperationException("The bill was saved but could not be read back.");
        }

        /// <summary>Deletes a Draft bill and its items. A finalized bill is a financial record and can never be deleted.</summary>
        public void DeleteDraft(string billId)
        {
            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                DataTable locked = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT status, patient_id FROM bills WHERE bill_id = @id FOR UPDATE", DatabaseHelper.Param("@id", billId));
                if (locked.Rows.Count == 0) throw new InvalidOperationException("This bill no longer exists.");
                if (DatabaseHelper.GetString(locked.Rows[0], "status") != BillingRules.StatusDraft)
                    throw new InvalidOperationException("Only a draft can be deleted. A finalized bill is corrected by voiding it.");

                DatabaseHelper.ExecuteNonQuery(conn, tx, "DELETE FROM bills WHERE bill_id = @id", DatabaseHelper.Param("@id", billId));
                AuditHelper.Log(conn, tx, AuditHelper.Delete, AuditHelper.Bill, billId,
                    DatabaseHelper.GetString(locked.Rows[0], "patient_id"), "Draft deleted");
            });
        }

        // ------------------------------------------------------------------
        // Void (the correction of a finalized bill)
        // ------------------------------------------------------------------

        /// <summary>Why a bill cannot be voided right now, or null when it can. netPaid = payments minus refunds.</summary>
        public static string? VoidBlocker(string status, decimal netPaid)
        {
            if (status == BillingRules.StatusDraft) return "A draft is not voided - delete it or keep editing it.";
            if (status == BillingRules.StatusVoided) return "This bill is already voided.";
            if (netPaid > 0)
                return $"{SettingsHelper.FormatMoney(netPaid)} has been paid on this bill. Refund the payments first " +
                       "(Payments & Receipts > Billing Records > Refund), then void the bill.";
            return null;
        }

        /// <summary>
        /// Voids a finalized bill (one transaction): the bill is locked, must be Finalized with nothing left paid on it,
        /// gets status Voided with the reason, the user and the time, and its linked charges become unbilled again.
        /// With createCorrection a new Draft is created with the same patient, discount and items (corrects_bill_id = the voided bill),
        /// ready to be fixed and finalized. Returns the id of that new draft, or null.
        /// </summary>
        public string? VoidBill(string billId, string reason, bool createCorrection)
        {
            string text = (reason ?? string.Empty).Trim();
            if (text.Length < BillingRules.MinVoidReason)
                throw new InvalidOperationException($"Enter the reason for voiding the bill (at least {BillingRules.MinVoidReason} characters).");
            if (text.Length > BillingRules.MaxVoidReason)
                throw new InvalidOperationException($"The reason can have at most {BillingRules.MaxVoidReason} characters.");

            return DatabaseHelper.ExecuteInTransaction<string?>((conn, tx) =>
            {
                DataTable locked = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT status, patient_id FROM bills WHERE bill_id = @id FOR UPDATE", DatabaseHelper.Param("@id", billId));
                if (locked.Rows.Count == 0) throw new InvalidOperationException("This bill no longer exists.");
                string status = DatabaseHelper.GetString(locked.Rows[0], "status");
                string patientId = DatabaseHelper.GetString(locked.Rows[0], "patient_id");

                object? paid = DatabaseHelper.ExecuteScalar(conn, tx,
                    "SELECT amount_paid FROM v_bill_payment_summary WHERE bill_id = @id", DatabaseHelper.Param("@id", billId));
                decimal netPaid = paid == null || paid == DBNull.Value ? 0m : Convert.ToDecimal(paid, CultureInfo.InvariantCulture);

                string? blocker = VoidBlocker(status, netPaid);
                if (blocker != null) throw new InvalidOperationException(blocker);

                object? total = DatabaseHelper.ExecuteScalar(conn, tx,
                    "SELECT grand_total FROM v_bill_totals WHERE bill_id = @id", DatabaseHelper.Param("@id", billId));

                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "UPDATE bills SET status = @voided, voided_at = NOW(), voided_by_user_id = @user, void_reason = @reason " +
                    "WHERE bill_id = @id AND status = @final",
                    DatabaseHelper.Param("@voided", BillingRules.StatusVoided),
                    DatabaseHelper.Param("@user", SessionManager.CurrentUserId),
                    DatabaseHelper.Param("@reason", text),
                    DatabaseHelper.Param("@id", billId),
                    DatabaseHelper.Param("@final", BillingRules.StatusFinalized));
                AuditHelper.Log(conn, tx, AuditHelper.Void, AuditHelper.Bill, billId, patientId,
                    $"Voided (total {Convert.ToDecimal(total ?? 0m, CultureInfo.InvariantCulture).ToString("N2", CultureInfo.InvariantCulture)}): {text}");

                if (!createCorrection) return null;

                string newId = GeneratorHelper.Generate(GeneratorHelper.Prefix.Bill, conn, tx);
                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "INSERT INTO bills (bill_id, patient_id, status, discount_type, discount_value, discount_id_number, tax_rate, " +
                    "                   authorized_by, created_by_user_id, corrects_bill_id, " +
                    "                   coverage_type, coverage_number, coverage_provider, coverage_amount) " +
                    "SELECT @new, patient_id, @draft, discount_type, discount_value, discount_id_number, tax_rate, authorized_by, @user, bill_id, " +
                    "       coverage_type, coverage_number, coverage_provider, coverage_amount " +
                    "FROM bills WHERE bill_id = @id",
                    DatabaseHelper.Param("@new", newId),
                    DatabaseHelper.Param("@draft", BillingRules.StatusDraft),
                    DatabaseHelper.Param("@user", SessionManager.CurrentUserId),
                    DatabaseHelper.Param("@id", billId));
                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "INSERT INTO bill_items (bill_id, bill_category, description, quantity, unit_price, source_type, source_id) " +
                    "SELECT @new, bill_category, description, quantity, unit_price, source_type, source_id " +
                    "FROM bill_items WHERE bill_id = @id ORDER BY bill_item_id",
                    DatabaseHelper.Param("@new", newId),
                    DatabaseHelper.Param("@id", billId));
                AuditHelper.Log(conn, tx, AuditHelper.Create, AuditHelper.Bill, newId, patientId, $"Draft created to correct voided bill {billId}");
                return newId;
            });
        }
    }
}