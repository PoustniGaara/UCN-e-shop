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
        private IOrderDataAccess? orderDataAccess;

        public UserDAO(string connectionstring, IOrderDataAccess orderDataAccess)
        {
            this.connectionstring = connectionstring;
            this.orderDataAccess = orderDataAccess;
        }

        public UserDAO(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public async Task<string> CreateAsync(User user)
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
                await command.ExecuteNonQueryAsync();
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

        public async Task<bool> DeleteAsync(string email)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = "DELETE FROM [User] WHERE email = @email";
                command.Parameters.AddWithValue("@email", email);
                await command.ExecuteNonQueryAsync();
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
                SqlDataReader reader = await selectCommand.ExecuteReaderAsync();
                while (reader.Read())
                {
                    users.Add(new User(reader.GetString("email"), reader.GetString("name"), reader.GetString("surname"), reader.GetString("phone"), reader.GetString("address"), reader.GetString("password"), reader.GetBoolean("isAdmin")));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while getting users from DB '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
            return users;
        }

        public async Task<User?> GetByEmailAsync(string email)
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
                IEnumerable<Order> customersOrders = await orderDataAccess.GetByUserAsync(email);
                return new User(reader.GetString("email"), reader.GetString("name"), reader.GetString("surname"), reader.GetString("phone"), reader.GetString("address"), reader.GetString("password"), reader.GetBoolean("isAdmin"), customersOrders);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while getting user from DB '{ex.Message}'.", ex);
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
                SqlDataReader reader = await command.ExecuteReaderAsync();
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
                throw new Exception($"An error occured while updating user password: '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<bool> UpdateAsync(User user)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();                            //email, name, surename, phone, address, username, password, isAdmin
                SqlCommand command = new SqlCommand("UPDATE [User] SET email = @email, name = @name, surname = @surname, phone = @phone, address = @address, password = @password, isAdmin = @isAdmin WHERE email = @email", connection);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@surname", user.Surname);
                command.Parameters.AddWithValue("@phone", user.PhoneNumber);
                command.Parameters.AddWithValue("@address", user.Address);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                int affected = await command.ExecuteNonQueryAsync();
                if (affected == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while updating user information: '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
