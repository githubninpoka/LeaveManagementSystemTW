using LeaveManagementSystem.Data;
using LeaveManagementSystemTW.Security.Data;
using LeaveManagementSystemTW.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;
using SecurityDbContext = LeaveManagementSystemTW.Security.Data.SecurityDbContext;

namespace LeaveManagementSystemTW.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var securityConnectionString = builder.Configuration.GetConnectionString("SecurityConnection") ?? throw new InvalidOperationException("Connection string 'SecurityConnection' not found.");
            builder.Services.AddDbContext<SecurityDbContext>(options =>
                {
                    options.UseSqlServer(securityConnectionString);
                    options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
                }
                );
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<Security.Data.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SecurityDbContext>();

            builder.Services.AddDbContext<LeaveManagementSystemDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DataConnection"))
            );

            //builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // because I moved the automapper profile to the servicelayer ->
            builder.Services.AddAutoMapper(Assembly.Load("LeaveManagementSystemTW.Services"));

            builder.Services.AddScoped<ILeaveTypesService, LeaveTypesService>();
            builder.Services.AddScoped<ILeaveAllocationsService, LeaveAllocationsService>();
            builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
