using WebApiClient.DTOs;

namespace WebAppMVC.ViewModels
{
    public class ProductIndexVM
    {
        public IEnumerable<ProductDto> Products { get; set; } 
        public string PageTitle { get; set; }

        public ProductIndexVM(IEnumerable<ProductDto> products)
        {
            Products = products;
        }
    }
}
