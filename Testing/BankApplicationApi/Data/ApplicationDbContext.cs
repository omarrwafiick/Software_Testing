using Microsoft.EntityFrameworkCore; 
using UnitTestingProj.Bank.Entities; 

namespace BankApplicationApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { 
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<SavingAccount> SavingAccounts { get; set; } 
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber)
                .OnDelete(DeleteBehavior.Cascade); 
        } 
    }
}
