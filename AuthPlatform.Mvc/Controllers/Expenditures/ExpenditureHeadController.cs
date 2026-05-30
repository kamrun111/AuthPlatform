using Microsoft.AspNetCore.Mvc;
using AuthPlatform.Mvc.Models;
using AuthPlatform.Mvc.Services;
using AuthPlatform.Mvc.Models.Expenditures;
using AuthPlatform.Mvc.Filters;
using AuthPlatform.Mvc.Session;
using AuthPlatform.Mvc.Authorization.Attributes;



namespace AuthPlatform.Mvc.Controllers.Expenditures
{



    [SessionAuthorize]
    [PermissionAuthorize]
    public class ExpenditureHeadController : Controller
    {
        private readonly ApiClientService _apiClient;
        private readonly TokenSessionManager _tokenSessionManager;

        public ExpenditureHeadController(ApiClientService apiClient, TokenSessionManager tokenSessionManager)
        {
            _apiClient = apiClient;
            _tokenSessionManager = tokenSessionManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _apiClient
                .GetAsync<ApiResponse<List<ExpenditureHeadViewModel>>>(
                    "expenditure-heads");

            var data = response?.Data;

            if (data == null)
            {
                data = new List<ExpenditureHeadViewModel>();
            }

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenditureHeadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.RecordCreatedBy = _tokenSessionManager.GetAuthUserId();
            var response = await _apiClient
                .PostAsync<ApiResponse<string>>(
                    "expenditure-heads",
                    model);

            if (response == null || !response.Success)
            {
                ViewBag.Error = response?.Message ?? "Failed to create expenditure head.";
                return View(model);
            }
            TempData["success"] = "Expenditure head created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _apiClient
                .GetAsync<ApiResponse<ExpenditureHeadViewModel>>(
                    $"expenditure-heads/{id}");

            if (response == null || !response.Success || response.Data == null)
            {
                return RedirectToAction(nameof(Index));
            }
            

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExpenditureHeadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.RecordUpdatedBy = _tokenSessionManager.GetAuthUserId();
            var response = await _apiClient
                .PutAsync<ApiResponse<string>>(
                    $"expenditure-heads/{model.ExpenditureHeadId}",
                    model);

            if (response == null || !response.Success)
            {
                ViewBag.Error = response?.Message ?? "Failed to update expenditure head.";
                return View(model);
            }
            TempData["success"] = "Expenditure head updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _apiClient
                .DeleteAsync<ApiResponse<string>>(
                    $"expenditure-heads/{id}");
            TempData["success"] = "Expenditure head deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
