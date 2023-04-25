using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;

namespace Ostral.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginWithEmailAndPassword([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ResponseDTO<object>.Fail(errors, (int) HttpStatusCode.BadRequest));
            }

            var response = await _identityService.LoginWithEmailAndPassword(model);

            if (!response.Success)
                return BadRequest(
                    ResponseDTO<object>.Fail(response.Errors, (int) HttpStatusCode.BadRequest));

            return Ok(ResponseDTO<object>.Success(response.Data!));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterDTO register)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ResponseDTO<object>.Fail(errors, (int) HttpStatusCode.BadRequest));
            }

            var response = await _identityService.RegisterStudent(register);

            if (!response.Success)
                return BadRequest(
                    ResponseDTO<object>.Fail(response.Errors, (int) HttpStatusCode.BadRequest));

            return Ok(ResponseDTO<object>.Success(response.Data!, "", (int) HttpStatusCode.Created));
        }

        [HttpPost("register-tutor")]
        public async Task<IActionResult> RegisterTutor([FromForm] RegisterDTO register)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                return BadRequest(ResponseDTO<object>.Fail(errors, (int) HttpStatusCode.BadRequest));
            }

            var response = await _identityService.RegisterTutor(register);

            if (!response.Success)
                return BadRequest(
                    ResponseDTO<object>.Fail(response.Errors, (int) HttpStatusCode.BadRequest));

            return Ok(ResponseDTO<object>.Success(response.Data!, "", (int) HttpStatusCode.Created));
        }
    }
}