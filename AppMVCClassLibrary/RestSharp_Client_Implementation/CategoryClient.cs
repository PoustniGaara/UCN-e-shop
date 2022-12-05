using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;

namespace WebApiClient.RestSharp_Client_Implementation
{
    public class CategoryClient : ICategoryClient
    {
        RestClient _client;
        public CategoryClient(string restUrl) => _client = new RestClient(restUrl);

        public async Task<IEnumerable<CategoryDto>?> GetAllAsync()
        {

            return await _client.GetAsync<IEnumerable<CategoryDto>>(new RestRequest());
            //var response = await _client.ExecuteGetAsync<IEnumerable<ProductDto>>(new RestRequest());
            //if (!response.IsSuccessful)
            //{
            //    throw new Exception($"Error in retrieving all products. Message was {response.Content}");
            //}
            //return response.Data;
        }
    }
}
