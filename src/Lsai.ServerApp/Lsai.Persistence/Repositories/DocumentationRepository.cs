using Lsai.Domain.Common.Exceptions;
using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class DocumentationRepository(AppDbContext dbContext)
    : EntityRepositoryBase<DocumentationModel>(dbContext), IDocumentationRepository
{
    public new ValueTask<DocumentationModel> CreateAsync(DocumentationModel documentation, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(documentation, saveChanges, cancellationToken);

    public new IQueryable<DocumentationModel> Get(Expression<Func<DocumentationModel, bool>>? predicate = null, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<DocumentationModel?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<DocumentationModel> UpdateAsync(DocumentationModel documentation, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEntity = dbContext.Documentations.FirstOrDefault(dbDocumentation => dbDocumentation.Id == documentation.Id)
            ?? throw new EntityNotFoundException(typeof(DocumentationModel));

        foundEntity.Title = documentation.Title;
        foundEntity.Technology = documentation.Technology;
        foundEntity.UserId = documentation.UserId;

        return base.UpdateAsync(foundEntity, saveChanges, cancellationToken);
    }

    public new ValueTask<DocumentationModel> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}