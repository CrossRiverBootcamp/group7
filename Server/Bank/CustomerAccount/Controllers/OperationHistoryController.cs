using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomerAccount.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationHistoryController : Controller
{
    IOperationHistoryService _OperationHistoryService;
    IMapper _IMapper;

    public OperationHistoryController(IOperationHistoryService OperationHistoryService, IMapper IMapper)
    {
        _OperationHistoryService = OperationHistoryService;
        _IMapper = IMapper;
    }
    // GET/
    [HttpGet()]
    public async Task<ActionResult<List<OperationHistoryDTO>>> getOperationHistory(int accountID)
    {
        List<OperationHistoryModel> OperationHistory = await _OperationHistoryService.getOperationHistory(accountID);
        //לבדוק
        List<OperationHistoryDTO> operationsList = OperationHistory.ConvertAll(operation => _IMapper.Map<OperationHistoryDTO>(operation));
        return operationsList;
    }

}
