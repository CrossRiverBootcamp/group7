

using CustomerAccount.Service.Models;
using NSB.Event;
using NServiceBus;

namespace CustomerAccount.Service.Interfaces;

public interface IAccountService
{
    public Task<bool> createNewAccount(CustomerModel customer);

    public Task<AccountCustomerInfoModel> getAccountCustomerInfo(int accountID);

    public Task<AccountUpdated> updateBalance(UpdateBalanceModel updateBalance);



}
