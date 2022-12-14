

using System.ComponentModel.DataAnnotations;

namespace CustomerAccount.Storage.Entites;

public class EmailVerification
{
    [Key]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string VerificationCode { get; set; }

    [Required]
    public DateTime ExpirationTime { get; set; }

    [Required]
    [Range(0,3)]
    public int  NumOfTrials { get; set; }

    [Required]
    public DateTime FirsteEnteringTime { get; set; }


}

