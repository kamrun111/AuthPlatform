using AuthPlatform.Mvc.Filters;
using AuthPlatform.Mvc.Models;
using AuthPlatform.Mvc.Models.Expenditures;
using AuthPlatform.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace AuthPlatform.Mvc.Controllers.Expenditures
{

    [SessionAuthorize]
    public class ExpenditureInvoiceDetailController : Controller
    {
        private readonly ApiClientService _apiClient;

        public ExpenditureInvoiceDetailController(ApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int invoiceId)
        {
            ViewBag.InvoiceId = invoiceId;

            var response = await _apiClient
                .GetAsync<ApiResponse<List<ExpenditureInvoiceDetailViewModel>>>(
                    $"expenditure-invoice-details/by-invoice/{invoiceId}");

            return View(response?.Data ?? new List<ExpenditureInvoiceDetailViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Create(int invoiceId)
        {
            await LoadInvoicesAsync();

            return View(new ExpenditureInvoiceDetailViewModel
            {
                ExpenditureInvoiceId = invoiceId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenditureInvoiceDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadInvoicesAsync();
                return View(model);
            }

            var response = await _apiClient
                .PostAsync<ApiResponse<string>>(
                    "expenditure-invoice-details",
                    model);

            if (response?.Success != true)
            {
                ViewBag.Error = response?.Message ?? "Create failed.";
                await LoadInvoicesAsync();
                return View(model);
            }

            TempData["Success"] = "Invoice detail created successfully.";

            return RedirectToAction(nameof(Index), new
            {
                invoiceId = model.ExpenditureInvoiceId
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _apiClient
                .GetAsync<ApiResponse<ExpenditureInvoiceDetailViewModel>>(
                    $"expenditure-invoice-details/{id}");

            if (response?.Success != true || response.Data == null)
            {
                TempData["Error"] = "Invoice detail not found.";
                return RedirectToAction("Index", "ExpenditureInvoice");
            }

            await LoadInvoicesAsync();

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExpenditureInvoiceDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadInvoicesAsync();
                return View(model);
            }

            var response = await _apiClient
                .PutAsync<ApiResponse<string>>(
                    $"expenditure-invoice-details/{model.ExpenditureInvoiceDetailId}",
                    model);

            if (response?.Success != true)
            {
                ViewBag.Error = response?.Message ?? "Update failed.";
                await LoadInvoicesAsync();
                return View(model);
            }

            TempData["Success"] = "Invoice detail updated successfully.";

            return RedirectToAction(nameof(Index), new
            {
                invoiceId = model.ExpenditureInvoiceId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int invoiceId)
        {
            var response = await _apiClient
                .DeleteAsync<ApiResponse<string>>(
                    $"expenditure-invoice-details/{id}");

            TempData[response?.Success == true ? "Success" : "Error"] =
                response?.Success == true
                    ? "Invoice detail deleted successfully."
                    : response?.Message ?? "Delete failed.";

            return RedirectToAction(nameof(Index), new
            {
                invoiceId
            });
        }

        private async Task LoadInvoicesAsync()
        {
            var response = await _apiClient
                .GetAsync<ApiResponse<List<ExpenditureInvoiceViewModel>>>(
                    "expenditure-invoices");

            ViewBag.Invoices = new SelectList(
                response?.Data ?? new List<ExpenditureInvoiceViewModel>(),
                "ExpenditureInvoiceId",
                "InvoiceNumber");
        }
    }
}
