

using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Interfaces;

namespace CustomerAccount.Service;

public class CustomerService : ICustomerService
{

    IMapper _Mapper;
    ICustomerStorage _CustomerStorage;
    public CustomerService(ICustomerStorage CustomerStorage, IMapper Mapper)
    {
        _CustomerStorage = CustomerStorage;
        _Mapper = Mapper;
    }
    public async Task<int> login(LoginModel login)
    {
         return await _CustomerStorage.login(login.Email, login.Password);
       
    }
}
