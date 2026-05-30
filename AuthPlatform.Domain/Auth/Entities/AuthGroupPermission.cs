using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Common;

namespace AuthPlatform.Domain.Auth.Entities
{
    public class AuthGroupPermission : BaseEntity
    {
        public int AuthGroupPermissionId { get; set; }
        public int AuthGroupId { get; set; }
        public AuthGroup AuthGroup { get; set; } = null!;

        public int AuthPermissionId { get; set; }
        public AuthPermission AuthPermission { get; set; } = null!;
    }
}
