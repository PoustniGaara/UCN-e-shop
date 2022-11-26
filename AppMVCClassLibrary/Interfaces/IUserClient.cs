using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace WebApiClient.Interfaces
{
    public interface IUserClient
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<string> CreateUserAsync(UserDto User);
        Task<bool> DeleteUserAsync(string email);
        Task<bool> UpdateUserAsync(UserDto user);

    }
}
