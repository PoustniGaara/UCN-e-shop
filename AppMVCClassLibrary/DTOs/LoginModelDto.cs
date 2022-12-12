using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApiClient.Interfaces;

namespace WebApiClient.DTOs
{
    public class LoginModelDto
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
