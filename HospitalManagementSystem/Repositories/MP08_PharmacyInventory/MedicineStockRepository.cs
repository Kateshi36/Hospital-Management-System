using System.Data;
using System.Globalization;
using System.Text;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models.Common;
using HospitalManagementSystem.Models.MP08_MedicineStock;
using HospitalManagementSystem.Repositories.Common;
using MySqlConnector;

namespace HospitalManagementSystem.Repositories.MP08_MedicineStock
{
    /// <summary>
    /// All SQL for the Medicine Catalog &amp; Stock Manager (MP08, Step 12).
    ///
    /// STOCK of a batch  = quantity_received + SUM(stock_movements.quantity_change)   (view v_batch_stock)
    /// STOCK of a medicine = sum of its batches (view v_medicine_stock). Adjustments are stored per batch, so the
    /// Adjust Stock dialog asks for the batch. A batch (and therefore a medicine) can never go below zero: an adjustment
    /// that would do that is refused while the batch row is locked.
    ///
    /// ALERTS (view v_medicine_stock_status, only batches that still have stock count):
    ///   Low Stock      total stock &lt;= reorder level
    ///   Expiring Soon  a batch with stock expires within the setting expiring_soon_days (60) days from today
    ///   Expired        a batch with stock has an expiry date before today
    /// The status column shows the most serious one (Expired, then Expiring Soon, then Low Stock); the Alerts column lists
    /// every alert that applies.
    ///
    /// FIFO (used by the dispensing screen of MP09): stock is taken from the batch with the oldest expiry date first,
    /// skipping empty and expired batches. The batch list shows this as the "Use order" column.
    /// </summary>
    public class MedicineStockRepository
    {
        private const string MedicineSelect =
            "SELECT m.medicine_id, m.medicine_name, m.generic_name, m.category, m.manufacturer, m.unit_type, " +
            "       m.unit_price, m.reorder_level, m.storage_instructions " +
            "FROM medicines m ";

        public static int ExpiringSoonDays => Math.Max(1, SettingsHelper.GetInt(SettingsHelper.ExpiringSoonDays, 60));

        // ------------------------------------------------------------------
        // Lookups
        // ------------------------------------------------------------------

        // The values already in use; the drop-downs also accept a newly typed one.
        public List<LookupItem> GetCategories() => LookupRepository.Distinct("medicines", "category");
        public List<LookupItem> GetManufacturers() => LookupRepository.Distinct("medicines", "manufacturer");
        public List<LookupItem> GetUnitTypes() => LookupRepository.Distinct("medicines", "unit_type");
        public List<LookupItem> GetSuppliers() => LookupRepository.Distinct("stock_batches", "supplier");

        // ------------------------------------------------------------------
        // Medicine catalog
        // ------------------------------------------------------------------

        /// <summary>Catalog entries, optionally filtered (every typed word must be part of the name, generic name or medicine ID).</summary>
        public List<MedicineRecord> GetMedicines(string? text)
        {
            var sql = new StringBuilder(MedicineSelect + "WHERE 1 = 1");
            var parameters = new List<MySqlParameter>();
            AppendWords(sql, parameters, text, "m.medicine_name", "m.generic_name", "m.medicine_id");
            sql.Append(" ORDER BY m.medicine_name");

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(ReadMedicine).ToList();
        }

        /// <summary>Every rule of the catalog form, in plain words (empty list = valid).</summary>
        public static List<string> ValidateMedicine(MedicineInput input)
        {
            var errors = new List<string>();
            string name = input.Name.Trim();

            if (name.Length == 0) errors.Add("Enter the medicine name.");
            else if (name.Length > MedicineStockRules.MaxName) errors.Add($"The medicine name can have at most {MedicineStockRules.MaxName} characters.");
            if (input.GenericName.Trim().Length > MedicineStockRules.MaxGenericName) errors.Add($"The generic name can have at most {MedicineStockRules.MaxGenericName} characters.");
            if (string.IsNullOrWhiteSpace(input.Category)) errors.Add("Choose or type the category.");
            else if (input.Category.Trim().Length > MedicineStockRules.MaxCategory) errors.Add($"The category can have at most {MedicineStockRules.MaxCategory} characters.");
            if (input.Manufacturer.Trim().Length > MedicineStockRules.MaxManufacturer) errors.Add($"The manufacturer can have at most {MedicineStockRules.MaxManufacturer} characters.");
            if (string.IsNullOrWhiteSpace(input.UnitType)) errors.Add("Choose or type the unit type.");
            else if (input.UnitType.Trim().Length > MedicineStockRules.MaxUnitType) errors.Add($"The unit type can have at most {MedicineStockRules.MaxUnitType} characters.");
            if (input.UnitPrice <= 0 || input.UnitPrice > MedicineStockRules.PriceMax) errors.Add($"The unit price must be greater than 0 and at most {MedicineStockRules.PriceMax:N0}.");
            else if (Math.Round(input.UnitPrice, 2) != input.UnitPrice) errors.Add("The unit price can have at most 2 decimal places.");
            if (input.ReorderLevel < 0 || input.ReorderLevel > MedicineStockRules.ReorderMax) errors.Add($"The reorder level must be a whole number from 0 to {MedicineStockRules.ReorderMax:N0}.");
            if (input.StorageInstructions.Trim().Length > MedicineStockRules.MaxStorage) errors.Add($"The storage instructions can have at most {MedicineStockRules.MaxStorage} characters.");
            return errors;
        }

        /// <summary>Adds a medicine (empty MedicineId: the MED-XXX id is generated) or updates one; the name must be unique.</summary>
        public MedicineRecord SaveMedicine(MedicineInput input)
        {
            List<string> errors = ValidateMedicine(input);
            if (errors.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, errors));

            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                string name = input.Name.Trim();

                DataTable duplicate = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT medicine_id FROM medicines WHERE medicine_name = @name AND medicine_id <> @id LIMIT 1",
                    DatabaseHelper.Param("@name", name), DatabaseHelper.Param("@id", input.MedicineId));
                if (duplicate.Rows.Count > 0)
                    throw new InvalidOperationException($"A medicine named \"{name}\" already exists ({DatabaseHelper.GetString(duplicate.Rows[0], "medicine_id")}). The medicine name must be unique.");

                string id = input.MedicineId;
                if (id.Length == 0)
                {
                    id = GeneratorHelper.Generate(GeneratorHelper.Prefix.Medicine, conn, tx);
                    DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "INSERT INTO medicines (medicine_id, medicine_name, generic_name, category, manufacturer, unit_type, unit_price, reorder_level, storage_instructions) " +
                        "VALUES (@id, @name, @generic, @category, @manufacturer, @unit, @price, @reorder, @storage)",
                        MedicineParams(input).Append(DatabaseHelper.Param("@id", id)).ToArray());
                }
                else
                {
                    object? exists = DatabaseHelper.ExecuteScalar(conn, tx,
                        "SELECT medicine_id FROM medicines WHERE medicine_id = @id FOR UPDATE", DatabaseHelper.Param("@id", id));
                    if (exists == null || exists == DBNull.Value)
                        throw new InvalidOperationException("This medicine no longer exists. The list will be refreshed.");

                    DatabaseHelper.ExecuteNonQuery(conn, tx,
                        "UPDATE medicines SET medicine_name = @name, generic_name = @generic, category = @category, manufacturer = @manufacturer, " +
                        "  unit_type = @unit, unit_price = @price, reorder_level = @reorder, storage_instructions = @storage WHERE medicine_id = @id",
                        MedicineParams(input).Append(DatabaseHelper.Param("@id", id)).ToArray());
                }

                DataTable saved = DatabaseHelper.ExecuteQuery(conn, tx, MedicineSelect + "WHERE m.medicine_id = @id", DatabaseHelper.Param("@id", id));
                return ReadMedicine(saved.Rows[0]);
            });
        }

        /// <summary>Deletes a medicine that has never been stocked or prescribed; otherwise explains why not.</summary>
        public void DeleteMedicine(string medicineId)
        {
            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                object? name = DatabaseHelper.ExecuteScalar(conn, tx,
                    "SELECT medicine_name FROM medicines WHERE medicine_id = @id FOR UPDATE", DatabaseHelper.Param("@id", medicineId));
                if (name == null || name == DBNull.Value)
                    throw new InvalidOperationException("This medicine no longer exists. The list will be refreshed.");

                long batches = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COUNT(*) FROM stock_batches WHERE medicine_id = @id", DatabaseHelper.Param("@id", medicineId));
                long prescribed = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COUNT(*) FROM prescription_items WHERE medicine_id = @id", DatabaseHelper.Param("@id", medicineId));

                if (batches > 0 || prescribed > 0)
                {
                    var reasons = new List<string>();
                    if (batches > 0) reasons.Add($"{batches} stock batch(es)");
                    if (prescribed > 0) reasons.Add($"{prescribed} prescription line(s)");
                    throw new InvalidOperationException(
                        $"{name} cannot be deleted because it has {string.Join(" and ", reasons)}. " +
                        "Medicines that were stocked or prescribed are kept for the records; set the unit price or reorder level instead.");
                }

                DatabaseHelper.ExecuteNonQuery(conn, tx, "DELETE FROM medicines WHERE medicine_id = @id", DatabaseHelper.Param("@id", medicineId));
            });
        }

        // ------------------------------------------------------------------
        // Stock
        // ------------------------------------------------------------------

        /// <summary>Stock per medicine with the alert status, medicines in alert first.</summary>
        public List<StockSummaryRow> GetStockSummary(string? text, bool alertsOnly)
        {
            var sql = new StringBuilder(
                "SELECT s.medicine_id, s.medicine_name, s.reorder_level, s.total_stock, s.usable_stock, " +
                "       s.nearest_expiry, s.is_low_stock, s.has_expired, s.has_expiring_soon, s.stock_status " +
                "FROM v_medicine_stock_status s WHERE 1 = 1");
            var parameters = new List<MySqlParameter>();
            AppendWords(sql, parameters, text, "s.medicine_name", "s.medicine_id");
            if (alertsOnly) sql.Append(" AND s.stock_status <> 'Normal'");
            sql.Append(" ORDER BY CASE s.stock_status WHEN 'Expired' THEN 1 WHEN 'Expiring Soon' THEN 2 WHEN 'Low Stock' THEN 3 ELSE 4 END, s.medicine_name");

            DataTable table = DatabaseHelper.ExecuteQuery(sql.ToString(), parameters.ToArray());
            return table.Rows.Cast<DataRow>().Select(r => new StockSummaryRow
            {
                MedicineId = DatabaseHelper.GetString(r, "medicine_id"),
                MedicineName = DatabaseHelper.GetString(r, "medicine_name"),
                ReorderLevel = DatabaseHelper.GetInt(r, "reorder_level"),
                TotalStock = Convert.ToInt32(r["total_stock"], CultureInfo.InvariantCulture),
                UsableStock = Convert.ToInt32(r["usable_stock"], CultureInfo.InvariantCulture),
                NearestExpiry = DatabaseHelper.GetNullableDate(r, "nearest_expiry"),
                IsLowStock = Convert.ToInt32(r["is_low_stock"], CultureInfo.InvariantCulture) == 1,
                HasExpired = Convert.ToInt32(r["has_expired"], CultureInfo.InvariantCulture) == 1,
                HasExpiringSoon = Convert.ToInt32(r["has_expiring_soon"], CultureInfo.InvariantCulture) == 1,
                StatusName = DatabaseHelper.GetString(r, "stock_status")
            }).ToList();
        }

        /// <summary>The batches of a medicine in FIFO order (oldest expiry first) with status and use order.</summary>
        public List<BatchRecord> GetBatches(string medicineId)
        {
            DataTable table = DatabaseHelper.ExecuteQuery(
                "SELECT bs.batch_id, bs.medicine_id, bs.batch_number, bs.expiry_date, bs.date_received, bs.quantity_received, " +
                "       bs.quantity_remaining, bs.supplier " +
                "FROM v_batch_stock bs " +
                "WHERE bs.medicine_id = @id ORDER BY bs.expiry_date, bs.batch_id",
                DatabaseHelper.Param("@id", medicineId));

            int soonDays = ExpiringSoonDays;
            int order = 0;
            var list = new List<BatchRecord>();
            foreach (DataRow r in table.Rows)
            {
                var batch = new BatchRecord
                {
                    BatchId = DatabaseHelper.GetInt(r, "batch_id"),
                    MedicineId = DatabaseHelper.GetString(r, "medicine_id"),
                    BatchNumber = DatabaseHelper.GetString(r, "batch_number"),
                    ExpiryDate = DatabaseHelper.GetNullableDate(r, "expiry_date") ?? DateTime.MinValue,
                    DateReceived = DatabaseHelper.GetNullableDate(r, "date_received") ?? DateTime.MinValue,
                    QuantityReceived = DatabaseHelper.GetInt(r, "quantity_received"),
                    QuantityRemaining = Convert.ToInt32(r["quantity_remaining"], CultureInfo.InvariantCulture),
                    SupplierName = DatabaseHelper.GetString(r, "supplier")
                };

                int daysLeft = (batch.ExpiryDate.Date - DateTime.Today).Days;
                if (batch.QuantityRemaining <= 0) batch.StatusText = "Empty";
                else if (daysLeft < 0) batch.StatusText = "Expired";
                else if (daysLeft <= soonDays) batch.StatusText = "Expiring Soon";
                else batch.StatusText = "OK";

                if (batch.QuantityRemaining > 0 && daysLeft >= 0) batch.DispenseOrder = ++order;
                list.Add(batch);
            }
            return list;
        }

        public static List<string> ValidateBatch(StockBatchInput input)
        {
            var errors = new List<string>();
            string number = input.BatchNumber.Trim();

            if (string.IsNullOrEmpty(input.MedicineId)) errors.Add("Select a medicine first.");
            if (number.Length == 0) errors.Add("Enter the batch number.");
            else if (number.Length > MedicineStockRules.MaxBatchNumber) errors.Add($"The batch number can have at most {MedicineStockRules.MaxBatchNumber} characters.");
            if (input.Quantity < 1 || input.Quantity > MedicineStockRules.QuantityMax) errors.Add($"The quantity must be a whole number from 1 to {MedicineStockRules.QuantityMax:N0}.");
            if (input.ExpiryDate.Date <= DateTime.Today) errors.Add("The expiry date must be a future date.");
            if (input.ExpiryDate.Date > DateTime.Today.AddYears(MedicineStockRules.ExpiryMaxYears)) errors.Add($"The expiry date can be at most {MedicineStockRules.ExpiryMaxYears} years from today.");
            if (string.IsNullOrWhiteSpace(input.Supplier)) errors.Add("Choose or type the supplier.");
            else if (input.Supplier.Trim().Length > MedicineStockRules.MaxSupplier) errors.Add($"The supplier can have at most {MedicineStockRules.MaxSupplier} characters.");
            if (input.PurchasePrice <= 0 || input.PurchasePrice > MedicineStockRules.PriceMax) errors.Add($"The purchase price must be greater than 0 and at most {MedicineStockRules.PriceMax:N0}.");
            else if (Math.Round(input.PurchasePrice, 2) != input.PurchasePrice) errors.Add("The purchase price can have at most 2 decimal places.");
            return errors;
        }

        /// <summary>Records a new batch (it increases the stock of the medicine). Batch numbers are unique per medicine.</summary>
        public void AddStock(StockBatchInput input)
        {
            List<string> errors = ValidateBatch(input);
            if (errors.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, errors));

            DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                object? medicine = DatabaseHelper.ExecuteScalar(conn, tx,
                    "SELECT medicine_name FROM medicines WHERE medicine_id = @id FOR UPDATE", DatabaseHelper.Param("@id", input.MedicineId));
                if (medicine == null || medicine == DBNull.Value)
                    throw new InvalidOperationException("This medicine no longer exists. The list will be refreshed.");

                long duplicate = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COUNT(*) FROM stock_batches WHERE medicine_id = @id AND batch_number = @number",
                    DatabaseHelper.Param("@id", input.MedicineId), DatabaseHelper.Param("@number", input.BatchNumber.Trim()));
                if (duplicate > 0)
                    throw new InvalidOperationException($"Batch {input.BatchNumber.Trim()} of {medicine} already exists. Use a different batch number, or adjust the existing batch.");

                DatabaseHelper.ExecuteInsert(conn, tx,
                    "INSERT INTO stock_batches (medicine_id, batch_number, quantity_received, expiry_date, supplier, purchase_price, date_received, received_by_user_id) " +
                    "VALUES (@medicine, @number, @qty, @expiry, @supplier, @price, CURDATE(), @user)",
                    DatabaseHelper.Param("@medicine", input.MedicineId),
                    DatabaseHelper.Param("@number", input.BatchNumber.Trim()),
                    DatabaseHelper.Param("@qty", input.Quantity),
                    DatabaseHelper.Param("@expiry", input.ExpiryDate.Date),
                    DatabaseHelper.Param("@supplier", input.Supplier.Trim()),
                    DatabaseHelper.Param("@price", input.PurchasePrice),
                    DatabaseHelper.Param("@user", SessionManager.CurrentUserId > 0 ? SessionManager.CurrentUserId : (int?)null));

                AuditHelper.Log(conn, tx, AuditHelper.StockIn, AuditHelper.MedicineStock, input.MedicineId, null,
                    $"{medicine}: batch {input.BatchNumber.Trim()}, {input.Quantity} unit(s), expires {input.ExpiryDate:yyyy-MM-dd}, " +
                    $"supplier {input.Supplier.Trim()}, {input.PurchasePrice.ToString("N2", System.Globalization.CultureInfo.InvariantCulture)} each");
            });
        }

        public static List<string> ValidateAdjustment(StockAdjustInput input)
        {
            var errors = new List<string>();
            if (input.BatchId <= 0) errors.Add("Choose the batch to adjust.");
            if (input.QuantityChange == 0) errors.Add("Enter a quantity greater than 0.");
            if (Math.Abs((long)input.QuantityChange) > MedicineStockRules.QuantityMax) errors.Add($"The quantity can be at most {MedicineStockRules.QuantityMax:N0}.");
            if (string.IsNullOrWhiteSpace(input.Reason)) errors.Add("Enter the reason for the adjustment.");
            else if (input.Reason.Trim().Length > MedicineStockRules.MaxReason) errors.Add($"The reason can have at most {MedicineStockRules.MaxReason} characters.");
            return errors;
        }

        /// <summary>
        /// Records a correction (damage, loss, expired-stock disposal or a count correction) against one batch.
        /// Refused when it would take the batch below zero. Returns the quantity left in the batch.
        /// </summary>
        public int AdjustStock(StockAdjustInput input)
        {
            List<string> errors = ValidateAdjustment(input);
            if (errors.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, errors));

            return DatabaseHelper.ExecuteInTransaction((conn, tx) =>
            {
                DataTable batch = DatabaseHelper.ExecuteQuery(conn, tx,
                    "SELECT batch_number, quantity_received FROM stock_batches WHERE batch_id = @id FOR UPDATE",
                    DatabaseHelper.Param("@id", input.BatchId));
                if (batch.Rows.Count == 0)
                    throw new InvalidOperationException("This batch no longer exists. The list will be refreshed.");

                long moved = DatabaseHelper.ExecuteScalar<long>(conn, tx,
                    "SELECT COALESCE(SUM(quantity_change), 0) FROM stock_movements WHERE batch_id = @id",
                    DatabaseHelper.Param("@id", input.BatchId));
                long remaining = DatabaseHelper.GetInt(batch.Rows[0], "quantity_received") + moved;

                if (remaining + input.QuantityChange < 0)
                    throw new InvalidOperationException(
                        $"Batch {DatabaseHelper.GetString(batch.Rows[0], "batch_number")} has only {remaining} left, so {Math.Abs(input.QuantityChange)} cannot be removed. Stock can never go below zero.");

                DatabaseHelper.ExecuteInsert(conn, tx,
                    "INSERT INTO stock_movements (batch_id, movement_type, quantity_change, reason, moved_by_user_id) VALUES (@batch, 'ADJUSTMENT', @change, @reason, @user)",
                    DatabaseHelper.Param("@batch", input.BatchId),
                    DatabaseHelper.Param("@change", input.QuantityChange),
                    DatabaseHelper.Param("@reason", input.Reason.Trim()),
                    DatabaseHelper.Param("@user", SessionManager.CurrentUserId > 0 ? SessionManager.CurrentUserId : (int?)null));

                object? medicineId = DatabaseHelper.ExecuteScalar(conn, tx,
                    "SELECT medicine_id FROM stock_batches WHERE batch_id = @id", DatabaseHelper.Param("@id", input.BatchId));
                AuditHelper.Log(conn, tx, AuditHelper.Adjust, AuditHelper.MedicineStock, medicineId as string ?? input.BatchId.ToString(System.Globalization.CultureInfo.InvariantCulture), null,
                    $"Batch {DatabaseHelper.GetString(batch.Rows[0], "batch_number")}: {(input.QuantityChange > 0 ? "+" : "")}{input.QuantityChange} " +
                    $"(was {remaining}, now {remaining + input.QuantityChange}) - {input.Reason.Trim()}");

                return (int)(remaining + input.QuantityChange);
            });
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        private static MySqlParameter[] MedicineParams(MedicineInput input)
        {
            var list = new List<MySqlParameter>
            {
                DatabaseHelper.Param("@name", input.Name.Trim()),
                DatabaseHelper.Param("@generic", string.IsNullOrWhiteSpace(input.GenericName) ? null : input.GenericName.Trim()),
                DatabaseHelper.Param("@category", input.Category.Trim()),
                DatabaseHelper.Param("@manufacturer", string.IsNullOrWhiteSpace(input.Manufacturer) ? null : input.Manufacturer.Trim()),
                DatabaseHelper.Param("@unit", input.UnitType.Trim()),
                DatabaseHelper.Param("@price", input.UnitPrice),
                DatabaseHelper.Param("@reorder", input.ReorderLevel),
                DatabaseHelper.Param("@storage", string.IsNullOrWhiteSpace(input.StorageInstructions) ? null : input.StorageInstructions.Trim())
            };
            return list.ToArray();
        }

        private static MedicineRecord ReadMedicine(DataRow r)
        {
            return new MedicineRecord
            {
                MedicineId = DatabaseHelper.GetString(r, "medicine_id"),
                Name = DatabaseHelper.GetString(r, "medicine_name"),
                GenericName = DatabaseHelper.GetString(r, "generic_name"),
                Category = DatabaseHelper.GetString(r, "category"),
                Manufacturer = DatabaseHelper.GetString(r, "manufacturer"),
                UnitType = DatabaseHelper.GetString(r, "unit_type"),
                UnitPrice = DatabaseHelper.GetDecimal(r, "unit_price"),
                ReorderLevel = DatabaseHelper.GetInt(r, "reorder_level"),
                StorageInstructions = DatabaseHelper.GetString(r, "storage_instructions")
            };
        }

        /// <summary>Every typed word must appear in at least one of the given columns.</summary>
        private static void AppendWords(StringBuilder sql, List<MySqlParameter> parameters, string? text, params string[] columns)
        {
            string[] words = (text ?? string.Empty).Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                sql.Append(" AND (" + string.Join(" OR ", columns.Select(c => $"{c} LIKE @w{i}")) + ")");
                parameters.Add(DatabaseHelper.Param($"@w{i}", "%" + words[i].Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_") + "%"));
            }
        }
    }
}