

using CustomerAccount.Storage.Entites;

namespace CustomerAccount.Storage.Interfaces;

public interface IAccountStorage
{
    public Task<bool> createNewAccount(Account  account, Customer customer);
    public Task<Account> getAccountCustomerInfo(int accountID);
    public Task<bool> accountExist(int accountID);
    public Task<bool> balanceCheacking( float ammount , int accountID);
    public Task<BalanceObject> updateBalance( float ammount , int fromAccountID, int toAccountID);

}
