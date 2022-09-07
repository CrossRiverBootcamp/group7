
using Transaction.Service.Models;
using NServiceBus;

namespace Transaction.Service.Interfaces;

public interface ITransactionService
{
    public Task<bool> createTransaction(TransactionModel transaction , IMessageSession messageSession);
    public Task<bool> updateTransaction(TransactionModel transaction);

}
