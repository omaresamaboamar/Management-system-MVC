using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentMVC.DAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public int LastModifiBy { get; set; }
        public DateTime LastModifiedOn  { get; set; }
        public bool IsDeleted { get; set; } 

    }
}
