using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;


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
    [HttpGet("{accountID}")]
    public async Task<ActionResult<List<OperationHistoryDTO>>> getOperationHistory(int accountID,int page, int records)
    {
        List<OperationHistoryModel> OperationsHistory = await _OperationHistoryService.getOperationHistory(accountID , page , records);
        List<OperationHistoryDTO> operationsList = OperationsHistory.ConvertAll(operation => _IMapper.Map<OperationHistoryDTO>(operation));
        return Ok(operationsList);
    }

    // GET/
    [HttpGet("transactionDetailes/{accountID}")]
    public async Task<ActionResult<CustomerInfoDTO>> getCustomerInfo(int accountID)
    {
        CustomerInfoModel customerInfo= await _OperationHistoryService.getCustomerInfo(accountID);
        CustomerInfoDTO customer = _IMapper.Map<CustomerInfoModel, CustomerInfoDTO>(customerInfo);
        return Ok(customer);
    }

    // GET/
    [HttpGet("operationHistoryRecoreds/{accountID}")]
    public async Task<ActionResult<int>> getOperationHistoryRecoredsCount(int accountID)
    {
        return await _OperationHistoryService.getOperationHistoryRecoredsCount(accountID);
    }

}




