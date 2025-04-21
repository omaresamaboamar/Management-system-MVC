using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.BLL.DTO;
using AssignmentMVC.BLL.Factories;
using AssignmentMVC.DAL.Data.Repositries.Interfacies;
using AssignmentMVC.DAL.Models;

namespace AssignmentMVC.BLL.Service
{
    public class DepartmentService(IDepartmentRepositroy _departmentRepositroy) : IDepartmentService
    {
        //private readonly IDepartmentRepositroy _departmentRepositroy = departmentRepositroy;

        public IEnumerable<DepartmentDto> GetAllDepartment()
        {
            var department = _departmentRepositroy.GetAll();
            // 1.manaul mapping
            //var departmentToReturn = department.Select(D => new DepartmentDto()
            //{
            //    Id = D.Id,
            //    Name = D.Name,
            //    Code = D.Code,
            //    Description = D.Description,
            //    DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            //});
            // return departmentToReturn;

            // 2.Extention Method
            return department.Select(D => D.TodepartmentDto());
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepositroy.GetByID(id);
            //if (department is null) return null;
            //else
            //{
            //    var departmentToReturn = new DepartmentDto()
            //    {
            //        Id = department.Id,
            //        Name = department.Name,
            //        Code = department.Code,
            //        Description = department.Description,
            //        CreatedOn = DateOnly.FromDateTime(department.CreatedOn)
            //    }; return departmentToReturn;
            //}

            // 1. manaul mapping 
            //return department is null ? null : new DepartmentDetailsDto()
            //{
            //    Id = id,
            //    Name = department.Name,
            //    Code = department.Code,
            //    Description = department.Description,
            //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn)
            //};
            // 2 . Extention Method Mapping
            return department is null ? null : department.TODepartmentDetailsDto();
        }

        public int AddDepartment(CreateDepartmentDto departmentdto)
        {
            var department = departmentdto.ToDepartment();
            return _departmentRepositroy.Add(department);
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            return _departmentRepositroy.Update(departmentDto.ToDepartment());
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepositroy.GetByID(id);
            if (department is null) return false;
            else
            {
                int resulte = _departmentRepositroy.Delete(department);
                return resulte > 0 ? true : false;
            }
        }
    }
}
