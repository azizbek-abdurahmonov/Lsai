using Lsai.Domain.Entities;

namespace Lsai.Application.Common.QASection.Services;

public interface IQuestionOptionService
{
    IQueryable<QuestionOption> GetByQuestionId(Guid id);
}
