using Lsai.Application.Common.Identity.Services;
using Lsai.Application.Common.Notification.Services;
using Lsai.Domain.Common.Notification;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lsai.Infrastructure.Common.Notification.Services;

public class EmailOrchestrationService(
    IEmailSenderService emailSenderService,
    IEmailRenderService emailRenderService,
    IEmailTemplateService emailTemplateService)
    : IEmailOrchestrationService
{
    public async ValueTask<bool> SendAsync(EmailRequest emailRequest, CancellationToken cancellationToken)
    {
        if (emailRequest.EmailTemplate is null)
            emailRequest.EmailTemplate = await emailTemplateService.GetByTypeAsync(emailRequest.NotificationType);

        //render
        var emailMessage = await emailRenderService.RenderAsync(emailRequest);

        //send
        var result = emailSenderService.SendEmail(emailMessage);

        return result;
    }
}
