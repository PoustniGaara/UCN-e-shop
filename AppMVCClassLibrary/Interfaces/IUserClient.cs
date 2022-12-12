using WebApiClient.DTOs;

namespace WebApiClient.Interfaces
{
    public interface IUserClient
    {
        Task<UserDto> GetByEmailAsync(string email);
        Task<string> CreateAsync(UserDto User);
        Task DeleteAsync(string email);
        Task UpdatePasswordAsync(UserDto user);
        Task UpdateAsync(UserDto user);

    }
}
