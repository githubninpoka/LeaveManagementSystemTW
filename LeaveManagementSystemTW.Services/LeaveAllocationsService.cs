using LeaveManagementSystem.Data;
using LeaveManagementSystem.Domain.Models;
using LeaveManagementSystemTW.Security.Data;
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
    IHttpContextAccessor httpContextAccessor,
    UserManager<ApplicationUser> _userManager) : ILeaveAllocationsService
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

    public async Task<List<LeaveAllocation>> GetAllocationsAsync()
    {
        var user = await _userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
        if (user == null)
        {
            throw new Exception("There was no employeeId");
        }

        var leaveAllocations = await _lmsContext.LeaveAllocations
            .Where(q => q.EmployeeId == user.Id)
            .Include(q => q.LeaveType)
            .Include(q => q.Period)
            .ToListAsync();
        return leaveAllocations;
    }
}
