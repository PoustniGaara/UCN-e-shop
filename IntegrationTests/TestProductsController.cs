using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Model;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;
using WebApi;
using WebApi.ActionFilters;
using WebApi.Controllers;
using WebApi.MapperProfiles;

namespace IntegrationTests
{
    public class TestProductsController : IAsyncLifetime
    {
        private HttpClient _httpClient = null!;

        public async Task InitializeAsync()
        {
            var hostBuilder = Program.CreateHostBuilder(new string[0])
            .ConfigureWebHostDefaults(webHostBuilder => 
            {
                webHostBuilder.UseTestServer();
            })
            .ConfigureServices((_, services) =>
            {
                services.AddAutoMapper(typeof(Startup));

                services.AddControllers(options =>
                {
                    options.Filters.Add<ExceptionFilter>();
                });
            });

            var host = await hostBuilder.StartAsync();
            _httpClient = host.GetTestClient();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task<Task> Get_Products_WithoutQueryString_UnSuccess()
        {
            return AssertThatGetFullProfileHandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task AssertThatGetFullProfileHandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            var category = "";

            var response = await _httpClient.GetAsync($"api/products/{category}");
            Assert.Equal(resultingStatusCode, response.StatusCode);
        }


    }
}