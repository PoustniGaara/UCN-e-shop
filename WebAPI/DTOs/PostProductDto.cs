using DataAccessLayer.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class PostProductDto
    {
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

        [Required(ErrorMessage = "Size is required")]
        public string Size { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }

    }
}