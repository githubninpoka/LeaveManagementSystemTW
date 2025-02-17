namespace LeaveManagementSystemTW.Services;

public partial class LeaveRequestService
{
    public enum LeaveRequestStatusEnum
    {
        Pending = 1,
        Approved = 2,
        Declined = 3,
        Cancelled = 4
    }
}
