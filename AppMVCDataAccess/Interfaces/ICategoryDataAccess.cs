using DataAccessLayer.Model;

namespace DataAccessLayer.Interfaces
{
    public interface ICategoryDataAccess 
    {
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category> GetByNameAsync(string name);
        public Task CreateAsync(Category category);
        public Task DeleteAsync(string name);
    }

}