
namespace CustomerAccount.Service.Models;

public class EmailVerificationModel
{

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }

    public string VerificationCode { get; set; }

    public DateTime ExpirationTime { get; set; }
}
