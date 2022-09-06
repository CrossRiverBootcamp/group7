

using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;

namespace CustomerAccount.Service;

public class AccountService : ITransactionService
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
        if (await _AccountStorage.cheackAcountExist(customer.Email) == false)
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
}
