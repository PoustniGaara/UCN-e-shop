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
        public ActionResult Login() => View(new LoginModelVM());

        //receives the login form on submit
        [HttpPost]
        public async Task<IActionResult> Login(LoginModelVM loginInfo/*, [FromQuery] string returnUrl*/)
        {
            //if (string.IsNullOrEmpty(returnUrl)) { return RedirectToAction(); }

            LoginModelDto loginModelDto = _mapper.Map<LoginModelDto>(loginInfo);
            string result = await _client.LoginAsync(loginModelDto);
            if (result != "")
            {
                string? TokenString = (string?)JObject.Parse(result)["token"];

                if (TokenString != null)
                {
                    JwtSecurityToken Jst = new(TokenString);

                    List<Claim> theApiClaims = (List<Claim>)Jst.Claims.ToList();
                    theApiClaims.Add(new Claim("token", TokenString));

                    var claimsIdentity = new ClaimsIdentity(theApiClaims, "Login");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        #region often used options - to consider including in cookie
                        //AllowRefresh = <bool>,
                        // Refreshing the authentication session should be allowed.

                        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        //IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        //IssuedUtc = <DateTimeOffset>,
                        // The time at which the authentication ticket was issued.

                        //RedirectUri = <string>
                        // The full path or absolute URI to be used as an http 
                        // redirect response value. 
                        #endregion
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Login");
                }
            }
            else
            {
                return View("../Login/Login");
            }
            //if (user != null) { await SignIn(user); }
            //if (string.IsNullOrEmpty(returnUrl)) { return RedirectToAction(); }
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
