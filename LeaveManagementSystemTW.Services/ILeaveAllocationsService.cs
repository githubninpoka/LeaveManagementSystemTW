using LeaveManagementSystem.Domain.Models;
using LeaveManagementSystemTW.Services.Models.LeaveAllocations;

namespace LeaveManagementSystemTW.Services
{
    public interface ILeaveAllocationsService
    {
        Task AllocateLeaveAsync(string employeeId);
        Task<List<LeaveAllocation>> GetAllocationsAsync(string? userId);
        Task<EmployeeAllocationVM> GetEmployeeAllocationsAsync(string? userId);
        Task<LeaveAllocationEditVM> GetEmployeeAllocationAsync(int allocationId);
        Task<List<EmployeeListVM>> GetEmployeesAsync();
        Task EditAllocationAsync(LeaveAllocationEditVM leaveAllocationEditVM);
    }
}