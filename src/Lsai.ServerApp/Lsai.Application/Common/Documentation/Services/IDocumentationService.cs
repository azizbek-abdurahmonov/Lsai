using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Application.Common.Documentation.Services;

public interface IDocumentationService
{
    ValueTask<DocumentationModel> CreateAsync(DocumentationModel documentation, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<DocumentationModel> Get(Expression<Func<DocumentationModel, bool>>? predicate = default,PaginationOptions? paginationOptions = default, bool asNoTracking = false);

    ValueTask<DocumentationModel?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<DocumentationModel> UpdateAsync(DocumentationModel documentation, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<DocumentationModel> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
