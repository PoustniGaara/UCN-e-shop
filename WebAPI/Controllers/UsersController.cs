using AutoMapper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Properties and Constructor
        IUserDataAccess _userDataAccess;
        private readonly IMapper _mapper;

        public UsersController(IUserDataAccess userDataAccess, IMapper mapper)
        {
            _userDataAccess = userDataAccess;
            _mapper = mapper;
        }
        #endregion


        #region Methods
        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            IEnumerable<User> users = await _userDataAccess.GetAllAsync();
            IEnumerable<UserDto> usersDto = users.Select(user => _mapper.Map<UserDto>(user));
            return Ok(usersDto);
        }

        // GET api/<UserController>/1
        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> Get(string email)
        {
            if(email == null) { throw new ArgumentNullException("email"); }
            User user = await _userDataAccess.GetUserByEmailAsync(email);
            if (user == null) { return NotFound(); }
            UserDto userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        // DELETE api/<UserController>/1
        [HttpDelete("{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            if (await _userDataAccess.DeleteUserAsync(email))
                return Ok();
            else
                return NotFound();
        }

        //POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] UserDto newUserDto)
        {
            string email = await _userDataAccess.CreateUserAsync(_mapper.Map<User>(newUserDto));
            return Ok(email);
        }

        // PUT api/<OrderController>/1
        [HttpPut("{email}")]
        public async Task<ActionResult> Put([FromBody] UserDto updatedUserDto)
        {
            if (await _userDataAccess.UpdateUserAsync(_mapper.Map<User>(updatedUserDto)))
                return Ok();
            else
                return NotFound();
        }

        #endregion

    }
}
