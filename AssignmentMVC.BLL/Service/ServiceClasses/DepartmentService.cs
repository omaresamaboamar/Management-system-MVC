using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.BLL.DTO.DepartmentsDTO;
using AssignmentMVC.BLL.Factories;
using AssignmentMVC.BLL.Service.ServicesInterface;
using AssignmentMVC.DAL.Data.Repositries.Interfacies;
using AssignmentMVC.DAL.Models;
using AssignmentMVC.DAL.Models.Departments;

namespace AssignmentMVC.BLL.Service.ServiceClasses
{
    public class DepartmentService(IUnitOfwork unitOfwork) : IDepartmentService
    {
        private readonly IUnitOfwork _unitOfwork = unitOfwork;

        //private readonly IDepartmentRepositroy _departmentRepository = departmentRepositroy;

        public IEnumerable<DepartmentDto> GetAllDepartment(string? DepartmentSearchName)
        {
            IEnumerable<Department> department;
            if (!String.IsNullOrWhiteSpace(DepartmentSearchName))

            department = unitOfwork.DepartmentRepository.GetAll(d=> d.Name.ToLower().Contains(DepartmentSearchName.ToLower()));
            else
                department = unitOfwork.DepartmentRepository.GetAll();
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
            var department = _unitOfwork.DepartmentRepository.GetByID(id);
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
             unitOfwork.DepartmentRepository.Add(department);
            return _unitOfwork.saveChanges();
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
           unitOfwork.DepartmentRepository.Update(departmentDto.ToDepartment());
            return _unitOfwork.saveChanges();
        }

        public bool DeleteDepartment(int id)
        {
            var department = unitOfwork.DepartmentRepository.GetByID(id);
            if (department is null) return false;
            else
            {
                unitOfwork.DepartmentRepository.Delete(department);
                return _unitOfwork.saveChanges() > 0 ? true : false;
            }
        }

      
    }
}
