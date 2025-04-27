using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.BLL.DTO.EmployeesDTO;
using AssignmentMVC.BLL.Service.ServicesInterface;
using AssignmentMVC.DAL.Data.Repositries.Interfacies;
using AssignmentMVC.DAL.Models.Employees;
using AutoMapper;

namespace AssignmentMVC.BLL.Service.ServiceClasses
{
    public class EmployeeService(IUnitOfwork unitOfwork ,IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking )
        {
            var employees = unitOfwork.EmployeeRepository.GetAll(WithTracking);
            //var returnEmpToDto = employees.Select(e => new EmployeeDto()
            //{
            //    Id = e.Id,
            //    Name = e.Name,
            //    Email = e.Email,
            //    Age = e.Age,
            //    Salary = e.Salary,
            //    IsActive = e.IsActive,
            //    EmployeeType = e.EmployeeType.ToString(),
            //    Gender  = e.Gender.ToString(),
            //});
            //return returnEmpToDto;
            var returnEmpToDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(employees);
            return returnEmpToDto;
        }
        public IEnumerable<EmployeeDto> SearchEmployeeByName(string name)
        {
            var employees = unitOfwork.EmployeeRepository.GetEmployeeByName(name.ToLower());
            //var returnEmpToDto = employees.Select(e => new EmployeeDto()
            //{
            //    Id = e.Id,
            //    Name = e.Name,
            //    Email = e.Email,
            //    Age = e.Age,
            //    Salary = e.Salary,
            //    IsActive = e.IsActive,
            //    EmployeeType = e.EmployeeType.ToString(),
            //    Gender  = e.Gender.ToString(),
            //});
            //return returnEmpToDto;
            var returnEmpToDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return returnEmpToDto;
        }
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = unitOfwork.EmployeeRepository.GetByID(id);
            //if (employee == null) return null;
            //else
            //{
            //    var returnToEmpdetailsDto = new EmployeeDetailsDto()
            //    {
            //        Id = employee.Id,
            //        Name = employee.Name,
            //        Email = employee.Email,
            //        Age = employee.Age,
            //        Salary = employee.Salary,
            //        IsActive = employee.IsActive,
            //        EmployeeType = employee.EmployeeType.ToString(),
            //        Gender = employee.Gender.ToString(),
            //        //Address = employee.Address,
            //        PhoneNumber = employee.PhoneNumber,
            //        HiringDate = DateOnly.FromDateTime(employee.HiringDate),
            //        CreatedBy = 1,
            //        LastModifiedBy = 1,
            //    };
            //    return returnToEmpdetailsDto;
            //}
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        }
        public int CreateEmployee(CreateEmployeeDto employee)
        {
            var Employee = _mapper.Map<CreateEmployeeDto, Employee>(employee);
             unitOfwork.EmployeeRepository.Add(Employee);
            return unitOfwork.saveChanges();
        }
        public int UpdateEmployee(UpdateEmployeeDto employee)
        {
            unitOfwork.EmployeeRepository.Update(_mapper.Map<UpdateEmployeeDto , Employee>(employee));
            return unitOfwork.saveChanges();
        }
        public bool DeleteEmployee(int id)
        {
            var employee = unitOfwork.EmployeeRepository.GetByID(id);
            if (employee == null) return false;
            else
            {
                employee.IsDeleted = true;
                unitOfwork.EmployeeRepository.Update(employee) ;
                return unitOfwork.saveChanges() > 0 ? true : false;
            }
        }

        
    }
}
