
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Storage;

public class EmailVerificationStorage : IEmailVerificationStorage
{
    private readonly IDbContextFactory<CustomerAccountDbContext> _factory;

    public EmailVerificationStorage(IDbContextFactory<CustomerAccountDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<bool> addEmailVarifiction(EmailVerification emailVerification)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                await _BankDbContext.EmailsVerification.AddAsync(emailVerification);
                await _BankDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new DbContextException();
            }
        }
    }

    public async Task<bool> verifyUser(string emailVerification , string email)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                if (await _BankDbContext.EmailsVerification.FirstOrDefaultAsync(verification =>
                verification.Email == email &&
                verification.ExpirationTime.AddMinutes(5) <= DateTime.Now &&
                verification.VerificationCode == emailVerification) == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            { 
                throw new DbContextException();
            }
        }
    }
}
