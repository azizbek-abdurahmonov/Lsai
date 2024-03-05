using FluentValidation;
using Lsai.Domain.Entities;

namespace Lsai.Infrastructure.Common.Validators;

public class DocumentationPartValidator : AbstractValidator<DocumentationPart>
{
    public DocumentationPartValidator()
    {
        RuleFor(docPart => docPart.Title)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(512);

        RuleFor(docPart => docPart.Content)
            .NotEmpty()
            .MinimumLength(3);
    }
}
