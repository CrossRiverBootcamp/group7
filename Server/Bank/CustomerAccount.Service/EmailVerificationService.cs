
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
    ISendEmail _sendEmail;
    ICustomerStorage _customerStorage;
    public EmailVerificationService(IEmailVerificationStorage EmailVerificationStorage, IMapper Mapper, IAccountStorage accountStorage , ISendEmail sendEmail, ICustomerStorage customerStorage)
    {
        _EmailVerificationStorage = EmailVerificationStorage;
        _Mapper = Mapper;
        _AccountStorage = accountStorage;
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
                _sendEmail.sendEmail(email, subject, body);

                EmailVerificationModel emailVerificationModel = new EmailVerificationModel()
                {
                    Email = email,
                    VerificationCode = code,
                    ExpirationTime = DateTime.Now
                };

                if (emailVerificationModel != null)
                {
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


    //public EmailVerificationModel sendEmail(string email)
    //{
    //    //generate a code
    //    var code = new Random(Guid.NewGuid().GetHashCode()).Next(0, 9999).ToString("D4");
    //    //send a mail
    //    MailAddress from = new MailAddress("crossriver@outlook.co.il");
    //    MailAddress to = new MailAddress(email);
    //    MailMessage message = new MailMessage(from, to);
    //    message.Subject = "Confirm your email address";
    //    message.Body = $"Your confirmation code is below — enter it in your open browser window and sign in :) \n {code}";
    //    SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
    //    SmtpServer.Port = 587;
    //    SmtpServer.UseDefaultCredentials = false;
    //    SmtpServer.Credentials = new System.Net.NetworkCredential("crossriver@outlook.co.il", "Zipi&Shira");
    //    SmtpServer.EnableSsl = true;
    //    try
    //    {
    //        SmtpServer.Send(message);
    //        EmailVerificationModel emailVerificationModel = new EmailVerificationModel()
    //        {
    //            Email = email,
    //            VerificationCode = code,
    //            ExpirationTime = DateTime.Now
    //        };
    //        return emailVerificationModel;
    //    }
    //    catch (SmtpException ex)
    //    {
    //        Console.WriteLine(ex);
    //        return null;
    //    }
    //}
}


    





