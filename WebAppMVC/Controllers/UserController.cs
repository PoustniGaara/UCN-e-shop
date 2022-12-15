using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;
using WebAppMVC.ActionFilters;
using WebAppMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public async Task<ActionResult> Edit()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;

            var userDto = await _userClient.GetByEmailAsync(email);

            UserEditVM userEditVM = _mapper.Map<UserEditVM>(userDto);

            return View(userEditVM);
        }

        [Authorize]
        public async Task<ActionResult> Details()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;

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

            var viewData = new ViewDataDictionary<LoginVM>(new EmptyModelMetadataProvider(), ModelState);
            viewData = new ViewDataDictionary<LoginVM>(viewData, new LoginVM { Message = "Registration was successful!" });
            return new ViewResult()
            {
                ViewName = "/Views/Authentication/Login.cshtml",
                ViewData = viewData
            };

        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdateProfile(UserEditVM user)
        {
            if(user.Email != User.FindFirst(ClaimTypes.Email).Value)
            {
                return View("/Views/Shared/ActionForbiden.cshtml");
            }
            await _userClient.UpdateAsync(_mapper.Map<UserDto>(user));
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdatePassword(UserEditVM user)
        {
            if (user.Email != User.FindFirst(ClaimTypes.Email).Value)
            {
                return View("/Views/Shared/ActionForbiden.cshtml");
            }
            UserDto userDto = _mapper.Map<UserDto>(user);
            userDto.Email = User.FindFirst(ClaimTypes.Email).Value;
            await _userClient.UpdatePasswordAsync(userDto);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authentication");
        }
    }
}
