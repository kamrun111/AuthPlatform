using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Auth.Entities
{
    public class AuthGroup : BaseEntity
    {
        public int AuthGroupId { get; set; }

        public string GroupName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<AuthGroupPermission> AuthGroupPermissions { get; set; }
            = new List<AuthGroupPermission>();

        public ICollection<AuthUserGroup> AuthUserGroups { get; set; }
            = new List<AuthUserGroup>();
    }
}
