using LeaveManagementSystem.Domain.Models;

namespace LeaveManagementSystemTW.Services
{
    public interface ILeaveAllocationsService
    {
        Task AllocateLeaveAsync(string employeeId);
        Task<List<LeaveAllocation>> GetAllocationsAsync();
    }
}