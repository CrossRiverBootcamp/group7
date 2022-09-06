
using System.ComponentModel.DataAnnotations;

namespace Transaction.Storage.Entites;

public class Transaction
{
    [Required]
    public int FromAccountId { get; set; }

    [Required]
    public int ToAccountId { get; set; }

    [Required]
    public int Amount { get; set; }

    [Required]
    public DateTime OpenDate { get; set; }
    public int Status { get; set; }
    public string FailureReason { get; set; }
}
