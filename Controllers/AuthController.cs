using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairMainCore.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] MyServiceRegistrationDto userService)
        {
            string result = await _authService.RegisterServiceAsync(userService.ServiceName, userService.Password);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] MyServiceLoginDto userService)
        {
            string token = await _authService.LoginServiceAsync(userService.ServiceName, userService.Password);
            if (token.StartsWith("Invalid"))
                return Unauthorized(token);

            return Ok(new { Token = token });
        }
    }
}
