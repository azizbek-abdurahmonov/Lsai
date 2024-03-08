using Lsai.Application.Common.Notification.Services;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;

namespace Lsai.Infrastructure.Common.Notification.Services;

public class FutureMailService(IFutureMailRepository futureMailRepository) : IFutureMailService
{
    public ValueTask<FutureMail> CreateAsync(FutureMail message, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(message.Subject))
            throw new Exception("The message title must not be empty!");

        if (string.IsNullOrWhiteSpace(message.Subject))
            throw new Exception("The message body must not be empty!");

        return futureMailRepository.CreateAsync(message, saveChanges, cancellationToken);
    }

    public ValueTask<FutureMail> DeleteByIdAsync(Guid mailId, bool saveChanges = true, CancellationToken cancellationToken = default)
        => futureMailRepository.DeleteByIdAsync(mailId, saveChanges, cancellationToken);
}
