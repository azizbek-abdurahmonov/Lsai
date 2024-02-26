using FluentValidation;
using Lsai.Application.Common.Identity.Services;
using Lsai.Domain.Common.Constants;
using Lsai.Domain.Common.Extensions;
using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Infrastructure.Common.Identity.Services;

public class UserService(IUserRepository userRepository, IValidator<User> validator) : IUserService
{
    public ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(user, EntityEvent.OnCreated);
        return userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate = null, PaginationOptions? paginationOptions = default, bool asNoTracking = false)
        => paginationOptions is null ? userRepository.Get(predicate, asNoTracking) : userRepository.Get(predicate, asNoTracking).ApplyPagination(paginationOptions);

    public ValueTask<User?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => userRepository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(user, EntityEvent.OnUpdated);

        return userRepository.UpdateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => userRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);

    private void Validate(User user, string ruleSet)
    {
        var validationResult = validator.Validate(user, options => options.IncludeRuleSets(ruleSet));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}
