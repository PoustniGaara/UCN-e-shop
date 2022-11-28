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
        Task<UserDto> GetByEmailAsync(string email);
        Task<string> CreateAsync(UserDto User);
        Task<bool> DeleteAsync(string email);
        Task<bool> UpdateAsync(UserDto user);

    }
}
