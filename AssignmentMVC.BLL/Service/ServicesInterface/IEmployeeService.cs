using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.BLL.DTO.EmployeesDTO;

namespace AssignmentMVC.BLL.Service.ServicesInterface
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName,bool WithTracking = false);
        //IEnumerable<EmployeeDto> SearchEmployeeByName(string name);
        EmployeeDetailsDto? GetEmployeeById(int id);

        int CreateEmployee(CreateEmployeeDto employee);
        int UpdateEmployee(UpdateEmployeeDto employee);
        bool DeleteEmployee(int id);

    }
}
