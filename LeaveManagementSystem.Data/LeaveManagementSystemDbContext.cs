using LeaveManagementSystem.Data.Configurations;
using LeaveManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Data;

public class LeaveManagementSystemDbContext : DbContext
{
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<Period> Periods { get; set; }

    public DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public LeaveManagementSystemDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new LeaveRequeststatusConfiguration());
    }
}
