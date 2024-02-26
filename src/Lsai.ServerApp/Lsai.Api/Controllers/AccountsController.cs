using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Application.Common.Identity.Services;
using Lsai.Domain.Common.Constants;
using Lsai.Domain.Common.Enums;
using Lsai.Domain.Common.Filters;
using Lsai.Domain.Common.Identity;
using Lsai.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(
    IUserService userService,
    IAccountOrchestrationService accountOrchestrationService,
    IMapper mapper) : ControllerBase
{
    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] UserDto userDto, CancellationToken cancellationToken)
    {
        var result = await userService.UpdateAsync(mapper.Map<User>(userDto), cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<UserDto>(result)) : BadRequest();
    }

    [HttpGet]
    public ValueTask<IActionResult> Get([FromQuery] PaginationOptions paginationOptions, CancellationToken cancellationToken)
    {
        var result = userService.Get(paginationOptions: paginationOptions);
        return new(result.Any() ? Ok(mapper.Map<List<UserDto>>(result)) : NoContent());
    }

    [HttpGet("{userId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var result = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<UserDto>(result)) : NotFound();
    }

    [HttpDelete("{userId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var result = await userService.DeleteByIdAsync(userId, cancellationToken: cancellationToken);

        return result is not null ? Ok(mapper.Map<UserDto>(result)) : BadRequest();
    }

    [Authorize(Roles = "System")]
    [HttpPut("grandRole/{userId:guid}/{role}")]
    public async ValueTask<IActionResult> GrandRole([FromRoute] Guid userId, [FromRoute] Role role, CancellationToken cancellationToken)
    {
        var result = await accountOrchestrationService.GrandRoleAsync(userId, role, cancellationToken: cancellationToken);

        return result ? Ok(result) : BadRequest();
    }

    [Authorize]
    [HttpPut("changePassword")]
    public async ValueTask<IActionResult> UpdatePassword([FromBody] ChangePasswordDetails changePasswordDetails, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);
        var result = await accountOrchestrationService.UpdatePasswordAsync(userId, changePasswordDetails, cancellationToken);

        return result ? Ok(result) : BadRequest();
    }

    [Authorize, HttpPost("verification")]
    public async ValueTask<IActionResult> SendVerificationCode(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);
        var result = await accountOrchestrationService.SendVerificationCodeAsync(userId, cancellationToken);

        return result ? Ok(result) : BadRequest();
    }

    [Authorize, HttpPut("verify/{code:int}")]
    public async ValueTask<IActionResult> Verify([FromRoute] int code)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);
        var result = await accountOrchestrationService.VerifyAccountAsync(userId, code);

        return result ? Ok(result) : BadRequest();
    }

    [Authorize, HttpPost("resetPassword/{email}")]
    public async ValueTask<IActionResult> SendResetPasswordVerificationCode([FromRoute] string email, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);
        var result = await accountOrchestrationService.SendResetPasswordCodeAsync(userId, email, cancellationToken);

        return result ? Ok(result) : BadRequest();
    }

    [Authorize, HttpPut("resetPassword")]
    public async ValueTask<IActionResult> ResetPasswod([FromBody] ResetPasswordDetails resetPasswordDetails, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);
        var result = await accountOrchestrationService.ResetPassword(userId, resetPasswordDetails, cancellationToken);

        return result ? Ok(result) : BadRequest();
    }
}