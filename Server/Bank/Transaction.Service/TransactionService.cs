
using AutoMapper;
using Transaction.Service.Interfaces;
using Transaction.Service.Models;
using Transaction.Storage.Interfaces;

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
    public async Task<bool> createTransaction(TransactionModel transaction)
    {
        Storage.Entites.Transaction newTransaction = _IMapper.Map<TransactionModel, Storage.Entites.Transaction>(transaction);
        return await _transactionStorage.createTransaction(newTransaction);
    }
}
