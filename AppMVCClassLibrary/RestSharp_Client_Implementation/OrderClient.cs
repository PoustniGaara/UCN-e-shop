using RestSharp;
using System.Net;
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
                throw new ProductOutOfStockException($"Creating of order was unsuccessful. One of products was out of stock");
            }
            else if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Creating of order was unsuccessful. Message was'{response.ErrorMessage}'");
            }
            return response.Data;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var response = await _client.ExecuteGetAsync<IEnumerable<OrderDto>>(new RestRequest());
            if (!response.IsSuccessStatusCode || response.Data == null)
            {
                throw new Exception($"Error retrieving all orders. Message was {response.ErrorMessage}");

            }
            return response.Data;
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var request = new RestRequest($"{id}");
            var response = await _client.ExecuteGetAsync<OrderDto>(request);
            if (!response.IsSuccessStatusCode || response.Data == null)
            {
                throw new Exception($"Error retrieving all orders. Message was {response.ErrorMessage}");
            }
            return response.Data;
        }
    }
}
