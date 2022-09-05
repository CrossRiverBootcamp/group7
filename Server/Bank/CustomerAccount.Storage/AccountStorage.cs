using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Storage;

public class AccountStorage : IAccountStorage
{
    private readonly IDbContextFactory<BankDbContext> _factory;
    public AccountStorage(IDbContextFactory<BankDbContext> factory)
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
                throw new CreateUserException();
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
}



   

