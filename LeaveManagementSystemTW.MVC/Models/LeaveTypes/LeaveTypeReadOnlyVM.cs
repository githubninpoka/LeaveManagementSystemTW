using System.ComponentModel;

namespace LeaveManagementSystemTW.MVC.Models.LeaveTypes;

public class LeaveTypeReadOnlyVM
{
    public int Id { get; set; }
    [DisplayName("Type of leave")]
    public string Name { get; set; } = string.Empty;
    [DisplayName("Available days")]
    public int Days { get; set; }
}
