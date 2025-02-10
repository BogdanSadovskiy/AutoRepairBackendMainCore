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
        private IMediaService _mediaService;
        private IUserService _userService;

        public MediaController(IMediaService mediaService, IUserService userService)
        {
            _mediaService = mediaService;
            _userService = userService;
        }

        [Authorize(policy: "AdminOrUser")]
        [HttpPost("upload-logo")]
        public async Task<IActionResult> UploadLogo([FromForm] int userId, IFormFile? logoFile)
        {
            AutoService autoservice = await _userService.GetAutoServiceById(userId);

            _mediaService.SaveAutoServiceLogo(autoservice, logoFile);

            return Ok("Your logo successfuly saved");
        }
    }
}
