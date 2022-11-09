using AutoMapper;
using DataAccessLayer.Model;
using WebApi.DTOs;

namespace WebApi.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //ProductSize and Category are complex types so, map them to simple type using for member
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Size, act => act.MapFrom(src => src.ProductSize.Size))
                .ForMember(dest => dest.Stock, act => act.MapFrom(src => src.ProductSize.Stock))
                .ForMember(dest => dest.Category, act => act.MapFrom(src => src.Category.Name))
                .ReverseMap();
            //CreateMap<IEnumerable<Product>, IEnumerable<ProductDto>>()
            //    .ForMember(dest => dest, act => act.MapFrom(src => src));




        }
    }
}
