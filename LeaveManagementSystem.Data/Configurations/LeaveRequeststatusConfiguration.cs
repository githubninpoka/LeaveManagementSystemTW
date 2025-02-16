using LeaveManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Data.Configurations;

internal class LeaveRequeststatusConfiguration : IEntityTypeConfiguration<LeaveRequestStatus>
{
    public void Configure(EntityTypeBuilder<LeaveRequestStatus> builder)
    {
        builder.HasData(
            new LeaveRequestStatus { Id = 1, Name = "Pending"},
            new LeaveRequestStatus { Id = 2, Name = "Approved"},
            new LeaveRequestStatus { Id = 3, Name = "Declined"},
            new LeaveRequestStatus { Id = 4, Name = "Canceled"});
    }
}
