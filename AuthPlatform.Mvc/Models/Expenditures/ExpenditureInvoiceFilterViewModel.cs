using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AuthPlatform.Mvc.Models.Expenditures
{
    public class ExpenditureInvoiceFilterViewModel
    {
        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Expenditure Head")]
        public long? ExpenditureHeadId { get; set; }

        public List<SelectListItem> ExpenditureHeads { get; set; } = new();
    }
}
