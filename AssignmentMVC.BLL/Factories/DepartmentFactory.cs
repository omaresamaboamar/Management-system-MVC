using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.BLL.DTO;
using AssignmentMVC.DAL.Models;

namespace AssignmentMVC.BLL.Factories
{
    public static class DepartmentFactory
    {
        public static DepartmentDto TodepartmentDto(this Department D)
        {
            return new DepartmentDto()
            {
                Id = D.Id,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            };
        }

        public static DepartmentDetailsDto TODepartmentDetailsDto(this Department department) 
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
            };
        }
        public static Department ToDepartment(this CreateDepartmentDto departmentDto)
        {
            return new Department()
            {
                
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly()),
                
            };
        }

        public static Department ToDepartment(this UpdateDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }
    }
}
