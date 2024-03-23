using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;

namespace Lsai.Application.Common.Documentation.Services;

public interface ICommentService
{
    ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<Comment> Get(PaginationOptions paginationOptions);

    ValueTask<Comment> DeletByIdAsync(Guid commentId, Guid userId, CancellationToken cancellationToken = default);

    ValueTask<int> GetCommentsCountByDocumentationIdAsync(Guid documentationId, CancellationToken cancellationToken = default);

    IQueryable<Comment> GetCommentsByDocumentationId(Guid documentationId,PaginationOptions paginationOptions, bool asNoTracking = false);
}
