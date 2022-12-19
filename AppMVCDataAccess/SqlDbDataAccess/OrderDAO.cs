using DataAccessLayer.Model;
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
        private IUserDataAccess userDAO;

        public OrderDAO(string connectionstring, ILineItemDataAccess lineItemDAO, IProductSizeStockDataAccess productSizeStockDAO, IUserDataAccess userDAO)
        { 
            this.connectionstring = connectionstring;
            this.lineItemDAO = lineItemDAO;
            this.productSizeStockDAO = productSizeStockDAO;
            this.userDAO = userDAO;
        }

        public async Task<int> CreateAsync(Order order)
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
                command.Parameters.AddWithValue("@status", order.Status);
                command.Parameters.AddWithValue("@customer", order.UserEmail);
                id = (int)command.ExecuteScalar();
                order.Id = id;

                foreach(LineItem item in order.Items)
                {
                    await productSizeStockDAO.DecreaseStockWithCheckAsync(command, item.Product.Id, item.SizeId, item.Quantity);
                    await lineItemDAO.CreateAsync(command, id, item);
                }
                transaction.Commit();
            }
            catch (ProductOutOfStockException outOfStockEx)
            {
                transaction.Rollback();
                throw new ProductOutOfStockException($"The product's stock is less then desired! Error: '{outOfStockEx.Message}'.", outOfStockEx);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"An error occured while creating a new order: '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
            return id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            SqlCommand command = connection.CreateCommand();            
            try
            {
                command.CommandText = "DELETE FROM [Order] WHERE id = @id";
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured deleting an order: '{ex.Message}'.", ex);
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
                    List<LineItem> items = (List<LineItem>) await lineItemDAO.GetByOrderIdAsync(reader.GetInt32("id"));
                    orders.Add(new Order(reader.GetInt32("id"), reader.GetDateTime("date"), reader.GetDecimal("total"), (Status)reader.GetInt32("status"), reader.GetString("address"), reader.GetString("note"), reader.GetString("customer"), items));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while retrieving orders from database: '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
            return orders;
        }

        public async Task<Order> GetByIdAsync(int id)
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

                List<LineItem> items = (List<LineItem>)await lineItemDAO.GetByOrderIdAsync(reader.GetInt32("id"));
                return new Order(reader.GetInt32("id"), reader.GetDateTime("date"), reader.GetDecimal("total"), (Status)reader.GetInt32("status"), reader.GetString("address"), reader.GetString("note"), reader.GetString("customer"), items);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while retrieving an order from database: '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<IEnumerable<Order>> GetByUserAsync(string email)
        {
            List<Order> orders = new List<Order>();
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from [Order] where customer = @email", connection);
                command.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<LineItem> items = (List<LineItem>)await lineItemDAO.GetByOrderIdAsync(reader.GetInt32("id"));
                    orders.Add(new Order(reader.GetInt32("id"), reader.GetDateTime("date"), reader.GetDecimal("total"), (Status)reader.GetInt32("status"), reader.GetString("address"), reader.GetString("note"), email, items));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while retrieving orders of a user from database: '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
            return orders;
        }
    }
}