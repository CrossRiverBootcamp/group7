

using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Interfaces;

namespace CustomerAccount.Service;

public class CustomerService : ICustomerService
{

  
    ICustomerStorage _CustomerStorage;

    public CustomerService(ICustomerStorage CustomerStorage)
    {
        _CustomerStorage = CustomerStorage;

    }
    public async Task<int> login(LoginModel login)
    {
   
        return await _CustomerStorage.login(login.Email, login.Password);

       
    }
}
