using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserProfile(string email)
        {
            var response = await _userService.GetUserByEmail(email);

            if (response.Success)
                return Ok(ResponseDTO<object>.Success(response.Data!));

            return BadRequest(ResponseDTO<object>.Fail(response.Errors));  
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserDTO updateUserDTO)
        {
            var response = await _userService.UpdateUserProfile(updateUserDTO);

            if (response.Success)
                return Ok(ResponseDTO<object>.Success(response.Data!));

            return BadRequest(ResponseDTO<object>.Fail(response.Errors));
        }
    }
}
