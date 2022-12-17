using System.Net;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;
using WebApiClient.RestSharpClientImplementation;

namespace IntegrationTests.Frontend
{
    public class TestProductsController : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;

        public TestProductsController(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }


        //INDEX
        [Fact]
        public async Task Index_Returns_Success()
        {
            //ARRANGE
            string url = "https://localhost:7183/Product";
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
            string url = "https://localhost:7183/Product/Details/1";
            //ACT
            var response = await _client.GetAsync(url);
            //ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }



    }
}
