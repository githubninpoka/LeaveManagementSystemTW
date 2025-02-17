using AutoMapper;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Domain.Models;
using LeaveManagementSystemTW.Security.Data;
using LeaveManagementSystemTW.Services.Models.LeaveAllocations;
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
    public async Task CancelLeaveRequestAsync(int leaveRequestId)
    {
        var response = await _leaveManagementSystemDbContext.LeaveRequests.FindAsync(leaveRequestId);
        response.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Cancelled;

        // restore the number of days in the allocation
        var thisYear = response.EndDate.Year;
        var thisPeriod = await _leaveManagementSystemDbContext.Periods.FirstAsync(x => x.EndDate.Year == thisYear);
        var numberOfDays = response.EndDate.DayNumber - response.StartDate.DayNumber;
        var allocationToAdd = await _leaveManagementSystemDbContext.LeaveAllocations
            .FirstAsync(x => x.LeaveTypeId == response.LeaveTypeId && x.EmployeeId == response.EmployeeId && x.PeriodId == thisPeriod.Id);
        allocationToAdd.Days += numberOfDays;

        await _leaveManagementSystemDbContext.SaveChangesAsync();
    }

    public async Task CreateLeaveRequestAsync(LeaveRequestCreateVM model)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(model);
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        var userId = user.Id;
        var currentPeriod = await _leaveManagementSystemDbContext.Periods.SingleAsync(x => x.EndDate.Year == DateTime.Now.Year);
        leaveRequest.EmployeeId = userId;
        leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;
        var days = model.EndDate.DayNumber - model.StartDate.DayNumber;
        var allocation = await _leaveManagementSystemDbContext.LeaveAllocations
            .FirstAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveRequest.LeaveTypeId && x.Period.Id == currentPeriod.Id);
        
        allocation.Days = allocation.Days - days;
        _leaveManagementSystemDbContext.Add(leaveRequest);
        _leaveManagementSystemDbContext.Update(allocation);
        await _leaveManagementSystemDbContext.SaveChangesAsync();

        

    }

    public async Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequestsAsync()
    {
        var leaveRequests = await _leaveManagementSystemDbContext.LeaveRequests
            .Include(x => x.LeaveType)
            .ToListAsync();

        // remember, for this specific case there was no mapper defined.
        var leaveRequestVM = leaveRequests.Select(x => new LeaveRequestReadonlyListVM
        {
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Id = x.Id,
            LeaveType = x.LeaveType.Name,
            LeaveRequestStatus = (LeaveRequestStatusEnum)x.LeaveRequestStatusId,
            NumberOfDays = x.EndDate.DayNumber - x.StartDate.DayNumber
        }).ToList();

        var model = new EmployeeLeaveRequestListVM
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(x => x.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved),
            PendingRequests = leaveRequests.Count(x => x.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending),
            DeclinedRequests = leaveRequests.Count(x => x.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Declined),
            LeaveRequests = leaveRequestVM

        };
        return model;
    }

    public async Task<List<LeaveRequestReadonlyListVM>> GetEmployeeLeaveRequestsAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        var leaveRequests = await _leaveManagementSystemDbContext.LeaveRequests
            .Include(x => x.LeaveType)
            .Where(x => x.EmployeeId == user.Id)
            .ToListAsync();

        // not a mapper this time, but could do
        List<LeaveRequestReadonlyListVM> model = leaveRequests.Select(x => new LeaveRequestReadonlyListVM
        {
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Id = x.Id,
            LeaveType = x.LeaveType.Name,
            LeaveRequestStatus = (LeaveRequestStatusEnum)x.LeaveRequestStatusId,
            NumberOfDays = x.EndDate.DayNumber - x.StartDate.DayNumber
        }).ToList();

        return model;
    }

    public async Task ReviewLeaveRequestAsync(int leaveRequestId, bool approved)
    {
        var reviewingUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        var request = await _leaveManagementSystemDbContext.LeaveRequests.FindAsync(leaveRequestId);
        request.LeaveRequestStatusId = approved ? (int)LeaveRequestStatusEnum.Approved : (int)LeaveRequestStatusEnum.Declined;
        request.ReviewerId = reviewingUser.Id;
        if (!approved)
        {
            // i know this bit of code is a bit redundant but the teachers logic introduces some serious logical flaws.
            var thisPeriod = await _leaveManagementSystemDbContext.Periods
                .FirstAsync(x => x.EndDate.Year == request.EndDate.Year);
            var allocation = await _leaveManagementSystemDbContext.LeaveAllocations
                .FirstAsync(
                x => x.EmployeeId == request.EmployeeId && 
                x.LeaveTypeId == request.LeaveTypeId &&
                x.PeriodId == thisPeriod.Id);
            allocation.Days += request.EndDate.DayNumber - request.StartDate.DayNumber;
        }
        await _leaveManagementSystemDbContext.SaveChangesAsync();
    }
    public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model)
    {
        int numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        Period thisPeriod = await _leaveManagementSystemDbContext.Periods.FirstAsync(x => x.EndDate.Year == model.EndDate.Year);
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        var allocation = await _leaveManagementSystemDbContext.LeaveAllocations.FirstAsync(
            x => x.LeaveTypeId == model.LeaveTypeId && 
            x.EmployeeId == user.Id && 
            x.PeriodId == thisPeriod.Id);
        
        return allocation.Days < numberOfDays;
    }

    public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReviewAsync(int leaveRequestId)
    {
        var request = await _leaveManagementSystemDbContext.LeaveRequests
            .Include(q => q.LeaveType)
            .FirstAsync(q => q.Id == leaveRequestId);

        var requestingUser = await _userManager.FindByIdAsync(request.EmployeeId);

        var model = new ReviewLeaveRequestVM
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            NumberOfDays = request.EndDate.DayNumber - request.StartDate.DayNumber,
            LeaveRequestStatus = (LeaveRequestStatusEnum)request.LeaveRequestStatusId,
            Id = request.Id,
            LeaveType = request.LeaveType.Name,
            RequestComment = request.RequestComments,
            Employee = new EmployeeListVM
            {
                Id = request.EmployeeId,
                FirstName = requestingUser.FirstName,
                LastName = requestingUser.LastName,
                Email = requestingUser.Email,
            }
        };

        return model;

    }
}
