using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Models;

namespace AssignmentMVC.DAL.Data.Repositries.Interfacies
{
    public interface IDepartmentRepositroy
    {
        // Get All 
        IEnumerable<Department> GetAll(bool withtracking = false);

        // GetById 

        Department GetByID(int id);

        int Update(Department Entity);

        int Delete(Department Entity);

        int Add(Department Entity);

    }
}
