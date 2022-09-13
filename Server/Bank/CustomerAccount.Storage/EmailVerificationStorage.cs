
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Storage;

public class EmailVerificationStorage : IEmailVerificationStorage
{
    private readonly IDbContextFactory<CustomerAccountDbContext> _factory;

    public EmailVerificationStorage(IDbContextFactory<CustomerAccountDbContext> factory)
    {

        _factory = factory;
    }
    public async Task<int> verifyUser(EmailVerification emailVerification)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                if (await _BankDbContext.EmailsVerification.FirstOrDefaultAsync(verification =>
                verification.Email == emailVerification.Email &&
                verification.ExpirationTime.AddMinutes(2) <= emailVerification.ExpirationTime &&
                verification.VerificationCode == emailVerification.VerificationCode) == null)
                {
                    return 21;
                }
                return 0;

            }
            catch
            {
                return 1;
                //throw new DbContextException();//
            }
        }
    }
}
