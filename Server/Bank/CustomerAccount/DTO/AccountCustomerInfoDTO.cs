namespace CustomerAccount.WebApi.DTO;

public class AccountCustomerInfoDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int CustomerID { get; set; }
    public DateTime OpenDate { get; set; }
    public float Balance { get; set; }
}
