using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentMVC.DAL.Models.Departments;
using AssignmentMVC.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;

namespace AssignmentMVC.DAL.Data.Configurations
{
    public class EmployeeConfiguration : BaseConfigurations<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)");
            builder.Property(e => e.Address).HasColumnType("varchar(150)");
            builder.Property(e => e.Salary).HasColumnType("decimal(10,2)");

            builder.Property(E=> E.Gender).
                HasConversion((empGender)=>empGender.ToString(),
                (reternedGender)=> (Gender) Enum.Parse(typeof(Gender), reternedGender));

            builder.Property(E => E.EmployeeType).
                HasConversion((empType) => empType.ToString(),
                (reternedType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), reternedType));
            base.Configure(builder);
        }
    }
}
