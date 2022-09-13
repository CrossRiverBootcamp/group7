

using CustomerAccount.Service.Models;

namespace CustomerAccount.Service.Interfaces;

public interface IEmailVerificationService
{
    public Task<int> verifyUser(EmailVerificationModel emailVerification);

}
