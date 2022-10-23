using DataAccessLayer.Model;
using WebApi.DTOs;

namespace WebApi.DTOs.Converters
{
    public static class DtoConverter
    {
        //This class serves as a converter for model to modelDto and vice versa. Methods are separated by regions by model names.

        #region Product conversion methods
        public static ProductDto ToDto(this Product ProductToConvert)
        {
            var ProductDto = new ProductDto();
            ProductToConvert.CopyPropertiesTo(ProductDto);
            return ProductDto;
        }

        public static Product FromDto(this ProductDto ProductDtoToConvert)
        {
            var Product = new Product();
            ProductDtoToConvert.CopyPropertiesTo(Product);
            return Product;
        }

        public static IEnumerable<ProductDto> ToDtos(this IEnumerable<Product> ProductsToConvert)
        {
            foreach (var Product in ProductsToConvert)
            {
                yield return Product.ToDto();
            }
        }

        public static IEnumerable<Product> FromDtos(this IEnumerable<ProductDto> ProductDtosToConvert)
        {
            foreach (var ProductDto in ProductDtosToConvert)
            {
                yield return ProductDto.FromDto();
            }
        }
        #endregion
    }
}
