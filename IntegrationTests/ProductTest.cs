using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;
using WebApiClient.RestSharpClientImplementation;
using WebAppMVC.Exceptions;

namespace IntegrationTests
{
    public class ProductTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private IProductClient _client = new ProductClient(Configuration.WEB_API_URL);

        //private readonly HttpClient _client;
        //public ProductTest(TestingWebAppFactory<Program> factory)
        //    => _client = factory.CreateClient();

        //DELETE COMPLETE
        [Fact]
        public async Task<Task> Delete_Product_DbException_ServiceUnavailable503()
        {
            return Assert_That_DeleteProduct_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_DeleteProduct_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            int id = 5;
            var response = _client.DeleteAsync(id);
            HttpStatusCode statusCode = (HttpStatusCode)response.Status;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        //GET BY ID COMPLETE
        [Fact]
        public async Task<Task> GET_ProductById_DbException_ServiceUnavailable503()
        {
            return Assert_That_GetProductById_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_GetProductById_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            int id = 5;
            var response = _client.GetByIdAsync(id);
            HttpStatusCode statusCode = (HttpStatusCode)response.Status;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        //PUT COMPLETE
        [Fact]
        public async Task<Task> Put_Product_DbException_ServiceUnavailable503()
        {
            return Assert_That_PutProduct_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }


        private async Task Assert_That_PutProduct_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            ProductDto entity = new();
            int id = 5;
            var response = _client.UpdateAsync(entity);
            HttpStatusCode statusCode = (HttpStatusCode)response.Status;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        [Fact]
        public async Task Put_Wrong_Data_In_Product_Should_Return_UnprocessableEntity422()
        {
        //ARRANGE
        int id = 5;
        ProductDto entity = new();
        entity.Name = "";
        //ACT
        await Assert.ThrowsAsync<UnprocessableEntityException>(() => _client.UpdateAsync(entity));


        //var response = _client.UpdateProductAsync(entity);

        ////ASSERT
        //HttpStatusCode statusCode = (HttpStatusCode)response.Status;
        //Assert.Equal(HttpStatusCode.UnprocessableEntity, statusCode);
        }
        [Fact]
        public async Task Put_Null_Data_In_Product_Should_Return_BadRequestObjectResult400()
        {
        //ARRANGE
        int id = 5;
        ProductDto entity = new();
        //ACT
        var response = _client.UpdateAsync(entity);

        //ASSERT
        await Assert.ThrowsAsync<BadRequestException>(() => _client.UpdateAsync(entity));

        //HttpStatusCode statusCode = (HttpStatusCode)response.Status;
        //Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        //POST COMPLETE
        //public async Task Post_Wrong_Data_In_Product_Should_Return_UnprocessableEntity422()
        //{
        //    //ARRANGE
        //    //ACT
        //    var response = await _httpClient.PostAsync($"api/products", new StringContent(
        //        JsonConvert.SerializeObject(new ProductDto()
        //        {
        //            Name = "",
        //            Description = "John",
        //            Price = 3,
        //            Category = "111-222-3333",
        //            Size = "222-333-4444",
        //            Stock = 2
        //        }),
        //    Encoding.UTF8,
        //    "application/json"));
        //    //ASSERT
        //    response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        //}

        [Fact]
        public async Task<Task> Post_Product_DbException_ServiceUnavailable503()
        {
        return Assert_That_PostProduct_HandlesGivenException(
        givenException: new Exception($"database is down"),
        resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_PostProduct_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
        ProductDto entity = new();
        var response = _client.CreateAsync(entity);
        HttpStatusCode statusCode = (HttpStatusCode)response.Status;
        Assert.Equal(resultingStatusCode, statusCode);
        }

        //GET ALL
        [Fact]
        public async Task<Task> Get_Products_WithoutQueryString_DbException_ServiceUnavailable503()
        {
        return Assert_That_GetProducts_HandlesGivenException(
        givenException: new Exception($"database is down"),
        resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_GetProducts_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            var response = _client.GetAllAsync();
            HttpStatusCode statusCode = (HttpStatusCode)response.Status;
            Assert.Equal(resultingStatusCode, statusCode);
        }

    }


}
