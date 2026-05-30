namespace AuthPlatform.Mvc.Models.Auth
{
    public class LoginResponseViewModel
    {
        public int AuthUserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime AccessTokenExpiration { get; set; }

        public List<string> Permissions { get; set; } = new();

        public List<string> RolePermissions { get; set; } = new();
      
    }
}
