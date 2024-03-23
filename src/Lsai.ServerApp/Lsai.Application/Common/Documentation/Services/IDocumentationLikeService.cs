using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;

namespace Lsai.Application.Common.Documentation.Services;

public interface IDocumentationLikeService
{
    ValueTask<DocumentationLike> CreateAsync(DocumentationLike documentationLike, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<int> GetLikeCountByIdAsync(Guid documentationId, CancellationToken cancellationToken = default);

    IQueryable<DocumentationLike> Get(PaginationOptions? paginationOptions = default);

    IQueryable<DocumentationLike> GetUserLikes(Guid userId, PaginationOptions? paginationOptions = default);

    IQueryable<DocumentationLike> GetDocumentationLikes(Guid documentationId, PaginationOptions? paginationOptions = default);

}
