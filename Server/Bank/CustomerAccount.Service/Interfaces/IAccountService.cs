

using CustomerAccount.Service.Models;

namespace CustomerAccount.Service.Interfaces;

public interface IAccountService
{
    public Task<bool> createNewAccount(CustomerModel customer);

    public Task<AccountCustomerInfoModel> getAccountCustomerInfo(int accountID);
    public Task<bool> updateBalance(UpdateBalanceModel updateBalance);



}
