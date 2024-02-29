using Lsai.Application.Common.Notification.Models;
using Lsai.Domain.Common.Notification;

namespace Lsai.Application.Common.Notification.Services;

public interface IEmailRenderService
{
    ValueTask<EmailMessage> RenderAsync(EmailRequest emailRequest, bool renderSubject = true, bool renderBody = true, CancellationToken cancellationToken = default);
}
