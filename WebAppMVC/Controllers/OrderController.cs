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

        private readonly static List<OrderDto> orders = new List<OrderDto>()
        {
            new OrderDto()
            {
                Id = 1, Date = DateTime.Now.AddDays(-3), Status = 1, Note = "If possible, please, package as a gift. Thanks", TotalPrice = 450, UserEmail = "matejrolko2",
                Items = new List<LineItemDto>()
                {
                    new LineItemDto()
                    {
                        Id = 0, Name = "Black T-Shirt", Description = "High quality T-shirt with the logo of UCN", Price = 100, Quantity = 2,
                        Total = 200
                    }, new LineItemDto()
                    {
                        Id = 1, Name = "Navy Blue Hoodie", Description = "High quality hoodie with embroided logo of UCN", Price = 250, Quantity = 1,
                        Total = 250
                    }
                }
            },new OrderDto()
            {
                Id = 2, Date = DateTime.Now.AddDays(-12), Status = 1, Note = "", TotalPrice = 100, UserEmail = "matejrolko2",
                Items = new List<LineItemDto>()
                {
                    new LineItemDto()
                    {
                        Id = 0, Name = "White T-Shirt", Description = "High quality T-shirt with the logo of UCN", Price = 100, Quantity = 1,
                        Total = 100
                    }
                }
            },
        };

        private IOrderClient _client;
        private readonly IMapper _mapper;

        public OrderController(IOrderClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexAsync()
        {
            HttpContext.Session.Set<OrderDto>("shopping_cart", null);
            IEnumerable<OrderDto> orderDtoList = orders; //await _client.GetAllOrdersAsync();
            return View(orderDtoList);
        }

        public IActionResult Details(int id)
        {
            return View(orders.First(order => order.Id == id));
           //return View(_client.GetOrderByIdAsync(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderDto order)
        {
            int id = -1;
            try
            {
                id = await _client.CreateOrderAsync(order);
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
