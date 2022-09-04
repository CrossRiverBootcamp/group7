

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerAccount.Storage.Entites;

public class Account
{
    [Required]
    public int ID { get; set; }

    [Required]
    public int CustomerID { get; set; }

    [Required]
    public DateTime OpenDate  { get; set; }

    [Required]
    public float Balance { get; set; }


    [JsonIgnore]
    [ForeignKey("CustomerID")]
    public Customer Customer{ get; set; }



}
