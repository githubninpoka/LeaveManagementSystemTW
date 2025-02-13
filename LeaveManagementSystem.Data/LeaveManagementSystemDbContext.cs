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
    public LeaveManagementSystemDbContext(DbContextOptions options) : base(options)
    {
    }
}
