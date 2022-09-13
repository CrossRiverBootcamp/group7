using AutoMapper;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Service.Models;
using CustomerAccount.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class EmailVerificationController : ControllerBase
{
    IEmailVerificationService _EmailVerificationService;
    IMapper _Mapper;
    public EmailVerificationController(IEmailVerificationService EmailVerificationService , IMapper Mapper)
    {
        _EmailVerificationService= EmailVerificationService;
        _Mapper= Mapper;
    }
    // POST/
    [HttpPost]
    public async Task<int> verifyUser([FromBody] EmailVerificationDTO verification)
    {
        EmailVerificationModel emailVerification = _Mapper.Map<EmailVerificationDTO, EmailVerificationModel>(verification);
        return  await _EmailVerificationService.verifyUser(emailVerification);
        

    }
}
