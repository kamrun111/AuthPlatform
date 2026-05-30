using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Auth.Entities
{
    public class AuthRefreshToken : BaseEntity
    {
        public int AuthRefreshTokenId { get; set; }

        public int AuthUserId { get; set; }
        public AuthUser AuthUser { get; set; } = null!;

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime? RevokedAt { get; set; }
    }
}
