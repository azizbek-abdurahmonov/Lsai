using FluentValidation;
using Lsai.Application.Common.Documentation.Services;
using Lsai.Domain.Common.Extensions;
using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;

namespace Lsai.Infrastructure.Common.Documentation.Services;

public class DocumentationLikeService(
    IDocumentationLikeRepository documentationLikeRepository,
    IValidator<DocumentationLike> validator) : IDocumentationLikeService
{
    public ValueTask<DocumentationLike> CreateAsync(DocumentationLike documentationLike, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var validationResult = validator.Validate(documentationLike);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return documentationLikeRepository.CreateAsync(documentationLike, saveChanges, cancellationToken);
    }

    public ValueTask<int> GetLikeCountByIdAsync(Guid documentationId, CancellationToken cancellationToken = default)
    {
        var likes = documentationLikeRepository.Get(like => like.DocumentationId == documentationId);

        return new(likes.Count());
    }

    public IQueryable<DocumentationLike> Get(PaginationOptions? paginationOptions = default)
    {
        return paginationOptions is not null
            ? documentationLikeRepository.Get().ApplyPagination(paginationOptions)
            : documentationLikeRepository.Get();
    }

    public IQueryable<DocumentationLike> GetUserLikes(Guid userId, PaginationOptions? paginationOptions = null)
    {
        var likes = documentationLikeRepository.Get(like => like.UserId == userId);

        return paginationOptions is not null
            ? likes.ApplyPagination(paginationOptions)
            : likes;
    }

    public IQueryable<DocumentationLike> GetDocumentationLikes(Guid documentationId, PaginationOptions? paginationOptions = null)
    {
        var likes = documentationLikeRepository.Get(like => like.DocumentationId == documentationId);

        return paginationOptions is not null
            ? likes.ApplyPagination(paginationOptions)
            : likes;
    }
}
