using System.Globalization;

namespace HospitalManagementSystem.Models.MP12_Payment
{
    /// <summary>The limits and fixed values of Step 16. The form and the repository both use these constants.</summary>
    public static class PaymentRules
    {
        // payments.payment_method (an ENUM in the database)
        public const string MethodCash = "Cash";
        public const string MethodDebit = "Debit";
        public const string MethodCredit = "Credit";
        public const string MethodOnline = "Online";
        public const string MethodCheque = "Cheque";

        /// <summary>Every method; Debit, Credit and Online need a reference number (also a CHECK in the database).</summary>
        public static readonly PaymentMethod[] Methods =
        {
            new PaymentMethod { Id = MethodCash, Name = MethodCash },
            new PaymentMethod { Id = MethodDebit, Name = MethodDebit, RequiresReference = true },
            new PaymentMethod { Id = MethodCredit, Name = MethodCredit, RequiresReference = true },
            new PaymentMethod { Id = MethodOnline, Name = MethodOnline, RequiresReference = true },
            new PaymentMethod { Id = MethodCheque, Name = MethodCheque }
        };

        // bills.status
        public const string BillFinalized = "Finalized";

        // payment status words (v_bill_payment_summary)
        public const string Outstanding = "Outstanding";
        public const string PartiallyPaid = "Partially Paid";
        public const string FullyPaid = "Fully Paid";

        public const int MaxReference = 60;
        public const decimal MaxAmount = 99999999.99m;
        public const int ListLimit = 300;
        public const int PayableLimit = 200;
    }

    /// <summary>A payment method (Cash, Debit, Credit, Online, Cheque) and whether it needs a reference number.</summary>
    public class PaymentMethod
    {
        /// <summary>The stored value (Cash, Debit ...); empty for the "Select" placeholder.</summary>
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool RequiresReference { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// A finalized bill with its totals: Grand total, Amount paid so far and Balance (from the view v_bill_payment_summary).
    /// STATUS TRANSITION: Outstanding (nothing paid) -> Partially Paid (paid more than 0 but less than the total) ->
    /// Fully Paid (paid = total). The status is never stored; it is derived from the payments every time.
    /// </summary>
    public class PayableBill
    {
        public string BillId { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public DateTime BillDate { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;

        public string TotalText => GrandTotal.ToString("N2", CultureInfo.InvariantCulture);
        public string PaidText => AmountPaid.ToString("N2", CultureInfo.InvariantCulture);
        public string BalanceText => Balance.ToString("N2", CultureInfo.InvariantCulture);
        public string DateText => BillDate.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);
    }

    /// <summary>One payment of a bill (a row of payments).</summary>
    public class PaymentRecord
    {
        /// <summary>PAY-YYYYMMDD-XXX (also the receipt number).</summary>
        public string PaymentId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string MethodName { get; set; } = string.Empty;
        public string ReferenceNumber { get; set; } = string.Empty;
        public decimal? Tendered { get; set; }
        public string ReceivedBy { get; set; } = string.Empty;
        public DateTime PaidAt { get; set; }

        public string AmountText => Amount.ToString("N2", CultureInfo.InvariantCulture);
        public string ReferenceText => ReferenceNumber.Length == 0 ? "-" : ReferenceNumber;
        public string TenderedText => Tendered.HasValue ? Tendered.Value.ToString("N2", CultureInfo.InvariantCulture) : "-";
        public string PaidAtText => PaidAt.ToString("MMM dd, yyyy  hh:mm tt", CultureInfo.InvariantCulture);
    }

    /// <summary>What the cashier typed for a new payment.</summary>
    public class PaymentInput
    {
        public string BillId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty;
        public string ReferenceNumber { get; set; } = string.Empty;
        public decimal? Tendered { get; set; }      // cash only
    }

    /// <summary>Which part of the screen a validation message belongs to.</summary>
    public enum PaymentField { Bill, Amount, Method, Reference, Tendered }

    public class PaymentIssue
    {
        public PaymentField Field { get; }
        public string Message { get; }

        public PaymentIssue(PaymentField field, string message)
        {
            Field = field;
            Message = message;
        }
    }

    /// <summary>Everything printed on an Official Receipt (the receipt dialog only writes the text).</summary>
    public class Receipt
    {
        public string PaymentId { get; set; } = string.Empty;
        public string BillId { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public decimal BillTotal { get; set; }
        public decimal AmountPaid { get; set; }          // this transaction
        public decimal PaidBefore { get; set; }          // paid on the bill before this payment
        public decimal NewBalance { get; set; }          // balance after this payment
        public string MethodName { get; set; } = string.Empty;
        public string ReferenceNumber { get; set; } = string.Empty;
        public decimal? Tendered { get; set; }
        public string ReceivedBy { get; set; } = string.Empty;
        public DateTime PaidAt { get; set; }
        public string StatusAfter { get; set; } = string.Empty;

        public decimal? Change => Tendered.HasValue ? Tendered.Value - AmountPaid : null;

        public static string Money(decimal value) => value.ToString("N2", CultureInfo.InvariantCulture);
    }
}