using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Application.Common.Identity.Services;

public interface IUserService
{
    ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default,PaginationOptions? paginationOptions = default, bool asNoTracking = false);

    ValueTask<User> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
