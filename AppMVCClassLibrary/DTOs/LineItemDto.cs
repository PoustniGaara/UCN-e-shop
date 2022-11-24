namespace WebApiClient.DTOs
{
    public class LineItemDto
    { 
        // post
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }

        // get
        //public int ProductId { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public decimal Price { get; set; }
        //public int SizeId { get; set; }
        //public int Quantity { get; set; }
    }
}
