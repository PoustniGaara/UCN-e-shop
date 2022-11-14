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
    public class TestProductsController : /*IClassFixture<WebApplicationFactory<WebApi.Startup>>*/IAsyncLifetime
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
        //private readonly WebApplicationFactory<WebApi.Startup> _factory;
        //private IProductDataAccess _productDataAcces;
        //private IMapper _mapper;
        //private ProductsController _productsController;

        //public TestProductsController(WebApplicationFactory<WebApi.Startup> factory)
        //{
        //    _factory = factory;
        //    _productDataAcces = A.Fake<IProductDataAccess>();
        //    //mapper config 
        //    var config = new MapperConfiguration(cfg => {
        //        cfg.AddProfile(new ProductProfile());
        //    });
        //    _mapper = config.CreateMapper();

        //    _productsController = new(_productDataAcces, _mapper);
        //}

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

            //_profileServiceMock.Setup(profileService => profileService.GetFullProfile(username))
            //    .ThrowsAsync(givenException);

            var response = await _httpClient.GetAsync($"api/products/{category}");
            Assert.Equal(resultingStatusCode, response.StatusCode);
        }


    }
}