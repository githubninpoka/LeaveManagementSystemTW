using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Domain.Models;
using LeaveManagementSystemTW.MVC.Models.LeaveTypes;
using AutoMapper;

namespace LeaveManagementSystemTW.MVC.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly LeaveManagementSystemDbContext _context;
        private readonly IMapper mapper;

        public LeaveTypesController(LeaveManagementSystemDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            //var data = await _context.LeaveTypes.Select(item => new IndexViewModel
            //{
            //    Id = item.Id,
            //    Name = item.Name,
            //    Days = item.NumberOfDays
            //}).ToListAsync();
            var data = await _context.LeaveTypes.ToListAsync();
            var dataDtos = mapper.Map<List<LeaveTypeReadOnlyVM>> (data);
            return View(dataDtos);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            var leaveTypeDto = mapper.Map<LeaveTypeReadOnlyVM>(leaveType);

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
            if ( await LeaveTypeNameExistsAsync(leaveTypeVM.Name))
            {
                ModelState.AddModelError("LeaveType", $"Leavetype {leaveTypeVM.Name} already exists");
            }
            if (ModelState.IsValid)
            {
                LeaveType leaveType = mapper.Map<LeaveType>(leaveTypeVM);
                _context.Add(leaveType);
                await _context.SaveChangesAsync();
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

            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            LeaveTypeEditVM leaveTypeEditVM = mapper.Map<LeaveTypeEditVM>(leaveType);

            return View(leaveTypeEditVM);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Days")] LeaveTypeEditVM leaveTypeVM)
        {
            if (id != leaveTypeVM.Id)
            {
                return NotFound();
            }

            if (await LeaveTypeNameExistsForEditAsync(leaveTypeVM))
            {
                ModelState.AddModelError("LeaveType", $"Leavetype {leaveTypeVM.Name} already exists");
            }

            if (ModelState.IsValid)
            {
                LeaveType leaveType = null;
                try
                {
                    leaveType = mapper.Map<LeaveType>(leaveTypeVM);
                    _context.Update(leaveType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveTypeExists(leaveType.Id))
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
            return View(leaveTypeVM);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType != null)
            {
                _context.LeaveTypes.Remove(leaveType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.Id == id);
        }

        private async Task<bool> LeaveTypeNameExistsAsync(string leaveTypeName)
        {
            if (await _context.LeaveTypes.AnyAsync(leavetype => leavetype.Name.ToLower() == leaveTypeName.ToLower()))
            {
                return true;
            }
            return false;
        }

        private async Task<bool> LeaveTypeNameExistsForEditAsync(LeaveTypeEditVM leaveTypeEdit)
        {
            if (await _context.LeaveTypes.AnyAsync(leavetype => leavetype.Name.ToLower() == leaveTypeEdit.Name.ToLower() && leavetype.Id != leaveTypeEdit.Id))
            {
                return true;
            }
            return false;
        }
    }
}
