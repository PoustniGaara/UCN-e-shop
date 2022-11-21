using AutoMapper;
using DataAccessLayer.Model;
using WebApi.DTOs;

namespace WebApi.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //GetProductDto to Product mapper
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductSizeStocks, act => act.MapFrom(src => src.ProductSizeStocks))
                .ForMember(dest => dest.Category, act => act.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.ProductSizeStocks, act => act.MapFrom(src => src.ProductSizeStocks))
                .ForPath(dest => dest.Category.Name, act => act.MapFrom(src => src.Category))
                .ReverseMap();
        }
    }
}
