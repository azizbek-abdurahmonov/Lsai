using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IDocumentationLikeRepository
{
    IQueryable<DocumentationLike> Get(Expression<Func<DocumentationLike, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<DocumentationLike?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<DocumentationLike> CreateAsync(DocumentationLike documentationLike, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<DocumentationLike> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
