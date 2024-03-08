namespace Lsai.Application.Common.Notification.Services;

public interface IFutureMailOrchestrationService
{
    ValueTask SendAsync(CancellationToken cancellationToken = default);
}
