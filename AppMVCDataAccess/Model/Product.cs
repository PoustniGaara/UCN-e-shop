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
        public ProductSize ProductSize { get; set; }
        public Category Category { get; set; }
    }
}