using DataAccessLayer.Model;

namespace DataAccessLayer
{
    public interface IProductDataAccess
        {
            public Task<IEnumerable<Product>> GetAllAsync();
            public Task<Product> GetProductByIdAsync(int id);
            public Task<Product> GetProductByCategoryAsync(Category category);
            public Task<int> CreateProductAsync(Product product);
            public Task<bool> DeleteProductAsync(int id);
            public Task<bool> UpdateProductAsync(Product product);
        }
 
}