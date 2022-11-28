using System.ComponentModel.DataAnnotations;
using WebApiClient.DTOs;

namespace WebAppMVC.ViewModels
{
    public class ProductDetailsVM 
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

        public IEnumerable<ProductSizeStockDto> ProductSizeStocks { get; set; }
        public string PageTitle
        {
            get
            {
                return Name.Length <1 ? "No products in this category" : "Products page";
            }
            set { }
        }
    }
}
