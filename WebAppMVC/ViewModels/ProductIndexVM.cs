
namespace WebAppMVC.ViewModels
{
    public class ProductIndexVM 
    {
        public IEnumerable<ProductDetailsVM> Products { get; set; }
        public IEnumerable<CategoryVM> Categories { get; set; }
        public string PageTitle { get
            {
                return "Products";
            }            
            set { } 
        }


    }
}
