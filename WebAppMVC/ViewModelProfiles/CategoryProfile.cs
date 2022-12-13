using AutoMapper;
using WebApiClient.DTOs;
using WebAppMVC.ViewModels;

namespace WebAppMVC.ViewModelProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryVM, CategoryDto>()
                .ReverseMap();
        }
    }
}
