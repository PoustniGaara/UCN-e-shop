using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.InMemoryTestDataAccess;
using LoggerService;
using WebApi.Controllers;

namespace Testing.Backend.Controllers
{
    public class TestProductController
    {
        IProductDataAccess productDataAcces;
        ILoggerManager logger;
        IMapper  mapper;

        public TestProductController()
        {
            productDataAcces = new InMemoryProductDAO();
            logger = new LoggerManager();
        }
        
        [Test]
        public async Task Get_Products_Without_Parameter_Success()
        {
            //ARRANGE
            ProductsController controller = new(productDataAcces, mapper, logger);
            //controller.Request = new HttpRequestMessage();
            //controller.Configuration = new HttpConfiguration();

            //ACT
            var response = controller.Get();
            //ASSERT

            //// Arrange
            //var controller = new ProductsController(repository);
            //controller.Request = new HttpRequestMessage();
            //controller.Configuration = new HttpConfiguration();

            //// Act
            //var response = controller.Get(10);

            //// Assert
            //Product product;
            //Assert.IsTrue(response.TryGetContentValue<Product>(out product));
            //Assert.AreEqual(10, product.Id);

        }
    }
}
