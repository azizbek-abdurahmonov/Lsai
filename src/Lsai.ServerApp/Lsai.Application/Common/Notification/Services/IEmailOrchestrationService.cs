using Lsai.Domain.Common.Notification;

namespace Lsai.Application.Common.Identity.Services;

public interface IEmailOrchestrationService
{
    ValueTask<bool> SendAsync(EmailRequest emailRequest, CancellationToken cancellationToken = default);  
}
