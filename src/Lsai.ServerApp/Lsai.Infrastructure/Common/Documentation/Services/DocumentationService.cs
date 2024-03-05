using FluentValidation;
using Lsai.Application.Common.Documentation.Services;
using Lsai.Domain.Common.Extensions;
using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Infrastructure.Common.Documentation.Services;

public class DocumentationService(
    IDocumentationRepository documentationRepository,
    IValidator<DocumentationModel> validator) : IDocumentationService
{
    public ValueTask<DocumentationModel> CreateAsync(DocumentationModel documentation, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(documentation);
        return documentationRepository.CreateAsync(documentation, saveChanges, cancellationToken);
    }

    public IQueryable<DocumentationModel> Get(Expression<Func<DocumentationModel, bool>>? predicate = null, PaginationOptions? paginationOptions = null, bool asNoTracking = false)
        => paginationOptions is null
            ? documentationRepository.Get(predicate, asNoTracking)
            : documentationRepository.Get(predicate, asNoTracking).ApplyPagination(paginationOptions);

    public ValueTask<DocumentationModel?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => documentationRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<DocumentationModel> UpdateAsync(DocumentationModel documentation, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(documentation);
        return documentationRepository.UpdateAsync(documentation, saveChanges, cancellationToken);
    }

    public ValueTask<DocumentationModel> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => documentationRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);

    private void Validate(DocumentationModel documentationModel)
    {
        var validationResult = validator.Validate(documentationModel);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}
