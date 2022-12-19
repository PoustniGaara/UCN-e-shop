using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendIntegrationTest.DataAccess
{
    public class TestOrderDAO : IDisposable
    {

        IOrderDataAccess _OrderDataAccess;
        IUserDataAccess _UserDataAcces;
        IProductDataAccess _ProductDataAccess;
        IProductSizeStockDataAccess _SizeStockDataAccess;
        private readonly string connectionString = "Data Source=.;Initial Catalog=TSP_Test_Database;Integrated Security=True;";
        Order _newOrder;
        Product _product;
        ProductSizeStock _sizeStock;
        string _userEmail;

        //SETUP
        public TestOrderDAO()
        {
            _OrderDataAccess = DataAccessFactory.CreateRepository<IOrderDataAccess>(connectionString);
            _UserDataAcces = DataAccessFactory.CreateRepository<IUserDataAccess>(connectionString);
            _ProductDataAccess = DataAccessFactory.CreateRepository<IProductDataAccess>(connectionString);
            _SizeStockDataAccess = DataAccessFactory.CreateRepository<IProductSizeStockDataAccess>(connectionString);
            _product = GetProduct();
            _userEmail = GetUserEmail();
            _sizeStock = GetSizeStock();
            _newOrder = new Order(DateTime.Now, 100, Status.PLACED, "Vesterbro 25, 9000 Aalborg", "empty..", _userEmail, new List<LineItem>() { new LineItem(_product, _sizeStock.Id, 1) });

        }

        ProductSizeStock GetSizeStock()
        {
            return _SizeStockDataAccess.GetByProductIdAsync(_product.Id).Result.First();
        }

        Product GetProduct()
        {
            return _ProductDataAccess.GetAllAsync().Result.First();
        }

        string GetUserEmail()
        {
            return _UserDataAcces.GetAllAsync().Result.First().Email;
        }

        //TEARDOWN
        public async void Dispose()
        {
            //CLEAN UP
            await _OrderDataAccess.DeleteAsync(_newOrder.Id);
        }

        //GetAll
        [Fact]
        public async Task Order_GetAll_Success()
        {
            //ARRANGE

            //ACT
            IEnumerable<Order> result = await _OrderDataAccess.GetAllAsync();
            //ASSERT
            Assert.True(result.Count() > 0);
        }

        //GetById
        [Fact]
        public async Task Order_GetById_Success()
        {
            //ARRANGE
            int id = (await _OrderDataAccess.GetAllAsync()).First().Id;
            //ACT
            var result = await _OrderDataAccess.GetByIdAsync(id);
            //ASSERT
            Assert.NotNull(result);
        }

        //GetByUser
        [Fact]
        public async Task Order_GetByUser_Success()
        {
            //ARRANGE
            //ACT
            var result = await _OrderDataAccess.GetByUserAsync(_userEmail);
            //ASSERT
            Assert.True(result.Count() > 0);
        }

        //Create
        [Fact]
        public async Task Order_Create_Success()
        {
            //ARRANGE
            //ACT
            var result = await _OrderDataAccess.CreateAsync(_newOrder);
            //ASSERT
            Assert.True(result > 0);
        }

        //Delete
        [Fact]
        public async Task Order_Delete_Success()
        {
            //ARRANGE
            int id = await _OrderDataAccess.CreateAsync(_newOrder);
            //ACT
            var result = await _OrderDataAccess.DeleteAsync(id);
            //ASSERT
            Assert.True(result);
        }

        //Update is not tested because it is not implemented yet
    }
}
