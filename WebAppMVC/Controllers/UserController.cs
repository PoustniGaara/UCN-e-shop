using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;
using WebAppMVC.ActionFilters;
using WebAppMVC.Tools;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class UserController : Controller
    {
        private IUserClient _userClient;
        private readonly IMapper _mapper;
        public UserController(IUserClient userClient, IMapper mapper)
        {
            _userClient = userClient;
            _mapper = mapper;
        }

        public async Task<ActionResult> Edit(string email)
        {
            var userDto = await _userClient.GetByEmailAsync(email);

            UserEditVM userEditVM = _mapper.Map<UserEditVM>(userDto);

            return View(userEditVM);
        }

        public async Task<ActionResult> Details(string email)
        {
            var userDto = await _userClient.GetByEmailAsync(email);

            UserDetailsVM userDetailsVM = _mapper.Map<UserDetailsVM>(userDto);

            return View(userDetailsVM);
        }

        public async Task<ActionResult> Register() => View(new UserEditVM());


        [HttpPost]
        public async Task<ActionResult> Create(UserEditVM userVM)
        {
            UserDto userDto = _mapper.Map<UserDto>(userVM);

            await _userClient.CreateAsync(userDto);

            return RedirectToAction("Login", "Authentication", new RouteValueDictionary(new LoginVM { Message = "Registration was succesful, now you can login!" }));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProfile(UserEditVM user)
        {
            await _userClient.UpdateAsync(_mapper.Map<UserDto>(user));
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(UserEditVM user)
        {
            UserDto userDto = _mapper.Map<UserDto>(user);
            userDto.Email = User.FindFirst(ClaimTypes.Email).Value;
            await _userClient.UpdatePasswordAsync(userDto);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authentication");
        }
    }
}
