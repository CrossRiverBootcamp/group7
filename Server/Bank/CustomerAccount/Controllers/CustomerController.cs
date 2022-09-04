using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CustomerController : ControllerBase
{
    ICustomerService _CustomerService;
    IMapper _IMapper;

    public CustomerController(ICustomerService CustomerService, IMapper IMapper)
    {
        _CustomerService = CustomerService;
        _IMapper = IMapper;
    }

    // POST/
    [HttpPost]
    public async Task<ActionResult<int>> login([FromBody] LoginDTO login)
    {
        LoginModel newLogin = _IMapper.Map<LoginDTO, LoginModel>(login);
        return await _CustomerService.login(newLogin);

    }

}