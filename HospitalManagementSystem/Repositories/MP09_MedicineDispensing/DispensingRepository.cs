using System.Data;
using System.Globalization;
using System.Text;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP09_Dispensing;
using HospitalManagementSystem.Repositories.Common;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.MP09_Dispensing
{
    /// <summary>
    /// All SQL for the Medicine Dispensing Simulator (MP09, Step 13).
    ///
    /// WHAT CAN BE DISPENSED
    ///   - a prescription whose Valid Until date has passed is refused (Expired);
    ///   - a prescription whose lines are all dispensed is refused (Fully Dispensed);
    ///   - a partly dispensed prescription only allows the REMAINING quantity of each line
    ///     (view v_prescription_item_balance: prescribed - already dispensed).
    ///
    /// STOCK (decision: PARTIAL DISPENSING IS ALLOWED)
    ///   The quantity to dispense can never be more than the remaining prescribed quantity and never more than the usable stock
    ///   (batches that have not expired). When the stock is short, the screen shows the shortfall and the pharmacist dispenses
    ///   what is available; the rest stays open on the prescription and can be dispensed later, as another dispensing record.
    ///
    /// SAVE (one transaction)
    ///   1. lock the prescription row, re-check expiry and the remaining quantity of every line;
    ///   2. lock the batches of every medicine, re-check the usable stock;
    ///   3. generate MS-YYYYMMDD-XXX with GeneratorHelper and insert dispensing_records;
    ///   4. per line insert dispensing_items (unit price = a snapshot of the catalog price) and take the stock FIFO:
    ///      oldest expiry date first, expired and empty batches skipped, one DISPENSE row in stock_movements per batch used.
    ///   If anything fails everything is rolled back, including the code number.
    /// </summary>
    public class DispensingRepository
    {
        private const string PrescriptionSelect =
            "SELECT r.prescription_id, r.issued_date, r.valid_until, p.patient_id, " +
            "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
            "       TIMESTAMPDIFF(YEAR, p.date_of_birth, CURDATE()) AS age, p.allergies, " +
            "       CONCAT(d.first_name, ' ', d.last_name) AS doctor_name, " +
            "       COUNT(b.prescription_item_id) AS line_count, " +
            "       COALESCE(SUM(b.quantity_prescribed), 0) AS qty_prescribed, " +
            "       COALESCE(SUM(b.quantity_dispensed), 0) AS qty_dispensed, " +
            "       COALESCE(SUM(b.quantity_remaining), 0) AS qty_remaining " +
            "FROM prescriptions r " +
            "JOIN consultations c ON c.consultation_id = r.consultation_id " +
            "JOIN appointments a ON a.appointment_id = c.appointment_id " +
            "JOIN patients p ON p.patient_id = a.patient_id " +
            "JOIN doctors d ON d.doctor_id = a.doctor_id " +
            "LEFT JOIN v_prescription_item_balance b ON b.prescription_id = r.prescription_id ";

        private const string PrescriptionGroup =
            " GROUP BY r.prescription_id, r.issued_date, r.valid_until, p.patient_id, " +
            "p.first_name, p.last_name, p.date_of_birth, p.allergies, d.first_name, d.last_name";

        private const string SlipSelect =
            "SELECT dr.dispensing_id, dr.dispensed_at, dr.prescription_id, p.patient_id, " +
            "       CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " +
            "       TIMESTAMPDIFF(YEAR, p.date_of_birth, DATE(dr.dispensed_at)) AS age, " +
            "       CONCAT(d.first_name, ' ', d.last_name) AS doctor_name, dr.payment_method AS method_name, u.full_name AS dispensed_by, " +
            "       t.total_amount, " +
            "       (SELECT COUNT(*) FROM dispensing_items x WHERE x.dispensing_id = dr.dispensing_id) AS item_count, " +
            "       (SELECT COALESCE(SUM(bal.quantity_remaining), 0) FROM v_prescription_item_balance bal WHERE bal.prescription_id = dr.prescription_id) AS remaining_units " +
            "FROM dispensing_records dr " +
            "JOIN v_dispensing_totals t ON t.dispensing_id = dr.dispensing_id " +
            "JOIN prescriptions r ON r.prescription_id = dr.prescription_id " +
            "JOIN consultations c ON c.consultation_id = r.consultation_id " +
            "JOIN appointments a ON a.appointment_id = c.appointment_id " +
            "JOIN patients p ON p.patient_id = a.patient_id " +
            "JOIN doctors d ON d.doctor_id = a.doctor_id " +
            "LEFT JOIN users u ON u.user_id = dr.dispensed_by_user_id ";

        // ------------------------------------------------------------------
        // Lists
        // ------------------------------------------------------------------

        /// <summary>"Charge to Bill": the medicines are not paid at the pharmacy; they appear in Itemized Billing (MP11) as unbilled charges.</summary>
        public const string ChargeToBill = "Charge to Bill";

        public static readonly string[] DispensingMethods = LookupRepository.PaymentMethods.Concat(new[] { ChargeToBill }).ToArray();

        public List<LookupItem> GetPaymentMethods() => LookupRepository.Fixed(DispensingMethods);

        /// <summary>Prescriptions of the last 90 days (newest first), optionally filtered by patient / prescription ID and by "can still be dispensed".</summary>
        public List<PrescriptionChoice> GetPrescriptions(string? text, bool onlyDispensable)
        {
            var sql = new StringBuilder(PrescriptionSelect +
                $"WHERE r.issued_date >= DATE_SUB(CURDATE(), INTERVAL {DispensingRules.PrescriptionLookbackDays} DAY)");
            var parameters = new List<MySqlParameter>();
            AppendWords(sql, parameters, text, "r.prescription_id");
            sql.Append(PrescriptionGroup);
            if (onlyDispensable)
                sql.Append(" HAVING COALESCE(SUM(b.quantity_remaining), 0) > 0 AND r.valid_until >= CURDATE()");
            sql.Append($" ORDER BY r.issued_date DESC, r.prescription_id DESC LIMIT {DispensingRules.PrescriptionListLimit}");

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(row => new PrescriptionChoice
            {
                PrescriptionId = DatabaseHelper.GetString(row, "prescription_id"),
                IssuedDate = DatabaseHelper.GetNullableDate(row, "issued_date") ?? DateTime.Today,
                ValidUntil = DatabaseHelper.GetNullableDate(row, "valid_until") ?? DateTime.Today,
                PatientId = DatabaseHelper.GetString(row, "patient_id"),
                PatientName = DatabaseHelper.GetString(row, "patient_name"),
                Age = DatabaseHelper.GetInt(row, "age"),
                Allergies = DatabaseHelper.GetString(row, "allergies"),
                DoctorName = DatabaseHelper.GetString(row, "doctor_name"),
                LineCount = DatabaseHelper.GetInt(row, "line_count"),
                QuantityPrescribed = DatabaseHelper.GetInt(row, "qty_prescribed"),
                QuantityDispensed = DatabaseHelper.GetInt(row, "qty_dispensed"),
                QuantityRemaining = DatabaseHelper.GetInt(row, "qty_remaining")
            }).ToList();
        }

        /// <summary>The medicine lines of a prescription with prescribed / already dispensed / remaining and the usable stock.
        /// QuantityToDispense starts at the highest allowed quantity.</summary>
        public List<DispenseLine> GetLines(string prescriptionId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT i.prescription_item_id, i.medicine_id, m.medicine_name, i.dosage, i.instructions, " +
                "       b.quantity_prescribed, b.quantity_dispensed, b.quantity_remaining, m.unit_price, " +
                "       COALESCE(s.usable_stock, 0) AS usable_stock " +
                "FROM prescription_items i " +
                "JOIN v_prescription_item_balance b ON b.prescription_item_id = i.prescription_item_id " +
                "JOIN medicines m ON m.medicine_id = i.medicine_id " +
                "LEFT JOIN v_medicine_stock s ON s.medicine_id = i.medicine_id " +
                "WHERE i.prescription_id = @id ORDER BY i.prescription_item_id",
                DatabaseHelper.Param("@id", prescriptionId));

            var lines = table.Rows.Cast<DataRow>().Select(row => new DispenseLine
            {
                PrescriptionItemId = DatabaseHelper.GetInt(row, "prescription_item_id"),
                MedicineId = DatabaseHelper.GetString(row, "medicine_id"),
                MedicineName = DatabaseHelper.GetString(row, "medicine_name"),
                Dosage = DatabaseHelper.GetString(row, "dosage"),
                Instructions = DatabaseHelper.GetString(row, "instructions"),
                QuantityPrescribed = DatabaseHelper.GetInt(row, "quantity_prescribed"),
                QuantityAlreadyDispensed = DatabaseHelper.GetInt(row, "quantity_dispensed"),
                QuantityRemaining = DatabaseHelper.GetInt(row, "quantity_remaining"),
                UnitPrice = DatabaseHelper.GetDecimal(row, "unit_price"),
                UsableStock = DatabaseHelper.GetInt(row, "usable_stock")
            }).ToList();

            foreach (DispenseLine line in lines) line.QuantityToDispense = line.MaxQuantity;
            return lines;
        }

        // ------------------------------------------------------------------
        // Rules
        // ------------------------------------------------------------------

        /// <summary>The rules that need no database (empty list = valid). The repository re-checks the rest inside the transaction.</summary>
        public static List<string> Validate(DispenseRequest request)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(request.PrescriptionId)) errors.Add("Select a prescription first.");
            if (!DispensingMethods.Contains(request.PaymentMethod)) errors.Add("Choose a payment method.");
            if (request.Lines.Any(l => l.Quantity < 0)) errors.Add("A quantity to dispense cannot be negative.");
            if (!request.Lines.Any(l => l.Quantity > 0)) errors.Add("Nothing to dispense: enter a quantity of at least 1 for one medicine.");
            return errors;
        }

        // ------------------------------------------------------------------
        // Dispense
        // ------------------------------------------------------------------

        private sealed class BatchStock
        {
            public int BatchId { get; set; }
            public int Remaining { get; set; }
        }

        /// <summary>Saves a dispensing in one transaction and returns its dispensing_id (MS-...). Throws InvalidOperationException with a plain message when a rule is broken.</summary>
        public string Dispense(DispenseRequest request)
        {
            List<string> errors = Validate(request);
            if (errors.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, errors));

            List<DispenseRequestLine> wanted = request.Lines.Where(l => l.Quantity > 0).ToList();
            int? userId = SessionManager.CurrentUserId > 0 ? SessionManager.CurrentUserId : (int?)null;

            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                // 1. the prescription (locked: two pharmacists cannot dispense the same prescription at the same time)
                DataTable rx = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT valid_until, (valid_until < CURDATE()) AS is_expired " +
                    "FROM prescriptions WHERE prescription_id = @id FOR UPDATE",
                    DatabaseHelper.Param("@id", request.PrescriptionId));
                if (rx.Rows.Count == 0)
                    throw new InvalidOperationException("This prescription no longer exists. The list will be refreshed.");

                string rxCode = request.PrescriptionId;
                if (DatabaseHelper.GetInt(rx.Rows[0], "is_expired") == 1)
                {
                    DateTime validUntil = DatabaseHelper.GetNullableDate(rx.Rows[0], "valid_until") ?? DateTime.Today;
                    throw new InvalidOperationException(
                        $"Prescription {rxCode} expired on {validUntil.ToString("MMM d, yyyy", CultureInfo.InvariantCulture)} and cannot be dispensed.");
                }

                // 2. the remaining quantity of every line, read after the lock
                DataTable items = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT i.prescription_item_id, i.medicine_id, m.medicine_name, m.unit_price, " +
                    "       i.quantity - COALESCE((SELECT SUM(di.quantity_dispensed) FROM dispensing_items di " +
                    "                              WHERE di.prescription_item_id = i.prescription_item_id), 0) AS remaining " +
                    "FROM prescription_items i JOIN medicines m ON m.medicine_id = i.medicine_id " +
                    "WHERE i.prescription_id = @id ORDER BY i.prescription_item_id",
                    DatabaseHelper.Param("@id", request.PrescriptionId));

                var lineRows = items.Rows.Cast<DataRow>().ToDictionary(r => DatabaseHelper.GetInt(r, "prescription_item_id"));
                if (lineRows.Values.All(r => DatabaseHelper.GetInt(r, "remaining") <= 0))
                    throw new InvalidOperationException($"Prescription {rxCode} has already been fully dispensed and cannot be dispensed again.");

                foreach (DispenseRequestLine line in wanted)
                {
                    if (!lineRows.TryGetValue(line.PrescriptionItemId, out DataRow? row))
                        throw new InvalidOperationException("A medicine line does not belong to this prescription.");

                    int remaining = DatabaseHelper.GetInt(row, "remaining");
                    string name = DatabaseHelper.GetString(row, "medicine_name");
                    if (line.Quantity > remaining)
                        throw new InvalidOperationException(
                            $"{name}: only {remaining} unit(s) remain on the prescription, so {line.Quantity} cannot be dispensed. Another pharmacist may have dispensed part of it - reopen the prescription.");
                }

                // 3. the usable stock of every medicine (batches locked, oldest expiry first)
                var batchesByMedicine = new Dictionary<string, List<BatchStock>>();
                foreach (string medicineId in wanted.Select(l => DatabaseHelper.GetString(lineRows[l.PrescriptionItemId], "medicine_id")).Distinct().OrderBy(id => id, StringComparer.Ordinal))
                {
                    DataTable batches = DatabaseHelper.ExecuteQuery(conn, tx,
                        "SELECT batch_id, quantity_received FROM stock_batches " +
                        "WHERE medicine_id = @m AND expiry_date >= CURDATE() ORDER BY expiry_date, batch_id FOR UPDATE",
                        DatabaseHelper.Param("@m", medicineId));

                    var list = new List<BatchStock>();
                    foreach (DataRow b in batches.Rows)
                    {
                        int batchId = DatabaseHelper.GetInt(b, "batch_id");
                        long moved = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                            "SELECT COALESCE(SUM(quantity_change), 0) FROM stock_movements WHERE batch_id = @b",
                            DatabaseHelper.Param("@b", batchId));
                        int remaining = (int)(DatabaseHelper.GetInt(b, "quantity_received") + moved);
                        if (remaining > 0) list.Add(new BatchStock { BatchId = batchId, Remaining = remaining });
                    }
                    batchesByMedicine[medicineId] = list;
                }

                foreach (var byMedicine in wanted.GroupBy(l => DatabaseHelper.GetString(lineRows[l.PrescriptionItemId], "medicine_id")))
                {
                    int requested = byMedicine.Sum(l => l.Quantity);
                    int usable = batchesByMedicine[byMedicine.Key].Sum(b => b.Remaining);
                    if (requested > usable)
                    {
                        string name = DatabaseHelper.GetString(lineRows[byMedicine.First().PrescriptionItemId], "medicine_name");
                        throw new InvalidOperationException(
                            $"{name}: only {usable} unit(s) are in usable stock (expired batches are not counted), but {requested} were requested. Short by {requested - usable}. Reduce the quantity and dispense the rest later.");
                    }
                }

                // 4. the record: id from GeneratorHelper inside this transaction
                string dispensingId = GeneratorHelper.Generate(GeneratorHelper.Prefix.Dispensing, conn, tx);
                DatabaseHelper.ExecuteNonQuery(conn, tx,
                    "INSERT INTO dispensing_records (dispensing_id, prescription_id, payment_method, dispensed_by_user_id) " +
                    "VALUES (@id, @rx, @method, @user)",
                    DatabaseHelper.Param("@id", dispensingId),
                    DatabaseHelper.Param("@rx", request.PrescriptionId),
                    DatabaseHelper.Param("@method", request.PaymentMethod),
                    DatabaseHelper.Param("@user", userId));

                // 5. the items and the FIFO stock movements
                foreach (DispenseRequestLine line in wanted.OrderBy(l => l.PrescriptionItemId))
                {
                    DataRow row = lineRows[line.PrescriptionItemId];
                    long itemId = DatabaseHelper.ExecuteInsert(conn, tx,
                        "INSERT INTO dispensing_items (dispensing_id, prescription_item_id, quantity_dispensed, unit_price) " +
                        "VALUES (@disp, @item, @qty, @price)",
                        DatabaseHelper.Param("@disp", dispensingId),
                        DatabaseHelper.Param("@item", line.PrescriptionItemId),
                        DatabaseHelper.Param("@qty", line.Quantity),
                        DatabaseHelper.Param("@price", DatabaseHelper.GetDecimal(row, "unit_price")));

                    int need = line.Quantity;
                    foreach (BatchStock batch in batchesByMedicine[DatabaseHelper.GetString(row, "medicine_id")])
                    {
                        if (need <= 0) break;
                        int take = Math.Min(need, batch.Remaining);
                        if (take <= 0) continue;

                        DatabaseHelper.ExecuteInsert(conn, tx,
                            "INSERT INTO stock_movements (batch_id, movement_type, quantity_change, dispensing_item_id, moved_by_user_id) " +
                            "VALUES (@batch, 'DISPENSE', @change, @item, @user)",
                            DatabaseHelper.Param("@batch", batch.BatchId),
                            DatabaseHelper.Param("@change", -take),
                            DatabaseHelper.Param("@item", (int)itemId),
                            DatabaseHelper.Param("@user", userId));

                        batch.Remaining -= take;
                        need -= take;
                    }

                    if (need > 0)
                        throw new InvalidOperationException($"{DatabaseHelper.GetString(row, "medicine_name")}: the stock changed while saving. Nothing was dispensed - please try again.");
                }

                object? patient = DatabaseHelper.ExecuteScalar(conn, tx,
                    "SELECT a.patient_id FROM prescriptions r JOIN consultations c ON c.consultation_id = r.consultation_id " +
                    "JOIN appointments a ON a.appointment_id = c.appointment_id WHERE r.prescription_id = @rx",
                    DatabaseHelper.Param("@rx", request.PrescriptionId));
                AuditHelper.Log(conn, tx, AuditHelper.Dispense, AuditHelper.Dispensing, dispensingId, patient as string,
                    $"Prescription {request.PrescriptionId}: {wanted.Sum(l => l.Quantity)} unit(s) of {wanted.Count} medicine(s), payment {request.PaymentMethod}");

                return dispensingId;
            });
        }

        // ------------------------------------------------------------------
        // Slip and history
        // ------------------------------------------------------------------

        /// <summary>One dispensing with its items (the print-style slip).</summary>
        public DispensingSlip? GetSlip(string dispensingId)
        {
            DataTable header = DatabaseHelper.ExecuteQuery(SlipSelect + "WHERE dr.dispensing_id = @id",
                DatabaseHelper.Param("@id", dispensingId));
            if (header.Rows.Count == 0) return null;

            DispensingSlip slip = ReadSlip(header.Rows[0]);
            DataTable items = DatabaseHelper.ExecuteQuery(
                "SELECT m.medicine_name, i.dosage, di.quantity_dispensed, di.unit_price, di.line_total " +
                "FROM dispensing_items di " +
                "JOIN prescription_items i ON i.prescription_item_id = di.prescription_item_id " +
                "JOIN medicines m ON m.medicine_id = i.medicine_id " +
                "WHERE di.dispensing_id = @id ORDER BY di.dispensing_item_id",
                DatabaseHelper.Param("@id", dispensingId));
            slip.Items = items.Rows.Cast<DataRow>().Select(row => new DispensingSlipItem
            {
                MedicineName = DatabaseHelper.GetString(row, "medicine_name"),
                Dosage = DatabaseHelper.GetString(row, "dosage"),
                Quantity = DatabaseHelper.GetInt(row, "quantity_dispensed"),
                UnitPrice = DatabaseHelper.GetDecimal(row, "unit_price"),
                LineTotal = DatabaseHelper.GetDecimal(row, "line_total")
            }).ToList();
            return slip;
        }

        /// <summary>Every dispensing, newest first, optionally filtered by patient, prescription ID or dispensing ID.</summary>
        public List<DispensingSlip> GetHistory(string? text)
        {
            var sql = new StringBuilder(SlipSelect + "WHERE 1 = 1");
            var parameters = new List<MySqlParameter>();
            AppendWords(sql, parameters, text, "dr.prescription_id", "dr.dispensing_id");
            sql.Append($" ORDER BY dr.dispensed_at DESC, dr.dispensing_id DESC LIMIT {DispensingRules.HistoryLimit}");

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(ReadSlip).ToList();
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        private static DispensingSlip ReadSlip(DataRow row)
        {
            return new DispensingSlip
            {
                DispensingId = DatabaseHelper.GetString(row, "dispensing_id"),
                DispensedAt = DatabaseHelper.GetNullableDate(row, "dispensed_at") ?? DateTime.Now,
                PrescriptionId = DatabaseHelper.GetString(row, "prescription_id"),
                PatientId = DatabaseHelper.GetString(row, "patient_id"),
                PatientName = DatabaseHelper.GetString(row, "patient_name"),
                Age = DatabaseHelper.GetInt(row, "age"),
                DoctorName = DatabaseHelper.GetString(row, "doctor_name"),
                PaymentMethod = DatabaseHelper.GetString(row, "method_name"),
                DispensedBy = DatabaseHelper.GetString(row, "dispensed_by"),
                Total = DatabaseHelper.GetDecimal(row, "total_amount"),
                ItemCount = DatabaseHelper.GetInt(row, "item_count"),
                RemainingUnits = DatabaseHelper.GetInt(row, "remaining_units")
            };
        }

        /// <summary>Every typed word must match the patient's first, middle or last name, the patient ID or one of the extra columns.</summary>
        private static void AppendWords(StringBuilder sql, List<MySqlParameter> parameters, string? text, params string[] extraColumns)
        {
            string[] words = (text ?? string.Empty).Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                var extra = new StringBuilder();
                foreach (string column in extraColumns) extra.Append($" OR {column} LIKE @w{i}");
                sql.Append($" AND (p.first_name LIKE @w{i} OR p.middle_name LIKE @w{i} OR p.last_name LIKE @w{i} OR p.patient_id LIKE @w{i}{extra})");
                parameters.Add(DatabaseHelper.Param($"@w{i}", "%" + words[i].Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_") + "%"));
            }
        }
    }
}