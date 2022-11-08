using DataAccessLayer.Model;

namespace DataAccessLayer
{
    public interface IOrderDataAccess
    {
        public Task<IEnumerable<Order>> GetAllAsync();
        public Task<Order> GetOrderByIdAsync(int id);
        public Task<Order> GetOrderByUserAsync(User user);
        public Task<int> CreateOrderAsync(Order order);
        public Task<bool> DeleteOrderAsync(int id);
        public Task<bool> UpdateOrderAsync(Order order);
    }
}