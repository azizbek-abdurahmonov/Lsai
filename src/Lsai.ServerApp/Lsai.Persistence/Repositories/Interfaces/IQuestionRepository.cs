using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IQuestionRepository
{
    ValueTask<QuestionModel> CreateAsync(QuestionModel question, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<QuestionModel> Get(Expression<Func<QuestionModel, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<QuestionModel?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<QuestionModel> UpdateAsync(QuestionModel question, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<QuestionModel> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
}

