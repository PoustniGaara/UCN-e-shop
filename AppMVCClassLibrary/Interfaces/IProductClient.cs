using WebApiClient.DTOs;

namespace WebApiClient.Interfaces
{
    public interface IProductClient
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync(string? category);
    }
}
