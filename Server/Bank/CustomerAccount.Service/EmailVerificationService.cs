

using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;

namespace CustomerAccount.Service;

public class EmailVerificationService : IEmailVerificationService
{
    IEmailVerificationStorage _EmailVerificationStorage;
    IMapper _Mapper;
    IAccountStorage _AccountStorage;
    public EmailVerificationService(IEmailVerificationStorage EmailVerificationStorage, IMapper Mapper, IAccountStorage accountStorage)
    {
        _EmailVerificationStorage = EmailVerificationStorage;
        _Mapper = Mapper;
        _AccountStorage = accountStorage;
    }
    public async Task<bool> verifyUser(EmailVerificationModel emailVerification)
    {
        EmailVerification verification = _Mapper.Map<EmailVerificationModel, EmailVerification>(emailVerification);
        bool userIsVerify = await _EmailVerificationStorage.verifyUser(verification);
        if (userIsVerify)
        {
            Customer newCustomer = _Mapper.Map<EmailVerificationModel, Customer>(emailVerification);
            /* var salt = _AuthorizationFuncs.GenerateSalt(8);
             newCustomer.Password = _AuthorizationFuncs.HashPassword(newCustomer.Password, salt, 1000, 8);*/
            AccountModel accunt = new AccountModel() { Balance = 1000, OpenDate = DateTime.Now };
            Account newAccont = _Mapper.Map<AccountModel, Account>(accunt);
            return await _AccountStorage.createNewAccount(newAccont, newCustomer);
        }
        else
        {
            return false;
        }


    }
}

