
using Microsoft.EntityFrameworkCore;

namespace Transaction.Storage.Entites;

public class TransactionDbContext : DbContext
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {

    }
    public DbSet<Transaction> Transactions { get; set; }
}
