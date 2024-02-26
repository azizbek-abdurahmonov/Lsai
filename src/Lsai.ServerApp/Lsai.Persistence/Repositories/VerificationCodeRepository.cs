using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class VerificationCodeRepository(AppDbContext dbContext)
    : EntityRepositoryBase<VerificationCode>(dbContext), IVerificationCodeRepository
{
    public ValueTask<VerificationCode> CreateAsync(VerificationCode verificationCode, CancellationToken cancellationToken = default)
        => base.CreateAsync(verificationCode, true, cancellationToken);

    public IQueryable<VerificationCode> Get(Expression<Func<VerificationCode, bool>> predicate, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);
}
