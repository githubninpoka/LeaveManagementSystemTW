using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystemTW.Security.Data.Configurations;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(
                    new ApplicationUser
                    {
                        Id = "2a81d8f9-f054-4acc-b024-d2663b8e64a7",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "ADMIN@LOCALHOST.COM",
                        NormalizedUserName = "ADMIN@LOCALHOST.COM",
                        UserName = "admin@localhost.com",
                        //PasswordHash= "AQAAAAIAAYagAAAAEHodb2tgnqTz/jgT6xtYgxSWDdz8uipG3FzohM9jydkKqhjpSpgym//5oF3tNq6BWQ==",
                        PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                        EmailConfirmed = true,
                        FirstName = "Ad",
                        LastName = "Min"
                    }

                    );

    }
}
