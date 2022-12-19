using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Extensions;

namespace BackendIntegrationTest.DataAccess
{
    public class TestCategoryDAO 
    {
        ICategoryDataAccess _CategoryDataAccess;
        private readonly string connectionString = "Data Source=.;Initial Catalog=TSP_Test_Database;Integrated Security=True;";
        Category _newCategory;
        string _categoryName;

        //SETUP
        public TestCategoryDAO()
        {
            _CategoryDataAccess = DataAccessFactory.CreateRepository<ICategoryDataAccess>(connectionString);
            _categoryName = GetCategory();
            _newCategory = new Category()
            {
                Name= "Test",
                Description= "Test",
            };
        }

        string GetCategory()
        {
            return _CategoryDataAccess.GetAllAsync().Result.First().Name;
        }

        //GetAll
        [Fact]
        public async Task Category_GetAll_Success()
        {
            //ARRANGE
            //ACT
            IEnumerable<Category> result = await _CategoryDataAccess.GetAllAsync();
            //ASSERT
            Assert.True(result.Count() > 0);
        }

        //GetByName
        [Fact]
        public async Task Category_GetByName_Success()
        {
            //ARRANGE
            //ACT
            var result = await _CategoryDataAccess.GetByNameAsync(_categoryName);
            //ASSERT
            Assert.NotNull(result);
        }

        //Create not implemented

        //Delete not implemented

    }

}
