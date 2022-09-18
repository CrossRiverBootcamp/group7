
using CustomerAccount.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAccount.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class EmailVerificationController : ControllerBase
{
    IEmailVerificationService _EmailVerificationService;

    public EmailVerificationController(IEmailVerificationService EmailVerificationService)
    {
        _EmailVerificationService = EmailVerificationService;
    }

    // POST/
    [HttpPost]
    public async Task<bool> verifyUser([FromBody] string email)
    {
        return await _EmailVerificationService.verifyUser(email);
    }

}
