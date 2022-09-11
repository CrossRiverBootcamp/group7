using AutoMapper;
using NSB.Command;
using NSB.Event;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Service.Interfaces;
using Transaction.Service.Models;

namespace Transactions.NSB;



public class TransactionPolicy : Saga<TransactionPolicyData>,
 IAmStartedByMessages<Payload>, IAmStartedByMessages<AccountUpdated>
{
    static ILog log = LogManager.GetLogger<TransactionPolicy>();
    ITransactionService _transactionService;
    IMapper _mapper;
    public TransactionPolicy(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }

    public async Task Handle(Payload message, IMessageHandlerContext context)
    {
        log.Info($"Received TransactionMessage,  TransactionID = {message.TransactionID}");
        Data.TransactionID = message.TransactionID;
        await context.Send(new UpdateAccount()
        {
            TransactionID = Data.TransactionID,
            FromAccountId = message.FromAccountId,
            ToAccountId = message.ToAccountId,
            Amount = message.Amount
        });
    }
    public async Task Handle(AccountUpdated message, IMessageHandlerContext context)
    {
        log.Info($"Received AccountUpdated,  TransactionID = {message.TransactionID}");
        UpdateTransactionModel transactionModel = _mapper.Map<AccountUpdated, UpdateTransactionModel>(message);
        bool result = await _transactionService.updateTransaction(transactionModel);
        MarkAsComplete();
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
    {
        mapper.MapSaga(saga => saga.TransactionID)
            .ToMessage<Payload>(message => message.TransactionID)
            .ToMessage<AccountUpdated>(msg => msg.TransactionID);
    }
}
