using IntegrationTests.Backend;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace BackendIntegrationTest
{
    public class TestOrderController : IClassFixture<TestingWebApiFactory<Program>>
    {
        private readonly HttpClient _client;
        private string orderUrl = "https://localhost:44346/api/v1/orders";
        public TestOrderController(TestingWebApiFactory<Program> factory)
            => _client = factory.CreateClient();

        //DELETE COMPLETE
        [Fact]
        public async Task<Task> Delete_Order_DbException_ServiceUnavailable503()
        {
            return Assert_That_DeleteOrder_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_DeleteOrder_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            int id = 5;
            var response = await _client.DeleteAsync($"{orderUrl}/{id}");
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        //GET BY ID COMPLETE
        [Fact]
        public async Task<Task> GET_OrderById_DbException_ServiceUnavailable503()
        {
            return Assert_That_GetOrderById_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_GetOrderById_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            int id = 5;
            var response = await _client.DeleteAsync($"{orderUrl}/{id}");
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        //POST COMPLETE
        [Fact]
        public async Task<Task> Post_Order_DbException_ServiceUnavailable503()
        {
            return Assert_That_PostOrder_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_PostOrder_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            OrderDto entity = new();
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{orderUrl}", content);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        [Fact]
        public async Task Post_Incorrect_Data_In_Order_Should_Return_UnprocessableEntity422()
        {
            //ARRANGE
            OrderDto entity = new();
            entity.Address = "";
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //ACT
            var response = await _client.PostAsync($"{orderUrl}", content);
            //ASSERT
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.UnprocessableEntity, statusCode);
        }

        [Fact]
        public async Task Post_Wrong_Data_Type_Should_Return__BadRequestObjectResult400()
        {
            //ARRANGE
            var json = JsonConvert.SerializeObject("");
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //ACT
            var response = await _client.PostAsync($"{orderUrl}", content);
            //ASSERT
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        //GET ALL
        [Fact]
        public async Task<Task> Get_Orders_WithoutQueryString_DbException_ServiceUnavailable503()
        {
            return Assert_That_GetOrders_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_GetOrders_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            var response = await _client.GetAsync($"{orderUrl}");
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }
    }
}
