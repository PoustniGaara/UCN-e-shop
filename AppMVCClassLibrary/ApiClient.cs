using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTOs;
using WebApiClient.DTOs;

namespace WebApiClient
{
    public class ApiClient : IApiClient
    {

        private RestClient _restClient;
        public ApiClient(string uri) => _restClient = new RestClient(new Uri(uri));

        public async Task<IEnumerable<GetProductDto>> GetAllProductsAsync()
        {
            var response = await _restClient.RequestAsync<IEnumerable<GetProductDto>>(Method.Get, $"products");
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all products. Message was {response.Content}");
            }

            return response.Data;
        }

        public Task<GetProductDto> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateOrderAsync(OrderDto entity)
        {
            var response = await _restClient.RequestAsync<int>(Method.Post, "orders", entity);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating Order with id={entity.Id}. Message was {response.Content}");
            }
            return response.Data;
        }
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var response = await _restClient.RequestAsync<IEnumerable<OrderDto>>(Method.Get, $"orders");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all orders. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var response = await _restClient.RequestAsync<OrderDto>(Method.Get, $"order/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all orders. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<bool> UpdateOrderAsync(OrderDto entity)
        {
            var response = await _restClient.RequestAsync(Method.Put, $"orders/{entity.Id}", entity);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error updating order with id={entity.Id}. Message was {response.Content}");
            }

        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var response = await _restClient.RequestAsync(Method.Delete, $"orders/{id}", null);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error deleting order with id={id}. Message was {response.Content}");
            }
        }


    }
}
