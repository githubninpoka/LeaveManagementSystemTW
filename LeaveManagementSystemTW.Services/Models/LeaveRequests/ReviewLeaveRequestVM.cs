using LeaveManagementSystemTW.Services.Models.LeaveAllocations;

namespace LeaveManagementSystemTW.Services.Models.LeaveRequests;

public class ReviewLeaveRequestVM : LeaveRequestReadonlyListVM
{
    public EmployeeListVM Employee { get; set; } = new();
    public string? RequestComment { get; set; }
}