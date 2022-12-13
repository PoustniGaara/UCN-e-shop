namespace WebAppMVC.ViewModels
{
    public class LineItemVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int Quantity { get; set; }
    }
}
