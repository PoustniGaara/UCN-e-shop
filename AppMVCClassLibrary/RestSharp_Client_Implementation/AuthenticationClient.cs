using RestSharp;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;

namespace WebApiClient.RestSharp_Client_Implementation
{
    public class AuthenticationClient : IAuthenticationClient
    {
        RestClient _client;
        public AuthenticationClient(string restUrl) => _client = new RestClient(restUrl);

        public async Task<string> LoginAsync(LoginModelDto loginModel)
        {
            var request = new RestRequest().AddBody(loginModel);
            var response = await _client.PostAsync(request);
            return response.Content;
        }
    }
}
