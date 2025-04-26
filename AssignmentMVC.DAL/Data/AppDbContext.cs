using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Data.Configurations;
using AssignmentMVC.DAL.Models.Departments;
using AssignmentMVC.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = . ; DataBase = MCV03 ; Trusted_Connection = True ; ");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // If configuration In the Same Project
        }

       public DbSet<Department> Departments { get; set; }
       public DbSet<Employee> Employee { get; set; }
    }
}
