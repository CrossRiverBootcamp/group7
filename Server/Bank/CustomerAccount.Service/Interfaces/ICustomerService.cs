

using CustomerAccount.Service.Models;

namespace CustomerAccount.Service.Interfaces;

public interface ICustomerService
{
    public Task<int> login(LoginModel login);
}

