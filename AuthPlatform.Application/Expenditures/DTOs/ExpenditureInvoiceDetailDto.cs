using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Application.Expenditures.DTOs
{
    public class ExpenditureInvoiceDetailDto
    {
        public int ExpenditureInvoiceDetailId { get; set; }

        public int ExpenditureInvoiceId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string? Description { get; set; }
        public DateTime? ExpenseDate { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }

        public bool IsActive { get; set; }

        public int? RecordCreatedBy { get; set; }

        public int? RecordUpdatedBy { get; set; }
    }
}
