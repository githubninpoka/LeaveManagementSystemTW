using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystemTW.Services.Models.LeaveRequests;

public class LeaveRequestCreateVM : IValidatableObject
{
    [Required]
    [DisplayName("Start date")]
    public DateOnly StartDate { get; set; }
    [Required]
    [DisplayName("Last date of your leave")]
    public DateOnly EndDate { get; set; }
    [DisplayName("Type of Leave")]
    [Required]
    public int LeaveTypeId { get; set; }

    [DisplayName("Further information")]
    [StringLength(144)]
    public string? RequestComments { get; set; }

    public SelectList? LeaveTypes { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate > EndDate)
        {
            yield return new ValidationResult("Start date must be before End date", new[]{nameof(StartDate), nameof(EndDate)});
        }
    }
}