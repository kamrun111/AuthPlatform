using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Application.Expenditures.DTOs
{
    public class ExpenditureInvoiceDto
    {
        public int ExpenditureInvoiceId { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }

        public int ExpenditureHeadId { get; set; }

        public string? ExpenditureHeadName { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsActive { get; set; }

        public int? RecordCreatedBy { get; set; }

        public int? RecordUpdatedBy { get; set; }
        public List<ExpenditureInvoiceDetailDto> Details { get; set; } = new();
    }
}
