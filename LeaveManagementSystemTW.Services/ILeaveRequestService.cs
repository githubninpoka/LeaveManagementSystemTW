using LeaveManagementSystemTW.Services.Models.LeaveRequests;

namespace LeaveManagementSystemTW.Services;

public interface ILeaveRequestService
{
    Task CreateLeaveRequestAsync(LeaveRequestCreateVM model);
    Task<List<LeaveRequestReadonlyListVM>> GetEmployeeLeaveRequestsAsync();
    Task CancelLeaveRequestAsync(int leaveRequestId);

    Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequestsAsync();
    Task<ReviewLeaveRequestVM> GetLeaveRequestForReviewAsync(int leaveRequestId);
    Task ReviewLeaveRequestAsync(int leaveRequestId, bool approved);

    Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model);
}