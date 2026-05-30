using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Application.Auth.DTOs
{
    public class SaveUserPermissionsDto
    {
        public int AuthUserId { get; set; }

        public List<int> AuthPermissionIds { get; set; } = new();
    }
}
