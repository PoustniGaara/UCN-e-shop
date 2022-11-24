using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;

namespace WebApiClient.RestSharpClientImplementation
{
    public class OrderClient : IOrderClient
    {
        RestClient _client;
        public OrderClient(string restUrl) => _client = new RestClient(restUrl);

        public async Task<int> CreateOrderAsync(OrderDto orderDto)
        {
            var request = new RestRequest();
            request.AddBody(orderDto);
            return  await _client.PostAsync<int>(request);
        }
       
        public async Task<IEnumerable<OrderDto>?> GetAllOrdersAsync()
        {
            return await _client.GetAsync<IEnumerable<OrderDto>>(new RestRequest());
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var request = new RestRequest($"{id}");
            return await _client.GetAsync<OrderDto?>(request);
        }

        public async Task<bool> UpdateOrderAsync(OrderDto orderDto)
        {
            var request = new RestRequest($"{orderDto.Id}");
            request.AddBody(orderDto);
            return await _client.PutAsync<bool>(request);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var request = new RestRequest($"{id}");
            return await _client.DeleteAsync<bool>(request);
        }
    }
}
