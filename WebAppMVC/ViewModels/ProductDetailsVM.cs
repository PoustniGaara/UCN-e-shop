
namespace WebAppMVC.ViewModels
{
    public class ProductDetailsVM 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public IEnumerable<ProductSizeStockVM> ProductSizeStocks { get; set; } 
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
