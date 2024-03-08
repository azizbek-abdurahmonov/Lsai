using Lsai.Domain.Entities;

namespace Lsai.Application.Common.Notification.Services;

public interface IFutureMailService
{
    ValueTask<FutureMail> CreateAsync(FutureMail message, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<FutureMail> DeleteByIdAsync(Guid mailId, bool saveChanges = true, CancellationToken cancellationToken = default);
}
