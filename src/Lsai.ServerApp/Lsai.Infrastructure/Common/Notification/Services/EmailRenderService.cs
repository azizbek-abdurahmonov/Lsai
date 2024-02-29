using Lsai.Application.Common.Notification.Models;
using Lsai.Application.Common.Notification.Services;
using Lsai.Domain.Common.Notification;

namespace Lsai.Infrastructure.Common.Notification.Services;

public class EmailRenderService : IEmailRenderService
{
    public ValueTask<EmailMessage> RenderAsync(EmailRequest emailRequest, bool renderSubject = true, bool renderBody = true, CancellationToken cancellationToken = default)
    {
        string renderedSubject = emailRequest.EmailTemplate.Subject;
        string renderedBody = emailRequest.EmailTemplate.Body;

        foreach (var variable in emailRequest.Variables)
        {
            if (renderSubject)
                renderedSubject = renderedSubject.Replace(variable.Key, variable.Value);

            if (renderBody)
                renderedBody = renderedBody.Replace(variable.Key, variable.Value);
        }

        return new(new EmailMessage()
        {
            ReceiverEmail = emailRequest.ReceiverEmail,
            Subject = renderedSubject,
            Body = renderedBody
        });
    }
}
