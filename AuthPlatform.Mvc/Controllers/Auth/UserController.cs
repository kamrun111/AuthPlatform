using AuthPlatform.Mvc.Models;
using AuthPlatform.Mvc.Models.Auth;
using AuthPlatform.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthPlatform.Mvc.Controllers.Auth
{
    public class UserController : Controller
    {
        private readonly ApiClientService _apiClient;

        public UserController(ApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response =
                await _apiClient.GetAsync<ApiResponse<List<AuthUserViewModel>>>(
                    "users");

            return View(response?.Data ?? new List<AuthUserViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new UserCreateViewModel();

            await LoadGroupsAsync(model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(
                    nameof(model.ConfirmPassword),
                    "Password and confirm password do not match.");
            }
            if (!model.AuthGroupIds.Any())
            {
                ModelState.AddModelError(
                    nameof(model.AuthGroupIds),
                    "At least one group must be selected.");
            }

            if (!ModelState.IsValid)
            {
                await LoadGroupsAsync(model);
                return View(model);
            }

            var request = new
            {
                model.FirstName,
                model.LastName,
                model.UserName,
                model.Email,
                model.Password,
                model.IsActive,
                model.AuthGroupIds
            };

            var response =
                await _apiClient.PostAsync<ApiResponse<string>>(
                    "users",
                    request);

            if (response?.Success == true)
            {
                TempData["success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = response?.Message ?? "User creation failed.";

            await LoadGroupsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int authUserId)
        {
            var response =
                await _apiClient.GetAsync<ApiResponse<AuthUserViewModel>>(
                    $"users/{authUserId}");

            if (response?.Success != true || response.Data == null)
            {
                TempData["error"] = "User not found.";
                return RedirectToAction(nameof(Index));
            }

            var model = new UserCreateViewModel
            {
                AuthUserId = response.Data.AuthUserId,
                FirstName = response.Data.FirstName,
                LastName = response.Data.LastName,
                UserName = response.Data.UserName,
                Email = response.Data.Email,
                IsActive = response.Data.IsActive,
                AuthGroupIds = response.Data.AuthGroupIds
            };

            await LoadGroupsAsync(model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserCreateViewModel model)
        {
            if (!model.AuthGroupIds.Any())
            {
                ModelState.AddModelError(nameof(model.AuthGroupIds),
                    "At least one group must be selected.");
            }
            if (!ModelState.IsValid)
            {
                await LoadGroupsAsync(model);
                return View(model);
            }

            var request = new
            {
                model.AuthUserId,
                model.FirstName,
                model.LastName,
                model.UserName,
                model.Email,
                model.IsActive,
                model.AuthGroupIds
            };

            var response =
                await _apiClient.PutAsync<ApiResponse<string>>(
                    $"users/{model.AuthUserId}",
                    request);

            if (response?.Success == true)
            {
                TempData["success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = response?.Message ?? "User update failed.";

            await LoadGroupsAsync(model);

            return View(model);
        }

        private async Task LoadGroupsAsync(UserCreateViewModel model)
        {
            var groupsResponse =
                await _apiClient.GetAsync<ApiResponse<List<AuthGroupViewModel>>>(
                    "groups");

            model.Groups = groupsResponse?.Data?
                .Where(x => x.IsActive)
                .Select(x => new SelectListItem
                {
                    Value = x.AuthGroupId.ToString(),
                    Text = x.GroupName,
                    Selected = model.AuthGroupIds.Contains(x.AuthGroupId)
                })
                .ToList()
                ?? new List<SelectListItem>();
        }
    }
}