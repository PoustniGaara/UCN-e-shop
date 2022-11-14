using WebApiClient.DTOs;

namespace WebAppMVC.ViewModels
{
    public class ProductIndexVM
    {
        public IEnumerable<GetProductDto> Products { get; set; } 
        public string PageTitle { get; set; }

        //public ProductIndexVM(IEnumerable<ProductDto> products)
        //{
        //    Products = products;
        //}
    }
}
