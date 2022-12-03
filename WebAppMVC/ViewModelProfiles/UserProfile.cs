using AutoMapper;
using WebApiClient.DTOs;
using WebAppMVC.ViewModels;

namespace WebAppMVC.ViewModelProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, UserDetailsVM>();

            CreateMap<UserDto, UserEditVM>()
                .ReverseMap();
        }
    }
}
