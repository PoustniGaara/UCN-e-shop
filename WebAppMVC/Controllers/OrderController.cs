using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;
using WebAppMVC.ActionFilters;
using WebAppMVC.Tools;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class OrderController : Controller
    {

        private IOrderClient _client;
        private IOrderClient _productClient;
        private readonly IMapper _mapper;

        public OrderController(IOrderClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<ActionResult> IndexAsync()
        {
            IEnumerable<OrderDto> orderDtoList = await _client.GetAllAsync();
            return View(orderDtoList);
        }

        public async Task<ActionResult> DetailsAsync(int id)
        {
            var order = await _client.GetByIdAsync(id);

            OrderDetailsVM ordervm = _mapper.Map<OrderDetailsVM>(order);
            return View(ordervm);
        }

        public ActionResult Create()
        {
            var cart = HttpContext.GetCart();
            return View(cart);
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderDto order)
        {
            int id = -1;

            order.Items = HttpContext.GetCart().Items;
            order.TotalPrice = CalculateTotalOrderPrice(order);
            order.Address = order.Street + (order.AptNumber.HasValue ? ", " + order.AptNumber : "") + ", " + order.City + " " + order.PostalCode;
            await _client.CreateAsync(order);
            return RedirectToAction(nameof(Index));
        }

        private decimal CalculateTotalOrderPrice(OrderDto order)
        {
            var Shipping = 35;
            decimal total = Shipping + order.Items.Sum(i => i.Price * i.Quantity);
            return total;
        }

    }
}
