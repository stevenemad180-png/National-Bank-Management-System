
using Microsoft.EntityFrameworkCore;

namespace National_Bank_Management_System
{
    public class AppDbContext : DbContext
    {
        // Tables
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountCustomer> AccountCustomers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        // Database Connection
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.\SQLEXPRESS;Database=NationalBankDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        // Apply Fluent API Configurations
        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly
            );
        }
    }
}
