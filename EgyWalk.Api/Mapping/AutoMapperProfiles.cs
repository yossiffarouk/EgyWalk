using AutoMapper;
using EgyWalk.Api.Dtos.RegionDtos;
using EgyWalk.Api.Dtos.WalkDtos;
using EgyWalk.Api.Models.Domain;

namespace EgyWalk.Api.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() { 

            CreateMap<Walk , AddWalkDto>().ReverseMap();
            CreateMap<Walk , ReadWalkDto>().ReverseMap();
            CreateMap<Region , ReadRegionDto>().ReverseMap();
            CreateMap<Region , AddRegionDto>().ReverseMap();
        }
    }
}
