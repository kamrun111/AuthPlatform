using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Domain.Common
{
    public abstract class BaseEntity
    {
        public bool IsActive { get; set; } = true;

        public DateTime? RecordCreatedDate { get; set; }

        public int? RecordCreatedBy { get; set; }

        public DateTime? RecordUpdatedDate { get; set; }

        public int? RecordUpdatedBy { get; set; }
    }
}
