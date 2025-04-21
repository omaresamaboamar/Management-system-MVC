using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AssignmentMVC.BLL.DTO
{
    public class CreateDepartmentDto
    {
        
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }

        public DateOnly DateOfCreation { get; set; }
    }
}
