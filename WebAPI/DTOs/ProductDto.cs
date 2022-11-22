using DataAccessLayer.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class ProductDto : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name length can't be more than 20.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(20, ErrorMessage = "Name length can't be more than 20.")]
        public string Category { get; set; }

        public IEnumerable<ProductSizeStock> ProductSizeStocks { get; set; }

    }
}