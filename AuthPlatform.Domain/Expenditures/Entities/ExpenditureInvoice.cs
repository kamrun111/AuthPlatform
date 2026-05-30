using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Expenditures.Entities
{
    public class ExpenditureInvoice : BaseEntity
    {
        
        public int ExpenditureInvoiceId { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }

        public decimal TotalAmount { get; set; }

        // Foreign Key
        public int ExpenditureHeadId { get; set; }

        // Navigation Property (Many Invoices → One Head)
        public ExpenditureHead ExpenditureHead { get; set; } = null!;

        // Navigation Property (One Invoice → Many Details)
        public ICollection<ExpenditureInvoiceDetail> Details { get; set; }
            = new List<ExpenditureInvoiceDetail>();
    }
}
