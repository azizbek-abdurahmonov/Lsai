using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IUserAnswerRepository
{
    ValueTask<UserAnswer> CreateAsync(UserAnswer answer, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<UserAnswer> Get(Expression<Func<UserAnswer, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<UserAnswer?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
}
