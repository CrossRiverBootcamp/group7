
using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.Storage.Entites;
using CustomerAccount.Storage.Interfaces;

namespace CustomerAccount.Service;

public class OperationHistoryService : IOperationHistoryService
{
    IOperationHistoryStorage _OperationHistoryStorage;
    IAccountStorage _AccountStorage;
    IMapper _Mapper;
    public OperationHistoryService(IOperationHistoryStorage OperationHistoryStorage, IMapper Mapper, IAccountStorage AccountStorage)
    {
        _OperationHistoryStorage = OperationHistoryStorage;
        _AccountStorage= AccountStorage;
        _Mapper = Mapper;

    }

    

    public async Task<List<OperationHistoryModel>> getOperationHistory(int accountID)
    {
        List<OperationHistory> operationHistory = await _OperationHistoryStorage.getOperationHistory(accountID);
        List<OperationHistoryModel> operationsList = operationHistory.ConvertAll(operation => _Mapper.Map<OperationHistoryModel>(operation));
        return operationsList;
    }

    public async Task<CustomerInfoModel> getCustomerInfo(int accountID)
    {
        Account account = await _AccountStorage.getAccountCustomerInfo(accountID);
        CustomerInfoModel customerInfo = _Mapper.Map<Account, CustomerInfoModel>(account);
        return customerInfo;
    }
}
