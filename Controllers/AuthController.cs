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
        public async Task<IActionResult> Register([FromBody] AutoServiceAuthDto userService)
        {
            string result = await _authService.RegisterServiceAsync(userService);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AutoServiceAuthDto userService)
        {
            try
            {
                string token = await _authService.LoginServiceAsync(userService);
                return Ok(new { Token = token });
            }
            catch (Exception ex) 
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
