

using CustomerAccount.Storage.Entites;

namespace CustomerAccount.Storage.Interfaces;

public interface IEmailVerificationStorage
{
    public Task<bool> verifyUser(EmailVerification emailVerification);
    public Task<bool> addEmailVarifiction(EmailVerification emailVerification);

}
