using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;
using WebApiClient.RestSharpClientImplementation;
using WebAppMVC.ActionFilters;
using WebAppMVC.Tools;
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

<<<<<<< Updated upstream
        [HttpPost]
        public async Task<ActionResult> UpdateProfile(UserEditVM user)
        {
            var succes = await _userClient.UpdateAsync(_mapper.Map<UserDto>(user));
            if (succes) 
                return View();
            else 
                return null;       
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(UserEditVM user)
        {
            var succes = await _userClient.UpdateAsync(_mapper.Map<UserDto>(user));
            if (succes)
                return View();
            else
                return null;
        }
=======
>>>>>>> Stashed changes
    }
}
