namespace WebApiClient.DTOs
{
    public class LineItemDto
    { 
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int total { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }  

    }
}
