

using CustomerAccount.Storage.Entites;

namespace CustomerAccount.Storage.Interfaces;

public interface IAccountStorage
{
    public Task<bool> createNewAccount(Account  account, Customer customer);
    public Task<bool> emailExist(string email);

    public Task<Account> getAccountCustomerInfo(int accountID);
    public Task<bool> accountExist(int accountID);
    public Task<bool> balanceCheacking( float ammount , int accountID);
    public Task<bool> updateBalance( float ammount , int fromAccountID, int toAccountID);





}
