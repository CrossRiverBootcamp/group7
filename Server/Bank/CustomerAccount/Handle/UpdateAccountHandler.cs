
using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using NSB.Command;
using NSB.Event;
using NServiceBus;
using NServiceBus.Logging;

namespace CustomerAccount.WebApi.Handale;

public class UpdateAccountHandler : IHandleMessages<UpdateAccount>
{
    static ILog log = LogManager.GetLogger<UpdateAccountHandler>();
    IAccountService _AccountService;
    IMapper _mapper;
    public UpdateAccountHandler(IAccountService accountService, IMapper mapper)
    {
        _AccountService = accountService;
        _mapper = mapper;
    }

    public async Task Handle(UpdateAccount message, IMessageHandlerContext context)
    {
        log.Info($"Received the massage to update account balance, TransactionID = {message.TransactionID}");
        UpdateBalanceModel updateBalance = _mapper.Map<UpdateAccount, UpdateBalanceModel>(message);
        AccountUpdated accountUpdated= await _AccountService.updateBalance(updateBalance);
        log.Info($"Finish to update account balance, TransactionID = {message.TransactionID}");
        await context.Publish(accountUpdated);
    }
}
