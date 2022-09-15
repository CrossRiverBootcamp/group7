

using CustomerAccount.Storage.Entites;

namespace CustomerAccount.Storage.Interfaces;

public interface IEmailVerificationStorage
{
    public Task<bool> verifyUser(string verfication , string email);
    public Task<bool> addEmailVarifiction(EmailVerification emailVerification);

}
