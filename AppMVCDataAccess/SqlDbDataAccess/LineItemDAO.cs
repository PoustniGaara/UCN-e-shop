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
            command.CommandText = "INSERT INTO dbo.OrderLineItem ('order_id', 'product_id', 'size_id', 'amount') VALUES (@orderId, @productId, @sizeId, @amount)";
            command.Parameters.AddWithValue("@order_id", orderId);
            command.Parameters.AddWithValue("@product_id", item.ProductId);
            command.Parameters.AddWithValue("@sizeId", item.SizeId);
            command.Parameters.AddWithValue("@amount", item.Quantity);
            command.ExecuteNonQuery();
        }

        public async Task<IEnumerable<LineItem>> GetOrderLineItems(int orderId)
        {
            List<LineItem> items = new List<LineItem>();

            using SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM OrderLineItems WHERE order_id = @orderId", connection);
            command.Parameters.AddWithValue("@orderId", orderId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new LineItem(reader.GetInt32("product_id"), reader.GetInt32("size_id"), reader.GetInt32("amount")));
            }
            return items;     
        }

        public Task<bool> UpdateLineItemAsync(LineItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteLineItemAsync(int orderId, LineItem item)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM OrderLineItems WHERE order_id = @order_id, product_id = @product_id, size_id = @size_id", connection);
                command.Parameters.AddWithValue("@order_id", orderId);
                command.Parameters.AddWithValue("@product_id", item.ProductId);
                command.Parameters.AddWithValue("@sizeId", item.SizeId); 
                int affected = command.ExecuteNonQuery();
                if (affected == 1)
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while deleting a LineItem: " + ex);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }

        public async Task<bool> DeleteOrderLineItemsAsync(int orderId)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM OrderLineItems WHERE order_id = @order_id", connection);
                command.Parameters.AddWithValue("@order_id", orderId);
                int affected = command.ExecuteNonQuery();
                if (affected > 0)
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while deleting a LineItem: " + ex);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
    }
}
