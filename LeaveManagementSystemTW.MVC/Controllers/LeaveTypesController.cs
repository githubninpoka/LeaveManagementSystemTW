using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystemTW.Services;
using LeaveManagementSystemTW.Services.Models.LeaveTypes;
using Microsoft.AspNetCore.Authorization;
using LeaveManagementSystemTW.Services.Common;

namespace LeaveManagementSystemTW.MVC.Controllers
{
    [Authorize(Roles =Roles.Administrator)]
    public class LeaveTypesController : Controller
    {
        //private readonly LeaveManagementSystemDbContext _context;
        //private readonly IMapper _mapper;
        private readonly ILeaveTypesService _leaveTypesService;

        public LeaveTypesController(ILeaveTypesService leaveTypesService)
        {
            _leaveTypesService = leaveTypesService;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            var dataDtos = await _leaveTypesService.GetAllAsync();
            return View(dataDtos);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var leaveTypeDto = await _leaveTypesService.GetAsync<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveTypeDto == null)
            {
                return NotFound();
            }
            return View(leaveTypeDto);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Days")] LeaveTypeCreateVM leaveTypeVM)
        {
            if ( await _leaveTypesService.LeaveTypeNameExistsAsync(leaveTypeVM.Name))
            {
                ModelState.AddModelError("LeaveType", $"Leavetype {leaveTypeVM.Name} already exists");
            }
            if (ModelState.IsValid)
            {
                await _leaveTypesService.CreateAsync(leaveTypeVM);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeVM);
        }



        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveTypeEditVM = await _leaveTypesService.GetAsync<LeaveTypeEditVM>(id.Value);
            if (leaveTypeEditVM == null)
            {
                return NotFound();
            }

            return View(leaveTypeEditVM);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Days")] LeaveTypeEditVM leaveTypeEditVM)
        {
            if (id != leaveTypeEditVM.Id)
            {
                return NotFound();
            }

            if (await _leaveTypesService.LeaveTypeNameExistsForEditAsync(leaveTypeEditVM))
            {
                ModelState.AddModelError("LeaveType", $"Leavetype {leaveTypeEditVM.Name} already exists");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveTypesService.EditAsync(leaveTypeEditVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_leaveTypesService.LeaveTypeExists(leaveTypeEditVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeEditVM);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypesService.GetAsync<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveType = await _leaveTypesService.GetAsync<LeaveTypeReadOnlyVM>(id);
            if (leaveType != null)
            {
                await _leaveTypesService.RemoveAsync(leaveType.Id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
