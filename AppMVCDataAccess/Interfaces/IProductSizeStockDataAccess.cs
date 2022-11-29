using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProductSizeStockDataAccess
    {
        public Task<IEnumerable<ProductSizeStock>> GetByProductIdAsync(int id);
        public Task<ProductSizeStock> GetByIdAsync(int id);
        public Task CreateAsync(ProductSizeStock productSizeStock);
        public Task DeleteAsync(int id);
        public Task<bool> DecreaseStockWithCheck(SqlCommand command, int productId, int sizeId, int amountToDecrease);
    }
}
