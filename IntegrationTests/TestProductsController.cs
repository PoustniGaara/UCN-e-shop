namespace IntegrationTests
{
    public class TestProductsController : IClassFixture<TestingWebAppFactory<Program>>
    {

        private readonly HttpClient _client;

        public TestProductsController(TestingWebAppFactory<Program> factory)
        => _client = factory.CreateClient();

        [Fact]
        public void Test1()
        {

        }
    }
}