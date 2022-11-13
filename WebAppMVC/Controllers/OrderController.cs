using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiClient;

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
    }
}
