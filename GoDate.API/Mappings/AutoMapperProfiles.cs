using AutoMapper;
using GoDate.API.Entities.Domain;
using GoDate.API.Entities.DTO;
using GoDate.API.Entities.DTO.Auth;
using GoDate.API.Extensions;

namespace GoDate.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MemberDto, User>().ReverseMap()
                .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))                                
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url))
                // Customizes PhotoUrl property on the destination
                .ForMember(d => d.Photos, o => o.MapFrom(s => s.Photos));   // PhotoDto list is mapped

            CreateMap<PhotoDto, Photo>().ReverseMap();
            CreateMap<MemberUpdateDto, User>();
            CreateMap<RegisterDto, User>();
            CreateMap<string, DateOnly>().ConvertUsing(s => DateOnly.Parse(s));    // Convert registerDto string date into DateOnly type
        }
    }
}
