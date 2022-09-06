
using Microsoft.EntityFrameworkCore;
using Transaction.Storage.Entites;
using Transaction.Storage.Interfaces;

namespace Transaction.Storage;

public class TransactionStorage : ITransactionStorage
{
    private readonly IDbContextFactory<TransactionDbContext> _factory;
    public TransactionStorage(IDbContextFactory<TransactionDbContext> factory)
    {
        _factory = factory;
    }
    public async Task<bool> createTransaction(Entites.Transaction transaction)
    {
        using var _TransactionDbContext = _factory.CreateDbContext();
        {
            try
            {
                await _TransactionDbContext.Transactions.AddAsync(transaction);
                await _TransactionDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}



   

