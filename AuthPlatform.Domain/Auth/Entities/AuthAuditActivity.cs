using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Auth.Entities
{
    public class AuthAuditActivity : BaseEntity
    {
        public int AuthAuditActivityId { get; set; }
        public int? PerformedByUserId { get; set; }

        public string? PerformedByUserName { get; set; }

        public string Action { get; set; } = string.Empty;

        public string EntityName { get; set; } = string.Empty;

        public string? EntityId { get; set; }

        public string? Description { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public string? IpAddress { get; set; }

        public string? UserAgent { get; set; }

        public DateTime? ActivityAt { get; set; }
    }
}
