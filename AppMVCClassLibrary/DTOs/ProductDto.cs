using System.ComponentModel;

namespace WebApiClient.DTOs
{
    public class ProductDto
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        int Stock { get; set; }
        SupplierDto Supplier { get; set; }
        CategoryDto Category { get; set; }

        public ProductDto(string name, string description, decimal price, int stock, SupplierDto supplier, CategoryDto category)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Supplier = supplier;
            Category = category;
        }

        public ProductDto(int id, string name, string description, decimal price, int stock, SupplierDto supplier, CategoryDto category)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Supplier = supplier;
            Category = category;

        }
    }
}