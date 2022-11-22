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

        public async Task<int> CreateOrderAsync(OrderDto entity)
        {
            var response = await _client.RequestAsync<int>(Method.Post, "orders", entity);
            if (!response.IsSuccessful)
                throw new Exception($"Error creating Order with id={entity.Id}. Message was {response.Content}");
            return response.Data;
        }
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var response = await _client.RequestAsync<IEnumerable<OrderDto>>(Method.Get, "orders");
            if (!response.IsSuccessful)
                throw new Exception($"Error retrieving all orders. Message was {response.Content}");
            return response.Data;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var response = await _client.RequestAsync<OrderDto>(Method.Get, $"order/{id}");
            if (!response.IsSuccessful)
                throw new Exception($"Error retrieving all orders. Message was {response.Content}");
            return response.Data;
        }

        public async Task<bool> UpdateOrderAsync(OrderDto entity)
        {
            var response = await _client.RequestAsync(Method.Put, $"orders/{entity.Id}", entity);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                throw new Exception($"Error updating order with id={entity.Id}. Message was {response.Content}");
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var response = await _client.RequestAsync(Method.Delete, $"orders/{id}");
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                throw new Exception($"Error deleting order with id={id}. Message was {response.Content}");
        }

    }
}
