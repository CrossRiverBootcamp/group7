

using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Storage.Entites;

public class CustomerAccountDbContext : DbContext
{
    public CustomerAccountDbContext(DbContextOptions<CustomerAccountDbContext> options) : base(options)
    {

    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }

}
