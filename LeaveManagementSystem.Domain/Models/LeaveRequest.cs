using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Domain.Models;

public class LeaveRequest : BaseEntity
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }

    public LeaveRequestStatus? Status { get; set; }
    public int LeaveRequestStatusId { get; set; }

    public string EmployeeId { get; set; }

    public string? ReviewerId { get; set; }

    public string? RequestComments { get; set; }

}
