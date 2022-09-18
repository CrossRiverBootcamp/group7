

using CustomerAccount.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace CustomerAccount.Service;

public class SendEmail : ISendEmail
{


    IConfiguration _configuration;
    public SendEmail(IConfiguration configuration)
    {

        _configuration = configuration;
    }
    public bool sendEmail(string email , string subject ,string body )
    {
        string emailAddress = _configuration.GetSection("Email:Address").ToString();
        string emailPassword = _configuration.GetSection("Email:Password").ToString();
        //send a mail
        MailAddress from = new MailAddress(emailAddress);
        MailAddress to = new MailAddress(email);
        MailMessage message = new MailMessage(from, to);
        message.Subject = subject;
        message.Body = body;
        SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
        SmtpServer.Port = 587;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Credentials = new System.Net.NetworkCredential(emailAddress, emailPassword);
        SmtpServer.EnableSsl = true;
        try
        {
            SmtpServer.Send(message);
            return true;
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}
