using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Models;
using AssignmentMVC.DAL.Models.Departments;

namespace AssignmentMVC.DAL.Data.Repositries.Interfacies
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        // Get All 
        IEnumerable<TEntity> GetAll(bool withtracking = false);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>>predicate);
        // GetById 
        TEntity GetByID(int id);
        void Update(TEntity Entity);
        void Delete(TEntity Entity);
        void Add(TEntity Entity);
    }
}
