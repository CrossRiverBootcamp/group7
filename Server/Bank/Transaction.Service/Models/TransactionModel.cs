namespace Transaction.Service.Models;
public class TransactionModel
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public int Amount { get; set; }
    public DateTime OpenDate { get; set; }
}
