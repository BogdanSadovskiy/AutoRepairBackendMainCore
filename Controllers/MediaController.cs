using AutoRepairMainCore.DTO.Models;
using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Service;
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

        [HttpPost("upload-logo")]
        public async Task<IActionResult> UploadLogo(
             [FromForm] string token,
             [FromForm] IFormFile logoFile)
        {
            _tokenValidationService.ValidateToken(token, RolesEnum.anyone);
            string autoserviceName = _tokenValidationService.GetAutoServiceNameFromToken(token);
            AutoService autoservice = await _userService.GetAutoServiceByName(autoserviceName);
            _mediaService.SaveAutoServiceLogo(autoservice, logoFile);

            return Ok("Your logo successfuly saved");
        }
    }
}
