using AutoMapper;
using DataAccessLayer.Model;
using WebApi.DTOs;

namespace WebApi.DtoProfiles
{
    public class ProductSizeStockProfile : Profile
    {
        public ProductSizeStockProfile()
        {
            CreateMap<ProductSizeStockDto, ProductSizeStock>();

        }

    }
}
