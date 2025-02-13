using Microsoft.AspNetCore.Identity;

namespace LeaveManagementSystemTW.Security.Data;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}
