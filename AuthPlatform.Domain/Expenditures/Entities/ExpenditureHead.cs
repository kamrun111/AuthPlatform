using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Expenditures.Entities
{
    public class ExpenditureHead : BaseEntity
    {
        public int ExpenditureHeadId { get; set; }

        public string ExpenditureHeadName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<ExpenditureInvoice> Invoices { get; set; } = new List<ExpenditureInvoice>();
    }
}
