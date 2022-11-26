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
        public Task<User> GetUserByEmailAsync(string email);
        public Task<string> CreateUserAsync(User User);
        public Task<bool> DeleteUserAsync(string email);
        public Task<bool> UpdateUserAsync(User user);
    }
}
