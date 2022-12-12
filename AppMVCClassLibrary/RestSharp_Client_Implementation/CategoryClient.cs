using RestSharp;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;

namespace WebApiClient.RestSharp_Client_Implementation
{
    public class CategoryClient : ICategoryClient
    {
        RestClient _client;
        public CategoryClient(string restUrl) => _client = new RestClient(restUrl);

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var response = await _client.ExecuteGetAsync<IEnumerable<CategoryDto>>(new RestRequest());
            if (!response.IsSuccessStatusCode || response.Data == null)
            {
                throw new Exception($"Error retrieving all categories. Message was {response.ErrorMessage}");
            }
            return response.Data;
        }
    }
}
