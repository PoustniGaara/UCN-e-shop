using RestSharp;
using WebApiClient.DTOs;
using WebApiClient.Exceptions;
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
            var  response = await _client.PostAsync(request);
                if (response.StatusCode.Equals(403))
                {
                    throw new WrongLoginException($"Incorect login data={loginModel}. Message was {response.Content}");
                }
            
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error while loggin in={loginModel}. Message was {response.Content}");
            }
            return response.Content;
        }
    }
}
