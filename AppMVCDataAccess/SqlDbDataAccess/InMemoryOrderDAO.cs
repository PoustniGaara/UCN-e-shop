using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.SqlDbDataAccess
{
    public class InMemoryOrderDAO : IOrderDataAccess
    {
        private int nextId = 0;
        private List<Order> orders;

        public InMemoryOrderDAO()
        {
            orders = new List<Order>();
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            orders.Add(order);
            int index = nextId;
            order.Id = nextId;
            nextId++;
            return index;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return orders.Remove(orders.Where(order => order.Id == id).First());
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return orders.Where(order => order.Id == id).First();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(User user)
        {
            return orders.Where(order => order.User == user).ToList();
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            int index = orders.IndexOf(order);
            orders[index] = order;
            return true;
        }
    }
}
