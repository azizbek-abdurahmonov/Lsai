using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IDocumentationPartRepository
{
    ValueTask<DocumentationPart> CreateAsync(DocumentationPart documentationPart, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<DocumentationPart> Get(Expression<Func<DocumentationPart, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<DocumentationPart?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<DocumentationPart> UpdateAsync(DocumentationPart documentationPart, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<DocumentationPart> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
