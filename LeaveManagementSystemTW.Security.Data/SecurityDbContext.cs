
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystemTW.Security.Data;

public class SecurityDbContext : IdentityDbContext<ApplicationUser>
{
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>()
            .HasData(
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

        var hasher = new PasswordHasher<ApplicationUser>();
        builder.Entity<ApplicationUser>()
            .HasData(
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
        builder.Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "dfe72870-e5ed-4235-a9b9-8103c3f0f0f3",
                    UserId = "2a81d8f9-f054-4acc-b024-d2663b8e64a7"
                }
            );
    }
}
