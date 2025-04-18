using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairMainCore.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IMediaService _mediaService;
        private IEmployeeService _employeeService;
        private ITokenValidationService _tokenValidationService;

        public UserController(IUserService userService, IEmployeeService employeeService, 
            IMediaService mediaService, ITokenValidationService tokenValidationService)
        {
            _userService = userService;
            _mediaService = mediaService;
            _employeeService = employeeService;
            _tokenValidationService = tokenValidationService;
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPost("add-employee")]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeDto employee)
        {
            string token = Request.Headers["Authorization"].ToString();
            int userId = _tokenValidationService.GetAutoServiceIdFromToken(token);
            AutoService autoService = await _userService.GetAutoServiceById(userId);
            Employee createdEmployee = _employeeService.CreateEmployee(autoService, employee);

            if (employee.Photo != null) //this check must be because of the method in media service will throw exception
            {
                string photoPath = await _mediaService.SaveEmployeePhoto(autoService, createdEmployee, employee.Photo);
                _employeeService.UpdateEmployee(createdEmployee, newPhotoFilePath: photoPath);
            }

            return Ok(_employeeService.CreateEmployeeForFrontend(createdEmployee));
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPut("update-employee")]
        public async Task<IActionResult> UpdateEmployee([FromForm] UpdateEmployeeDto employee)
        {
            string token = Request.Headers["Authorization"].ToString();
            int userId = _tokenValidationService.GetAutoServiceIdFromToken(token);
            Employee existingEmployee = _employeeService.FindEmployeeById(userId, employee.Id);
            string photoPath = "";

            if (employee.Photo != null)
            {
                AutoService autoService = await _userService.GetAutoServiceById(userId);
                photoPath = await _mediaService.SaveEmployeePhoto(autoService, existingEmployee, employee.Photo);
            }
            _employeeService.UpdateEmployee(existingEmployee, employee.Name, photoPath, employee.IsCurrentlyWorks);

            return Ok(_employeeService.CreateEmployeeForFrontend(existingEmployee));
        }

        //[HttpPost("add-client")]
        //public async Task<IActionResult> AddClient([FromBody] ClientDto client)
        //{

        //}

    }
}
