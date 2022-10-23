using AppMVCClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.SqlDbDataAccess
{
    internal class ProductDataAccess : IProductDataAccess
    {
        public int AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetProductByCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
