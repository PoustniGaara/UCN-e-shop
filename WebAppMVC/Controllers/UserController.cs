using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;
using WebApiClient.RestSharpClientImplementation;
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

        [HttpPost]
        public async Task<ActionResult> Create(UserDto userDto)
        {
            //UserDto userDto = _mapper.Map<UserDto>(userVM);

            if (!ModelState.IsValid)
            {
                return View("Register", userDto); // Return view with name Register with userDto objekt 
            }

            await _userClient.CreateAsync(userDto);

            return RedirectToAction("Login", "Authentication");
        }


        public async Task<ActionResult> Register() => View(new UserDto());


        //[HttpPost]
        //public async Task<ActionResult> UpdateProfile(UserEditVM user)
        //{
        //    var succes = await _userClient.UpdatePasswordAsync(_mapper.Map<UserDto>(user));
        //    if (succes) 
        //        return View();
        //    else 
        //        return null;       
        //}

        //[HttpPost]
        //public async Task<ActionResult> UpdatePassword(UserEditVM user)
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;
        //    string email = identity.FindFirst(ClaimTypes.Email).Value;
        //    UserDto userDto = _mapper.Map<UserDto>(user);
        //    userDto.Email = email;
        //    var succes = await _userClient.UpdatePasswordAsync(userDto);
        //    if (succes)
        //    {
        //        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //        return RedirectToAction("Login", "Authentication");
        //    }

        //    else
        //        return null;
        //}
    }
}
