using System.ComponentModel.DataAnnotations;

namespace CustomerAccount.WebApi.DTO;

public class EmailVerificationDTO
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string VerificationCode { get; set; }

    [Required]
    public DateTime ExpirationTime { get; set; }
}
