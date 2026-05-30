using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Auth.Entities
{
    public class AuthUserGroup : BaseEntity
    {
        // Primary Key
        public int AuthUserGroupId { get; set; }

        // Foreign Key
        public int AuthUserId { get; set; }

        public AuthUser AuthUser { get; set; } = null!;

        // Foreign Key
        public int AuthGroupId { get; set; }

        public AuthGroup AuthGroup { get; set; } = null!;
    }
}
