using Microsoft.AspNetCore.Mvc;
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
        }
    }
}
