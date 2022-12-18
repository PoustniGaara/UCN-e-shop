

using FakeItEasy;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAppMVC.ViewModels;

namespace FrontendIntegrationTest
{
    public class TestAuthenticationController : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TestAuthenticationController(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        //LOGIN
        [Fact]
        public async Task Login_Returns_Success()
        {
            //ARRANGE
            string url = "https://localhost:7183/Authentication/Login/";
            LoginVM user = A.Fake<LoginVM>();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email", user.Email),

            });
            //ACT
            var response = await _client.PostAsync(url, formData);
            //ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        //LOGOUT
        [Fact]
        public async Task Logout_Returns_Success()
        {
            //ARRANGE
            string url = "https://localhost:7183/Authentication/Login";
            //ACT
            var response = await _client.GetAsync(url);
            //ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }


    }
}
