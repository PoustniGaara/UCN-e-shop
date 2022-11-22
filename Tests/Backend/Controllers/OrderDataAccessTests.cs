using DataAccessLayer.Model;
using DataAccessLayer.SqlDbDataAccess;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace Tests.Backend.Controllers
{
    public class OrderDataAccessTests
    {
        private IOrderDataAccess _orderDAO;
        private Order _order;


        public OrderDataAccessTests()
        {
            _orderDAO = new OrderDAO("Server=hildur.ucn.dk;Database=CSC-CSD-S212_10407569;User Id=CSC-CSD-S212_10407569;Password=Password1!;");
        }

        [Fact]
        public async void Test_Creating_New_Order_Async()
        {            
            //ARRANGE & ACT
            _order = new Order(DateTime.Now, 100, Status.PLACED, "Vesterbro 25, 9000 Aalborg", "empty..", new User(), new List<LineItem>() { new LineItem(1, 2, 1)  });
            _order.Id = await _orderDAO.CreateOrderAsync(_order);
            
            //ASSERT
            Assert.True(_order.Id >0);
        }

        [Fact]
        public async void Test_Getting_An_Order_By_Id()
        {
            //ARRANGE & ACT
            var order = await _orderDAO.GetOrderByIdAsync(_order.Id);

            //ASSERT
            Assert.NotNull(order);
        }

        [Fact]
        public async void Test_Updating_Order_Async()
        {
            //ARRANGE
            _order.Note = "Updated note text!";
            
            //ACT
            await _orderDAO.UpdateOrderAsync(_order);
            var updatedOrder = await _orderDAO.GetOrderByIdAsync(_order.Id);
            
            //ASSERT
            Assert.Equal(updatedOrder.Note, _order.Note);
        }

        [Fact]
        public async Task Test_Getting_All_Orders_Async()
        {
            //ARRANGE & ACT
            List<Order> orders = (List<Order>) await _orderDAO.GetAllAsync();

            //ASSERT
            Assert.True(orders.Count > 0);
        }

    }
}
