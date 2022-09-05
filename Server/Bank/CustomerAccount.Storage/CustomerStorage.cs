
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Storage;

public class CustomerStorage : ICustomerStorage
{
    public async Task<int> login(string email, string password)
    {
        using (var _BankDbContext = new BankDbContext())
        {
            try
            {
                Customer customer = await _BankDbContext.Customers.FirstOrDefaultAsync(customer => customer.Email == email && customer.Password == password);
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
                throw new Exception();
            }
        }
    }



    public async Task<int> findAccountByCustomerID(int customerID)
    {
        using (var _BankDbContext = new BankDbContext())
        {
            try
            {
                Account account = await _BankDbContext.Accounts.FirstOrDefaultAsync(account => account.CustomerID == customerID);
                if(account != null)
                {
                    return account.ID; ;
                }
                else
                {
                    throw new ArgumentNullException("account");
                }

            }
            catch(Exception)
            {
                throw new Exception("internal error");
            }
        }
    }
}
