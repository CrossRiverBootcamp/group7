
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
    public async Task<bool> createNewAccount(Entites.Transaction transaction)
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

    public async Task<bool> cheackAcountExist(string email)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
               if(await _BankDbContext.Customers.FirstOrDefaultAsync(customer => customer.Email== email) ==null )
                {
                    return false;
                }
                return true;

            }
            catch
            {
                throw new DbContextException();
            }
        }
    }

    public async Task<Entites.Transaction> getAccountCustomerInfo(int accountID)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                var account = await _BankDbContext.Accounts.Where(account => account.ID == accountID).Include(A=>A.Customer).FirstOrDefaultAsync();
                if (account != null)
                {
                    return account;
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
            catch
            {
                throw new DbContextException();
            }
        }
    }
}



   

