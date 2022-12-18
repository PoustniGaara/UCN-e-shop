using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendIntegrationTest
{
    public class TestOrderController : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;

        public TestOrderController(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        //INDEX
        [Fact]
        public async Task Index_Returns_Success()
        {
            //ARRANGE
            string url = "https://localhost:7183/Order";
            //ACT
            var response = await _client.GetAsync(url);
            //ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        //DETAILS
        [Fact]
        public async Task Details_Returns_Success()
        {
            //ARRANGE
            string url = "https://localhost:7183/Order/Details/1";
            //ACT
            var response = await _client.GetAsync(url);
            //ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }


    }
}
