using System;
using System.Collections.Generic;
using System.Linq;
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
        // GetById 
        TEntity GetByID(int id);
        int Update(TEntity Entity);
        int Delete(TEntity Entity);
        int Add(TEntity Entity);
    }
}
