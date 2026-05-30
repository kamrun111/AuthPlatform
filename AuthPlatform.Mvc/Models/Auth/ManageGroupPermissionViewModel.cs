

using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthPlatform.Mvc.Models.Auth
{
    public class ManageGroupPermissionViewModel
    {
        public int AuthGroupId { get; set; }

        public List<SelectListItem> Groups { get; set; } = new();

        public List<PermissionItemViewModel> Permissions { get; set; }= new();
    }


}
