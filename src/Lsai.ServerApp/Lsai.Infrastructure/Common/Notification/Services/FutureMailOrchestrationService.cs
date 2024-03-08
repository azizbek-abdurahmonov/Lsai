using Lsai.Application.Common.Notification.Models;
using Lsai.Application.Common.Notification.Services;
using Lsai.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lsai.Infrastructure.Common.Notification.Services;

public class FutureMailOrchestrationService(
    IFutureMailRepository futureMailRepository,
    IEmailSenderService emailSenderService,
    IFutureMailService futureMailService) : IFutureMailOrchestrationService
{
    public async ValueTask SendAsync(CancellationToken cancellationToken = default)
    {
        var futureMails = futureMailRepository.Get()
            .Where(mail => (!mail.IsDeleted) && (mail.Date > DateTime.UtcNow && mail.Date < DateTime.UtcNow.AddMinutes(1)));

        await futureMails.ForEachAsync(futureMail =>
        {
            var emailMessage = new EmailMessage
            {
                ReceiverEmail = futureMail.ReceiverEmail,
                Subject = futureMail.Subject,
                Body = futureMail.Body,
            };

            var result = emailSenderService.SendEmail(emailMessage);

            if (result)
                futureMailService.DeleteByIdAsync(futureMail.Id);

        }, cancellationToken: cancellationToken);
    }
}
