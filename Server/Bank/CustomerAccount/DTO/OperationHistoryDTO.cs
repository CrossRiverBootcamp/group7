namespace CustomerAccount.WebApi.DTO;

public class OperationHistoryDTO
{

    public int AccountId { get; set; }

    public bool IsDebit { get; set; }


    public float Amount { get; set; }

 
    public float Balance { get; set; }

    public DateTime Date { get; set; }
}
