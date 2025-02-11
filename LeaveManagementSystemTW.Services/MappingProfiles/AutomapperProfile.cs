using AutoMapper;
using LeaveManagementSystem.Domain.Models;
using LeaveManagementSystemTW.Services.Models.LeaveTypes;

namespace LeaveManagementSystemTW.Services.MappingProfiles;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeReadOnlyVM>()
            .ForMember(dest => dest.Days, option => option
                .MapFrom(source => source.NumberOfDays))
            .ReverseMap();

        CreateMap<LeaveTypeCreateVM, LeaveType>()
            .ForMember(dest => dest.NumberOfDays, option => option
            .MapFrom(source => source.Days));

        CreateMap<LeaveTypeEditVM, LeaveType>()
            .ForMember(dest => dest.NumberOfDays, option => option
            .MapFrom(source => source.Days))
            .ReverseMap();
    }
}
