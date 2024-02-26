using Lsai.Domain.Common.Exceptions;
using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class UserCredentialsRepository(AppDbContext dbContext)
    : EntityRepositoryBase<UserCredentials>(dbContext), IUserCredentialsRepository
{
    public new ValueTask<UserCredentials> CreateAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(userCredentials, saveChanges, cancellationToken);

    public new IQueryable<UserCredentials> Get(Expression<Func<UserCredentials, bool>>? predicate = null, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<UserCredentials?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<UserCredentials> UpdateAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEntity = dbContext.UserCredentials.FirstOrDefault(dbUserCredentials => dbUserCredentials.Id == userCredentials.Id)
            ?? throw new EntityNotFoundException(typeof(UserCredentials));

        foundEntity.PasswordHash = userCredentials.PasswordHash;
        foundEntity.UserId = userCredentials.UserId;

        return base.UpdateAsync(foundEntity, saveChanges, cancellationToken);
    }

    public new ValueTask<UserCredentials> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}
