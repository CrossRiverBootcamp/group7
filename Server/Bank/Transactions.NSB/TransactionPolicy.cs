using AutoMapper;
using NSB.Command;
using NSB.Event;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Service.Interfaces;
using Transaction.Service.Models;

namespace Transactions.NSB;



public class TransactionPolicy : Saga<TransactionPolicyData>,
 IAmStartedByMessages<Payload> , IAmStartedByMessages<AccountUpdated>
{
    static ILog log = LogManager.GetLogger<TransactionPolicy>();
    ITransactionService _transactionService;
    IMapper _mapper;
    public TransactionPolicy(ITransactionService transactionService , IMapper mapper)
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
        TransactionModel transactionModel = _mapper.Map<AccountUpdated, TransactionModel>(message);
        bool result = await _transactionService.updateTransaction(transactionModel);
        MarkAsComplete();
    }

    //public Task Handle(TrackingAdded message, IMessageHandlerContext context)
    //{
    //    log.Info($"Received TrackingAdded, cardId = {message.CardID}");
    //    Data.IsTracingAdded = true;
    //    return ProcessOrder(context);
    //}

    //private async Task ProcessOrder(IMessageHandlerContext context)
    //{
    //    if (Data.IsBMIChanged && Data.IsTracingAdded)
    //    {

    //        await context.Publish(new MeasureComplited() { Status = "success", CardID = Data.CardID, MeasureID = Data.MeasureID });
    //        MarkAsComplete();
    //    }
    //}

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
    {
        mapper.MapSaga(sagaData => sagaData.TransactionID)
            .ToMessage<Payload>(message => message.TransactionID);
    }


}




