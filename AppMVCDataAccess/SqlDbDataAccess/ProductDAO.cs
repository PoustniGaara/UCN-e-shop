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
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw new Exception("An unspecified error occured while trying to retrieve all the products from the database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public async Task<int> CreateAsync(Product order)
        {
            return 5;
        }

        public async Task DeleteAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "DELETE * FROM Product WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                await sizeStockDAO.DeleteAsync(id);
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw new Exception("An unspecified error occured while trying to retrieve all the products from the database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task UpdateAsync(Product product)
        {
            return;
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
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw new Exception("An unspecified error occured while trying to retrieve the product from the database: " + ex.Message);
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
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<ProductSizeStock> productSizeStocks = (List<ProductSizeStock>)await sizeStockDAO.GetByProductIdAsync(reader.GetInt32("id"));
                    products.Add(new Product(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("description"), reader.GetDecimal("price"), productSizeStocks, reader.GetString("category")));
                }
                return products;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw new Exception("An unspecified error occured while trying to retrieve all the products from the database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion
    }
}
