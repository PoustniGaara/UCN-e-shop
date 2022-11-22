using AutoMapper;
using DataAccessLayer.Model;
using WebApi.DTOs;

namespace WebApi.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            //    .ForMember(dest => dest.ProductSizeStocks, act => act.MapFrom(src => src.ProductSizeStocks))
            //    .ReverseMap();

            //CreateMap<ProductDto, Product>()
            //    .ForMember(dest => dest.ProductSizeStocks, act => act.MapFrom(src => src.ProductSizeStocks))
            //    .ReverseMap();
        }
    }
}
