using System.ComponentModel;

namespace WebApi.DTOs
{
    public class ProductDto
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        int Stock { get; set; }
        string Category { get; set; }

    }
}