using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;
using WebApiClient.Exceptions;
using WebApiClient.Interfaces;

namespace WebApiClient.RestSharpClientImplementation
{
    public class OrderClient : IOrderClient
    {
        RestClient _client;
        public OrderClient(string restUrl) => _client = new RestClient(restUrl);

        public async Task<int> CreateAsync(OrderDto orderDto)
        {

                var request = new RestRequest().AddBody(orderDto);
                var response = await _client.ExecutePostAsync<int>(request);
                if (response.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    throw new ProductOutOfStockException($"Incorect data={orderDto}");
                }
                return  response.Data;
           


        }
       
        public async Task<IEnumerable<OrderDto>?> GetAllAsync()
        {
            return await _client.GetAsync<IEnumerable<OrderDto>>(new RestRequest());
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var request = new RestRequest($"{id}");
            return await _client.GetAsync<OrderDto?>(request);
        }

        public async Task<bool> UpdateAsync(OrderDto orderDto)
        {
            var request = new RestRequest($"{orderDto.Id}");
            request.AddBody(orderDto);
            return await _client.PutAsync<bool>(request);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var request = new RestRequest($"{id}");
            return await _client.DeleteAsync<bool>(request);
        }
    }
}
