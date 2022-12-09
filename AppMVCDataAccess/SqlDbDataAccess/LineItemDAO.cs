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
        private IProductDataAccess productDAO;

        public LineItemDAO(string connectionstring)
        {
            this.connectionstring = connectionstring;
            productDAO = new ProductDAO(connectionstring);
        }

        public async Task CreateLineItemAsync(SqlCommand command, int orderId, LineItem item)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO dbo.OrderLineItem (order_id, product_id, size_id, amount) VALUES (@order_id, @product_id, @size_id, @amount)";
                command.Parameters.AddWithValue("@order_id", orderId);
                command.Parameters.AddWithValue("@product_id", item.Product.Id);
                command.Parameters.AddWithValue("@size_id", item.SizeId);
                command.Parameters.AddWithValue("@amount", item.Quantity);
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception("An error occured while decreasing stock: " + ex);

            }

        }

        public async Task<IEnumerable<LineItem>> GetOrderLineItems(int orderId)
        {
            List<LineItem> items = new List<LineItem>();

            using SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM OrderLineItem WHERE order_id = @orderId", connection);
            command.Parameters.AddWithValue("@orderId", orderId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product product = await productDAO.GetByIdAsync(reader.GetInt32("product_id"));
                items.Add(new LineItem(product, reader.GetInt32("size_id"), reader.GetInt32("amount")));
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
                SqlCommand command = new SqlCommand("DELETE FROM OrderLineItem WHERE order_id = @order_id, product_id = @product_id, size_id = @size_id", connection);
                command.Parameters.AddWithValue("@order_id", orderId);
                command.Parameters.AddWithValue("@product_id", item.Product.Id);
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
                SqlCommand command = new SqlCommand("DELETE FROM OrderLineItem WHERE order_id = @order_id", connection);
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
