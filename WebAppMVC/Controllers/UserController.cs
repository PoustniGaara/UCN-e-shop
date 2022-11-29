using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiClient.Interfaces;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class UserController : Controller
    {
        private IUserClient _userClient;
        private readonly IMapper _mapper;
        public UserController(IUserClient userClient, IMapper mapper)
        {
            _userClient = userClient;
            _mapper = mapper;
        }

        public async Task<ActionResult> Details(string email)
        {
            var userDto = await _userClient.GetByEmailAsync(email);

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
