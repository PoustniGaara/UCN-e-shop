using DataAccessLayer.Interfaces;
using FakeItEasy;
using IntegrationTests.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackendIntegrationTest
{
    public class TestCategoriesController : IClassFixture<TestingWebApiFactory<Program>>
    {
        private readonly HttpClient _client;
        private string categoryUrl = "https://localhost:44346/api/v1/categories";
        public TestCategoriesController(TestingWebApiFactory<Program> factory)
            => _client = factory.CreateClient();

        //GET ALL
        [Fact]
        public async Task<Task> Get_Categories_WithoutQueryString_DbException_ServiceUnavailable503()
        {
            return Assert_That_GetProducts_HandlesGivenException(
            givenException: new Exception($"database is down"),
            resultingStatusCode: HttpStatusCode.ServiceUnavailable);
        }

        private async Task Assert_That_GetProducts_HandlesGivenException(Exception givenException, HttpStatusCode resultingStatusCode)
        {
            var response = await _client.GetAsync($"{categoryUrl}");
            HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(resultingStatusCode, statusCode);
        }

    }
}
