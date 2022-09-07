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
                return false;
            }
        }
    }

    public async Task<bool> emailExist(string email)
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

    public async Task<Account> getAccountCustomerInfo(int accountID)
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

  

    public async Task<bool> updateBalance(float ammount, int fromAccountID, int toAccountID)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                var fromAccount = await _BankDbContext.Accounts.FirstOrDefaultAsync(account => account.ID == fromAccountID);
                fromAccount.Balance-=ammount;
                var toAccount = await _BankDbContext.Accounts.FirstOrDefaultAsync(account => account.ID == toAccountID);
                toAccount.Balance+= ammount;
                _BankDbContext.Entry(family).CurrentValues.SetValues(family1);

                _BankDbContext.SaveChangesAsync();
                return true;

            }
            catch
            {
                throw new DbContextException();
            }
        }
    }
}




   

