using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystemTW.Services.Models.LeaveTypes;

public class LeaveTypeEditVM
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    [DisplayName("Type of leave")]
    public string Name { get; set; }

    [Required]
    [Range(1, 365)]
    [DisplayName("Available days")]
    public int Days { get; set; }
}

