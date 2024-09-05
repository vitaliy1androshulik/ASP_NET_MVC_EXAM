using _03_SecondHomeWorkViewModel.Entities;
using AutoMapper;
using Core.Dtos;
using Data.Entities;

namespace Core.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<MercedesDto, Mercedes>().ReverseMap();
        }
    }
}
