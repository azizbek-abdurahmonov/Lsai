using Lsai.Domain.Common.Exceptions;
using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class DocumentationPartRepository(AppDbContext dbContext)
    : EntityRepositoryBase<DocumentationPart>(dbContext), IDocumentationPartRepository
{
    public new ValueTask<DocumentationPart> CreateAsync(DocumentationPart documentationPart, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(documentationPart, saveChanges, cancellationToken);

    public new IQueryable<DocumentationPart> Get(Expression<Func<DocumentationPart, bool>>? predicate = null, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<DocumentationPart?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<DocumentationPart> UpdateAsync(DocumentationPart documentationPart, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEntity = dbContext.DocumentationParts
            .FirstOrDefault(docPart => docPart.Id == documentationPart.Id)
            ?? throw new EntityNotFoundException(typeof(DocumentationPart));

        foundEntity.Title = documentationPart.Title;
        foundEntity.Content = documentationPart.Content;
        foundEntity.DocumentationId = documentationPart.Id;

        return base.UpdateAsync(foundEntity, saveChanges, cancellationToken);
    }

    public new ValueTask<DocumentationPart> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}
