
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomerAccount.Storage;

public class OperationHistoryStorage : IOperationHistoryStorage
{
    private readonly IDbContextFactory<CustomerAccountDbContext> _factory;
    public OperationHistoryStorage(IDbContextFactory<CustomerAccountDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<OperationHistory>> getOperationHistory(int accountID)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                var innerJoinQuery = from toOperationsHistory in _BankDbContext.OperationsHistory
                                     join fromoperationsHistory in _BankDbContext.OperationsHistory
                                     on toOperationsHistory.TransactionID equals fromoperationsHistory.TransactionID
                                     where fromoperationsHistory.AccountId == accountID && toOperationsHistory.AccountId!= fromoperationsHistory.AccountId
                                     select new OperationHistory
                                     {
                                         AccountId = toOperationsHistory.AccountId,
                                         IsDebit = fromoperationsHistory.IsDebit,
                                         TransactionAmount = fromoperationsHistory.TransactionAmount,
                                         Balance = fromoperationsHistory.Balance,
                                         OperationTime = fromoperationsHistory.OperationTime
                                     };

                return innerJoinQuery.ToList();
            }
            catch
            {
                throw new Exception();//לשנותתתתתתתתתתת
            }
        }
    }

    public async Task<bool> addOperationHistory(OperationHistory operationFrom, OperationHistory operationTo)
    {
        using var _BankDbContext = _factory.CreateDbContext();
        {
            try
            {
                await _BankDbContext.OperationsHistory.AddRangeAsync(operationFrom, operationTo);
                await _BankDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception();//לשנותתתתתתתתתתת
            }
        }
    }

}
