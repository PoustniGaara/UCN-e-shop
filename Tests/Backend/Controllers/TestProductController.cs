using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.InMemoryTestDataAccess;
using DataAccessLayer.Model;
using FakeItEasy;
using FluentAssertions;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using WebApi.Controllers;
using WebApi.DTOs;
using WebApi.MapperProfiles;

namespace Testing.Backend.Controllers
{
    public class TestProductController
    {
        private IProductDataAccess _productDataAcces;
        //private ILoggerManager _logger;
        private IMapper _mapper;
        private ProductsController _productsController;

        public TestProductController()
        {
            _productDataAcces = A.Fake<IProductDataAccess>();

            //_logger = new LoggerManager();

            //mapper config 
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ProductProfile());
            });
            _mapper = config.CreateMapper();

            _productsController = new(_productDataAcces, _mapper/*, _logger*/);
        }

        [Fact]
        public async Task Get_Products_WithoutQueryString_Success()
        {
            //ARRANGE
            var products = A.Fake<IEnumerable<Product>>();
            A.CallTo(() => _productDataAcces.GetAllAsync()).Returns(products);
            //ACT
            var request = await _productsController.Get("");
            var result = request.Result as ObjectResult;
            //ASSERT
            //Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            //request.Should().BeOfType<Task<ActionResult<IEnumerable<ProductDto>>>>()
            //.Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }

        //This test must be done by integration test
        //[Fact]
        //public async Task Get_Products_WithoutQueryString_UnSuccess()
        //{
        //    //ARRANGE
        //    var products = A.Fake<IEnumerable<Product>>();
        //    A.CallTo(() => _productDataAcces.GetAllAsync()).ThrowsAsync(new Exception());
        //    //ACT
        //    var request = await _productsController.Get("");
        //    var result = request.Result as ObjectResult;

        //    //ASSERT
        //    Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        //    request.Should().BeOfType<OkObjectResult>()
        //    .Which.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        //    //Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        //}
    }
}
