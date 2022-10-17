using AutoMapper;
using Project.WebApi.Models.Domain;
using Project.WebApi.Models.DTO;

namespace Project.WebApi.Helper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionRequest>().ReverseMap();
            CreateMap<Region, UpdateRegionRequest>().ReverseMap();

            CreateMap<WalkDifficulty, WalkDifficultyDto>().ReverseMap();
            CreateMap<WalkDifficulty, UpdateWalkDifficultyRequest>().ReverseMap();
            CreateMap<WalkDifficulty, AddWalkDifficultyRequest>().ReverseMap();

            CreateMap<WalkDto, Walk>().ReverseMap();
            CreateMap<Walk, AddWalkRequest>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequest>().ReverseMap();
        }
    }
}
