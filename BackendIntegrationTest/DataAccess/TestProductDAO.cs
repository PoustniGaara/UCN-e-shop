
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using FakeItEasy;
using Newtonsoft.Json;
using System.Drawing;
using System.Net;
using System.Text;

namespace BackendIntegrationTest.DataAccess
{
    public class TestProductDAO : IDisposable
    {
        IProductDataAccess _productDataAccess;
        ICategoryDataAccess _categoryDataAcces;  
        private readonly string connectionString = "Data Source=.;Initial Catalog=TSP_Test_Database;Integrated Security=True;";
        Product _newProduct;
        string _category;

        //SETUP
        public TestProductDAO()
        {
            _productDataAccess = DataAccessFactory.CreateRepository<IProductDataAccess>(connectionString);
            _categoryDataAcces = DataAccessFactory.CreateRepository<ICategoryDataAccess>(connectionString);
            _category = GetCategory();
            _newProduct = new Product()
            {
                Name = "test",
                Description = "test",
                Price = 1,
                ProductSizeStocks = new List<ProductSizeStock>(),
                Category = _category

            };
        }

        string GetCategory()
        {
            return  _categoryDataAcces.GetAllAsync().Result.First().Name;
        }

        //TEARDOWN
        public async void Dispose()
        {
            //CLEAN UP
            await _productDataAccess.DeleteAsync(_newProduct.Id);
        }

        //GetAll
        [Fact]
        public async Task Product_GetAll_Success()
        {
            //ARRANGE
            //ACT
            IEnumerable<Product> result = await _productDataAccess.GetAllAsync();
            //ASSERT
            Assert.True(result.Count() > 0);
        }

        //GetById
        [Fact]
        public async Task Product_GetById_Success()
        {
            //ARRANGE
            int id = 5;
            //ACT
            var result = await _productDataAccess.GetByIdAsync(5);
            //ASSERT
            Assert.NotNull(result);
        }

        //Create
        [Fact]
        public async Task Product_Create_Success()
        {
            //ARRANGE
            //ACT
            var result = await _productDataAccess.CreateAsync(_newProduct);
            //ASSERT
            Assert.True(!result.Equals(0));
            
        }

        //Delete
        [Fact]
        public async Task Product_Delete_Success()
        {
            //ARRANGE
            int id = await _productDataAccess.CreateAsync(_newProduct);
            //ACT
            var result = await _productDataAccess.DeleteAsync(id);
            //ASSERT
            Assert.True(result);
        }

        //GetAllByCategory
        [Fact]
        public async Task Produt_GetAllByCategoryAsync_Success()
        {
            //ARRANGE
            //ACT
            var result = await _productDataAccess.GetAllByCategoryAsync(_category);
            //ASSERT
            Assert.True(result.Count() > 0);
        }

        //Update is not tested because it is not implemented yet



    }
}
