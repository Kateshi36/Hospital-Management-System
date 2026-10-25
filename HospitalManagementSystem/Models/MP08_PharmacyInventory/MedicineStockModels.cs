using System.Globalization;
using HospitalManagementSystem.Helpers;

namespace HospitalManagementSystem.Models.MP08_MedicineStock
{
    /// <summary>The limits of Step 12. The forms and the repository both use these constants.</summary>
    public static class MedicineStockRules
    {
        public const int MaxName = 120;
        public const int MaxGenericName = 120;
        public const int MaxStorage = 250;
        public const int MaxCategory = 60;          // medicines.category
        public const int MaxManufacturer = 100;     // medicines.manufacturer
        public const int MaxUnitType = 30;          // medicines.unit_type
        public const int MaxSupplier = 100;         // stock_batches.supplier
        public const int MaxBatchNumber = 50;
        public const int MaxReason = 250;
        public const decimal PriceMax = 1000000m;
        public const int ReorderMax = 1000000;
        public const int QuantityMax = 1000000;
        public const int ExpiryMaxYears = 10;
    }

    /// <summary>A medicine of the catalog.</summary>
    public class MedicineRecord
    {
        /// <summary>MED-XXX</summary>
        public string MedicineId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string GenericName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string UnitType { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int ReorderLevel { get; set; }
        public string StorageInstructions { get; set; } = string.Empty;

        public string UnitPriceText => SettingsHelper.FormatMoney(UnitPrice);
    }

    /// <summary>What the pharmacist typed in the catalog form (an empty MedicineId means a new medicine).</summary>
    public class MedicineInput
    {
        public string MedicineId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string GenericName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string UnitType { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int ReorderLevel { get; set; }
        public string StorageInstructions { get; set; } = string.Empty;
    }

    /// <summary>One row of the stock summary (per medicine).</summary>
    public class StockSummaryRow
    {
        public string MedicineId { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public int TotalStock { get; set; }
        public int UsableStock { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime? NearestExpiry { get; set; }
        public bool IsLowStock { get; set; }
        public bool HasExpired { get; set; }
        public bool HasExpiringSoon { get; set; }

        /// <summary>Normal, Low Stock, Expiring Soon or Expired (priority: Expired, Expiring Soon, Low Stock).</summary>
        public string StatusName { get; set; } = string.Empty;

        public bool IsAlert => StatusName != "Normal";
        public string NearestExpiryText => NearestExpiry.HasValue ? NearestExpiry.Value.ToString("MMM d, yyyy", CultureInfo.InvariantCulture) : "-";

        /// <summary>Every alert that applies (a medicine can be Expired and Low Stock at the same time).</summary>
        public string AlertsText
        {
            get
            {
                var parts = new List<string>();
                if (HasExpired) parts.Add("Expired");
                if (HasExpiringSoon) parts.Add("Expiring Soon");
                if (IsLowStock) parts.Add("Low Stock");
                return parts.Count == 0 ? "-" : string.Join(", ", parts);
            }
        }
    }

    /// <summary>A stock batch with its remaining quantity.</summary>
    public class BatchRecord
    {
        public int BatchId { get; set; }
        public string MedicineId { get; set; } = string.Empty;
        public string BatchNumber { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public DateTime DateReceived { get; set; }
        public int QuantityReceived { get; set; }
        public int QuantityRemaining { get; set; }
        public string SupplierName { get; set; } = string.Empty;

        /// <summary>Empty, Expired, Expiring Soon or OK.</summary>
        public string StatusText { get; set; } = string.Empty;

        /// <summary>1 = the batch to use first (oldest expiry that is not expired and not empty); 0 = not usable.</summary>
        public int DispenseOrder { get; set; }

        public string ExpiryText => ExpiryDate.ToString("MMM d, yyyy", CultureInfo.InvariantCulture);
        public string OrderText => DispenseOrder > 0 ? DispenseOrder.ToString(CultureInfo.InvariantCulture) : "-";
        public string Display => $"{BatchNumber}  -  {QuantityRemaining} left  -  expires {ExpiryText}  ({StatusText})";
    }

    /// <summary>Data of the Add Stock dialog.</summary>
    public class StockBatchInput
    {
        public string MedicineId { get; set; } = string.Empty;
        public string BatchNumber { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public decimal PurchasePrice { get; set; }
    }

    /// <summary>Data of the Adjust Stock dialog (QuantityChange is negative for damage, loss or disposal).</summary>
    public class StockAdjustInput
    {
        public int BatchId { get; set; }
        public int QuantityChange { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}