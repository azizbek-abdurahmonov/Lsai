using Lsai.Domain.Entities;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IFutureMailRepository
{
    ValueTask<FutureMail> CreateAsync(FutureMail futureMail, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<FutureMail> Get();

    ValueTask<FutureMail> DeleteByIdAsync(Guid mailId, bool saveChanges = true, CancellationToken cancellationToken = default);
}
