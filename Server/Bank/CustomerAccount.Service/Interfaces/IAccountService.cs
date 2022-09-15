

using CustomerAccount.Service.Models;
using NServiceBus;

namespace CustomerAccount.Service.Interfaces;

public interface IAccountService
{
    public Task<bool> createNewAccount(string email);

    public Task<AccountCustomerInfoModel> getAccountCustomerInfo(int accountID);

    public Task<bool> updateBalance(UpdateBalanceModel updateBalance, IMessageHandlerContext context);



}
