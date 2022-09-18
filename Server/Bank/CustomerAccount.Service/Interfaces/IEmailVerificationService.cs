
namespace CustomerAccount.Service.Interfaces;

public interface IEmailVerificationService
{
    public Task<bool> verifyUser(string email);

}
