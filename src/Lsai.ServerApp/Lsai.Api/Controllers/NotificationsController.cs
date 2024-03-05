using Lsai.Application.Common.Notification.Services;
using Lsai.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController(IEmailTemplateService emailTemplateService) : ControllerBase
{
    [HttpGet("templates")]
    public ValueTask<IActionResult> Get()
    {
        var result = emailTemplateService.Get();
        return new(result.Any() ? Ok(result) : BadRequest());
    }

    [HttpGet("template/{notificationType}")]
    public async ValueTask<IActionResult> Get(NotificationType notificationType)
    {
        var result = await emailTemplateService.GetByTypeAsync(notificationType);
        return result is not null ? Ok(result) : BadRequest();
    }
}
