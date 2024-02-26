using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Application.Common.Identity.Services;
using Lsai.Domain.Common.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAccountOrchestrationService accountOrchestrationService, IMapper mapper) : ControllerBase
{
    [HttpPost("sign-up")]
    public async ValueTask<IActionResult> SignUp([FromBody] SignUpDetails signUpDetails, CancellationToken cancellationToken)
    {
        var result = await accountOrchestrationService.SignUpAsync(signUpDetails, cancellationToken);
        return Ok(mapper.Map<UserDto>(result));
    }

    [HttpGet("sign-in")]
    public async ValueTask<IActionResult> SignIn([FromQuery] SignInDetails signInDetails, CancellationToken cancellationToken)
    {
        var result = await accountOrchestrationService.SignInAsync(signInDetails, cancellationToken);
        return Ok(result);
    }
}
