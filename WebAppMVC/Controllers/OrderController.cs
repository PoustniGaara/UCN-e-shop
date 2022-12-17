using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        private readonly IOrderClient _client;
        private readonly IMapper _mapper;

        public OrderController(IOrderClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<OrderDto> orderDtoList = await _client.GetAllAsync();
            OrderIndexVM orderIndexVM = _mapper.Map<OrderIndexVM>(orderDtoList);
            return View(orderIndexVM);
        }

        [Authorize]
        public async Task<ActionResult> Details(int id)
        {
            var order = await _client.GetByIdAsync(id);
            OrderDetailsVM orderVM = _mapper.Map<OrderDetailsVM>(order);
            //Check if the user email match with email in demanded order info
            if(orderVM.UserEmail != User.FindFirst(ClaimTypes.Email).Value)
            {
                return View("Views/Shared/ActionForbiden.cshtml");
            }
            return View(orderVM);
        }

        public ActionResult Create()
        {
            //Check if user is logged in
            if (!User.Identity.IsAuthenticated)
            {
                return View("NoAccountInfo");
            }
            else
            {
                var orderCreateVM = HttpContext.GetCart();
                //Add user info from cookie to order (cart) object, so it can be autofilled in the view
                orderCreateVM.Name = User.FindFirst(ClaimTypes.Name).Value;
                orderCreateVM.Surname = User.FindFirst(ClaimTypes.Surname).Value;
                orderCreateVM.Phone = User.FindFirst(ClaimTypes.MobilePhone).Value;
                orderCreateVM.UserEmail = User.FindFirst(ClaimTypes.Email).Value;
                return View(orderCreateVM);
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderCreateVM orderVM)
        {
            //Adjustments
            orderVM.Items = HttpContext.GetCart().Items;
            orderVM.TotalPrice = CalculateTotalOrderPrice(orderVM);
            orderVM.UserEmail = User.FindFirst(ClaimTypes.Email).Value;
            orderVM.Address = orderVM.Street + (orderVM.AptNumber.HasValue ? ", " + orderVM.AptNumber : "") + ", " + orderVM.City + " " + orderVM.PostalCode;
            //Create
            OrderDto orderDto = _mapper.Map<OrderDto>(orderVM);
            await _client.CreateAsync(orderDto);
            //Clear the cart
            HttpContext.SaveCart(new OrderCreateVM());

            return RedirectToAction(nameof(Index));
        }

        private decimal CalculateTotalOrderPrice(OrderCreateVM order)
        {
            var Shipping = 35;
            decimal total = Shipping + order.Items.Sum(i => i.Price * i.Quantity);
            return total;
        }

    }
}
