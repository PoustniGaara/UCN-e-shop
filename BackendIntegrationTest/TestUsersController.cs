using DataAccessLayer.Interfaces;
using FakeItEasy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.DTOs;

namespace IntegrationTests.Backend
{
    public class TestUsersController : IClassFixture<TestingWebApiFactory<Program>>
    {
        private readonly HttpClient _client;
        private string UserUrl = "https://localhost:44346/api/v1/Users";
        public TestUsersController(TestingWebApiFactory<Program> factory)
            => _client = factory.CreateClient();

        //DELETE COMPLETE
        [Fact]
        public async Task<Task> Delete_User_DbException_ServiceUnavailable503()
        {
            return Assert_That_DeleteUser_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_DeleteUser_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            string email = "best@ucn.dk";
            var response = await _client.DeleteAsync($"{UserUrl}/{email}");
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        //GET BY ID COMPLETE
        [Fact]
        public async Task<Task> GET_UserById_DbException_ServiceUnavailable503()
        {
            return Assert_That_GetUserByEmail_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_GetUserByEmail_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            string email = "best@ucn.dk";
            var response = await _client.DeleteAsync($"{UserUrl}/{email}");
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        //PUT COMPLETE
        [Fact]
        public async Task<Task> Put_User_DbException_ServiceUnavailable503()
        {
            return Assert_That_PutUser_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }


        private async Task Assert_That_PutUser_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            UserDto entity = new();
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{UserUrl}", content);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        [Fact]
        public async Task Put_Incorrect_Data_In_User_Should_Return_UnprocessableEntity422()
        {
            //ARRANGE
            UserDto entity = new();
            entity.Name = "";
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //ACT
            var response = await _client.PutAsync($"{UserUrl}/put", content);
            //ASSERT
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.UnprocessableEntity, statusCode);
        }

        [Fact]
        public async Task Put_Wrong_Data_Type_Should_Return_BadRequestObjectResult400()
        {
            //ARRANGE
            var json = JsonConvert.SerializeObject("");
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //ACT
            var response = await _client.PutAsync($"{UserUrl}/put", content);
            //ASSERT
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        //POST COMPLETE
        [Fact]
        public async Task<Task> Post_User_DbException_ServiceUnavailable503()
        {
            return Assert_That_PostUser_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_PostUser_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            UserDto entity = new();
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{UserUrl}", content);
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

        [Fact]
        public async Task Post_Incorrect_Data_In_User_Should_Return_UnprocessableEntity422()
        {
            //ARRANGE
            UserDto entity = new();
            entity.Name = "";
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //ACT
            var response = await _client.PostAsync($"{UserUrl}", content);
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
            var response = await _client.PostAsync($"{UserUrl}", content);
            //ASSERT
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        //GET ALL
        [Fact]
        public async Task<Task> Get_Users_WithoutQueryString_DbException_ServiceUnavailable503()
        {
            return Assert_That_GetUsers_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_GetUsers_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            var response = await _client.GetAsync($"{UserUrl}");
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }
    }
}
