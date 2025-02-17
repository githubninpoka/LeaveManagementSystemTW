﻿using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystemTW.Services.Models.LeaveRequests;

public class EmployeeLeaveRequestListVM
{
    [Display(Name = "Total Requests")]
    public int TotalRequests { get; set; }

    [Display(Name = "Approved Requests")]
    public int ApprovedRequests { get; set; }

    [Display(Name = "Pending Requests")]
    public int PendingRequests { get; set; }

    [Display(Name = "Rejected Requests")]
    public int DeclinedRequests { get; set; }

    public List<LeaveRequestReadonlyListVM> LeaveRequests { get; set; } = new();
}