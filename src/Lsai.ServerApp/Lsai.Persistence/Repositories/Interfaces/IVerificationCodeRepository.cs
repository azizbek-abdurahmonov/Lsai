using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IVerificationCodeRepository
{
    ValueTask<VerificationCode> CreateAsync(VerificationCode verificationCode, CancellationToken cancellationToken = default);

    IQueryable<VerificationCode> Get(Expression<Func<VerificationCode, bool>> predicate, bool asNoTracking = false);
}
