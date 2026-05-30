using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Expenditures.Entities
{
    public class ExpenditureInvoiceDetail : BaseEntity
    {
        public int ExpenditureInvoiceDetailId { get; set; }
        public int ExpenditureInvoiceId { get; set; }
        public ExpenditureInvoice ExpenditureInvoice { get; set; } = null!;

        public string ItemName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public DateTime? ExpenseDate { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }

    }
}
