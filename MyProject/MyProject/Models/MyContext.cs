using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MyContext : DbContext
    {
        public MyContext() : base("WebAppCon") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}