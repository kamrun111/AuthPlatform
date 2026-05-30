namespace AuthPlatform.Mvc.Reports.ReportDataSets.Expenditure
{
    public class ExpenditureInvoiceFilterReportDto
    {
        public string InvoiceNumber { get; set; } = string.Empty;

        public string InvoiceDate { get; set; } = string.Empty;

        public string ExpenditureHeadName { get; set; } = string.Empty;

        public string ExpenseDate { get; set; } = string.Empty;

        public string ItemName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }

        public decimal GrandTotalAmount { get; set; }

        public string FilterStartDate { get; set; } = string.Empty;

        public string FilterEndDate { get; set; } = string.Empty;

        public string FilterHeadName { get; set; } = string.Empty;

        public string PrintDate { get; set; } = string.Empty;

        public string PrintedBy { get; set; } = string.Empty;
    }
}