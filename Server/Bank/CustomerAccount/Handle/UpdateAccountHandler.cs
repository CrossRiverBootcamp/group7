
using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using NSB.Command;
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
        log.Info($"Received UpdateAccount, TransactionID = {message.TransactionID}");
        UpdateBalanceModel updateBalance = _mapper.Map<UpdateAccount, UpdateBalanceModel>(message);
        await _AccountService.updateBalance(updateBalance , context);

        
    }
}
