using AutoMapper;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Domain.Models;
using LeaveManagementSystemTW.Services.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystemTW.Services;

public class LeaveTypesService : ILeaveTypesService
{
    private IMapper _mapper;
    private LeaveManagementSystemDbContext _leaveManagementSystemDbContext;

    public LeaveTypesService(LeaveManagementSystemDbContext leaveManagementSystemDbContext, IMapper mapper)
    {
        _leaveManagementSystemDbContext = leaveManagementSystemDbContext;
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeReadOnlyVM>> GetAllAsync()
    {
        var data = await _leaveManagementSystemDbContext.LeaveTypes.ToListAsync();
        return _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
    }

    public async Task<T?> GetAsync<T>(int id) where T : class
    {
        var data = await _leaveManagementSystemDbContext.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (data == null)
        {
            return null;
        }
        return _mapper.Map<T>(data);
    }

    public async Task EditAsync(LeaveTypeEditVM leaveTypeEditVM)
    {
        var leaveType = _mapper.Map<LeaveType>(leaveTypeEditVM);
        _leaveManagementSystemDbContext.Update(leaveType);
        await _leaveManagementSystemDbContext.SaveChangesAsync();
    }

    public async Task CreateAsync(LeaveTypeCreateVM leaveTypeCreateVM)
    {
        var leaveType = _mapper.Map<LeaveType>(leaveTypeCreateVM);
        _leaveManagementSystemDbContext.Add(leaveType);
        await _leaveManagementSystemDbContext.SaveChangesAsync();
    }
    public async Task RemoveAsync(int id)
    {
        var toRemove = await _leaveManagementSystemDbContext.LeaveTypes.FindAsync(id);
        if (toRemove != null)
        {
            _leaveManagementSystemDbContext.Remove(toRemove);
            await _leaveManagementSystemDbContext.SaveChangesAsync();
        }
        // have asked the trainer how to handle erroneous deletes if this method has no return type
    }

    public bool LeaveTypeExists(int id)
    {
        return _leaveManagementSystemDbContext.LeaveTypes.Any(e => e.Id == id);
    }

    public async Task<bool> LeaveTypeNameExistsAsync(string leaveTypeName)
    {
        if (await _leaveManagementSystemDbContext.LeaveTypes.AnyAsync(leavetype => leavetype.Name.ToLower() == leaveTypeName.ToLower()))
        {
            return true;
        }
        return false;
    }

    public async Task<bool> LeaveTypeNameExistsForEditAsync(LeaveTypeEditVM leaveTypeEdit)
    {
        if (await _leaveManagementSystemDbContext.LeaveTypes.AnyAsync(leavetype => leavetype.Name.ToLower() == leaveTypeEdit.Name.ToLower() && leavetype.Id != leaveTypeEdit.Id))
        {
            return true;
        }
        return false;
    }

    public async Task<bool> DaysExceedMaximum(int leaveTypeId, int days)
    {
        var leaveType = await _leaveManagementSystemDbContext.LeaveTypes.FindAsync(leaveTypeId);
        return leaveType.NumberOfDays < days;
    }
}
