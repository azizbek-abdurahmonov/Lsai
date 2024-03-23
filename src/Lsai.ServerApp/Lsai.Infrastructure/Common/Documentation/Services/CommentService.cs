using FluentValidation;
using Lsai.Application.Common.Documentation.Services;
using Lsai.Application.Common.Identity.Services;
using Lsai.Domain.Common.Extensions;
using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using Lsai.Domain.Common.Enums;

namespace Lsai.Infrastructure.Common.Documentation.Services;

public class CommentService(
    ICommentRepository commentRepository,
    IUserService userService,
    IValidator<Comment> validator) : ICommentService
{
    public ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(comment);
        return commentRepository.CreateAsync(comment, saveChanges, cancellationToken);
    }

    public ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(comment);
        return commentRepository.UpdateAsync(comment, saveChanges, cancellationToken);
    }

    public IQueryable<Comment> Get(PaginationOptions paginationOptions)
    {
        var comments = commentRepository.Get().ApplyPagination(paginationOptions);
        return comments;
    }

    public IQueryable<Comment> GetCommentsByDocumentationId(Guid id, PaginationOptions paginationOptions, bool asNoTracking = false)
    {
        var comments = commentRepository.Get(comment =>
            comment.DocumentationId == id, asNoTracking).ApplyPagination(paginationOptions);

        return comments;
    }

    public ValueTask<int> GetCommentsCountByDocumentationIdAsync(Guid documentationId, CancellationToken cancellationToken = default)
    {
        var comments = commentRepository.Get(comment =>
           comment.DocumentationId == documentationId);

        return new(comments.Count());
    }


    private void Validate(Comment comment)
    {
        var validationResult = validator.Validate(comment);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }

    public async ValueTask<Comment> DeletByIdAsync(Guid commentId, Guid userId, CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetByIdAsync(commentId, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Comment not found with this Id!");

        var user = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken);

        if (user.Role == Role.User && comment.UserId != userId)
            throw new InvalidOperationException("The user doesn't have access to delete the comment!");

        return await commentRepository.DeleteByIdAsync(commentId, cancellationToken: cancellationToken);
    }
}
