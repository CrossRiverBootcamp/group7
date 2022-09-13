

using CustomerAccount.Storage.Entites;

namespace CustomerAccount.Storage.Interfaces;

public interface IEmailVerificationStorage
{
    public Task<int> verifyUser(EmailVerification emailVerification);

}
