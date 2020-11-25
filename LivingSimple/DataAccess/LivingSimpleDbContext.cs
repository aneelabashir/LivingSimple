using LivingSimple.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivingSimple.DataAccess
{
    public class LivingSimpleDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=CMDLHRLT809;Initial Catalog=LivingSimple;Persist Security Info=True;User ID=curemd;Password=Cure2000");
        }
    }
}
