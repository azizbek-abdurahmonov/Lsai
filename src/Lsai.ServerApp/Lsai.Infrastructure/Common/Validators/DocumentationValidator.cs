using FluentValidation;
using Lsai.Domain.Entities;

namespace Lsai.Infrastructure.Common.Validators;

public class DocumentationValidator : AbstractValidator<DocumentationModel>
{
    public DocumentationValidator()
    {
        RuleFor(doc => doc.Title).MinimumLength(5).MaximumLength(512);
        RuleFor(doc => doc.Technology).MinimumLength(1).MaximumLength(256);
    }
}
