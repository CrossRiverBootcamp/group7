
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Storage;

public class CustomerStorage : ICustomerStorage
{
    private readonly IDbContextFactory<CustomerAccountDbContext> _factory;
    IAccountStorage _accountStorage;
    public CustomerStorage(IDbContextFactory<CustomerAccountDbContext> factory, IAccountStorage accountStorage)
    {
        _factory = factory;
        _accountStorage = accountStorage;
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
                    return await _accountStorage.findAccountByCustomerID(customer.ID);
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

    public async Task<bool> emailExist(string email)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                if (await _BankDbContext.Customers.FirstOrDefaultAsync(customer => customer.Email == email) == null)
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

    
}
