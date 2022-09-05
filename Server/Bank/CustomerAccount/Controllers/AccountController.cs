﻿using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;


namespace CustomerAccount.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    IAccountService _AccountService;
    IMapper _IMapper;

    public AccountController(IAccountService AccountService, IMapper IMapper)
    {
        _AccountService = AccountService;
        _IMapper = IMapper;
    }

    // POST/
    [HttpPost]
    public async Task<ActionResult<bool>> addNewAccount([FromBody] CustomerDTO customer)
    {
        CustomerModel newAcustomer = _IMapper.Map<CustomerDTO, CustomerModel>(customer);
        return await _AccountService.createNewAccount(newAcustomer);

    }

    // GET/
    [HttpGet]
    public async Task<ActionResult<AccountCustomerInfoDTO>> getAccountCustomerInfo(int accountID)
    {
        AccountCustomerInfoModel accountInfo= _AccountService.getAccountCustomerInfo(accountID);
        AccountCustomerInfoDTO newAcustomer = _IMapper.Map<AccountCustomerInfoModel, AccountCustomerInfoDTO>(accountInfo);
        return newAcustomer;
    }









}


