using AutoMapper;
using WebApiClient.DTOs;
using WebAppMVC.ViewModels;

namespace WebAppMVC.ViewModelProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductSizeStockVM, ProductSizeStockDto>()
                .ReverseMap();

            CreateMap<ProductDto, ProductDetailsVM>()
                .ReverseMap();

            CreateMap<IEnumerable<ProductDetailsVM>, ProductIndexVM>()
                .ForMember(dest => dest.Products, act => act.MapFrom(src => src));

            CreateMap<IEnumerable<ProductDto>, IEnumerable<ProductDetailsVM>>();

            CreateMap<IEnumerable<ProductDto>, ProductIndexVM>()
                .ForMember(dest => dest.Products, act => act.MapFrom(src => src));
        }
    }
}
