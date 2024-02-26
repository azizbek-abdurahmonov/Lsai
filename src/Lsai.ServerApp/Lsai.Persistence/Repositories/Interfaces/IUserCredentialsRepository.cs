using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IUserCredentialsRepository
{
    ValueTask<UserCredentials> CreateAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<UserCredentials> Get(Expression<Func<UserCredentials, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<UserCredentials?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> UpdateAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
