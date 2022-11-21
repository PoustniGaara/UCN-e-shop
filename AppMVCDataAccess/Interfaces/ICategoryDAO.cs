using DataAccessLayer.Model;

namespace DataAccessLayer.Interfaces
{
    public interface IProductDataAccess 
    {
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task<Product> GetByIdAsync(int id);
        public Task<int> CreateAsync(Product model);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(Product product);
        public Task<IEnumerable<Product>> GetAllByCategoryAsync(string category);
        public Task<Product> GetByCategoryAsync(string category);
    }

}