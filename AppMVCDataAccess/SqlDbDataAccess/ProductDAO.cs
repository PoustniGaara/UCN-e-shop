using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.SqlDbDataAccess
{
    public class ProductDAO : IProductDataAccess
    {

        #region Properties + Constructor
        public string ConnectionString { get; set; }

        public ProductDAO(string connectionString)
        {
            ConnectionString = connectionString;
        }
        #endregion

        #region Methods

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // THIS DATA IS JUST FOR TEST PURPOUSES

            Category Category = new Category();
            Category.Name = "Gifts";
            Category.Description = "Lovely gifts";

            ProductSize productSize = new ProductSize();
            productSize.Size = "M";
            productSize.Stock = 1;


            Product P1 = new Product();
            P1.Name = "Bottle1";
            P1.Description = "Big bottle";
            P1.Price = 20;
            P1.ProductSize = productSize;
            P1.Category = Category;

            Product P2 = new Product();
            P2.Name = "Bottle2";
            P2.Description = "Medium bottle";
            P2.Price = 10;
            P2.ProductSize = productSize;
            P2.Category = Category;


            Product P3 = new Product();
            P3.Name = "Bottle3";
            P3.Description = "Small bottle";
            P3.Price = 5;
            P3.ProductSize = productSize;
            P3.Category = Category;


            List<Product> list = new List<Product>();
            list.Add(P1);   
            list.Add(P2);   
            list.Add(P3);

            //throw new Exception("From data acces");

            return list;
        }


        public async Task<int> CreateAsync(Product order)
        {
            return 5;
        }

        public async Task DeleteAsync(int id)
        {
        }

        public async Task UpdateAsync(Product order)
        {
            return;
        }

        public async Task<Product> GetByIdAsync(int id) 
        {
            Category Category = new Category();
            Category.Name = "Gifts";
            Category.Description = "Lovely gifts";

            ProductSize productSize = new ProductSize();
            productSize.Size = "M";
            productSize.Stock = 1;

            Product P1 = new Product();
            P1.Id = 5;
            P1.Name = "Bottle1";
            P1.Description = "Big bottle";
            P1.Price = 20;
            P1.ProductSize = productSize;
            P1.Category = Category;

            Product P2 = new Product();
            P2.Name = "Bottle2";
            P2.Description = "Medium bottle";
            P2.Price = 10;
            P2.ProductSize = productSize;
            P2.Category = Category;

            return P1;
        }

        public Task<IEnumerable<Product>> GetAllByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        Task<Product> GetByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        Task<Product> IProductDataAccess.GetByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
