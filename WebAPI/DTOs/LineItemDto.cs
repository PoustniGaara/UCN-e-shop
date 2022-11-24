namespace WebApi.DTOs
{
    public class LineItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //public string Description { get; set; }
        public decimal Price { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
