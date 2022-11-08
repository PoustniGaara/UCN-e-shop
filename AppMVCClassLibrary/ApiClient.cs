using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient
{
    public class ApiClient : IApiClient
    {

        private RestClient _restClient;
        public ApiClient(string uri) => _restClient = new RestClient(new Uri(uri));

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var response = await _restClient.RequestAsync<IEnumerable<ProductDto>>(Method.Get, $"product");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all products. Message was {response.Content}");
            }
            return response.Data;
        }

        public Task<ProductDto> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
