

using CustomerAccount.Storage.Entites;

namespace CustomerAccount.Storage.Interfaces;

public interface IAccountStorage
{
    public Task<bool> createNewAccount(Account  account, Customer customer);
    public Task<bool> cheackAcountExist(string email);

    public Task<Account> getAccountCustomerInfo(int accountID);


}
