using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace BackendIntegrationTest.DataAccess
{
    public class TestUserDAO : IDisposable
    {
        IUserDataAccess _UserDataAccess;
        private readonly string connectionString = "Data Source=.;Initial Catalog=TSP_Test_Database;Integrated Security=True;";
        User _newUser;
        User _existingUser;
        Product _product;
        ProductSizeStock _sizeStock;

        //SETUP
        public TestUserDAO()
        {
            _UserDataAccess = DataAccessFactory.CreateRepository<IUserDataAccess>(connectionString);
            _existingUser =  _UserDataAccess.GetAllAsync().Result.First();
            _newUser = new User()
            {
                Email = "email@email.com",
                Name = "John",
                Surname = "Wick",
                PhoneNumber = "0909090909",
                Address = "best adddress",
                Password = "aaaa",
                IsAdmin = false,
                Orders = new List<Order>() { }
            };

        }

        //TEARDOWN
        public async void Dispose()
        {
           await _UserDataAccess.DeleteAsync(_newUser.Email);
        }

        //GetAll
        [Fact]
        public async Task User_GetAll_Success()
        {
            //ARRANGE

            //ACT
            IEnumerable<User> result = await _UserDataAccess.GetAllAsync();
            //ASSERT
            Assert.True(result.Count() > 0);
        }

        //GetByEmailAsync
        [Fact]
        public async Task User_GetByEmailAsync_Success()
        {
            //ARRANGE
            //ACT
            var result = await _UserDataAccess.GetByEmailAsync(_existingUser.Email);
            //ASSERT
            Assert.NotNull(result);
        }

        //Create
        [Fact]
        public async Task User_Create_Success()
        {
            //ARRANGE
            //ACT
            var result = await _UserDataAccess.CreateAsync(_newUser);
            //ASSERT
            Assert.True(!result.Equals(""));
        }

        //Delete not implemented 
        //[Fact] 
        public async Task User_Delete_Success()
        {
            //ARRANGE
            string email = await _UserDataAccess.CreateAsync(_newUser);
            //ACT
            var result = await _UserDataAccess.DeleteAsync(email);
            //ASSERT
            Assert.True(result);
        }

        //Update
        [Fact]
        public async Task User_Update_Success()
        {
            //ARRANGE
            string updatedName = "Jack";
            _newUser.Name = updatedName;
            //ACT
            var result = await _UserDataAccess.UpdateAsync(_existingUser);
            //ASSERT
            Assert.True(result);
        }

        //UpdatePassword
        [Fact]
        public async Task User_UpdatePassword_Success()
        {
            //ARRANGE
            await _UserDataAccess.CreateAsync(_newUser);
            string updatedPassword = "aaaa";
            _newUser.NewPassword = updatedPassword;
            //ACT
            var result = await _UserDataAccess.UpdatePasswordAsync(_newUser.Email, _newUser.Password, updatedPassword);
            //ASSERT
            Assert.True(result);
        }

        //Login
        [Fact]
        public async Task User_Login_Success()
        {
            //ARRANGE
            await _UserDataAccess.CreateAsync(_newUser);
            //ACT
            var result = await _UserDataAccess.LoginAsync(_newUser.Email, _newUser.Password);
            //ASSERT
            Assert.NotNull(result);
        }
    }
}
