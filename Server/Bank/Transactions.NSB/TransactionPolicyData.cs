

using NServiceBus;

namespace Transactions.NSB;

public class TransactionPolicyData : ContainSagaData
{
    public int TransactionID { get; set; }
}
