using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Data.Repositries.Interfacies;
using AssignmentMVC.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.DAL.Data.Repositries.Classes
{
    public class DepartmentRepositroy(AppDbContext dbContext) : IDepartmentRepositroy
    {
        private readonly AppDbContext _DbContext = dbContext;

        public int Add(Department Entity)
        {
            _DbContext.Departments.Add(Entity);
            return _DbContext.SaveChanges();
        }

        public int Delete(Department Entity)
        {
            _DbContext.Departments.Remove(Entity);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll(bool withtracking = false)
        {
            if (withtracking) 
            {
                return _DbContext.Departments.ToList();
            }
            else
            {
                return _DbContext.Departments.AsNoTracking().ToList();
            }
            
        }

        public Department GetByID(int id)
        {
            return _DbContext.Departments.Find(id);
        }

        public int Update(Department Entity)
        {
            _DbContext.Departments.Update(Entity);
            return _DbContext.SaveChanges();
        }
    }
}
