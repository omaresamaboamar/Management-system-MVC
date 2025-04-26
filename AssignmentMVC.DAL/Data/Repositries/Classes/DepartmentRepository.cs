using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Data.Repositries.Interfacies;
using AssignmentMVC.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.DAL.Data.Repositries.Classes
{
    public class DepartmentRepository(AppDbContext dbContext) : GenericRepository<Department>(dbContext) ,IDepartmentRepository
    {
    }
}
