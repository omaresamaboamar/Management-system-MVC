using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Models.Employees;

namespace AssignmentMVC.DAL.Data.Repositries.Interfacies
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeeByName(string name);
    }
}
