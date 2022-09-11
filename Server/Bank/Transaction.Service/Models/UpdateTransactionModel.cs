
namespace Transaction.Service.Models;
public class UpdateTransactionModel
{
    public int TransactionID { get; set; }
    public int Status { get; set; }
    public string FailureReason { get; set; }
}
