using DataAccessLayer.Model;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer.Interfaces;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Authentication;
using System.Text.RegularExpressions;

namespace DataAccessLayer.SqlDbDataAccess
{
    public class UserDAO : IUserDataAccess
    {
        private string connectionstring;
        private IOrderDataAccess orderDataAccess;

        public UserDAO(string connectionstring)
        {
            this.connectionstring = connectionstring;
            orderDataAccess = DataAccessFactory.CreateRepository<IOrderDataAccess>(connectionstring);
        }

        public async Task<string> CreateUserAsync(User user)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);

            try
            {
                var passwordHash = BCryptTool.HashPassword(user.Password);

                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "INSERT INTO dbo.[User] (email, name, surname, phone, address, password, isAdmin) VALUES (@email, @name, @surname, @phone, @address, @password, @isAdmin)";
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@surname", user.Surname);
                command.Parameters.AddWithValue("@phone", user.PhoneNumber);
                command.Parameters.AddWithValue("@address", user.Address);
                command.Parameters.AddWithValue("@password", passwordHash);
                command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while creating user '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }


            return user.Email;
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "DELETE FROM [User] WHERE email = @email";
                command.Parameters.AddWithValue("@email", email);
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting user from DB '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            List<User> users = new List<User>();
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("Select * from [User]");
                selectCommand.Connection = connection;
                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    // dorobit
                    users.Add(new User(reader.GetString("email"), reader.GetString("name"), reader.GetString("surname"), reader.GetString("phone"), reader.GetString("address"), reader.GetString("password"), reader.GetBoolean("isAdmin")));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting users from DB '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
            return users;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                string query = "SELECT * FROM [User] WHERE email = @email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                IEnumerable<Order> customersOrders = await orderDataAccess.GetOrdersByUserAsync(email);
                return new User(reader.GetString("email"), reader.GetString("name"), reader.GetString("surname"), reader.GetString("phone"), reader.GetString("address"), reader.GetString("password"), reader.GetBoolean("isAdmin"), customersOrders);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting user from DB '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * from [User] where email = @email", connection);
                command.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                User user = new User(reader.GetString("email"), reader.GetString("name"), reader.GetString("surname"), reader.GetString("phone"), reader.GetString("address"),  reader.GetString("password"), reader.GetBoolean("isAdmin"));
                bool statement = BCryptTool.ValidatePassword(password, user.Password);
                if (user != null && statement) 
                return user;
                else
                {
                    throw new WrongLoginException($"Incorect login information for user '{user}'");
                }
            }
            catch (System.InvalidOperationException logEx){
                throw new WrongLoginException($"Incorect login information, message was '{logEx.Message}'");
            }
            finally
            {
                connection.Close();
            }

        }

        public async Task<bool> UpdatePasswordAsync(string email, string oldPassword, string newPassword)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                User? user = await LoginAsync(email, oldPassword);
                if(user != null)
                {
                    connection.Open();
                    var newPasswordHash = BCryptTool.HashPassword(newPassword);
                    SqlCommand command = new SqlCommand("UPDATE [User] SET password = @password WHERE email = @email", connection);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", newPasswordHash);
                    command.ExecuteNonQuery();
                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while updating user into DB '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {

            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();                            //email, name, surename, phone, address, username, password, isAdmin
                SqlCommand command = new SqlCommand("UPDATE [User] SET email = @email, name = @name, surename = @surename, phone = @phone, adddress = @address, password = @password, isAdmin = @isAdmin WHERE email = @email", connection);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@surename", user.Surname);
                command.Parameters.AddWithValue("@phone", user.PhoneNumber);
                command.Parameters.AddWithValue("@address", user.Address);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                int affected = command.ExecuteNonQuery();
                if (affected == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while updating user into DB '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
    }
}
