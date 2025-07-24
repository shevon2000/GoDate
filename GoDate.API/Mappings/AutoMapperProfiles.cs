using AutoMapper;
using GoDate.API.Entities.Domain;
using GoDate.API.Entities.DTO;
using GoDate.API.Extensions;

namespace GoDate.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserDto, User>().ReverseMap()
                .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))                                
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));
                // Customizes PhotoUrl property on the destination

            CreateMap<PhotoDto, Photo>().ReverseMap();
        }
    }
}
