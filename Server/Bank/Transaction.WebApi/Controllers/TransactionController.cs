
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

    public TransactionController(ITransactionService TransactionService, IMapper IMapper)
    {
        _TransactionService = TransactionService;
        _IMapper = IMapper;
    }

    // POST/
    [HttpPost]
    public async Task<ActionResult<bool>> createTransaction([FromBody] TransactionDTO transaction)
    {
        TransactionModel newTansaction = _IMapper.Map<TransactionDTO, TransactionModel>(transaction);
        var result = await _TransactionService.createTransaction(newTansaction);
        return Ok(result);

    }
 }


