using Lsai.Domain.Common.Exceptions;
using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class QuestionRepository(AppDbContext dbContext)
    : EntityRepositoryBase<QuestionModel>(dbContext), IQuestionRepository
{
    public new ValueTask<QuestionModel> CreateAsync(QuestionModel question, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(question, saveChanges, cancellationToken);

    public new IQueryable<QuestionModel> Get(Expression<Func<QuestionModel, bool>>? predicate = null, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<QuestionModel?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<QuestionModel> UpdateAsync(QuestionModel question, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEntity = dbContext.Questions.FirstOrDefault(dbQuestion => dbQuestion.Id == question.Id)
            ?? throw new EntityNotFoundException(typeof(QuestionModel));

        foundEntity.Question = question.Question;
        foundEntity.DocumentationPartId = question.DocumentationPartId;
        return base.UpdateAsync(foundEntity, saveChanges, cancellationToken);
    }

    public new ValueTask<QuestionModel> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(id, saveChanges, cancellationToken);
}

