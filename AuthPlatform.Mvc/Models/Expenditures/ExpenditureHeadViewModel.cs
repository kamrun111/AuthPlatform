using System.ComponentModel.DataAnnotations;

namespace AuthPlatform.Mvc.Models.Expenditures
{
    public class ExpenditureHeadViewModel
    {
    public int ExpenditureHeadId { get; set; }

    [Required(ErrorMessage = "Expenditure head name is required.")]
    public string ExpenditureHeadName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public int? RecordCreatedBy { get; set; }

    public int? RecordUpdatedBy { get; set; }
    }
}
