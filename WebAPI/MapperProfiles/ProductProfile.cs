using AutoMapper;
using DataAccessLayer.Model;
using WebApi.DTOs;

namespace WebApi.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
            //ProductSize is a Complex type, so Map ProductSize to Simple type using For Member
                .ForMember(dest => dest.Size, act => act.MapFrom(src => src.ProductSize.Size))
                .ForMember(dest => dest.Stock, act => act.MapFrom(src => src.ProductSize.Stock))
                .ForMember(dest => dest.Category, act => act.MapFrom(src => src.Category.Name))
                .ReverseMap();

        }
    }
}
