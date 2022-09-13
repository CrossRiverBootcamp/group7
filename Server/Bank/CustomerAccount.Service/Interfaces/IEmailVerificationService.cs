

using CustomerAccount.Service.Models;

namespace CustomerAccount.Service.Interfaces;

public interface IEmailVerificationService
{
    public Task<bool> verifyUser(EmailVerificationModel emailVerification);

}
