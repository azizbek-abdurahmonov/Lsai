using Lsai.Domain.Common.Exceptions;
using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class CommentRepository(AppDbContext dbContext)
    : EntityRepositoryBase<Comment>(dbContext), ICommentRepository
{
    public new IQueryable<Comment> Get(Expression<Func<Comment, bool>>? predicate = null, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<Comment?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(comment, saveChanges, cancellationToken);

    public new ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEntity = dbContext.Comments
            .FirstOrDefault(dbComment => dbComment.Id == comment.Id)
            ?? throw new EntityNotFoundException(typeof(Comment));

        foundEntity.Content = comment.Content;
        foundEntity.DocumentationId = comment.DocumentationId;
        foundEntity.ParentId = comment.ParentId;

        return base.UpdateAsync(foundEntity, saveChanges, cancellationToken);
    }

    public new ValueTask<Comment> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}