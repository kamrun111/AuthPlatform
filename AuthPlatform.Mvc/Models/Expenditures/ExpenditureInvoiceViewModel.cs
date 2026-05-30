using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AuthPlatform.Mvc.Models.Expenditures
{
    public class ExpenditureInvoiceViewModel
    {
        public int ExpenditureInvoiceId { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Expenditure head is required.")]
        public int ExpenditureHeadId { get; set; }

        public string? ExpenditureHeadName { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsActive { get; set; } = true;

        public int? RecordCreatedBy { get; set; }

        public int? RecordUpdatedBy { get; set; }

        public List<ExpenditureInvoiceDetailViewModel> Details { get; set; } = new();

        public List<SelectListItem> ExpenditureHeads { get; set; } = new();
    }
}
