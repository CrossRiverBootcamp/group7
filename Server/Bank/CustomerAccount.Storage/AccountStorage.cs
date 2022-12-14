using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Storage;

public class AccountStorage : IAccountStorage
{
    private readonly IDbContextFactory<CustomerAccountDbContext> _factory;

    public AccountStorage(IDbContextFactory<CustomerAccountDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<bool> createNewAccount(Account account, Customer customer)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                account.Customer = (await _BankDbContext.Customers.AddAsync(customer)).Entity;
                await _BankDbContext.Accounts.AddAsync(account);
                await _BankDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new DbContextException();
            }
        }
    }

  
    public async Task<Account> getAccountCustomerInfo(int accountID)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                var account = await _BankDbContext.Accounts.Where(account => account.ID == accountID)
                    .Include(customer=> customer.Customer).FirstOrDefaultAsync();
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

    public async Task<bool> accountExist(int accountID)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                if (await _BankDbContext.Accounts.FirstOrDefaultAsync(account => account.ID == accountID) == null)
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

    public async Task<bool> balanceCheacking( float ammount, int accountID)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                var account = await _BankDbContext.Accounts.FirstOrDefaultAsync(account => account.ID == accountID);
                if (account.Balance < ammount)
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

    public async Task<BalanceObject> updateBalance(float ammount, int fromAccountID, int toAccountID)   
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                var fromAccount = await _BankDbContext.Accounts.FirstOrDefaultAsync(account => account.ID == fromAccountID);
                fromAccount.Balance -= ammount;
                var toAccount = await _BankDbContext.Accounts.FirstOrDefaultAsync(account => account.ID == toAccountID);
                toAccount.Balance += ammount;
                BalanceObject balance = new BalanceObject()
                {
                    fromBalance = fromAccount.Balance,
                    toBalance = toAccount.Balance,

                };
                await _BankDbContext.SaveChangesAsync();
                return balance;

            }
            catch
            {
                throw new DbContextException();
            }
        }
    }

    public async Task<int> findAccountByCustomerID(int customerID)
    {
        using var context = _factory.CreateDbContext();
        {
            try
            {
                Account account = await context.Accounts.FirstOrDefaultAsync(account => account.CustomerID == customerID);
                if (account != null)
                {
                    return account.ID; ;
                }
                else
                {
                    throw new ArgumentNullException("account");
                }
            }
            catch
            {
                throw new DbContextException();
            }
        }
    }
}




   

