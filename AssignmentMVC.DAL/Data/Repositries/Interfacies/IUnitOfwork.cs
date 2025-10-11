using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentMVC.DAL.Data.Repositries.Interfacies
{
    public interface IUnitOfwork
    {
        public IEmployeeRepository EmployeeRepository { get;  }
        public IDepartmentRepository DepartmentRepository { get;  }
        int saveChanges();
    }
}
