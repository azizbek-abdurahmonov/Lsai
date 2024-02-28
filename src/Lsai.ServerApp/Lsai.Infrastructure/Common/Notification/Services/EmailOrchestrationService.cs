using Lsai.Application.Common.Identity.Services;
using Lsai.Application.Common.Notification.Services;
using Lsai.Domain.Common.Notification;
using Lsai.Application.Common.Notification.Models;

namespace Lsai.Infrastructure.Common.Notification.Services;

public class EmailOrchestrationService(
    IEmailSenderService emailSenderService)
    : IEmailOrchestrationService
{
    public async ValueTask<bool> SendAsync(EmailRequest emailRequest, CancellationToken cancellationToken)
    {
        // rendering message

        var result = emailSenderService.SendEmail(
            new EmailMessage()
            {
                ReceiverEmail = emailRequest.ReceiverEmail,
                Subject = emailRequest.EmailTemplate.Subject,
                Body = emailRequest.EmailTemplate.Body
            },
            emailRequest.Variables);

        return result;
    }
}
