
using CustomerAccount.Storage.Entites;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Storage.Entites;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    { }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("server=DESKTOP-QM3UF42; database=BankProject;Trusted_Connection=True;");
    //}

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }

}
