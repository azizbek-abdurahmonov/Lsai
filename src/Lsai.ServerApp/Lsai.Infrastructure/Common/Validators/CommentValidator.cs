using FluentValidation;
using Lsai.Domain.Entities;

namespace Lsai.Infrastructure.Common.Validators;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(comment => comment.Content)
            .MinimumLength(1)
            .MaximumLength(1024);
    }
}
