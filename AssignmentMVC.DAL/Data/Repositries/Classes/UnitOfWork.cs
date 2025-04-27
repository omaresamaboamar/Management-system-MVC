using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Data.Repositries.Interfacies;

namespace AssignmentMVC.DAL.Data.Repositries.Classes
{
    public class UnitOfWork : IUnitOfwork
    {
        private Lazy<IEmployeeRepository> _EmployeeRepository;
        private Lazy<IDepartmentRepository> _DepartmentRepository;
        private readonly AppDbContext _dbCotext;

        public UnitOfWork(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository , AppDbContext dbCotext)
        {
            _EmployeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbCotext));
            _DepartmentRepository = new Lazy<IDepartmentRepository>(()=> new DepartmentRepository(dbCotext));
            _dbCotext = dbCotext;
        }
        public IEmployeeRepository EmployeeRepository
        {

            get
            {
                return _EmployeeRepository.Value;
            }

           

        }
        public IDepartmentRepository DepartmentRepository
        {

            get
            {
                return _DepartmentRepository.Value;
            }

            

        }
        

        public int saveChanges()
        {
            return _dbCotext.SaveChanges();
        }
    }
}
