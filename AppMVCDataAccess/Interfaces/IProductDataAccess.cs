using DataAccessLayer.Model;

namespace DataAccessLayer.Interfaces
{
    public interface IProductDataAccess 
    {
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task<Product> GetByIdAsync(int id);
        public Task<int> CreateAsync(Product product);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(Product product);
        public Task<IEnumerable<Product>> GetAllByCategoryAsync(string category);
    }

}