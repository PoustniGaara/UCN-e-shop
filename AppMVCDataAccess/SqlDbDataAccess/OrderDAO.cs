using DataAccessLayer.Model;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Exceptions;

namespace DataAccessLayer.SqlDbDataAccess
{
    public class OrderDAO : IOrderDataAccess
    {
        private string connectionstring;
        private ILineItemDataAccess lineItemDAO;
        private IProductSizeStockDataAccess productSizeStockDAO;


        public OrderDAO(string connectionstring)
        { 
            this.connectionstring = connectionstring;
            lineItemDAO = new LineItemDAO(connectionstring);
            productSizeStockDAO = new ProductSizeStockDAO(connectionstring);
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            int id = -1;
            using SqlConnection connection = new SqlConnection(connectionstring);

            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);

            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            try
            {
                command.CommandText = "INSERT INTO dbo.[Order] (date, total, address, note, status, customer) VALUES " +
                            "(@date, @total, @address, @note, @status, @customer); SELECT CAST(scope_identity() AS int)";
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@total", order.TotalPrice);
                command.Parameters.AddWithValue("@address", order.Address);
                command.Parameters.AddWithValue("@note", (order.Note == null ? "" : order.Note));
                command.Parameters.AddWithValue("@status", Convert.ToInt32(order.Status));
                command.Parameters.AddWithValue("@customer", order.User.Email);
                id = (int)command.ExecuteScalar();
                order.Id = id;

                foreach(LineItem item in order.Items)
                {
                    await productSizeStockDAO.DecreaseStockWithCheck(command, item.Product.Id, item.SizeId, item.Quantity);
                    await lineItemDAO.CreateLineItemAsync(command, id, item);
                }
                transaction.Commit();
            }
            catch (ProductOutOfStockException outOfStockEx)
            {
                transaction.Rollback();
                throw new ProductOutOfStockException($"Error while creating products from DB '{outOfStockEx.Message}'.", outOfStockEx);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Error creating order: '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
            return id;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            
            try
            {
                command.CommandText = "DELETE FROM [Order] WHERE id = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                await lineItemDAO.DeleteOrderLineItemsAsync(id);

                transaction.Commit();
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            List<Order> orders = new List<Order>();
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("Select * from [Order]");
                selectCommand.Connection = connection;
                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User()
                    {
                        Email = reader.GetString("customer")
                    }; //UserDAO.GetByIdAsync(reader.GetString("customer"));
                    List<LineItem> items = (List<LineItem>) await lineItemDAO.GetOrderLineItems(reader.GetInt32("id"));
                    orders.Add(new Order(reader.GetInt32("id"), reader.GetDateTime("date"), reader.GetDecimal("total"), (Status)reader.GetInt32("status"), reader.GetString("address"), reader.GetString("note"), user, items));
                }
            }
            finally
            {
                connection.Close();
            }
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                string query = "SELECT * FROM [Order] WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                
                reader.Read();
                User user = new User()
                {
                    Email = reader.GetString("customer")
                }; //UserDAO.GetByIdAsync(reader.GetString("customer"));
                List<LineItem> items = (List<LineItem>)await lineItemDAO.GetOrderLineItems(reader.GetInt32("id"));
                return new Order(reader.GetInt32("id"), reader.GetDateTime("date"), reader.GetDecimal("total"), (Status)reader.GetInt32("status"), reader.GetString("address"), reader.GetString("note"), user, items);
            }
            finally
            {
                connection.Close();
            }
            return null;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(string email)
        {
            List<Order> orders = new List<Order>();
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from [Order] where customer = @email", connection);
                command.Parameters.AddWithValue("@email", email);
                //command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<LineItem> items = (List<LineItem>)await lineItemDAO.GetOrderLineItems(reader.GetInt32("id"));
                    orders.Add(new Order(reader.GetInt32("id"), reader.GetDateTime("date"), reader.GetDecimal("total"), (Status)reader.GetInt32("status"), reader.GetString("address"), reader.GetString("note"), new User { Email = email, }, items));
                }
            }
            finally
            {
                connection.Close();
            }
            return orders;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {

            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE [Order] SET date = @date, total = @total, address = @address, note = @note, orderStatus = @staus WHERE id = @id", connection);
                command.Parameters.AddWithValue("@date", order.Date);
                command.Parameters.AddWithValue("@total", order.TotalPrice);
                command.Parameters.AddWithValue("@address", order.Address);
                command.Parameters.AddWithValue("@note", order.Note);
                command.Parameters.AddWithValue("@status", (int)order.Status);
                command.Parameters.AddWithValue("@id", order.Id);
                int affected = command.ExecuteNonQuery();
                if (affected == 1)
                    return true;
                else
                    return false;
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
    }
}