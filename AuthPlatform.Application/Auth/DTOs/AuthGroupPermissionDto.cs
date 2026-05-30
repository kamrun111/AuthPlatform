using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Application.Auth.DTOs
{
    public class AuthGroupPermissionDto
    {
        public int AuthRolePermissionId { get; set; }

        public int AuthGroupId{ get; set; }

        public int AuthPermissionId { get; set; }

        public string? GroupName { get; set; }

        public string? PermissionName { get; set; }

        public string? PermissionCode { get; set; }
    }
}
