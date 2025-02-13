using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Domain.Models;

public class LeaveAllocation : BaseEntity
{
    public int Days { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }


    public Period? Period { get; set; }
    public int PeriodId { get; set; }

    //public ApplicationUser Employee { get; set; }
    public string EmployeeId { get; set; }
}
