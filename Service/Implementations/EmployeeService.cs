using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Exceptions.AutoServiceExceptions;
using AutoRepairMainCore.Infrastructure;

namespace AutoRepairMainCore.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly MySqlContext _context;

        public EmployeeService(MySqlContext context)
        {
            _context = context;
        }

        public Employee FindEmployeeByName(int? autoServiceId, string employeeName)
        {
            if (autoServiceId == null || string.IsNullOrEmpty(employeeName))
            {
                throw new InvalidParameterException("Not all parameters are valid");
            }
            return _context.employees.FirstOrDefault(e => e.AutoServiceId == autoServiceId && e.EmployeeName == employeeName);
        }

        public Employee FindEmployeeById(int? autoServiceId, int? employeeId)
        {
            if (autoServiceId == null || employeeId == null)
            {
                throw new InvalidParameterException("Not all parameters are valid");
            }
            return _context.employees.FirstOrDefault(e => e.AutoServiceId == autoServiceId && e.Id == employeeId);
        }

        public Employee CreateEmployee(AutoService autoService, EmployeeDto employeeDto)
        {
            Employee existingEmployee = FindEmployeeByName(autoService.Id, employeeDto.Name);

            if (existingEmployee != null)
            {
                return (ReactivateEmployee(existingEmployee));
            }

            var newEmployee = new Employee
            {
                AutoServiceId = autoService.Id,
                EmployeeName = employeeDto.Name,
                CurrentlyWorking = true
            };

            _context.employees.Add(newEmployee);
            _context.SaveChanges();
            return newEmployee;
        }

        public EmployeeFrontendDto CreateEmployeeForFrontend(Employee employee)
        {
            return new EmployeeFrontendDto()
            {
                Id = employee.Id,
                Name = employee.EmployeeName,
                IsCurrentlyWorks = employee.CurrentlyWorking,
                PhotoPath = employee.EmployeePhotoFilePath
            };
        }

        private Employee ReactivateEmployee(Employee employee)
        {
            if (employee.CurrentlyWorking)
            {
                throw new InvalidParameterException($"The employee {employee.EmployeeName} is already working.");
            }

            employee.CurrentlyWorking = true;
            _context.employees.Update(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee UpdateEmployee(Employee employee, string? newName = null,
            string? newPhotoFilePath = null, bool? isWorking = null)
        {
            bool isSomethingChanged = false;
            isSomethingChanged |= UpdateEmployeeName(employee, newName);
            isSomethingChanged |= UpdateEmployeeWorkingStatus(employee, isWorking);
            isSomethingChanged |= UpdateEmployeePhotoFilePath(employee, newPhotoFilePath);

            if (!isSomethingChanged)
            {
                throw new InvalidParameterException("Nothing to update");
            }

            _context.employees.Update(employee);
            _context.SaveChanges();
            return employee;
        }

        private bool UpdateEmployeeName(Employee employee, string? newName)
        {
            if (!string.IsNullOrWhiteSpace(newName) && employee.EmployeeName != newName)
            {
                employee.EmployeeName = newName;
                return true;
            }
            return false;
        }

        private bool UpdateEmployeePhotoFilePath(Employee employee, string? path)
        {
            if (!string.IsNullOrWhiteSpace(path) && employee.EmployeePhotoFilePath != path)
            {
                employee.EmployeePhotoFilePath = path;
                return true;
            }
            return false;
        }

        private bool UpdateEmployeeWorkingStatus(Employee employee, bool? status = null)
        {
            if (status.HasValue && employee.CurrentlyWorking != status)
            {
                employee.CurrentlyWorking = (bool)status;
                return true;
            }
            return false;
        }
    }
}
