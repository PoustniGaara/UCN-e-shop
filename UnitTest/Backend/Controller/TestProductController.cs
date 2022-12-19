using AutoMapper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using WebApi.Controllers;
using WebApi.DTOs;
using WebApi.MapperProfiles;
using WebApiClient.DTOs;
using ProductDto = WebApi.DTOs.ProductDto;

namespace UnitTest.Backend.Controllers
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
        public async Task GetAll_Products_WithQueryString_Success()
        {
            //ARRANGE
            var products = A.Fake<IList<Product>>();
            Product product = new ();
            product.Category = "TestCategory";
            products.Add(product);
            A.CallTo(() => _productDataAcces.GetAllAsync()).Returns(products);
            //ACT
            var request = await _productsController.Get("TestCategory");
            var result = request.Result as ObjectResult;
            //ASSERT
            result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_Product_Success()
        {
            //ARRANGE
            Product product = new();
            int id = 150;
            product.Id = id;
            A.CallTo(() => _productDataAcces.GetByIdAsync(id)).Returns(product);
            //ACT
            var request = await _productsController.Get(id);
            var result = request.Result as ObjectResult;
            //ASSERT
            result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Post_Product_Success()
        {
            //ARRANGE
            var productDto = A.Fake<ProductDto>();
            //var product = A.Fake<Product>();
            //A.CallTo(() => _productDataAcces.CreateAsync(product)).Returns(3);
            //ACT
            var request = await _productsController.Post(productDto);
            var result = request.Result as ObjectResult;
            //ASSERT
            result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Put_Product_Success()
        {
            //ARRANGE
            var productDto = A.Fake<ProductDto>();
            //A.CallTo(() => _productDataAcces.UpdateAsync(new Product())).Returns(true);
            //ACT
            var request = await _productsController.Put(12, productDto);
            //ASSERT
            Assert.IsType<OkResult>(request);
        }

        [Fact]
        public async Task Delete_Product_Success()
        {
            //ARRANGE
            int idToDelete = 5;
            Product product = new();
            product.Id = idToDelete;
            A.CallTo(() => _productDataAcces.DeleteAsync(idToDelete));
            //ACT
            var request = await _productsController.Delete(idToDelete);
            //ASSERT
            Assert.IsType<OkResult>(request);

        }

    }
}
