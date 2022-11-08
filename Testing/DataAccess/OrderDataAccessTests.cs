using DataAccessLayer;
using DataAccessLayer.Model;
using DataAccessLayer.SqlDbDataAccess;

namespace Testing.DataAccess
{
    public class OrderDataAccessTests
    {
        private Order _order;
        private IOrderDataAccess _orderDAO;

        [SetUp]
        public async Task Setup()
        {
            _orderDAO = new InMemoryOrderDAO();
            await CreateNewOrderAsync();
        }

        private async Task<Order> CreateNewOrderAsync()
        {
            _order = new Order() { Date = DateTime.Now, TotalPrice = 57, Note = "note...", Status = Status.PLACED, User = new User(), Items = new List<LineItem>() };
            _order.Id = await _orderDAO.CreateOrderAsync(_order);
            return _order;
        }

        [TearDown]
        public async Task CleanUp()
        {
            await _orderDAO.DeleteOrderAsync(_order.Id);
        }

        [Test]
        public void CreateOrder()
        {
            //ARRANGE & ACT is done in Setup()
            //ASSERT
            Assert.IsTrue(_order.Id > -1, "Created Order ID not returned");
        }

        [Test]
        public async Task GetAllOrders()
        {
            //ARRANGE
            //ACT
            var orders = await _orderDAO.GetAllAsync();
            //ASSERT
            Assert.IsTrue(orders.Count() > 0, "No Orders returned");
        }


    }
}