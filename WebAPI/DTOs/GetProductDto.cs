using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class GetProductDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Size { get; set; }
        public string Category { get; set; }

    }
}