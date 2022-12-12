using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;

namespace WebApiClient.RestSharpClientImplementation
{
    public class UserClient : IUserClient
    {
        RestClient _client;
        public UserClient(string restUrl) => _client = new RestClient(restUrl);

        public async Task<string> CreateAsync(UserDto userDto)
        {
            var request = new RestRequest();
            request.AddBody(userDto);
            return await _client.PostAsync<string>(request);
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            
            var request = new RestRequest($"{email}");
            var user = await _client.GetAsync<UserDto?>(request);
            return user;
        }

        public async Task<bool> UpdatePasswordAsync(UserDto userDto)
        {
            try
            {
                var request = new RestRequest($"{userDto.Email}/Password");
                request.AddBody(userDto);
                await _client.PutAsync<bool>(request);
                return true;
                    
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(string email)
        {
            var request = new RestRequest($"{email}");
            return await _client.DeleteAsync<bool>(request);
        }
    }
}
