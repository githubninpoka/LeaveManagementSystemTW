using AutoMapper;
using LeaveManagementSystem.Domain.Models;
using LeaveManagementSystemTW.Security.Data;
using LeaveManagementSystemTW.Services.Models.LeaveAllocations;
using LeaveManagementSystemTW.Services.Models.LeaveTypes;
using LeaveManagementSystemTW.Services.Models.Periods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystemTW.Services.MappingProfiles;

public class LeaveAllocationAutomapperProfile : Profile
{
    public LeaveAllocationAutomapperProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationVM>()
            .ForMember(dest => dest.NumberOfDays, option => option
            .MapFrom(source => source.Days)); ;
        CreateMap<LeaveAllocation, LeaveAllocationEditVM>()
            .ForMember(dest => dest.NumberOfDays, option => option
            .MapFrom(source => source.Days));
        CreateMap<Period, PeriodVM>();
        CreateMap<ApplicationUser, EmployeeListVM>();

    }
}
