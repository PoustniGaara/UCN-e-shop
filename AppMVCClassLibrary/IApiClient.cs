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
    public interface IApiClient
    {
        Task<GetProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<GetProductDto>> GetAllProductsAsync();


        Task<int> CreateOrderAsync(OrderDto entity);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderAsync(OrderDto entity);
        Task<bool> DeleteOrderAsync(int id);
    }
    
}
