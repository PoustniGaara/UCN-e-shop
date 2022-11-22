using System.ComponentModel;

namespace WebApiClient.DTOs
{
    public class ProductSizeStock
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public int Stock { get; set; }

        public ProductSizeStock(int id, string size, int stock)
        {
            Id = id;
            Size = size;
            Stock = stock;
        }

    }
}