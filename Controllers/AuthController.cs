using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Application.Services;
using practicing.Domain.Dtos;

namespace Crud.Controllers
{
    [Route("api/[controller]")]// configuration= gets data from the appsettings.json file and uses it to
                               // determine the route for this controller. The [controller] placeholder
                               // will be replaced with the name of the controller, which in this case is
                               // "Auth". So, the route for this controller will be "api/auth".
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.Username))
            {
                return BadRequest("All fields are required");
            }

            var result = await _authService.Register(dto);

            if (result.UserId == 0)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("Email and password are required");
            }

            var result = await _authService.Login(dto);

            if (result.UserId == 0)
            {
                return Unauthorized(result.Message);
            }

            return Ok(result);
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Auth API is working!");
        }
    }
}
