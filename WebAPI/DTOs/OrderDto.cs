using System.ComponentModel.DataAnnotations;
using WebApi.Interfaces;

namespace WebApi.DTOs
{
    public class OrderDto : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }
        public string? Note { get; set; }
        [Required(ErrorMessage = "E-mail is required!")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "Items are required!")]
        public IEnumerable<LineItemDto> Items { get; set; } 

    }
}

