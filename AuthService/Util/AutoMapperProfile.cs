using AuthService.Entities;
using AuthService.Models;
using AutoMapper;

namespace AuthService.Util
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, LoginDto>();
            CreateMap<UserDto, User>();
            CreateMap<LoginDto, User>();
        }
    }
}