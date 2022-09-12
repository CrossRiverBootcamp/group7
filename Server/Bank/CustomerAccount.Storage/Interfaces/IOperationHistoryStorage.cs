
using CustomerAccount.Storage.Entites;

namespace CustomerAccount.Storage.Interfaces;

public interface IOperationHistoryStorage
{
    public Task<List<OperationHistory>> getOperationHistory(int accountID);
    public Task<bool> addOperationHistory(OperationHistory operationFrom , OperationHistory operationTo);


}
