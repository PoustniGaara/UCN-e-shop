using System.ComponentModel;

namespace WebApiClient.DTOs
{
    public class Product
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        int Stock { get; set; }
        Supplier Supplier { get; set; }
        Category Category { get; set; }

        public Product(string name, string description, decimal price, int stock, Supplier supplier, Category category)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Supplier = supplier;
            Category = category;
        }

        public Product(int id, string name, string description, decimal price, int stock, Supplier supplier, Category category)
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