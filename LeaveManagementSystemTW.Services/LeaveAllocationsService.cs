using AutoMapper;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Domain.Models;
using LeaveManagementSystemTW.Security.Data;
using LeaveManagementSystemTW.Services.Common;
using LeaveManagementSystemTW.Services.Models.LeaveAllocations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystemTW.Services;

public class LeaveAllocationsService(
    LeaveManagementSystemDbContext _lmsContext,
    IHttpContextAccessor _httpContextAccessor,
    UserManager<ApplicationUser> _userManager,
    IMapper _mapper) : ILeaveAllocationsService
{
    public async Task AllocateLeaveAsync(string employeeId)
    {
        // get all leave types
        var leaveTypes = await _lmsContext.LeaveTypes.ToListAsync();

        // get current period
        var currentDate = DateTime.Now;
        var currentYear = DateTime.Now.Year;

        var currentPeriod = await _lmsContext.Periods.SingleAsync(q => q.EndDate.Year == currentYear);
        var daysInThisYear = DateTime.IsLeapYear(currentYear) ? 366 : 365;
        decimal multiplier = (decimal)daysInThisYear / (daysInThisYear - currentDate.DayOfYear);
        // for each leave type, create an entry
        foreach (var leaveType in leaveTypes)
        {
            if (await AllocationExistsAsync(userId: employeeId,periodId: currentPeriod.Id,leaveTypeId: leaveType.Id))
            {
                continue;
            }
            var leaveAllocation = new LeaveAllocation
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = currentPeriod.Id,
                // calc leave based on number of months left in the period. this is probably accurate enough for the purpose of this exercise.
                Days = (int)(leaveType.NumberOfDays / multiplier),
            };
            _lmsContext.Add(leaveAllocation);
        }
        await _lmsContext.SaveChangesAsync();
    }

    public async Task EditAllocationAsync(LeaveAllocationEditVM leaveAllocationEditVM)
    {
        LeaveAllocationEditVM toEditAllocation = await GetEmployeeAllocationAsync(leaveAllocationEditVM.Id);
        if (toEditAllocation == null)
        {
            throw new NullReferenceException();
        }

        toEditAllocation.NumberOfDays = leaveAllocationEditVM.NumberOfDays;
        // option 1: bigger query
        // option 1 _lmsContext.Update(toEditAllocation);
        // option 2 _lmsContext.Entry(toEditAllocation).State = EntityState.Modified;
        // option 1 & 2 await _lmsContext.SaveChangesAsync();
        // option 3: when you know you just want to update one attribute
        await _lmsContext.LeaveAllocations
            .Where(x => x.Id == leaveAllocationEditVM.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, leaveAllocationEditVM.NumberOfDays));
    }

    public async Task<List<LeaveAllocation>> GetAllocationsAsync(string? userId = null)
    {
        string employeeId;
        if (!string.IsNullOrEmpty(userId))
        {
            employeeId = userId;
        }
        else
        {

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                throw new Exception("There was no employeeId");
            }
            employeeId = user.Id;
        }
        var period = await _lmsContext.Periods.SingleAsync(q => q.EndDate.Year == DateTime.Now.Year);
        var leaveAllocations = await _lmsContext.LeaveAllocations
            .Where(q => q.EmployeeId == employeeId && q.Period == period)
            .Include(q => q.LeaveType)
            .Include(q => q.Period)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<LeaveAllocationEditVM> GetEmployeeAllocationAsync(int allocationId)
    {
        var allocation = await _lmsContext.LeaveAllocations
            .Include(x => x.LeaveType)
            .Include(x => x.Period)
            .SingleAsync(x => x.Id == allocationId);
            
        var leaveAllocationEdit = _mapper.Map<LeaveAllocationEditVM>(allocation);
        var user = await  _userManager.FindByIdAsync(allocation.EmployeeId);
        leaveAllocationEdit.Employee = _mapper.Map<EmployeeListVM>(user);
        return leaveAllocationEdit;
    }

    public async Task<EmployeeAllocationVM> GetEmployeeAllocationsAsync(string? userId = null)
    {

        var user = string.IsNullOrEmpty(userId) ?
            await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User) :
            await _userManager.FindByIdAsync(userId);

        var allocations = await GetAllocationsAsync(userId);
        var allocationsVmList = _mapper.Map<List<LeaveAllocationVM>>(allocations);
        var numberOfLeaveTypes = await _lmsContext.LeaveTypes.CountAsync();

        var employeeVm = new EmployeeAllocationVM
        {
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            LeaveAllocations = allocationsVmList,
            IsAllocationCompleted = numberOfLeaveTypes == allocations.Count

        };
        return employeeVm;

    }

    public async Task<List<EmployeeListVM>> GetEmployeesAsync()
    {
        var employees = await _userManager.GetUsersInRoleAsync(Roles.Employee);
        var employeeList = _mapper.Map<List<EmployeeListVM>>(employees);
        return employeeList;
    }

    private async Task<bool> AllocationExistsAsync(string userId, int periodId, int leaveTypeId)
    {
        return await _lmsContext.LeaveAllocations.AnyAsync(x => x.LeaveTypeId == leaveTypeId && x.EmployeeId == userId && x.PeriodId == periodId);
    }
}
