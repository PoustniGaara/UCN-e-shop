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
    public class CategoryDAO : ICategoryDataAccess
    {
        private string connectionstring;

        public CategoryDAO(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public async Task CreateAsync(Category category)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO dbo.Category ('name', 'description') VALUES (@name, @description)";
            command.Parameters.AddWithValue("@name", category.Name);
            command.Parameters.AddWithValue("@description", category.Description);
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(string name)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Category WHERE name = @name", connection);
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            IList<Category> categories = new List<Category>();

            using SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Category", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category(reader.GetString("name"), reader.GetString("description")));
            }
            return categories;
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            using SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Category where name = @name", connection);
                command.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return new Category(reader.GetString("name"), reader.GetString("description"));
            }

            catch (Exception ex)
            {
                throw new Exception($"An error occured while getting category from DB '{ex.Message}'.", ex);
            }
            finally
            {
                connection.Close();
            }
           
        }
    }
}
