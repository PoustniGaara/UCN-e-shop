//using FluentAssertions;
//using Microsoft.AspNetCore.TestHost;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Newtonsoft.Json;
//using System.Net;
//using System.Text;
//using WebApi;
//using WebApi.ActionFilters;
//using WebApiClient.DTOs;
//using WebAppMVC.Exceptions;
//using WebApiClient.RestSharpClientImplementation;

//namespace IntegrationTests
//{
//    public class TestProductsController : IAsyncLifetime
//    {
//        private HttpClient _httpClient = null!;
//        private ProductClient _client = new ProductClient(Configuration.WEB_API_URL);

//        public async Task InitializeAsync()
//        {
//            var hostBuilder = Program.CreateHostBuilder(new string[0])
//            .ConfigureWebHostDefaults(webHostBuilder => 
//            {
//                webHostBuilder.UseTestServer();
//            })
//            .ConfigureServices((_, services) =>
//            {

//                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//                services.AddControllers(options =>
//                {
//                    options.Filters.Add<ExceptionFilter>();
//                });
//            });

//            var host = await hostBuilder.StartAsync();
//            _httpClient = host.GetTestClient();
//        }

//        public Task DisposeAsync()
//        {
//            return Task.CompletedTask;
//        }

//        //DELETE COMPLETE
//        public async Task<Task> Delete_Product_DbException_ServiceUnavailable503()
//        {
//            return Assert_That_DeleteProduct_HandlesGivenException(
//            givenException: new Exception($"database is down"),
//            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
//        }

//        private async Task Assert_That_DeleteProduct_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
//        {
//            int id = 5;
//            var response = _client.DeleteOrderAsync(id);
//            HttpStatusCode statusCode = (HttpStatusCode)response.Status;
//            Assert.Equal(resultingStatusCode, statusCode);
//        }

//        //GET BY ID COMPLETE
//        public async Task<Task> GET_ProductById_DbException_ServiceUnavailable503()
//        {
//            return Assert_That_GetProductById_HandlesGivenException(
//            givenException: new Exception($"database is down"),
//            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
//        }

//        private async Task Assert_That_GetProductById_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
//        {
//            int id = 5;
//            var response = _client.GetProductByIdAsync(id);
//            HttpStatusCode statusCode = (HttpStatusCode)response.Status;
//            Assert.Equal(resultingStatusCode, statusCode);
//        }

//        //PUT COMPLETE
//        public async Task<Task> Put_Product_DbException_ServiceUnavailable503()
//        {
//            return Assert_That_PutProduct_HandlesGivenException(
//            givenException: new Exception($"database is down"),
//            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
//        }


//        private async Task Assert_That_PutProduct_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
//        {
//            ProductDto entity = new();
//            int id = 5;
//            var response = _client.UpdateProductAsync(entity);
//            HttpStatusCode statusCode = (HttpStatusCode)response.Status;
//            Assert.Equal(resultingStatusCode, statusCode);
//        }

//        [Fact]
//        public async Task Put_Wrong_Data_In_Product_Should_Return_UnprocessableEntity422()
//        {
//            //ARRANGE
//            int id = 5;
//            ProductDto entity = new();
//            entity.Name = "";
//            //ACT
//            await Assert.ThrowsAsync<UnprocessableEntityException>(() => _client.UpdateProductAsync(entity));


//            //var response = _client.UpdateProductAsync(entity);

//            ////ASSERT
//            //HttpStatusCode statusCode = (HttpStatusCode)response.Status;
//            //Assert.Equal(HttpStatusCode.UnprocessableEntity, statusCode);
//        }

//        public async Task Put_Null_Data_In_Product_Should_Return_BadRequestObjectResult400()
//        {
//            //ARRANGE
//            int id = 5;
//            ProductDto entity = new();
//            //ACT
//            var response = _client.UpdateProductAsync(entity);

//            //ASSERT
//            await Assert.ThrowsAsync<BadRequestException>(() => _client.UpdateProductAsync(entity));

//            //HttpStatusCode statusCode = (HttpStatusCode)response.Status;
//            //Assert.Equal(HttpStatusCode.BadRequest, statusCode);
//        }

//        //POST COMPLETE
//        public async Task Post_Wrong_Data_In_Product_Should_Return_UnprocessableEntity422()
//        {
//            //ARRANGE
//            //ACT
//            var response = await _httpClient.PostAsync($"api/products", new StringContent(
//                JsonConvert.SerializeObject(new ProductDto()
//                {
//                    Name = "",
//                    Description = "John",
//                    Price = 3,
//                    Category = "111-222-3333",
//                    Size = "222-333-4444",
//                    Stock = 2
//                }), 
//            Encoding.UTF8, 
//            "application/json"));
//            //ASSERT
//            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
//        }

//        public async Task<Task> Post_Product_DbException_ServiceUnavailable503()
//        {
//            return Assert_That_PostProduct_HandlesGivenException(
//            givenException: new Exception($"database is down"),
//            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
//        }

//        private async Task Assert_That_PostProduct_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
//        {
//            ProductDto entity = new();
//            var response = _client.CreateProductAsync(entity);
//            HttpStatusCode statusCode = (HttpStatusCode)response.Status;
//            Assert.Equal(resultingStatusCode, statusCode);
//        }

//        //GET ALL
//        public async Task<Task> Get_Products_WithoutQueryString_DbException_ServiceUnavailable503()
//        {
//            return Assert_That_GetProducts_HandlesGivenException(
//            givenException: new Exception($"database is down"),
//            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
//        }

//        private async Task Assert_That_GetProducts_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
//        {
//            var response = _client.GetAllProductsAsync();
//            HttpStatusCode statusCode = (HttpStatusCode)response.Status;
//            Assert.Equal(resultingStatusCode, statusCode);
//        }


//    }
//}