using Lsai.Domain.Common.Exceptions;
using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class UserRepository(AppDbContext dbContext)
    : EntityRepositoryBase<User>(dbContext), IUserRepository
{
    public new ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(user, saveChanges, cancellationToken);

    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate = null, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<User?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new async ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEntity = await dbContext.Users.FirstOrDefaultAsync(dbUser => dbUser.Id == user.Id, cancellationToken)
            ?? throw new EntityNotFoundException(typeof(User));

        foundEntity.FirstName = user.FirstName;
        foundEntity.LastName = user.LastName;
        foundEntity.Email = user.Email;
        foundEntity.IsVerified = user.IsVerified;

        return await base.UpdateAsync(foundEntity, saveChanges, cancellationToken);
    }

    public new ValueTask<User> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}
