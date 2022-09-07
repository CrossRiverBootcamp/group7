

using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using NSB.Event;
using NServiceBus;

namespace CustomerAccount.Service;

public class AccountService : IAccountService
{

    IMapper _IMapper;
    IAccountStorage _AccountStorage;
    IAuthorizationFuncs _AuthorizationFuncs;

    public AccountService(IAccountStorage AccountStorage, IMapper Mapper, IAuthorizationFuncs authorizationFuncs)
    {
        _AccountStorage = AccountStorage;
        _IMapper = Mapper;
        _AuthorizationFuncs = authorizationFuncs;
    }
    public async Task<bool> createNewAccount(CustomerModel customer)
    {
        if (await _AccountStorage.emailExist(customer.Email) == false)
        {
            Customer newCustomer = _IMapper.Map<CustomerModel, Customer>(customer);
            /*var salt = _AuthorizationFuncs.GenerateSalt(8);
            newCustomer.Password = _AuthorizationFuncs.HashPassword(newCustomer.Password, salt, 1000, 8);*/
            AccountModel accunt = new AccountModel() { Balance = 1000, OpenDate = DateTime.Now };
            Account newAccont = _IMapper.Map<AccountModel, Account>(accunt);
            return await _AccountStorage.createNewAccount(newAccont, newCustomer);
        }

        else
        {
            return false;
        }
    }

    public async Task<AccountCustomerInfoModel> getAccountCustomerInfo(int accountID)
    {
        Account account = await _AccountStorage.getAccountCustomerInfo(accountID);
        AccountCustomerInfoModel accountInfo = _IMapper.Map<Account, AccountCustomerInfoModel>(account);
        return accountInfo;


    }

    public async Task<bool> updateBalance(UpdateBalanceModel updateBalance, IMessageSession messageSession)
    {
        if (await _AccountStorage.accountExist(updateBalance.FromAccountId) == false)
        {
            AccountUpdated accountUpdated = new AccountUpdated()
            {
                TransactionID = updateBalance.TransactionId,
                Status = 2,
                FailureReason = "From Account is not exsist"
            };
            await messageSession.Publish(accountUpdated);
            return false;
        }
        if (await _AccountStorage.accountExist(updateBalance.ToAccountId) == false)
        {
            AccountUpdated accountUpdated = new AccountUpdated()
            {
                TransactionID = updateBalance.TransactionId,
                Status = 2,
                FailureReason = "To Account is not exsist"
            };
            await messageSession.Publish(accountUpdated);
            return false;
        }
        else if (await _AccountStorage.balanceCheacking(updateBalance.Amount, updateBalance.FromAccountId) == false)
        {
            AccountUpdated accountUpdated = new AccountUpdated()
            {
                TransactionID = updateBalance.TransactionId,
                Status = 2,
                FailureReason = "The balnce is not inahf"
            };
            await messageSession.Publish(accountUpdated);
            return false;
        }
        else
        {
            await _AccountStorage.updateBalance(updateBalance.Amount, updateBalance.FromAccountId, updateBalance.ToAccountId);
            AccountUpdated accountUpdated = new AccountUpdated()
            {
                TransactionID = updateBalance.TransactionId,
                Status = 1
            };
            await messageSession.Publish(accountUpdated);
            return true;
        }
    }
}
