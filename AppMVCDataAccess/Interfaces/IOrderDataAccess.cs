using DataAccessLayer.Model;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderDataAccess
    {
        public Task<IEnumerable<Order>> GetAllAsync();
        public Task<Order> GetByIdAsync(int id);
        public Task<IEnumerable<Order>> GetByUserAsync(string email);
        public Task<int> CreateAsync(Order order);
        public Task<bool> DeleteAsync(int id);
    }
}