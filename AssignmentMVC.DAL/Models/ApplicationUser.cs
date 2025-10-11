using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AssignmentMVC.DAL.Models
{
    public class ApplicationUser :IdentityUser
    {
        public bool IsAgree{ get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
    }
}
