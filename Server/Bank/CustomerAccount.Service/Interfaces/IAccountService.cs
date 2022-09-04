

using CustomerAccount.Service.Models;

namespace CustomerAccount.Service.Interfaces;

public interface IAccountService
{
    public Task<bool> createNewAccount(CustomerModel customer);

}
