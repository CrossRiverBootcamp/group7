
using CustomerAccount.Storage.Entites;

namespace CustomerAccount.Storage.Interfaces;

public interface IOperationHistoryStorage
{
    public Task<List<OperationHistory>> getOperationHistory(int accountID, int pageNumber, int numberOfRecords);
    public Task<bool> addOperationHistory(OperationHistory operationFrom , OperationHistory operationTo);
    public Task<int> getOperationHistoryRecoredsCount(int accountID);

}
