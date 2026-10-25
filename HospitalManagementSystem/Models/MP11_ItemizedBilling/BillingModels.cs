using System.Globalization;

namespace HospitalManagementSystem.Models.MP11_Billing
{
    /// <summary>The limits and fixed values of Step 15. The form and the repository both use these constants.</summary>
    public static class BillingRules
    {
        // bills.status
        public const string StatusDraft = "Draft";
        public const string StatusFinalized = "Finalized";

        // bills.discount_type
        public const string DiscountFlat = "Flat";
        public const string DiscountPercent = "Percent";
        public static readonly string[] DiscountTypes = { DiscountFlat, DiscountPercent };

        // bill_items.bill_category (and service_catalog.bill_category)
        public const string CategoryConsultation = "Consultation";
        public const string CategoryMedicine = "Medicine";
        public const string CategoryLabTest = "Lab Test";
        public const string CategoryRoom = "Room";
        public const string CategoryProcedure = "Procedure";
        public const string CategoryMiscellaneous = "Miscellaneous";
        public static readonly string[] Categories =
        {
            CategoryConsultation, CategoryMedicine, CategoryLabTest, CategoryRoom, CategoryProcedure, CategoryMiscellaneous
        };

        public const int MaxItems = 100;
        public const int MaxQuantity = 999;
        public const int MaxRoomDays = 365;
        public const decimal MaxUnitPrice = 999999.99m;
        public const decimal MaxFlatDiscount = 99999999.99m;   // bills.discount_value is DECIMAL(10,2)
        public const int MaxDescription = 200;
        public const int MaxAuthorizedBy = 100;
        public const int ListLimit = 300;
        public const int PatientListLimit = 100;
    }

    /// <summary>One charge on a bill (a row of bill_items).</summary>
    public class BillItem
    {
        public int BillItemId { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }

        /// <summary>Quantity x unit price, rounded to 2 decimals (the database computes the same value).</summary>
        public decimal Amount => BillCalculator.Round2(Quantity * UnitPrice);

        public string QuantityText => Quantity.ToString(CultureInfo.InvariantCulture);
        public string UnitPriceText => UnitPrice.ToString("N2", CultureInfo.InvariantCulture);
        public string AmountText => Amount.ToString("N2", CultureInfo.InvariantCulture);

        public BillItem Clone() => new BillItem
        {
            BillItemId = BillItemId,
            Category = Category,
            Description = Description,
            Quantity = Quantity,
            UnitPrice = UnitPrice
        };
    }

    /// <summary>A bill with its items (bills + bill_items).</summary>
    public class Bill
    {
        public string BillId { get; set; } = string.Empty;     // BILL-YYYYMMDD-XXX; empty = not saved yet
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string Status { get; set; } = BillingRules.StatusDraft;
        public string DiscountType { get; set; } = BillingRules.DiscountFlat;
        public decimal DiscountValue { get; set; }
        public decimal TaxRate { get; set; }            // a fraction: 0.12 = 12 %
        public string AuthorizedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? FinalizedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public List<BillItem> Items { get; set; } = new List<BillItem>();

        public bool IsSaved => BillId.Length > 0;
        public bool IsFinalized => Status == BillingRules.StatusFinalized;
        public decimal Subtotal => BillCalculator.Round2(Items.Sum(i => i.Amount));
    }

    /// <summary>The four amounts at the bottom of a bill.</summary>
    public class BillTotals
    {
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal GrandTotal { get; set; }
    }

    /// <summary>
    /// THE BILL CALCULATION (used live on the screen and again by the repository when the bill is saved).
    ///
    ///   1. Subtotal        = sum of every item amount (quantity x unit price)
    ///   2. Discount        = flat amount, or Subtotal x percent / 100 (rounded to 2 decimals)
    ///   3. Taxable amount  = Subtotal - Discount
    ///   4. Tax             = Taxable amount x tax rate (rounded to 2 decimals)   - tax is charged AFTER the discount
    ///   5. Grand total     = Taxable amount + Tax
    ///
    /// The view v_bill_totals in the database uses the same order, so the screen and the reports always agree.
    ///
    /// AUTHORIZATION: a discount above the threshold needs a name in "Authorized By" before the bill can be finalized.
    /// The threshold comes from app_settings (discount_threshold_percent = 20 and discount_threshold_amount = 5000):
    /// authorization is needed when the discount is MORE THAN 20 % of the subtotal OR MORE THAN 5,000.00 in money,
    /// whichever way the discount was typed (a flat amount is converted to a percentage for the comparison).
    /// </summary>
    public static class BillCalculator
    {
        public static decimal Round2(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);

        public static decimal DiscountAmount(decimal subtotal, string discountType, decimal discountValue)
        {
            return discountType == BillingRules.DiscountPercent
                ? Round2(subtotal * discountValue / 100m)
                : Round2(discountValue);
        }

        public static BillTotals Calculate(decimal subtotal, string discountType, decimal discountValue, decimal taxRate)
        {
            decimal discount = DiscountAmount(subtotal, discountType, discountValue);
            decimal taxable = subtotal - discount;
            decimal tax = Round2(taxable * taxRate);
            return new BillTotals
            {
                Subtotal = subtotal,
                DiscountAmount = discount,
                TaxableAmount = taxable,
                TaxAmount = tax,
                GrandTotal = taxable + tax
            };
        }

        public static BillTotals Calculate(Bill bill) =>
            Calculate(bill.Subtotal, bill.DiscountType, bill.DiscountValue, bill.TaxRate);

        /// <summary>The discount as a percentage of the subtotal (for a percent discount: the typed value).</summary>
        public static decimal DiscountPercentOf(decimal subtotal, string discountType, decimal discountValue, decimal discountAmount)
        {
            if (discountType == BillingRules.DiscountPercent) return discountValue;
            return subtotal > 0 ? discountAmount / subtotal * 100m : 0m;
        }

        public static bool NeedsAuthorization(decimal subtotal, string discountType, decimal discountValue,
                                              decimal percentThreshold, decimal amountThreshold)
        {
            decimal amount = DiscountAmount(subtotal, discountType, discountValue);
            if (amount <= 0) return false;
            decimal percent = DiscountPercentOf(subtotal, discountType, discountValue, amount);
            return percent > percentThreshold || amount > amountThreshold;
        }
    }

    /// <summary>One entry of a price list (service, medicine, lab test or room type).</summary>
    public class PriceListItem
    {
        public const string SelectId = "<select>";      // the "Select the item..." placeholder
        public const string OtherId = "<other>";        // "other / custom item"

        /// <summary>The key of the priced row (MED-001, TST-001, a room type or a service name), or SelectId / OtherId.</summary>
        public string Id { get; set; } = SelectId;
        public bool IsPlaceholder => Id == SelectId;
        public bool IsOther => Id == OtherId;
        public bool IsListed => !IsPlaceholder && !IsOther;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string DisplayText { get; set; } = string.Empty;

        public override string ToString() => DisplayText;
    }

    /// <summary>One row of the "All Bills" list.</summary>
    public class BillListRow
    {
        public string BillId { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public int ItemCount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime CreatedAt { get; set; }

        public string ItemCountText => ItemCount.ToString(CultureInfo.InvariantCulture);
        public string SubtotalText => Subtotal.ToString("N2", CultureInfo.InvariantCulture);
        public string DiscountText => DiscountAmount.ToString("N2", CultureInfo.InvariantCulture);
        public string TaxText => TaxAmount.ToString("N2", CultureInfo.InvariantCulture);
        public string GrandTotalText => GrandTotal.ToString("N2", CultureInfo.InvariantCulture);
        public string CreatedText => CreatedAt.ToString("MMM dd, yyyy  hh:mm tt", CultureInfo.InvariantCulture);
    }

    /// <summary>Which part of the screen a validation message belongs to.</summary>
    public enum BillingField { Patient, Items, Discount, Tax, Authorization }

    /// <summary>One problem found when a bill is saved or finalized.</summary>
    public class BillingIssue
    {
        public BillingField Field { get; }
        public string Message { get; }

        public BillingIssue(BillingField field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}