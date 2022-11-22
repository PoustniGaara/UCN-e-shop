using AutoMapper;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApi.DTOs;

namespace WebApi.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
            //.ForPath(dest => dest.ProductSizeStocks, act => act.MapFrom(src => src.ProductSizeStocks))
            .ReverseMap();

            //CreateMap<Product, ProductDto>();
            //CreateMap<IEnumerable<ProductSizeStock>, IEnumerable<ProductSizeStockDto>>()
            //    .ForMember(dest => dest.GetType().GetGenericArguments()[0].Name , act => act.MapFrom(src => src.GetType().GetGenericArguments()[0].Name));


            //CreateMap<ProductDto, Product>()
            //    .ForMember(dest => dest.ProductSizeStocks, act => act.MapFrom(src => src.ProductSizeStocks))
            //    .ForPath(dest => dest.Category.Name, act => act.MapFrom(src => src.Category))
            //    .ReverseMap();
        }
    }
}
