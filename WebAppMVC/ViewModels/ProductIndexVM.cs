using WebApiClient.DTOs;

namespace WebAppMVC.ViewModels
{
    public class ProductIndexVM 
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
        public string PageTitle { get
            {
                return "Products";
            }            
            set { } 
        }


    }
}
