using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Data.Repositries.Interfacies;
using AssignmentMVC.DAL.Models;
using AssignmentMVC.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AssignmentMVC.DAL.Data.Repositries.Classes
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public void Add(TEntity Entity)
        {
            _dbContext.Set<TEntity>().Add(Entity);
        }

        public void Delete(TEntity Entity)
        {
            _dbContext.Set<TEntity>().Remove(Entity);
        }

        public IEnumerable<TEntity> GetAll(bool withtracking = false)
        {
            if (withtracking)
            {
                return _dbContext.Set<TEntity>().Where(e=>e.IsDeleted != true).ToList();
            }
            else
            {
                return _dbContext.Set<TEntity>().Where(e=>e.IsDeleted!= true).AsNoTracking().ToList();
            }

        }

        public TEntity GetByID(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Update(TEntity Entity)
        {
            _dbContext.Set<TEntity>().Update(Entity);
        }
    }
}
