
namespace CustomerAccount.Service.Interfaces;

public interface ISendEmail
{
    public void sendEmail(string email, string subject, string body);

}
