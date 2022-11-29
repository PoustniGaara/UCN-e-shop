using AutoMapper;
using WebAppMVC.ViewModels;

namespace WebAppMVC.ViewModelProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<IEnumerable<ProductDto>, ProductIndexVM>()
            //   .ForMember(dest => dest.Products, act => act.MapFrom(src => src))
            //   .ReverseMap();
        }
    }
}
