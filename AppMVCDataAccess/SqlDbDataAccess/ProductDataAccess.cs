using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.SqlDbDataAccess
{
    internal class ProductDataAccess : IProductDataAccess
    {

        #region Properties + Constructor
        public string ConnectionString { get; set; }

        public ProductDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region Methods
        public Task<int> CreatProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            // THIS DATA IS JUST FOR TEST PURPOUSES

            Supplier Supplier = new Supplier();
            Supplier.Name = "FrankSRO";
            Supplier.Email = "Frank@sro.sk";

            Category Category = new Category();
            Category.Name = "Gifts";
            Category.Description = "Lovely gifts";

            Product P1 = new Product();
            P1.Name = "Bottle1";
            P1.Description = "Big bottle";
            P1.Price = 20;
            P1.Stock = 8;
            P1.Supplier = Supplier;
            P1.Category = Category;

            Product P2 = new Product();
            P2.Name = "Bottle2";
            P2.Description = "Medium bottle";
            P2.Price = 10;
            P2.Stock = 3;
            P2.Supplier = Supplier;
            P2.Category = Category;

            Product P3 = new Product();
            P3.Name = "Bottle3";
            P3.Description = "Small bottle";
            P3.Price = 5;
            P3.Stock = 11;
            P3.Supplier = Supplier;
            P3.Category = Category;


            IEnumerable<Product> list = new List<Product>();
            list.Append(P1);   
            list.Append(P2);   
            list.Append(P3);

            return (Task<IEnumerable<Product>>)list;
        }

        public Task<Product> GetProductByCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
