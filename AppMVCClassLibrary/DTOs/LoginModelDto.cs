using System.Threading.Tasks;
using WebApiClient.Interfaces;

namespace WebApiClient.DTOs
{
    public class LoginModelDto : IEntity
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
