namespace AuthPlatform.Mvc.Models.Auth
{
    public class PermissionItemViewModel
    {
        public int AuthPermissionId { get; set; }

        public string PermissionName { get; set; } = string.Empty;

        public string PermissionCode { get; set; } = string.Empty;

        public bool IsSelected { get; set; }
    }
}
