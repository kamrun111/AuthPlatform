using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Application.Auth.DTOs
{
    public class AuthUserPermissionDto
    {
        public int AuthUserPermissionId { get; set; }

        public int AuthUserId { get; set; }

        public int AuthPermissionId { get; set; }

        public string? UserName { get; set; }

        public string? PermissionName { get; set; }

        public string? PermissionCode { get; set; }
    }
}
