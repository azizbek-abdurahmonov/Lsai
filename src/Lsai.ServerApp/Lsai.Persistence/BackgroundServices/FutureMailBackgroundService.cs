using Lsai.Application.Common.Notification.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lsai.Persistence.BackgroundServices;

public class FutureMailBackgroundService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            var futureMailOrchestrationService = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IFutureMailOrchestrationService>();
            await futureMailOrchestrationService.SendAsync(stoppingToken);
            await Task.Delay(60000);
        }
    }
}
