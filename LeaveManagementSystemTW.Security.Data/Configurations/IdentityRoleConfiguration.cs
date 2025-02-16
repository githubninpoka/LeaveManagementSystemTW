using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystemTW.Security.Data.Configurations;

internal class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
                new IdentityRole
                {
                    Id = "4a1a2b2a-0ceb-4a00-ae7c-e57e3c637090",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Id = "b15eeb74-0e1a-4270-8358-790eb8b1f5e3",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR"
                },
                new IdentityRole
                {
                    Id = "dfe72870-e5ed-4235-a9b9-8103c3f0f0f3",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
    }
}
