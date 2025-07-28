using AutoMapper;
using Maps.src.Maps.Core.Models;
using Maps.src.Maps.Application.DTOs;

namespace Maps.src.Maps.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
