﻿
using CustomerAccount.Service.Models;

namespace CustomerAccount.Service.Interfaces;

public interface IOperationHistoryService
{
    public Task<List<OperationHistoryModel>> getOperationHistory( int accountID);
}
