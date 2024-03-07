using Lsai.Domain.Common.Exceptions;
using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class QuestionOptionRepository(AppDbContext dbContext)
    : EntityRepositoryBase<QuestionOption>(dbContext), IQuestionOptionRepository
{
    public new ValueTask<QuestionOption> CreateAsync(QuestionOption questionOption, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(questionOption, saveChanges, cancellationToken);

    public new IQueryable<QuestionOption> Get(Expression<Func<QuestionOption, bool>>? predicate = null, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<QuestionOption?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<QuestionOption> UpdateAsync(QuestionOption questionOption, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEntity = dbContext.QuestionOptions
            .FirstOrDefault(option => option.Id == questionOption.Id)
            ?? throw new EntityNotFoundException(typeof(QuestionOption));

        foundEntity.Answer = questionOption.Answer;
        foundEntity.IsCorrect = questionOption.IsCorrect;
        foundEntity.QuestionId = questionOption.QuestionId;

        return base.UpdateAsync(foundEntity, saveChanges, cancellationToken);
    }

    public new ValueTask<QuestionOption> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}
