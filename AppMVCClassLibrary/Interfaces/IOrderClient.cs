using WebApiClient.DTOs;

namespace WebApiClient.Interfaces
{
    public interface IOrderClient
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<int> CreateAsync(OrderDto order);
    }
}
