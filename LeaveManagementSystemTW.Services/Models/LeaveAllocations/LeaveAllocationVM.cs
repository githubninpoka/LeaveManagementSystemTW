using LeaveManagementSystemTW.Services.Models.LeaveTypes;
using LeaveManagementSystemTW.Services.Models.Periods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystemTW.Services.Models.LeaveAllocations;

public class LeaveAllocationVM
{
    public int Id { get; set; }
    [Display(Name = "Number of days")]
    public int NumberOfDays { get; set; }
    [Display(Name = "Allocation Period")]
    public PeriodVM Period { get; set; } = new PeriodVM();

    public LeaveTypeReadOnlyVM LeaveType { get; set; } = new LeaveTypeReadOnlyVM();
}
