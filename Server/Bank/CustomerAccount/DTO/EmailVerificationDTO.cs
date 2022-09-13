using System.ComponentModel.DataAnnotations;

namespace CustomerAccount.WebApi.DTO;

public class EmailVerificationDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(2)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2)]
    public string LastName { get; set; }


    [Required]
    [MinLength(3)]
    public string Password { get; set; }

    [Required]
    public string VerificationCode { get; set; }

    [Required]
    public DateTime ExpirationTime { get; set; }
}
