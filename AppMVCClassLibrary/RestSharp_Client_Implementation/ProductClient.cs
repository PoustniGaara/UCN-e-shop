using RestSharp;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;

namespace WebApiClient.RestSharpClientImplementation
{
    public class ProductClient : IProductClient
    {
        RestClient _client;
        public ProductClient(string restUrl) => _client = new RestClient(restUrl);

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var request = new RestRequest($"{id}");
            return await _client.GetAsync<ProductDto?>(request);
        }

        public async Task<IEnumerable<ProductDto>?> GetAllAsync(string? category)
        {
            var request = new RestRequest().AddQueryParameter("category", category);
            return await _client.GetAsync<IEnumerable<ProductDto>>(request);
        }

        public async Task<int> CreateAsync(ProductDto productDto)
        {
            var request = new RestRequest();
            request.AddBody(productDto);
            return await _client.PostAsync<int>(request);
        }

        public async Task<bool> UpdateAsync(ProductDto productDto)
        {
            var request = new RestRequest($"{productDto.Id}");
            request.AddBody(productDto);
            return await _client.PutAsync<bool>(request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var request = new RestRequest($"{id}");
            return await _client.DeleteAsync<bool>(request);
        }
    }
}
