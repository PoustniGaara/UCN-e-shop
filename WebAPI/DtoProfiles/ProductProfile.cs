using AutoMapper;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;
using WebApi.DTOs;

namespace WebApi.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductSizeStock, ProductSizeStockDto>()
                .ReverseMap();

            CreateMap<Product, ProductDto>()
                .ReverseMap();

        }
    }
}
