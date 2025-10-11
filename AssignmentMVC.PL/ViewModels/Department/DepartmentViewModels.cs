using System.ComponentModel.DataAnnotations;

namespace AssignmentMVC.PL.ViewModels.Department
{
    public class DepartmentViewModels
    {
        [Required(ErrorMessage = "Name is Required!!")]
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }

        public DateOnly DateOfCreation { get; set; }
    }
}
