namespace AuthPlatform.Mvc.Reports.ReportDataSets.Expenditure
{



    public class ExpenditureInvoiceReportDto
    {
        public string InvoiceNumber { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }

        public string ExpenditureHeadName { get; set; } = string.Empty;

        public string ItemName { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string ExpenseDate { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime PrintDate { get; set; }

        public string PrintedBy { get; set; } = string.Empty;
    }
}
