
namespace Transaction.Storage.Entites;
public class UpdateTransactionStatus
{
    public int TransactionID { get; set; }
    public int Status { get; set; }
    public string FailureReason { get; set; }
}
