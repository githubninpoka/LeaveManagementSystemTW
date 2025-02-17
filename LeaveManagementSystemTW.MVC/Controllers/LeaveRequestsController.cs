using LeaveManagementSystemTW.Services;
using LeaveManagementSystemTW.Services.Models.LeaveRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagementSystemTW.MVC.Controllers;

[Authorize]
public class LeaveRequestsController(
    ILeaveTypesService _leaveTypesService,
    ILeaveRequestService _leaveRequestService
    ) : Controller
{
    // view requests by employee
    public async Task<IActionResult> Index()
    {
        var leaveRequests = await _leaveRequestService.GetEmployeeLeaveRequestsAsync();
        return View(leaveRequests);
    }

    // create requests
    public async Task<IActionResult> Create()
    {
        var leaveTypes = await _leaveTypesService.GetAllAsync();
        SelectList leaveTypesList = new SelectList(leaveTypes, "Id", "Name");
        var model = new LeaveRequestCreateVM
        {
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            LeaveTypes = leaveTypesList
        };


        return View(model);
    }

    // create requests post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveRequestCreateVM model)
    {
        if (await _leaveRequestService.RequestDatesExceedAllocation(model))
        {
            ModelState.AddModelError(nameof(model.EndDate), "That number of days is not available for your allocation.");
        }
        if (ModelState.IsValid)
        {
            await _leaveRequestService.CreateLeaveRequestAsync(model);
            return RedirectToAction("Index");
        }
        var leaveTypes = await _leaveTypesService.GetAllAsync();
        SelectList leaveTypesList = new SelectList(leaveTypes, "Id", "Name");
        model.LeaveTypes = leaveTypesList;
        return View(model);
    }

    // employee cancel
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int leaveRequestId )
    {
        await _leaveRequestService.CancelLeaveRequestAsync(leaveRequestId);

        return RedirectToAction("Index");
    }

    // admin view requests
    public async Task<IActionResult> ListRequests()
    {
        var model = await _leaveRequestService.AdminGetAllLeaveRequestsAsync();
        return View(model);
    }

    // admin review request
    public async Task<IActionResult> Review(int leaveRequestId)
    {
        ReviewLeaveRequestVM reviewLeaveRequestVM = await _leaveRequestService.GetLeaveRequestForReviewAsync(leaveRequestId);
        return View(reviewLeaveRequestVM);
    }
    // admin review post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review(int leaveRequestId, bool approved)
    {
        await _leaveRequestService.ReviewLeaveRequestAsync(leaveRequestId, approved);
        return RedirectToAction("ListRequests");
    }
}
