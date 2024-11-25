using AutoRepairMainCore.Models;
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
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authService.RegisterServiceAsync(model.ServiceName, model.Password);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _authService.LoginServiceAsync(model.ServiceName, model.Password);
            if (token.StartsWith("Invalid"))
                return Unauthorized(token);

            return Ok(new { Token = token });
        }
    }
}
