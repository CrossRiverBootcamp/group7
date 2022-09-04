using System.ComponentModel.DataAnnotations;

namespace CustomerAccount.WebApi.DTO;

public class LoginDTO
{
    [Required]
    public string Email  { get; set; }
    [Required]
    [MinLength(4)]
    public string Password { get; set; }
}
