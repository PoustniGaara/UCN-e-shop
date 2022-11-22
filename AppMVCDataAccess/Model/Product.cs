using DataAccessLayer.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<ProductSizeStock> ProductSizeStocks { get; set; }
        public string Category { get; set; }

        public Product(int id, string name, string description, decimal price, IEnumerable<ProductSizeStock> productSizeStocks, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ProductSizeStocks = productSizeStocks;
            Category = category;
        }

        public Product() { }
    }
}