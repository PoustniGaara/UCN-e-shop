using AutoMapper;
using WebApiClient.DTOs;
using WebAppMVC.ViewModels;

namespace WebAppMVC.ViewModelProfiles
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginVM, LoginModelDto>().
                ReverseMap();
        }
    }
}
