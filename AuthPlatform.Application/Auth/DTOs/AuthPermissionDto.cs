using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Application.Auth.DTOs
{
    public class AuthPermissionDto
    {
        public int AuthPermissionId { get; set; }

        public string PermissionName { get; set; } = string.Empty;

        public string PermissionCode { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
