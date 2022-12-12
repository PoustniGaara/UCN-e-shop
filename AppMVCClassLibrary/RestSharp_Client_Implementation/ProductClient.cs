using RestSharp;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;

namespace WebApiClient.RestSharpClientImplementation
{
    public class ProductClient : IProductClient
    {
        RestClient _client;
        public ProductClient(string restUrl) => _client = new RestClient(restUrl);

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var request = new RestRequest($"{id}");
            var response = await _client.ExecuteGetAsync<ProductDto>(request);
            if (!response.IsSuccessStatusCode || response.Data == null)
            {
                throw new Exception($"Error retrieving product by id. Message was {response.ErrorMessage}");
            }
            return response.Data;
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync(string? category)
        {
            var request = new RestRequest().AddQueryParameter("category", category);
            var response = await _client.ExecuteGetAsync<IEnumerable<ProductDto>>(request);
            if (!response.IsSuccessStatusCode || response.Data == null)
            {
                throw new Exception($"Error retrieving all products. Message was {response.ErrorMessage}");
            }
            return response.Data;
        }
    }
}
