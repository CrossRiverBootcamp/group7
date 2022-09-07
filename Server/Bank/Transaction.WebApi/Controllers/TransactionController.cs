
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Transaction.Service.Interfaces;
using Transaction.Service.Models;
using Transaction.WebApi.DTO;

namespace Transaction.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{

    ITransactionService _TransactionService;
    IMapper _IMapper;
    IMessageSession _messageSession;

    public TransactionController(ITransactionService TransactionService, IMapper IMapper, IMessageSession messageSession)
    {
        _TransactionService = TransactionService;
        _IMapper = IMapper;
        _messageSession = messageSession;
    }

    // POST/
    [HttpPost]
    public async Task<ActionResult<bool>> createTransaction([FromBody] TransactionDTO transaction)
    {
        TransactionModel newTansaction = _IMapper.Map<TransactionDTO, TransactionModel>(transaction);
        var result = await _TransactionService.createTransaction(newTansaction, _messageSession);
        return Ok(result);

    }
 }


