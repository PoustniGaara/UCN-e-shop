
namespace WebApiClient.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public IEnumerable<ProductSizeStockDto> ProductSizeStocks { get; set; }

    }
}