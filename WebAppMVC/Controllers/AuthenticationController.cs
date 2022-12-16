using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using WebApiClient.DTOs;
using WebApiClient.Interfaces;
using WebAppMVC.ActionFilters;
using WebAppMVC.ViewModels;
using System.IdentityModel.Tokens.Jwt;

namespace WebAppMVC.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    public class AuthenticationController : Controller
    {
        private IAuthenticationClient _client;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginVM loginInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(loginInfo);
            }
            LoginModelDto loginModelDto = _mapper.Map<LoginModelDto>(loginInfo);
            string result = await _client.LoginAsync(loginModelDto);
            string? TokenString = (string?)JObject.Parse(result)["token"];

            if (TokenString != null)
            {
                JwtSecurityToken Jst = new(TokenString);

                List<Claim> theApiClaims = (List<Claim>)Jst.Claims.ToList();
                theApiClaims.Add(new Claim("token", TokenString));

                var claimsIdentity = new ClaimsIdentity(theApiClaims, "Login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(loginInfo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
