using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IDocumentationRepository
{
    ValueTask<DocumentationModel> CreateAsync(DocumentationModel documentation, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<DocumentationModel> Get(Expression<Func<DocumentationModel, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<DocumentationModel?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<DocumentationModel> UpdateAsync(DocumentationModel documentation, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<DocumentationModel> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
