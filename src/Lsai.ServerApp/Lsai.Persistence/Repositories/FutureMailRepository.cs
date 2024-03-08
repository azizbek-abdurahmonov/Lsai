using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;

namespace Lsai.Persistence.Repositories;

public class FutureMailRepository(AppDbContext dbContext)
    : EntityRepositoryBase<FutureMail>(dbContext), IFutureMailRepository
{
    public new ValueTask<FutureMail> CreateAsync(FutureMail futureMail, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(futureMail, saveChanges, cancellationToken);

    public new ValueTask<FutureMail> DeleteByIdAsync(Guid mailId, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(mailId, saveChanges, cancellationToken);

    public IQueryable<FutureMail> Get()
        => base.Get();
}
