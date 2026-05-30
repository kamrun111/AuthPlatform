using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthPlatform.Mvc.Models.Auth
{
    public class UserPermissionViewModel
    {
        public int AuthUserId { get; set; }

        public List<SelectListItem> Users { get; set; } = new();

        public List<PermissionItemViewModel> Permissions { get; set; }
            = new();
    }
}
