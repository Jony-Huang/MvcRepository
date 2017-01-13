using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcRepository3.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MvcRepository3.DAL
{
    public class Context:DbContext
    {
        public Context() : base("MVCConnection") { }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<RoleMenu> RoleMenu { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer<Context>(new Initializer());
            //base.OnModelCreating(modelBuilder);
        }


    }
}