using AuthPlatform.Mvc.Authorization.Attributes;
using AuthPlatform.Mvc.Filters;
using AuthPlatform.Mvc.Models;
using AuthPlatform.Mvc.Models.Expenditures;
using AuthPlatform.Mvc.Services;
using AuthPlatform.Mvc.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthPlatform.Mvc.Controllers.Expenditures
{
    [SessionAuthorize]
    [PermissionAuthorize]
    public class ExpenditureInvoiceController : Controller
    {
        private readonly ApiClientService _apiClient;
        private readonly TokenSessionManager _tokenSessionManager;

        public ExpenditureInvoiceController(ApiClientService apiClient, TokenSessionManager tokenSessionManager)
        {
            _apiClient = apiClient;
            _tokenSessionManager = tokenSessionManager;
        }

        [HttpGet]
       
        public async Task<IActionResult> Index()
        {
            var response = await _apiClient.GetAsync<ApiResponse<List<ExpenditureInvoiceViewModel>>>("expenditure-invoices");

            return View(response?.Data ?? new List<ExpenditureInvoiceViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ExpenditureInvoiceViewModel
            {
                InvoiceDate = DateTime.Today,
                Details = new List<ExpenditureInvoiceDetailViewModel>
                {
                    new ExpenditureInvoiceDetailViewModel()
                }
            };

            await LoadExpenditureHeadsAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenditureInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadExpenditureHeadsAsync();
                return View(model);
            }

            model.RecordCreatedBy = _tokenSessionManager.GetAuthUserId();

            var response = await _apiClient.PostAsync<ApiResponse<string>>("expenditure-invoices", model);

            if (response?.Success != true)
            {
                ViewBag.Error = response?.Message ?? "Create failed.";
                await LoadExpenditureHeadsAsync();
                return View(model);
            }

            TempData["success"] = "Expenditure invoice created successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _apiClient.GetAsync<ApiResponse<ExpenditureInvoiceViewModel>>($"expenditure-invoices/{id}");

            if (response?.Success != true || response.Data == null)
            {
                TempData["error"] = "Expenditure invoice not found.";

                return RedirectToAction(nameof(Index));
            }

            await LoadExpenditureHeadsAsync();

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExpenditureInvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadExpenditureHeadsAsync();
                return View(model);
            }

            model.RecordUpdatedBy = _tokenSessionManager.GetAuthUserId();

            var response = await _apiClient.PutAsync<ApiResponse<string>>($"expenditure-invoices/{model.ExpenditureInvoiceId}", model);

            if (response?.Success != true)
            {
                ViewBag.Error = response?.Message ?? "Update failed.";
                await LoadExpenditureHeadsAsync();
                return View(model);
            }

            TempData["success"] = "Expenditure invoice updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _apiClient.DeleteAsync<ApiResponse<string>>($"expenditure-invoices/{id}");

            if (response?.Success == true)
            {
                TempData["success"] = "Expenditure invoice deleted successfully.";
            }
            else
            {
                TempData["error"] = response?.Message ?? "Delete failed.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadExpenditureHeadsAsync()
        {
            var response = await _apiClient.GetAsync<ApiResponse<List<ExpenditureHeadViewModel>>>("expenditure-heads");

            var heads = response?.Data ?? new List<ExpenditureHeadViewModel>();

            var activeHeads = heads.Where(x => x.IsActive).ToList();

            ViewBag.ExpenditureHeads = new SelectList(activeHeads, "ExpenditureHeadId", "ExpenditureHeadName");
        }
    }
}