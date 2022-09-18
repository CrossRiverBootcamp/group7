
using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using Exceptions;

namespace CustomerAccount.Service;

public class EmailVerificationService : IEmailVerificationService
{
    IEmailVerificationStorage _EmailVerificationStorage;
    IMapper _Mapper;
    ISendEmail _sendEmail;
    ICustomerStorage _customerStorage;
    
    public EmailVerificationService(IEmailVerificationStorage EmailVerificationStorage, IMapper Mapper, ISendEmail sendEmail, ICustomerStorage customerStorage)
    {
        _EmailVerificationStorage = EmailVerificationStorage;
        _Mapper = Mapper;
        _sendEmail = sendEmail;
        _customerStorage = customerStorage;
    }
   
    public async Task<bool> verifyUser(string email)
    {
        
        if (await _customerStorage.emailExist(email) == false)
        {
            if (await _EmailVerificationStorage.numOfTrialsIsOver(email) == false)
            {
                var code = new Random(Guid.NewGuid().GetHashCode()).Next(0, 9999).ToString("D4");
                string subject = "Confirm your email address";
                string body = $"Your confirmation code is below — enter it in your open browser window and sign in :) \n {code}";
                bool sendEmail= _sendEmail.sendEmail(email, subject, body);

                if (sendEmail == true)
                {
                    EmailVerificationModel emailVerificationModel = new EmailVerificationModel()
                    {
                        Email = email,
                        VerificationCode = code,
                        ExpirationTime = DateTime.Now
                    };

                    EmailVerification emailVerification = _Mapper.Map<EmailVerificationModel, EmailVerification>(emailVerificationModel);
                    return await _EmailVerificationStorage.addEmailVarifiction(emailVerification);
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
        else
        {
            return false;
        }
        
    }
}


    





