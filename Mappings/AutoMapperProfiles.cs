using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            
            CreateMap<Region, RegionDto>()
                .ForMember(d => d.Time, opt => opt.MapFrom(s => s.Time.ToLocalTime()));
            CreateMap<RegionDto, Region>();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto,Walk>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto ,Walk>().ReverseMap();
        }
    }
}
