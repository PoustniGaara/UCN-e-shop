using DataAccessLayer.Model;
using System.Data.SqlClient;

namespace DataAccessLayer.Interfaces
{
    public interface ILineItemDataAccess
    {
        public Task<IEnumerable<LineItem>> GetByOrderIdAsync(int orderId);
        public Task CreateAsync(SqlCommand command, int orderID, LineItem item);
        public Task<bool> DeleteAsync(int orderID, LineItem item);
        public Task<bool> DeleteByOrderIdAsync(int orderID);
    }
}