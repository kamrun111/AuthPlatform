using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Auth.Entities
{
    public class AuthLoginHistory : BaseEntity
    {
        public int AuthLoginHistoryId { get; set; }
        public int? AuthUserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public bool IsSuccess { get; set; }
        public string? FailureReason { get; set; }

        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public DateTime LoginAt { get; set; } = DateTime.UtcNow;
    }
}
