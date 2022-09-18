

using CustomerAccount.Service.Interfaces;
using System.Net.Mail;

namespace CustomerAccount.Service;

public class SendEmail : ISendEmail
{
    public void sendEmail(string email , string subject ,string body )
    {
        //send a mail
        MailAddress from = new MailAddress("crossriver@outlook.co.il");
        MailAddress to = new MailAddress(email);
        MailMessage message = new MailMessage(from, to);
        message.Subject = subject;
        message.Body = body;
        SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
        SmtpServer.Port = 587;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Credentials = new System.Net.NetworkCredential("crossriver@outlook.co.il", "Zipi&Shira");
        SmtpServer.EnableSsl = true;
        try
        {
            SmtpServer.Send(message);
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex);
       
        }
    }
}
