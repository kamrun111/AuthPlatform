using AuthPlatform.Mvc.Models;
using AuthPlatform.Mvc.Models.Auth;
using AuthPlatform.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthPlatform.Mvc.Controllers.Auth
{
    public class GroupPermissionController : Controller
    {
        private readonly ApiClientService _apiClient;

        public GroupPermissionController(ApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Manage(int authGroupId = 0)
        {
            var model = new ManageGroupPermissionViewModel();

            // Load groups
            var groupsResponse =
                await _apiClient.GetAsync<ApiResponse<List<AuthGroupViewModel>>>(
                    "groups");

            if (groupsResponse?.Data != null)
            {
                model.Groups = groupsResponse.Data
                    .Select(x => new SelectListItem
                    {
                        Value = x.AuthGroupId.ToString(),
                        Text = x.GroupName
                    })
                    .ToList();
            }

            // Load all permissions
            var permissionsResponse =
                await _apiClient.GetAsync<ApiResponse<List<PermissionItemViewModel>>>(
                    "permissions");

            var selectedPermissions = new List<string>();

            // Load selected group permissions
            if (authGroupId > 0)
            {
                model.AuthGroupId = authGroupId;

                var selectedResponse =
                    await _apiClient.GetAsync<ApiResponse<List<string>>>(
                        $"permissions/group/{authGroupId}");

                selectedPermissions =
                    selectedResponse?.Data ?? new List<string>();
            }

            // Bind permissions
            if (permissionsResponse?.Data != null)
            {
                model.Permissions = permissionsResponse.Data
                    .Select(x => new PermissionItemViewModel
                    {
                        AuthPermissionId = x.AuthPermissionId,
                        PermissionName = x.PermissionName,
                        PermissionCode = x.PermissionCode,
                        IsSelected = selectedPermissions.Contains(x.PermissionCode)
                    })
                    .ToList();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ManageGroupPermissionViewModel model)
        {
            var selectedPermissionIds = model.Permissions
                .Where(x => x.IsSelected)
                .Select(x => x.AuthPermissionId)
                .ToList();

            var request = new
            {
                AuthGroupId = model.AuthGroupId,
                AuthPermissionIds = selectedPermissionIds
            };

            await _apiClient.PostAsync<ApiResponse<string>>(
                "permissions/save-group-permissions",
                request);

            TempData["success"] =
                "Group permissions saved successfully.";

            return RedirectToAction(nameof(Manage), new
            {
                authGroupId = model.AuthGroupId
            });
        }
    }
}