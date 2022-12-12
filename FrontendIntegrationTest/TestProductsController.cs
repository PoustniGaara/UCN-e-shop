using DataAccessLayer.Interfaces;
using FakeItEasy;
using RestSharp;
using System.Net;
using WebApi.DTOs;
using System.Web;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace IntegrationTests.Frontend
{
    public class TestProductsController : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private string productUrl = "https://localhost:7183/Product";
        public TestProductsController(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        IProductDataAccess _productDataAcces = A.Fake<IProductDataAccess>();


        //[Fact]
        //public async Task<Task> Product_Details_Returns_Error_View_On_Exception()
        //{
        //    _client.BaseAddress = new Uri(productUrl);
        //    _client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));
        //}

        ////DETAILS
        //[Fact]
        //public async Task<Task> GET_ProductById_DbException_ServiceUnavailable503()
        //{
        //    return Assert_That_GetProductById_HandlesGivenException(
        //    givenException: new Exception($"database is down"),
        //    resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        //}

        //private async Task Assert_That_GetProductById_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        //{
        //    _client.BaseAddress = new Uri(productUrl);
        //    _client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpStatusCode statusCode = response.StatusCode;
        //    Assert.Equal(resultingStatusCode, statusCode);
        //}

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
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

    }


}
