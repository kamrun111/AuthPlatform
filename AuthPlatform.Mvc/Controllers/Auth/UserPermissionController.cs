using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Mvc.Models.Auth;
using AuthPlatform.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AuthPlatform.Mvc.Controllers.Auth
{

        public class UserPermissionController : Controller
        {
            private readonly ApiClientService _apiClient;

            public UserPermissionController(ApiClientService apiClient)
            {
                _apiClient = apiClient;
            }

            [HttpGet]
            public async Task<IActionResult> Manage(int authUserId = 0)
            {
                var model = new UserPermissionViewModel();

                var usersResponse =await _apiClient.GetAsync<ApiResponse<List<AuthUserViewModel>>>("users");

                if (usersResponse?.Data != null)
                {
                    model.Users = usersResponse.Data
                        .Select(x => new SelectListItem
                        {
                            Value = x.AuthUserId.ToString(),
                            Text = $"{x.UserName} - {x.FirstName} {x.LastName}"
                        })
                        .ToList();
                }

                var permissionsResponse =
                    await _apiClient.GetAsync<ApiResponse<List<PermissionItemViewModel>>>(
                        "permissions");

                var selectedPermissions = new List<string>();

                if (authUserId > 0)
                {
                    model.AuthUserId = authUserId;

                    var selectedResponse =
                        await _apiClient.GetAsync<ApiResponse<List<string>>>(
                            $"permissions/user/{authUserId}");

                    selectedPermissions =
                        selectedResponse?.Data ?? new List<string>();
                }

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
            public async Task<IActionResult> Save(UserPermissionViewModel model)
            {
                var selectedPermissionIds = model.Permissions
                    .Where(x => x.IsSelected)
                    .Select(x => x.AuthPermissionId)
                    .ToList();

                var request = new
                {
                    AuthUserId = model.AuthUserId,
                    AuthPermissionIds = selectedPermissionIds
                };

                await _apiClient.PostAsync<ApiResponse<string>>(
                    "permissions/save-user-permissions",
                    request);

                TempData["success"] = "User permissions saved successfully.";

                return RedirectToAction(nameof(Manage), new
                {
                    authUserId = model.AuthUserId
                });
            }
        }
    
}
