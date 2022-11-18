using AutoMapper;
using Azure;
using DataAccessLayer.Interfaces;
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
        private IMapper _mapper;
        private ProductsController _productsController;

        public TestProductController()
        {
            _productDataAcces = A.Fake<IProductDataAccess>();

            //mapper config 
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new ProductProfile());
            });
            _mapper = config.CreateMapper();

            _productsController = new(_productDataAcces, _mapper);
        }

        [Fact]
        public async Task GetAll_Products_WithoutQueryString_Success()
        {
            //ARRANGE
            var products = A.Fake<IEnumerable<Product>>();
            A.CallTo(() => _productDataAcces.GetAllAsync()).Returns(products);
            //ACT
            var request = await _productsController.Get("");
            var result = request.Result as ObjectResult;
            //ASSERT
            result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_Success()
        {
            //ARRANGE
            int idToDelete = 5;
            Product product = new();
            product.Id = idToDelete;

            var products = A.Fake<IEnumerable<Product>>();
            products.Append(product);
            A.CallTo(() => _productDataAcces.DeleteAsync(idToDelete));
            //ACT
            var request = await _productsController.Delete(idToDelete);
            //ASSERT
            Assert.IsType<OkResult>(request);

        }

    }
}
