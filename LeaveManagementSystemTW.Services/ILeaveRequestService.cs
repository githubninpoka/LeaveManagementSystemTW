using LeaveManagementSystemTW.Services.Models.LeaveRequests;

namespace LeaveManagementSystemTW.Services;

public interface ILeaveRequestService
{
    Task CreateLeaveRequestAsync(LeaveRequestCreateVM model);
    Task<EmployeeLeaveRequestListVM> GetEmployeeLeaveRequestsAsync();
    Task<LeaveRequestListVM> GetAllLeaveRequestsAsync();
    Task CancelLeaveRequestAsync(int leaveRequestId);
    Task ReviewLeaveRequestAsync(ReviewLeaveRequestVM model);

    Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model);
}