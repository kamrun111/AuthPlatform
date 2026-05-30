using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Application.Expenditures.DTOs
{
    public class ExpenditureHeadDto
    {
        public int ExpenditureHeadId { get; set; }

        public string ExpenditureHeadName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public int? RecordCreatedBy { get; set; }

        public int? RecordUpdatedBy { get; set; }
    }
}
