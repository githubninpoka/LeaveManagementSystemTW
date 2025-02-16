using LeaveManagementSystemTW.Security.Data;
using LeaveManagementSystemTW.Services;
using LeaveManagementSystemTW.Services.Models.LeaveAllocations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystemTW.MVC.Controllers
{
    [Authorize] // only logged on users
    public class LeaveAllocationController(
        ILeaveAllocationsService _leaveAllocationsService,
        ILeaveTypesService _leaveTypesService) : Controller
    {
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            var employees = await _leaveAllocationsService.GetEmployeesAsync();
            return View(employees);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave(string? id = null)
        {
            await _leaveAllocationsService.AllocateLeaveAsync(id);
            return RedirectToAction(nameof(Details), new { userId = id });
        }

        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> EditAllocation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LeaveAllocationEditVM allocationVM = await _leaveAllocationsService.GetEmployeeAllocationAsync(allocationId: id.Value);
            if (allocationVM == null)
            {
                return NotFound();
            }
            return View(allocationVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM leaveAllocationEditVM)
        {
            //var days = leaveAllocationEditVM.NumberOfDays;
            //leaveAllocationEditVM = await _leaveAllocationsService.GetEmployeeAllocationAsync(leaveAllocationEditVM.Id);
            //leaveAllocationEditVM.NumberOfDays = days;
            if (await _leaveTypesService.DaysExceedMaximum(
                leaveAllocationEditVM.LeaveType.Id,
                leaveAllocationEditVM.NumberOfDays))
            {
                ModelState.AddModelError("NumberOfDays", "The allocation exceeds maximum allocation value");
            }
            if (ModelState.IsValid)
            {
                await _leaveAllocationsService.EditAllocationAsync(leaveAllocationEditVM);
                return RedirectToAction(nameof(Details), new { userId = leaveAllocationEditVM.Employee!.Id });
            }
            //var days = leaveAllocationEditVM.NumberOfDays;
            //var allocation = await _leaveAllocationsService.GetEmployeeAllocationAsync(leaveAllocationEditVM.Id);
            return View(leaveAllocationEditVM);
        }
        public async Task<IActionResult> Details(string? userId = null)
        {
            var employeeAllocation = await _leaveAllocationsService.GetEmployeeAllocationsAsync(userId);
            return View(employeeAllocation);
        }
    }
}
