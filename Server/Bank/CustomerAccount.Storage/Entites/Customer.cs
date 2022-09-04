
using System.ComponentModel.DataAnnotations;

namespace CustomerAccount.Storage.Entites;

public class Customer
{
    [Required]
    public int ID { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

}
