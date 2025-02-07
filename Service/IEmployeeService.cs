using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.Service
{
    public interface IEmployeeService
    {
        Employee FindEmployeeByName(int? autoServiceId, string employeeName);

        Employee FindEmployeeById(int? autoServiceId, int? employeeId);

        Employee CreateEmployee(AutoService autoservice, EmployeeDto employee);

        Employee UpdateEmployee(Employee employee, string? newName = null,
            string? newPhotoFilePath = null, bool? isWorking = null);

        EmployeeFrontendDto CreateEmployeeForFrontend(Employee employee);
    }
}
