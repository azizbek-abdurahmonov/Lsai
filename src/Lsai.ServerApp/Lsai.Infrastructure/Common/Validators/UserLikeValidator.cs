using FluentValidation;
using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace Lsai.Infrastructure.Common.Validators;

public class UserLikeValidator : AbstractValidator<DocumentationLike>
{
    public UserLikeValidator(IServiceScopeFactory serviceScopeFactory)
    {
        var provider = serviceScopeFactory.CreateScope().ServiceProvider;
        var dbContext = provider.GetRequiredService<AppDbContext>();

        RuleFor(documentationLike => documentationLike).Custom((DocumentationLike like, ValidationContext<DocumentationLike> context) =>
        {
            if (dbContext.DocumentationLikes.Any(dbLike =>
                !dbLike.IsDeleted &&
                dbLike.UserId == like.UserId &&
                dbLike.DocumentationId == like.DocumentationId))
            {
                context.AddFailure("User already liked this documentation");
            }
        });
    }
}
