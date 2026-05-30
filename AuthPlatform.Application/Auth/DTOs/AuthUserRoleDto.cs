using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Application.Auth.DTOs
{
    public class AuthUserGroupDto
    {
        public int AuthUserGroupId { get; set; }

        public int AuthUserId { get; set; }

        public int AuthGroupId{ get; set; }

        public string? UserName { get; set; }

        public string? GroupName { get; set; }
    }
}
