using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTOs;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;

namespace WebApiClient.RestSharpClientImplementation
{
    public class UserClient : IUserClient
    {
        RestClient _client;
        public UserClient(string restUrl) => _client = new RestClient(restUrl);

        public async Task<string> CreateUserAsync(UserDto userDto)
        {
            var request = new RestRequest();
            request.AddBody(userDto);
            return await _client.PostAsync<string>(request);
        }

        public async Task<IEnumerable<UserDto>?> GetAllAsync()
        {
            return await _client.GetAsync<IEnumerable<UserDto>>(new RestRequest());
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var request = new RestRequest($"{email}");
            return await _client.GetAsync<UserDto?>(request);
        }

        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            var request = new RestRequest($"{userDto.Email}");
            request.AddBody(userDto);
            return await _client.PutAsync<bool>(request);
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            var request = new RestRequest($"{email}");
            return await _client.DeleteAsync<bool>(request);
        }
    }
}
