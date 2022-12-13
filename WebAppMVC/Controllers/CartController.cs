using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMVC.ActionFilters;
using WebAppMVC.Tools;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.GetCart();
            return View(cart);
        }

        public IActionResult Clear()
        {
            HttpContext.SaveCart(new OrderCreateVM());
            return Redirect("/cart");
        }

    }
}