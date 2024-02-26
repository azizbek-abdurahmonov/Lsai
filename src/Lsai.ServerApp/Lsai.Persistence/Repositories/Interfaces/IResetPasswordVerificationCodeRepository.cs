using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IResetPasswordVerificationCodeRepository
{
    ValueTask<ResetPasswordVerificationCode> CreateAsync(ResetPasswordVerificationCode resetPasswordVerificationCode, CancellationToken cancellationToken = default);

    IQueryable<ResetPasswordVerificationCode> Get(Expression<Func<ResetPasswordVerificationCode, bool>> predicate, bool asNoTracking = false);
}
