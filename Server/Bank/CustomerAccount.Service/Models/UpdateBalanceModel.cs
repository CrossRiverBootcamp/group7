

namespace CustomerAccount.Service.Models;

public class UpdateBalanceModel
{
    public int TransactionId { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public int Amount { get; set; }
}
