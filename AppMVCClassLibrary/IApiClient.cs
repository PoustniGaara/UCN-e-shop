using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;

namespace WebApiClient
{
    public interface IApiClient
    {
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllBlogPostsAsync();
    }
}
