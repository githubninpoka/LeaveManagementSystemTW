using LeaveManagementSystemTW.Security.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystemTW.Services.Models.LeaveAllocations;

public class LeaveAllocationEditVM: LeaveAllocationVM
{
    public EmployeeListVM? Employee { get; set; }
}
