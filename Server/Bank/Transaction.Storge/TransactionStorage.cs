
using Exceptions;
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
    public async Task<Storage.Entites.Transaction> createTransaction(Entites.Transaction transaction)
    {
        using var _TransactionDbContext = _factory.CreateDbContext();
        {
            try
            {
                var newTransaction = (await _TransactionDbContext.Transactions.AddAsync(transaction)).Entity;
                await _TransactionDbContext.SaveChangesAsync();
                return newTransaction;
            }
            catch
            {
                throw new Exception();
            }
        }
    }

    public async Task<bool> updateTransaction(Entites.Transaction transaction)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                Entites.Transaction newTransaction = await _BankDbContext.Transactions.FirstOrDefaultAsync(account => account.ID == transaction.ID);
                newTransaction.Status = transaction.Status;
                if(transaction.Status == 2)
                {
                    newTransaction.FailureReason = transaction.FailureReason;
                }
                await _BankDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new DbContextException();
            }
        }
    }
}