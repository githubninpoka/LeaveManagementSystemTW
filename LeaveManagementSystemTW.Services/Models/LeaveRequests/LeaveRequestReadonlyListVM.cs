using System.ComponentModel;
using LeaveManagementSystemTW.Services;

namespace LeaveManagementSystemTW.Services.Models.LeaveRequests;

public class LeaveRequestReadonlyListVM
{
    public int Id { get; set; }

    [DisplayName("Start date")]
    public DateOnly StartDate { get; set; }

    [DisplayName("End date")]
    public DateOnly EndDate { get; set; }

    [DisplayName("Total Days")]
    public int NumberOfDays { get; set; }

    [DisplayName("Leave type")]
    public string LeaveType { get; set; } = string.Empty;

    [DisplayName("Status")]
    public LeaveRequestService.LeaveRequestStatusEnum LeaveRequestStatus { get; set; }
}