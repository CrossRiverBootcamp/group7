

using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Interfaces;

namespace CustomerAccount.Service;

public class CustomerService : ICustomerService
{

  
    ICustomerStorage _CustomerStorage;
    IAuthorizationFuncs _AuthorizationFuncs;
    public CustomerService(ICustomerStorage CustomerStorage, IAuthorizationFuncs authorizationFuncs)
    {
        _CustomerStorage = CustomerStorage;
        _AuthorizationFuncs = authorizationFuncs;
    }
    public async Task<int> login(LoginModel login)
    {
       /* var salt = _AuthorizationFuncs.GenerateSalt(8);
        login.Password = _AuthorizationFuncs.HashPassword(login.Password, salt, 1000, 8);*/
        return await _CustomerStorage.login(login.Email, login.Password);

       
    }
}
