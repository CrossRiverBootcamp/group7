using System.ComponentModel.DataAnnotations;

namespace CustomerAccount.WebApi.DTO;

public class CustomerInfoDTO
{
  
    public string FirstName { get; set; }
  
    public string LastName { get; set; }
    public string Email { get; set; }

    [Required]
    public DateTime ExpirationTime { get; set; }
}
