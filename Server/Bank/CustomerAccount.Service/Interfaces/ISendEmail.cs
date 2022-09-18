
namespace CustomerAccount.Service.Interfaces;

public interface ISendEmail
{

    public bool sendEmail(string email, string subject, string body);

}
