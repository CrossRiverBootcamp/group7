

using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using NSB.Command;
using NServiceBus;
using NServiceBus.Logging;

namespace CustomerAccount.NSB;

public class UpdateAccountHandler : IHandleMessages<UpdateAccount>
{
    static ILog log = LogManager.GetLogger<UpdateAccountHandler>();
    IAccountService _AccountService;
    IMapper _mapper;
    public UpdateAccountHandler(IAccountService accountService, IMapper mapper)
    {
        _AccountService = accountService;
        _mapper= mapper;
    }

    public async Task Handle(UpdateAccount message, IMessageHandlerContext context)
    {
        log.Info($"Received UpdateAccount, TransactionID = {message.TransactionID}");
        UpdateBalanceModel updateBalance = _mapper.Map<UpdateAccount, UpdateBalanceModel>(message);
        bool success = await _AccountService.updateBalance(updateBalance);
        TrackingDone trackingDone = new TrackingDone()
        {
            MeasureID = message.MeasureID
        };
        if (success)
            trackingDone.Success = true;
        else
            trackingDone.Success = false;
        await context.Publish(trackingDone);
    }
}
