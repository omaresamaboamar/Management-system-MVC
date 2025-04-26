using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Data.Repositries.Interfacies;
using AssignmentMVC.DAL.Models.Employees;

namespace AssignmentMVC.DAL.Data.Repositries.Classes
{
    public class EmployeeRepository(AppDbContext dbContext) : GenericRepository<Employee>(dbContext), IEmployeeRepository
    {

    }
}
