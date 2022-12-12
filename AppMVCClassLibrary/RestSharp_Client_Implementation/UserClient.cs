using RestSharp;
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
            var request = new RestRequest().AddBody(userDto);
            var response = await _client.ExecutePostAsync<string>(request);
            if (!response.IsSuccessStatusCode || response.Data == null)
            {
                throw new Exception($"Error creating user. Message was {response.ErrorMessage}");
            }
            return response.Data;
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {

            var request = new RestRequest($"{email}");
            var response = await _client.ExecuteGetAsync<UserDto>(request);
            if (!response.IsSuccessStatusCode || response.Data == null)
            {
                throw new Exception($"Error retrieving user by email:'{email}'. Message was {response.ErrorMessage}");
            }
            return response.Data;
        }

        public async Task UpdatePasswordAsync(UserDto userDto)
        {
            var request = new RestRequest($"{userDto.Email}/Password").AddBody(userDto);
            var response = await _client.ExecutePutAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error updating password. Message was {response.ErrorMessage}");
            }
            return;
        }

        public async Task DeleteAsync(string email)
        {
            //Try catch block is needed because DeleteAsync throws exception when request fails.
            var request = new RestRequest($"{email}");
            try
            {
                await _client.DeleteAsync(request);
                return;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting user. Message was {ex.Message}");
            }

        }
    }
}
