

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
    public EmailVerificationService(IEmailVerificationStorage EmailVerificationStorage, IMapper Mapper)
    {
        _EmailVerificationStorage= EmailVerificationStorage;
        _Mapper=Mapper;
    }
    public Task<int> verifyUser(EmailVerificationModel emailVerification)
    {
        EmailVerification verification = _Mapper.Map<EmailVerificationModel, EmailVerification>(emailVerification);
        return await _EmailVerificationStorage.verifyUser(emailVerification);


    }
}

