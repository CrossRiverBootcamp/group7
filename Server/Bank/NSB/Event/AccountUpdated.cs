
namespace NSB.Event
{
    public class AccountUpdated
    {
        public int TransactionID { get; set; }
        public int Status { get; set; }

        public string FailureReason { get; set; }
    }
}
