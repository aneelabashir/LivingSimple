using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AccountManagementEventHandler.Model
{
    public partial class AccountManagementContext : DbContext
    {
        //Scaffold-dbcontext "Data Source=CMDLHRLT809;Initial Catalog=AccountManagement;Persist Security Info=True;User ID=curemd;Password=Cure2000" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model
        public AccountManagementContext()
        {
        }

        public AccountManagementContext(DbContextOptions<AccountManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=CMDLHRLT809;Initial Catalog=AccountManagement;Persist Security Info=True;User ID=curemd;Password=Cure2000");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
