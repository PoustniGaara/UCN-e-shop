using AutoMapper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        #region Properties and constructor
        IUserDataAccess _userDataAccess;
        private readonly IMapper _mapper;


        public LoginsController(IUserDataAccess userDataAccess, IMapper mapper)
        {
            _userDataAccess = userDataAccess;
            _mapper = mapper;
        }
        #endregion

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] string email, string password)
        {
            User? user = await _userDataAccess.LoginAsync(email, password);
            if (email == null || password == null)
            {
                return BadRequest("Invalid login request");
            }
            if (user != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(/*new AuthenticatedResponse { Token = tokenString }*/);
            }
            return Unauthorized();
        }
    }
}
