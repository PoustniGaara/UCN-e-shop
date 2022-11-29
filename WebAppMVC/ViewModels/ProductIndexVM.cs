using WebApiClient.DTOs;

namespace WebAppMVC.ViewModels
{
    public class ProductIndexVM 
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public string PageTitle { get
            {
                return Products.Count() > 0 ? "No products in this category" : "Products page";
            }            
            set { } 
        }


    }
}
