using System.Data;
using System.Globalization;
using System.Text;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
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
    /// </summary>
    public class BillingRepository
    {
        private const string BillSelect =
            "SELECT b.bill_id, b.patient_id, CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
            "       b.status, b.discount_type, b.discount_value, b.tax_rate, b.authorized_by, " +
            "       b.created_at, b.finalized_at, u.full_name AS created_by " +
            "FROM bills b " +
            "JOIN patients p ON p.patient_id = b.patient_id " +
            "LEFT JOIN users u ON u.user_id = b.created_by_user_id ";

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
                "SELECT bi.bill_item_id, bi.bill_category, bi.description, bi.quantity, bi.unit_price " +
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
                UnitPrice = DatabaseHelper.GetDecimal(r, "unit_price")
            }).ToList();
        }

        private static Bill ReadBill(DataRow row, List<BillItem> items) => new Bill
        {
            BillId = DatabaseHelper.GetString(row, "bill_id"),
            PatientId = DatabaseHelper.GetString(row, "patient_id"),
            PatientName = DatabaseHelper.GetString(row, "patient_name"),
            Status = DatabaseHelper.GetString(row, "status"),
            DiscountType = DatabaseHelper.GetString(row, "discount_type"),
            DiscountValue = DatabaseHelper.GetDecimal(row, "discount_value"),
            TaxRate = DatabaseHelper.GetDecimal(row, "tax_rate"),
            AuthorizedBy = DatabaseHelper.GetString(row, "authorized_by"),
            CreatedAt = DatabaseHelper.GetNullableDate(row, "created_at") ?? DateTime.Now,
            FinalizedAt = DatabaseHelper.GetNullableDate(row, "finalized_at"),
            CreatedBy = DatabaseHelper.GetString(row, "created_by"),
            Items = items
        };

        /// <summary>The "All Bills" list. An empty status = every status; the totals come from the view v_bill_totals.</summary>
        public List<BillListRow> GetBills(string? text, string status)
        {
            var sql = new StringBuilder(
                "SELECT b.bill_id, b.status, p.patient_id, " +
                "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, ps.payment_status, " +
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

            // ---- discount ----
            decimal subtotal = bill.Subtotal;
            bool discountOk = true;
            if (bill.DiscountType != BillingRules.DiscountFlat && bill.DiscountType != BillingRules.DiscountPercent)
            {
                discountOk = false;
                issues.Add(new BillingIssue(BillingField.Discount, "Choose the discount type (Flat or Percent)."));
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

            return issues;
        }

        // ------------------------------------------------------------------
        // Saving
        // ------------------------------------------------------------------

        public Bill SaveDraft(Bill bill) => Save(bill, finalize: false);

        public Bill Finalize(Bill bill) => Save(bill, finalize: true);

        private Bill Save(Bill bill, bool finalize)
        {
            List<BillingIssue> issues = Validate(bill, finalize);
            if (issues.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, issues.Select(i => i.Message)));

            // The authorization name is kept only when the discount really needs it.
            string? authorizedBy = NeedsAuthorization(bill) && bill.AuthorizedBy.Trim().Length > 0 ? bill.AuthorizedBy.Trim() : null;

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
                        "INSERT INTO bills (bill_id, patient_id, status, discount_type, discount_value, tax_rate, authorized_by, created_by_user_id) " +
                        "VALUES (@id, @patient, @status, @dtype, @dvalue, @tax, @auth, @user)",
                        DatabaseHelper.Param("@id", id),
                        DatabaseHelper.Param("@patient", bill.PatientId),
                        DatabaseHelper.Param("@status", BillingRules.StatusDraft),
                        DatabaseHelper.Param("@dtype", bill.DiscountType),
                        DatabaseHelper.Param("@dvalue", bill.DiscountValue),
                        DatabaseHelper.Param("@tax", bill.TaxRate),
                        DatabaseHelper.Param("@auth", authorizedBy),
                        DatabaseHelper.Param("@user", SessionManager.CurrentUserId));
                }
                else
                {
                    DataTable locked = DatabaseHelper.ExecuteQuery(conn, tx,
                        "SELECT status FROM bills WHERE bill_id = @id FOR UPDATE", DatabaseHelper.Param("@id", id));
                    if (locked.Rows.Count == 0)
                        throw new InvalidOperationException("This bill no longer exists. Open the list of bills again.");
                    if (DatabaseHelper.GetString(locked.Rows[0], "status") != BillingRules.StatusDraft)
                        throw new InvalidOperationException("This bill has already been finalized and can no longer be changed.");

                    DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "UPDATE bills SET patient_id = @patient, discount_type = @dtype, discount_value = @dvalue, " +
                        "  tax_rate = @tax, authorized_by = @auth WHERE bill_id = @id",
                        DatabaseHelper.Param("@patient", bill.PatientId),
                        DatabaseHelper.Param("@dtype", bill.DiscountType),
                        DatabaseHelper.Param("@dvalue", bill.DiscountValue),
                        DatabaseHelper.Param("@tax", bill.TaxRate),
                        DatabaseHelper.Param("@auth", authorizedBy),
                        DatabaseHelper.Param("@id", id));

                    DatabaseHelper.ExecuteNonQuery(conn, tx, "DELETE FROM bill_items WHERE bill_id = @id", DatabaseHelper.Param("@id", id));
                }

                foreach (BillItem item in bill.Items)
                {
                    DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "INSERT INTO bill_items (bill_id, bill_category, description, quantity, unit_price) " +
                        "VALUES (@bill, @category, @description, @quantity, @price)",
                        DatabaseHelper.Param("@bill", id),
                        DatabaseHelper.Param("@category", item.Category),
                        DatabaseHelper.Param("@description", item.Description.Trim()),
                        DatabaseHelper.Param("@quantity", item.Quantity),
                        DatabaseHelper.Param("@price", item.UnitPrice));
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
                    "SELECT status FROM bills WHERE bill_id = @id FOR UPDATE", DatabaseHelper.Param("@id", billId));
                if (locked.Rows.Count == 0) throw new InvalidOperationException("This bill no longer exists.");
                if (DatabaseHelper.GetString(locked.Rows[0], "status") != BillingRules.StatusDraft)
                    throw new InvalidOperationException("A finalized bill cannot be deleted.");

                DatabaseHelper.ExecuteNonQuery(conn, tx, "DELETE FROM bills WHERE bill_id = @id", DatabaseHelper.Param("@id", billId));
            });
        }
    }
}