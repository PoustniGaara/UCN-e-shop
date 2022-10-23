using AppMVCClassLibrary;

namespace DataAccessLayer
{
    public class IProductDataAccess
    {
        public interface IMovieDataAccess
        {
            public IEnumerable<Product> GetAll();
            public Product GetProductById(int id);
            public Product GetProductByCategory(Category category);
            public int AddProduct(Product product);
            public bool DeleteProduct(int id);
            public bool UpdateProduct(Product product);
        }
    }
}