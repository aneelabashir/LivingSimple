using AccountManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.DataAccess
{
    public class AccountManagementDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=CMDLHRLT809;Initial Catalog=AccountManagement;Persist Security Info=True;User ID=curemd;Password=Cure2000");
        }
    }
}
