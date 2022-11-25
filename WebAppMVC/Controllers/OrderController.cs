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
            IEnumerable<OrderDto> orderDtoList = await _client.GetAllOrdersAsync();
            return View(orderDtoList);
        }

        public async Task<ActionResult> DetailsAsync(int id)
        {
            var order = await _client.GetOrderByIdAsync(id);
            OrderDetailVM ordervm = _mapper.Map<OrderDetailVM>(order);
            return View(ordervm);
        }

        public ActionResult Create()
        {
            return View(HttpContext.GetCart());
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderDto order)
        {
            int id = -1;
            try
            {
                id = await _client.CreateOrderAsync(order);
                order.Id = id;
                return RedirectToAction(nameof(Created));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _client.DeleteOrderAsync(id);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }
    }
}
