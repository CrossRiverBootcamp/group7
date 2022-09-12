

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerAccount.Storage.Entites;

public class OperationHistory
{
    [Required]
    public int ID { get; set; }

    [Required]
    public int AccountId { get; set; }

    [Required]
    public int TransactionID { get; set; }

    [Required]
    public bool IsDebit { get; set; }

    [Required]
    public float TransactionAmount { get; set; }

    [Required]
    public float Balance { get; set; }

    [Required]
    public DateTime OperationTime { get; set; }


    [JsonIgnore]
    [ForeignKey("AccountId")]
    public Account account { get; set; }
}
