

using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using System.Net.Mail;

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
    public async Task<bool> verifyUser(string email)
    {

        if (await _AccountStorage.emailExist(email) == false)
        {
            EmailVerificationModel emailVerificationModel = sendEmail(email);
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


    public EmailVerificationModel sendEmail(string email)
    {
        //generate a code
        var code = new Random(Guid.NewGuid().GetHashCode()).Next(0, 9999).ToString("D4");
        //send a mail
        MailAddress from = new MailAddress("crossriver@outlook.co.il");
        MailAddress to = new MailAddress(email);
        MailMessage message = new MailMessage(from, to);
        message.Subject = "Confirm your email address";
        message.Body = $"Your confirmation code is below — enter it in your open browser window and sign in :) \n {code}";
        SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
        SmtpServer.Port = 587;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Credentials = new System.Net.NetworkCredential("crossriver@outlook.co.il", "Zipi&Shira");
        SmtpServer.EnableSsl = true;
        try
        {
            SmtpServer.Send(message);
            EmailVerificationModel emailVerificationModel = new EmailVerificationModel()
            {
                Email = email,
                VerificationCode = code,
                ExpirationTime = DateTime.Now
            };
            return emailVerificationModel;
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}


    





