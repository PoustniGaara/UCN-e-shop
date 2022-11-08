using DataAccessLayer.Model;

namespace DataAccessLayer.SqlDbDataAccess
{
    public class OrderDataAccessDAO : IOrderDataAccess
    {
        private string connectionstring;

        public OrderDataAccessDAO(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public Task<int> CreateOrderAsync(Order order)
        {
            try
            {
                // SQL statement
            }
            catch (Exception e)
            {
                throw new Exception("message", e);
            }
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}