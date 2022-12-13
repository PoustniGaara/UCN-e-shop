
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.ActionFilters;

namespace WebAppMVC.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}