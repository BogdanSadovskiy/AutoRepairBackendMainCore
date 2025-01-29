using AutoRepairMainCore.DTO.Models;
using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairMainCore.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MediaController : Controller
    {
        private ITokenValidationService _tokenValidationService;
        private IMediaService _mediaService;
        private IUserService _userService;

        public MediaController(ITokenValidationService tokenValidationService,
            IMediaService mediaService, IUserService userService)
        {
            _tokenValidationService = tokenValidationService;
            _mediaService = mediaService;
            _userService = userService;
        }

        [Authorize(policy: "AdminOrUser")]
        [HttpPost("upload-logo")]
        public async Task<IActionResult> UploadLogo([FromForm] IFormFile? logoFile)
        {
            string token = Request.Headers["Authorization"].ToString();

            AutoService autoservice = await _userService.GetAutoServiceById
                (_tokenValidationService.GetAutoServiceIdFromToken(token));

            _mediaService.SaveAutoServiceLogo(autoservice, logoFile);

            return Ok("Your logo successfuly saved");
        }
    }
}
