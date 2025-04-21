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
        private readonly AppDbContext _dbContext = dbContext;

        public int Add(Department Entity)
        {
            _dbContext.Departments.Add(Entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department Entity)
        {
            _dbContext.Departments.Remove(Entity);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll(bool withtracking = false)
        {
            if (withtracking) 
            {
                return _dbContext.Departments.ToList();
            }
            else
            {
                return _dbContext.Departments.AsNoTracking().ToList();
            }
            
        }

        public Department GetByID(int id)
        {
            return _dbContext.Departments.Find(id);
        }

        public int Update(Department Entity)
        {
            _dbContext.Departments.Update(Entity);
            return _dbContext.SaveChanges();
        }
    }
}
