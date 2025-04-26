using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Models;

namespace AssignmentMVC.BLL.DTO.DepartmentsDTO
{

    public class DepartmentDetailsDto
    {
        // constructor Method
        //public DepartmentDetailsDto(Department department)
        //{

        //Id = department.Id;
        //    Name = department.Name;
        //    Code = department.Code;
        //    Description = department.Description;
        //    createdOn = DateOnly.FromDateTime(department.createdOn);
        //}
        public int Id { get; set; }
        public DateOnly CreatedOn { get; set; }

        public int LastModifiedBy { get; set; }
        public int CreatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
