using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class ProjectManagementContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }

        public System.Data.Entity.DbSet<ProjectManagement.Models.Users> Users { get; set; }

        public System.Data.Entity.DbSet<ProjectManagement.Models.Projects> Projects { get; set; }

        public System.Data.Entity.DbSet<ProjectManagement.Models.Tasks> Tasks { get; set; }
    }
}
