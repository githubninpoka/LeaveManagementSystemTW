using LeaveManagementSystemTW.Services.Models.LeaveTypes;

namespace LeaveManagementSystemTW.Services
{
    public interface ILeaveTypesService
    {
        Task CreateAsync(LeaveTypeCreateVM leaveTypeCreateVM);
        Task EditAsync(LeaveTypeEditVM leaveTypeEditVM);
        Task<List<LeaveTypeReadOnlyVM>> GetAllAsync();
        Task<T?> GetAsync<T>(int id) where T : class;
        bool LeaveTypeExists(int id);
        Task<bool> LeaveTypeNameExistsAsync(string leaveTypeName);
        Task<bool> LeaveTypeNameExistsForEditAsync(LeaveTypeEditVM leaveTypeEdit);
        Task RemoveAsync(int id);
    }
}