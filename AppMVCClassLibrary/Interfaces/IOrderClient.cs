using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient.Interfaces
{
    public interface IOrderClient
    {
        Task<int> CreateOrderAsync(OrderDto entity);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderAsync(OrderDto entity);
        Task<bool> DeleteOrderAsync(int id);
    }
}
