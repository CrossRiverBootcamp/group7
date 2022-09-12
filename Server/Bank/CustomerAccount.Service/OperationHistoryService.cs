
using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;

namespace CustomerAccount.Service;

public class OperationHistoryService : IOperationHistoryService
{
    IOperationHistoryStorage _OperationHistoryStorage;
    IMapper _Mapper;
    public OperationHistoryService(IOperationHistoryStorage OperationHistoryStorage, IMapper Mapper)
    {
        _OperationHistoryStorage = OperationHistoryStorage;
        _Mapper = Mapper;

    }

    public async Task<List<OperationHistoryModel>> getOperationHistory(int accountID)
    {
        List<OperationHistory> operationHistory = await _OperationHistoryStorage.getOperationHistory(accountID);
        List<OperationHistoryModel> operationsList = operationHistory.ConvertAll(operation => _Mapper.Map<OperationHistoryModel>(operation));
        return operationsList;
    }
}
