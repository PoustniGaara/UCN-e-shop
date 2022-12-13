using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApiClient.DTOs;
using WebAppMVC.Models;
using WebAppMVC.Tools;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        public IActionResult About()
        {
            return View();
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}