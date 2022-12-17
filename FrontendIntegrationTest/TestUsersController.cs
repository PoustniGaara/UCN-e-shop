using FakeItEasy;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAppMVC.ViewModels;

namespace IntegrationTests.Frontend
{
    public class TestUsersController : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public TestUsersController(WebApplicationFactory<Program> factory)
            => _client = factory.CreateClient();

        //DETAILS
        [Fact]
        public async Task Details_Returns_Success()
        {
            //ARRANGE
            string url = "https://localhost:7183/User/Details/1";
            //ACT
            var response = await _client.GetAsync(url);
            //ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        //CREATE
        [Fact]
        public async Task Create_Returns_Success()
        {
            //ARRANGE
            string url = "https://localhost:7183/User/Create";
            UserEditVM user = A.Fake<UserEditVM>();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email", user.Email),
                new KeyValuePair<string, string>("Name", user.Name),
                new KeyValuePair<string, string>("Surname", user.Surname),
                new KeyValuePair<string, string>("PhoneNumber", user.PhoneNumber),
                new KeyValuePair<string, string>("Address", user.Address),
                new KeyValuePair<string, string>("Password", user.Password),
                new KeyValuePair<string, string>("NewPassword", user.NewPassword),
            });
            //ACT
            var response = await _client.PostAsync(url, formData);
            //ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        //EDIT
        [Fact]
        public async Task Edit_Returns_Success()
        {
            //ARRANGE
            string url = "https://localhost:7183/User/Edit";
            //ACT
            var response = await _client.GetAsync(url);
            //ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        //UPDATE_PROFILE
        [Fact]
        public async Task UpdateProfile_Returns_Success()
        {
            //ARRANGE
            string url = "https://localhost:7183/User/UpdateProfile";
            UserEditVM user = A.Fake<UserEditVM>();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email", user.Email),
                new KeyValuePair<string, string>("Name", user.Name),
                new KeyValuePair<string, string>("Surname", user.Surname),
                new KeyValuePair<string, string>("PhoneNumber", user.PhoneNumber),
                new KeyValuePair<string, string>("Address", user.Address),
                new KeyValuePair<string, string>("Password", user.Password),
                new KeyValuePair<string, string>("NewPassword", user.NewPassword),
            });
            //ACT
            var response = await _client.PostAsync(url, formData);
            //ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        //UPDATE_PASSWORD
        [Fact]
        public async Task UpdatePassword_Returns_Success()
        {
            //ARRANGE
            string url = "https://localhost:7183/User/UpdatePassword";
            UserEditVM user = A.Fake<UserEditVM>();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email", user.Email),
                new KeyValuePair<string, string>("Name", user.Name),
                new KeyValuePair<string, string>("Surname", user.Surname),
                new KeyValuePair<string, string>("PhoneNumber", user.PhoneNumber),
                new KeyValuePair<string, string>("Address", user.Address),
                new KeyValuePair<string, string>("Password", user.Password),
                new KeyValuePair<string, string>("NewPassword", user.NewPassword),
            });
            //ACT
            var response = await _client.PostAsync(url, formData);
            //ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        private async Task LoginUser()
        {
            var requestUrl = "https://localhost:7183/Authentication/Login";

            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email", "email@email.com"),
                new KeyValuePair<string, string>("Password", "qqqq"),
            });

            var response = await _client.PostAsync(requestUrl, formData);
        }

    }
}
