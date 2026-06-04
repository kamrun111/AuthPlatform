using AuthPlatform.Mvc.Models;
using AuthPlatform.Mvc.Models.Expenditures;
using AuthPlatform.Mvc.Reports.ReportDataSets.Expenditure;
using AuthPlatform.Mvc.Services;
using AuthPlatform.Mvc.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthPlatform.Mvc.Reports.ReportControllers
{
    public class ExpenditureReportController : Controller
    {
        private readonly ApiClientService _apiClient;
        private readonly TokenSessionManager _tokenSessionManager;

        public ExpenditureReportController(
            ApiClientService apiClient,
            TokenSessionManager tokenSessionManager)
        {
            _apiClient = apiClient;
            _tokenSessionManager = tokenSessionManager;
        }

        [HttpGet]
        public async Task<IActionResult> Invoice(int id)
        {
            var response =
                await _apiClient.GetAsync<ApiResponse<ExpenditureInvoiceViewModel>>(
                    $"expenditure-invoices/{id}");

            if (response?.Success != true || response.Data == null)
            {
                return RedirectToAction("Index", "ExpenditureInvoice");
            }

            var invoice = response.Data;
            var printedBy = _tokenSessionManager.GetDisplayName();

            var reportData = invoice.Details.Select(x => new ExpenditureInvoiceReportDto
            {
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                ExpenditureHeadName = invoice.ExpenditureHeadName ?? "",
                ExpenseDate = x.ExpenseDate?.ToString("dd/MM/yyyy") ?? "",
                ItemName = x.ItemName,
                Description = x.Description,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                LineTotal = x.LineTotal,
                TotalAmount = invoice.TotalAmount,
                PrintDate = DateTime.Now,
                PrintedBy = printedBy
            }).ToList();

            if (!reportData.Any())
            {
                return BadRequest("No records found for this invoice.");
            }

            return View("~/Views/ExpenditureReport/Invoice.cshtml", reportData);
        }

        [HttpGet]
        public async Task<IActionResult> InvoiceFilter()
        {
            var model = new ExpenditureInvoiceFilterViewModel
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };

            await LoadExpenditureHeadsAsync(model);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> InvoiceFilterPrint(
            DateTime startDate,
            DateTime endDate,
            int? expenditureHeadId)
        {
            if (startDate.Date > endDate.Date)
            {
                return BadRequest("Start Date cannot be greater than End Date.");
            }

            var response =
                await _apiClient.GetAsync<ApiResponse<List<ExpenditureInvoiceViewModel>>>(
                    "expenditure-invoices");

            if (response?.Success != true || response.Data == null)
            {
                return BadRequest("Invoice data could not be loaded.");
            }

            var invoices = response.Data
                .Where(x => x.InvoiceDate.Date >= startDate.Date &&
                            x.InvoiceDate.Date <= endDate.Date)
                .ToList();

            if (expenditureHeadId.HasValue && expenditureHeadId.Value > 0)
            {
                invoices = invoices
                    .Where(x => x.ExpenditureHeadId == expenditureHeadId.Value)
                    .ToList();
            }

            var printedBy = _tokenSessionManager.GetDisplayName();

            var filterHeadName = "All Heads";

            if (expenditureHeadId.HasValue && expenditureHeadId.Value > 0)
            {
                filterHeadName = invoices
                    .FirstOrDefault()?.ExpenditureHeadName ?? "Selected Head";
            }

            var reportData = invoices
                .SelectMany(invoice => invoice.Details.Select(detail =>
                    new ExpenditureInvoiceFilterReportDto
                    {
                        InvoiceNumber = invoice.InvoiceNumber,
                        InvoiceDate = invoice.InvoiceDate.ToString("dd/MM/yyyy"),
                        ExpenditureHeadName = invoice.ExpenditureHeadName ?? "",
                        ExpenseDate = detail.ExpenseDate?.ToString("dd/MM/yyyy") ?? "",
                        ItemName = detail.ItemName,
                        Description = detail.Description,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice,
                        LineTotal = detail.LineTotal,

                        FilterStartDate = startDate.ToString("dd/MM/yyyy"),
                        FilterEndDate = endDate.ToString("dd/MM/yyyy"),
                        FilterHeadName = filterHeadName,

                        PrintDate = DateTime.Now.ToString("dd/MM/yyyy"),
                        PrintedBy = printedBy
                    }))
                .ToList();

            if (!reportData.Any())
            {
                return BadRequest("No records found for selected filter.");
            }

            var grandTotal = reportData.Sum(x => x.LineTotal);

            foreach (var item in reportData)
            {
                item.GrandTotalAmount = grandTotal;
            }

            return View("~/Views/ExpenditureReport/InvoiceFilterPrint.cshtml", reportData);
        }

        private async Task LoadExpenditureHeadsAsync(
            ExpenditureInvoiceFilterViewModel model)
        {
            var response =
                await _apiClient.GetAsync<ApiResponse<List<ExpenditureHeadViewModel>>>(
                    "expenditure-heads");

            model.ExpenditureHeads = response?.Data?
                .Where(x => x.IsActive)
                .Select(x => new SelectListItem
                {
                    Value = x.ExpenditureHeadId.ToString(),
                    Text = x.ExpenditureHeadName
                })
                .ToList()
                ?? new List<SelectListItem>();
        }
    }
}