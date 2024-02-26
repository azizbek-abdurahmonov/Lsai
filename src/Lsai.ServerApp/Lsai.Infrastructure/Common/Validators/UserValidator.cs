using FluentValidation;
using Lsai.Domain.Common.Constants;
using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using System.Text.RegularExpressions;

namespace Lsai.Infrastructure.Common.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator(AppDbContext dbContext)
    {
        RuleSet(EntityEvent.OnCreated.ToString(), () =>
        {
            RuleFor(user => user.FirstName).NotEmpty().MinimumLength(3).MaximumLength(124);
            RuleFor(user => user.LastName).NotEmpty().MinimumLength(3).MaximumLength(124);
            RuleFor(user => user.Email).Custom((email, context) =>
            {
                if (!Regex.IsMatch(email, RegexConstants.EmailRegex))
                    context.AddFailure(nameof(User.Email), "Invalid Email addres");

                if (dbContext.Users.Any(user => user.Email == email))
                    context.AddFailure(nameof(User.Email), "This email already used");
            });
        });

        RuleSet(EntityEvent.OnUpdated.ToString(), () =>
        {
            RuleFor(user => user.FirstName).NotEmpty().MinimumLength(3).MaximumLength(124);
            RuleFor(user => user.LastName).NotEmpty().MinimumLength(3).MaximumLength(124);
            RuleFor(user => user.Email).Custom((email, context) =>
            {
                if (!Regex.IsMatch(email, RegexConstants.EmailRegex))
                    context.AddFailure(nameof(User.Email), "Invalid Email addres");
            });
        });
    }
}
