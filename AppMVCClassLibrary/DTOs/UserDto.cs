
using System.ComponentModel.DataAnnotations;

namespace WebApiClient.DTOs
{
    public class UserDto
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<OrderDto> Orders = new List<OrderDto>();
    }
}
