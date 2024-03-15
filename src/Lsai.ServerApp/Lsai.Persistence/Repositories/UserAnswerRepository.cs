using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class UserAnswerRepository(AppDbContext dbContext)
    : EntityRepositoryBase<UserAnswer>(dbContext), IUserAnswerRepository
{
    public new ValueTask<UserAnswer> CreateAsync(UserAnswer answer, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(answer, saveChanges, cancellationToken);

    public new IQueryable<UserAnswer> Get(Expression<Func<UserAnswer, bool>>? predicate = null, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<UserAnswer?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(id, asNoTracking, cancellationToken);
}
