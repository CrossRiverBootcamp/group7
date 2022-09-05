
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Storage;

public class CustomerStorage : ICustomerStorage
{
    private readonly IDbContextFactory<BankDbContext> _factory;
    public CustomerStorage(IDbContextFactory<BankDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<int> login(string email, string password)
    {

        using var context = _factory.CreateDbContext();
        {
            try
            {
                Customer customer = await context.Customers.FirstOrDefaultAsync(customer => customer.Email == email && customer.Password == password);
                if (customer != null)
                {
                    return await findAccountByCustomerID(customer.ID);
                }
                else
                {
                    throw new ArgumentNullException("customer");
                }
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
                if(account != null)
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
