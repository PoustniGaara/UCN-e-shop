using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserDataAccess
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User?> GetByEmailAsync(string email);
        public Task<string> CreateAsync(User User);
        public Task<bool> DeleteAsync(string email);
        public Task<bool> UpdateAsync(User user);
        public Task<User?> LoginAsync(string email, string password);
        Task<bool> UpdatePasswordAsync(string email, string oldPassword, string newPassword);

    }
}
