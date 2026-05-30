namespace AuthPlatform.Mvc.Models.Expenditures
{
    public class ExpenditureInvoiceDetailViewModel
    {
        public int ExpenditureInvoiceDetailId { get; set; }

        public int ExpenditureInvoiceId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string? Description { get; set; }
        public DateTime? ExpenseDate { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }

        public bool IsActive { get; set; } = true;
        public int? RecordCreatedBy { get; set; }

        public int? RecordUpdatedBy { get; set; }
    }
}
