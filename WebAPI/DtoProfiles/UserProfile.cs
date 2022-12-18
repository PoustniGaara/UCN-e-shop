using AutoMapper;
using DataAccessLayer.Model;
using WebApi.DTOs;

namespace WebApi.DtoProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<User, UserDto>()
               .ReverseMap();

            // order to orderDto mapping may be needed
        }
    }
}
