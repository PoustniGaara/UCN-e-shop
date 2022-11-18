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
                .ForMember(dest => dest.Size, act => act.MapFrom(src => src.ProductSize.Size))
                .ForMember(dest => dest.Stock, act => act.MapFrom(src => src.ProductSize.Stock))
                .ForMember(dest => dest.Category, act => act.MapFrom(src => src.Category.Name))
                .ReverseMap();

            ////PostProductDto to Product mapper
            //CreateMap<Product, PostProductDto>()
            //    .ForMember(dest => dest.Size, act => act.MapFrom(src => src.ProductSize.Size))
            //    .ForMember(dest => dest.Stock, act => act.MapFrom(src => src.ProductSize.Stock))
            //    .ForMember(dest => dest.Category, act => act.MapFrom(src => src.Category.Name))
            //    .ReverseMap();

            CreateMap<ProductDto, Product>()
                .ForPath(dest => dest.ProductSize.Size, act => act.MapFrom(src => src.Size))
                .ForPath(dest => dest.ProductSize.Stock, act => act.MapFrom(src => src.Stock))
                .ForPath(dest => dest.Category.Name, act => act.MapFrom(src => src.Category))
                .ReverseMap();
        }
    }
}
