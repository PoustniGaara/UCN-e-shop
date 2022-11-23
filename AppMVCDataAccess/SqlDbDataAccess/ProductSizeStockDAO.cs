using DataAccessLayer.Exceptions;
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
    public class ProductSizeStockDAO : IProductSizeStockDataAccess
    {

        #region Properties + Constructor
        public string connectionString;

        public ProductSizeStockDAO(string connectionstring)
        {
            this.connectionString = connectionstring;
        }
        #endregion

        public Task CreateAsync(ProductSizeStock productSizeStock)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductSizeStock>> GetAByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductSizeStock>> GetByProductIdAsync(int id)
        {
            List<ProductSizeStock> productSizeStocks = new List<ProductSizeStock>();
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "SELECT ProductStock.size_id, ProductStock.stock, Sizes.size FROM ProductStock FULL OUTER JOIN Sizes ON ProductStock.size_id = Sizes.id WHERE product_id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    productSizeStocks.Add(new ProductSizeStock(reader.GetInt32("size_id"), reader.GetString("size"), reader.GetInt32("stock")));
                }
                return productSizeStocks;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw new Exception("An unspecified error occured while trying to retrieve all the product stock and sizes from the database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public Task<ProductSizeStock> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DecreaseStockWithCheck(int productId, int sizeId, int amountToDecrease)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);

            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            try
            {
                //Get the stock
                command.Parameters.AddWithValue("@product_id", productId);
                command.Parameters.AddWithValue("@size_id", sizeId);
                command = new SqlCommand("SELECT stock FROM ProductStock WHERE product_id = @productId AND size_id = sizeId", connection);
                SqlDataReader reader = command.ExecuteReader();

                //Check the stock 
                int stockAmount = reader.GetInt32("amount");
                if(stockAmount < amountToDecrease) { throw new ProductOutOfStockException();}

                //Decrease the stock
                command.Parameters.AddWithValue("@amountToDecrease", amountToDecrease);
                command = new SqlCommand("UPDATE ProductStock SET stock = stock - @amountToDecrease WHERE product_id = @productId AND size_id = sizeId", connection);
                command.ExecuteReader();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("An error occured while updateing name of an account: " + ex);
            }
            finally
            {
                connection.Close();
            }
            return true;
        }
    }
}
