using WebApiClient.DTOs;

namespace WebApiClient.Interfaces
{
    public interface ICategoryClient
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
    }
}
