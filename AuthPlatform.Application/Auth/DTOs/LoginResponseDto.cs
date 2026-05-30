using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPlatform.Application.Auth.DTOs
{
    public class LoginResponseDto
    {
        public int AuthUserId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LasttName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime AccessTokenExpiration { get; set; }

        public List<string> Permissions { get; set; } = new();
        public List<string> GroupPermissions  { get; set; } = new();
    }
}
