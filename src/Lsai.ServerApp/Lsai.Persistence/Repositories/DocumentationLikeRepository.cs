using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class DocumentationLikeRepository(AppDbContext dbContext)
    : EntityRepositoryBase<DocumentationLike>(dbContext), IDocumentationLikeRepository
{
    public new IQueryable<DocumentationLike> Get(Expression<Func<DocumentationLike, bool>>? predicate = null, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<DocumentationLike?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<DocumentationLike> CreateAsync(DocumentationLike documentationLike, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(documentationLike, saveChanges, cancellationToken);

    public new ValueTask<DocumentationLike> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}
