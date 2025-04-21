using AssignmentMVC.BLL.DTO;

namespace AssignmentMVC.BLL.Service
{
    public interface IDepartmentService
    {
        int AddDepartment(CreateDepartmentDto departmentdto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartment();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
    }
}