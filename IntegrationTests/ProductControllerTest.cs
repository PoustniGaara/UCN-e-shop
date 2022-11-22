using DataAccessLayer.Interfaces;
using FakeItEasy;
using RestSharp;
using System.Net;
using WebApi.DTOs;
using WebAppMVC.Exceptions;
using System.Web;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTests
{
    public class ProductControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private string productUrl = "https://localhost:44346/api/v1/products";
        public ProductControllerTest(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        IProductDataAccess _productDataAcces = A.Fake<IProductDataAccess>();


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
            var response = await _client.DeleteAsync($"{productUrl}/{id}");
            HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
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
            var response = await _client.DeleteAsync($"{productUrl}/{id}");
            HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
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
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{productUrl}", content);
            HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        [Fact]
        public async Task Put_Incorrect_Data_In_Product_Should_Return_UnprocessableEntity422()
        {
            //ARRANGE
            ProductDto entity = new();
            entity.Name = "";
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //ACT
            var response = await _client.PutAsync($"{productUrl}/put",content);
            //ASSERT
            HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
            Assert.Equal(HttpStatusCode.UnprocessableEntity, statusCode);
        }

        [Fact]
        public async Task Put_Wrong_Data_Type_Should_Return_BadRequestObjectResult400()
        {
            //ARRANGE
            var json = JsonConvert.SerializeObject("");
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //ACT
            var response = await _client.PutAsync($"{productUrl}/put", content);
            //ASSERT
            HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        //POST COMPLETE
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
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{productUrl}", content);
            HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        public async Task Post_Incorrect_Data_In_Product_Should_Return_UnprocessableEntity422()
        {
            //ARRANGE
            ProductDto entity = new();
            entity.Name = "";
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //ACT
            var response = await _client.PostAsync($"{productUrl}", content);
            //ASSERT
            HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
            Assert.Equal(HttpStatusCode.UnprocessableEntity, statusCode);
        }

        [Fact]
        public async Task Post_Wrong_Data_Type_Should_Return__BadRequestObjectResult400()
        {
            //ARRANGE
            var json = JsonConvert.SerializeObject("");
            var content = new StringContent(json, Encoding.UTF8,"application/json");
            //ACT
            var response = await _client.PostAsync($"{productUrl}", content);
            //ASSERT
            HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
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
            var response = await _client.GetAsync($"{productUrl}");
            HttpStatusCode statusCode = (HttpStatusCode)response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

    }


}
