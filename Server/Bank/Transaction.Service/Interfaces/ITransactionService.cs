
using Transaction.Service.Models;

namespace Transaction.Service.Interfaces;

public interface ITransactionService
{
    public Task<bool> createTransaction(TransactionModel transaction);

}
