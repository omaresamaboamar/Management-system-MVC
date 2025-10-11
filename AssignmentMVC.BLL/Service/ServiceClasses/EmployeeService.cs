using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.BLL.DTO.EmployeesDTO;
using AssignmentMVC.BLL.Service.AttachmentService;
using AssignmentMVC.BLL.Service.ServicesInterface;
using AssignmentMVC.DAL.Data.Repositries.Interfacies;
using AssignmentMVC.DAL.Models.Employees;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AssignmentMVC.BLL.Service.ServiceClasses
{
    public class EmployeeService(IUnitOfwork unitOfwork, IMapper _mapper, IAttachmentService _attachmentService) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName, bool WithTracking)
        {
            IEnumerable<Employee> employees;
            if (!String.IsNullOrWhiteSpace(EmployeeSearchName))
                employees = unitOfwork.EmployeeRepository.GetAll(e=> e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            else
            employees = unitOfwork.EmployeeRepository.GetAll(WithTracking);
            
            var returnEmpToDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return returnEmpToDto;
        }
        //public IEnumerable<EmployeeDto> SearchEmployeeByName(string name)
        //{
        //    var employees = unitOfwork.EmployeeRepository.GetEmployeeByName(name.ToLower());
        //    //var returnEmpToDto = employees.Select(e => new EmployeeDto()
        //    //{
        //    //    Id = e.Id,
        //    //    Name = e.Name,
        //    //    Email = e.Email,
        //    //    Age = e.Age,
        //    //    Salary = e.Salary,
        //    //    IsActive = e.IsActive,
        //    //    EmployeeType = e.EmployeeType.ToString(),
        //    //    Gender  = e.Gender.ToString(),
        //    //});
        //    //return returnEmpToDto;
        //    var returnEmpToDto = _mapper.Map<IQueryable<Employee>, IEnumerable<EmployeeDto>>(employees);
        //    return returnEmpToDto;
        //}
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
        public int CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var Employee = _mapper.Map<CreateEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null)
            {
                Employee.ImageName = _attachmentService.Uploud(employeeDto.Image, "images");
            }
            unitOfwork.EmployeeRepository.Add(Employee);
            return unitOfwork.saveChanges();
        }
        public int UpdateEmployee(UpdateEmployeeDto employee)
        {
            var Employee = _mapper.Map<UpdateEmployeeDto, Employee>(employee);
            if (employee.Image is not null)
            {
                Employee.ImageName = _attachmentService.Uploud(employee.Image, "images");
            }
            unitOfwork.EmployeeRepository.Update(Employee);
            return unitOfwork.saveChanges();
        }
        public bool DeleteEmployee(int id)
        {
            var employee = unitOfwork.EmployeeRepository.GetByID(id);
            if (employee == null) return false;
            else
            {

                employee.IsDeleted = true;
                _attachmentService.Delete(employee.ImageName, "images");
                employee.ImageName = null;
                unitOfwork.EmployeeRepository.Update(employee);
                unitOfwork.saveChanges();
                return true;
            }
        }


    }
}
