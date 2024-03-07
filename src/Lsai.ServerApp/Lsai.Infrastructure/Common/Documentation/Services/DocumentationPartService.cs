using FluentValidation;
using Lsai.Application.Common.Documentation.Services;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Infrastructure.Common.Documentation.Services;

public class DocumentationPartService
    (IDocumentationPartRepository documentationPartRepository, IValidator<DocumentationPart> validator) : IDocumentationPartService
{
    public ValueTask<DocumentationPart> CreateAsync(DocumentationPart documentationPart, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(documentationPart);
        return documentationPartRepository.CreateAsync(documentationPart, saveChanges, cancellationToken);
    }

    public IQueryable<DocumentationPart> Get(Expression<Func<DocumentationPart, bool>>? predicate = null, bool asNoTracking = false)
        => documentationPartRepository.Get(predicate, asNoTracking);

    public ValueTask<DocumentationPart?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => documentationPartRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<DocumentationPart> UpdateAsync(DocumentationPart documentationPart, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(documentationPart);
        return documentationPartRepository.UpdateAsync(documentationPart, saveChanges, cancellationToken);
    }

    public ValueTask<DocumentationPart> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => documentationPartRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);

    private void Validate(DocumentationPart documentationPart)
    {
        var validationResult = validator.Validate(documentationPart);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }

    public IQueryable<DocumentationPart> GetByDocumentationId(Guid id)
    {
        var documentationParts = documentationPartRepository
            .Get()
            .Where(documentationPart => documentationPart.DocumentationId == id);

        return documentationParts;
    }
}
