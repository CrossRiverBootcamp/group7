
using AutoMapper;
using NSB.Event;
using NServiceBus;
using Transaction.Service.Interfaces;
using Transaction.Service.Models;
using Transaction.Storage.Entites;
using Transaction.Storage.Interfaces;
using Transaction.Storage.Models;

namespace Transaction.Service;

public class TransactionService : ITransactionService
{
    IMapper _IMapper;
    ITransactionStorage _transactionStorage;
    
    public TransactionService(ITransactionStorage TransactionStorage, IMapper Mapper)
    {
        _transactionStorage = TransactionStorage;
        _IMapper = Mapper;
    }
    public async Task<bool> createTransaction(TransactionModel transaction, IMessageSession messageSession)
    {
        Storage.Entites.Transaction newTransaction = _IMapper.Map<TransactionModel, Storage.Entites.Transaction>(transaction);
        newTransaction.Status = (int)Status.PROCESSING;
        var transactionAdded = await _transactionStorage.createTransaction(newTransaction);
        if(transactionAdded!= null)
        {
            Payload payload = new()
            {
                FromAccountId = transaction.FromAccountId,
                ToAccountId = transaction.ToAccountId,
                Amount = transaction.Amount,
                TransactionID = transactionAdded.ID,
            };

            await messageSession.Publish(payload);
            return true;
        }
         return false;
    }

    public async Task<bool> updateTransaction(UpdateTransactionModel transaction)
    {
        UpdateTransactionStatus newTransaction = _IMapper.Map<UpdateTransactionModel, UpdateTransactionStatus>(transaction);
        return await _transactionStorage.updateTransaction(newTransaction);

    }
}
