using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApiClient;
using WebApiClient.DTOs;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class OrderController : Controller
    {
        private IApiClient _client;
        private readonly IMapper _mapper;

        public OrderController(IApiClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderDto order)
        {
            try
            {
                await _client.CreateOrderAsync(order);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<OrderDto> orderDtoList = Enumerable.Empty<OrderDto>();

            try
            {
                orderDtoList = await _client.GetAllOrdersAsync();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            OrderIndexVM orderIndexVM = _mapper.Map<OrderIndexVM>(orderDtoList);
            return View(orderIndexVM);
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

        [HttpPost]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                await _client.GetOrderByIdAsync(id);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

    }
}
