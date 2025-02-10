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

        public UserController(IUserService userService, IEmployeeService employeeService, IMediaService mediaService)
        {
            _userService = userService;
            _mediaService = mediaService;
            _employeeService = employeeService;
        }

        [Authorize(Policy = "AdminOrUser")]
        [HttpPost("add-employee")]
        public async Task<IActionResult> CreateEmployee([FromForm] int userId, EmployeeDto employee)
        {
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
        [HttpPost("update-employee")]
        public async Task<IActionResult> UpdateEmployee([FromForm] int userId, UpdateEmployeeDto employee)
        {
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

    }
}
