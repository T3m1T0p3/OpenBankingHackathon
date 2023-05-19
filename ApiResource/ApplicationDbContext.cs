using ApiResource.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiResource
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Debit> Debits { get; set; }
        //
        //public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BankCustomer> BankCustomers { get; set; }
        public DbSet<AccountBalance> AccountBalances { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<BankCustomer>().HasMany<BankCustomer>();
            //modelBuilder.Entity<AccountBalance>().HasNoKey();
        }
    }
}
