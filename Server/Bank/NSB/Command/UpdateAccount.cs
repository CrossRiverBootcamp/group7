
namespace NSB.Command;

public class UpdateAccount
{
    public int TransactionID { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public int Amount { get; set; }
}
