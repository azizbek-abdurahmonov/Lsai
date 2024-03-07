using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IQuestionOptionRepository
{
    ValueTask<QuestionOption> CreateAsync(QuestionOption questionOption, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<QuestionOption> Get(Expression<Func<QuestionOption, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<QuestionOption?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<QuestionOption> UpdateAsync(QuestionOption questionOption, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<QuestionOption> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}
