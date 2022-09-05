using System.ComponentModel.DataAnnotations;

namespace CustomerAccount.WebApi.DTO
{
    public class AccountDTO
    {
        [Required]
        public int CustomerID { get; set; }

        [Required]
        public DateTime OpenDate { get; set; }

        [Required]
        public float Balance { get; set; }


    }
}
