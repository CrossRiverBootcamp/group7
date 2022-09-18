using AutoMapper;

using NSB.Command;
using NSB.Event;
using NServiceBus;
using NServiceBus.Logging;

namespace Transactions.NSB;

public class TransactionPolicy : Saga<TransactionPolicyData>,
 IAmStartedByMessages<Payload>, IHandleMessages<AccountUpdated>
{
    static ILog log = LogManager.GetLogger<TransactionPolicy>();
    
    public async Task Handle(Payload message, IMessageHandlerContext context)
    {
        log.Info($"Received Payload,  TransactionID = {message.TransactionID}");
        Data.TransactionID = message.TransactionID;
        await context.Send(new UpdateAccount()
        {
            TransactionID = message.TransactionID,
            FromAccountId = message.FromAccountId,
            ToAccountId = message.ToAccountId,
            Amount = message.Amount
        });
    }

    public Task Handle(AccountUpdated message, IMessageHandlerContext context)
    {
        log.Info($"Received update account balance,  TransactionID = {message.TransactionID}");
        MarkAsComplete();
        return Task.CompletedTask;
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
    {
        mapper.MapSaga(saga => saga.TransactionID)
            .ToMessage<Payload>(message => message.TransactionID)
            .ToMessage<AccountUpdated>(msg => msg.TransactionID);
    }
}
