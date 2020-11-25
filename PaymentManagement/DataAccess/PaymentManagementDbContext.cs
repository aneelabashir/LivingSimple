using Microsoft.EntityFrameworkCore;
using PaymentManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManagement.DataAccess
{
    public class PaymentManagementDbContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=CMDLHRLT809;Initial Catalog=PaymentManagement;Persist Security Info=True;User ID=curemd;Password=Cure2000");
        }
    }
}
