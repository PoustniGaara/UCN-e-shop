using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductSizeStock = DataAccessLayer.Model.ProductSizeStock;

namespace DataAccessLayer.SqlDbDataAccess
{
    public class ProductDAO : IProductDataAccess
    {

        #region Properties + Constructor
        public string connectionString;
        private IProductSizeStockDataAccess sizeStockDAO;
        private ICategoryDataAccess categoryDAO;


        public ProductDAO(string connectionstring)
        {
            this.connectionString = connectionstring;
            this.sizeStockDAO = new ProductSizeStockDAO(connectionString);
            this.categoryDAO = new CategoryDAO(connectionString);
        }
        #endregion

        #region Methods

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            List<Product> products = new List<Product>();
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "SELECT * FROM Product";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<ProductSizeStock> productSizeStocks = (List<ProductSizeStock>)await sizeStockDAO.GetByProductIdAsync(reader.GetInt32("id"));
                    products.Add(new Product(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("description"), reader.GetDecimal("price"), productSizeStocks, reader.GetString("category")));
                }
                return products;
            }
            finally
            {
                connection.Close();
            }
        }


        public async Task<int> CreateAsync(Product product)
        {
            int id = 0;
            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            try
            {
                command.CommandText = "INSERT INTO dbo.Product (name, description, price, category) VALUES " +
                            "(@name, @description, @price, @category); SELECT CAST(scope_identity() AS int)";
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@category", product.Category);
                id = (int)command.ExecuteScalar();
                product.Id = id;

                await sizeStockDAO.CreateSizeStocksFromProductListAsync(command, id, product.ProductSizeStocks);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
            }
            return id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "DELETE FROM Product WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                await sizeStockDAO.DeleteAsync(id);
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public async Task UpdateAsync(Product product)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction; 
            try
            {
                command.CommandText = "UPDATE dbo.Product SET name = @name, description = @description, price = @price, category = @category WHERE id = @id";
                command.Parameters.AddWithValue("@id", product.Id);
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@category", product.Category);
                command.ExecuteNonQuery();

                await sizeStockDAO.UpdateProductSizeStock(command, product.Id, product.ProductSizeStocks);
                transaction.Commit();
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<Product> GetByIdAsync(int id) 
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "SELECT * FROM Product WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                List<ProductSizeStock> productSizeStocks = (List<ProductSizeStock>)await sizeStockDAO.GetByProductIdAsync(id);
                return new Product(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("description"), reader.GetDecimal("price"), productSizeStocks, reader.GetString("category"));
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<IEnumerable<Product>> GetAllByCategoryAsync(string category)
        {
            List<Product> products = new List<Product>();
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "SELECT * FROM Product where category = @category";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@category", category);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<ProductSizeStock> productSizeStocks = (List<ProductSizeStock>)await sizeStockDAO.GetByProductIdAsync(reader.GetInt32("id"));
                    products.Add(new Product(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("description"), reader.GetDecimal("price"), productSizeStocks, reader.GetString("category")));
                }
                return products;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion
    }
}
