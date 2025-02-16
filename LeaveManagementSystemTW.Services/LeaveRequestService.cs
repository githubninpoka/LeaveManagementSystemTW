using AutoMapper;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Domain.Models;
using LeaveManagementSystemTW.Security.Data;
using LeaveManagementSystemTW.Services.Models.LeaveRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystemTW.Services;

public partial class LeaveRequestService(
    IMapper _mapper,
    UserManager<ApplicationUser> _userManager,
    IHttpContextAccessor _httpContextAccessor,
    LeaveManagementSystemDbContext _leaveManagementSystemDbContext
    ) : ILeaveRequestService
{
    public Task CancelLeaveRequestAsync(int leaveRequestId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateLeaveRequestAsync(LeaveRequestCreateVM model)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(model);
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        var userId = user.Id;
        leaveRequest.EmployeeId = userId;
        leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatus.Pending;
        var days = model.EndDate.DayNumber - model.StartDate.DayNumber;
        var allocation = await _leaveManagementSystemDbContext.LeaveAllocations
            .FirstAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveRequest.LeaveTypeId);
        
        allocation.Days = allocation.Days - days;
        _leaveManagementSystemDbContext.Add(leaveRequest);
        _leaveManagementSystemDbContext.Update(allocation);
        await _leaveManagementSystemDbContext.SaveChangesAsync();

        

    }

    public Task<LeaveRequestListVM> GetAllLeaveRequestsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeLeaveRequestListVM> GetEmployeeLeaveRequestsAsync()
    {
        throw new NotImplementedException();
    }

    public Task ReviewLeaveRequestAsync(ReviewLeaveRequestVM model)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model)
    {
        int numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        var allocation = await _leaveManagementSystemDbContext.LeaveAllocations.FirstAsync(x => x.LeaveTypeId == model.LeaveTypeId && x.EmployeeId == user.Id);
        
        return allocation.Days < numberOfDays;
    }

}
