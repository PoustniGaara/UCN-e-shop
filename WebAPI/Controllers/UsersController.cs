using AutoMapper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using WebApi.ActionFilters;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Properties and Constructor
        private readonly IUserDataAccess _userDataAccess;
        private readonly IMapper _mapper;

        public UsersController(IUserDataAccess userDataAccess, IMapper mapper)
        {
            _userDataAccess = userDataAccess;
            _mapper = mapper;
        }
        #endregion


        #region Methods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            IEnumerable<User> users = await _userDataAccess.GetAllAsync();
            if(users == null) { return NotFound(); }
            IEnumerable<UserDto> usersDto = users.Select(user => _mapper.Map<UserDto>(user));
            return Ok(usersDto);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> Get(string email)
        {
            User user = await _userDataAccess.GetByEmailAsync(email);
            if (user == null) { return NotFound(); }
            UserDto userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult<bool>> Delete(string email)
        {
            if (await _userDataAccess.DeleteAsync(email)) { return Ok(); }
            return NotFound();
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<ActionResult<string>> Post([FromBody] UserDto newUserDto)
        {
            User user = _mapper.Map<User>(newUserDto);
            string email = await _userDataAccess.CreateAsync(user);
            if(email == null || email.Equals("")) { return BadRequest(); }
            return Ok(email);
        }

        [HttpPut("{email}")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<ActionResult> Put([FromBody] UserDto updatedUserDto)
        {
            if (await _userDataAccess.UpdateAsync(_mapper.Map<User>(updatedUserDto))) { return Ok(); }
            return NotFound();
        }

        [HttpPut("{email}/Password")]
        public async Task<ActionResult> UpdatePassword(string email, [FromBody] UserDto passwordUpdateInfo)
        {
            if (await _userDataAccess.UpdatePasswordAsync(email, passwordUpdateInfo.Password, passwordUpdateInfo.NewPassword))
            { return Ok(true); }
             return NotFound(false); 
        }

        #endregion

    }
}
