using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.SqlDbDataAccess
{
    public class LineItemDAO : ILineItemDataAccess
    {
        private string connectionstring;

        public LineItemDAO(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public async Task CreateLineItemAsync(int orderId, LineItem item)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.OrderLineItem ('order_id', 'product_id', 'amount') VALUES (@orderId, @productId, @amount)";
            command.Parameters.AddWithValue("@order_id", orderId);
            command.Parameters.AddWithValue("@product_id", item.ProductId);
            command.Parameters.AddWithValue("@amount", item.Quantity);
            command.ExecuteNonQuery();
        }

        public async Task<IEnumerable<LineItem>> GetOrderLineItems(int orderId)
        {
            List<LineItem> items = new List<LineItem>();

            using SqlConnection connection = new SqlConnection(connectionstring);
           
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM OrderLineItems WHERE OrderID = @orderId", connection);
            command.Parameters.AddWithValue("@orderId", orderId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new LineItem(reader.GetInt32("proudcutID"), reader.GetInt32("amount")));
            }
            return items;     
        }

        public Task<bool> UpdateLineItemAsync(LineItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLineItemAsync(LineItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrderLineItemsAsync(int orderID)
        {
            throw new NotImplementedException();
        }
    }
}
