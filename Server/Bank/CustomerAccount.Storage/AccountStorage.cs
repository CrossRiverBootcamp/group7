using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
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
        using var context = _factory.CreateDbContext();
        {
            try
            {
                account.Customer = (await context.Customers.AddAsync(customer)).Entity;
                await context.Accounts.AddAsync(account);
                await context.SaveChangesAsync();
                return true;

            }
            catch
            {
                throw new Exception();
            }
        }
    }

    public async Task<bool> cheackAcountExist(string email)
    {
        using var context = _factory.CreateDbContext();
        {
            try
            {
               
               if(await context.Customers.FirstOrDefaultAsync(customer => customer.Email== email) ==null )
                {
                    return false;
                }
                return true;

            }
            catch
            {
                throw new Exception();
            }
        }
    }

    public async Task<Account> getAccountCustomerInfo(int accountID)
    {
        using var context = _factory.CreateDbContext();
        {
            try
            {
                var account = await context.Accounts.Where(account => account.ID == accountID).Include(customer=>customer.Customer).FirstOrDefaultAsync();
                if (account != null)
                {
                    return account;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch
            {
                throw new Exception();
            }
        }
    }
}



   

