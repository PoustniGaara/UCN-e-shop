using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections;
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

        public async Task CreateSizeStocksFromProductListAsync(SqlCommand command, int product_id, IEnumerable<ProductSizeStock> productSizeStocks)
        {
            foreach(ProductSizeStock productSizeStock in productSizeStocks)
            {
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO dbo.ProductStock (product_id, size_id, stock) VALUES (@product_id, @size_id, @stock)";
                command.Parameters.AddWithValue("@product_id", product_id);
                command.Parameters.AddWithValue("@size_id", productSizeStock.Id);
                command.Parameters.AddWithValue("@stock", productSizeStock.Stock);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateSizeStockAsync(SqlCommand command, int product_id, IEnumerable<ProductSizeStock> productSizeStocks)
        {
            foreach (ProductSizeStock productSizeStock in productSizeStocks)
            {
                command.Parameters.Clear();
                command.CommandText = "UPDATE dbo.ProductStock SET stock = @stock WHERE product_id = @product_id AND size_id = @size_id";
                command.Parameters.AddWithValue("@product_id", product_id);
                command.Parameters.AddWithValue("@size_id", productSizeStock.Id);
                command.Parameters.AddWithValue("@stock", productSizeStock.Stock);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM dbo.ProductStock WHERE product_id = @product_id";
                command.Parameters.AddWithValue("@product_id", id);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while deleting product stock: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
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
            catch (Exception ex)
            {
                throw new Exception("An error occured while trying to retrieve all the product stock and sizes from the database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<bool> DecreaseStockWithCheckAsync(SqlCommand command, int productId, int sizeId, int amountToDecrease)
        {
            try
            {
                //Get the product stock
                command.Parameters.Clear();
                command.CommandText = "SELECT stock FROM ProductStock WHERE product_id = @product_id AND size_id = @size_id";
                command.Parameters.AddWithValue("@product_id", productId);
                command.Parameters.AddWithValue("@size_id", sizeId);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                //Check the amount of stock 
                reader.Read(); 
                int stockAmount = reader.GetInt32("stock");
                if(stockAmount < amountToDecrease)
                {
                    reader.Close();
                    throw new ProductOutOfStockException();
                }
                reader.Close();

                //Decrease the product stock
                command.Parameters.Clear();
                command.CommandText = "UPDATE ProductStock SET stock = stock - @amountToDecrease WHERE product_id = @product_id AND size_id = @size_id";
                command.Parameters.AddWithValue("@amountToDecrease", amountToDecrease);
                command.Parameters.AddWithValue("@product_id", productId);
                command.Parameters.AddWithValue("@size_id", sizeId);
                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch(ProductOutOfStockException outOfStockEx)
            {
                throw new ProductOutOfStockException($"The desired product is out of stock or the stock's quanity is less then desired: '{outOfStockEx.Message}'.", outOfStockEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while decreasing product stock: " + ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
