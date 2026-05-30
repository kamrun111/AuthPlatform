namespace AuthPlatform.Mvc.Models.Auth
{
    public class AuthUserViewModel
    {
        public int AuthUserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public List<int> AuthGroupIds { get; set; } = new();
    }
}