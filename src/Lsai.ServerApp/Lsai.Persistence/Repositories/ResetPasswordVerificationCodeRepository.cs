using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class ResetPasswordVerificationCodeRepository(AppDbContext dbContext)
    : EntityRepositoryBase<ResetPasswordVerificationCode>(dbContext), IResetPasswordVerificationCodeRepository
{
    public ValueTask<ResetPasswordVerificationCode> CreateAsync(ResetPasswordVerificationCode resetPasswordVerificationCode, CancellationToken cancellationToken = default)
        => base.CreateAsync(resetPasswordVerificationCode, true, cancellationToken);

    public IQueryable<ResetPasswordVerificationCode> Get(Expression<Func<ResetPasswordVerificationCode, bool>> predicate, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);
}
