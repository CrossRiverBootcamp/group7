
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
                EmailVerification verification = await _BankDbContext.EmailsVerification.FirstOrDefaultAsync(verification => verification.Email == emailVerification.Email);
                if (verification == null)
                {
                    emailVerification.NumOfTrials = 1;
                    emailVerification.FirsteEnteringTime = emailVerification.ExpirationTime;
                    await _BankDbContext.EmailsVerification.AddAsync(emailVerification);
                    await _BankDbContext.SaveChangesAsync();
                }
                else
                {
                    await updateNumOfTrials(emailVerification);
                }
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
                EmailVerification verification = await _BankDbContext.EmailsVerification.FirstOrDefaultAsync(verification => verification.Email == email);
                if (verification != null)
                {
                    if (DateTime.Compare( verification.ExpirationTime.AddMinutes(5) ,DateTime.Now) > 0 && verification.VerificationCode.Equals( emailVerification))
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }
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

    public async Task<bool> updateNumOfTrials(EmailVerification emailVerification)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                EmailVerification verification = await _BankDbContext.EmailsVerification.FirstOrDefaultAsync(verification => verification.Email == emailVerification.Email);
                if(DateTime.Compare(verification.FirsteEnteringTime.AddMinutes(30) , emailVerification.ExpirationTime) <0 )
                {
                    verification.NumOfTrials = 1;
                    verification.FirsteEnteringTime = emailVerification.ExpirationTime;
                    verification.VerificationCode=emailVerification.VerificationCode;
                }
                else
                {
                    verification.NumOfTrials += 1;
                    verification.ExpirationTime = emailVerification.ExpirationTime;
                    verification.VerificationCode = emailVerification.VerificationCode;
                }
                await _BankDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new DbContextException();
            }
        }
    }



    public async Task<bool> numOfTrialsIsOver(string email)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                EmailVerification verification = await _BankDbContext.EmailsVerification.FirstOrDefaultAsync(verification => verification.Email == email);
                if (verification!= null && verification.NumOfTrials + 1 == 4 && DateTime.Compare( verification.FirsteEnteringTime.AddMinutes(30), DateTime.Now) >0)
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
