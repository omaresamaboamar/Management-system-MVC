using AssignmentMVC.BLL.DTO.DepartmentsDTO;

namespace AssignmentMVC.BLL.Service.ServicesInterface
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