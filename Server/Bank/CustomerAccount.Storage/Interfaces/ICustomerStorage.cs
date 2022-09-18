

namespace CustomerAccount.Storage.Interfaces;

public interface ICustomerStorage
{
    public Task<int> login(String email , string password);
    public Task<bool> emailExist(string email);
}
