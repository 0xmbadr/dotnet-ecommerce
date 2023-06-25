using AutoMapper;
using Core.Dtos;
using Core.Entities;

namespace Core.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserRegisterDto, User>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.UserName.ToLower())
                );
        }
    }
}
