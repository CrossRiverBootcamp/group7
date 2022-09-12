

using AutoMapper;
using NSB.Event;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Service.Interfaces;
using Transaction.Service.Models;

namespace Transaction.WebApi.Handale;

public class UpdateTransactionHandler : IHandleMessages<AccountUpdated>
{
    static ILog log = LogManager.GetLogger<UpdateTransactionHandler>();
    ITransactionService _transactionService;
    IMapper _mapper;
    public UpdateTransactionHandler(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }

    public async Task Handle(AccountUpdated message, IMessageHandlerContext context)
    {
        log.Info($"Received UpdateTransacion, TransactionID = {message.TransactionID}");
        UpdateTransactionModel updateBalance = _mapper.Map<AccountUpdated, UpdateTransactionModel>(message);
        await _transactionService.updateTransaction(updateBalance);
        
    }

    
}
