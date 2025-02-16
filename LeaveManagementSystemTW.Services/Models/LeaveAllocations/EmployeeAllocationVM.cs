using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystemTW.Services.Models.LeaveAllocations;

public class EmployeeAllocationVM : EmployeeListVM
{
    [Display(Name = "Date of Birth")]
    [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateOnly? DateOfBirth { get; set; }

    public bool IsAllocationCompleted { get; set; }

    public List<LeaveAllocationVM> LeaveAllocations { get; set; }
}
