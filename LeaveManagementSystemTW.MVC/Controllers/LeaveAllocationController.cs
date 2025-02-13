using LeaveManagementSystemTW.Security.Data;
using LeaveManagementSystemTW.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystemTW.MVC.Controllers
{
    [Authorize] // only logged on users
    public class LeaveAllocationController(ILeaveAllocationsService _leaveAllocationsService, SecurityDbContext _securityDbContext) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var leaveAllocations = await _leaveAllocationsService.GetAllocationsAsync();
            return View(leaveAllocations);
        }
    }
}
