using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient
{
    internal class ApiClient
    {

        private RestClient _restClient;
        public ApiClient(string uri) => _restClient = new RestClient(new Uri(uri));

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var response = await _restClient.RequestAsync<IEnumerable<ProductDto>>(Method.Get, $"blogposts");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all blogposts. Message was {response.Content}");
            }
            return response.Data;
        }
    }
}
