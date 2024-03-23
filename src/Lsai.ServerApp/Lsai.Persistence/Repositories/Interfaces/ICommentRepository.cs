using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface ICommentRepository
{
    IQueryable<Comment> Get(Expression<Func<Comment, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<Comment?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Comment> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
