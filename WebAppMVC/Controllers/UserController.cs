using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiClient.Interfaces;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class UserController : Controller
    {
        private IUserClient _client;
        private readonly IMapper _mapper;
        public UserController(IUserClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<ActionResult> Details(string email)
        {
            var userDto = await _client.GetByEmailAsync(email);

            UserDetailsVM userDetailVM = _mapper.Map<UserDetailsVM>(userDto);

            return View(userDetailVM);
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
