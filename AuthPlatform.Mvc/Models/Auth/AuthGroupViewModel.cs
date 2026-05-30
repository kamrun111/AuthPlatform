namespace AuthPlatform.Mvc.Models.Auth
{
    public class AuthGroupViewModel
    {
        public int AuthGroupId { get; set; }

        public string GroupName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
