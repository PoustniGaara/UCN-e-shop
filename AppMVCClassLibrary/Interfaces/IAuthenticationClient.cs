using WebApiClient.DTOs;

namespace WebApiClient.Interfaces
{
    public interface IAuthenticationClient
    {
        public Task<String> LoginAsync(LoginModelDto loginModel);
    }
}
