﻿

using System.ComponentModel.DataAnnotations;

namespace CustomerAccount.Storage.Entites;

public class EmailVerification
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string VerificationCode { get; set; }

    [Required]
    public DateTime ExpirationTime { get; set; }


}

