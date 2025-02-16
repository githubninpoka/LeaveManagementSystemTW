using AutoMapper;
using LeaveManagementSystem.Domain.Models;
using LeaveManagementSystemTW.Services.Models.LeaveRequests;

namespace LeaveManagementSystemTW.Services.MappingProfiles;

public class LeaveRequestAutomapperProfile : Profile
{
    public LeaveRequestAutomapperProfile()
    {
        CreateMap<LeaveRequestCreateVM, LeaveRequest>();

    }
}