using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Auth.Entities
{


    public class AuthUser : BaseEntity
    {
        public int AuthUserId { get; set; }


        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsLocked { get; set; }

        public int FailedLoginAttempts { get; set; }

        public ICollection<AuthUserPermission> UserPermissions { get; set; }
            = new List<AuthUserPermission>();

        public ICollection<AuthUserGroup> AuthUserGroups { get; set; }
              = new List<AuthUserGroup>();

        public ICollection<AuthRefreshToken> RefreshTokens { get; set; }
            = new List<AuthRefreshToken>();
    }
}
