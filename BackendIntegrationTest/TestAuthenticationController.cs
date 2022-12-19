using DataAccessLayer.Interfaces;
using FakeItEasy;
using IntegrationTests.Backend;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using WebApi.DTOs;

namespace BackendIntegrationTest
{
    public class TestAuthenticationController : IClassFixture<TestingWebApiFactory<Program>>
    {
        private readonly HttpClient _client;
        private string authenticationUrl = "https://localhost:44346/api/v1/authentication";
        public TestAuthenticationController(TestingWebApiFactory<Program> factory)
            => _client = factory.CreateClient();

        //LOGIN
        [Fact]
        public async Task Post_Incorrect_Data_In_Login_Should_Return_Forbiden()
        {
            //ARRANGE
            LoginModelDto model = new();
            model.Email = "";
            model.Password= "password";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //ACT
            var response = await _client.PostAsync($"{authenticationUrl}", content);
            //ASSERT
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.Forbidden, statusCode);
        }
    }
}
