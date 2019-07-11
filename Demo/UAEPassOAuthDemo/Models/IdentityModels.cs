using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UAEPassOAuthDemo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public UAEPassOAuthCodeLibrary.UAEPassUser UAEPassUserDetails { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }

}